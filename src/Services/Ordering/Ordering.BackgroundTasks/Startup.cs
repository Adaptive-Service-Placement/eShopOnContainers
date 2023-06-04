﻿namespace Ordering.BackgroundTasks
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Ordering.BackgroundTasks.Extensions;
    using Ordering.BackgroundTasks.Services;
    using Ordering.BackgroundTasks.Events;
    using Ordering.BackgroundTasks.EventHandling;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomHealthCheck(this.Configuration)
                .Configure<BackgroundTaskSettings>(this.Configuration)
                .AddOptions()
                .AddHostedService<GracePeriodManagerService>()
                .AddEventBus(this.Configuration);
        }


        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions.IEventBus>();

            eventBus.Subscribe<RandomCatalogBackgroundEvent, RandomCatalogBackgroundEventHandler>();
            eventBus.Subscribe<RandomOrderingBackgroundEvent, RandomOrderingBackgroundEventHandler>();
            eventBus.Subscribe<RandomWebhookBackgroundEvent, RandomWebhookBackgroundEventHandler>();
        }
    }
}

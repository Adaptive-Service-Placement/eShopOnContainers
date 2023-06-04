namespace Ordering.BackgroundTasks
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
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
    using Autofac;
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;

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
                .AddHostedService<GracePeriodManagerService>();

            RegisterEventBus(services);
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

        private void RegisterEventBus(IServiceCollection services)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    }
    }
}

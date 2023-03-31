#!/usr/bin/env bash

echo "####### Release sql-data ######"
helm uninstall sql-data-chart

echo "####### Release rabbitmq ######"
helm uninstall rabbitmq-chart

echo "####### Release apigwms ######"
helm uninstall apigwms-chart

echo "####### Release apigwws ######"
helm uninstall apigwws-chart

echo "####### Release basket-api ######"
helm uninstall basket-chart

echo "####### Release basket-data ######"
helm uninstall basket-data-chart

echo "####### Release catalog-api ######"
helm uninstall catalog-chart

echo "####### Release eshop-common ######"
helm uninstall eshop-common-chart

echo "####### Release identity-api ######"
helm uninstall identity-chart

echo "####### Release keystore-data ######"
helm uninstall keystore-chart

echo "####### Release mobileshoppingagg ######"
helm uninstall mobileshoppingagg-chart

echo "####### Release nosql-data ######"
helm uninstall nosql-chart

echo "####### Release ordering-api ######"
helm uninstall ordering-chart

echo "####### Release ordering-backgroundtasks ######"
helm uninstall ordering-backgroundtasks-chart

echo "####### Release ordering-signalrhub ######"
helm uninstall ordering-signalrhub-chart

echo "####### Release payment-api ######"
helm uninstall payment-chart

echo "####### Release tls-support ######"
helm uninstall tls-support-chart

echo "####### Release webhooks-api ######"
helm uninstall webhooks-chart

echo "####### Release webhooks-web ######"
helm uninstall webhooks-web-chart

echo "####### Release webmvc ######"
helm uninstall webmvc-chart

echo "####### Release webshoppingagg ######"
helm uninstall webshoppingagg-chart

echo "####### Release webspa ######"
helm uninstall webspa-chart

echo "####### Release webstatus ######"
helm uninstall webstatus-chart
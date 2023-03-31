#!/usr/bin/env bash

echo "####### Release eshop-apigwms ######"
helm uninstall eshop-apigwms

echo "####### Release eshop-apigwws ######"
helm uninstall eshop-apigwws

echo "####### Release eshop-basket-api ######"
helm uninstall eshop-basket-api

echo "####### Release eshop-basket-data ######"
helm uninstall eshop-basket-data

echo "####### Release eshop-catalog-api ######"
helm uninstall eshop-catalog-api

echo "####### Release eshop-identity-api ######"
helm uninstall eshop-identity-api

echo "####### Release eshop-keystore-data ######"
helm uninstall eshop-keystore-data

echo "####### Release eshop-mobileshoppingagg ######"
helm uninstall eshop-mobileshoppingagg

echo "####### Release eshop-nosql-data ######"
helm uninstall eshop-nosql-data

echo "####### Release eshop-ordering-api ######"
helm uninstall eshop-ordering-api

echo "####### Release eshop-ordering-backgroundtasks ######"
helm uninstall eshop-ordering-backgroundtasks

echo "####### Release eshop-ordering-signalrhub ######"
helm uninstall eshop-ordering-signalrhub

echo "####### Release eshop-payment-api ######"
helm uninstall eshop-payment-api

echo "####### Release eshop-rabbitmq ######"
helm uninstall eshop-rabbitmq

echo "####### Release eshop-sql-data ######"
helm uninstall eshop-sql-data

echo "####### Release eshop-webhooks-api ######"
helm uninstall eshop-webhooks-api

echo "####### Release eshop-webhooks-web ######"
helm uninstall eshop-webhooks-web

echo "####### Release eshop-webmvc ######"
helm uninstall eshop-webmvc

echo "####### Release eshop-webshoppingagg ######"
helm uninstall eshop-webshoppingagg

echo "####### Release eshop-webspa ######"
helm uninstall eshop-webspa

echo "####### Release eshop-webstatus ######"
helm uninstall eshop-webstatus
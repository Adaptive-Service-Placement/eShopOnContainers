#!/usr/bin/env bash

echo "####### Deploying sql-data ######"
helm upgrade --install sql-data-chart deploy/k8s/helm/sql-data/ --values deploy/k8s/helm/sql-data/values.yaml

echo "####### Deploying rabbitmq ######"
helm upgrade --install rabbitmq-chart deploy/k8s/helm/rabbitmq/ --values deploy/k8s/helm/rabbitmq/values.yaml

echo "####### Deploying apigwms ######"
helm upgrade --install apigwms-chart deploy/k8s/helm/apigwms/ --values deploy/k8s/helm/apigwms/values.yaml

echo "####### Deploying apigwws ######"
helm upgrade --install apigwws-chart deploy/k8s/helm/apigwws/ --values deploy/k8s/helm/apigwws/values.yaml

echo "####### Deploying basket-api ######"
helm upgrade --install basket-chart deploy/k8s/helm/basket-api/ --values deploy/k8s/helm/basket-api/values.yaml

echo "####### Deploying basket-data ######"
helm upgrade --install basket-data-chart deploy/k8s/helm/basket-data/ --values deploy/k8s/helm/basket-data/values.yaml

echo "####### Deploying catalog-api ######"
helm upgrade --install catalog-chart deploy/k8s/helm/catalog-api/ --values deploy/k8s/helm/catalog-api/values.yaml

echo "####### Deploying eshop-common ######"
helm upgrade --install eshop-common-chart deploy/k8s/helm/eshop-common/ --values deploy/k8s/helm/eshop-common/values.yaml

echo "####### Deploying identity-api ######"
helm upgrade --install identity-chart deploy/k8s/helm/identity-api/ --values deploy/k8s/helm/identity-api/values.yaml

echo "####### Deploying keystore-data ######"
helm upgrade --install keystore-chart deploy/k8s/helm/keystore-data/ --values deploy/k8s/helm/keystore-data/values.yaml

echo "####### Deploying mobileshoppingagg ######"
helm upgrade --install mobileshoppingagg-chart deploy/k8s/helm/mobileshoppingagg/ --values deploy/k8s/helm/mobileshoppingagg/values.yaml

echo "####### Deploying nosql-data ######"
helm upgrade --install nosql-chart deploy/k8s/helm/nosql-data/ --values deploy/k8s/helm/nosql-data/values.yaml

echo "####### Deploying ordering-api ######"
helm upgrade --install ordering-chart deploy/k8s/helm/ordering-api/ --values deploy/k8s/helm/ordering-api/values.yaml

echo "####### Deploying ordering-backgroundtasks ######"
helm upgrade --install ordering-backgroundtasks-chart deploy/k8s/helm/ordering-backgroundtasks/ --values deploy/k8s/helm/ordering-backgroundtasks/values.yaml

echo "####### Deploying ordering-signalrhub ######"
helm upgrade --install ordering-signalrhub-chart deploy/k8s/helm/ordering-signalrhub/ --values deploy/k8s/helm/ordering-signalrhub/values.yaml

echo "####### Deploying payment-api ######"
helm upgrade --install payment-chart deploy/k8s/helm/payment-api/ --values deploy/k8s/helm/payment-api/values.yaml

echo "####### Deploying tls-support ######"
helm upgrade --install tls-support-chart deploy/k8s/helm/tls-support/ --values deploy/k8s/helm/tls-support/values.yaml

echo "####### Deploying webhooks-api ######"
helm upgrade --install webhooks-chart deploy/k8s/helm/webhooks-api/ --values deploy/k8s/helm/webhooks-api/values.yaml

echo "####### Deploying webhooks-web ######"
helm upgrade --install webhooks-web-chart deploy/k8s/helm/webhooks-web/ --values deploy/k8s/helm/webhooks-web/values.yaml

echo "####### Deploying webmvc ######"
helm upgrade --install webmvc-chart deploy/k8s/helm/webmvc/ --values deploy/k8s/helm/webmvc/values.yaml

echo "####### Deploying webshoppingagg ######"
helm upgrade --install webshoppingagg-chart deploy/k8s/helm/webshoppingagg/ --values deploy/k8s/helm/webshoppingagg/values.yaml

echo "####### Deploying webspa ######"
helm upgrade --install webspa-chart deploy/k8s/helm/webspa/ --values deploy/k8s/helm/webspa/values.yaml

echo "####### Deploying webstatus ######"
helm upgrade --install webstatus-chart deploy/k8s/helm/webstatus/ --values deploy/k8s/helm/webstatus/values.yaml
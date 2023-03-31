#!/usr/bin/env bash

HELM_PATH='cat deploy/k8s/helm'
echo "####### Deploying basket-api ######"
helm upgrade --install basket-chart $HELM_PATH/basket-api/ --values $HELM_PATH/basket-api/values.yaml

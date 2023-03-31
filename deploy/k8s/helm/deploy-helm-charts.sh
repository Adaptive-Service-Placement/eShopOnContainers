#!/usr/bin/env bash

PATH='cat deploy/k8s/helm'
echo "####### Deploying basket-api ######"
helm upgrade --install basket-chart $PATH/basket-api/ --values $PATH/basket-api/values.yaml

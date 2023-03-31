#!/usr/bin/env bash

HELM_PATH='cat deploy/k8s/helm'
echo "####### Deploying basket-api ######"
helm upgrade --install basket-chart deploy/k8s/helm/basket-api/ --values deploy/k8s/helm/basket-api/values.yaml

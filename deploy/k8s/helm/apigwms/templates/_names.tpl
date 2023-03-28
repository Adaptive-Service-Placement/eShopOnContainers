{{- define "suffix-name" -}}
"my-eshop"
{{- end -}}

{{- define "sql-name" -}}
{{- printf "%s" "sql-data" -}}
{{- end -}}

{{- define "mongo-name" -}}
{{- printf "%s" "nosql-data" -}}
{{- end -}}



{{- define "pathBase" -}}
/mobileshoppingapigw
{{- end -}}

{{- define "fqdn-image" -}}
envoyproxy/envoy
{{- end -}}
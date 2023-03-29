{{- define "suffix-name" -}}
"my-eshop"
{{- end -}}

{{- define "sql-name" -}}
{{- printf "%s" "sql-data" -}}
{{- end -}}

{{- define "mongo-name" -}}
{{- printf "%s" "nosql-data" -}}
{{- end -}}

{{- define "url-of" -}}
{{- $name := first .}}
{{- $ctx := last .}}
{{- if eq $name "" -}}
testapplication.com
{{- else -}}
{{- printf "%s/%s" "testapplication.com" $name -}}                {{/*Value is just <dns>/<name> */}}
{{- end -}}
{{- end -}}



{{- define "pathBase" -}}
{{- .Values.pathBase -}}
{{- end -}}

{{- define "fqdn-image" -}}
{{- .Values.image.repository -}}
{{- end -}}

{{- define "protocol" -}}
{{- printf "%s" "http" -}}
{{- end -}}
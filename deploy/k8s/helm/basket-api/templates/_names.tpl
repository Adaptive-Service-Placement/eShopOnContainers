{{- define "suffix-name" -}}
{{- if .Values.app.name -}}
{{- .Values.app.name -}}
{{- else -}}
{{- .Release.Name -}}
{{- end -}}
{{- end -}}
{{- define "sql-name" -}}
{{- if .Values.inf.sql.host -}}
{{- .Values.inf.sql.host -}}
{{- else -}}
{{- printf "%s" "sql-data" -}}
{{- end -}}
{{- end -}}

{{- define "mongo-name" -}}
{{- if .Values.inf.mongo.host -}}
{{- .Values.inf.mongo.host -}}
{{- else -}}
{{- printf "%s" "nosql-data" -}}
{{- end -}}
{{- end -}}

{{- define "url-of" -}}
{{- $name := first .}}
{{- $ctx := last .}}
{{- if eq $name "" -}}
{{- $ctx.Values.inf.k8s.dns -}}
{{- else -}}
{{- printf "%s/%s" $ctx.Values.inf.k8s.dns $name -}}                {{/*Value is just <dns>/<name> */}}
{{- end -}}
{{- end -}}



{{- define "pathBase" -}}
{{- .Values.pathBase -}}
{{- end -}}

{{- define "fqdn-image" -}}
eshop/basket.api
{{- end -}} 

{{- define "mongo-name" -}}
{{- if .Values.inf.mongo.host -}}
{{- .Values.inf.mongo.host -}}
{{- else -}}
{{- printf "%s" "seq" -}}
{{- end -}}
{{- end -}}


{{- define "pathBase" -}}
{{- .Values.pathBase -}}
{{- end -}}
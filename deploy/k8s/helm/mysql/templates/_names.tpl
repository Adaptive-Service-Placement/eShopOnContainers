{{- define "suffix-name" -}}
{{- if .Values.app.name -}}
{{- .Values.app.name -}}
{{- else -}}
{{- .Release.Name -}}
{{- end -}}
{{- end -}}

{{- define "mysql-name" -}}
{{- if .Values.inf.mysql.host -}}
{{- .Values.inf.mysql.host -}}
{{- else -}}
{{- printf "%s" "mysql" -}}
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
{{- if .Values.inf.k8s.suffix -}}
{{- $suffix := include "suffix-name" . -}}
{{- printf "%s-%s"  .Values.pathBase $suffix -}}
{{- else -}}
{{- .Values.pathBase -}}
{{- end -}}
{{- end -}}

{{- define "fqdn-image" -}}
{{- if .Values.inf.registry -}}
{{- printf "%s/%s" .Values.inf.registry.server .Values.image.repository -}}
{{- else -}}
{{- .Values.image.repository -}}
{{- end -}}
{{- end -}}
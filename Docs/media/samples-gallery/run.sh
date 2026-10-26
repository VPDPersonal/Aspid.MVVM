#!/bin/sh
# run.sh <file.cs> <Class.Method> [json-args]: runs a static method in the capture Editor (CAPTURE_PROJECT).
C=${CAPTURE_PROJECT:?set CAPTURE_PROJECT to the throwaway Unity project}
unity command --project-path "$C" --no-banner --timeout ${T:-60} --json script run --file "$1" --entry "$2" ${3:+--args "$3"} | python3 -c 'import json, sys
d=json.load(sys.stdin); r = (d.get("data") or {}).get("result") or {}
print(r.get("result") if r.get("success") else (r.get("errorDetails") or d))
sys.exit(0 if r.get("success") else 1)'

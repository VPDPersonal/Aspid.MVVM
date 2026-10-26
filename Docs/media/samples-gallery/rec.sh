#!/bin/sh
# rec.sh <slug> <scene asset path> <Dark|Light>  -> frames in $CAPTURE_PROJECT/Temp/rec/<slug>-<theme>/
set -e
G=$(cd "$(dirname "$0")" && pwd)
C=${CAPTURE_PROJECT:?set CAPTURE_PROJECT to the throwaway Unity project}
S=${CAPTURE_SCRIPTS:-$HOME/.claude/skills/asp-unity-capture/scripts}
cmd() { unity command --project-path "$C" --no-banner --timeout 60 --json "$@"; }
cloak() { cmd script run --file "$S/cloak.cs" --entry CaptureCloak.Run >/dev/null 2>&1 || true; }
mode() { cmd editor status 2>/dev/null | python3 -c 'import json, sys
r = json.load(sys.stdin)["data"]["result"]
print("busy" if r["compiling"] or r["domainReloadInProgress"] else r["playMode"])' 2>/dev/null || echo busy; }
wait_mode() { i=0; until [ "$(mode)" = "$1" ]; do i=$((i+1)); [ $i -gt 90 ] && { echo "never reached $1"; exit 1; }; sleep 1; done; }
slug=$1; scene=$2; theme=$3
out=$C/Temp/rec/$slug-$(echo $theme | tr A-Z a-z)
[ "$(mode)" = playing ] && { cmd editor.playmode stop >/dev/null; wait_mode stopped; }
wait_mode stopped
printf %s "$scene" > "$C/Temp/rec-scene.txt"
printf '%s %s' "$slug" "$theme" > "$C/Temp/rec-args.txt"
sh "$G/run.sh" "$G/open.cs" OpenRecScene.Main >/dev/null
cmd editor.playmode start >/dev/null
wait_mode playing
cloak
sh "$G/run.sh" "$G/record.cs" MvvmRec.Main >/dev/null
i=0; until [ -f "$out/done.txt" ]; do i=$((i+1)); [ $i -gt 300 ] && { echo "recorder did not finish"; cmd editor.playmode stop >/dev/null; exit 1; }; sleep 1; done
cmd editor.playmode stop >/dev/null
wait_mode stopped
cloak
echo "$slug $theme: $(cat "$out/done.txt")"

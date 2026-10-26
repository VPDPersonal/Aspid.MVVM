#!/bin/sh
# all.sh [slug…]: records every sample of all.txt (or only the given slugs) in both themes.
G=$(cd "$(dirname "$0")" && pwd)
while IFS='|' read -r slug scene; do
  [ -n "$1" ] && ! echo " $* " | grep -q " $slug " && continue
  for th in Dark Light; do sh "$G/rec.sh" "$slug" "$scene" $th </dev/null || echo "FAILED $slug $th"; done
done < "$G/all.txt"

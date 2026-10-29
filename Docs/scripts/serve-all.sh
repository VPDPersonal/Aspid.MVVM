#!/bin/sh
# Build the site (all locales) and serve it detached on localhost: one server per checkout, so agents in different
# worktrees never replace each other's build. The main checkout serves on 3002 (Aspid.FastTools takes 3001); a linked worktree gets its own port
# (3200-3999, derived from its path, the next free one if taken) and borrows node_modules from the main checkout.
# Re-run after any change to verify it: it replaces only this checkout's server, rebuilds and serves again. It also
# stops servers whose checkout was deleted.
#   Docs/scripts/serve-all.sh            # build + serve
#   Docs/scripts/serve-all.sh --stop     # stop this checkout's server only
# Log: Docs/.serve-all.log, port: Docs/.serve-all.port
set -e
cd "$(dirname "$0")/.."
SITE="$PWD"
LOG="$SITE/.serve-all.log"
PORT_FILE="$SITE/.serve-all.port"
MAIN_SITE="$(git worktree list --porcelain | sed -n '1s/^worktree //p')/Docs"

cwd_of() { lsof -a -p "$1" -d cwd -Fn 2>/dev/null | sed -n 's/^n//p'; }

for pid in $(pgrep -f 'docusaurus serve' || true); do
  cwd=$(cwd_of "$pid")
  case "$cwd" in
    "$SITE") kill "$pid" 2>/dev/null || true ;;                         # this checkout's previous server
    *'/.ccd-trash'*) kill "$pid" 2>/dev/null || true ;;                 # worktree removed by the app
    ?*) [ -d "$cwd" ] || kill "$pid" 2>/dev/null || true ;;             # worktree deleted by hand
  esac
done
rm -f "$PORT_FILE"
[ "$1" = "--stop" ] && { echo "stopped"; exit 0; }

if [ "$SITE" = "$MAIN_SITE" ]; then
  PORT=3002
  pids=$(lsof -tiTCP:$PORT -sTCP:LISTEN 2>/dev/null || true)
  [ -n "$pids" ] && kill $pids 2>/dev/null
else
  [ -e node_modules ] || ln -s "$MAIN_SITE/node_modules" node_modules
  PORT=$((3200 + $(printf %s "$SITE" | cksum | cut -d' ' -f1) % 800))
  while lsof -tiTCP:$PORT -sTCP:LISTEN >/dev/null 2>&1; do PORT=$((PORT + 1)); done
fi
sleep 1

npm run build
nohup npx docusaurus serve --port $PORT --no-open >"$LOG" 2>&1 &
for i in $(seq 1 30); do
  curl -sf "http://localhost:$PORT/Aspid.MVVM/" >/dev/null 2>&1 && break
  sleep 1
done
echo "$PORT" >"$PORT_FILE"
echo "serving: http://localhost:$PORT/Aspid.MVVM/  (ru: http://localhost:$PORT/Aspid.MVVM/ru/)"

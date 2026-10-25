#!/bin/sh
# Prints the git status and hint lines that SKILL.md acts on.
# An optional argument is the root of another repo to check instead of cwd.
[ -n "$1" ] && { cd "$1" || exit 1; }
# The short format with fixed settings, not --porcelain: its paths are relative to cwd, so `git add` takes them
# from a subfolder too. Non-ASCII names stay as they are, not octal escapes.
s=$(git -c status.relativePaths=true -c color.status=false -c core.quotePath=false status --short -b)
printf '%s\n' "$s"
head=$(printf '%s\n' "$s" | head -n 1)
b=$(git branch --show-current 2>/dev/null)

[ "$(printf '%s\n' "$s" | wc -l)" -le 1 ] && echo "CLEAN"

case "$head" in *'[gone]'*) echo "GONE" ;; esac
case "$head" in
  *...*) case "$head" in *'[ahead '*) echo "AHEAD" ;; esac ;;
  *) echo "NO_UPSTREAM" ;;
esac

case "$b" in
  '' | main | master) echo "BRANCH new" ;;
  # The app names worktree branches claude/<words>-<hex>; rename one before its first push.
  # A cloud session pushes only to the branch it was given, so it keeps the name.
  claude/*) [ "$CLAUDE_CODE_REMOTE" = true ] || git rev-parse -q --verify "refs/remotes/origin/$b" >/dev/null \
    || echo "BRANCH rename" ;;
esac

# Paths already gone from the index: deletions and the old side of renames.
printf '%s\n' "$s" | sed -n -e 's/^D. \(.*\)/SKIP_ADD \1/p' -e 's/^R. \(.*\) -> .*/SKIP_ADD \1/p'

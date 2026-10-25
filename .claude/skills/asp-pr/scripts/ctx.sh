#!/bin/sh
# Collects everything asp-pr needs in one pass. Never exits non-zero
# (a failing injection command would abort the skill).
dir=$(cd "$(dirname "$0")" && pwd)
# An optional argument is the root of another repo. Only a manual run passes it, so a bad path may exit 1.
[ -n "$1" ] && { cd "$1" || exit 1; }
# The template and the repo rules are read from the root, also when the agent works in a subfolder.
top=$(git rev-parse --show-toplevel 2>/dev/null) && cd "$top"
cur=$(git rev-parse --abbrev-ref HEAD 2>/dev/null || echo '?')
echo "CUR $cur"
case "$cur" in
  # A PR needs its own branch: a push from main or master goes straight to the base.
  HEAD | main | master) echo "BRANCH new" ;;
  # The app names worktree branches claude/<words>-<hex>; rename one before its first push.
  # A cloud session pushes only to the branch it was given, so it keeps the name.
  claude/*) [ "$CLAUDE_CODE_REMOTE" = true ] || git rev-parse -q --verify "refs/remotes/origin/$cur" >/dev/null \
    || echo "BRANCH rename" ;;
esac
# Uncommitted changes do not go into the PR.
[ -n "$(git status --porcelain 2>/dev/null | head -n 1)" ] && echo "DIRTY"
# gh needs -R when it runs outside the repo, e.g. from the folder of the attachments.
echo "REPO $(git remote get-url origin 2>/dev/null | sed -E 's#\.git$##; s#^.*[:/]([^/]+/[^/]+)$#\1#')"
# The network call runs while the base is detected locally.
tmp=$(mktemp)
# Without an argument gh also finds the branch's merged or closed PR. Only an open one is updated.
# A merged one is reported: after a squash merge its commits are still on this branch.
gh pr view --json number,state,title,isDraft,labels,baseRefName,body \
  -q 'if .state == "OPEN" then "\(.number) draft=\(.isDraft) base=\(.baseRefName) labels=[\(.labels|map(.name)|join(","))] \(.title)\n--- body\n\(.body)"
      elif .state == "MERGED" then "merged \(.number)" else empty end' \
  >"$tmp" 2>/dev/null &
base_out=$(sh "$dir/detect-base.sh" 2>/dev/null || echo NONE)
# On main or master, the new branch starts from it, so it is the base.
case "$cur" in main | master) base_out="BASE $cur" ;; esac
wait
pr=$(cat "$tmp"); rm -f "$tmp"
# printf, not echo: the echo of sh turns \n and \t of the body into control characters and stops at \c.
printf 'PR %s\n' "${pr:-none}"
# An open PR already has its base; detection matters only for a new one.
pr_base=$(printf '%s\n' "$pr" | sed -n '1s/.* base=\([^ ]*\) .*/\1/p')
[ -n "$pr_base" ] && base_out="BASE $pr_base"
if [ "$base_out" = NONE ]; then
  base=$(git symbolic-ref -q --short refs/remotes/origin/HEAD 2>/dev/null | sed 's@^origin/@@')
  [ -n "$base" ] || base=$(gh repo view --json defaultBranchRef -q .defaultBranchRef.name 2>/dev/null)
  base_out="BASE ${base:-main}"
fi
echo "$base_out"
case "$base_out" in BASE*) base=${base_out#BASE } ;; *) base= ;; esac
if [ -n "$base" ]; then
  # Compare with the remote branch: the local one may be stale or missing.
  ref=$base
  git rev-parse -q --verify "refs/remotes/origin/$base" >/dev/null && ref="origin/$base"
  n=$(git rev-list --count "$ref..HEAD" 2>/dev/null || echo 0)
  echo "--- commits $ref..HEAD"; git log --oneline "$ref..HEAD" 2>/dev/null | head -20
  [ "$n" -gt 20 ] && echo "... and $((n - 20)) more"
  echo "--- diffstat";           git -c core.quotePath=false diff --stat "$ref...HEAD" 2>/dev/null | tail -30
  echo "FILES $(git diff --name-only "$ref...HEAD" 2>/dev/null | wc -l | tr -d ' ')"
fi
echo "--- template"
# The first hit only: macOS matches both spellings to the same file.
for t in .github/pull_request_template.md .github/PULL_REQUEST_TEMPLATE.md pull_request_template.md \
  docs/pull_request_template.md; do [ -f "$t" ] && { head -60 "$t"; break; }; done
echo "--- repo rules"; cat .claude/asp-pr.md 2>/dev/null || echo none
exit 0

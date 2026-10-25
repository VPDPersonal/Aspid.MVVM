#!/bin/sh
# Wait for a new review verdict on a PR, print its line and exit 0; exit 1 after the timeout.
# Usage: wait-verdict.sh <owner/repo> <pr> [timeout-seconds] [since-iso-time]
# Only bot comments and reviews posted after <since> (default: now) count, so an older verdict does not end the wait.
# A comment counts by its edit time: a review action may reuse one sticky comment for every run.
set -eu
repo=$1 pr=$2 timeout=${3:-1200}
since=${4:-$(date -u +%Y-%m-%dT%H:%M:%SZ)}
end=$(( $(date +%s) + timeout ))
while [ "$(date +%s)" -lt "$end" ]; do
  verdict=$( {
    gh api --paginate "repos/$repo/issues/$pr/comments" \
      --jq ".[] | select(.user.type == \"Bot\" and .updated_at > \"$since\") | .body" || true
    gh api --paginate "repos/$repo/pulls/$pr/reviews" \
      --jq ".[] | select(.user.type == \"Bot\" and .submitted_at > \"$since\") | .body" || true
  } 2>/dev/null | tr -d '\r' | grep -m1 -E '^Verdict: [0-9]+ blocking, [0-9]+ minor$' || true)
  if [ -n "$verdict" ]; then
    echo "$verdict"
    exit 0
  fi
  sleep 30
done
echo "No verdict on $repo#$pr after ${timeout}s."
exit 1

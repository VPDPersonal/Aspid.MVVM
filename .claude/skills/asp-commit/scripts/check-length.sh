#!/bin/sh
# Stops the chain before anything is pushed when a commit subject or PR title is over 72 characters.
# GitHub cuts a longer subject with an ellipsis, and a repo CI may reject the PR title.
status=0
for s in "$@"; do
  if [ ${#s} -gt 72 ]; then
    echo "TOO LONG ${#s}/72: $s"
    status=1
  fi
done
exit $status

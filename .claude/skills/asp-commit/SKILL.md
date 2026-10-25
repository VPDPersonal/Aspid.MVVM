---
name: asp-commit
description: "Git commit + push of current-session changes only, in one Bash chain. Use when the user asks to commit or push."
when_to_use: "`/asp-commit`, «закоммить», «комить», «комит», «комить и пуш», «запушь», «commit changes», «push»; any commit request, also after a long task."
effort: low
allowed-tools: Bash(sh ${CLAUDE_SKILL_DIR}/scripts/*) Bash(git switch -c:*) Bash(git branch -m:*) Bash(git diff:*) Bash(git add:*) Bash(git rm --cached:*) Bash(git commit:*) Bash(git push -u origin HEAD)
---

```!
sh ${CLAUDE_SKILL_DIR}/scripts/ctx.sh
```

No output above? Your agent does not run this block: run `sh scripts/ctx.sh` from the folder of this file. There, `${CLAUDE_SKILL_DIR}` is that folder.

## Hint lines

| Line | Action |
|---|---|
| `GONE` | The branch was merged and deleted on origin. Ask with `AskUserQuestion` first. |
| `CLEAN` | Nothing to commit. Go to [Push only](#push-only). |
| `AHEAD`, `NO_UPSTREAM` | Used only in [Push only](#push-only). |
| `BRANCH new` | Branch step: `git switch -c <name>`. |
| `BRANCH rename` | Branch step: `git branch -m <name>`. The app made this worktree branch, and origin does not have it yet. |
| `SKIP_ADD <path>` | The path is already gone from the index. Keep it in `git commit -- <paths>`, but not in `git add`. |

Before a branch step, load the `asp-branch` skill to get `<name>`.

## Pick the files

- Take the files you changed in this session and the files the user named.
- Add the Unity `.meta` files of these files and of their folders.
- Take paths only from the status lines. A file inside `?? <dir>/` counts.
- A changed file is not in the status? In another repo, see [Special cases](#special-cases). Otherwise it is ignored: skip it.
- Changes that are not yours and that the user did not name? Ask with `AskUserQuestion` whether to add them.
- Run `git diff -- <file>` only for a file you did not change.

## Commit and push: one Bash call, no subagents

1. Write the subject: `type(scope): imperative summary`, 72 chars max, no period.
2. Add a body of 2–4 bullets only if there are more than 5 files or the reason is not clear.
3. Run [the chain](#the-chain). At `TOO LONG`, rewrite the subject to 65 chars or fewer. Then run the chain again.
4. Report the commits in a table with the columns: subject, short SHA, push. List the skipped files below the table.
   In the push column, write the branch name, for example `feat/login`. Do not use the SHA range or `HEAD`.
   Write `failed` if the push failed.

Run no other git commands, except `git diff` in [Pick the files](#pick-the-files) and in [Special cases](#special-cases).

### The chain

```sh
sh ${CLAUDE_SKILL_DIR}/scripts/check-length.sh '<subject>' \
  && [<branch step> &&] git add -- <paths> && git commit -m "$(cat <<'MSG'
<subject>

<bullets, if any>

<attribution lines from the system reminder, if any>
MSG
)" -- <paths> && git push -u origin HEAD
```

Give `check-length.sh` the subject in single quotes, the same text as in the message.
Do not use `'` in the subject: write "do not", not "don't".

Write `<paths>` literally in `git add` and in `git commit`. Do not put them in a shell variable: zsh does not split it.

## Push only

On `CLEAN`:

1. Continue only on `AHEAD`, or on `NO_UPSTREAM` if the user asked to push. Otherwise stop.
2. On `BRANCH new`, ask with `AskUserQuestion` first.
3. Run `[<branch step> &&] git push -u origin HEAD`.

## Special cases

- **Unrelated changes.** One `git add && git commit -- <paths>` per group in the same chain. One push at the end.
  One `check-length.sh` at the start takes the subjects of all groups.
- **Untrack a file but keep it on disk.** Run `git rm --cached -- <file> && git commit -m …` with no `-- <paths>`.
  With `-- <file>`, the commit adds the file back from disk.
- **Files in another repo** (outside cwd, a package with its own `.git`). The hint lines above do not cover it.
  1. Run `sh ${CLAUDE_SKILL_DIR}/scripts/ctx.sh <repo>`. Act on its hint lines.
  2. Run a separate chain with `git -C <repo>` in every git command. Do not use `cd`.
  3. Never `git add` a folder that holds its own `.git`.
- **A `pre-commit` or `commit-msg` hook failed.** Fix the cause.
  Run again only the steps that did not run: no branch step, no commits that succeeded.
- **Push failed.** Report it. Do not retry.

## Forbidden

- `git add -A`, `git add .`, `git add -u`.
- `--amend`, `--no-verify`, force-push.
- Committing `.env`, `*.pem`, `*.key`, `*.keystore`, `*.jks`, `*.p12`, `id_rsa*`, `credentials.*`.

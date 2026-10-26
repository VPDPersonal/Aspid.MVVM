---
name: asp-mvvm-release
description: Release an Aspid.MVVM version end to end. Phase 1 opens the release PR (version files, CHANGELOG section, local tests, Unity EditMode); the user merges it. Phase 2 pushes the v<version> tag after the user's yes and checks what the Release workflow published. The version alone picks the channel. - `/asp-mvvm-release 1.1.0-rc.1`, `/asp-mvvm-release 1.1.0`.
argument-hint: <version>
disable-model-invocation: true
---

# Release

The argument is the version, for example `1.1.0-rc.1`. Remove a leading `v`. No argument → ask for the version.

## Channel

The version picks the channel. It is the same rule as in `scripts/set-version.sh` and `.github/workflows/release.yml`.

- A version with a hyphen (`1.1.0-rc.1`) → `upm-preview`: a GitHub prerelease.
- A version without a hyphen (`1.1.0`) → `upm`: a stable release. Only it has the steps marked **Stable**.

Both channels get an immutable `upm/<version>` tag, the pinned install URL of the README.

## Which phase

Run `git fetch origin`. Then pick the phase:

1. The tag `v<version>` is on origin (`git ls-remote --tags origin v<version>`) → go to "Phase 3".
2. The `package.json` on `origin/main` has the version → go to "Phase 2".
3. The PR from `chore/release-<version>` is open (`gh pr view chore/release-<version> --json state,url`) →
   report its link and its checks, then stop.
4. Else → go to "Phase 1".

## Phase 1: release PR

Do every step without questions. Stop at the first failure and report it.

1. Check that the working tree is clean.
2. Check that the version is greater than the `package.json` version on `origin/main`. Use SemVer precedence:
   `1.1.0-rc.10` > `1.1.0-rc.9`, and `1.1.0` > `1.1.0-rc.9`.
3. Create the branch: `git switch -c chore/release-<version> origin/main`.
4. Check the CHANGELOGs. `set-version.sh` releases the notes under `## [Unreleased]` or under the version's own
   heading marked unreleased (`## [1.1.0] — Unreleased`, `## [1.1.0] — Не выпущено` in `Docs/i18n/ru/CHANGELOG.md`).
   A prerelease of a line whose notes sit under the line's heading (`1.1.0-rc.1` under `## [1.1.0] — Unreleased`)
   needs its own section: write `## [<version>] — <today>` above the line's heading in both CHANGELOGs, with a short
   summary of the preview, in the style of the file. Keep the line's heading as it is.
5. Run `scripts/set-version.sh <version>`. It ends with `scripts/check-version.mjs`.
6. Start two `unity-verify` agents in the background, with EditMode tests and no filter:
   - a project from `scripts/make-unity-test-project.sh <scratchpad>/unity-min <minimum>`, where `<minimum>` is the
     first version of `UNITY_VERSIONS` in `.github/workflows/ci.yml`;
   - the dev project `Aspid.MVVM/` of this checkout.

   The `unity-verify` agent is user-level. Without it, run the batch command from the header of
   `scripts/make-unity-test-project.sh` for both projects, in the background.

   The Editor of `<minimum>` may not be installed (`ls /Applications/Unity/Hub/Editor`). Then use the oldest
   installed 6000.0.x and write that version in the report. No 6000.0.x Editor → skip the minimum project and write
   that in the report. The release workflow runs every version of `UNITY_VERSIONS` anyway.
7. Run the local checks of `.github/workflows/ci.yml`:
   - `node --test scripts/*.test.mjs` and `node scripts/check-package-files.mjs`;
   - the `dotnet-tests` jobs: `dotnet test <path> --nologo` for each `path` of its matrix;
   - the `roslyn-dlls` job: `scripts/build-roslyn-dlls.sh`, then `git status --short -- Aspid.MVVM/Packages/tech.aspid.mvvm`.
8. A DLL diff in step 7 means stale DLLs on `main`. Run `git checkout --` on the DLLs. Stop and report.
9. Find the old version in prose:
   `git grep -n -F '<old>' -- . ':!CHANGELOG.md' ':!Docs/i18n/ru/CHANGELOG.md' ':!scripts/'`.
   Also look at the README install section (the `Preview (1.1.0)` summary, the channel text). Do not edit the hits.
   Put them into the report.
10. Wait for both `unity-verify` agents. A failed test stops the release.
11. Commit with the `asp-commit` skill. Message: `chore(release): v<version>`.
12. Open the PR with the `asp-pr` skill. Title: `chore(release): v<version>`. Body: the channel and the version's
    CHANGELOG section in one line.
13. Send the report (format below). The user merges the PR.

## Phase 2: tag

1. Find the merge commit: `gh pr view chore/release-<version> --json state,mergeCommit`.
2. The PR is not merged → say so and stop.
3. Ask the user in one message: the version, the channel, the commit SHA and subject. Say that the tag starts the
   Release workflow, that a published tag cannot be replaced, and that the `Release tags` ruleset lets only an admin
   push it.
4. Wait for a clear yes. Any other answer → stop.
5. Create the tag: `git tag -a v<version> -m "Release v<version>" <merge-sha>`.
6. Push it: `git push origin v<version>`.
7. Find the run of the tag:
   `gh run list --workflow release.yml --branch v<version> --limit 1 --json databaseId,status,conclusion`.
   No run yet → wait 10 seconds and try again. After 6 tries, stop and report.
8. Run `gh run watch <id> --exit-status` in the background. Go to "Phase 3" when it ends.

## Phase 3: check the publication

1. Find the run of the tag as in "Phase 2" step 7.
2. `status` is not `completed` → go to "Phase 2" step 8.
3. `conclusion` is not `success` → go to "Release failed".
4. Check the GitHub release: `gh release view v<version> --json url,isPrerelease`.
   `isPrerelease` must be true on `upm-preview` and false on `upm`.
5. Check that `refs/heads/<channel>` and `refs/tags/upm/<version>` point to one commit:
   `git ls-remote origin refs/heads/<channel> refs/tags/upm/<version>`.
6. Check the package version on the channel:
   `gh api 'repos/VPDPersonal/Aspid.MVVM/contents/package.json?ref=<channel>' --jq .content | base64 -d`.
7. Check that the bundled `CHANGELOG.md` on the channel has the version's section:
   `gh api 'repos/VPDPersonal/Aspid.MVVM/contents/CHANGELOG.md?ref=<channel>' --jq .content | base64 -d`.
8. **Stable:** find the milestone. Its title is `<version>` or `v<version>`:
   `gh api 'repos/VPDPersonal/Aspid.MVVM/milestones?state=open&per_page=100' --jq '.[] | select(.title == "<version>" or .title == "v<version>") | .number'`.
   No milestone → say so in the report and skip step 9.
9. **Stable:** ask before you close the milestone. Then close it:
   `gh api -X PATCH repos/VPDPersonal/Aspid.MVVM/milestones/<number> -f state=closed`.
10. Send the final report: the release URL, the channel, the open items from the Phase 1 report.

## Release failed

Read the failed step: `gh run view <id> --log-failed`. Then:

- **The job `Preflight`, a `CI / …` job or `release` failed.** Only the `v<version>` tag is out: the push of the UPM
  branch and its tag is atomic.
  1. Report the cause.
  2. A transient cause (network, Unity license): ask, then run `gh run rerun <id> --failed`.
     It keeps the finished jobs. Go to "Phase 3" when the run ends.
  3. Preflight says the commit is not in `main`: the tag is on the wrong commit. Merge that commit into `main` with
     a PR. Then go to step 5 with the merge commit.
  4. Any other cause: the fix goes to `main` in a separate PR.
  5. After the fix, ask before you delete the tag: `git push origin :refs/tags/v<version>`, `git tag -d v<version>`.
  6. Go to "Phase 2" step 3 with the merge commit of that PR.
- **Only the job `GitHub release` failed.** The UPM tag and the channel branch are out.
  1. Run `gh run rerun <id> --failed`. It runs only that job. Go to "Phase 3" when the run ends.
     Do not rerun the whole run: preflight would refuse the UPM tag that the first run already published.
  2. It fails again → ask, then create the release by hand.
     The command is in the comment above the job in `release.yml`.

## Not done by this skill

Put these items into the Phase 1 report. Do not do them without a request.

- The API reference on the site (`docs-site` skill). Ask if a PR that changed public API merged without it since the
  last release.
- A docs version of the Docusaurus site for a stable release (`docs-site` skill).
- **Stable:** the Asset Store upload. It is manual: the user uploads the package.

## Report format

Write the report in Russian. Keep it short:

1. The first line: the PR link (Phase 1) or the release link (Phase 3), and the channel.
2. The checks, one line each: script tests, `dotnet test`, DLLs, Unity minimum, Unity dev project.
3. The old-version hits from Phase 1 step 9: the file and the line.
4. The open items from "Not done by this skill" that apply to this version.
5. The last line: what the user does next.

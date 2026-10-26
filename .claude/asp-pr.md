# PR rules for Aspid.MVVM

`.github/workflows/pr-checks.yml` checks the title, sets the `type:*`, `breaking-change` and `area:*` labels from it
and the paths, and checks the CHANGELOG rule. Keep to the rest by hand.

- Types: `feat` `fix` `perf` `refactor` `style` `docs` `test` `build` `ci` `chore` `revert`.
- Scopes (or none): `core` `starterkit` `binders` `converters` `collections` `editor` `samples` `generators`
  `analyzers` `package` · `project` `site` `api` `changelog` `release` `ci` `github` `skills` `claude` `deps`.
- `!` before the colon marks a breaking change: a public API of the package is removed or changed. The PR then also
  updates `MIGRATION.md` and `MIGRATION.ru.md`.
- Shipped package code changed (`Aspid.MVVM/Packages/tech.aspid.mvvm/` except `Documentation/`, `Tests/` and Markdown;
  the generator and analyzer DLLs; the package `package.json`) → the same entry under the top `Unreleased` version in
  `CHANGELOG.md` and `CHANGELOG.ru.md`. No entry yet → the `needs-changelog` label.
- Labels to pass at `gh pr create`:
  - one `type:*` from the title type: `feat` → `type: feature`, `fix` → `type: fix`, `perf` → `type: performance`,
    `refactor` → `type: refactor`, `style` → `type: style`, `docs` → `type: documentation`, `test` → `type: test`,
    `ci` → `type: ci`, `build` `chore` `revert` → `type: chore`;
  - `breaking-change` with `!`;
  - `area:*` from the paths: `area: runtime` (package code outside `Editor/` and `Tests/`), `area: editor`
    (`Editor/` folders), `area: samples` (`Samples~/`), `area: docs` (`Documentation/`, `Docs/`, root `*.md`),
    `area: ci` (`.github/`, `scripts/`, `global.json`, `Directory.Build.props`), `area: generator` and
    `area: analyzer` (a submodule and its DLL), `area: claude` (`.claude/`, `.agents/`, `AGENTS.md`, `CLAUDE.md`);
    `pr-checks.yml` sets the `type:*`, `breaking-change` and `area:*` labels itself, on every change of the PR;
  - `dependencies` for a package, tool or submodule bump;
  - `status: blocked` when the PR waits for another PR or for the owner.
- `.github/workflows/pr-ready-labels.yml` sets `status: needs-review` and `status: work-in-progress` on ready and
  draft; do not pass them.
- A change in `Aspid.MVVM.Generators/`, `Aspid.MVVM.Analyzers/` or `Aspid.MVVM.Unity.Generators/` gets its own PR in
  that submodule repo. The PR here bumps the submodule and the committed DLL.

## Review loop

The bot review rules with the verdict format are in `.github/claude-review.md`. The `review` job of
`.github/workflows/claude.yml` runs them when a PR opens ready or leaves draft; the re-review comment runs them through
its `mention` job.

Logic paths:

- package: `Aspid.MVVM/Packages/tech.aspid.mvvm/` except `Documentation/` and `*.md`, including the committed
  generator and analyzer DLLs;
- .NET: the submodules `Aspid.MVVM.Generators/`, `Aspid.MVVM.Analyzers/`, `Aspid.MVVM.Unity.Generators/`;
- dev project: `Aspid.MVVM/Packages/manifest.json`, `Aspid.MVVM/Packages/packages-lock.json`, `Aspid.MVVM/Assets/`,
  `Aspid.MVVM/ProjectSettings/`;
- site: `Docs/` except `api/` and `static/`;
- CI: `.github/workflows/`, `.github/claude-review.md`.

Other paths are text: `Documentation/`, sample `README.md` and `README.ru.md`, `Docs/api/`, `Docs/static/`,
`.claude/skills/`, root `*.md`. A fix there needs no re-review.

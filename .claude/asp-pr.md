# PR rules for Aspid.MVVM

`.github/workflows/pr-checks.yml` checks the title, sets the `type:*`, `breaking-change` and `area:*` labels from it
and the paths, and checks the CHANGELOG rule. Keep to the rest by hand.

- Types: `feat` `fix` `perf` `refactor` `style` `docs` `test` `build` `ci` `chore` `revert`.
- Scopes (or none): `core` `starterkit` `binders` `converters` `collections` `editor` `samples` `generators`
  `analyzers` `package` · `project` `site` `api` `changelog` `release` `ci` `github` `skills` `claude` `deps`.
- `!` before the colon marks a breaking change: a public API of the package is removed or changed. The PR then also
  updates the migration guide of the next version, `Docs/migration/<version>.md` and its translation in
  `Docs/i18n/ru/docusaurus-plugin-content-docs-migration/current/`.
- Shipped package code changed (`Aspid.MVVM/Packages/tech.aspid.mvvm/` except `Tests/` and Markdown;
  the generator and analyzer DLLs; the package `package.json`) → the same entry under the top `Unreleased` version in
  `CHANGELOG.md` and `Docs/i18n/ru/CHANGELOG.md`. No entry yet → the `needs-changelog` label.
- Labels to pass at `gh pr create`:
  - one `type:*` from the title type: `feat` → `type: feature`, `fix` → `type: fix`, `perf` → `type: performance`,
    `refactor` → `type: refactor`, `style` → `type: style`, `docs` → `type: documentation`, `test` → `type: test`,
    `ci` → `type: ci`, `build` `chore` `revert` → `type: chore`;
  - `breaking-change` with `!`;
  - `area:*` from the paths: `area: runtime` (package code outside `Editor/` and `Tests/`), `area: editor`
    (`Editor/` folders), `area: samples` (`Samples~/`), `area: docs` (`Docs/`, root `*.md`),
    `area: ci` (`.github/`, `scripts/`, `global.json`, `Directory.Build.props`), `area: generator` and
    `area: analyzer` (a source folder and its DLL), `area: claude` (`.claude/`, `.agents/`, `AGENTS.md`, `CLAUDE.md`);
    `pr-checks.yml` sets the `type:*`, `breaking-change` and `area:*` labels itself, on every change of the PR;
  - `dependencies` for a package or tool bump;
  - `status: blocked` when the PR waits for another PR or for the owner.
- `.github/workflows/pr-ready-labels.yml` sets `status: needs-review` and `status: work-in-progress` on ready and
  draft; do not pass them.
- A change in `Aspid.MVVM.Generators/` or `Aspid.MVVM.Analyzers/` goes in the same PR
  as the DLL that `scripts/build-roslyn-dlls.sh` rebuilds from it.

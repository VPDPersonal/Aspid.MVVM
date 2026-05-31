# Submodule-aware sync (the checkpoint mechanism)

Git is the maintenance engine. The wiki tracks the last ingested commit(s) so each update only processes what changed — cost stays bounded as the project grows. Aspid.MVVM has **three git submodules** (the generators/analyzers), so the plain single-`last_commit` git-wiki scheme is extended with per-submodule checkpoints.

## Checkpoint state

Stored in `docs/wiki/index.md` frontmatter:
- `last_commit` — superproject `HEAD` at last ingest.
- `submodule_commits.{generators,unity_generators,analyzers}` — each submodule's `HEAD` at last ingest.

Missing or empty `last_commit` ⇒ no checkpoint ⇒ treat as **first run**.

## First run

Build the candidate file set and ingest the full taxonomy:

```bash
# superproject tracked files, minus the vault, minus Unity meta noise
git ls-files | grep -v '^docs/wiki/' | grep -v '\.meta$'
# each submodule's tracked files
git -C Aspid.MVVM.Generators       ls-files | grep -v '\.meta$'
git -C Aspid.MVVM.Unity.Generators ls-files | grep -v '\.meta$'
git -C Aspid.MVVM.Analyzers        ls-files | grep -v '\.meta$'
```

## Incremental run

Diff each repo independently against its checkpoint. The superproject diff shows submodule **pointer bumps**, not the source changes inside them — so the submodules must be diffed in their own repos.

```bash
# superproject (main Assets tree)
git diff --name-status -M <last_commit> HEAD -- . ':(exclude)docs/wiki/**' | grep -v '\.meta$'
# submodules — diff inside each one
git -C Aspid.MVVM.Generators       diff --name-status -M <generators_sha> HEAD
git -C Aspid.MVVM.Unity.Generators diff --name-status -M <unity_generators_sha> HEAD
git -C Aspid.MVVM.Analyzers        diff --name-status -M <analyzers_sha> HEAD
```

`-M` enables rename tracking. Map each changed path to the pages whose `source_paths` reference it; update/create only those. A changed generator file affects `generation/` pages **and** any `concepts/`/`flows/` page describing the code it emits — follow the link graph.

## Stale handling

- Source file deleted or heavily changed and not yet reflected ⇒ set the page's `status: stale` and note the drift. **Never auto-delete** a page.
- Committed generator DLLs (`*.dll` under `Assets/`) changing without a corresponding submodule source change is a signal the DLL was rebuilt — verify the `generation/` pages still match.

## After ingest

Advance the checkpoint (superproject + all three submodules) in `index.md`, set `updated_at`, and append a `docs/wiki/log.md` entry: date, commit range(s), pages touched.

## Lint checks

- Broken `[[wikilinks]]` (target file absent).
- Orphan pages (no inbound links).
- `source_paths` pointing at non-existent files.
- Changed modules (from the diff) with no covering page.
- Concepts mentioned across ≥3 pages with no dedicated page → suggest one.

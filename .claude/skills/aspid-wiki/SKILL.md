---
name: aspid-wiki
description: Build, update, query, and lint the Aspid.MVVM knowledge wiki — a navigable Obsidian vault of concept/entity/binder/flow/risk pages under docs/wiki/ that lets agents and developers orient in the codebase without reading hundreds of source files. Use this skill whenever the user asks to "build/update/ingest the wiki", "обнови вики", "построй вики по проекту", "document the codebase", "sync the wiki with the code", "lint the wiki", "answer from the wiki", or "export the wiki", or when you are about to read many source files to answer an architecture/orientation question and a wiki page would answer it more cheaply. Also use after a significant code change to keep the wiki in sync via the git-diff checkpoint.
---

# Aspid.MVVM Knowledge Wiki

A persistent, compounding knowledge base for the Aspid.MVVM framework. The committed **code at git `HEAD` is the source of truth**; the wiki is a separate, human- and agent-readable layer that explains architecture, concepts, flows, conventions, and accumulated decisions — *not* a copy of the code.

**Why it exists:** re-establishing project context from raw source costs 5,000–15,000 tokens per session before any real work begins. A structured wiki, navigated by index + summary pages, replaces that with a few cheap page reads. Read the wiki first; fall back to source only when a page is missing or marked `stale`.

- **Vault location:** `docs/wiki/` at the repo root — deliberately **outside** `Assets/` so Unity does not generate `.meta` files for notes and `.obsidian/` churn does not pollute the asset tree.
- **Format:** Markdown + YAML frontmatter + Obsidian `[[wikilinks]]`. Opened directly as an Obsidian vault (graph view, Dataview). Separate from the public GitBook docs.
- **Entry point:** `docs/wiki/index.md` — the navigation hub answering 8 orientation questions. Always start here.
- **Languages:** `docs/wiki/` is **English and the single source of truth** — the only vault ingested from code. Translations live in sibling vaults `docs/wiki-<lang>/` (e.g. `docs/wiki-ru/`) that mirror the English tree 1:1 and are generated **from the English wiki, never from code**. See `references/translations.md`.

## Operations

This skill exposes four operations. Pick the one matching the request.

### 1. Ingest — build or update the wiki from source

Keeps the wiki in sync with the code, cheaply, by only processing what changed.

1. Read `docs/wiki/index.md` frontmatter. If `last_commit` is missing/empty → **first run** (build everything). Otherwise → **incremental run**.
2. Determine the changed set (see `references/sync.md` for exact commands — it covers the **submodule-aware** checkpoint, which plain git-wiki does not handle):
   - First run: candidate set from `git ls-files` at `HEAD`, excluding `docs/wiki/**` and filtering `*.meta`.
   - Incremental: `git diff --name-status -M <last_commit> HEAD` for the superproject **plus** a per-submodule diff against `submodule_commits` (the generators/analyzers live in separate git repos; the superproject diff only shows pointer bumps).
3. Map changed files → wiki pages via each page's `source_paths` frontmatter. Update or create only affected pages.
4. **Generated-code awareness (Aspid-specific):** `[ViewModel]`, `[Bind]`, `[RelayCommand]` emit the *other half* of each partial class at build time. Pages must describe the **generated** members and the relationships they create, not only the hand-written partial. A reader looking only at source files will not see them — the wiki is where that knowledge lives.
5. Advance the checkpoint: write the new superproject `HEAD` to `last_commit` and each submodule `HEAD` to `submodule_commits`, only **after** the full changed set is processed. Append an entry to `docs/wiki/log.md`.
6. Respect granularity: per-concept for the core, **per-category (not per-file)** for the ~593 StarterKit binders — see `references/taxonomy.md`.

For a full first-run ingest across the whole taxonomy, prefer a **Workflow** that fans out one agent per page (each reads its `source_paths` and writes per `references/page-templates.md`), then a Lint pass. Do not hand-write 50+ pages serially.

### 2. Query — answer a question from the wiki

1. Read `docs/wiki/index.md`, follow `[[wikilinks]]` to the relevant pages.
2. Answer from the wiki. **Verify against `source_paths`** before stating anything load-bearing — pages can drift or contain inferences.
3. If the answer required reading source the wiki should have covered, file it back: create/extend a page so the next query is cheaper (the artifact compounds).

### 3. Lint — detect drift and gaps

Report (do not auto-fix): pages whose `source_paths` no longer exist or changed significantly (mark `status: stale`, never delete), broken `[[wikilinks]]`, orphan pages (no inbound links), and changed modules with no covering page. Suggest dedicated pages for concepts referenced across ≥3 pages but lacking their own. See `references/sync.md`.

### 4. Export — bundle for sharing / GitBook / LLM feed

Concatenate all pages into one deterministic markdown file (sorted by path, TOC, per-file start/end markers), flattening `[[wikilinks]]` to plain text. The live vault stays Obsidian-native; the bundle is the portable artifact. (Bundling script lives under `scripts/` once added; until then, produce the bundle inline.)

## Conventions (read the references before writing pages)

- **`references/taxonomy.md`** — folder layout, page granularity (per-concept vs per-category), slug/naming rules.
- **`references/page-templates.md`** — frontmatter schema per page type, page templates, writing style (target a "smart newcomer", explain *why* before *what*, keep pages under ~500 words, separate facts from inferences).
- **`references/sync.md`** — the submodule-aware git-diff checkpoint mechanism, first-run vs incremental, stale handling.

## Hard rules

- Never put the vault inside `Assets/`. Always filter `*.meta` from any file scan.
- Page filenames are **human-readable** and unique across the vault (the filename is the Obsidian node label): code types mirror the identifier (`IViewModel.md`, `BindMode.md`), other pages use Title Case (`ViewModel Generation.md`). System files stay lowercase (`index.md`, `log.md`). Full rules in `references/taxonomy.md`.
- `Aspid.Collections` is an external UPM package (`tech.aspid.collections`), not in this repo — document it as an external dependency with a link out, never as an in-repo module.
- Generator DLLs are committed into `Assets/`; Unity consumes the DLL, not the submodule source. Capture this in the relevant `generation/` and `risks/` pages.

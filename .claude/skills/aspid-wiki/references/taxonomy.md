# Wiki taxonomy & granularity

The vault root is `docs/wiki/`. Top-level folders are fixed; pages go in the folder matching their type.

## Folders

| Folder | Holds | Granularity |
|---|---|---|
| `overview/` | What Aspid.MVVM is, architecture, getting started | A handful of orientation pages |
| `concepts/` | Framework concepts: viewmodel generation, data binding, bindable members, bind modes, relay commands, the source-generation pipeline, DI integration | **Per concept** |
| `entities/` | Core public contracts / API surface: `IViewModel`, `IRelayCommand`, `IBinder`, `View` | **Per contract** (the small, high-value `Source/` core) |
| `binders/` | The StarterKit binder catalog (~593 files, ~33 UI categories) | **Per category, NOT per file** — one page per UI-element family (texts, images, toggles, sliders, …). A per-class page would produce 500+ near-duplicate stubs and be unusable. |
| `converters/` | The ~44 StarterKit value converters | One overview page + clusters if needed |
| `flows/` | End-to-end execution paths: how a `[ViewModel]` becomes generated code, runtime binding resolution, View initialization | **Per flow** |
| `generation/` | The source-generation pipeline, keyed to the 3 submodules: source generator, Unity generators, analyzer (diagnostics) | **Per submodule/tool** |
| `risks/` | Gotchas that break the build/runtime: must-be-`partial`, committed generator DLLs, submodule init, .NET 9 SDK pin | **Per gotcha** |
| `reference/` | Samples, Unity editor tooling, external dependencies (Collections UPM) | Mixed, summary-level |
| `notes/` | Anything that doesn't fit yet | Free |

## Granularity principle

The codebase is **heavily skewed**: StarterKit is ~73% of files and the binder/converter catalog is wide, shallow, and repetitive; the conceptually dense core (`Source/`, 67 files) and the generators are small. **Invest depth in the small core + generation pipeline; summarize the large repetitive catalogs at category level.** A category page lists the binders in that family, the shared base class/pattern, the Mono/Switcher/Enum variants, and the one or two things that actually differ — not one page per class.

## File naming rules

Filenames are **human-readable** — they double as the node label in Obsidian's graph and file tree (Obsidian shows the filename, not the frontmatter `title`).

- Pages that *are* a code type → the **exact identifier**: `IViewModel.md`, `BindMode.md`, `IRelayCommand.md`.
- Concept / flow / risk / overview / binder-category pages → **Title Case with spaces**: `ViewModel Generation.md`, `Runtime Binding Resolution.md`, `Text Binders.md`, `Must Be Partial.md`.
- Names are **unique across the entire vault** — Obsidian resolves `[[links]]` by basename regardless of folder. Never create two names differing only in case (macOS is case-insensitive, so they collide).
- **No leading dot** in a filename (it hides the file on Unix and confuses tooling): write `NET 9 SDK Pin.md`, not `.NET 9 SDK Pin.md`.
- **System/meta files stay lowercase**: the hub is the only `index.md`; the journal is `log.md`. Section landing pages get a normal name (`Binders Catalog.md`), never a second `index.md`.
- `[[wikilinks]]` use the human name: `[[IViewModel]]`, `[[ViewModel Generation]]`. A link to a page that doesn't exist yet is fine — it marks a page worth writing (Obsidian shows it unresolved; Lint flags it as missing coverage).

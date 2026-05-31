---
title: External Dependencies
type: reference
status: active
source_paths:
  - Aspid.MVVM/Packages/manifest.json
  - CLAUDE.md
  - Readme.md
tags:
  - reference
  - dependencies
  - upm
  - integration
updated_at: 2026-05-31
---

# External Dependencies

> The third-party packages Aspid.MVVM relies on, how each is pulled in, and which ones are absent from this checkout — read this before chasing a "missing type" compile error.

Aspid.MVVM keeps its core (the part consuming the [[Committed DLLs]]) lean, but the [[Samples]] and [[StarterKit ViewModels|StarterKit]] components depend on several external packages declared in `Aspid.MVVM/Packages/manifest.json`. Most are pulled via UPM git URLs, not the asset; if you copy only `Assets/Aspid/` into another project you must re-add these yourself.

## Key dependencies

| Package | UPM id | Source | Used for |
|---|---|---|---|
| Aspid.Collections | `tech.aspid.collections` | git `#upm` | Observable collections (`ObservableList<T>`, etc.), [[Collection Binders]], [[VirtualizedList Binders]] |
| UniTask | `com.cysharp.unitask` | git `#2.1.0` | Allocation-free async/await utilities |
| VContainer | `jp.hadashikick.vcontainer` | git `#1.16.0` | [[DI Integration]] for [[View Initialization]] |
| Zenject | (not in manifest) | external | Alternate [[DI Integration]] target |
| TextMeshPro | via `com.unity.ugui` 2.0.0 | Unity registry | Text/[[InputField Binders]], [[Dropdown Binders]] |

## Aspid.Collections is NOT a submodule

This is the most common confusion. Unlike the generator/analyzer repos (see [[Submodule Init]]), Aspid.Collections is **not** a git submodule — it is a UPM git package (`tech.aspid.collections`). It is therefore **absent from this working tree**: there is no `Assets/Aspid/Collections/` folder in this checkout. Unity downloads it into the package cache when the project opens. The CLAUDE.md project map shows a `Collections/` entry, but that reflects the resolved-package layout, not files committed here.

If you `grep` for `ObservableList<T>` source and find nothing, that is expected — it lives in the external package, not this repo.

## DI is opt-in

Zenject and VContainer are integration *targets*, not hard requirements of the core. VContainer is wired into the manifest; Zenject appears in StarterKit assembly references and is supported but not declared here, so it likely must be added manually if you use it. The [[StarterKit ViewModels|StarterKit]] view initializers branch on whichever container is present. See [[DI Integration]].

## UniTask and TextMeshPro

UniTask (pinned `2.1.0`) backs async flows. TextMeshPro ships inside `com.unity.ugui` 2.0.0 (the modern Unity packaging), so the [[Text Binders]] and field binders that target `TMP_Text` resolve without a separate dependency line.

## Source

- `Aspid.MVVM/Packages/manifest.json` — authoritative dependency list
- `CLAUDE.md`, `Readme.md` — overview and integration notes

See also: [[Submodule Init]], [[Committed DLLs]], [[Getting Started]].

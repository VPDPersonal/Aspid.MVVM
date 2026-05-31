---
title: Unity Generators
type: generation
status: active
source_paths:
  - Aspid.MVVM.Unity.Generators/Readme.md
  - Aspid.MVVM.Unity.Generators/Directory.Build.targets
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/AddBinderContextMenuGenerator.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/ContextMenuData.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/Descriptions/Defines.UnityEngine.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/Descriptions/Classes.UnityEngine.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Helpers/CodeWriter.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Helpers/Extensions/Declarations/TypeDeclarationSyntaxExtensions.cs
tags: [generation, unity, roslyn, codegen, editor]
updated_at: 2026-05-31
---

# Unity Generators

> A separate Roslyn generator submodule (`Aspid.MVVM.Unity.Generators`) that emits Unity-Editor-only convenience code, plus the shared `Aspid.Generator.Helpers` codegen layer (CodeWriter, symbol/syntax extensions) used across all Aspid generators.

## Why it exists

Core generation (the [[Source Generator]]) is engine-agnostic. Unity-specific niceties — like wiring a binder into the Inspector's right-click menu — depend on `UnityEngine`/`UnityEditor` types and must be guarded behind `#if UNITY_EDITOR`. Isolating that into its own generator keeps the core clean while still removing hand-written boilerplate from [[IBinder|binder]] authors.

## How it works

The only generator here is `AddBinderContextMenuGenerator`, an `IIncrementalGenerator` triggered by `[AddBinderContextMenu]` via `ForAttributeWithMetadataName`. For each annotated `class` it builds a `ContextMenuData` record (declaration, symbol, menu `Path`/`SubPath`, priority) then emits an internal static `__<Name>Editor` class containing a `[MenuItem("CONTEXT/<Type>/<path>", priority=...)]` method. Invoking that menu item calls `gameObject.AddComponent(...)`, so a target component can be added straight from another component's context menu. The whole emission is wrapped in `#if UNITY_EDITOR`. When no explicit `Path` is given it falls back to the class's `[AddComponentMenu]` value, otherwise to `Add <Type> Binder/<Name>`.

The richer half of the submodule is `Aspid.Generator.Helpers`, the shared codegen toolkit:

- **`CodeWriter`** — an `IndentedTextWriter` wrapper exposing `Append`/`AppendLine`/`AppendMultiline`, `BeginBlock`/`EndBlock`, and disposable `BeginIndentScope`/`BeginBlockScope`. Produces a Roslyn `SourceText`.
- **Syntax extensions** (`TypeDeclarationSyntaxExtensions`, etc.) — derive `DeclarationText` (modifiers, `class`/`struct`, generic args), namespace name, and output file names from declaration nodes.
- **Symbol extensions** (`SymbolExtensions`, `TypeSymbolExtensions`) — attribute lookup (`HasAnyAttribute`), base-type/interface walks, global display strings.
- **Descriptions** (`Classes`, `Defines`, `Namespaces`, `General`) — typed constants like `MenuItem`, `MonoBehaviour`, `UNITY_EDITOR`, so generators reference Unity types by name without hard-coded strings.

These helpers (likely shared by the core [[Source Generator]] and [[Analyzer]] too) are what every Aspid emission is built from.

## Key relationships

- Consumes `[AddBinderContextMenu]`, defined in core runtime (`Unity/Runtime/ContextMenus/`).
- `Directory.Build.targets` copies the built DLL into `Assets/Aspid/MVVM/Unity/` — see [[Committed DLLs]]; Unity loads the DLL, never the source.
- Submodule layout and init: [[Submodule Init]]; SDK requirement: [[NET 9 SDK Pin]].
- Sits alongside [[ViewModel Generation]] and [[Architecture]] in the [[Source Generation Pipeline]].

## Source

`Aspid.MVVM.Unity.Generators/` (submodule) — `Generators/ContextMenu/` and `Helpers/`.

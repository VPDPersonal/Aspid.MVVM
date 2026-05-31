---
title: Source Generation Pipeline
type: concept
status: active
source_paths:
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Views/ViewGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Binders/BinderGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Ids/IdGenerator.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/AddBinderContextMenuGenerator.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeAnalyzer.cs
  - CLAUDE.md
tags: [concept, source-generation, roslyn, generators, analyzer]
updated_at: 2026-05-31
---

# Source Generation Pipeline

> The compile-time machinery that turns `[ViewModel]`, `[Bind]`, `[RelayCommand]` and `[View]` markers into real C# members, so you write intent and the framework writes the boilerplate — with zero reflection at runtime.

## Why it exists

MVVM is mostly mechanical glue: property change notification, binding member lookup, command wrappers, view-to-viewmodel wiring. Hand-writing that is slow and error-prone. Aspid moves it to **build time** via Roslyn, keeping runtime code allocation-free and reflection-free. See [[Architecture]] for the bigger picture.

## How it works

Three separate Roslyn tools cooperate, each shipped as a **committed DLL** that Unity consumes — **Unity never compiles the generator source** (it lives in submodules). After editing a generator you must rebuild its solution and re-commit the DLL. See [[Committed DLLs]], [[Submodule Init]], [[NET 9 SDK Pin]].

1. **[[Source Generator]]** (`Aspid.MVVM.Generators.dll`) — the core incremental generators:
   - `ViewModelGenerator` reacts to `[ViewModel]` and emits [[ViewModel to Generated Code|the IViewModel implementation]]: bindable members, generated properties + change-notification, `RelayCommand` wrappers, and a `FindBindableMembers` body. It requires `partial` and rejects `static` classes.
   - `ViewGenerator` reacts to `[View]` and emits the [[View]] wiring (also `partial`-only).
   - `BinderGenerator` targets `partial` types whose base list implies [[IBinder]], generating binder plumbing.
   - `IdGenerator` emits binding-member id constants used by [[Runtime Binding Resolution]].
2. **[[Unity Generators]]** (`Aspid.MVVM.Unity.Generators.dll`) — Unity-specific output. For example `AddBinderContextMenuGenerator` reads `[AddBinderContextMenu]` to generate Editor context-menu entries that add binders to a [[View]]. See [[Unity Editor Tooling]].
3. **[[Analyzer]]** (`Aspid.MVVM.Analyzers.dll`) — diagnostics + code fixes, not generation:
   - `PartialAnalyzer` raises `AM0003`–`AM0006` (errors) when a `[View]`/`[ViewModel]` class or struct is not `partial` — see [[Must Be Partial]].
   - `BindAttributeAnalyzer` raises `AM0001` (error) / `AM0002` (warning) when a `[Bind]`-marked backing field is read/written directly instead of through its generated property.

All generators are `IIncrementalGenerator`s: a syntactic predicate cheaply filters candidate declarations, a semantic `Find` step builds a data model, and `RegisterSourceOutput` emits the code. This caching keeps incremental compiles fast (inference: standard incremental-generator behavior).

## Key relationships

- `[ViewModel]` + `[Bind]` + `[RelayCommand]` -> generated members: [[ViewModel Generation]], [[Bindable Members]], [[Relay Commands]], [[BindMode]].
- `[View]` -> generated wiring: [[View Initialization]].
- Generated ids feed [[Runtime Binding Resolution]] and [[Data Binding]].
- `Aspid.Collections` is an external UPM package (`tech.aspid.collections`), not produced by this pipeline — see [[External Dependencies]].

## Source

- [[Source Generator]], [[Unity Generators]], [[Analyzer]], [[Committed DLLs]]

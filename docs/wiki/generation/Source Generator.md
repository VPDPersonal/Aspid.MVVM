---
title: Source Generator
type: generation
status: active
source_paths:
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Views/ViewGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Binders/BinderGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Ids/IdGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Descriptions/Classes.Aspid.cs
tags: [generation, roslyn, source-generator, viewmodel, view, binder]
updated_at: 2026-05-31
---

# Source Generator

> The Roslyn incremental generator that turns `[ViewModel]`, `[View]`, binders and `[BindId]` markers into the MVVM plumbing — so the framework binds with zero reflection. It ships as a **committed DLL**, not source.

## Why it exists

Hand-writing `IViewModel` implementations, bindable-member wiring, binder caching and string id constants is repetitive and error-prone. The generator emits all of it at compile time, keeping bindings allocation-free and reflection-free. Because Unity consumes the built artifact ([[Committed DLLs]]), the generator source lives in a submodule ([[Submodule Init]]) and must be rebuilt with the [[NET 9 SDK Pin|.NET 9 SDK]] and re-committed after changes.

## How it works

Each sub-generator is a separate `[Generator]` implementing `IIncrementalGenerator`. They register an attribute/syntax provider, run a syntactic predicate, build a data model, then emit partial classes:

| Sub-generator | Trigger | Emits |
|---|---|---|
| **ViewModels** | `[ViewModel]` on a `partial` class | `IViewModel` impl, bindable members, `FindBindableMember`, generated properties + change notification, `[RelayCommand]` command properties |
| **Views** | `[View]` on a `partial` class | `Initialize`, cached binder fields, generic view init |
| **Binders** | `partial` type with a base list (likely `IBinder`) | `BinderLog` partial (editor debug logging) |
| **Ids** | `partial` type with `[BindId]`-style attributes | `Aspid.MVVM.Generated.Ids` with `const string` id constants |
| **CreateFrom** | `[CreateFrom]` attribute (folder is a stub here) | nothing in this repo |
| **Descriptions** | n/a | shared metadata: type/namespace/attribute name tables |

ViewModels and Views use `ForAttributeWithMetadataName` (the fast, attribute-driven path); Binders and Ids use `CreateSyntaxProvider`. All predicates require `partial` and reject `static` ([[Must Be Partial]]). The **Ids** generator is collection-based: it gathers ids across all types via `provider.Collect()` and writes one shared `Ids` class.

**Descriptions** is not a generator — `Classes.Aspid.cs` and friends are constant tables ([[IViewModel]], [[BindMode]], [[IBinder]], attribute names) that the other generators reference so emitted code uses correct fully-qualified names.

**CreateFrom** here contains only a stub; the active `[CreateFrom]` handling appears to live in [[Unity Generators]] (this repo only declares the `CreateFromAttribute` name).

## Key relationships

- ViewModel emission detail: [[ViewModel to Generated Code]], [[ViewModel Generation]].
- View emission and runtime: [[View]], [[View Initialization]], [[Runtime Binding Resolution]].
- Command emission: [[Relay Commands]], [[IRelayCommand]].
- Validation companion: [[Analyzer]] (diagnostics, e.g. missing `partial`).
- End-to-end view: [[Source Generation Pipeline]], [[Architecture]].

## Source

`Aspid.MVVM.Generators/.../Generators/` — `{ViewModels,Views,Binders,Ids,Descriptions,CreateFrom}/`. Each sub-generator splits into `*.cs` (`Initialize` + predicate), `*.Find.cs` (data model) and `*.Generate.cs` (emit). Built DLL: `Aspid.MVVM.Generators.dll` under `Assets/Aspid/MVVM/`.

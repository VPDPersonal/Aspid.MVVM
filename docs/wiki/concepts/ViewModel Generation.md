---
title: ViewModel Generation
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
tags: [generation, viewmodel, bind]
updated_at: 2026-05-31
---

# ViewModel Generation

> `[ViewModel]` turns a hand-written `partial` class into a full [[IViewModel]]; `[Bind]` turns a field into an observable bindable member. The generated half is invisible in your source — this page is where it's documented.

## Why it exists

You write the data (fields) and intent (attributes); the generator writes the mechanical [[IViewModel]] plumbing. No reflection, no hand-rolled change notification.

## How it works

- `[ViewModel]` (`ViewModelAttribute`, valid on class/struct) drives the generator to emit the [[IViewModel]] implementation — notably `FindBindableMember(...)` — for the decorated type and to analyze its members.
- `[Bind]` (`BindAttribute`, on a field) emits a **bindable property** wrapping that field, with change-notification wired to the binders bound to it.
- The decorated type **must be `partial`** — the generator emits a second partial; otherwise you get compile errors. → [[Must Be Partial]]

## Default bind mode (worth memorising)

`[Bind]` with no argument picks the mode by field mutability:

| Field | Default mode |
|---|---|
| mutable | `TwoWay` |
| `readonly` | `OneTime` |

With `[Bind(mode)]` on a `readonly` field, only `OneTime`/`OneWay` are accepted (both behave as `OneTime`); other modes are rejected. Full enum: [[BindMode]].

## Key relationships

- Produces an [[IViewModel]] implementation consumed by [[Binders Catalog|binders]].
- `[RelayCommand]` methods become `IRelayCommand` properties via the same pipeline → [[Relay Commands]].
- End-to-end build walkthrough: [[ViewModel to Generated Code]].

## Source

`ViewModelAttribute.cs`, `BindAttribute.cs`, `IViewModel.cs`, and `ViewModelGenerator.cs` (in the `Aspid.MVVM.Generators` submodule — Unity consumes the **built DLL**, not this source).

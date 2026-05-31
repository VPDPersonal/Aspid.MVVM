---
title: "Risk: types must be partial"
type: risk
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM.Analyzers
tags: [risk, generation, partial]
updated_at: 2026-05-31
---

# Risk: types must be `partial`

> **Symptom:** compile errors (missing members, or "no implementation for IViewModel") on a class using `[ViewModel]`, `[Bind]`, or `[RelayCommand]`.

## Cause

These attributes drive a source generator that emits a **second `partial` declaration** of the type (the [[IViewModel]] implementation, bindable properties, command properties). If the type is not declared `partial`, there is nowhere for the generated half to land, so references to generated members don't resolve. → [[ViewModel Generation]]

## Fix

Declare the type `partial`:

```csharp
[ViewModel]
public partial class CounterViewModel   // <-- partial
{
    [Bind] private int _count;
}
```

## Prevention

The `Aspid.MVVM.Analyzers` submodule ships a Roslyn diagnostic that flags `[ViewModel]`/`[Bind]`/`[RelayCommand]` on non-`partial` types, so this surfaces as an analyzer warning/error in the IDE before a full build. Don't suppress it.

## Related

- [[Committed DLLs]] — the analyzer/generator run from **committed DLLs**; if the analyzer isn't firing, the DLL may be stale or the submodule uninitialised ([[Submodule Init]]).

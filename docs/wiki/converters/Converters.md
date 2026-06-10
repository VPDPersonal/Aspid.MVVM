---
title: Converters
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/IConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/ConverterExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/GenericFuncConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/SequenceConverters.cs
tags: [converter, starterkit, binding]
updated_at: 2026-05-31
---

# Converters

> Small one-way transforms that sit between a bindable member and a binder, reshaping a value (e.g. `int` -> `string`, `null` -> `bool`) so the View can display data the ViewModel never has to know about.

## Why it exists

A bindable member exposes the data type that makes sense for business logic. A binder needs the data type the UI element expects. Converters bridge that gap **without** polluting the ViewModel with View concerns. Because [[Data Binding]] in Aspid is generated and reflection-free, converters are plain serializable objects you compose in the Inspector, not runtime-resolved magic.

## How it works

The contract is intentionally tiny:

```csharp
public interface IConverter<in TFrom, out TTo>
{
    TTo Convert(TFrom value);
}
```

`in`/`out` variance lets a single converter slot accept compatible types. The conversion is **one-directional** — `Convert` maps `TFrom` to `TTo`. There is no `ConvertBack`, so converters pair naturally with one-way flows (see [[BindMode]]); reverse conversion appears to require a separate converter.

Three building blocks live alongside the families:

- **`GenericFuncConverter<TFrom, TTo>`** — adapter wrapping a `Func<TFrom?, TTo?>` (or another `IConverter`) as an `IConverter`. Lets code-defined lambdas plug into a converter slot.
- **`ConverterExtensions.ToConvert(...)`** — extension that turns any `Func<TFrom?, TTo?>` into an `IConverter` via `GenericFuncConverter`.
- **`SequenceConverters<T>`** — chains an `IConverter<T,T>[]`, applying each in order. On Unity 2023.1+ the array uses `[SerializeReference]` with a dropdown, so you assemble pipelines visually.

## Key relationships

Converters are referenced by binders that accept a converter field (commonly the [[Caster Binders]] and many [[Text Binders|StarterKit binders]]). The binder reads the source value from a bindable member, calls `Convert`, then pushes the result into the UI element — so converters are a stateless step inside [[Runtime Binding Resolution]]. They are part of the StarterKit, not the core [[Architecture]].

The four families:

| Family | Purpose |
|--------|---------|
| [[Bool Converters]] | comparisons, null/empty/number -> `bool` |
| [[Number Converters]] | arithmetic operations on numeric values |
| [[String Converters]] | `ToString`, formatting, culture, `TimeSpan` |
| [[Specific Converters]] | typed bool/number/string variants for concrete bindings |

## Source

`StarterKit/Runtime/Converters/` — `IConverter.cs`, `GenericFuncConverter.cs`, `ConverterExtensions.cs`, `SequenceConverters.cs`, plus the `Bools/`, `Numbers/`, `Strings/`, `Specific/` subfolders.

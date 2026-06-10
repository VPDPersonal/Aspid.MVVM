---
title: Bool Converters
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/NumberToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/ObjectNullToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/StringEmptyToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/Comparisons.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/IConverterObjectToBool.cs
tags: [converter, bool, starterkit]
updated_at: 2026-05-31
---

# Bool Converters

> StarterKit converters that turn a non-bool source value (a number, an object reference, or a string) into a `bool`, so a bindable member can drive a binder that expects a boolean (e.g. enabled/visible/interactable state).

## Why it exists

A ViewModel often exposes a number (`Health`), a reference (`SelectedItem`), or a string (`SearchText`), but a target binder wants a `bool` ("is the button interactable?", "show this panel?"). Rather than add a bool property to the ViewModel for every UI rule, you drop a reusable, serializable converter on the binder. Each converter is `[Serializable]` with `[SerializeField]` config, so its threshold/inversion is editable in the Unity Inspector. See [[Converters]] for the family overview.

## How it works

Each class implements one or more typed converter interfaces from the [[Specific Converters|Specific Converter]] set (`IConverter<TFrom, bool>`):

| Converter | Implements | Rule |
|-----------|------------|------|
| `NumberToBoolConverter` | `IConverter{Float,Double,Int,Long}ToBool` | `value` vs configured `_value` via a `Comparisons` op |
| `ObjectNullToBoolConverter` | `IConverterObjectToBool` | `value is null` (optionally inverted) |
| `StringEmptyToBoolConverter` | `IConverterStringToBool` | `string.IsNullOrEmpty(value)` (optionally inverted) |

`NumberToBoolConverter` is the richest: it accepts four numeric types but performs every comparison in `double` (`Compare(double)`) so large `long`/`double` magnitudes compare exactly without float rounding. The op comes from the `Comparisons` enum: `Equal`, `Inequality`, `LessThan`, `GreaterThan`, `LessThanOrEqual`, `GreaterThanOrEqual`. `Equal`/`Inequality` use an `Approximately` tolerance (relative epsilon) rather than exact `==`, which avoids false negatives on float drift.

The two boolean-flag converters share a shape: a single `_isInvert` `[SerializeField]`, a default ctor (`isInvert: false`) and a one-arg ctor. The result is `_isInvert ? !x : x` — so `ObjectNullToBoolConverter(isInvert: true)` reads as "true when set", and `StringEmptyToBoolConverter(isInvert: true)` reads as "true when non-empty".

## Key relationships

- Consumed by binders that take a `bool` source (Toggle/CanvasGroup/GameObject active state) wired through [[IBinder]] and [[Data Binding]].
- Sibling families: [[Number Converters]], [[String Converters]], [[Specific Converters]].
- The `Comparisons` enum is local to this folder but reused conceptually by other comparison converters (appears to be the canonical comparison op set).

## Source

`StarterKit/Runtime/Converters/Bools/` — `NumberToBoolConverter.cs`, `ObjectNullToBoolConverter.cs`, `StringEmptyToBoolConverter.cs`, `Comparisons.cs`. Interfaces in `Converters/Specific/Bool/`.

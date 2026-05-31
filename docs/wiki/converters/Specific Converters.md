---
title: Specific Converters
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Numbers/IConverterIntToFloat.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Numbers/NumberConverterSpecificExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/IConverterIntToBool.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/BoolConverterSpecificExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Strings/IConverterTimeSpanToString.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/IConverter.cs
tags: [converter, starterkit, binding, type-safety]
updated_at: 2026-05-31
---

# Specific Converters

> Strongly-typed marker interfaces (e.g. `IConverterIntToFloat`) that pin both ends of a conversion so a binder field can demand an exact converter shape instead of a loose `IConverter<,>`.

## Why it exists

[[Converters|IConverter]]`<in TFrom, out TTo>` (one method, `TTo Convert(TFrom)`) is generic. Unity cannot serialize an open generic into an inspector slot, and a binder author wants a field that accepts *only* "int → float" converters, not any converter. The `Specific` folder solves this by declaring closed, named marker interfaces — `IConverterIntToFloat : IConverter<int, float>` — concrete enough for a serialized-reference slot while still satisfying the generic contract.

## How it works

Each file is a one-line interface that closes the generic over a concrete pair. They group by the *target* family:

| Group | Count | Shape | Examples |
|-------|-------|-------|----------|
| Numbers | 16 | every pairing of `int`/`long`/`float`/`double` | `IConverterInt`, `IConverterIntToFloat`, `IConverterDoubleToLong` |
| Bool | 6 | `int`/`long`/`float`/`double`/`object?`/`string?` → `bool` | `IConverterIntToBool`, `IConverterStringToBool` |
| Strings | 3 | `string?`/`object?`/`TimeSpan` → `string?` | `IConverterString`, `IConverterTimeSpanToString` |

Same-type pairs (`IConverterInt : IConverter<int,int>`) exist so an in-place transform still satisfies a typed slot.

Each group ships a `*SpecificExtensions` static class (e.g. `NumberConverterSpecificExtensions`) with two factories per interface:

- `ToConvert(this Func<TFrom,TTo>)` — wraps a lambda.
- `ToConvertSpecific(this IConverter<TFrom,TTo>)` — re-wraps an existing generic converter.

Both return the closed interface, built on a private sealed class deriving from `GenericFuncConverter<TFrom,TTo>`. So `((Func<int,float>)(i => i / 100f)).ToConvert()` yields an `IConverterIntToFloat` ready to drop into a binder field.

These are plain runtime types — no `[ViewModel]`/`[Bind]` generation is involved. They appear to exist purely to give the [[Bindable Members]] / binder layer compile-time-checked converter slots; the numeric matrix is exhaustive precisely so any cross-numeric coercion a [[Number Converters|number binder]] needs is already named.

## Key relationships

- Closes [[Converters|IConverter]]`<TFrom,TTo>` over concrete pairs.
- Complements the behavior-rich families: [[Bool Converters]], [[Number Converters]], [[String Converters]] — those carry conversion *logic*; these carry only *type identity*.
- Consumed by binders selecting a converter — see [[Binder Base Classes]], [[Image Binders]], [[Slider Binders]].

## Source

`StarterKit/Runtime/Converters/Specific/{Bool,Numbers,Strings}/` — 25 marker interfaces plus 3 `*SpecificExtensions` factory classes.

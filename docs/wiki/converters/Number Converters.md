---
title: Number Converters
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Numbers/ArithmeticNumberConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Numbers/NumberOperation.cs
tags:
  - converter
  - numbers
  - starterkit
updated_at: 2026-05-31
---

# Number Converters

> Apply an arithmetic operation (`+ - × ÷`) with a fixed coefficient to a numeric binding value, while transparently bridging `int`, `long`, `float`, and `double`.

## Why it exists

ViewModel data and the View's display unit rarely match: a raw `int` health value may need scaling for a slider, or a `float` needs an offset before display. Rather than hand-writing a one-off [[Converters|converter]] per case, `ArithmeticNumberConverter` covers the common "scale/offset" need declaratively. Because it is `[Serializable]`, it can be configured directly in the Unity Inspector on a binder — no code change to retune a coefficient.

## How it works

The family has two types:

| Type | Role |
|------|------|
| `ArithmeticNumberConverter` | The converter itself — operation + coefficient |
| `NumberOperation` | enum: `Plus`, `Minus`, `Division`, `Multiply` |

The converter implements **16 `IConverter<TIn,TOut>` interface variants** across `int/long/float/double`. This means one instance can satisfy a binder expecting any of those numeric in/out pairings, so [[Runtime Binding Resolution]] can pick it up regardless of the bound member's numeric type.

The implementation is deliberately funnelled: every variant casts to `double`, delegates to the single `IConverter<double, double>.Convert`, then casts the result back to the target type. That core method is the only place the `NumberOperation` switch lives — `value + _coefficient`, `value - _coefficient`, `value * _coefficient`, or a guarded divide.

**Division-by-zero is safe:** if `_coefficient == 0`, `Divide` logs a `Debug.LogError` and returns the input unchanged rather than throwing or producing `Infinity`.

Two serialized fields, `_operation` and `_coefficient`, are wrapped in `#if UNITY_2022_1_OR_NEWER` `[SerializeField]` guards so the type also compiles outside Unity. A parameterless constructor (for serialization) and a `(operation, coefficient)` constructor (for code) are both provided.

> Note: all interface methods are **explicitly implemented**, so you must reference the converter through the relevant `IConverter<,>` interface — the `Convert` methods are not visible on the concrete type.

## Key relationships

- Consumed wherever a binder declares a numeric converter slot — see [[Slider Binders]], [[Scrollbar Binders]], [[Text Binders]], and the broader [[Binders Catalog]].
- Sibling converter families: [[Bool Converters]], [[String Converters]], [[Specific Converters]].
- Tied into binding via the `IConverter<TIn,TOut>` contract and [[BindMode]] one-way flows; appears designed for value-display (read) paths.

## Source

- `ArithmeticNumberConverter.cs`, `NumberOperation.cs` — `StarterKit/Runtime/Converters/Numbers/`

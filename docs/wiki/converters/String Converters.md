---
title: String Converters
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/StringFormatConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/GenericToString.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/ObjectToStringConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/TimeSpanToStringConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/CultureInfoMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/Extensions/ToCultureStringExtensions.cs
tags: [converter, string, formatting, culture, starterkit]
updated_at: 2026-05-31
---

# String Converters

> StarterKit converters that turn ViewModel values into display strings — applying `string.Format` templates and culture-aware number formatting before a text binder shows them.

## Why it exists

A bindable member often holds a raw value (a number, a `TimeSpan`, an arbitrary object) but the View wants a formatted label like `"Score: 1500"`. Rather than polluting the ViewModel with presentation strings, you attach a serializable converter to the binder. These converters slot into [[Data Binding]] and feed [[Text Binders]] / [[InputField Binders]], keeping formatting concerns in the View layer.

## How it works

All members are `[Serializable]` classes implementing a converter interface (`IConverter<TFrom?, string?>` or a domain marker like `IConverterString`), so they appear as inline-configurable fields in the Unity Inspector. Each exposes a single `Convert(value)` returning `string?`. See [[Converters]] for the shared interface model.

`StringFormatConverter` — string -> string. Applies a serialized `_format` via `string.Format(_format, value)`. If `_format` is blank it returns the value unchanged. `_formatEmptyValues` (default `false`) controls whether empty/whitespace inputs still get wrapped by the template, otherwise they pass through untouched.

`GenericToString<TFrom>` — the base for value-to-string conversion. Returns `null` for `null` input; with a `_format` it calls `string.Format`, otherwise it calls a `protected virtual ToStringValue(value)` (default `value.ToString()`) that subclasses can override.

`ObjectToStringConverter` (`GenericToString<object?>`) and `TimeSpanToStringConverter` (`GenericToString<TimeSpan>`) — sealed concrete specializations exposing the generic via domain marker interfaces so they are pickable in the Inspector.

`CultureInfoMode` + `ToCultureStringExtensions` — a serializable enum (CurrentCulture, InvariantCulture, etc.) plus extensions mapping it to a `CultureInfo` and providing `ToCultureString(this int/uint/long/double/float, CultureInfoMode)` overloads. These let callers format numbers under an explicit culture without hard-coding `CultureInfo` references. They appear to be helpers consumed by [[Number Converters]] rather than standalone `IConverter` types.

## Key relationships

- Implement [[Converters]] interfaces; sit inside a binder configured per [[BindMode]] (typically OneWay to a text sink).
- Sibling families: [[Number Converters]], [[Bool Converters]], [[Specific Converters]].
- `GenericToString<TFrom>` is the reusable base; the override hook makes it extensible without new plumbing.

## Source

`StarterKit/Runtime/Converters/Strings/` — `StringFormatConverter.cs`, `GenericToString.cs`, `ObjectToStringConverter.cs`, `TimeSpanToStringConverter.cs`, `CultureInfoMode.cs`, `Extensions/ToCultureStringExtensions.cs`.

---
title: Caster Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Casters
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Casters
tags:
  - binder
  - caster
  - converter
  - starterkit
updated_at: 2026-05-31
---

# Caster Binders

> Bridge binders that convert a ViewModel value of one type into another type before forwarding it to a setter or [[UnityEvent Binders|UnityEvent]].

## Why they exist
A bindable member exposes one type, but the consumer needs another (a `TimeSpan` shown as `string`, a `Vector2` driving a `Vector3` slot, a `string` toggling a `bool`). Rather than reshaping the ViewModel, a Caster binder sits in the middle: it receives the source type, runs an [[Converters|IConverter]], and emits the target type. This keeps the ViewModel type-clean and pushes type adaptation into the View layer.

## How they work
Each caster holds an `IConverter<TFrom, TTo>` plus a sink (a delegate or a `UnityEvent<TTo>`). `SetValue` calls `_converter.Convert(value)` and forwards the result. Mode is restricted: constructors call `mode.ThrowExceptionIfTwo()`, so casters are [[BindMode|OneWay]]-style only — they do not push back to source. A default converter is supplied when none is given (e.g. `GenericToString<object>`, `StringEmptyToBoolConverter`, `Vector2ToVector3Converter`).

## Family

| Binder | From → To | Default converter |
|--------|-----------|-------------------|
| `AnyToStringCasterBinder` ([[IBinder]] via `IAnyBinder`) | any → string | `GenericToString<object>` |
| `GenericToStringCasterBinder` | T → string | (generic) |
| `StringToBoolCasterBinder` | string → bool | `StringEmptyToBoolConverter` |
| `GenericToStringCasterMonoBinder<T>` (abstract base) | T → string | supplied per subclass |
| `TimeSpanToStringCasterMonoBinder` | TimeSpan → string | `TimeSpanToStringConverter` |
| `Vector2ToVector3CasterMonoBinder` / `Vector3ToVector2CasterMonoBinder` | Vector2 ↔ Vector3 | `Vector2ToVector3Converter` |
| `AnyToStringCasterMonoBinder`, `StringToBoolCasterMonoBinder` | as above | inspector-assigned |

## Variant axes
- **Plain vs Mono** — Plain casters extend [[Binder Base Classes|Binder]] and take a `setValue` `Action` in their constructor (code-driven). Mono casters extend `MonoBinder` (see [[Mono Binders]]), expose a serialized `IConverter` (`[SerializeReference]` + dropdown) and a `UnityEvent<TTo>` sink wired in the Inspector. They register under `Aspid/MVVM/Binders/Casters` via add-menu attributes.
- **Generic base + concrete subclass** — `GenericToStringCasterMonoBinder<T>` is `abstract partial`; concrete types (`TimeSpanToStringCasterMonoBinder`) just close the generic. The base branches on `UNITY_2023_1_OR_NEWER`: newer Unity serializes the generic `IConverter<T,string>` directly, older Unity needs a non-generic interface plus an abstract `Converter` property override.
- *(Inferred)* No Switcher / Enum / EnumGroup / OneWayToSource variants exist in this folder — those axes live in other binder categories, not in Casters.

## Notable behavior
- Mono casters guard a missing converter: they `Debug.LogError` and return without invoking the event; `[BinderLog]` decorates `SetValue`.
- `OnValidate` re-assigns the default converter when the Inspector field is cleared.

## Source
- `StarterKit/Runtime/Binders/Casters/` — plain `Binder` casters.
- `StarterKit/Unity/Runtime/Binders/Casters/Mono/` — `MonoBinder` casters.
- Converters live under [[Converters]]; see also [[Binders Catalog]].

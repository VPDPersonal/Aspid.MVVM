---
title: Binder Base Classes
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/TargetProperty/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/TargetProperty/TargetFloatBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/OneWayValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/TwoWayValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/OneWayToSourceValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Switchers/SwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericOneWayBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericCasterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericOneWayToSourceBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Casters/AnyToStringCasterBinder.cs
tags: [concept, binders, starterkit, base-classes]
updated_at: 2026-05-31
---

# Binder Base Classes

> The pure-C# building blocks under `StarterKit/Runtime/Binders` that every concrete binder category reuses, so each UI-specific binder only writes a tiny target accessor.

## Why it exists

Without shared bases, every Unity binder ([[Text Binders]], [[Slider Binders]], etc.) would re-implement [[IBinder]]/`IReverseBinder` plumbing: bind-mode validation, optional [[Converters|conversion]], and `OnBound` push-back. These five families absorb that boilerplate so concrete binders supply only a `Property` accessor or a setter delegate.

## How it works

All families derive from `Binder` (which holds `Mode` and exposes the `OnBound`/`OnUnbinding` hooks) and validate the requested [[BindMode]] in their constructor (`ThrowExceptionIfTwo`/`ThrowExceptionIfNone`).

- **TargetProperty** — abstract `TargetBinder<TTarget, TProperty>` binds a `Property { get; set; }` you override. `SetValue` writes the property; in `OneWayToSource` `OnBound` reads it back to the ViewModel. The 3-arg variant adds a serialized `IConverter`. Typed shortcuts (`TargetFloatBinder`, `TargetIntBinder`, `TargetBoolBinder`, `TargetStringBinder`) implement multi-type interfaces like `INumberBinder` so one binder accepts `int`/`long`/`float`/`double`.
- **Values** — concrete `OneWayValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` (a `TwoWayValue` locked to one mode), and `OneTimeValue`. They *store* the latest value (serialized `_value`), raise `Changed`/`ValueChanged`, and offer an implicit `T` conversion. Used as serialized fields inside generated Views rather than subclassed.
- **Switchers** — `SwitcherBinder<T>` / `<TTarget,T>` / `<TTarget,T,TConverter>` take a bound `bool` and forward a pre-configured `_trueValue` or `_falseValue` to an abstract `SetValue(T)`. Two-way is rejected.
- **Generics** — `GenericOneWayBinder`, `GenericTwoWayBinder`, `GenericOneWayToSourceBinder`, `GenericCasterBinder` wrap delegates (`Action<T>`, `Func<T>`) instead of overrides. A `<TTarget,...>` overload stores the target separately to avoid closure capture on Unity components.
- **Casters** — `GenericCasterBinder<TFrom,TTo>` plus ready casters (`AnyToStringCasterBinder` via `IAnyBinder`, `StringToBoolCasterBinder`) run an `IConverter` between bound and target types.

## Key relationships

- Implement [[IBinder]] / `IReverseBinder`; consumed during [[Runtime Binding Resolution]].
- Conversion is delegated to [[Converters]] (`IConverter<TFrom,TTo>`).
- Concrete Unity families ([[Image Binders]], [[Transform Binders]], [[Toggle Binders]], …) extend these; see [[Binders Catalog]] and [[Mono Binders]].
- `[BindModeOverride]` constrains the Inspector mode dropdown (see [[BindMode]]).

## Source

`StarterKit/Runtime/Binders/{TargetProperty,Values,Switchers,Generics,Casters}/`

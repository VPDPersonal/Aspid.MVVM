---
title: Generic Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Generics
tags:
  - binders
  - starterkit
  - generics
updated_at: 2026-05-31
---

# Generic Binders

> Reusable, target-agnostic [[IBinder]] implementations that bridge a ViewModel value of type `T` to any setter delegate — the escape hatch when no purpose-built binder (Text, Image, etc.) fits.

## Why it exists

Most StarterKit binders wrap one Unity component. Generic binders invert that: instead of hard-coding a target, they accept a setter (and optionally a getter/event) at construction. This lets code-driven bindings — or new custom binders — wire a [[Bindable Members|bindable member]] to an arbitrary property without writing a dedicated class. They are the building block other binders and the [[Caster Binders]] family lean on.

## Family table

| Variant | Implements | Direction |
|---------|-----------|-----------|
| `GenericOneWayBinder<T>` | `IBinder<T>` | ViewModel → View |
| `GenericOneTimeBinder<T>` | (subclass of OneWay) | once, then stops |
| `GenericTwoWayBinder<T>` | `IBinder<T>`, `IReverseBinder<T>` | both directions |
| `GenericOneWayToSourceBinder<T>` | `IReverseBinder<T>` | View → ViewModel |
| `GenericCasterBinder<TFrom,TTo>` | `IBinder<TFrom>` | ViewModel → View, converted |

Each lives in `Aspid.MVVM.StarterKit` and derives from [[Binder Base Classes|Binder]].

## Variant axes

- **Plain vs Unity.** Two parallel sets exist. The plain set (`GenericOneWayBinder<T>`, …) takes a `System.Action<T?>` setter. The Unity set (`UnityGenericOneWayBinder<T>`, …) is identical but takes a `UnityEngine.Events.UnityAction<T?>`, suiting Unity-event-style callbacks. Unity OneWay/OneTime constructors are `protected` (meant to be subclassed), the plain ones `public`.
- **Single vs target-scoped.** Every type ships in two arities: `<T>` captures the setter directly, while `<TTarget, T>` stores a `TTarget target` and passes it as the first setter arg. The target-scoped form avoids allocating a closure when using method-group setters on a component — a deliberate allocation optimization (per XML docs).
- **Caster arities.** `GenericCasterBinder` adds an [[Converters|IConverter]] argument and grows a `<TTarget, TFrom, TTo>` overload alongside `<TFrom, TTo>`.

## Notable behavior

- **TwoWay throws on misuse.** OneWay/OneTime/Caster call `mode.ThrowExceptionIfTwo()` — passing [[BindMode|BindMode.TwoWay]] throws. `GenericOneTimeBinder` simply hard-codes `BindMode.OneTime`.
- **Reverse flow via `initialize`.** `GenericTwoWayBinder` and `GenericOneWayToSourceBinder` take an `Action<Action<T>> initialize`; they hand their internal `OnValueChanged` callback to that action so the caller registers it on a View-side event. They raise `ValueChanged` to push values back.
- **Bound/unbound hooks.** Both reverse-capable binders accept optional `onBoundValueChanged` / `onUnboundValueChanged` factories; their results are pushed to the ViewModel on `OnBound` / `OnUnbinding`. The parameterless `OneWayToSource` constructor requires at least one of them.

These are runtime/code-level binders (not MonoBehaviour components); for the inspector-facing equivalents see the per-component pages in the [[Binders Catalog]]. The brief's "Switcher / Enum / EnumGroup / MonoBinder" axes are **not** present in this folder — those patterns appear in other binder families.

## Source

- `StarterKit/Runtime/Binders/Generics/` — plain (`System.Action`) variants.
- `StarterKit/Unity/Runtime/Binders/Generics/` — `UnityAction` variants.
- `XmlExampleDoc-Generics-1.1.0.xml`, `XmlExampleDoc-UnityGenerics-1.1.0.xml` — `<example>` includes.

See also [[Data Binding]], [[Runtime Binding Resolution]], [[Caster Binders]], [[Mono Binders]].

---
title: Mono Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentProperty/ComponentMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentProperty/ComponentColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/Switchers/SwitcherMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/Enums/EnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/EnumGroups/EnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentToSourceMonoBinder.cs
tags: [binders, starterkit, mono, abstract-base]
updated_at: 2026-05-31
---

# Mono Binders

> The reusable abstract scaffolding the StarterKit's concrete binders inherit from — they map a generic bound value onto a Unity `Component` property so you rarely write binding plumbing by hand.

This is not a leaf binder family you drop on a GameObject directly; it is the set of generic base classes that nearly every concrete StarterKit binder ([[Text Binders]], [[Image Binders]], [[Slider Binders]], etc.) extends. Understanding the axes here explains how the whole [[Binders Catalog]] is structured.

## Family table

| Base | Binds | What it does |
|------|-------|--------------|
| `ComponentMonoBinder<TComponent>` | (component handle) | Caches the target `Component`; root of the property family. |
| `ComponentMonoBinder<TComponent, TProperty>` | `TProperty` | Reads/writes one component property via abstract `Property` get/set. |
| `ComponentMonoBinder<TComponent, TProperty, TConverter>` | `TProperty` | Same, with an optional serialized [[Converters\|converter]]. |
| `SwitcherMonoBinder<…>` | `bool` | Picks one of two serialized values by a bound bool. |
| `EnumMonoBinder<…>` | `Enum` | Maps a bound enum to a value via an `EnumValues<T>` table. |
| `EnumGroupMonoBinder<…>` | `Enum` | Applies "selected" to the matching element, "default" to the rest. |
| `ComponentToSourceMonoBinder<TComponent>` | (reverse) | Pushes the component reference back to the ViewModel. |

## Variant axes

- **Plain `MonoBinder` vs `ComponentMonoBinder`.** Every base ultimately derives from [[Binder Base Classes\|MonoBinder]] (a `MonoBehaviour` managing the [[IBinder]] lifecycle). The `Component`-flavored bases additionally cache a typed `Component` so subclasses operate on a property; the plain `MonoBinder` overloads (e.g. `SwitcherMonoBinder<T>`, `EnumMonoBinder<T>`) target anything, not a component.
- **Property vs Switcher vs Enum vs EnumGroup.** Property bases forward one value straight through. Switcher converts a `bool` into one of two serialized `T` values. Enum resolves a `System.Enum` through a serialized lookup table. EnumGroup fans the enum across many elements, calling `SetSelectedValue`/`SetDefaultValue`.
- **Converter overload.** Each family adds an extra generic parameter `TConverter : IConverter<T, T>` that, when assigned via a `[SerializeReference]` dropdown, transforms the value before it is applied (EnumGroup carries separate default/selected converters).
- **OneWayToSource / reverse.** `ComponentMonoBinder<TComponent, TProperty>` also implements `IReverseBinder<TProperty>`; in [[BindMode]] `OneWayToSource` it raises `ValueChanged` on bind to seed the ViewModel. `ComponentToSourceMonoBinder` is reverse-only — it sends the cached component reference itself back on bind.

## Notable behavior

- Concrete typed bases (`ComponentColorMonoBinder<T>`, `ComponentFloat…`, etc.) just fix the generics and add the matching binder interface (`IColorBinder`, etc.). They appear under `ComponentProperty/`, `Switchers/`, `Enums/`, `EnumGroups/` in `Bool/Color/Float/Int/String/Vector3` flavors.
- The converter type aliases switch between generic `IConverter<,>` (Unity 2023.1+) and legacy typed interfaces via `#if`.
- `[BindModeOverride]` narrows which modes a base accepts; `[BinderLog]` on `SetValue` is profiling instrumentation (see [[Runtime Binding Resolution]]).

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/` — `ComponentProperty/`, `Switchers/`, `Enums/`, `EnumGroups/`, `ComponentToSourceMonoBinder.cs`. Lifecycle root: [[Binder Base Classes]].

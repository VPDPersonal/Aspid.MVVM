---
title: Slider Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/SliderValueBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/Mono/SliderValueMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/Mono/SliderValueEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/SliderValueMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/MinMax/SliderMinMaxBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/MinMax/SliderMinMaxSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/OneWayToSource/Mono/SliderToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Command/SliderCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Extensions/SliderSetters.cs
tags: [binder, starterkit, slider, ui]
updated_at: 2026-05-31
---

# Slider Binders

> StarterKit binders that connect a Unity UI `Slider` to a ViewModel — its `value`, its `min/max` range, or a command fired on drag.

## What it binds

Two distinct `Slider` aspects. **Value** binders read/write `Slider.value`. **MinMax** binders write `Slider.minValue` / `Slider.maxValue` (via the `SliderSetters.SetMinMax` extension, gated by a `SliderValueMode` of `Min`, `Max`, or `Range`). All accept an optional [[Converters|converter]] applied before assignment, and value binders suppress `onValueChanged` re-entrancy while pushing a value.

Value binders implement `INumberBinder` + `INumberReverseBinder`, so a ViewModel `int`/`long`/`float`/`double` flows both ways (out-going ints are cast to float). See [[Bindable Members]].

## Family

| Binder | Base | Target | Notes |
|--------|------|--------|-------|
| `SliderValueBinder` | `TargetBinder<Slider>` | `value` | OneWay / TwoWay / OneWayToSource |
| `SliderValueMonoBinder` | `ComponentMonoBinder<Slider>` | `value` | same, inspector-driven |
| `SliderMinMaxBinder` | `TargetBinder<Slider,Vector2,Converter>` | `min`/`max` | not TwoWay; `SliderValueMode` |
| `SliderMinMaxMonoBinder` | mono | `min`/`max` | inspector-driven |
| `SliderMinMaxSwitcherBinder` / `…MonoBinder` | `SwitcherBinder<…>` | `min`/`max` | bool picks one of two ranges |
| `SliderValueSwitcherMonoBinder` | switcher | `value` | bool picks one of two values |
| `SliderValueEnum/EnumGroupMonoBinder`, `SliderMinMaxEnum/EnumGroupMonoBinder` | enum-float mono | `value` / `min`/`max` | enum maps to per-state value |
| `SliderToSourceMonoBinder` | `ComponentToSourceMonoBinder<Slider>` | `value` | pushes current value to source |
| `SliderCommandBinder` / `SliderCommandBinder<T…>` | `TargetBinder<Slider>` | `onValueChanged` | runs an [[IRelayCommand]] per change |

## Variant axes

- **Plain vs Mono** — the same logic exists as a hand-instantiated [[IBinder]] (`TargetBinder` subclass, `[Serializable]`) and as a `MonoBinder` component (`AddComponentMenu` "Aspid/MVVM/Binders/UI/Slider/…"). See [[Mono Binders]] and [[Binder Base Classes]].
- **Switcher** — a bool ViewModel value selects `trueValue` or `falseValue`.
- **Enum / EnumGroup** — an enum value drives a per-state float; *EnumGroup* applies it to every `Slider` in a group.
- **OneWayToSource** — `SliderToSourceMonoBinder` plus the [[BindMode|OneWayToSource]] mode on value binders: on bind, the slider's current value is immediately forwarded to the ViewModel, then on every drag.

## Notable behavior

`SliderCommandBinder` accepts a command typed by `int`/`long`/`float`/`double` (plus 1-3 extra `Param` args in the generic overloads) and dispatches to whichever is non-null, float first. Its `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) reflects `CanExecute` onto the slider — handy for [[Relay Commands]]. Value binders' OneWayToSource immediately syncs the source on bind; this likely seeds the ViewModel with the inspector default.

## Source

`StarterKit/Unity/Runtime/Binders/Sliders/`. Related: [[Scrollbar Binders]], [[Toggle Binders]], [[Binders Catalog]], [[Data Binding]].

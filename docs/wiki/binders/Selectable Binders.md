---
title: Selectable Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/SelectableInteractableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/Mono/SelectableInteractableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/Mono/SelectableInteractableEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/SelectableColorBlockBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/SelectableColorBlockSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/Mono/SelectableColorBlockMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/Mono/SelectableColorBlockEnumMonoBinder.cs
tags: [binder, starterkit, ugui, selectable]
updated_at: 2026-05-31
---

# Selectable Binders

> Drive a UGUI `Selectable` (the base of Button, Toggle, Slider, Dropdown, InputField) from a ViewModel — enable/disable interaction or swap its color states.

## What it binds

`UnityEngine.UI.Selectable` exposes two bindable surfaces, and the family splits along them:

- **Interactable** — the `Selectable.interactable` bool. Use it to gate a control on a ViewModel flag (e.g. a "Submit" button enabled only when a form is valid).
- **ColorBlock** — the `Selectable.colors` struct (normal/highlighted/pressed/disabled tints). Bind it to restyle a control reactively.

Because every interactive UGUI widget *is* a `Selectable`, these binders work uniformly across all of them, complementing the type-specific [[Button Binders]], [[Toggle Binders]], [[Slider Binders]], [[Dropdown Binders]] and [[InputField Binders]].

## Family table

| Binder | Property | Base | Value |
|---|---|---|---|
| `SelectableInteractableBinder` | `interactable` | `TargetBoolBinder<Selectable>` | bool |
| `SelectableInteractableMonoBinder` | `interactable` | `ComponentBoolMonoBinder<Selectable>` | bool |
| `SelectableInteractableEnumMonoBinder` | `interactable` | `EnumMonoBinder<…>` | enum→bool |
| `SelectableInteractableEnumGroupMonoBinder` | `interactable` | `EnumGroupMonoBinder<Selectable, bool>` | enum→group |
| `SelectableColorBlockBinder` | `colors` | `TargetBinder<…>` | ColorBlock |
| `SelectableColorBlockMonoBinder` | `colors` | `ComponentMonoBinder<…>` | ColorBlock |
| `SelectableColorBlockSwitcherBinder` | `colors` | `SwitcherBinder<…>` | bool→pair |
| `SelectableColorBlockEnumMonoBinder` / `…EnumGroupMonoBinder` | `colors` | `EnumMonoBinder` / `EnumGroupMonoBinder` | enum→value |

## Variant axes

- **Plain vs Mono** — Plain `…Binder` types are `[Serializable]` C# objects constructed in code/inspector against a passed `Selectable`. The `…MonoBinder` variants are MonoBehaviours that cache their own component (`CachedComponent`) and ship `[AddComponentMenu]` / `[AddBinderContextMenu]` entries for drag-and-drop wiring. See [[Mono Binders]] and [[Binder Base Classes]].
- **Switcher** (ColorBlock only) — `SwitcherBinder` toggles `colors` between a `trueValue` and `falseValue` based on a bound bool. It is guarded behind `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`.
- **Enum / EnumGroup** — Enum variants resolve a property value from the bound enum; EnumGroup applies the resolved value across a *group* of targets (e.g. exclusive-selection styling). Both are Mono-only here.
- **OneWayToSource** — the Mono variants document support for [[BindMode|OneWayToSource]]: on bind they push the control's current value back to the ViewModel. Interactable also supports an `isInvert` flag.

## Notable behavior

- Both plain base binders call `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — **TwoWay is rejected**, since a `Selectable` raises no change event to read back continuously.
- Defaults differ by intent: Interactable defaults to `BindMode.OneTime`, ColorBlock to `BindMode.OneWay`.
- The ColorBlock converter alias swaps between a generic `IConverter<ColorBlock, ColorBlock>` (2023.1+) and `IConverterColorBlock` on older Unity — a [[Converters]] indirection, not a behavioral change.

## Source

`StarterKit/Unity/Runtime/Binders/Selectables/` (`Interactable/`, `ColorBlock/`, plus their `Mono/` subfolders). Bound members on the ViewModel side come from [[Bindable Members]] via [[Source Generation Pipeline|generated]] code. See also [[IBinder]] and the [[Binders Catalog]].

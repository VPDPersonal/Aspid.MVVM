---
title: Dropdown Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/DropdownValueBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/DropdownValueSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/Mono/DropdownValueEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/Mono/DropdownValueEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Options/DropdownOptionsBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Command/DropdownCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/AlphaFadeSpeed/DropdownAlphaFadeSpeedBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/OneWayToSource/Mono/DropdownToSourceMonoBinder.cs
tags:
  - binders
  - starterkit
  - dropdown
  - tmp
updated_at: 2026-05-31
---

# Dropdown Binders

> StarterKit binders that connect a ViewModel to a `TMP_Dropdown`: selected index, the option list, fade speed, and selection-change commands.

Every binder in this family targets TextMeshPro's `TMP_Dropdown`, so the whole folder is gated behind `#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`. Use these instead of writing custom code when a dropdown's selection or content should be driven by a [[Bindable Members|bindable member]].

## Family

| Folder | Target property | Bound type | Built on |
|--------|-----------------|-----------|----------|
| `Value/` | `TMP_Dropdown.value` (selected index) | `int` | `TargetIntBinder<TMP_Dropdown>` |
| `Options/` | `TMP_Dropdown.options` | `List<string>` / `List<Sprite>` / `OptionData` | `TargetBinder<TMP_Dropdown>` |
| `AlphaFadeSpeed/` | `TMP_Dropdown.alphaFadeSpeed` | `float` (clamped ≥ 0) | `TargetFloatBinder<TMP_Dropdown>` |
| `Command/` | reacts to `onValueChanged` | `IRelayCommand<int/long[,...]>` | `TargetBinder<TMP_Dropdown>` |

## Variant axes

The same property group is regenerated across orthogonal axes:

- **Plain vs Mono** — Plain binders (e.g. `DropdownValueBinder`) are `[Serializable]` classes embedded in a [[View]]; Mono variants (e.g. `DropdownValueMonoBinder`) are MonoBehaviour components with `[AddComponentMenu]` / `[AddBinderContextMenu]` entries under *Aspid/MVVM/Binders/UI/Dropdown*. See [[Mono Binders]].
- **Switcher** — `…SwitcherBinder` toggles `value` between two preset `int`s from a bound `bool` (built on `SwitcherIntBinder`).
- **Enum / EnumGroup** — Mono-only. `…EnumMonoBinder` (built on `EnumIntMonoBinder`) maps a bound enum to one dropdown's index; `…EnumGroupMonoBinder` (built on `EnumGroupIntMonoBinder`) drives several dropdowns at once.
- **OneWayToSource** — `DropdownToSourceMonoBinder` is a `ComponentToSourceMonoBinder<TMP_Dropdown>` that pushes the current dropdown state back to the ViewModel on bind.

## Notable behavior

- Most value binders reject [[BindMode|TwoWay]] (they call `ThrowExceptionIfMatches(BindMode.TwoWay)`); `DropdownOptionsBinder` declares `[BindModeOverride(OneWay, OneTime, OneWayToSource)]`. In `OneWayToSource`, `DropdownOptionsBinder.OnBound` raises `ValueChanged` with the current `options`, propagating them to the source.
- `DropdownOptionsBinder` implements several [[IBinder]] interfaces, so the same field accepts string labels, sprites, or `OptionData`; it clears existing options before applying.
- `DropdownCommandBinder` (with 0–3 extra `Param` generics) executes a bound [[IRelayCommand]] on every selection change, passing `Target.value`. Its `InteractableMode` (Interactable / Visible / Custom / None) reflects `CanExecute` onto the dropdown's interactable state, and it accepts either `int` or `long` command argument typings.
- `DropdownAlphaFadeSpeedBinder` clamps its converted value to `Mathf.Max(value, 0)`.

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/`. Base classes live in [[Binder Base Classes]]; the overall catalog is [[Binders Catalog]]. Closely related families: [[Toggle Binders]], [[Slider Binders]], [[InputField Binders]].

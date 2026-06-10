---
title: Toggle Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/IsOn/ToggleIsOnBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/IsOn/Mono/ToggleIsOnMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/Command/ToggleCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/OneWayToSource/Mono/ToggleToSourceMonoBinder.cs
tags: [binder, starterkit, ui, toggle, command]
updated_at: 2026-05-31
---

# Toggle Binders

> StarterKit binders that connect a Unity UI `Toggle` to ViewModel booleans and commands, targeting `Toggle.isOn` and `Toggle.onValueChanged`.

## Family

| Variant | Bound type | Default mode | Target |
|---|---|---|---|
| `ToggleIsOnBinder` / `ToggleIsOnMonoBinder` | `bool` | `TwoWay` | `Toggle.isOn` |
| `ToggleCommandBinder` (+ generic arities) / `ToggleCommandMonoBinder` | `IRelayCommand`, `IRelayCommand<bool[, T...]>` | `OneWay` | `onValueChanged` -> `Execute` |
| `ToggleToSourceMonoBinder` | (the toggle's serialized property) | one-way-to-source | `Toggle.isOn` -> source |

## Variant axes

**Plain vs Mono.** Every binder exists as a serializable plain class deriving from [[Binder Base Classes|TargetBinder<Toggle>]] (constructed in code) and, for IsOn/Command, a `partial` MonoBehaviour deriving from `ComponentMonoBinder<Toggle>` for inspector wiring. Mono variants carry `[AddComponentMenu]` / `[AddBinderContextMenu]` and are the `partial` half consumed by [[Mono Binders]]; the other half is generated. OneWayToSource ships **only** as a Mono binder (`ComponentToSourceMonoBinder<Toggle>`).

**IsOn.** Two-way by default: implements `IBinder<bool>` and `IReverseBinder<bool>`, so the toggle both reflects and reports the bound value. An `_isInvert` flag flips the logical value in each direction. A `_isNotifyValueChanged` guard suppresses the echo when [[Data Binding|SetValue]] writes `isOn`, preventing a feedback loop back to the source.

**Command.** Subscribes to `onValueChanged` and runs the bound [[Relay Commands|command]] on each change. Overloads accept `IRelayCommand` (parameterless) or `IRelayCommand<bool>` (receives `isOn`); generic `ToggleCommandBinder<T>`, `<T1,T2>`, `<T1,T2,T3>` forward extra serialized `Param` values. An `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) reflects `CanExecute` onto the toggle; `Custom` drives an `ICanExecuteView`.

## Notable behavior

- **Mode constraints (inferred from guards).** IsOn rejects [[BindMode|None]]. Command rejects `TwoWay` (`ThrowExceptionIfTwo`). `[BindModeOverride(IsAll = true)]` on IsOn lets the inspector pick any mode.
- **OneWayToSource priming.** When IsOn binds in `OneWayToSource`, `OnBound` immediately pushes the current `isOn` to the source.
- **Cleanup.** `OnUnbound` removes the `onValueChanged` listener; Command also nulls each command reference, detaching `CanExecuteChanged`.

> Note: this folder contains only IsOn, Command, and OneWayToSource — there are no Switcher or Enum/EnumGroup toggle variants here.

## Source

`StarterKit/Unity/Runtime/Binders/Toggles/` — `IsOn/`, `Command/`, `OneWayToSource/` (each with a `Mono/` subfolder). See [[Binders Catalog]], [[IBinder]], [[IRelayCommand]].

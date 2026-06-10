---
title: Scrollrect Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/Command/ScrollRectCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/Command/Mono/ScrollRectCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/OneWayToSource/Mono/ScrollRectToSourceMonoBinder.cs
tags: [binder, starterkit, ui, scrollrect, command]
updated_at: 2026-05-31
---

# Scrollrect Binders

> Binds Unity's `ScrollRect` to a ViewModel: fires a command on every scroll, or hands the live `ScrollRect` reference back to the ViewModel.

## Why it exists

Unlike value binders (Slider, Toggle) that push a number or bool, a `ScrollRect` has no single bindable "value" worth observing one-way. What you usually want is to *react* to scrolling. So this family is **command-centric**: it forwards the live scroll position to an [[IRelayCommand]] on `onValueChanged`. There are no `Switcher`, `Enum`, or `EnumGroup` variants here (those axes only make sense for binders that *set* discrete state) — making this one of the smaller binder families.

## Family

| Binder | Bind type | Direction |
|--------|-----------|-----------|
| `ScrollRectCommandBinder` (+ `<T>`, `<T1,T2>`, `<T1,T2,T3>`) | `IRelayCommand<Vector2/Vector3 [,T...]>` | View to ViewModel (command) |
| `ScrollRectCommandMonoBinder` (+ generic abstract variants) | same | View to ViewModel (command) |
| `ScrollRectToSourceMonoBinder` | `ScrollRect` | OneWayToSource |

## Variant axes

- **Plain vs Mono** — `ScrollRectCommandBinder` is `[Serializable]`, deriving from [[Binder Base Classes|TargetBinder<ScrollRect>]]; it is constructed in code and nested inside a host [[View]]. `ScrollRectCommandMonoBinder` is a `MonoBehaviour` (`ComponentMonoBinder<ScrollRect>`, see [[Mono Binders]]) dropped on the same GameObject as the `ScrollRect`, exposed via `[AddComponentMenu]`. Both share identical command logic.
- **Arity overloads** — `<T>`, `<T1,T2>`, `<T1,T2,T3>` add serialized `Param` fields forwarded after the position. Each accepts either a `Vector2` or `Vector3` command; the bound one wins, position cast as needed. Mono generic arities are `abstract` — you subclass to fix concrete `T`s (a Unity serialization constraint).
- **OneWayToSource** — `ScrollRectToSourceMonoBinder` is the lone non-command variant: a sealed `ComponentToSourceMonoBinder<ScrollRect>` that pushes the `ScrollRect` reference itself into the ViewModel ([[BindMode]] OneWayToSource), letting business logic drive scrolling.

## Notable behavior

- On bind, subscribes to `ScrollRect.onValueChanged`; on unbind, removes the listener and detaches the command (calls `SetValue(null)`).
- The command argument is `ScrollRect.normalizedPosition` (0..1 per axis), passed as `Vector2` or cast to `Vector3`.
- `BindMode.TwoWay` is rejected at construction (`mode.ThrowExceptionIfTwo()`) — scroll commands are inherently one-directional.
- An optional `ICanExecuteView _interactable` reflects the command's `CanExecuteChanged` so UI can disable when scrolling is not allowed.

## Source

`StarterKit/Unity/Runtime/Binders/Scrollrects/`. See [[Binders Catalog]], [[Relay Commands]], [[Scrollbar Binders]].

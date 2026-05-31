---
title: Scrollbar Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/Command/ScrollbarCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/Command/Mono/ScrollBarCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/OneWayToSource/Mono/ScrollbarsToSourceMonoBinder.cs
tags: [binders, starterkit, scrollbar, command, ugui]
updated_at: 2026-05-31
---

# Scrollbar Binders

> StarterKit binders that wire a uGUI `Scrollbar` to a ViewModel: fire an [[IRelayCommand]] on each drag, or hand the component itself to the ViewModel.

## What it binds

The family targets `UnityEngine.UI.Scrollbar`. Unlike most StarterKit families, the Scrollbar set has **no value-setter variant** (no binder that pushes a `float` into the scrollbar). Instead it is **command-first**: every binder listens to `Scrollbar.onValueChanged` and executes a bound command, passing the current `Scrollbar.value`. So a Scrollbar here is treated as an input gesture, not a displayed value.

## Family table

| Variant | Class | Base | Binds | Direction |
|---|---|---|---|---|
| Command (Plain) | `ScrollbarCommandBinder` (+ `<T>`, `<T1,T2>`, `<T1,T2,T3>`) | `TargetBinder<Scrollbar>` | `IRelayCommand<...>` | value -> command |
| Command (Mono) | `ScrollBarCommandMonoBinder` (+ generic arities, abstract) | `ComponentMonoBinder<Scrollbar>` | `IRelayCommand<...>` | value -> command |
| OneWayToSource (Mono) | `ScrollbarsToSourceMonoBinder` | `ComponentToSourceMonoBinder<Scrollbar>` | `Scrollbar` | binder -> ViewModel |

## Variant axes

- **Plain vs Mono** — Plain (`ScrollbarCommandBinder`) is `[Serializable]`, constructed in code with an explicit `Scrollbar target`. Mono is a `MonoBehaviour` placed in the scene (`[AddComponentMenu]`, `[AddBinderContextMenu]`), resolving its target via `CachedComponent`. See [[Mono Binders]] and [[Binder Base Classes]].
- **Command arity** — the generic forms `<T>`, `<T1,T2>`, `<T1,T2,T3>` forward 1-3 extra serialized `Param` values alongside the scrollbar value. The Mono generics are `abstract` (a closed subclass is required to serialize concrete `T`s); the Plain generics are concrete.
- **Numeric overload** — each binder implements four `IBinder<IRelayCommand<...>>` interfaces (`int`, `long`, `float`, `double`); whichever command was bound receives the value cast to its type. `float` wins if multiple are set.
- **OneWayToSource** — `ScrollbarsToSourceMonoBinder` passes the component reference to the ViewModel (see [[BindMode]] `OneWayToSource`).
- **Switcher / Enum / EnumGroup** — none exist for Scrollbar (no such files in this folder); those axes apply to display-oriented families like [[Toggle Binders]] / [[Image Binders]].

## Notable behavior

- **CanExecute -> interactability.** An `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) reflects the command's `CanExecute` result by toggling `Target.interactable`, `gameObject.SetActive`, or an `ICanExecuteView`. The `Custom` mode requires the `ICanExecuteView` constructor overload.
- **TwoWay is rejected.** Plain constructors call `mode.ThrowExceptionIfTwo()`.
- **Clean teardown.** `OnUnbound` removes the `onValueChanged` listener and nulls every command, unsubscribing each `CanExecuteChanged`.

These are hand-written binders. The `[RelayCommand]`-generated `IRelayCommand` they consume comes from the ViewModel side — see [[Relay Commands]] and [[ViewModel to Generated Code]].

## Source

`StarterKit/Unity/Runtime/Binders/Scrollbars/` — see `source_paths`. Related: [[Slider Binders]], [[Scrollrect Binders]], [[Button Binders]], [[Binders Catalog]].

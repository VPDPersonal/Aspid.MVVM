---
title: Animator Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetParameterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetBoolBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetTriggerBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/Mono/AnimatorSetBoolMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/Mono/AnimatorToSourceMonoBinder.cs
tags:
  - binder
  - animator
  - starterkit
updated_at: 2026-05-31
---

# Animator Binders

> Drive a Unity `Animator`'s parameters (bool/float/int) and triggers from ViewModel state, or hand the `Animator` reference back to the ViewModel.

## Why it exists

A common UI need is to push ViewModel state into an `Animator` — toggle a "isOpen" bool, set a "speed" float, or fire a "play" trigger — without writing glue code in a MonoBehaviour. This family turns those `Animator.SetBool/SetFloat/SetInteger/SetTrigger` calls into declarative [[Bindable Members|bindings]].

## Family

| Binder | Animator call | Notes |
|---|---|---|
| `AnimatorSetBoolBinder` | `SetBool` | optional `_isInvert` |
| `AnimatorSetFloatBinder` | `SetFloat` | |
| `AnimatorSetIntBinder` | `SetInteger` | |
| `AnimatorSetParameterBinder<T>` | (abstract base) | shared logic for the three above |
| `AnimatorSetTriggerBinder` | `SetTrigger` | `OneWayToSource` only |
| `AnimatorToSourceMonoBinder` | — | sends the `Animator` to the ViewModel |

## How it works

`AnimatorSetParameterBinder<T>` is the shared base, a [[Binder Base Classes|TargetBinder]]`<Animator>` carrying the serialized `ParameterName`. Concrete subclasses override `SetParameter(T)` to make the actual `Animator` call (the bool variant first compares against `GetBool` to skip redundant sets). `CanExecute` defaults to `Target.gameObject.activeInHierarchy`, gating every set.

`AnimatorSetTriggerBinder` does not derive from the parameter base — triggers carry no value, so it is a plain `TargetBinder<Animator>` exposing a parameterless `SetTrigger`.

## Variant axes

- **Plain vs Mono.** Plain binders (`AnimatorSetBoolBinder`) are `[Serializable]` fields hosted inside a view; `Mono` siblings (`AnimatorSetBoolMonoBinder`) are MonoBehaviours with `[AddComponentMenu]`/`[AddBinderContextMenu]` and read the component via `CachedComponent` instead of a ctor-supplied `Animator`. See [[Mono Binders]].
- **OneWayToSource.** Via `[BindModeOverride]` the parameter base allows `OneWay`, `OneTime`, and `OneWayToSource`; `TwoWay` is rejected at construction. In `OneWayToSource` the binder hands `SetParameter` (or `SetTrigger`) to the ViewModel as either a plain `Action<T>`/`Action` or an [[IRelayCommand]]`<T>` whose `CanExecute` mirrors the binder's gate (an [[Data Binding|IReverseBinder]]). The trigger binder is `OneWayToSource`-only.
- **ToSource component.** `AnimatorToSourceMonoBinder` is a `ComponentToSourceMonoBinder<Animator>` — it pushes the `Animator` reference itself, not a parameter.

Inference: there is no Switcher or Enum/EnumGroup variant in this family (unlike some other binder categories); only the four parameter kinds plus the source binder exist.

## Source

`StarterKit/Unity/Runtime/Binders/Animators/` (plain) and its `Mono/` subfolder. Part of the [[Binders Catalog]]; see [[BindMode]] for mode semantics.

---
title: Behaviour Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/BehaviourEnabledBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledByBindMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/OneWayToSource/Mono/BehaviourToSourceMonoBinder.cs
tags: [binder, starterkit, unity, behaviour, bool, enum]
updated_at: 2026-05-31
---

# Behaviour Binders

> StarterKit binders that toggle any Unity `Behaviour.enabled` flag from a ViewModel value — the simplest way to switch a script, collider, or component on/off through MVVM.

## What it binds

Every variant ultimately drives `Behaviour.enabled` (a `bool`). Because `Behaviour` is the base of nearly all Unity components (scripts, `Collider`, `Renderer` subtypes, etc.), this family is the generic on/off switch. One reverse variant instead pushes the `Behaviour` reference *back* into the ViewModel. The target property is always:

```csharp
get => Target.enabled;
set => Target.enabled = value;
```

## Family table

| Binder | Base class | Direction / value |
|--------|-----------|-------------------|
| `BehaviourEnabledBinder` | `TargetBoolBinder<Behaviour>` | bool in (Plain, serializable, no MonoBehaviour) |
| `BehaviourEnabledMonoBinder` | `ComponentBoolMonoBinder<Behaviour>` | bool in (component-based) |
| `BehaviourEnabledByBindMonoBinder` | [[Mono Binders\|MonoBinder]], `IAnyBinder` | OneTime; enables self by *binding presence* |
| `BehaviourEnabledEnumMonoBinder` | `EnumMonoBinder<Behaviour, bool>` | enum → bool |
| `BehaviourEnabledEnumGroupMonoBinder` | `EnumGroupMonoBinder<Behaviour, bool>` | enum → bool over a group |
| `BehaviourToSourceMonoBinder` | `ComponentToSourceMonoBinder<Behaviour>` | OneWayToSource (sends component out) |

## Variant axes

- **Plain vs Mono** — `BehaviourEnabledBinder` is a `[Serializable]` plain binder you embed as a field; the `MonoBinder` form is a drop-on component (`AddComponentMenu` / `AddBinderContextMenu`). See [[Binder Base Classes]] and [[Mono Binders]].
- **Bool invert** — both bool forms accept `isInvert`, flipping the value before applying it.
- **EnabledByBind** — does *not* forward a value; it sets its own `enabled` to whether a matching binding exists on the ViewModel (`IsBound`), optionally inverted. `SetValue<T>` is a no-op. Useful to show/hide a node when the ViewModel actually exposes the member.
- **Enum / EnumGroup** — resolve a `bool` from a bound enum value (see [[BindMode]] / converter wiring); the *Group* form applies it to each `Behaviour` in a serialized list.
- **OneWayToSource** — `BehaviourToSourceMonoBinder` reverses flow, writing the cached component into the ViewModel.

No "Switcher" variant exists in this folder (unlike some other families); the closest analogue is EnabledByBind.

## Notable behavior

- `BehaviourEnabledBinder` and `BehaviourEnabledMonoBinder` reject `BindMode.TwoWay` (the binder throws / overrides modes); the Mono form additionally supports `OneWayToSource`, sending the current `enabled` value back on bind.
- `BehaviourEnabledByBindMonoBinder` is `[BindModeOverride(BindMode.OneTime)]` and re-evaluates on `OnEnable`, `OnBound`, and `OnUnbound`.

## Source

`StarterKit/Unity/Runtime/Binders/Behaviours/` — `Enabled/` (bool, enum, enum-group, by-bind) and `OneWayToSource/`. Base classes documented in [[Binder Base Classes]]; binding flow in [[Data Binding]] and [[Runtime Binding Resolution]]. Browse all families via [[Binders Catalog]].

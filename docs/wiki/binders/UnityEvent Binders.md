---
title: UnityEvent Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/UnityEvents/Mono
tags: [binder, starterkit, unityevent, bridge]
updated_at: 2026-05-31
---

# UnityEvent Binders

> The escape hatch: relay any bound ViewModel value into Unity's serialized `UnityEvent` so designers can wire up effects in the Inspector without writing a custom binder.

## Why it exists

Most binders target a concrete component (Text, Slider, Animator). When no dedicated binder fits — or a designer just wants to trigger arbitrary listeners (play a sound, fade a panel, fire an animation) on a value change — these binders forward the bound value into a serialized `UnityEvent`. They turn data binding into the familiar Inspector-driven event wiring Unity developers already know.

## How it works

Each variant is a [[Mono Binders|MonoBinder]] exposing a serialized `UnityEvent<T>` field (`_set`) plus an optional `[SerializeReference]` [[Converters|converter]]. When the bound value resolves, `SetValue` applies the converter (if assigned) and invokes the event: `_set?.Invoke(_converter?.Convert(value) ?? value)`. The string variant additionally takes a `CultureInfoMode` to format numbers. Because these are MonoBinders, they participate in normal [[View Initialization]] and bind in the [[BindMode]] their target member declares — practically [[Data Binding|OneWay]].

## Family

| Variant | Target / event | Notable behavior |
|---------|----------------|------------------|
| Value binders (Bool, Int, Long, Float, Double, String, Color, Vector2, Vector3, Quaternion) | `UnityEvent<T>` | Invoke with converted value; numeric→string via culture |
| Enum | `UnityEvent` (no arg) | Extends `EnumMonoBinder<UnityEvent>`; invokes the event mapped to the bound enum member |
| Switcher | `UnityEvent` (no arg) | Extends `SwitcherMonoBinder<UnityEvent>`; picks one of two events from a bound `bool` |
| Number Condition | `UnityEvent<bool>` | Converts numeric value to `bool`, invokes event with the result |
| Number Condition Switcher | two `UnityEvent` | Converts numeric→`bool`, fires `_trueSet` or `_falseSet` |
| Bool By Bind | `UnityEvent<bool>` | Ignores the value; invokes on bind/unbind with `IsBound` (optionally inverted) |

## Variant axes

- **Plain vs Mono** — only MonoBinder variants exist here; there is no plain (non-component) flavor.
- **Switcher / Enum / EnumGroup** — Switcher (bool→one of two no-arg events) and Enum (enum→mapped event) reuse the shared [[Binder Base Classes]] `SwitcherMonoBinder<T>` / `EnumMonoBinder<T>`. No EnumGroup variant appears in this folder.
- **Condition** — NumberCondition family folds a numeric input through a float→bool converter, either emitting the `bool` or branching between two events.
- **OneWayToSource** — none present. The family is push-only (ViewModel → UnityEvent); it never writes back. `BoolByBind` is the closest to a lifecycle hook, firing purely on bind state.

The whole family lives under `Add General Binder/UnityEvent/...` in the editor menus (via `AddComponentMenu` / `AddBinderContextMenu`), and value variants advertise their bindable type with `AddBinderContextMenuByType`.

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/UnityEvents/Mono/` — see also [[Binder Base Classes]], [[Mono Binders]], [[Converters]], [[Binders Catalog]].

---
title: Object Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/ObjectNameBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/Mono/ObjectNameMonoBinder.cs
tags: [binder, starterkit, unity-object, name]
updated_at: 2026-05-31
---

# Object Binders

> Binders that drive `UnityEngine.Object.name` from a ViewModel `string` — useful for debugging, hierarchy labels, and inspector-friendly object naming.

This family targets the single property every `UnityEngine.Object` shares: `name`. It is the smallest member-level binder group in the StarterKit — there is only one bindable target, so the family exists mainly to demonstrate the Plain/Mono split and `OneWayToSource` round-tripping rather than to cover many properties.

## Family table

| Variant | Base class | Target | Direction |
|---------|-----------|--------|-----------|
| `ObjectNameBinder` | [[Binder Base Classes\|TargetBinder<Object>]] | `Object.name` | `IBinder<string>` + `IReverseBinder<string>` |
| `ObjectNameMonoBinder` | [[Mono Binders\|MonoBinder]] | `Object.name` | `IBinder<string>` + `IReverseBinder<string>` |

## Variant axes

- **Plain vs Mono** — `ObjectNameBinder` is `[Serializable]`, lives inside a [[View]] as a serialized reference, and takes its target via constructor. `ObjectNameMonoBinder` is a `MonoBehaviour` component (`partial`, so the generator emits its binding plumbing) with a serialized `Object _object`; its `OnValidate` defaults the target to the host `gameObject`.
- **Switcher / Enum / EnumGroup** — none exist here. Those axes apply to richer families (e.g. [[GameObject Binders]]); `name` is a plain string, so there is nothing to switch or enumerate.
- **OneWayToSource** — both variants implement `IReverseBinder<string>` and declare `[BindModeOverride(OneWay, OneTime, OneWayToSource)]`. `TwoWay` is rejected at construction (`ThrowExceptionIfMatches`). In `OneWayToSource`, `OnBound` pushes the current `name` back to the ViewModel once binding is established — the inspector value seeds the model.

## Notable behavior

- An optional `IConverter<string,string>` (the [[Converters|converter]] interface) can transform the value before it is applied or sent back. `GetConvertedValue` coalesces to `string.Empty`, so a null value never throws.
- The two variants differ in converter nullability typing: under `UNITY_2023_1_OR_NEWER` the Plain binder uses `IConverter<string?,string?>` while the Mono binder uses `IConverter<string,string>` — a minor inconsistency, likely incidental.
- The Mono binder applies `[BinderLog]` to `SetValue`; this attribute appears to drive generated debug logging (see [[Debug Binders]]).
- Both bind to `string`, so a `[[Bindable Members|Bind]]`-generated `string` member on a `[[ViewModel]]` is the natural source. See [[BindMode]] for the mode semantics.

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/` — `ObjectNameBinder.cs` and `Mono/ObjectNameMonoBinder.cs`. See [[Binders Catalog]] for the full binder index and [[Data Binding]] for the underlying contract.

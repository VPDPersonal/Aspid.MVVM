---
title: Transform Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/TransformPositionBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/TransformPositionSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/Mono/TransformPositionEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/OneWayToSource/Mono/TransformToSourceMonoBinder.cs
tags: [binders, starterkit, transform, rect-transform]
updated_at: 2026-05-31
---

# Transform Binders

> StarterKit binders that drive a `Transform` / `RectTransform` spatial property — position, rotation, scale, anchored position, size — from ViewModel values.

## What it binds

These binders bind a single `Transform` (or `RectTransform`) component to a bound member. Most properties are `Vector3`/`Quaternion`/`Vector2`-valued, so the family builds on the generic targets [[Binder Base Classes|TargetVector3Binder<Transform>]], `SwitcherVector3Binder<Transform>`, and friends. Each binder writes the value through `Transform` extension helpers (e.g. `SetPosition(value, space)`), so a `Space` field selects world vs. local coordinates where applicable.

## Family table

| Target property | Source folder |
|---|---|
| `position` / `localPosition` | `Transforms/Position` |
| `eulerAngles` | `Transforms/EulerAngels` |
| `rotation` | `Transforms/Rotation` |
| `localScale` | `Transforms/Scale` |
| `Transform` reference → source | `Transforms/OneWayToSource` |
| `anchoredPosition` | `RectTransforms/AnchoredPosition` |
| `sizeDelta` | `RectTransforms/SizeDelta` |

## Variant axes

Every property exists across the standard StarterKit variant matrix (see [[Mono Binders]] for the Plain/Mono split):

- **Plain** — `[Serializable]` class (e.g. `TransformPositionBinder`) used as a serialized field inside a [[View]]. Takes target + converter + [[BindMode]] in its constructor.
- **Mono** — `MonoBinder` component you drop on a GameObject (e.g. `TransformPositionMonoBinder`), discoverable via `[AddComponentMenu]` / `[AddBinderContextMenu]`.
- **Switcher** — picks between a `trueValue` and `falseValue` from a bound `bool` (`TransformPositionSwitcherBinder`, `…SwitcherMonoBinder`).
- **Enum / EnumGroup** — Mono-only; maps a bound enum to per-value targets. `EnumGroup` applies the value to every element in a configured group via an overridden `SetValue` (`TransformPositionEnumGroupMonoBinder`).
- **OneWayToSource** — `TransformToSourceMonoBinder` extends `ComponentToSourceMonoBinder<Transform>`; pushes the cached `Transform` reference *to* the ViewModel when binding is established, rather than reading from it.

## Notable behavior

- Write-only binders forbid two-way flow: `TransformPositionBinder` calls `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` in its constructor (inferred to hold across the position/rotation/scale variants). Use the OneWayToSource binder to flow data the other direction.
- `Space` defaults differ — `Space.World` for the Plain position binder, also `World` in EnumGroup; check the serialized field per variant.
- `RectTransform/SizeDelta` adds a `SizeDeltaMode` enum to control which axes of `sizeDelta` are written (inferred from the folder's `SizeDeltaMode.cs`).

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/` — `Transforms/` (Position, EulerAngels, Rotation, Scale, OneWayToSource) and `RectTransforms/` (AnchoredPosition, SizeDelta). See [[Binders Catalog]] and [[Data Binding]].

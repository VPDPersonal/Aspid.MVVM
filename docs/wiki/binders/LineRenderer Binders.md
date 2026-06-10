---
title: LineRenderer Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/LineRendererColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/LineRendererColorSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorSwitcherMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/OneWayToSource/Mono/LineRendererToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/LineRendererColorMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Extensions/LineRendererColorSetter.cs
tags: [binders, starterkit, linerenderer, color]
updated_at: 2026-05-31
---

# LineRenderer Binders

> Drive a Unity `LineRenderer`'s `startColor`/`endColor` from a ViewModel `Color` member — handy for highlighting trajectories, links, or laser/beam visuals that change with state.

## Why it exists

`LineRenderer` exposes its tint as two separate properties (`startColor`, `endColor`) rather than a single material color. This family wraps both behind one `Color` binding target so a ViewModel can recolor a line without knowing about Unity's two-endpoint API. Which endpoint(s) move is selected by a serialized `LineRendererColorMode` (`Start`, `End`, `StartAndEnd`).

## Family

| Variant | Base class | Binds |
|---|---|---|
| `LineRendererColorBinder` | `TargetColorBinder<LineRenderer>` | `Color` → endpoint(s) |
| `LineRendererColorMonoBinder` | `ComponentColorMonoBinder<LineRenderer>` | `Color` → endpoint(s) |
| `LineRendererColorSwitcherBinder` | `SwitcherColorBinder<LineRenderer>` | `bool` → true/false `Color` |
| `LineRendererColorSwitcherMonoBinder` | `SwitcherColorBinder` (Mono) | `bool` → true/false `Color` |
| `LineRendererColorEnumMonoBinder` | `EnumColorMonoBinder<LineRenderer>` | `enum` → per-value `Color` |
| `LineRendererColorEnumGroupMonoBinder` | `EnumGroupColorMonoBinder<LineRenderer>` | `enum` → grouped `Color`s |
| `LineRendererToSourceMonoBinder` | `ComponentToSourceMonoBinder<LineRenderer>` | component ref → ViewModel |

## Variant axes

- **Plain vs Mono** — Plain binders (`[Serializable]`, no `[AddComponentMenu]`) live as serialized fields on a custom [[View]]; Mono variants are standalone `MonoBehaviour` components (see [[Mono Binders]]) you drop on a GameObject and wire in the Inspector via `[AddComponentMenu]`/`[AddBinderContextMenu]`.
- **Switcher** — maps a bound `bool` to one of two configured colors (`trueValue`/`falseValue`) instead of passing a `Color` through directly.
- **Enum / EnumGroup** — `Enum` picks a color per enum case for one renderer; `EnumGroup` applies the resolved color across each element of a group.
- **OneWayToSource** — `LineRendererToSourceMonoBinder` pushes the cached `LineRenderer` reference back to the ViewModel when binding is established (see [[BindMode]] `OneWayToSource`). The plain color binder forbids `BindMode.TwoWay`; `LineRendererColorMonoBinder` additionally supports reading the current color back ([[BindMode]] `OneWayToSource`).

## Notable behavior

- The shared read/write logic lives in `LineRendererSetters.SetColor`/`GetColor` extensions, keyed by `LineRendererColorMode`.
- `GetColor` throws `ArgumentOutOfRangeException` for `StartAndEnd` (it appears ambiguous which endpoint to read). So any read-back path (`Property` getter, `OneWayToSource`) must use `Start` or `End`; using `StartAndEnd` only makes sense for write-only one-way bindings.
- All variants resolve at runtime through [[Runtime Binding Resolution]] like the rest of the [[Binders Catalog]].

## Source

`StarterKit/Unity/Runtime/Binders/LineRenderers/` — `Color/` (plain + `Mono/`), `OneWayToSource/Mono/`, `LineRendererColorMode.cs`, `Extensions/`. Compare with the material-color family in [[Renderer Binders]] and the general [[Graphic Binders]]. See [[Binder Base Classes]] for `TargetColorBinder`/`SwitcherColorBinder`/`EnumColorMonoBinder`.

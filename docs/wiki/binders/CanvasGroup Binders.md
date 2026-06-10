---
title: CanvasGroup Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/CanvasGroupAlphaBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/CanvasGroupAlphaSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/Mono/CanvasGroupAlphaMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/Mono/CanvasGroupAlphaEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Interactable/CanvasGroupInteractableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/OneWayToSource/Mono/CanvasGroupToSourceMonoBinder.cs
tags: [binder, starterkit, unity, canvasgroup, ui]
updated_at: 2026-05-31
---

# CanvasGroup Binders

> StarterKit binders that drive a Unity `CanvasGroup` from ViewModel state — fade panels in/out, toggle interactivity, and gate raycasts — without writing UI glue code.

A `CanvasGroup` controls a whole UI subtree at once: its `alpha`, `interactable`, `blocksRaycasts`, and `ignoreParentGroups`. This family exposes each of those four properties as a bindable target so a ViewModel value (float, bool, or enum) maps straight onto the component.

## Family table

| Target property | Type | Base binder | Default [[BindMode]] |
|---|---|---|---|
| `alpha` | float | `TargetFloatBinder<CanvasGroup>` | `OneWay` |
| `interactable` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |
| `blocksRaycasts` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |
| `ignoreParentGroups` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |

Each member implements a sealed `Property` get/set wrapper over the matching `CanvasGroup` field — that is the single point where the binder touches Unity.

## Variant axes

Every property is offered along the standard StarterKit axes (see [[Binder Base Classes]] and [[Mono Binders]]):

- **Plain vs Mono** — the plain `…Binder` is a `[Serializable]` class consumed in code/[[View]] fields; the `…MonoBinder` is a MonoBehaviour with `[AddComponentMenu]`/`[AddBinderContextMenu]` for drag-and-drop in the Inspector (e.g. `CanvasGroup Binder – Alpha`).
- **Switcher** — `CanvasGroupAlphaSwitcherBinder` binds a *bool* and picks between a `trueValue`/`falseValue` alpha (e.g. 1 vs 0 for show/hide).
- **Enum / EnumGroup** — `…EnumMonoBinder` selects a value per enum case; `…EnumGroupMonoBinder` applies it across a *group* of `CanvasGroup` components, ideal for tab/page switching.
- **OneWayToSource** — `CanvasGroupToSourceMonoBinder` sends the cached `CanvasGroup` reference back to the ViewModel on bind (see [[BindMode]] `OneWayToSource`). The Alpha Mono binder additionally supports `OneWayToSource`, pushing the current alpha back.

## Notable behavior

- **Alpha is clamped.** Both `GetConvertedValue` (plain/mono) and `SetValue` (switcher/enum) run `Mathf.Clamp01`, so converters can't drive alpha outside [0, 1]. Override hooks must call `base` to preserve this.
- **TwoWay is rejected.** `alpha` and `interactable` constructors call `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — a `CanvasGroup` has no change event, so two-way makes no sense. Use `OneWayToSource` for write-back instead.
- Bool binders accept an `isInvert` flag to flip the source value before applying.

## Source

`StarterKit/Unity/Runtime/Binders/CanvasGroups/` — subfolders `Alpha/`, `Interactable/`, `BlocksRaycasts/`, `IgnoreParentGroups/`, `OneWayToSource/`. See also [[Bool Converters]], [[Number Converters]], and the broader [[Binders Catalog]].

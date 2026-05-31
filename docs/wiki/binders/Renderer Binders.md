---
title: Renderer Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Materials/RendererMaterialsBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Materials/RendererMaterialsSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/MaterialsColor/RendererMaterialColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/MaterialsColor/Mono/RendererMaterialsColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/OneWayToSource/Mono/RendererToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Extensions/RendererSetters.cs
tags: [binders, starterkit, renderer, materials, color]
updated_at: 2026-05-31
---

# Renderer Binders

> StarterKit binders that drive a `UnityEngine.Renderer`'s `material`/`materials` array and its per-material shader color from ViewModel values.

## What it binds

Two distinct targets on a `Renderer`:

- **Materials** — the `Renderer.material` (single) or `Renderer.materials` (array). Accepts a `Material`, `Material[]`, or `IReadOnlyCollection<Material>`. Assignment routes through the `RendererSetters.SetMaterials` extension, which clears on null/empty, uses `material` for a single element, and applies an optional per-element `IConverter<Material, Material>`.
- **Material color** — a named shader color property (default `"_BaseColor"`, configurable) set on *all* materials of the renderer via `Shader.PropertyToID`. Base class is [[Binder Base Classes|TargetColorBinder]]`<Renderer>`.

## Family

| Folder | Target | Base class |
|--------|--------|-----------|
| `Materials/` | `material` / `materials` | [[Binder Base Classes|TargetBinder]]`<Renderer>` |
| `MaterialsColor/` | shader color property | `TargetColorBinder<Renderer>` |
| `OneWayToSource/` | the `Renderer` component itself | `ComponentToSourceMonoBinder<Renderer>` |

## Variant axes

Each target follows the StarterKit pattern (see [[Mono Binders]], [[Binders Catalog]]):

- **Plain vs Mono** — the `Serializable` plain binder (e.g. `RendererMaterialsBinder`, `RendererMaterialColorBinder`) is constructed in code; the `*MonoBinder` sibling under `Mono/` is an inspector-serialized MonoBehaviour with `[AddComponentMenu]` / `[AddBinderContextMenu]`.
- **Switcher** — `RendererMaterialsSwitcherBinder` / `RendererMaterialsColorSwitcherBinder` extend `SwitcherBinder<Renderer, T>`: toggle between a `trueValue` and `falseValue` driven by a bound `bool`.
- **Enum / EnumGroup** — `*EnumMonoBinder` maps enum values to settings on one renderer; `*EnumGroupMonoBinder` (e.g. `RendererMaterialsColorEnumGroupMonoBinder`) applies to a *group* of renderers based on a bound enum.
- **OneWayToSource** — `RendererToSourceMonoBinder` pushes the cached `Renderer` reference back to the ViewModel. Separately, `RendererMaterialsBinder` itself supports [[BindMode|BindMode.OneWayToSource]] via `IReverseBinder<Material>` / `IReverseBinder<Material[]>`, emitting current materials in `OnBound` and `null` in `OnUnbound`.

## Notable behavior

- `RendererMaterialsBinder` declares `[BindModeOverride(OneWay, OneTime, OneWayToSource)]` and throws on `TwoWay`; color binders likewise reject `TwoWay`.
- Color binders cache the property ID lazily (`Shader.PropertyToID`), so the inspector name resolves once.
- All variants accept an optional [[Converters|converter]] applied before assignment.

## Source

`StarterKit/Unity/Runtime/Binders/Renderers/` — subfolders `Materials/`, `MaterialsColor/`, `OneWayToSource/`, `Extensions/` (the `RendererSetters` helper). See also [[Graphic Binders]] and [[LineRenderer Binders]] for related visual targets.

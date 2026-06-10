---
title: Graphic Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/GraphicColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/GraphicColorSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/ColorComponent/GraphicColorComponentBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/ColorComponent.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Material/GraphicMaterialSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/OneWayToSource/Mono/GraphicToSourceMonoBinder.cs
tags:
  - binder
  - starterkit
  - unity-ui
  - graphic
updated_at: 2026-05-31
---

# Graphic Binders

> StarterKit binders that drive Unity UI's `Graphic` base class — color, single color channel, and material — so a ViewModel value paints any UI graphic (Image, RawImage, Text, etc.) without custom glue code.

## Why it exists

`Graphic` is the common base of every renderable Unity UI element. Binding against it (rather than a concrete subtype) lets one binder serve the whole UI hierarchy. The family covers the three things a graphic exposes to a ViewModel: its `color`, one channel of that color, and its `material`. See [[Data Binding]] and [[Binder Base Classes]].

## Family table

| Target axis | Bound property | Plain base | Mono base |
|-------------|----------------|------------|-----------|
| Color | `Graphic.color` | `TargetColorBinder<Graphic>` | `ComponentColorMonoBinder<Graphic>` |
| Color channel | `Graphic.color` R/G/B/A (float) | `TargetFloatBinder<Graphic>` | (Mono variants exist) |
| Material | `Graphic.material` | `SwitcherBinder<Graphic, Material, …>` | `…MonoBinder<Graphic>` |

## Variant axes

- **Plain vs Mono.** `[Serializable]` "plain" binders (e.g. `GraphicColorBinder`) are constructed in code and nest inside a [[View]]. `…MonoBinder` variants are `MonoBehaviour` components wired in the Inspector, carrying `[AddComponentMenu]` / `[AddBinderContextMenu]` so the context menu auto-creates them with the right serialized field (`m_Color`). See [[Mono Binders]].
- **Switcher.** `…SwitcherBinder` binds a **boolean** and flips the target between two preset values (`trueColor`/`falseColor`, or two `Material`s). One bool → two outcomes.
- **Enum / EnumGroup.** `…ColorEnumMonoBinder` maps a bound **enum** to a color. The **EnumGroup** variant applies the resolved value to *every* `Graphic` in a serialized group, not just one — useful for theming a panel of elements at once.
- **OneWayToSource.** `GraphicToSourceMonoBinder` reverses direction: on bind it pushes the cached `Graphic` reference back to the ViewModel (see [[BindMode]] `OneWayToSource`). `GraphicColorMonoBinder` additionally seeds the source with the current color when bound in that mode.

## Notable behavior

- **TwoWay is rejected.** Color and color-component constructors call `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — graphics are output sinks, so `OneWay`/`OneTime`/`OneWayToSource` only.
- **Color channel** uses the `ColorComponent` enum (R/G/B/A) plus `GraphicExtensions.GetColorComponent`/`SetColorComponent`; the bound value is a single `float`, letting you animate just alpha.
- **Material converter is version-gated:** Unity 2023.1+ uses `IConverter<Material?, Material?>`; older versions fall back to `IConverterMaterial`.

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/` — subfolders `Color/`, `ColorComponent/`, `Material/`, each with a `Mono/` folder, plus `OneWayToSource/` and shared `Extensions/`.

Related: [[Image Binders]], [[RawImage Binders]], [[Text Binders]], [[Renderer Binders]], [[Converters]], [[Binders Catalog]].

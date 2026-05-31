---
title: Layout Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/HorizontalOrVerticalLayoutGroup/Spacing/HorizontalOrVerticalLayoutSpacingBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/HorizontalOrVerticalLayoutGroup/Spacing/Mono/HorizontalOrVerticalLayoutSpacingMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/LayoutGroupPaddingBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/LayoutGroupPaddingSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/Mono/LayoutGroupPaddingEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/OneWayToSource/Mono/LayoutGroupToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/PaddingMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/Extensions/LayoutSetters.cs
tags: [binders, starterkit, layout, ui]
updated_at: 2026-05-31
---

# Layout Binders

> StarterKit binders that drive Unity UI `LayoutGroup` components from a ViewModel — control `padding` and `spacing` from data instead of hand-tuning the Inspector.

These exist because layout-driven UI (margins, gaps) often needs to react to state: density toggles, responsive padding, themed spacing. The family wraps two Unity targets — `LayoutGroup.padding` and `HorizontalOrVerticalLayoutGroup.spacing` — so a [[Bindable Members|bindable member]] can set them with no reflection.

## Family

| Group | Target property | Value type |
|---|---|---|
| `LayoutGroup` Padding | `LayoutGroup.padding` | `RectOffset` (or `int`/`float` via [[Converters\|INumberBinder]]) |
| HorizontalOrVertical Spacing | `HorizontalOrVerticalLayoutGroup.spacing` | `float` (also `INumberBinder`) |

## Variant axes

Each group follows the StarterKit binder matrix:

- **Plain vs Mono** — Plain binders (`...Binder`, `[Serializable]`) take their target via constructor and bind in code. The `...MonoBinder` form is a `MonoBehaviour` (`[AddComponentMenu]` / `[AddBinderContextMenu]`) that resolves its target from `CachedComponent` and is wired in the Inspector. See [[Binder Base Classes]] and [[Mono Binders]].
- **Switcher** — `...SwitcherBinder` extends `SwitcherBinder<,,>` and toggles between a `trueValue` / `falseValue` based on a bound `bool`.
- **Enum / EnumGroup** — `...EnumMonoBinder` (extends `EnumMonoBinder<,,>`) maps a single enum value to a layout value; the `...EnumGroupMonoBinder` variant handles a group/set of enum cases.
- **OneWayToSource** — `LayoutGroupToSourceMonoBinder` (a `ComponentToSourceMonoBinder<LayoutGroup>`) sends the component reference *back* to the ViewModel when binding is established. Spacing's plain `MonoBinder` also supports [[BindMode|BindMode.OneWayToSource]], pushing the current spacing to source. See [[Runtime Binding Resolution]].

## Notable behavior

- **`PaddingMode`** — padding binders carry a `[SerializeField] PaddingMode` (`All`, `Left`, `Right`, `Top`, `Bottom`) selecting which sides update. The shared `LayoutSetters.SetPadding` extension writes those sides and calls `LayoutRebuilder.MarkLayoutForRebuild`, so the layout actually re-flows.
- **Numeric convenience** — padding binders implement `INumberBinder`: an `int`/`long`/`float`/`double` is applied uniformly to all four sides (reusing a cached `RectOffset`). Spacing forwards the number directly.
- **No TwoWay** — plain padding and spacing binders throw on `BindMode.TwoWay` (`mode.ThrowExceptionIfMatches(BindMode.TwoWay)`); use OneWayToSource for source updates.
- **Unity version split** — converter aliases differ: `IConverter<RectOffset?, RectOffset?>` on Unity 2023.1+, else the `IConverterRectOffset` interface.

The Mono variants appear under the `Aspid/MVVM/Binders/UI/LayoutGroup/...` component menu. See the full [[Binders Catalog]] and [[Data Binding]] overview.

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/` — split into `LayoutGroup/` (Padding, OneWayToSource) and `HorizontalOrVerticalLayoutGroup/` (Spacing), with shared `PaddingMode.cs` and `Extensions/LayoutSetters.cs`.

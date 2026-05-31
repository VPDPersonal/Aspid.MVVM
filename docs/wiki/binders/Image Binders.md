---
title: Image Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/ImageSpriteBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Fill/ImageFillBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/ImageSpriteSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteAddressableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/OneToSource/Mono/ImageToSourceMonoBinder.cs
tags: [binder, starterkit, unity-ui, image]
updated_at: 2026-05-31
---

# Image Binders

> StarterKit binders that drive a Unity UI `Image` from a ViewModel — its sprite, fill amount, or component reference — so artwork and progress bars react to bound state without manual `Image` wiring.

The family splits along **what** it writes on the `Image`: the **Sprite** sub-group targets `Image.sprite`, and the **Fill** sub-group targets `Image.fillAmount` (clamped to `[0, 1]`).

## Family table

| Binder | Target property | Bound value |
|--------|----------------|-------------|
| `ImageSpriteBinder` | `Image.sprite` | `Sprite` (also `IBinder<Texture2D>` — builds a sprite from a texture) |
| `ImageSpriteSwitcherBinder` | `Image.sprite` | `bool` → picks `trueValue`/`falseValue` |
| `ImageSpriteEnumMonoBinder` / `…EnumGroupMonoBinder` | `Image.sprite` | enum → mapped sprite |
| `ImageSpriteAddressableMonoBinder` | `Image.sprite` | Addressables key → loaded sprite |
| `ImageFillBinder` | `Image.fillAmount` | `float` (clamped 0–1) |
| `ImageFillSwitcherBinder` / `…EnumMonoBinder` / `…EnumGroupMonoBinder` | `Image.fillAmount` | bool / enum |
| `ImageToSourceMonoBinder` | (none — sends `Image` ref) | `OneWayToSource` |

## Variant axes

- **Plain vs Mono** — Plain binders (`ImageSpriteBinder`, `ImageFillBinder`, the `…SwitcherBinder`s) are `[Serializable]` classes constructed in code with a target reference; they extend [[Binder Base Classes|TargetBinder]] / `TargetFloatBinder` / `SwitcherBinder`. Mono variants (under `Mono/`) are MonoBehaviours extending `ComponentMonoBinder<Image, …>` that cache the component and add `[AddComponentMenu]` / `[AddBinderContextMenu]` so they drop onto a GameObject from the Inspector. See [[Mono Binders]].
- **Switcher** — toggles between two preset sprites (or fill values) from a bound `bool`.
- **Enum / EnumGroup** — `Enum` maps one enum value to one sprite/fill; `EnumGroup` maps a set of values, useful for state-driven swaps.
- **OneWayToSource** — `ImageToSourceMonoBinder` reverses the flow: on bind it pushes the cached `Image` reference back to the ViewModel (see [[BindMode]] `OneWayToSource`).

## Notable behavior

- **`disabledWhenNull`** — Sprite binders expose this flag; when the resolved sprite is `null` the `Image` component is disabled, hiding it instead of showing a blank quad.
- **Texture2D path** — `ImageSpriteBinder` / `ImageSpriteMonoBinder` also accept `Texture2D`, calling `Sprite.Create` and destroying the generated sprite in `OnUnbound` to avoid leaks (likely the reason these binders manage lifecycle explicitly).
- **No TwoWay** — sprite/fill binders throw on `BindMode.TwoWay`.
- **Fill clamping** — `ImageFillBinder.GetConvertedValue` runs `Mathf.Clamp01` after conversion; overrides must call `base`.
- **Addressables variant** is gated behind `ASPID_MVVM_ADDRESSABLES_INTEGRATION` and shows a default sprite while loading. See [[External Dependencies]].

The Mono binders are `partial` because Aspid's generator emits the `IBinder` plumbing — see [[ViewModel to Generated Code]] and [[Must Be Partial]].

## Source

`StarterKit/Unity/Runtime/Binders/Images/` — `Sprite/`, `Fill/`, `OneToSource/`. Related: [[RawImage Binders]], [[Graphic Binders]], [[Binders Catalog]], [[Converters]].

---
title: RawImage Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/RawImageTextureBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/RawImageTextureSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureAddressableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/OneWayToSource/Mono/RawImageToSourceMonoBinder.cs
tags: [binder, starterkit, ui, rawimage, texture]
updated_at: 2026-05-31
---

# RawImage Binders

> StarterKit binders that drive a Unity UI `RawImage.texture` from a ViewModel — the raw-texture sibling of [[Image Binders]] (which works on sprites).

`RawImage` displays an arbitrary `Texture`, so this family binds ViewModel values to `RawImage.texture`. Every variant shares one behavior: when the resolved texture is `null` and the **Disable When Null** option is on (the default), the `RawImage` component is disabled, hiding a stale or empty graphic.

## Family

| Binder | Base class | Bound input |
|--------|-----------|-------------|
| `RawImageTextureBinder` | `TargetBinder<RawImage, Texture, Converter>` | `Texture` (also `Sprite` via `IBinder<Sprite>`) |
| `RawImageTextureMonoBinder` | `ComponentMonoBinder<RawImage, Texture, Converter>` | `Texture` / `Sprite` |
| `RawImageTextureSwitcherBinder` | `SwitcherBinder<…>` | `bool` -> true/false texture |
| `RawImageTextureSwitcherMonoBinder` | switcher MonoBinder | `bool` |
| `RawImageTextureEnumMonoBinder` | `EnumMonoBinder<…>` | enum -> texture |
| `RawImageTextureEnumGroupMonoBinder` | `EnumGroupMonoBinder<…>` | enum -> texture across many RawImages |
| `RawImageTextureAddressableMonoBinder` | `AddressableMonoBinder<Texture2D, RawImage>` | address key (loads asset) |
| `RawImageToSourceMonoBinder` | `ComponentToSourceMonoBinder<RawImage>` | reads RawImage back to VM |

## Variant axes

- **Plain vs Mono.** Plain `[Serializable]` binders (`RawImageTextureBinder`) are constructed in code with a target and [[BindMode]]; the `…MonoBinder` forms are `MonoBehaviour`s configured in the inspector and registered for context/component menus. See [[Mono Binders]] and [[Binder Base Classes]].
- **Switcher.** Picks between two preset textures from a bound `bool`.
- **Enum / EnumGroup.** Map enum cases to textures; EnumGroup applies the selection across a list of `RawImage` components at once.
- **Addressable.** Guarded by `ASPID_MVVM_ADDRESSABLES_INTEGRATION`; loads a `Texture2D` from the Addressables system, with a default texture shown while loading.
- **OneWayToSource.** `RawImageToSourceMonoBinder` pushes the component's current state back to the ViewModel ([[BindMode|OneWayToSource]]). The plain `RawImageTextureBinder` throws if constructed with `BindMode.TwoWay`; the Mono variant also supports OneWayToSource directly.

## Notable behavior

- Converter type is version-gated: on Unity 2023.1+ it is `IConverter<Texture?, Texture?>`; older versions use `IConverterTexture`. See [[Converters]].
- `Sprite` inputs are accepted by unwrapping `sprite.texture`, so the same binder serves either source value.
- `Disable When Null` defaults to `true` everywhere except the plain `RawImageTextureBinder` constructor parameter, which also defaults `true`.
- The Mono binder's `SetValue(Sprite)` is decorated with `[BinderLog]`, whose logging body is emitted by the [[Source Generator]] (not visible in hand-written source).

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/` — `Texture/` (plain + `Mono/`) and `OneWayToSource/Mono/`. See also [[IBinder]] and the [[Binders Catalog]].

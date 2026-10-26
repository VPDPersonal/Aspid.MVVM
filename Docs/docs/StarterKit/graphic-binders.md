# Graphic & Renderer Binders

Binders for the color and materials of UI components (`Graphic`) and 3D objects (`Renderer`).

---

## Graphic (UI)

### GraphicColorBinder

Binds `Graphic.color` (`Color`). Works with any UI component derived from `Graphic` (Image, Text, RawImage and others).

| Property | Description |
|----------|----------|
| Converter | `IConverter<Color, Color>` (optional) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class ThemeViewModel
{
    [OneWayBind] private Color _buttonColor;
}
```

---

### GraphicColorSwitcherBinder

`bool` → one of two colors.

---

### GraphicColorChannelBinder

Binds a single color channel (`R`, `G`, `B` or `A`) as a `float`.

| Property | Description |
|----------|----------|
| `_colorComponent` | Color channel: `R`, `G`, `B`, `A` |
| Converter | `IConverter<float, float>` (optional) |

Handy for binding alpha separately from the other channels:

```csharp
[ViewModel]
public partial class FadeViewModel
{
    [OneWayBind] private float _alpha;
    // GraphicColorChannelBinder with ColorChannels.A
}
```

**Modes:** OneWay, OneTime, OneWayToSource.

---

### GraphicColorChannelSwitcherBinder

`bool` → one of two channel values.

---

### GraphicMaterialBinder

Binds `Graphic.material` (`Material`).

**Modes:** OneWay, OneTime, OneWayToSource.

---

### GraphicMaterialSwitcherBinder

`bool` → one of two materials.

---

## Renderer (3D)

### RendererMaterialsColorBinder

Binds a `Renderer` material color through a shader property.

| Property | Description |
|----------|----------|
| `_colorPropertyName` | Shader property name (default `"_BaseColor"`) |
| Converter | `IConverter<Color, Color>` (optional) |

Sets the color on **all** `Renderer.materials` at once (the materials are instanced for this renderer). `Shader.PropertyToID` is used for caching.

**Modes:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class HighlightViewModel
{
    [OneWayBind] private Color _highlightColor;
    // RendererMaterialsColorBinder with _colorPropertyName = "_BaseColor"
}
```

---

### RendererMaterialsColorSwitcherBinder

`bool` → one of two colors for the shader property.

---

### RendererMaterialsBinder

Binds `Renderer.material` (`Material`) or `Renderer.materials` (`IReadOnlyCollection<Material>`); `null` or an empty collection clears the array. In OneWayToSource it hands out `sharedMaterial` / `sharedMaterials`.

**Modes:** OneWay, OneTime, OneWayToSource.

---

### RendererPropertyBlock*MonoBinder

`Float`, `Color`, `Vector`, `Texture`: write a single shader property through a `MaterialPropertyBlock` without instancing materials. The property name is set in the Inspector; an empty name is logged and disables writes until the next bind.

---

### RendererMaterialsSwitcherBinder

`bool` → one of two material arrays.

---

## See also

- [Image Binders](image-binders.md), sprite and fillAmount
- [Canvas Group Binders](canvas-group-binders.md), alpha through CanvasGroup
- [StarterKit overview](README.md)

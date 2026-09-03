# Graphic & Renderer Binders

Биндеры для цвета и материалов UI-компонентов (`Graphic`) и 3D-объектов (`Renderer`).

---

## Graphic (UI)

### GraphicColorBinder

Привязка `Graphic.color` (`Color`). Работает с любым UI-компонентом, наследующим `Graphic` (Image, Text, RawImage и др.).

| Свойство | Описание |
|----------|----------|
| Converter | `IConverter<Color, Color>` (опционально) |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class ThemeViewModel
{
    [OneWayBind] private Color _buttonColor;
}
```

---

### GraphicColorSwitcherBinder

`bool` → выбор между двумя цветами.

---

### GraphicColorChannelBinder

Привязка отдельного компонента цвета (`R`, `G`, `B` или `A`) как `float`.

| Свойство | Описание |
|----------|----------|
| `_colorComponent` | Компонент цвета: `R`, `G`, `B`, `A` |
| Converter | `IConverter<float, float>` (опционально) |

Удобно для привязки alpha отдельно от остальных компонентов:

```csharp
[ViewModel]
public partial class FadeViewModel
{
    [OneWayBind] private float _alpha;
    // GraphicColorChannelBinder с ColorChannels.A
}
```

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### GraphicColorChannelSwitcherBinder

`bool` → выбор между двумя значениями компонента цвета.

---

### GraphicMaterialBinder

Привязка `Graphic.material` (`Material`).

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### GraphicMaterialSwitcherBinder

`bool` → выбор между двумя материалами.

---

## Renderer (3D)

### RendererMaterialsColorBinder

Привязка цвета материала `Renderer` через shader-свойство.

| Свойство | Описание |
|----------|----------|
| `_colorPropertyName` | Имя shader-свойства (по умолчанию `"_BaseColor"`) |
| Converter | `IConverter<Color, Color>` (опционально) |

Устанавливает цвет **всех** материалов `Renderer.materials` одновременно (материалы инстанцируются для этого рендерера). Используется `Shader.PropertyToID` для кэширования.

**Режимы:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class HighlightViewModel
{
    [OneWayBind] private Color _highlightColor;
    // RendererMaterialsColorBinder с _colorPropertyName = "_BaseColor"
}
```

---

### RendererMaterialsColorSwitcherBinder

`bool` → выбор между двумя цветами для shader-свойства.

---

### RendererMaterialsBinder

Привязка `Renderer.material` (`Material`) или `Renderer.materials` (`IReadOnlyCollection<Material>`); `null` или пустая коллекция очищает массив. В OneWayToSource отдаёт `sharedMaterial` / `sharedMaterials`.

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### RendererPropertyBlock*MonoBinder

`Float`, `Color`, `Vector`, `Texture`: пишут одно shader-свойство через `MaterialPropertyBlock`, не инстанцируя материалы. Имя свойства задаётся в Inspector; пустое имя логируется и отключает запись до следующего bind.

---

### RendererMaterialsSwitcherBinder

`bool` → выбор между двумя массивами материалов.

---

## См. также

- [Image Binders](image-binders.md) — спрайт, fillAmount
- [Canvas Group Binders](canvas-group-binders.md) — alpha через CanvasGroup
- [Обзор StarterKit](README.md)

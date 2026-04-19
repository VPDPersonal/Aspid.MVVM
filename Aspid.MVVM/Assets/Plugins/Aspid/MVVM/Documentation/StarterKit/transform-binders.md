# Transform Binders

Биндеры для компонентов `Transform` и `RectTransform`.

---

## Transform

### TransformPositionBinder

Привязка позиции `Transform`.

| Интерфейс | Тип данных |
|-----------|-----------|
| `IBinder<Vector3>` | Устанавливает позицию |
| `INumberBinder` | Принимает числовые типы |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_space` | `Space.World` или `Space.Self` (local) |
| Converter | `IConverter<Vector3, Vector3>` (опционально) |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private Vector3 _position;
}
```

---

### TransformRotationBinder

Привязка поворота `Transform.rotation` / `Transform.localRotation` (`Quaternion`).

| Свойство | Описание |
|----------|----------|
| `_space` | `Space.World` (rotation) или `Space.Self` (localRotation) |

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### TransformEulerAnglesBinder

Привязка углов Эйлера `Transform.eulerAngles` / `Transform.localEulerAngles` (`Vector3`).

| Свойство | Описание |
|----------|----------|
| `_space` | `Space.World` (eulerAngles) или `Space.Self` (localEulerAngles) |

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### TransformScaleBinder

Привязка масштаба `Transform.localScale` (`Vector3`).

**Режимы:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class UIElementViewModel
{
    [OneWayBind] private Vector3 _scale;
}
```

---

### Switcher-варианты

Каждый биндер имеет Switcher-вариант (`bool` → выбор между двумя значениями):

| Биндер | Описание |
|--------|----------|
| `TransformPositionSwitcherBinder` | `bool` → `Vector3` позиция |
| `TransformRotationSwitcherBinder` | `bool` → `Quaternion` поворот |
| `TransformEulerAnglesSwitcherBinder` | `bool` → `Vector3` углы Эйлера |
| `TransformScaleSwitcherBinder` | `bool` → `Vector3` масштаб |

---

## RectTransform

### RectTransformAnchoredPositionBinder

Привязка `RectTransform.anchoredPosition` (`Vector2`).

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### RectTransformSizeDeltaBinder

Привязка `RectTransform.sizeDelta` (`Vector2`).

**Режимы:** OneWay, OneTime, OneWayToSource.

---

### RectTransform Switcher-варианты

| Биндер | Описание |
|--------|----------|
| `RectTransformAnchoredPositionSwitcherBinder` | `bool` → `Vector2` позиция |
| `RectTransformSizeDeltaSwitcherBinder` | `bool` → `Vector2` размер |

---

## См. также

- [GameObject Binders](gameobject-binders.md) — видимость, тег
- [Обзор StarterKit](README.md)

# Canvas Group Binders

Биндеры для компонента `CanvasGroup`.

---

## CanvasGroupAlphaBinder

Привязка прозрачности `CanvasGroup.alpha`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<float>` | Устанавливает alpha |
| `INumberBinder` | Принимает `int`, `float`, `long`, `double` |

Значение ограничивается: `Mathf.Clamp01(value)`.

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| Converter | `IConverter<float, float>` (опционально) |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class FadeViewModel
{
    [OneWayBind] private float _panelAlpha;  // 0.0 - 1.0
}
```

---

## CanvasGroupAlphaSwitcherBinder

`bool` → выбор между двумя значениями alpha.

```csharp
// В Inspector: trueValue = 1.0, falseValue = 0.0
// Аналог показа/скрытия, но с плавным управлением
```

**Режимы:** OneWay, OneTime.

---

## CanvasGroupInteractableBinder

Привязка `CanvasGroup.interactable`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<bool>` | Устанавливает interactable |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_isInvert` | Инвертирует значение |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## CanvasGroupBlocksRaycastsBinder

Привязка `CanvasGroup.blocksRaycasts`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<bool>` | Устанавливает blocksRaycasts |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_isInvert` | Инвертирует значение |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## CanvasGroupIgnoreParentGroupsBinder

Привязка `CanvasGroup.ignoreParentGroups`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<bool>` | Устанавливает ignoreParentGroups |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## Пример: панель с управляемой прозрачностью и интерактивностью

```csharp
[ViewModel]
public partial class ModalViewModel
{
    [OneWayBind] private float _overlayAlpha;
    [OneWayBind] private bool _isInteractable;
    [OneWayBind] private bool _blocksRaycasts;
}
```

---

## См. также

- [GameObject Binders](gameobject-binders.md) — альтернатива через SetActive
- [Обзор StarterKit](README.md)

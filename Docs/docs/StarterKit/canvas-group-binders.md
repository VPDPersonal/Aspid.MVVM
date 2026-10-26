# Canvas Group Binders

Binders for the `CanvasGroup` component.

---

## CanvasGroupAlphaBinder

Binds the opacity, `CanvasGroup.alpha`.

| Interface | Description |
|-----------|----------|
| `IBinder<float>` | Sets alpha |
| `INumberBinder` | Accepts `int`, `float`, `long`, `double` |

The value is clamped: `Mathf.Clamp01(value)`.

### Inspector properties

| Property | Description |
|----------|----------|
| Converter | `IConverter<float, float>` (optional) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class FadeViewModel
{
    [OneWayBind] private float _panelAlpha;  // 0.0 - 1.0
}
```

---

## CanvasGroupAlphaSwitcherBinder

`bool` → one of two alpha values.

```csharp
// Inspector: trueValue = 1.0, falseValue = 0.0
// Like show/hide, but with smooth control
```

**Modes:** OneWay, OneTime.

---

## CanvasGroupInteractableBinder

Binds `CanvasGroup.interactable`.

| Interface | Description |
|-----------|----------|
| `IBinder<bool>` | Sets interactable |

### Inspector properties

| Property | Description |
|----------|----------|
| `_converter` | Optional value converter (for example `BoolInvertConverter`) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## CanvasGroupBlocksRaycastsBinder

Binds `CanvasGroup.blocksRaycasts`.

| Interface | Description |
|-----------|----------|
| `IBinder<bool>` | Sets blocksRaycasts |

### Inspector properties

| Property | Description |
|----------|----------|
| `_converter` | Optional value converter (for example `BoolInvertConverter`) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## CanvasGroupIgnoreParentGroupsBinder

Binds `CanvasGroup.ignoreParentGroups`.

| Interface | Description |
|-----------|----------|
| `IBinder<bool>` | Sets ignoreParentGroups |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## Example: a panel with controlled opacity and interactivity

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

## See also

- [GameObject Binders](gameobject-binders.md), the SetActive alternative
- [StarterKit overview](README.md)

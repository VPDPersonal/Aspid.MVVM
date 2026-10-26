# Switcher Binders

The Switcher pattern: `bool` → one of two preset values.

---

## Overview

A Switcher binder takes a `bool` and returns one of two values:
- `true` → `_trueValue`
- `false` → `_falseValue`

An alternative to converters for simple "if/else" cases.

---

## SwitcherBinder\<T\>

The base class (not a MonoBehaviour):

```csharp
public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
{
    // Serialized in the Inspector:
    protected T _trueValue;
    protected T _falseValue;

    public void SetValue(bool value)
    {
        SetValue(value ? _trueValue : _falseValue);
    }

    protected abstract void SetValue(T value);
}
```

**Modes:** OneWay, OneTime. TwoWay and OneWayToSource are **not supported**.

---

## Ready-made Switcher binders

Almost every standard binder has a Switcher variant:

| Switcher binder | Picks between | Example |
|----------------|----------------|--------|
| `TextSwitcherBinder` | two strings | "Active" / "Inactive" |
| `TextFontSwitcherBinder` | two fonts | Bold / Regular |
| `TextFontSizeSwitcherBinder` | two sizes | 24 / 16 |
| `TextAlignmentSwitcherBinder` | two alignments | Center / Left |
| `ImageSpriteSwitcherBinder` | two sprites | CheckOn / CheckOff |
| `ImageFillSwitcherBinder` | two fillAmount values | 1.0 / 0.0 |
| `SliderMinMaxSwitcherBinder` | two min/max pairs | (0,100) / (0,10) |
| `GraphicColorSwitcherBinder` | two colors | Green / Red |
| `CanvasGroupAlphaSwitcherBinder` | two alpha values | 1.0 / 0.3 |
| `RendererMaterialsColorSwitcherBinder` | two colors | Lit / Dark |
| `SelectableColorBlockSwitcherBinder` | two ColorBlocks | Normal / Disabled |

---

## SwitcherMonoBinder

The MonoBehaviour variant for the Inspector:

```csharp
// Three generic overloads:
SwitcherMonoBinder<T>                    // no converter
SwitcherMonoBinder<T, TTarget>           // with a target component
SwitcherMonoBinder<T, TTarget, TConv>    // with a converter
```

---

## Example

### ViewModel

```csharp
[ViewModel]
public partial class TaskViewModel
{
    [OneWayBind] private bool _isCompleted;
}
```

### Inspector

1. Add `TextSwitcherBinder` to the object with TextMeshPro
2. Set:
   - `True Value` = "Done"
   - `False Value` = "In progress"
3. Bind to `IsCompleted`

Result: the text switches whenever `IsCompleted` changes.

### Status colors

1. Add `GraphicColorSwitcherBinder`
2. `True Value` = green
3. `False Value` = red

---

## Switcher vs converter

| Switcher | Converter |
|----------|-----------|
| Two fixed values | Arbitrary transformation |
| Configured in the Inspector | Logic in code |
| `bool` input only | Any input type |
| Quick setup | Reusable logic |

---

## See also

- [Converters](../08-converters.md), arbitrary transformations
- [StarterKit overview](README.md), every component in one table

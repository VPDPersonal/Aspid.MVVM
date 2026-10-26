# UnityEvent Binders

Binders that forward ViewModel values into a `UnityEvent`, so changes can be subscribed to in the Inspector.

---

## Overview

A UnityEvent binder receives a value from the ViewModel and invokes the matching `UnityEvent<T>`. Reactions to ViewModel changes are configured in the Inspector, without code.

---

## Typed binders

| Binder | UnityEvent | Data type |
|--------|-----------|-----------|
| `UnityEventBoolMonoBinder` | `UnityEvent<bool>` | `bool` |
| `UnityEventFloatMonoBinder` | `UnityEvent<float>` | `float` |
| `UnityEventIntMonoBinder` | `UnityEvent<int>` | `int` |
| `UnityEventLongMonoBinder` | `UnityEvent<long>` | `long` |
| `UnityEventDoubleMonoBinder` | `UnityEvent<double>` | `double` |
| `UnityEventStringMonoBinder` | `UnityEvent<string>` | `string` |
| `UnityEventColorMonoBinder` | `UnityEvent<Color>` | `Color` |
| `UnityEventVector2MonoBinder` | `UnityEvent<Vector2>` | `Vector2` |
| `UnityEventVector3MonoBinder` | `UnityEvent<Vector3>` | `Vector3` |
| `UnityEventQuaternionMonoBinder` | `UnityEvent<Quaternion>` | `Quaternion` |
| `UnityEventEnumMonoBinder` | `UnityEvent<int>` | `enum` (as int) |

---

## Special binders

### UnityEventBoolByBindMonoBinder

Invokes `UnityEvent<bool>` on **bind/unbind** of the binder rather than on value change:

- `OnBound` → `UnityEvent<bool>(true)`
- `OnUnbound` → `UnityEvent<bool>(false)`

Implements `IAnyBinder`: accepts any data type (the value type is ignored).

**Properties:**
- `_isInvert`: inverted, `true` on unbind and `false` on bind

**When to use:** show or hide a UI element depending on whether the binder is bound.

### UnityEventSwitcherMonoBinder

`bool` → one of two values → `UnityEvent<T>`.

### UnityEventNumberConditionMonoBinder

A numeric condition → `UnityEvent<bool>`. Compares the number with a threshold.

### UnityEventNumberConditionSwitcherMonoBinder

A numeric condition → one of two values.

---

## Supported modes

Every UnityEvent binder supports **OneWay** and **OneTime**.

---

## Example

```csharp
[ViewModel]
public partial class NotificationViewModel
{
    [OneWayBind] private bool _hasNewMessages;
    [OneWayBind] private string _message;
}
```

In the Inspector:
1. Add `UnityEventBoolMonoBinder` → bind to `HasNewMessages`
2. In the UnityEvent subscribe a method, for example `NotificationPanel.SetActive(bool)`
3. Add `UnityEventStringMonoBinder` → bind to `Message`
4. In the UnityEvent subscribe the method that shows the message

---

## See also

- [Binders](../06-binders.md), the basics
- [StarterKit overview](README.md), every component in one table

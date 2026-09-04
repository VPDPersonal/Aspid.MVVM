# Transform Binders

Binders for the `Transform` and `RectTransform` components.

---

## Transform

### TransformPositionBinder

Binds the `Transform` position.

| Interface | Data type |
|-----------|-----------|
| `IBinder<Vector3>` | Sets the position |
| `INumberBinder` | Accepts numeric types |

### Inspector properties

| Property | Description |
|----------|----------|
| `_space` | `Space.World` or `Space.Self` (local) |
| Converter | `IConverter<Vector3, Vector3>` (optional) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private Vector3 _position;
}
```

---

### TransformRotationBinder

Binds the rotation, `Transform.rotation` / `Transform.localRotation` (`Quaternion`).

| Property | Description |
|----------|----------|
| `_space` | `Space.World` (rotation) or `Space.Self` (localRotation) |

**Modes:** OneWay, OneTime, OneWayToSource.

---

### TransformEulerAnglesBinder

Binds the Euler angles, `Transform.eulerAngles` / `Transform.localEulerAngles` (`Vector3`).

| Property | Description |
|----------|----------|
| `_space` | `Space.World` (eulerAngles) or `Space.Self` (localEulerAngles) |

**Modes:** OneWay, OneTime, OneWayToSource.

---

### TransformScaleBinder

Binds the scale, `Transform.localScale` (`Vector3`). A number (`INumberBinder`) is applied as a uniform scale on all three axes.

**Modes:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class UIElementViewModel
{
    [OneWayBind] private Vector3 _scale;
}
```

---

### Switcher variants

Every binder has a Switcher variant (`bool` → one of two values):

| Binder | Description |
|--------|----------|
| `TransformPositionSwitcherBinder` | `bool` → `Vector3` position |
| `TransformRotationSwitcherBinder` | `bool` → `Quaternion` rotation |
| `TransformEulerAnglesSwitcherBinder` | `bool` → `Vector3` Euler angles |
| `TransformScaleSwitcherBinder` | `bool` → `Vector3` scale |

---

### TransformParentBinder and TransformSiblingIndexBinder

| Binder | Data type | Description |
|--------|-----------|----------|
| `TransformParentBinder` | `Transform` | `Transform.parent`; local position and rotation are kept, `null` detaches to the scene root |
| `TransformSiblingIndexBinder` | `int` | Index among siblings, clamped to the existing range |

---

## RectTransform

### RectTransformAnchoredPositionBinder

Binds `RectTransform.anchoredPosition` / `anchoredPosition3D` (`Vector3`, chosen through `_space`).

**Modes:** OneWay, OneTime, OneWayToSource.

---

### RectTransformSizeDeltaBinder

Binds `RectTransform.sizeDelta` (`Vector3`, axes chosen through `SizeDeltaMode`). In OneWayToSource the size is handed out both as `Vector3` and as `Vector2`.

**Modes:** OneWay, OneTime, OneWayToSource.

---

### RectTransform Switcher variants

| Binder | Description |
|--------|----------|
| `RectTransformAnchoredPositionSwitcherBinder` | `bool` → `Vector3` position |
| `RectTransformSizeDeltaSwitcherBinder` | `bool` → `Vector3` size |

---

### Other RectTransform binders

| Binder | Data type | Property |
|--------|-----------|----------|
| `RectTransformAnchorMinBinder` | `Vector2` | `anchorMin` |
| `RectTransformAnchorMaxBinder` | `Vector2` | `anchorMax` |
| `RectTransformOffsetMinBinder` | `Vector2` | `offsetMin` |
| `RectTransformOffsetMaxBinder` | `Vector2` | `offsetMax` |
| `RectTransformPivotBinder` | `Vector2` | `pivot` |

All write finite values only; NaN and infinity are logged and skipped.

`AnchorMin`, `AnchorMax` and `Pivot` have Switcher, Enum and EnumGroup variants (`RectTransformPivotSwitcherBinder`, `RectTransformPivotEnumMonoBinder`, …).

---

## See also

- [GameObject Binders](gameobject-binders.md), visibility and tag
- [StarterKit overview](README.md)

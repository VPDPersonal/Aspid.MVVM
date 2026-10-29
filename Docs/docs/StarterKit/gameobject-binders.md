# GameObject Binders

Binders that drive `GameObject` properties.

---

## GameObjectVisibleBinder

Binds visibility through `GameObject.SetActive(bool)`.

| Interface | Description |
|-----------|----------|
| `IBinder<bool>` | Sets the object's active state |
| `IReverseBinder<bool>` | Sends the current state (OneWayToSource) |

### Inspector properties

| Property | Description |
|----------|----------|
| `_converter` | Optional value converter (for example `BoolInvertConverter`) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class PanelViewModel
{
    [OneWayBind] private bool _isVisible;
}
```

### Inverted example

To hide the object when the value is `true`:

```csharp
[ViewModel]
public partial class LoadingViewModel
{
    [OneWayBind] private bool _isLoading;
    // BoolInvertConverter → the GameObject is hidden while isLoading = true
}
```

---

## GameObjectTagBinder

Binds the tag, `GameObject.tag`.

| Interface | Description |
|-----------|----------|
| `IBinder<string>` | Sets the tag |
| `IReverseBinder<string>` | Sends the current tag (OneWayToSource) |

### Inspector properties

| Property | Description |
|----------|----------|
| Converter | `IConverter<string?, string?>` (optional) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## GameObjectTagSwitcherBinder

`bool` → one of two tags.

**Modes:** OneWay, OneTime.

---

## GameObjectVisibleByBindMonoBinder

A MonoBinder wrapper for `GameObjectVisibleBinder`. Binds the visibility of a target `GameObject` through the Inspector.

Unlike the plain `GameObjectVisibleBinder`, it drives the active state of **another** `GameObject`, not the one the binder sits on.

---

## Example: showing and hiding panels

```csharp
[ViewModel]
public partial class UIViewModel
{
    [OneWayBind] private bool _showInventory;
    [OneWayBind] private bool _showMap;
    [OneWayBind] private bool _showSettings;
}
```

In the View bind a `GameObjectVisibleBinder` to each panel. The panels show and hide as the ViewModel properties change.

---

## See also

- [Canvas Group Binders](canvas-group-binders.md), the alpha/interactable alternative
- [Switcher Binders](switcher-binders.md), the Switcher pattern
- [StarterKit overview](README.md)

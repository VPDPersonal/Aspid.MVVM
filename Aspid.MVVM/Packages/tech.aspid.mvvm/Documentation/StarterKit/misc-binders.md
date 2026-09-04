# Misc Binders

Binders that do not belong to a specific UI component category.

---

## ObjectNameBinder

Binds the object name, `Object.name`.

| Interface | Description |
|-----------|----------|
| `IBinder<string>` | Sets the name |
| `IReverseBinder<string>` | Sends the current name (OneWayToSource) |

### Inspector properties

| Property | Description |
|----------|----------|
| Converter | `IConverter<string?, string?>` (optional) |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class ItemViewModel
{
    [OneWayBind] private string _itemName;
    // ObjectNameBinder sets gameObject.name = itemName
}
```

---

## ComponentToSourceMonoBinder\<T\>

The base MonoBinder for sending a component from the View to the ViewModel. On bind it sends the component reference through `IReverseBinder<TComponent>`.

Used to pass Unity components into the ViewModel:

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayToSourceBind] private Rigidbody _rigidbody;
}
// In the View a ComponentToSourceMonoBinder<Rigidbody> binds to the field
```

### Ready-made specializations

| Class | Component |
|-------|----------|
| `SliderToSourceMonoBinder` | `Slider` |
| `DropdownToSourceMonoBinder` | `TMP_Dropdown` |
| `AudioSourceToSourceMonoBinder` | `AudioSource` |
| `RendererToSourceMonoBinder` | `Renderer` |
| `RectTransformToSourceMonoBinder` | `RectTransform` |

### The universal ComponentToSourceMonoBinder

The untyped variant sends the `Component` as `object` through `IAnyReverseBinder`. Fits any component.

**Mode:** OneWayToSource only.

---

## ByBindMonoBinder

The "bind by binder" pattern: MonoBinder wrappers that drive a target component on **another** GameObject. Examples:

| Class | Description |
|-------|----------|
| `GameObjectVisibleByBindMonoBinder` | Shows/hides the given GameObject |

---

## See also

- [GameObject Binders](gameobject-binders.md)
- [Binders](../06-binders.md), writing custom binders
- [StarterKit overview](README.md)

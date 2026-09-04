# Binders

A binder is the bridge between a ViewModel property and a UI element. It receives data from the ViewModel and updates the UI, and in TwoWay/OneWayToSource modes sends changes back.

## Contents

- [Class hierarchy](#class-hierarchy)
- [Binder interfaces](#binder-interfaces)
- [Binder: the base class](#binder-the-base-class)
- [MonoBinder](#monobinder)
- [ComponentMonoBinder](#componentmonobinder)
- [TargetBinder](#targetbinder)
- [Writing a custom binder](#writing-a-custom-binder)
- [\[BindModeOverride\]](#bindmodeoverride)
- [\[UsedInModes\]](#usedinmodes)
- [DebugLogBinder](#debuglogbinder)

---

## Class hierarchy

```
Binder (abstract, not a MonoBehaviour)
  └── MonoBinder (MonoBehaviour, abstract)
        └── ComponentMonoBinder<TComponent>
              └── ComponentMonoBinder<TComponent, TProperty>
                    └── TargetBinder<TTarget, TProperty>
                          └── TargetBinder<TTarget, TProperty, TConverter>
                                └── Concrete binders (TextBinder, ImageSpriteBinder, ...)
```

---

## Binder interfaces

| Interface | Purpose |
|-----------|-----------|
| `IBinder<T>` | `void SetValue(T value)`: receives a value from the ViewModel |
| `IReverseBinder<T>` | `event Action<T> ValueChanged`: sends changes from the View |
| `IAnyBinder` | `void SetValue<T>(T value)`: accepts any type |
| `INumberBinder` | `SetValue(int)`, `SetValue(float)`, `SetValue(long)`, `SetValue(double)` |
| `IColorBinder` | `SetValue(Color)` |
| `IVectorBinder` | `SetValue(Vector3)` |
| `INumberReverseBinder` | Reverse binding for numeric types |

### IBinder\<T\>: the main interface

```csharp
public interface IBinder<in T> : IBinder
{
    void SetValue(T value);
}
```

Called on every change of the ViewModel property (OneWay/TwoWay modes).

### IReverseBinder\<T\>: reverse binding

```csharp
public interface IReverseBinder<T> : IBinder
{
    event Action<T>? ValueChanged;
}
```

The UI element raises `ValueChanged?.Invoke(newValue)` on change (typed text, a moved slider).

---

## Binder: the base class

Does not inherit `MonoBehaviour`. Holds the core binding logic:

```csharp
public abstract class Binder
{
    public BindMode Mode { get; }        // Binding mode (serialized)
    public virtual bool CanBind => true;  // Lets a binder opt out of binding
    public bool IsBound { get; }         // Whether it is bound right now

    public void Bind(IBinderAdder binderAdder);   // Bind
    public void Unbind();                          // Unbind

    // Virtual hooks:
    protected virtual void OnBinding() { }
    protected virtual void OnBound() { }
    protected virtual void OnUnbinding() { }
    protected virtual void OnUnbound() { }
}
```

---

## MonoBinder

A MonoBehaviour wrapper over `Binder`. The base of every Inspector binder:

```csharp
public abstract class MonoBinder : MonoBehaviour
{
    // Serialized binding mode, chosen in the Inspector
    [SerializeField] private BindMode _mode;
}
```

Every ready-made StarterKit binder inherits `MonoBinder`.

---

## ComponentMonoBinder

Adds an automatic `GetComponent<T>()`:

```csharp
// One generic parameter: finds the component itself
public abstract class ComponentMonoBinder<TComponent> : MonoBinder
{
    protected TComponent CachedComponent { get; } // Lazy GetComponent
}

// Two generic parameters: plus the property to bind
public abstract class ComponentMonoBinder<TComponent, TProperty> : ...
{
    // Override to bind a concrete property
    protected abstract TProperty Property { get; set; }
}
```

---

## TargetBinder

The StarterKit base class with converter support:

```csharp
public abstract class TargetBinder<TTarget, TProperty> : MonoBinder
{
    protected TTarget Target { get; }    // Target component
    protected abstract TProperty Property { get; set; }
}

// With a converter:
public abstract class TargetBinder<TTarget, TProperty, TConverter> : TargetBinder<TTarget, TProperty>
    where TConverter : IConverter<TProperty?, TProperty?>
{
    // The converter is assigned in the Inspector ([SerializeReference])
    [SerializeReference] private TConverter? _converter;

    // ViewModel → View
    protected override TProperty? GetConvertedValue(TProperty? value) => ...

    // View → ViewModel: runs only when the converter implements ITwoWayConverter
    protected override TProperty? GetConvertedBackValue(TProperty? value) => ...
}
```

The converter lives in a private field; a subclass overrides `GetConvertedValue` /
`GetConvertedBackValue`, not the field. The `TProperty → TProperty` constraint is deliberate: a
converter on a binder changes the value, not its type. Cross-type conversions (`float → string`) are
done by the binder itself.

**Specialized base classes:**

| Class | Property type | Extras |
|-------|-------------|-----------------|
| `TargetBinder<T, bool>` | `bool` | `_converter`: optional `IConverter<bool, bool>` |
| `TargetBinder<T, string>` | `string` | `_converter`: optional `IConverter<string, string>` |
| `TargetFloatBinder<T>` | `float` | `IFloatBinder`: accepts int/long/double |
| `TargetIntBinder<T>` | `int` | `IIntBinder` |
| `TargetBinder<T, Vector3>` + `IVector3Binder` | `Vector3` | accepts `Vector2` (Z = 0) and a scalar (all three components) |
| `TargetBinder<T, Vector2>` + `IVector2Binder` | `Vector2` | accepts `Vector3` (drops Z) and a scalar (both components) |
| `TargetBinder<T, Color>` + `IColorBinder` | `Color` | accepts a hex/HTML color string |
| `TargetBinder<T, Quaternion>` + `IRotationBinder` | `Quaternion` | reads `Vector2`/`Vector3` as Euler angles, a scalar as the same angle on all three axes |

---

## Writing a custom binder

### Example: a binder for Text.color

```csharp
using TMPro;
using UnityEngine;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;

// Restrict the modes: OneWay and OneTime only
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public sealed class TextColorBinder : TargetBinder<TMP_Text, Color>, IColorBinder
{
    // Read and write the text color
    protected override Color Property
    {
        get => Target.color;
        set => Target.color = value;
    }
}
```

### Example: a binder with reverse binding

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class CustomToggleBinder : MonoBinder, IBinder<bool>, IReverseBinder<bool>
{
    [SerializeField] private GameObject _indicator;

    // IBinder<bool>: receive the value from the ViewModel
    public void SetValue(bool value)
    {
        _indicator.SetActive(value);
    }

    // IReverseBinder<bool>: send changes to the ViewModel
    public event Action<bool>? ValueChanged;

    // Call on user click
    public void OnClick()
    {
        var newValue = !_indicator.activeSelf;
        _indicator.SetActive(newValue);
        ValueChanged?.Invoke(newValue);
    }
}
```

### Example: a generic binder from code

```csharp
using Aspid.MVVM.StarterKit;

// No MonoBehaviour: binding from code
var binder = new DelegateOneWayBinder<string>(value =>
{
    Debug.Log($"Value changed: {value}");
});
```

---

## [BindModeOverride]

Restricts the binding modes offered in the Inspector:

```csharp
// OneWay and OneTime only
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public class MyBinder : MonoBinder { }

// Every mode
[BindModeOverride(IsAll = true)]
public class UniversalBinder : MonoBinder { }
```

If the binder has no reverse binding (no `IReverseBinder<T>`), exclude TwoWay and OneWayToSource.

---

## [UsedInModes]

Marks a serialized field as used only in the listed modes. In the Inspector it is greyed out when the
binder is bound in another mode and gets the tooltip `Not used in the current Mode.`:

```csharp
public class MyBinder : MonoBinder, IBinder<string>, IReverseBinder<string>
{
    [Tooltip("Returned when the reverse conversion fails.")]
    [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
    [SerializeField] private string _convertBackFallback = string.Empty;
}
```

The field may sit on the binder itself or inside any serializable object the binder holds: a nested
class, a converter, an array element. The mode is taken from the **nearest** binder above the field;
when a binder is nested in another binder, the inner one decides. Outside a binder the field stays
active.

The attribute changes nothing at runtime. It is Inspector-only and is stripped from builds without
`UNITY_EDITOR`.

---

## DebugLogBinder

A utility binder for debugging. It logs every value it receives:

```csharp
// DebugLogBinder supports every mode and every data type.
// Add it in the Inspector next to a regular binder
// to watch the values.
```

Implements `IAnyBinder` and `IAnyReverseBinder`, accepts any data type.

---

## See also

- [Views](05-views.md), declaring binders in a View
- [ViewModels](04-viewmodels.md), bindable properties
- [StarterKit](StarterKit/README.md), every ready-made binder
- [Converters](08-converters.md), value conversion

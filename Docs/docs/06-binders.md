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

All binder interfaces inherit `IBinder.IsBound`. A custom implementation sets it when binding succeeds, leaves it false when binding is skipped or rolled back, and clears it during cleanup. A `OneTime` binding remains logically bound until `Unbind()` even without a lasting subscription. `Binder` and `MonoBinder` implement this state automatically.

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

    public void Bind(in BindContext context);
    public void Unbind(in UnbindContext context);

    // Virtual hooks:
    protected virtual void OnBinding() { }
    protected virtual void OnAttach() { }
    protected virtual void OnBound() { }
    protected virtual void OnUnbinding() { }
    protected virtual void OnDetach() { }
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

Runtime `AddComponent` uses the binder's `DefaultMode` without requiring Editor `Reset`. Configure `binder.Mode` before binding; the setter rejects changes during binding, cleanup and while bound. Authored serialized modes are preserved.

`Binder.Bind` and `MonoBinder.Bind` throw `InvalidOperationException` on repeated or reentrant binding, preserving an existing binding. Every `BindSafely` / `UnbindSafely` overload logs binder exceptions without rethrowing, including single binders. Generated Views continue initializing after an individual binder fails; errors in View lifecycle hooks still trigger rollback.

The lifecycle has four exclusive states: `Unbound`, `Binding`, `Bound` and `Unbinding`. Installation follows `OnBinding → Adder.Add → OnAttach → Bound → OnBound`. `OnAttach` acquires reverse subscriptions after the first ViewModel value, while `IsBound == false`. `OnBound` notifies that the binding is ready and sends the initial `OneWayToSource` value, with `IsBound == true`.

Installation failures before `Bound` clean up partially acquired resources. Make `OnUnbinding` and `OnDetach` tolerate missing resources. Exceptions from `OnBound` propagate without rolling back the completed binding.

Cleanup follows `Unbinding → OnUnbinding → Remove → OnDetach → Unbound → OnUnbound`. `IsBound` is false from the start of cleanup, but `OnUnbinding` can still report a last value. `OnDetach` releases subscriptions and resources before `Owner` is cleared; `OnUnbound` runs without an owner. Cleanup attempts every step and then rethrows the original failure or an `AggregateException` for multiple failures.

Each active binder belongs to one owner. `BindContext` contains `IBinderAdder Adder` and `object Owner`; `UnbindContext` contains `object Owner`. Both contexts are `readonly struct` values. The owner can be a View, a composite binder or another object responsible for cleanup. `Unbind` compares owners through `ReferenceEquals` and does nothing for a different instance; `Equals` and `GetHashCode` are not used.

```csharp
binder.Bind(new BindContext(member, owner));
binder.Unbind(new UnbindContext(owner));
```

`BindSafely` takes `in BindContext` for a resolved member. Lookup overloads take `FindBindableMemberResult` and `owner`, creating a context only after a successful lookup. `UnbindSafely` takes `in UnbindContext`; diagnostics use its owner. Subclasses can use the protected `Owner` property. A new `Bind` and changes to `Mode` are allowed only in `Unbound`. Owner cleanup from `OnBinding`, the initial `SetValue` or `OnAttach` is rejected before installation finishes. `OnBound` may unbind and bind again, and `OnUnbound` may immediately call `Bind`. A completion hook must finish its resource changes before invoking external code. Debug Rebind preserves the owner; destroying a `MonoBinder` automatically releases its subscriptions.

Reuse one context for the whole collection:

```csharp
var context = new BindContext(member, owner);
binders.BindSafely(in context, memberName: "_values");

var unbindContext = new UnbindContext(this);
binders.UnbindSafely(in unbindContext, memberName: "_values");
```

Generated Views call `IBinder.Unbind(in context)` directly for a scalar binder and `BinderExtensions.Unbind(collection, in context)` for collections. A scalar `Unbind` extension is not needed. Cleanup skips null references, processes remaining binders after failures and then rethrows the original failure or an `AggregateException` for multiple failures. No binding list is retained.

Cleanup errors are collected across `Binder` / `MonoBinder`, collections, `MonoView` groups, generated Views and initializers. One failure retains its instance and stack trace; multiple failures are reported in an `AggregateException`. Use `exception.Flatten().InnerExceptions` for nested aggregates. The error list is created only for the second failure, so aggregation adds no allocations on the successful path. Failed `Bind` installation and View initialization preserve the original failure over rollback failures.

For `BindSafely(IEnumerable<T>)` and `UnbindSafely(IEnumerable<T>)`, enumeration and enumerator disposal failures also propagate: a single failure keeps its instance and stack trace; simultaneous traversal and disposal failures form an `AggregateException`. Binder failures remain separate log entries and do not join that aggregate. Earlier successful operations are not rolled back. An unsuccessful member lookup does not enumerate the collection.

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

`IsOne = true` adds `OneWay` and `OneTime`; `IsTwo = true` adds `TwoWay` and `OneWayToSource`. Both groups are combined with the explicit list. `[GenerateSerializableBinder]` applies the same restriction to the generated constructor. A null list, `None` or an undefined mode is invalid; the generator reports `ASPIDSB006` for an invalid override declaration.

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


## Nested binders

`[BinderLog]` and `[GenerateSerializableBinder]` support nested types when every container is `partial`. The serializable twin is emitted next to its source MonoBinder with the same type parameters and constraints; an existing twin is checked in the same container and at the same generic arity. The global namespace is supported too.

`[AddBinderContextMenu]` accepts a nested type accessible from the current assembly, with no open type parameters on the type or its containers. Private/protected targets and open generic targets report `ASPIDMENU001`: move the attribute to an accessible non-generic component or remove it. Unity's component and serialization restrictions still apply.

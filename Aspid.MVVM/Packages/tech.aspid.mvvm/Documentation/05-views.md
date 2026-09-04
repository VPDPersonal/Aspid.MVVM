# Views

A View is a MonoBehaviour that connects UI elements to ViewModel data through binders. The Source Generator emits the initialization and binding code.

## Contents

- [Creating a View](#creating-a-view)
- [Declaring binders](#declaring-binders)
- [The \[AsBinder\] attribute](#the-asbinder-attribute)
- [The \[BindId\] attribute](#the-bindid-attribute)
- [The \[IgnoreBind\] attribute](#the-ignorebind-attribute)
- [Lifecycle](#lifecycle)
- [IView\<T\>: a strongly typed View](#iviewt-a-strongly-typed-view)
- [EventMonoView](#eventmonoview)
- [Instantiate](#instantiate)

---

## Creating a View

```csharp
using UnityEngine;
using Aspid.MVVM;

[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder _health;
    [SerializeField] private MonoBinder[] _name;
    [SerializeField] private MonoBinder[] _attackCommand;
}
```

**Requirements:**
1. The class is `partial`
2. The `[View]` attribute
3. Inherits `MonoView`

### Naming rule

The field name without the `_`, `m_` or `s_` prefix must match the ViewModel property:

| View field | Bound ViewModel property |
|---|---|
| `_health` | `Health` |
| `_name` | `Name` |
| `_attackCommand` | `AttackCommand` |

### Single vs array

- `MonoBinder _field`: one binder. Handy when there is exactly one UI element.
- `MonoBinder[] _field`: an array. Several UI elements bind to one property.

---

## Declaring binders

Three ways:

### 1. Fields

```csharp
[View]
public partial class ExampleView : MonoView
{
    [SerializeField] private MonoBinder _name;        // single
    [SerializeField] private MonoBinder[] _items;     // array
}
```

### 2. Properties

```csharp
[View]
public partial class ExampleView : MonoView
{
    private MonoBinder NameBinder { get; }
    private MonoBinder[] ItemBinders { get; }
}
```

### 3. [AsBinder]

Wraps a Unity component in the given binder type (see below).

---

## The [AsBinder] attribute

Lets Unity components act as binders directly, without a separate MonoBinder component:

```csharp
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class ExampleView : MonoView
{
    // Image is wrapped in an ImageSpriteBinder
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image _icon;

    // Image[] is wrapped in ImageSpriteBinder[]
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image[] _images;
}
```

The Source Generator emits code that creates an `ImageSpriteBinder` from the `Image` component at initialization.

---

## The [BindId] attribute

Overrides the binding id of a View field:

```csharp
[View]
public partial class ItemView : MonoView
{
    // Binds to the "Number" property instead of "Name"
    [BindId("Number")]
    [SerializeField] private MonoBinder _name;
}
```

Can be combined with `[AsBinder]`:

```csharp
[View]
public partial class CustomView : MonoView
{
    [BindId("PlayerIcon")]
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image _icon;
}
```

---

## The [IgnoreBind] attribute

Excludes a field from automatic binding:

```csharp
[View]
public partial class MixedView : MonoView
{
    [SerializeField] private MonoBinder _name;

    // This binder is NOT bound automatically
    [IgnoreBind]
    [SerializeField] private MonoBinder _customBinder;

    protected override void OnInitializedInternal()
    {
        // Manual binding
    }
}
```

---

## Lifecycle

The Source Generator declares `partial` methods for every stage:

```csharp
[View]
public partial class LifecycleView : MonoView
{
    [SerializeField] private MonoBinder[] _data;

    // ── Initialization ──

    // Before binders are bound
    partial void OnInitializingInternal() { }

    // After every binder is bound
    partial void OnInitializedInternal() { }

    // ── Deinitialization ──

    // Before binders are unbound
    partial void OnDeinitializingInternal() { }

    // After every binder is unbound
    partial void OnDeinitializedInternal() { }

    // ── Binder array creation ──

    // Before binders are cached
    partial void OnInstantiatingBinders() { }

    // After binders are cached
    partial void OnInstantiatedBinders() { }
}
```

### Call order

1. `OnInstantiatingBinders()` / `OnInstantiatedBinders()`: once, on the first Initialize
2. `OnInitializingInternal()`: before binding
3. Every binder is bound through `FindBindableMember` + `Bind`
4. `OnInitializedInternal()`: after binding
5. *(running)*
6. `OnDeinitializingInternal()`: before unbinding
7. Every binder is unbound through `Unbind`
8. `OnDeinitializedInternal()`: after unbinding

---

## IView\<T\>: a strongly typed View

For type-safe initialization:

```csharp
[View]
public partial class StrongView : MonoView, IView<PlayerViewModel>
{
    [SerializeField] private MonoBinder[] _health;

    public void Initialize(PlayerViewModel viewModel)
    {
        // The Source Generator provides the implementation
    }
}
```

Lets you call `view.Initialize(playerVM)` instead of `view.Initialize((IViewModel)playerVM)`.

---

## EventMonoView

A MonoView with UnityEvents for subscribing in the Inspector:

```csharp
// Just inherit EventMonoView
[View]
public partial class NotifyView : EventMonoView
{
    [SerializeField] private MonoBinder _status;
}
```

Events available in the Inspector:
- `Initialized(IViewModel)`: after initialization
- `Deinitialized()`: after deinitialization

---

## Instantiate

A static helper that creates a View from a prefab and initializes it at once:

```csharp
// Create and initialize a View from a prefab
var view = MonoView.Instantiate(prefab, viewModel);

// With a parent Transform
var view = MonoView.Instantiate(prefab, viewModel, parentTransform);
```

---

## Initialization and deinitialization

### From code (the Bootstrap pattern)

```csharp
// Initialize
_view.Initialize(viewModel);

// Deinitialize and dispose the ViewModel
_view.DeinitializeView()?.DisposeViewModel();
```

### Through ViewInitializer (Inspector)

Without code, through the [ViewInitializer](11-view-initializers.md) component.

---

## See also

- [ViewModels](04-viewmodels.md), creating a ViewModel
- [Binders](06-binders.md), binder types
- [View Initializers](11-view-initializers.md), initialization from the Inspector

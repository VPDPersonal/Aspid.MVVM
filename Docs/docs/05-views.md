# Views

A View connects UI elements to ViewModel data through binders; in Unity it usually derives from `MonoView`. The Source Generator emits the initialization and binding code.

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
1. The class is non-static and `partial`
2. The `[View]` attribute

Derive from `MonoView` for Unity component integration; a plain class is also supported.

A nested View retains its original container and generic parameters. Every containing type must be `partial`; otherwise generation reports `ASPIDGEN001`. See [nested type rules and an example](04-viewmodels.md#nested-types).

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
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class ExampleView : MonoView
{
    // Image is wrapped in an ImageSpriteBinder
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image _icon;

    // Image[] is wrapped in ImageSpriteBinder[]
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image[] _images;
}
```

On the first initialization the Source Generator creates the binder once and caches it: `new ImageSpriteBinder(_icon)`,
and one binder per element of an array. The component goes into the first parameter of the binder constructor, so it
must not be `null`: mark the field `[SerializeField]` and assign it in the Inspector.

### Constructor arguments

The arguments after the type go to the binder constructor after the component, in order. They are attribute
arguments, so only constants fit: strings, numbers, `bool`, enums, `typeof`, `null`. Named arguments are not
supported: to reach a later parameter, pass the default of every parameter before it.

```csharp
// new AnimatorSetFloatBinder(_speed, "Speed")
[AsBinder(typeof(AnimatorSetFloatBinder), "Speed")]
[SerializeField] private Animator _speed;

// new AnimatorSetFloatBinder(_jumpHeight, "JumpHeight", null, BindMode.OneTime)
// null fills the converter parameter that stands before mode
[AsBinder(typeof(AnimatorSetFloatBinder), "JumpHeight", null, BindMode.OneTime)]
[SerializeField] private Animator _jumpHeight;
```

The type must be accessible, non-abstract, closed and implement `IBinder` (`ASPIDVIEW001`). An incompatible constructor, unreadable target or null argument array reports `ASPIDVIEW002`. To pass one null constructor argument, write `(object)null`. Strings and numbers retain their escaping and C# types independently of the current culture.

### nameof: passing a View member

An argument written as `nameof(X)` goes into the generated code as the member `X` itself, not as the string `"X"`.
This is how a value that an attribute cannot hold reaches the constructor, such as a converter:

```csharp
private static readonly IConverter<bool, bool> Invert = new BoolInvertConverter();

// new GameObjectVisibleBinder(_isDead, Invert)
[AsBinder(typeof(GameObjectVisibleBinder), nameof(Invert))]
[SerializeField] private GameObject _isDead;
```

The binders are created inside the View, so `X` can be any member visible there, a static or an instance one
(a serialized converter field, for example). Only the last part of the name is kept: `nameof(Converters.Invert)`
becomes `Invert`, which must resolve inside the View.

> [!NOTE]
> `[AsBinder]` fixes the binder type and its arguments in code. When a designer has to pick the binder, the converter
> or the mode in the Inspector, use a `MonoBinder` component instead.

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
    [SerializeField] private Image _icon;
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

    partial void OnInitializedInternal(IViewModel viewModel)
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

    [AsBinder(typeof(Aspid.MVVM.StarterKit.ImageSpriteBinder))]
    [SerializeField] private UnityEngine.UI.Image[] _icons;

    // ── Initialization ──

    // Before binders are bound
    partial void OnInitializingInternal(IViewModel viewModel) { }

    // After every binder is bound
    partial void OnInitializedInternal(IViewModel viewModel) { }

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

1. `OnInitializingInternal(viewModel)`: before binding
2. `OnInstantiatingBinders()` / `OnInstantiatedBinders()`: once, when wrappers or binder properties are first cached
3. Every binder is bound through `FindBindableMember` + `Bind`
4. `OnInitializedInternal(viewModel)`: after binding
5. *(running)*
6. `OnDeinitializingInternal()`: before unbinding
7. Every binder is unbound through `Unbind`
8. `OnDeinitializedInternal()`: after unbinding

Generated Views ignore repeated `Deinitialize()` calls during cleanup and reject calls during initialization with `InvalidOperationException`. Remaining cleanup runs after a binder or hook fails. A single exception retains its instance and stack trace; multiple failures are reported in an `AggregateException`. Successful nested bindings remain owned even if the outer binding fails.

The binder set is fixed between `Initialize` and `Deinitialize`: fields, cached binder properties and collection contents must retain the same instances. The generator releases them directly through `UnbindContext(this)`, without an additional binding list. Property and `[AsBinder]` wrapper caches remain. Cleanup ignores a different owner; rebinding for the same View continues that View's ownership. Dynamically replacing the binder set is unsupported.

`Reinitialize`, `DeinitializeView` and both `DisposeView` overloads require a non-null View and throw `ArgumentNullException` otherwise. These methods also reject destroyed Unity objects. A null return means the View had no ViewModel. `DisposeView<T>` requires a reference type. Passing null as the replacement ViewModel to `Reinitialize` only deinitializes the View. ViewModel disposal remains the caller's responsibility. If initialization through `Reinitialize` fails, a generated View stays detached and does not restore the old ViewModel.

`ReinitializeSafely`, `DeinitializeViewSafely` and `DisposeViewSafely` skip null Views and destroyed Unity objects. They log exceptions to the Unity Console. The methods return the previous ViewModel on success and null when it was absent, the View was skipped, or the operation failed.

---

## IView\<T\>: a strongly typed View

For type-safe initialization:

```csharp
[View]
public partial class StrongView : MonoView, IView<PlayerViewModel>
{
    [SerializeField] private MonoBinder[] _health;

    partial void OnInitializedInternal(IViewModel viewModel)
    {
        // Access the assigned ViewModel after binding.
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

### From the ViewModel

Use the extensions in `Aspid.MVVM` to initialize one or several Views:

```csharp
viewModel?.InitializeView(view);
viewModel?.InitializeViews(viewA, viewB);
viewModel?.InitializeViews(views);
```

`InitializeViews` accepts arrays, `List<T>` where `T : IView`, and `IEnumerable<IView>` (including read-only collections and lazy sequences). Arrays and lists of concrete View types are supported. Passing an existing array or list avoids enumerator allocations; the `params` call creates an array.

`InitializeViewSafely` and `InitializeViewsSafely` skip missing or destroyed ViewModels and Views. The batch method logs an individual View's exception and moves to the next View. An enumeration exception is logged and ends the batch.

With `?.`, a null ViewModel skips the call and argument evaluation. A direct call rejects null and destroyed Unity objects. If both arguments are invalid, it reports both in an `AggregateException`. Collections are processed in order and stop at the first exception; previously initialized Views remain initialized. Each call delegates to `IView.Initialize`, preserving the View's lifecycle rules.

### Through ViewInitializer (Inspector)

Without code, through the [ViewInitializer](11-view-initializers.md) component.

---

## See also

- [ViewModels](04-viewmodels.md), creating a ViewModel
- [Binders](06-binders.md), binder types
- [View Initializers](11-view-initializers.md), initialization from the Inspector

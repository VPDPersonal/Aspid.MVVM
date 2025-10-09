---
icon: sidebar
cover: ../.gitbook/assets/Beautiful Green UI Design.jpg
coverY: 0
---

# Overview - View

<h2 align="center"><i class="fa-sidebar">:sidebar:</i> <a href="../documentation/view/">Defining a View</a></h2>

To define a class as a View, you must:

1. Mark the class with the <mark style="color:$warning;">`[View]`</mark> attribute.
2. Declare the class as <mark style="color:$warning;">`partial`</mark>.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView { }
```

***

### <i class="fa-dna">:dna:</i> [Inheritance](../documentation/view/#inheritance)

A View can inherit from any class:

```csharp
using Aspid.MVVM;

public class SomeClass { }

[View]
public partial class MyView : SomeClass { }
```

If inheriting from another class with the <mark style="color:$warning;">`[View]`</mark> attribute, the child class must also have the <mark style="color:$warning;">`[View]`</mark> attribute:

```csharp
using Aspid.MVVM;
​
[View]
public partial class ParentView { } 
​
[View]
public partial class ChildView : ParentView { } 
```

***

### <i class="fa-trowel-bricks">:trowel-bricks:</i> [Base View Classes](../documentation/view/#base-view-classes)

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides two base classes that extend functionality:

1. [MonoView](../documentation/unity/monoview.md) - A View implemented as a <mark style="color:$warning;">`MonoBehaviour`</mark>. It includes additional debugging tools, such as inspecting the ViewModel used for initialization.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : MonoView { } 
```

2. [ScriptableView](../documentation/unity/scriptableview.md) - A View implemented as a <mark style="color:$warning;">`ScriptableObject`</mark>. It also includes debugging tools, such as inspecting the ViewModel used for initialization.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : ScriptableView { }
```

{% hint style="warning" %}
You can define a View that inherits directly from <mark style="color:$warning;">`MonoBehaviour`</mark>, but we recommend inheriting from <mark style="color:$warning;">`MonoView`</mark> as it provides convenient editor integration in Unity. Additionally, <mark style="color:$warning;">`MonoBinder`</mark> expects to work with <mark style="color:$warning;">`MonoView`</mark>.
{% endhint %}

***

### <i class="fa-arrow-right-to-bracket">:arrow-right-to-bracket:</i> [Initializing a View](../documentation/view/#initializing-a-view)

To bind a View to a specific ViewModel, call the <mark style="color:$warning;">`Initialize(IViewModel viewModel)`</mark> method on the View:

```csharp
using Aspid.MVVM;
using UnityEngine;

[ViewModel]
public partial class MyViewModel { }

[View]
public partial class MyView : MonoView { }

public class Bootstrap 
{
    [SerializeField] private MyView _view;
    
    private void Awake()
    {
        _view.Initialize(new MyViewModel());
    }
}
```

***

## <i class="fa-broom">:broom:</i> [Dispose](../documentation/view/#dispose)

A View can only be initialized with one ViewModel at a time. To initialize the View with a different ViewModel, you must first call <mark style="color:$warning;">`Deinitialize()`</mark>; otherwise, an error will occur:

```csharp
_view.Deinitialize();
```

To also dispose of the ViewModel, you can retrieve the ViewModel from the View before deinitialization:

```csharp
IViewModel viewModel = _view.ViewModel;

_view.Deinitialize();
if (viewModel is IDisposable disposable)
    disposable.Dispose();
```

You can use a shorter syntax with extension methods:

```csharp
// DeinitializeView: Calls Deinitialize on the View and returns the IViewModel.
// DisposeViewModel: Calls Dispose on the ViewModel if it exists.
_view.DeinitializeView()?.DisposeViewModel();
```

Additional extension methods are available:

```csharp
// DestroyView: Calls Dispose on the View if it exists; otherwise,
// calls Destroy(gameObject) on the View's GameObject.
_view.DestroyView()?.DisposeViewModel();

// DestroyViewModel: Calls Dispose on the ViewModel if it exists; otherwise,
// calls Destroy(ViewModel) on the ViewModel component.
_view.DestroyView()?.DestroyViewModel();
```

***

<h2 align="center"><i class="fa-link-horizontal">:link-horizontal:</i> <a href="../documentation/view/binder-members/">Binder Members</a></h2>

All fields and properties that implement <mark style="color:$warning;">`IBinder`</mark> participate in code generation.

### <i class="fa-input-text">:input-text:</i> [Fields](../documentation/view/binder-members/field.md)

In most cases, you will want to use <mark style="color:$warning;">`MonoBinder`</mark> in a <mark style="color:$warning;">`MonoView`</mark> to attach any <mark style="color:$warning;">`MonoBinder`</mark> descendant to a field.

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    // The field ID must match the ID of a field in the ViewModel
    // for binding to succeed.
    [SerializeField] private MonoBinder _name; 
}
```

Only one <mark style="color:$warning;">`MonoBinder`</mark> descendant can be assigned to the <mark style="color:$warning;">`_name`</mark> field. To support multiple binders, declare an array:

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoBinder[] _name; 
}
```

#### <i class="fa-check">:check:</i> [Binder Validation – RequireBinder](../documentation/view/binder-members/field.md#binder-validation-requirebinder)

To ensure a field is bound to a binder that supports binding to a specific type (e.g., <mark style="color:$warning;">`string`</mark>) and avoid runtime errors, mark the field with the <mark style="color:$warning;">`[RequireBinder(Type type)]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    // Only binders that support binding to string can be assigned:
    // IBinder<string> or IReverseBinder<string>. or IAnyBinder.
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name; 
}
```

***

### <i class="fa-pen-field">:pen-field:</i> [Properties](../documentation/view/binder-members/property.md)

Any property whose type implements <mark style="color:$warning;">`IBinder`</mark>:

```csharp
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;

[View]
public partial class MyView : MonoView
{
    // GameObjectVisibleBinder is a binder that enables or disables
    // a GameObject based on a bool value.

    private GameObjectVisibleBinder IsVisible => new(gameObject);
}
```

***

### <i class="fa-wand-sparkles">:wand-sparkles:</i> [\[AsBinder\]](../documentation/view/binder-members/asbinder.md)

To work with standard components through the Unity Editor, use the <mark style="color:$warning;">`[AsBinder(Type type, params object[] arguments)]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class MyView : MonoView
{
    // Constructor: ImageSpriteBinder => (Image target).
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image _image;
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image[] _images;
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image Image => GetComponent<Image>();
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image[] Images => GetComponentInChildren<Image>();
}
```

If your binder requires more arguments than just the target component, you can pass them via the <mark style="color:$warning;">`[AsBinder]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class MyView : MonoView
{
    [SerializeField] private bool _isActive;

    // Constructor: MyBinder -> (Image, int, string, float, bool, bool).
    [AsBinder(typeof(MyBinder), 0, "some", 1.34f, nameof(_isActive), false)]
    [SerializeField] private Image _image;
}
```

***

### <i class="fa-diagram-nested">:diagram-nested:</i> [Nested Views](../documentation/view/binder-members/nested-views.md)

To support nested Views, the Source Generator analyzes types that implement the <mark style="color:$warning;">`IView`</mark> interface in addition to those implementing <mark style="color:$warning;">`IBinder`</mark>:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class ParentViewModel
{
    [OneWayBind] private IViewModel _child = new ChildViewModel();
}

[ViewModel]
public partial class ChildViewModel { } 

[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoView _child;
}
```

***

### <i class="fa-link-horizontal-slash">:link-horizontal-slash:</i> [Ignore generation](../documentation/view/binder-members/ignore-generation.md)

To exclude a class implementing <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark> from code generation, use the <mark style="color:$warning;">`[BindIgnore]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

[View]
public partial class MyView : MonoView
{
    [BindIgnore]
    [SerializeField] private MonoBinder[] _binders;
    
    [BindIgnore]
    [SerializeField] private MonoView _view;
}
```

The generator also attempts to prevent cases where a binder’s <mark style="color:$warning;">`Bind`</mark> method would be called twice:

```csharp
using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoBinder _binder;
    
    // Excluded from generation to prevent the binder
    // from being initialized twice.
    private MonoBinder Binder1 => _binder;
}
```

***

<h2 align="center"><i class="fa-crosshairs">:crosshairs:</i> <a href="../documentation/view/iview-less-than-t-greater-than.md">IView&#x3C;T></a></h2>

To enable direct binding of a View to a specific ViewModel, implement the <mark style="color:$warning;">`IView<T>`</mark> interface, where <mark style="color:$warning;">`T`</mark> is a type that implements <mark style="color:$warning;">`IViewModel`</mark>:

```csharp
using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

[View]
public partial class MyView : MonoView, IView<MyViewModel>
{
    [RequireBinder(typeof(bool))]
    [SerializeField] private MonoBinder[] _isActive;

    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
    
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _age;
}
```

Implementing multiple <mark style="color:$warning;">`IView<T>`</mark> interfaces for a single View is possible, but a large number of implementations may impact performance if you call <mark style="color:$warning;">`Initialize(IViewModel viewModel)`</mark> instead of <mark style="color:$warning;">`Initialize<T>(T viewModel) where T : IViewModel`</mark>.

You can also enable direct binding with a [Custom ViewModel Interface](../documentation/viewmodel/custom-viewmodel-interface.md):

```csharp
using Aspid.MVVM;
using Aspid.MVVM.Unity;

[ViewModel]
public partial class MyViewModel : IMyViewModel
{
    [Bind] private int _age;
    [Bind] private string _name;
}

public interface IMyViewModel : IViewModel
{
    public IBinderAdder Age { get; }
    
    public IBinderAdder Name { get; }
}

[View]
public partial class MyView : MonoView, IView<IMyViewModel>
{
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _age;
    
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```

***

<h2 align="center"><i class="fa-microchip">:microchip:</i> <a href="../documentation/view/handlers.md">Handlers</a></h2>

Each View generates several partial methods:

```
Initialize(viewModel)
│
├── OnInitializingInternal(viewModel)        ← Before initialization
├── InstantiateBinders()
│   ├── OnInstantiatingBinders()             ← Before creating binders
│   ├── [создание и кэширование биндров]
│   └── OnInstantiatedBinders()              ← After creating binders
├── [BindSafely(...) для всех IBinder/IView]
└── OnInitializedInternal(viewModel)         ← After full initialization

Deinitialize()
│
├── OnDeinitializingInternal()               ← Before unbinding
├── [UnbindSafely(...) для всех IBinder/IView]
└── OnDeinitializedInternal()                ← After full deinitialization
```

***

<h2 align="center"><i class="fa-bug">:bug:</i> <a href="../documentation/unity/monoview.md#debug">Debug</a></h2>

Using <mark style="color:$warning;">`MonoView`</mark> provides additional debugging benefits:

### <i class="fa-link-horizontal">:link-horizontal:</i> [Display of Unassigned Binders](../documentation/unity/monoview.md#display-of-unassigned-binders)

Shows all unattached binders. If child objects of the View contain unattached binders, they will be displayed.

<table><thead><tr><th valign="top">All Binders Attached</th><th valign="top">Some Binders Unattached</th></tr></thead><tbody><tr><td valign="top"><div><figure><img src="../.gitbook/assets/image (58).png" alt=""><figcaption></figcaption></figure></div></td><td valign="top"><div><figure><img src="../.gitbook/assets/image (57).png" alt=""><figcaption></figcaption></figure></div></td></tr></tbody></table>

### <i class="fa-file-code">:file-code:</i> [Debugging the Bound ViewModel](../documentation/unity/monoview.md#debugging-the-bound-viewmodel)

After initializing a <mark style="color:$warning;">`MonoView`</mark> or <mark style="color:$warning;">`ScriptableView`</mark> with a ViewModel, you can debug the ViewModel through the Unity Inspector (view and modify values).

<figure><img src="../.gitbook/assets/image (68).png" alt=""><figcaption></figcaption></figure>

{% hint style="info" %}
#### Field Labels in ViewModel Inspection

* <mark style="color:$primary;">**Field**</mark>: Represents an object’s field.
* <mark style="color:$primary;">**Bind**</mark>: Represents a bindable property of the object.
{% endhint %}

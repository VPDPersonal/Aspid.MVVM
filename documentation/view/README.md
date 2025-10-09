---
icon: sidebar
cover: ../../.gitbook/assets/Beautiful Green UI Design.jpg
coverY: 0
layout:
  width: default
  cover:
    visible: true
    size: full
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: true
  pagination:
    visible: true
  metadata:
    visible: true
---

# View

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, the View component represents the visual layer that binds to a ViewModel to display data or interact with the user.

To define a class as a View, it must implement the <mark style="color:$warning;">`IView`</mark> interface:

* <mark style="color:orange;">Manually implementing this interface is not recommended, as it leads to excessive boilerplate code.</mark>
* <mark style="color:green;">The preferred approach is to use the</mark> <mark style="color:green;"></mark><mark style="color:green;">`[View]`</mark> <mark style="color:green;"></mark><mark style="color:green;">attribute to enable automatic code generation via the Source Generator.</mark>

To define a View with code generation, apply the <mark style="color:$warning;">`[View]`</mark> attribute to the desired class:

```csharp
using Aspid.MVVM;

[View]
public partial class MyView { }
```

{% hint style="warning" %}
#### Requirements for Code Generation

* The class must have the <mark style="color:$warning;">`[View]`</mark> attribute.
* The class must be <mark style="color:$warning;">`partial`</mark>. If you forget to mark the class as <mark style="color:$warning;">`partial`</mark>, the code analyzer will flag the error and suggest a fix.
* The class cannot be nested at this time.
{% endhint %}

#### A View can contain [Binder members](binder-members/):

* <mark style="color:$primary;">**Fields and properties**</mark> that implement <mark style="color:$warning;">`IBinder`</mark>
* <mark style="color:$primary;">**Fields and properties**</mark> that implement <mark style="color:$warning;">`IView`</mark>.
* <mark style="color:$primary;">**Fields and Properties with \[AsBinder(Type binderType)]**</mark>.
* <mark style="color:$primary;">**Handlers**</mark>: Handlers for binding the View to a ViewModel.

***

## <i class="fa-dna">:dna:</i> Inheritance

A class with the <mark style="color:$warning;">`[View]`</mark> attribute can inherit from any other class:

```csharp
using Aspid.MVVM;

public class SomeClass { }

[View]
public partial class MyView : SomeClass { }
```

If inheriting from another class with the <mark style="color:$warning;">`[View]`</mark> attribute, the child class must also have the <mark style="color:$warning;">`[View]`</mark> attribute:

```csharp
using Aspid.MVVM;

[View]
public partial class ParentView { } 

[View]
public partial class ChildView : ParentView { } 
```

***

## <i class="fa-trowel-bricks">:trowel-bricks:</i> Base View Classes

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides two base classes that extend functionality:

1. [MonoView](../unity/monoview.md) - A View implemented as a <mark style="color:$warning;">`MonoBehaviour`</mark>. It includes additional debugging tools, such as inspecting the ViewModel used for initialization.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : MonoView { } 
```

2. [ScriptableView](../unity/scriptableview.md) - A View implemented as a <mark style="color:$warning;">`ScriptableObject`</mark>. It also includes debugging tools, such as inspecting the ViewModel used for initialization.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : ScriptableView { }
```

{% hint style="warning" %}
You can define a View that inherits directly from <mark style="color:$warning;">`MonoBehaviour`</mark>, but we recommend inheriting from <mark style="color:$warning;">`MonoView`</mark> as it provides convenient editor integration in Unity. Additionally, <mark style="color:$warning;">`MonoBinder`</mark> expects to work with <mark style="color:$warning;">`MonoView`</mark>.
{% endhint %}

***

## <i class="fa-arrow-right-to-bracket">:arrow-right-to-bracket:</i> Initializing a View

To bind a View to a specific ViewModel, call the <mark style="color:$warning;">`Initialize(IViewModel viewModel)`</mark> method on the View:

```csharp
using Aspid.MVVM;
using UnityEngine;
​
[ViewModel]
public partial class MyViewModel { }
​
[View]
public partial class MyView : MonoView { }
​
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

## <i class="fa-broom">:broom:</i> Dispose

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
// DestroyView: Calls Dispose on the View if it exists; otherwise, calls
// Destroy(gameObject) on the View's GameObject.
_view.DestroyView()?.DisposeViewModel();

// DestroyViewModel: Calls Dispose on the ViewModel if it exists; otherwise, calls
// Destroy(ViewModel) on the ViewModel component.
_view.DestroyView()?.DestroyViewModel();
```

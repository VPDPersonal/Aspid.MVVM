---
icon: file-code
cover: ../.gitbook/assets/image (71).png
coverY: 556.8969181721573
---

# Overview - ViewModel

<h2 align="center"><i class="fa-file-code">:file-code:</i> <a href="../documentation/viewmodel/">Defining a ViewModel</a></h2>

To define a class as a ViewModel, you must:

1. Mark the class with the <mark style="color:$warning;">`[ViewModel]`</mark> attribute.
2. Declare the class as <mark style="color:$warning;">`partial`</mark>.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel { }
```

***

### <i class="fa-dna">:dna:</i> [Inheritance](../documentation/viewmodel/#inheritance)

A ViewModel can inherit from any class:

```csharp
using Aspid.MVVM;

public class SomeClass { }

[ViewModel]
public partial class MyViewModel : SomeClass { }
```

If inheriting from another class with the <mark style="color:$warning;">`[ViewModel]`</mark>  attribute, the child class must also have the <mark style="color:$warning;">`[ViewModel]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class ParentViewModel { } 

[ViewModel]
public partial class ChildViewModel : ParentViewModel { } 
```

***

### <i class="fa-trowel-bricks">:trowel-bricks:</i> [Base ViewModel Classes](../documentation/viewmodel/#base-viewmodel-classes)

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides two base classes that extend functionality:

1. [**MonoViewModel**](../documentation/unity/monoviewmodel.md) - A ViewModel implemented as a <mark style="color:$warning;">`MonoBehaviour`</mark>. It supports notifications when values are changed via the Unity Inspector and serializes commands for interaction in the Inspector.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : MonoViewModel { } 
```

2. [**ScriptableViewModel**](../documentation/unity/scriptableviewmodel.md) - A ViewModel implemented as a <mark style="color:$warning;">`ScriptableObject`</mark>. It supports notifications when values are changed via the Unity Inspector and serializes commands for interaction in the Inspector.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : ScriptableViewModel { }
```

{% hint style="info" %}
Inheriting from these classes is optional. In the future, they may include additional debugging features.
{% endhint %}

***

### <i class="fa-broom">:broom:</i> [Dispose](../documentation/viewmodel/#dispose)

The <mark style="color:$warning;">`IViewModel`</mark> interface does not include <mark style="color:$warning;">`IDisposable`</mark>, but you can implement it manually:

```csharp
IViewModel viewModel = ...;

if (viewModel is IDisposable disposable)
    disposable.Dispose();
```

For a shorter syntax to dispose of ViewModel resources, use the extension method:

```csharp
IViewModel viewModel = ...;
viewModel.DisposeViewModel();
```

***

<h2 align="center"><i class="fa-link-horizontal">:link-horizontal:</i> <a href="../documentation/viewmodel/binding-members/">Binding Members</a></h2>

To define a Binding Member, mark the member with one of the following attributes:

***

### <i class="fa-arrow-right-arrow-left">:arrow-right-arrow-left:</i> [\[Bind\]](../documentation/viewmodel/binding-members/bind.md)

<mark style="color:$primary;">**The \[Bind] attribute**</mark> automatically determines the binding mode (<mark style="color:$primary;">**OneWay**</mark>, <mark style="color:$primary;">**TwoWay**</mark>, <mark style="color:$primary;">**OneTime**</mark>, or <mark style="color:$primary;">**OneWayToSource**</mark>).

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    // OneTime Mode.
    [Bind] private const float Pi = 3.14f;

    // TwoWay Mode.
    [Bind] private string _name;
    
    // OneTime Mode.
    [Bind] private readonly int _age;
}
```

The code above generates the following ViewModel members:

* <mark style="color:$warning;">`public IReadOnlyBindableMember<string> NameBindable;`</mark> - A bindable component containing the current value and an event for value changes.
* <mark style="color:$warning;">`private string Name { get; set; }`</mark>  - A property for the <mark style="color:$warning;">`_name`</mark> field for internal use in the ViewModel.
* <mark style="color:$warning;">`private int Age { get; }`</mark>  - A property for the <mark style="color:$warning;">`_age`</mark> field for internal use in the ViewModel.
* <mark style="color:$warning;">`private void SetName(string value)`</mark>  - A method to set the value of the <mark style="color:$warning;">`_name`</mark> field (<mark style="color:$warning;">`SetName(newValue)`</mark> is equivalent to <mark style="color:$warning;">`Name = newValue`</mark>).
* [<mark style="color:$warning;">`partial void OnNameChanging(string oldValue, string newValue);`</mark>](overview-viewmodel.md#handlers)  - Called before the <mark style="color:$warning;">`_name`</mark> field changes.
* [<mark style="color:$warning;">`partial void OnNameChanged(string newValue);`</mark>](overview-viewmodel.md#handlers) - Called after the <mark style="color:$warning;">`_name`</mark> field changes.

***

### <i class="fa-sliders-up">:sliders-up:</i> [Overriding the Binding Mode](../documentation/viewmodel/binding-members/bind.md#overriding-the-binding-mode)

If the automatically selected binding mode is not suitable, you can override it using the <mark style="color:$warning;">`BindMode`</mark> parameter in the <mark style="color:$warning;">`[Bind(BindMode mode)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    // Only OneTime mode is allowed for constants
    [BindMode(BindMode.OneTime)] private const float Pi = 3.14f;

    [BindMode(BindMode.OneWay)] private string _name1;
    [BindMode(BindMode.TwoWay)] private string _name2;
    [BindMode(BindMode.OneTime)] private string _name3;
    [BindMode(BindMode.OneWayToSource)] private string _name4;
    
    // Only OneTime mode is allowed for readonly fields
    [BindMode(BindMode.OneTime)] private readonly string _name5;
}
```

***

### <i class="fa-down-left-and-up-right-to-center">:down-left-and-up-right-to-center:</i> Shorter Binding Mode Attributes

The following attributes are shorthand equivalents for overriding the binding mode:

| Атрибут                                                                                    | Эквивалент                                                             |
| ------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------- |
| [\[OneWayBind\]](../documentation/viewmodel/binding-members/onewaybind.md)                 | <mark style="color:$warning;">`[Bind(BindMode.OneWay)]`</mark>         |
| [\[TwoWayBind\]](../documentation/viewmodel/binding-members/twowaybind.md)                 | <mark style="color:$warning;">`[Bind(BindMode.TwoWay)]`</mark>         |
| [\[OneTimeBind\]](../documentation/viewmodel/binding-members/onetimebind.md)               | <mark style="color:$warning;">`[Bind(BindMode.OneTime)]`</mark>        |
| [\[OneWayToSourceBind\]](../documentation/viewmodel/binding-members/onewaytosourcebind.md) | <mark style="color:$warning;">`[Bind(BindMode.OneWayToSource)]`</mark> |

***

### <i class="fa-bell-on">:bell-on:</i> [\[BindAlso\]](../documentation/viewmodel/binding-members/bindalso.md)

To bind a read-only property that depends on other bound values, use the <mark style="color:$warning;">`[BindAlso(string propertyName)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    // Updates to FirstName notify components bound to
    // both FirstName and FullName.
    [BindAlso(nameof(FullName))]
    [OneWayBind] private string _firstName;
    
    // Updates to LastName notify components bound to
    // both LastName and FullName.
    [BindAlso(nameof(FullName))]
    [OneWayBind] private string _lastName;
    
    // Updates to FirstName or LastName notify components bound to FullName.
    private string FullName => _firstName + " " + _lastName;
}
```

***

### <i class="fa-unlock">:unlock:</i> [\[Access\]](../documentation/viewmodel/binding-members/access.md)

By default, all generated properties are fully private. To override access modifiers, mark the binding member with the <mark style="color:$warning;">`[Access]`</mark> attribute:

```csharp
[Access(Access.Public)] // Public access for get and set.
[OneWayBind] private string _name1;

[Access(Get = Access.Public)] // Public access for get only.
[OneWayBind] private string _name2;

[Access(Set = Access.Protected)] // Protected access for set only.
[OneWayBind] private string _name3;

[Access(Get = Access.Protected, Set = Access.Public)] // Combined access.
[OneWayBind] private string _name4;
```

***

### <i class="fa-microchip">:microchip:</i> [Handlers](../documentation/viewmodel/binding-members/handlers.md)

Binding members with <mark style="color:$primary;">**OneWay**</mark>, <mark style="color:$primary;">**TwoWay**</mark>, or <mark style="color:$primary;">**OneWayToSource**</mark> modes generate two handlers: one called before the value changes and one after.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneWayBind] private string _name;

    // Called before Name changes 
    partial void OnNameChanging(string oldValue, string newValue) { }
    
    // Called after Name changes
    partial void OnNameChanged(string newValue) { }
}
```

***

### <i class="fa-bullhorn">:bullhorn:</i> [NotifyAll](../documentation/viewmodel/binding-members/notifyall.md)

Every ViewModel generates a <mark style="color:$warning;">`NotifyAll`</mark> method that notifies all bound components to recheck their data.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [TwoWayBind] private string _name;
    [TwoWayBind] private int _age;
    
    public void Execute() =>
        NotifyAll();
}
```

***

<h2 align="center"><i class="fa-fingerprint">:fingerprint:</i> <a href="../documentation/generate-id.md#id-generation-in-viewmodel">Generate Id</a></h2>

By default, all binding members generate their <mark style="color:$warning;">`Id`</mark> according to the following rules:

* Removes the prefix (<mark style="color:$warning;">`_`</mark>, <mark style="color:$warning;">`_m`</mark>, <mark style="color:$warning;">`_s`</mark>), if present.
* Converts the first character to uppercase.

To specify a custom <mark style="color:$warning;">`Id`</mark> for a binding member, use the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [BindId("MyName")]
    [OneWayBind] private string _name;
}
```

***

<h2 align="center"><i class="fa-crosshairs">:crosshairs:</i> <a href="../documentation/viewmodel/custom-viewmodel-interface.md">Custom ViewModel Interface</a></h2>

To bind a View to a ViewModel directly through an interface, define the interface correctly:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : IMyViewModel
{
    [Bind] private int _age;
    [Bind] private string _name;
}

// The interface must implement IViewModel.
public interface IMyViewModel : IViewModel
{
    // The property type must be IBinderAdder.
    // The property name can be: _age, m_age, s_age, age, Age.
    // You can also use IReadOnlyBindableMember<int> or 
    // IReadOnlyValueBindableMember<int>.
    public IBinderAdder Age { get; }
    
    // Since the ID is overridden, the property name can be anything.
    // You can also use IReadOnlyBindableMember<string>
    // or IReadOnlyValueBindableMember<string>.
    [BindId("Name")]
    public IBinderAdder MyName { get; }
}
```

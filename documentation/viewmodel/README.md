---
icon: file-code
cover: ../../.gitbook/assets/image (71).png
coverY: 392.40128068303096
---

# ViewModel

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, the ViewModel is the connecting component between the View and the Model, responsible for managing the View's state, handling user interactions, and communicating with the Model.

To define a class as a ViewModel, it must implement the <mark style="color:$warning;">`IViewModel`</mark> interface.

* <mark style="color:orange;">Manually implementing this interface is not recommended, as it leads to excessive boilerplate code.</mark>
* <mark style="color:green;">The preferred approach is to use the</mark> <mark style="color:green;"></mark><mark style="color:green;">`[ViewModel]`</mark> <mark style="color:green;"></mark><mark style="color:green;">attribute, which enables automatic code generation via the Source Generator.</mark>

To define a ViewModel with code generation, apply the <mark style="color:$warning;">`[ViewModel]`</mark> attribute to the desired class:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel { }
```

{% hint style="warning" %}
## Requirements for Code Generation

* The class must have the <mark style="color:$warning;">`[ViewModel]`</mark> attribute.
* The class must be <mark style="color:$warning;">`partial`</mark>. If you forget to mark the class as <mark style="color:$warning;">`partial`</mark>, the code analyzer will flag the error and suggest a fix.
* The class cannot be nested at this time.
{% endhint %}

#### A ViewModel can contain:

1. [**Binding Members**](binding-members/) -  Constants and fields that automatically synchronize with the View.
2. [**Commands**](../commands/) - Methods invoked from the View (e.g., on button clicks).
3. [**Handlers**](binding-members/handlers.md) - Reactions to changes in binding members.

***

## <i class="fa-dna">:dna:</i> Inheritance

A class with the <mark style="color:$warning;">`[ViewModel]`</mark> attribute can inherit from any other class:

```csharp
using Aspid.MVVM;

public class SomeClass { }

[ViewModel]
public partial class MyViewModel : SomeClass { }
```

If inheriting from another class with the <mark style="color:$warning;">`[ViewModel]`</mark> attribute, the child class must also have the <mark style="color:$warning;">`[ViewModel]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class ParentViewModel { } 

[ViewModel]
public partial class ChildViewModel : ParentViewModel { } 
```

***

## <i class="fa-trowel-bricks">:trowel-bricks:</i> Base ViewModel Classes

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides two base classes that extend functionality:

1. [**MonoViewModel**](../unity/monoviewmodel.md) - A ViewModel implemented as a <mark style="color:$warning;">`MonoBehaviour`</mark>. Supports notifications for value changes via the Unity Inspector and serializes commands for Inspector interaction.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : MonoViewModel { } 
```

2. [**ScriptableViewModel**](../unity/scriptableviewmodel.md) - A ViewModel implemented as a <mark style="color:$warning;">`ScriptableObject`</mark>. Supports notifications for value changes via the Unity Inspector and serializes commands for Inspector interaction.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : ScriptableViewModel { }
```

{% hint style="info" %}
Inheriting from these classes is optional. In the future, they may receive additional debugging features.
{% endhint %}

***

## <i class="fa-broom">:broom:</i> Dispose

The <mark style="color:$warning;">`IViewModel`</mark> interface does not include <mark style="color:$warning;">`IDisposable`</mark>, but you can implement it manually:

```csharp
IViewModel viewModel = ...;

if (viewModel is IDisposable disposable)
    disposable.Dispose();
```

To simplify resource cleanup for ViewModels, an extension method is provided:

```csharp
IViewModel viewModel = ...;
viewModel.DisposeViewModel();
```

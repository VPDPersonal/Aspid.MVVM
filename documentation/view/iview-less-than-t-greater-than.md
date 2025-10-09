---
icon: crosshairs
---

# IView\<T>

<mark style="color:$primary;">**The IView\<T> interface**</mark> is used to specify the exact type of ViewModel that a View can work with. This avoids ID-based lookups and speeds up the initialization process, especially when dealing with a large number of fields and properties.

{% hint style="success" %}
#### Advantages

1. <mark style="color:$primary;">**No ID Lookup**</mark>: Binding occurs directly, bypassing <mark style="color:$warning;">`FindBindableMember(...)`</mark>, which is significantly faster for Views and ViewModels with many fields.
{% endhint %}

### <i class="fa-square-code">:square-code:</i> Example: View with a Specific ViewModel

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : IView<MyViewModel>
{
    // The View automatically binds to MyViewModel without ID lookup.
}
```

### <i class="fa-square-code">:square-code:</i> Example: View with a ViewModel Interface

```csharp
using Aspid.MVVM;

[View]
public partial class MyView : IView<IMyViewModel>
{
    // Works through an interface defining the ViewModel structure.
}
```

For more details, see the [Custom ViewModel Interface](../viewmodel/custom-viewmodel-interface.md) section.

### Potential Drawback

{% hint style="danger" %}
### Drawback

1. <mark style="color:$primary;">**Multiple ViewModel Types**</mark>: If a View can directly bind to multiple ViewModels, calling <mark style="color:$warning;">`Initialize`</mark> with <mark style="color:$warning;">`IViewModel`</mark> may result in multiple type checks (one for each ViewModel type). While this is unlikely, if it’s a concern, use the strongly-typed <mark style="color:$warning;">`Initialize<T>(T viewModel) where T : IViewModel`</mark> method instead.
{% endhint %}

#### Solution

If you know the exact type, call the strongly-typed method:

```csharp
MyViewModel viewModel = GetViewModel();
myView.Initialize(viewModel);
```

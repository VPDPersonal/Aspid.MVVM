---
icon: crosshairs
---

# Custom ViewModel Interface

A [View](../view/) can be bound to any ViewModel. This universal approach incurs overhead due to property lookup by ID. To avoid this, you can specify which ViewModel a View can bind to directly without ID lookup. For more details, see the [IView\<T>](../view/iview-less-than-t-greater-than.md) section.

If your View needs to bind to multiple ViewModels that do not share a common ancestor, a custom interface is required. This interface allows the View to understand how to bind directly to a ViewModel implementing it.

{% hint style="warning" %}
#### Interface Requirements

* The interface must inherit from <mark style="color:$warning;">`IViewModel`</mark>.
* All bindable properties must be of type <mark style="color:$warning;">`IBinderAdder`</mark> (suitable for all binding modes), <mark style="color:$warning;">`IReadOnlyBindableMember<T>`</mark> (not suitable for OneTime binding data), or <mark style="color:$warning;">`IReadOnlyValueBindableMember<T>`</mark> (suitable for all binding modes), where <mark style="color:$warning;">`T`</mark> is the data type.
* The property ID in the custom interface must match the ID of the bound member. For example, if the bound property's ID is <mark style="color:$warning;">`Age`</mark>, the property name must be <mark style="color:$warning;">`Age`</mark> or override the ID using the [\[BindId\]](../generate-id.md) attribute.
{% endhint %}

***

## <i class="fa-square-code">:square-code:</i> Example

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel : IMyViewModel
{
    [Bind] private int _age;
    [Bind] private string _name;
}

// Your interface must inherit from IViewModel.
public interface IMyViewModel : IViewModel
{
    // The property type must be IBinderAdder.
    // The property name can be: _age, m_age, s_age, age, Age.
    // You can also use IReadOnlyBindableMember<int>
    // or IReadOnlyValueBindableMember<int>.
    public IBinderAdder Age { get; }
    
    // Since the ID for this property is overridden, the property name can be anything.
    // You can also use IReadOnlyBindableMember<string>
    // or IReadOnlyValueBindableMember<string>.
    [BindId("Name")]
    public IBinderAdder MyName { get; }
}
```

This is a powerful tool for building a clean architecture and loosely coupled system. It is especially useful in team environments where ViewModels are developed independently of Views.

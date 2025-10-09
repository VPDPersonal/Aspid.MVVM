---
icon: microchip
---

# Handlers

Every bound property generated from [\[Bind\]](bind.md), [\[OneWayBind\]](onewaybind.md), [\[TwoWayBind\]](twowaybind.md), [\[OneWayToSourceBind\]](onewaytosourcebind.md) automatically supports two event handlers:

* <mark style="color:$warning;">`On[PropertyName]Changing((PropertyType) oldValue, (PropertyType) newValue)`</mark> - Called before the value changes.
* <mark style="color:$warning;">`On(PropertyName)Changed((PropertyType) value)`</mark> - Called after the value changes.

These are implemented as <mark style="color:$warning;">`partial`</mark> methods, which you can define manually, and they will be invoked at the appropriate time.

***

## <i class="fa-square-code">:square-code:</i> Example

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private string _name;

    partial void OnNameChanging(string oldValue, string newValue)
    {
        // Name is changing: oldValue ➡ newValue
    }

    partial void OnNameChanged(string newValue)
    {
        // Name successfully changed to: newValue
    }
}
```

{% hint style="warning" %}
#### Notes

* Works only for the following binding modes: OneWay, TwoWay, OneWayToSource.
* Does not work for OneTime bindings.
* Does not work with the [\[BindAlso\]](bindalso.md) attribute.&#x20;
* Method names are case-sensitive and must follow the pattern:
  * <mark style="color:$warning;">`On[PropertyName]Changing((PropertyType) oldValue, (PropertyType) newValue)`</mark>
  * <mark style="color:$warning;">`On(PropertyName)Changed((PropertyType) value)`</mark>
{% endhint %}

#### Why Use Handlers?

* Perform validation or preparation logic before a value changes.
* React to changes and trigger side effects (e.g., playing a sound).

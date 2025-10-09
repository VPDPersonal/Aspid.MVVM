---
icon: arrow-right-long
---

# \[OneWayBind]

<mark style="color:$primary;">**The \[OneWayBind] attribute**</mark> is an alternative to using [\[Bind(BindMode.OneWay)\]](bind.md#bind-bindmode-mode). It generates a property that supports only OneWay binding (updating the View when the ViewModel changes).

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneWayBind] private int _age;
}
```

{% hint style="warning" %}
#### Notes

The <mark style="color:$warning;">`[OneWayBind]`</mark> attribute does not work with constants or <mark style="color:$warning;">`readonly`</mark> fields.
{% endhint %}


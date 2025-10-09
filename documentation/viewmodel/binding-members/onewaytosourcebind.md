---
icon: arrow-left-long
---

# \[OneWayToSourceBind]

<mark style="color:$primary;">**The \[OneWayToSourceBind] attribute**</mark> is an alternative to using [\[Bind(BindMode.OneWayToSource)\]](bind.md#bind-bindmode-mode). It generates a property that supports only OneWayToSource binding (updating the ViewModel when the View changes).

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneWayToSourceBind] private int _age;
}
```

{% hint style="warning" %}
#### Notes

The <mark style="color:$warning;">`[OneWayToSourceBind]`</mark> attribute does not work with constants or <mark style="color:$warning;">`readonly`</mark> fields.
{% endhint %}

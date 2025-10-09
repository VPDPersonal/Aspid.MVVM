---
icon: arrow-right-arrow-left
---

# \[TwoWayBind]

<mark style="color:$primary;">**The \[TwoWayBind] attribute**</mark> is an alternative to using [\[Bind(BindMode.TwoWay)\]](bind.md#bind-bindmode-mode). It generates a property that supports TwoWay binding (bidirectional updates between View and ViewModel).

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [TwoWayBind] private int _age;
}
```

{% hint style="warning" %}
#### Notes

The <mark style="color:$warning;">`[TwoWayBind]`</mark> attribute does not work with constants or <mark style="color:$warning;">`readonly`</mark> fields.
{% endhint %}

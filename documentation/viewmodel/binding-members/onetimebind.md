---
icon: arrow-right-long
---

# \[OneTimeBind]

<mark style="color:$primary;">**The \[OneTimeBind] attribute**</mark> is an alternative to using [\[Bind(BindMode.OneTime)\]](bind.md#bind-bindmode-mode). It generates a property that supports only OneTime binding (a single update to the View during initialization).

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private const int Age = 42;

    [OneTimeBind] private int _age1;
    [OneTimeBind] private readonly int _age2;
}
```

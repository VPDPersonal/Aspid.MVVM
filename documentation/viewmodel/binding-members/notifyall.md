---
icon: bullhorn
---

# NotifyAll

Every ViewModel generates a <mark style="color:$warning;">`NotifyAll`</mark> method, which notifies all bound components to recheck their data.

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

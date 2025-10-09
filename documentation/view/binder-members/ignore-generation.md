---
icon: link-horizontal-slash
---

# Ignore generation

To exclude a class implementing <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark> from code generation, you can use the <mark style="color:$warning;">`[BindIgnore]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;
​
[View]
public partial class MyView : MonoView
{
    [BindIgnore]
    [SerializeField] private MonoBinder[] _binders;
    
    [BindIgnore]
    [SerializeField] private MonoView _view;
}
```

Additionally, the generator attempts to prevent cases where a binder’s <mark style="color:$warning;">`Bind`</mark> method would be called twice:

```csharp
using Aspid.MVVM;
using UnityEngine;
​
[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoBinder _binder;
    
    // Excluded from generation to prevent the binder
    // from being initialized twice.
    private MonoBinder Binder1 => _binder;
}
```

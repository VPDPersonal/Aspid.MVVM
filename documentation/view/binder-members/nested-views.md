---
icon: diagram-nested
---

# Nested Views

To support nested Views, the Source Generator analyzes types that implement the <mark style="color:$warning;">`IView`</mark> interface in addition to those implementing the <mark style="color:$warning;">`IBinder`</mark> interface.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class ParentViewModel
{
    [OneWayBind] private IViewModel _child = new ChildViewModel();
}

[ViewModel]
public partial class ChildViewModel { } 

[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoView _child;
}
```

This approach enables the creation of complex hierarchical user interface structures, where each nested View can be automatically bound to its corresponding ViewModel, making the architecture more modular and scalable.

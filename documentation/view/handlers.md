---
icon: microchip
---

# Handlers

<mark style="color:$primary;">**Aspid.MVVM**</mark> automatically generates <mark style="color:$warning;">`partial`</mark> methods for Views, allowing developers to inject custom behavior at each stage of the View's lifecycle, from initialization to unbinding.

These methods are optional to implement—if you don’t define them, they won’t be called. However, they provide flexibility for customizing View behavior.

```csharp
using Aspid.MVVM;

[View]
public partial class MyView
{
    // Called before View initialization.
    partial void OnInitializingInternal(IViewModel viewModel) { }
    
    // Called after View initialization.
    partial void OnInitializedInternal(IViewModel viewModel) { }
    
    // Called before View deinitialization.
    partial void OnDeinitializingInternal() { }
    
    // Called after View deinitialization.
    partial void OnDeinitializedInternal() { }
}
```

If the View contains:

* Properties implementing <mark style="color:$warning;">`IBinder<T>`</mark> or <mark style="color:$warning;">`IView<T>`</mark>
* Fields implementing <mark style="color:$warning;">`IView`</mark>
* Fields or properties with the <mark style="color:$warning;">`[AsBinder]`</mark> attribute

Then the following additional methods are generated:

```csharp
using Aspid.MVVM;

[View]
public partial class MyView
{
    // Called before caching binders
    partial void OnInstantiatingBinders();
    
    // Called after caching binders
    partial void OnInstantiatedBinders();
}
```

```
Initialize(viewModel)
│
├── OnInitializingInternal(viewModel)        ← Before initialization
├── InstantiateBinders()
│   ├── OnInstantiatingBinders()             ← Before creating binders
│   ├── [создание и кэширование биндров]
│   └── OnInstantiatedBinders()              ← After creating binders
├── [BindSafely(...) для всех IBinder/IView]
└── OnInitializedInternal(viewModel)         ← After full initialization

Deinitialize()
│
├── OnDeinitializingInternal()               ← Before unbinding
├── [UnbindSafely(...) для всех IBinder/IView]
└── OnDeinitializedInternal()                ← After full deinitialization
```

---
icon: input-text
layout:
  width: default
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: true
  pagination:
    visible: true
  metadata:
    visible: true
---

# Field

In all View classes marked with the <mark style="color:$warning;">`[View]`</mark> attribute, the Source Generator analyzes all fields that implement the <mark style="color:$warning;">`IBinder`</mark> interface.

This enables automatic binding of View components to the corresponding data in the ViewModel.

```csharp
using Aspid.MVVM;
using UnityEngine;
​
[View]
public partial class MyView : MonoView
{
    // The field ID must match the ID of a field in the ViewModel
    // for binding to succeed.
    [SerializeField] private MonoBinder _name; 
}
```

Only one <mark style="color:$warning;">`MonoBinder`</mark> descendant can be assigned to the <mark style="color:$warning;">`_name`</mark> field. To support multiple binders, you can declare an array:

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    [SerializeField] private MonoBinder[] _name; 
}
```

#### <i class="fa-check">:check:</i> Binder Validation – RequireBinder

To ensure that a field is bound to a binder that supports binding to a specific type (e.g., <mark style="color:$warning;">`string`</mark>) and avoid runtime errors, mark the field with the <mark style="color:$warning;">`[RequireBinder(Type type)]`</mark> attribute:

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    // Only binders that support binding to string can be assigned:
    // IBinder<string> or IReverseBinder<string> or IAnyBinder.
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name; 
}
```

#### <i class="fa-gear-code">:gear-code:</i> Abstract Example of Generated Code

{% code fullWidth="false" %}
```csharp
public partial class MyView : IView
{
    private ViewBinder __childViewCachedBinder;
    
    public IViewModel ViewModel { get; private set; }
    
    public void Initialize(IViewModel viewModel)
    {
        if (viewModel is null) 
            throw new ArgumentNullException(nameof(viewModel));
        
        if (ViewModel is not null) 
            throw new InvalidOperationException("View is already initialized.");
        
        ViewModel = viewModel;
        InitializeInternal(viewModel);
    }
    
    protected virtual void InitializeInternal(IViewModel viewModel)
    {
        OnInitializingInternal(viewModel);
        InstantiateBinders();
                
        _name.BindSafely(viewModel.FindBindableMember(new(Ids.Name)));
                
        OnInitializedInternal(viewModel);
    }
    
    partial void OnInitializingInternal(IViewModel viewModel);
    
    partial void OnInitializedInternal(IViewModel viewModel);
        
    public void Deinitialize()
    {
        if (ViewModel is null) return;
		
	DeinitializeInternal();
	ViewModel = null;
    }
    
    protected virtual void DeinitializeInternal()
    {
        OnDeinitializingInternal();
                
        _name.UnbindSafely();
                
        OnDeinitializedInternal();
    }
    
    partial void OnDeinitializingInternal();
    
    partial void OnDeinitializedInternal();
}
```
{% endcode %}

{% hint style="info" %}
For more details on partial methods, see the [Handlers](../handlers.md) section.
{% endhint %}

{% hint style="info" %}
#### Explanation

1. For all fields implementing <mark style="color:$warning;">`IBinder`</mark>, the <mark style="color:$warning;">`BindSafely`</mark> method is called.
2. Binders serve as the connecting element. Typically, their <mark style="color:$warning;">`Bind`</mark> method is called during binding, and their <mark style="color:$warning;">`Unbind`</mark> method is called during unbinding
{% endhint %}

---
icon: bell-on
---

# \[BindAlso]

<mark style="color:$primary;">**The \[BindAlso] attribute**</mark> is used when a computed property depends on multiple other fields, and you want the View to update automatically when those fields change.

It generates additional notification logic that informs the View when the dependent property changes, even if it does not have its own setter.

***

<h2 align="center"><i class="fa-square-code">:square-code:</i> Example</h2>

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [BindAlso(nameof(FullName))]
    [Bind] private string _name;
    
    [BindAlso(nameof(FullName))]
    [Bind] private string _family;

    private string FullName => $"{Name} {Family}";
}
```

***

### <i class="fa-gear-code">:gear-code:</i> Abstract Example of Generated Code

```csharp
public partial class MyViewModel
{
    public event Action<string> NameChanged;
    public event Action<string> FamilyChanged;
    public event Action<string> FullNameChanged;

    private string Name
    {
        get => _name;
        set => SetName(value);
    }

    private string Family
    {
        get => _family;
        set => SetFamily(value);
    }

    private void SetName(string value)
    {
        if (_name == value) return;
        
        _name = value;
        NameChanged?.Invoke(_name);
        FullNameChanged?.Invoke(FullName);
    }

    private void SetFamily(string value)
    {
        if (_family == value) return;
        
        _family = value;
        FamilyChanged?.Invoke(_family);
        FullNameChanged?.Invoke(FullName);
    }
}
```

{% hint style="info" %}
#### How It Works

* The <mark style="color:$warning;">`[BindAlso(nameof(FullName))]`</mark> attribute instructs the generator: "When this field changes, also trigger the <mark style="color:$warning;">`FullNameChanged`</mark> notification."
* All View elements bound to <mark style="color:$warning;">`FullName`</mark>, will automatically update when either <mark style="color:$warning;">`Name`</mark> or <mark style="color:$warning;">`Family`</mark> changes.
{% endhint %}

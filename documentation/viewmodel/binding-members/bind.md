---
icon: arrow-right-arrow-left
---

# \[Bind]

<mark style="color:$primary;">**The \[Bind] attribute**</mark> is a versatile way to mark a field or constant in a ViewModel for generating a bound property.

It allows the Source Generator to automatically determine the maximum allowed binding mode based on the field type

| Field Type                                      | Binding Mode |
| ----------------------------------------------- | ------------ |
| <mark style="color:$warning;">`conts`</mark>    | OneTime      |
| <mark style="color:$warning;">`readonly`</mark> | OneTime      |
| Regular field                                   | TwoWay       |

{% hint style="warning" %}
#### Requirements

1. The class must be marked with the [\[ViewModel\]](../#generaciya-viewmodel-s-pomoshyu-source-generator) attribute and be <mark style="color:$warning;">`partial`</mark>.
2. Fields must follow one of these naming formats:
   1. <mark style="color:$warning;">`_fieldName`</mark>
   2. <mark style="color:$warning;">`m_fieldName`</mark>
   3. <mark style="color:$warning;">`s_filedName`</mark>
   4. <mark style="color:$warning;">`fieldName`</mark>
3. Constants can have any name.
{% endhint %}

{% hint style="danger" %}
#### Current Limitations

Only the following are supported:

1. Fields
2. Constants

Properties and indexers are not supported but are planned for future versions. For now, the [\[BindAlso\]](bindalso.md) attribute can be used to bind properties.
{% endhint %}

***

<h2 align="center"><i class="fa-square-code">:square-code:</i> Example</h2>

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private const int ConstValue = 1;
    
    [Bind] private int _value;
    [Bind] private readonly int _readonlyValue;
}
```

Depending on the field type, a property with the corresponding binding mode will be generated.

***

### <i class="fa-gear-code">:gear-code:</i> Abstract Example of Generated Code

```csharp
public partial class MyViewModel : IViewModel
{
    public IReadOnlyValueBindableMember<int> ConstValueBindable;
    public IReadOnlyBindableMember<int> ValueBindable;
    public IReadOnlyValueBindableMember<int> ReadonlyValueBindable;
    
    private int Value
    {
        get => _value;
        set => SetValue();
    }
    
    private int ReadonlyValue => _readonlyValue;
    
    private void SetValue2(int value)
    {
        if (this._value == value) return;
        
        OnValueChanging(this._value, value);
        this._value = value;
        ValueChanged?.Invoke(value);
        OnValueChanged(value);
    }
    
    partial void OnValueChanging(int oldValue, int newValue);
    
    partial void OnValueChanged(int value);
}
```

{% hint style="info" %}
* By default, all generated properties are private. To override the access level, see the [\[Access\]](access.md) section
* For more details on partial methods, see the [Handlers](handlers.md) section.
{% endhint %}

***

<h2 align="center"><i class="fa-gear-code">:gear-code:</i> Using the Generated Property</h2>

Within the ViewModel, you must only use the generated property (or method) for binding to work correctly; otherwise, the code analyzer will report an error.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private int _age;
    
    public MyViewModel(int age)
    {
        // In the constructor, you can use the field directly:
        _age = age;
    }
    
    // Other Code
    // ...
    
    private void UpdateAge(int age)
    {
        // Elsewhere, use only the generated property:
        Age = age; // Ок
        // _age = age; Triggers an analyzer error
    }
}
```

{% hint style="warning" %}
#### Notes

Direct assignment to the field is allowed only in the constructor. In all other cases, use the generated property, or the analyzer will report an error.
{% endhint %}

***

### <i class="fa-signature">:signature:</i> Generated Property Name

{% hint style="info" %}
#### The transformation algorithm:

1. The prefix (<mark style="color:$warning;">`_`</mark>_,_ <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>) is removed.
2. The first letter is capitalized.
{% endhint %}

| Field                                           | Property                                      |
| ----------------------------------------------- | --------------------------------------------- |
| <mark style="color:$warning;">`_age`</mark>     | <mark style="color:$warning;">`Age`</mark>    |
| <mark style="color:$warning;">`m_health`</mark> | <mark style="color:$warning;">`Health`</mark> |
| <mark style="color:$warning;">`s_count`</mark>  | <mark style="color:$warning;">`Count`</mark>  |
| <mark style="color:$warning;">`score`</mark>    | <mark style="color:$warning;">`Score`</mark>  |

***

<h2 align="center"><i class="fa-sliders-up">:sliders-up:</i> Overriding the Binding Mode</h2>

If the automatically selected binding mode is unsuitable, you can override it using the <mark style="color:$warning;">`[Bind(BindMode mode)]`</mark> constructor parameter.

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    // For constants, only OneTime mode can be specified.
    [BindMode(BindMode.OneTime)] private const float Pi = 3.14f;

    [BindMode(BindMode.OneWay)] private string _name1;
    [BindMode(BindMode.TwoWay)] private string _name2;
    [BindMode(BindMode.OneTime)] private string _name3;
    [BindMode(BindMode.OneWayToSource)] private string _name4;
    
    // For readonly fields, only OneTime mode can be specified.
    [BindMode(BindMode.OneTime)] private readonly string _name5;
}
```

{% hint style="warning" %}
#### Notes

Constants and <mark style="color:$warning;">`readonly`</mark> fields can only use the OneTime binding mode.
{% endhint %}

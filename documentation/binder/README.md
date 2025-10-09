---
icon: link-horizontal
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

# Binder

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, the Binder component plays a key role in the data binding system, facilitating the transfer of values from the ViewModel to the View (with support for TwoWay binding).

Each Binder implements the <mark style="color:$warning;">`IBinder<T>`</mark> and/or <mark style="color:$warning;">`IReverseBinder<T>`</mark> interfaces, where <mark style="color:$warning;">`T`</mark> is the type of data transferred between the View and ViewModel.

#### Binder Interfaces

<table><thead><tr><th width="200.56640625">Interface</th><th>Description</th></tr></thead><tbody><tr><td><mark style="color:$warning;"><code>IBinder</code></mark></td><td>The base interface for all binders. Contains two methods: <mark style="color:$warning;"><code>Bind(IBinderAdder)</code></mark> and <mark style="color:$warning;"><code>Unbind()</code></mark>.</td></tr><tr><td><mark style="color:$warning;"><code>IBinder&#x3C;T></code></mark></td><td>An interface that provides one-way binding from ViewModel to View. Values are received via the <mark style="color:$warning;"><code>SetValue(T value)</code></mark> method.</td></tr><tr><td><mark style="color:$warning;"><code>IAnyBinder</code></mark></td><td>An interface that provides one-way binding from ViewModel to View. Values are received via the <mark style="color:$warning;"><code>SetValue&#x3C;T>(T value)</code></mark> method, accepting any data type, unlike <mark style="color:$warning;"><code>IBinder&#x3C;T></code></mark>.</td></tr><tr><td><mark style="color:$warning;"><code>IReverseBinder&#x3C;T></code></mark></td><td>An interface that provides reverse binding from View to ViewModel by invoking the <mark style="color:$warning;"><code>ValueChanged?.Invoke(value)</code></mark> event.</td></tr></tbody></table>

***

### <i class="fa-angle-right">:angle-right:</i> Simple Binder **- IBinder\<T>**

To create a custom binder, inherit from the <mark style="color:$warning;">`Binder`</mark> or <mark style="color:$warning;">`MonoBinder`</mark> class and implement the <mark style="color:$warning;">`IBinder<T>`</mark> interface, where <mark style="color:$warning;">`T`</mark> is the type used for binding.

```csharp
using Aspid.MVVM;
using UnityEngine;

// MonoBinder implements IBinder and inherits from MonoBehaviour.
public class TextMonoBinder : MonoBinder, IBinder<string>
{
    [SerializeField] private TMP_Text _text;

    // Called for OneWay, TwoWay, or OneTime binding.
    public void SetValue(string value) =>
        _text.text = value;
}
```

In this example, <mark style="color:$warning;">`TextMonoBinder`</mark> updates a Text element on the screen when the string in the ViewModel changes.

{% hint style="info" %}
* <mark style="color:$primary;">**Binder**</mark>: The base class that implements the <mark style="color:$warning;">`Bind`</mark> and <mark style="color:$warning;">`Unbind`</mark> methods.
* [**MonoBinder**](../unity/monobinder.md): The base class that implements the <mark style="color:$warning;">`Bind`</mark> and <mark style="color:$warning;">`Unbind`</mark> methods and inherits from <mark style="color:$warning;">`MonoBehaviour`</mark>.
{% endhint %}

***

### <i class="fa-chevrons-right">:chevrons-right:</i> Multiple IBinder\<T> Implementations

The <mark style="color:$warning;">`TextMonoBinder`</mark> above supports binding only for <mark style="color:$warning;">`string`</mark>. To support additional types, such as <mark style="color:$warning;">`float`</mark>, implement another <mark style="color:$warning;">`IBinder<float>`</mark> interface:

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
using System.Globalization;

public class TextMonoBinder : MonoBinder, IBinder<string>, IBinder<float>
{
    [SerializeField] private TMP_Text _text;

    public void SetValue(float value) =>
        SetValue(value.ToString(CultureInfo.InvariantCulture));

    public void SetValue(string value) =>
        _text.text = value;
}
```

This is useful when a single UI component needs to handle multiple data types.

***

### <i class="fa-circle">:circle:</i> Universal **Binder - IAnyBinder**

To create a binder that works with any data type, implement the <mark style="color:$warning;">`IAnyBinder`</mark> interface:

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;

public class TextMonoBinder : MonoBinder, IAnyBinder
{
    [SerializeField] private TMP_Text _text;

    public void SetValue<T>(T value) =>
        _text.text = value.ToString();
}
```

To override behavior for a specific data type, use a combined approach:

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
using System.Globalization;

public class TextMonoBinder : MonoBinder, IBinder<float>, IAnyBinder
{
    [SerializeField] private TMP_Text _text;
    
    // Called if the incoming value is not float.
    public void SetValue<T>(T value) =>
        _text.text = value.ToString();
        
    // Called if the incoming value is float.
    public void SetValue(float value) =>
        _text.text = value.ToString(CultureInfo.InvariantCulture);
}
```

{% hint style="warning" %}
#### Notes

* The <mark style="color:$warning;">`SetValue()`</mark> method is always called by the framework when data is updated from the ViewModel.
{% endhint %}

***

### <i class="fa-chevron-left">:chevron-left:</i> Reverse Binding - IReverseBinder\<T>

To enable reverse binding (TwoWay or OneWayToSource), follow these two steps:

1. Implement the <mark style="color:$warning;">`IReverseBinder<T>`</mark> interface, where <mark style="color:$warning;">`T`</mark> is the type used for binding.
2. Mark the binder with the <mark style="color:$warning;">`[BindModeOverride(BindMode.OneWayToSource)]`</mark>  attribute, as the Unity Inspector by default only allows OneWay and OneTime binding for <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark>.

```csharp
using System;
using Aspid.MVVM;

// Only OneWayToSource mode can be selected in the Inspector.
[BindModeOverride(BindMode.OneWayToSource)]
public class InputFieldMonoBinder : MonoBinder, IReverseBinder<string>
{
    public event Action<string> ValueChanged;
    
    [SerializeField] private TMP_InputField _inputField;
    
    // Called after binding.
    protected override void OnBound()
    {
        _inputField.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(_inputField.text);       
    }
        
    // Called after unbinding.
    protected override void OnUnbound() =>
        _inputField.onValueChanged.RemoveListener(OnValueChanged);
         
    private void OnValueChanged(string value) =>
        ValueChanged?.Invoke(value);
}
```

{% hint style="warning" %}
#### Notes

* <mark style="color:$warning;">`ValueChanged`</mark> is triggered when data changes in the View component (for reverse binding).
* The binding mode must be <mark style="color:$warning;">`BindMode.TwoWay`</mark> or <mark style="color:$warning;">`BindMode.OneWayToSource`</mark> when implementing <mark style="color:$warning;">`IReverseBinder`</mark> for reverse binding to work.
{% endhint %}

***

<h2 align="center"><i class="fa-microchip">:microchip:</i> Handlers</h2>

The base classes <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark> provide several virtual methods:

1. <mark style="color:$warning;">`protected virtual void OnBinding()`</mark> - Called before binding.
2. <mark style="color:$warning;">`protected virtual void OnBound()`</mark> - Called after binding.
3. <mark style="color:$warning;">`protected virtual void OnUnbinding()`</mark> - Called before unbinding.
4. <mark style="color:$warning;">`protected virtual void OnUnbound()`</mark> - Called after unbinding.

***

<h2 align="center"><i class="fa-hexagon-plus">:hexagon-plus:</i> Additional Notes</h2>

The base classes <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark> have a <mark style="color:$warning;">`public virtual bool IsBind => true`</mark> property, which you can override in derived classes. If <mark style="color:$warning;">`IsBind`</mark> returns <mark style="color:$warning;">`false`</mark>, binding will not occur.

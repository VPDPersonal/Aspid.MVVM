---
icon: link-horizontal
cover: ../.gitbook/assets/Aspid.MVVM Preview Binders.png
coverY: 0
---

# Overview - Binders

<h2 align="center"><i class="fa-link-simple">:link-simple:</i> <a href="../documentation/binder/">Binder</a></h2>

Every binder must implement the <mark style="color:$warning;">`IBinder`</mark> interface. There are two base implementations:

1. <mark style="color:$primary;">**Binder**</mark>: For standard C# classes.
2. <mark style="color:$primary;">**MonoBinder**</mark>: For classes inheriting from <mark style="color:$warning;">`MonoBehaviour`</mark>. Includes debugging mechanisms and is currently compatible only with Views inheriting from <mark style="color:$warning;">`MonoView`</mark>.

***

### <i class="fa-angle-right">:angle-right:</i> [Simple Binder - IBinder\<T>](../documentation/binder/#simple-binder-ibinder-less-than-t-greater-than)

To create a custom binder, inherit from the <mark style="color:$warning;">`Binder`</mark> or <mark style="color:$warning;">`MonoBinder`</mark> class and implement the <mark style="color:$warning;">`IBinder<T>`</mark> interface, where <mark style="color:$warning;">`T`</mark> is the type used for binding.

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;

public class TextMonoBinder : MonoBinder, IBinder<string>
{
    [SerializeField] private TMP_Text _text;

    // Called for OneWay, TwoWay, or OneTime binding.
    public void SetValue(string value) =>
        _text.text = value;
}
```

***

### <i class="fa-chevrons-right">:chevrons-right:</i> [Multiple IBinder\<T> Implementations](../documentation/binder/#multiple-ibinder-less-than-t-greater-than-implementations)

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

***

### <i class="fa-circle">:circle:</i> [Universal Binder – IAnyBinder](../documentation/binder/#universal-binder-ianybinder)

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

***

### <i class="fa-chevrons-left">:chevrons-left:</i> [Reverse Binding – IReverseBinder\<T>](../documentation/binder/#reverse-binding-ireversebinder-less-than-t-greater-than)

To enable reverse binding (<mark style="color:$primary;">**TwoWay**</mark> or <mark style="color:$primary;">**OneWayToSource**</mark>), follow these two steps:

1. Implement the <mark style="color:$warning;">`IReverseBinder<T>`</mark> interface, where T is the type used for binding.
2. Mark the binder with the <mark style="color:$warning;">`[BindModeOverride(BindMode.OneWayToSource)]`</mark> attribute, as the Unity Inspector by default only allows <mark style="color:$primary;">**OneWay**</mark> and <mark style="color:$primary;">**OneTime**</mark> binding for <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark>.

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

***

<h2 align="center"><i class="fa-microchip">:microchip:</i> <a href="../documentation/binder/#handlers">Handlers</a></h2>

The base classes <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark> provide several virtual methods:

1. <mark style="color:$warning;">`protected virtual void OnBinding()`</mark> - Called before binding.
2. <mark style="color:$warning;">`protected virtual void OnBound()`</mark> - Called after binding.
3. <mark style="color:$warning;">`protected virtual void OnUnbinding()`</mark> - Called before unbinding.
4. <mark style="color:$warning;">`protected virtual void OnUnbound()`</mark> - Called after unbinding.

***

<h2 align="center"><i class="fa-octagon-plus">:octagon-plus:</i> <a href="../documentation/binder/#additional-notes">Additional Notes</a></h2>

The base classes <mark style="color:$warning;">`Binder`</mark> and <mark style="color:$warning;">`MonoBinder`</mark> have a <mark style="color:$warning;">`public virtual bool IsBind => true`</mark> property, which you can override in derived classes. If <mark style="color:$warning;">`IsBind`</mark> returns <mark style="color:$warning;">`false`</mark>, binding will not occur.

***

<h2 align="center"><i class="fa-list-dropdown">:list-dropdown:</i> <a href="../documentation/binder/bind-mode.md">Bind Mode</a></h2>

By default, binders inheriting from <mark style="color:$warning;">`Binder`</mark> or <mark style="color:$warning;">`MonoBinder`</mark> can only be set to <mark style="color:$primary;">**OneWay**</mark> or <mark style="color:$primary;">**OneTime**</mark> binding modes in the Unity Inspector. To override the available binding modes, mark the binder with the <mark style="color:$warning;">`[BindModeOverride]`</mark> attribute:

```csharp
// Allows any binding mode to be selected in the Inspector.
[BindModeOverride(IsAll = true)]

// Allows OneWay and OneTime binding modes to be selected in the Inspector.
[BindModeOverride(IsOne = true)]

// Allows TwoWay and OneWayToSource binding modes to be selected in the Inspector.
[BindModeOverride(IsTwo = true)]

// Allows only the OneTime binding mode to be selected in the Inspector.
[BindModeOverride(BindMode.OneTime)]
```

***

<h2 align="center"><i class="fa-bug">:bug:</i> <a href="../documentation/unity/monobinder.md#debug">Debug</a></h2>

Each <mark style="color:$warning;">`MonoBinder`</mark> visually indicates whether it is bound to a field:

| Valid Binder                                                                                              | Invalid Binder                                                                                            |
| --------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------- |
| <div><figure><img src="../.gitbook/assets/image (54).png" alt=""><figcaption></figcaption></figure></div> | <div><figure><img src="../.gitbook/assets/image (55).png" alt=""><figcaption></figcaption></figure></div> |

### <i class="fa-square-terminal">:square-terminal:</i> [\[BinderLog\]](../documentation/binder/binderlog.md)

To log all received bound values, follow these two steps:

1. Define the binder class as <mark style="color:$warning;">`partial`</mark>.
2. Add the <mark style="color:$warning;">`[BinderLog]`</mark> attribute to the <mark style="color:$warning;">`SetValue`</mark> method.

This adds a toggle in the Unity Inspector to enable logging, with logs displayed directly on the component.

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
using System.Globalization;

public partial class TextMonoBinder : MonoBinder, IBinder<string>, IBinder<float>
{
    [SerializeField] private TMP_Text _text;

    [BinderLog]
    public void SetValue(float value) =>
        SetValue(value.ToString(CultureInfo.InvariantCulture));

    [BinderLog]
    public void SetValue(string value) =>
        _text.text = value;
}
```

<figure><img src="../.gitbook/assets/image (56).png" alt=""><figcaption></figcaption></figure>

<h2 align="center"><a href="../documentation/unity/monobinder.md#additional-notes">Additional Notes - MonoBinder</a></h2>

All binders in the <mark style="color:$warning;">`Aspid.MVVM.StarterKit.Unity`</mark> package support addition through the Unity context menu:

<figure><img src="../.gitbook/assets/image (43).png" alt=""><figcaption><p>Via Add Component.</p></figcaption></figure>

<figure><img src="../.gitbook/assets/image (44).png" alt=""><figcaption><p>Via the context menu of a component’s property.</p></figcaption></figure>

<figure><img src="../.gitbook/assets/image (62).png" alt=""><figcaption><p>Via the context menu of the component itself.</p></figcaption></figure>

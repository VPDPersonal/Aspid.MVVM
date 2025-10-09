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

# MonoBinder

<mark style="color:$primary;">**MonoBinder**</mark> is the base class for all binders in Unity that must be <mark style="color:$warning;">`MonoBehaviour`</mark>. It works in conjunction with <mark style="color:$warning;">`MonoView`</mark>, providing a straightforward way to connect scene View components to data from a ViewModel.

***

## <i class="fa-angle-right">:angle-right:</i> Simple Binder

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
​
public class TextMonoBinder : MonoBinder, IBinder<string>
{
    [SerializeField] private TMP_Text _text;
​
    public void SetValue(string value) =>
        _text.text = value;
}
```

***

## <i class="fa-chevrons-right">:chevrons-right:</i> Multiple IBinder\<T> Implementations

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
​
public class TextMonoBinder : MonoBinder, IBinder<string>, IBinder<int>
{
    [SerializeField] private TMP_Text _text;
​
    public void SetValue(int value) =>
        _text.text = value.ToString();

    public void SetValue(string value) =>
        _text.text = value;
}
```

***

## <i class="fa-circle">:circle:</i> Universal **Binder**

```csharp
using TMPro;
using Aspid.MVVM;
using UnityEngine;
​
public class TextMonoBinder : MonoBinder, IAnyBinder
{
    [SerializeField] private TMP_Text _text;

    public void SetValue<T>(T value) =>
        _text.text = value.ToString();
}
```

***

## <i class="fa-chevron-left">:chevron-left:</i> Reverse Binding - IReverseBinder\<T>

{% code fullWidth="false" %}
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
{% endcode %}

For more details on reverse binding, see the [**Reverse Binding - IReverseBinder\<T>**](../binder/#reverse-binding-ireversebinder-less-than-t-greater-than) section.

***

## <i class="fa-plug-circle-bolt">:plug-circle-bolt:</i> Integration in Unity Inspector

When you add a <mark style="color:$warning;">`MonoBinder`</mark> to a scene:

* A dropdown list of available Views appears in the Inspector.
* After selecting a View, a list of available IDs defined in the View is displayed.
* If the binder’s type is incompatible (e.g., <mark style="color:$warning;">`IBinder<int>`</mark> for a <mark style="color:$warning;">`string`</mark> binding), it will not appear in the list. This is enforced by the <mark style="color:$warning;">`[RequireBinder]`</mark> attribute in the View.

This prevents binding errors during setup without needing to run the scene.

***

## <i class="fa-bug">:bug:</i> Debug

| Valid Binder                                                                                                 | Invalid Binder                                                                                               |
| ------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------ |
| <div><figure><img src="../../.gitbook/assets/image (54).png" alt=""><figcaption></figcaption></figure></div> | <div><figure><img src="../../.gitbook/assets/image (55).png" alt=""><figcaption></figcaption></figure></div> |

***

## <i class="fa-circle-plus">:circle-plus:</i> Additional Notes

All binders in the <mark style="color:$warning;">`Aspid.MVVM.StarterKit.Unity`</mark> package support addition through the Unity context menu:

<figure><img src="../../.gitbook/assets/image (59).png" alt=""><figcaption><p>Via Add Component.</p></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (60).png" alt=""><figcaption><p>Via the context menu of a component’s property.</p></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (61).png" alt=""><figcaption><p>Via the context menu of the component itself.</p></figcaption></figure>

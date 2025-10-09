---
icon: rectangle-terminal
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

# \[BinderLog]

<mark style="color:$primary;">**The \[BinderLog] attribute**</mark> enables isolated logging within a specific Binder. This allows tracking of value transfers without cluttering the global Unity console.

#### Key Points:

* <mark style="color:$warning;">`[BinderLog]`</mark> adds optional logging that can be enabled or disabled via the Unity Inspector for a specific Binder.
* It provides a localized way to control message output for each Binder, preventing excessive logs in the console when working with multiple Binders in large projects.

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

{% hint style="warning" %}
#### Notes

* The Binder class must be declared as <mark style="color:$warning;">`partial`</mark>.
* The <mark style="color:$warning;">`SetValue`</mark> method must be implicitly implemented, as the code generator uses explicit interface method implementations.
{% endhint %}

### How It Works

* A logging toggle appears in the Unity Inspector for the Binder.
* When enabled, the <mark style="color:$warning;">`SetValue`</mark> method logs the values it receives.

---
icon: sidebar
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

# MonoView

To streamline the integration of Views with the Unity Inspector and the <mark style="color:$primary;">**Aspid.MVVM**</mark> binding system, the <mark style="color:$warning;">`MonoView`</mark> abstract class is used.

This class:

* Combines <mark style="color:$warning;">`MonoBehaviour`</mark> and <mark style="color:$warning;">`IView`</mark>.
* Supports validation and debugging directly in the Unity Inspector.

## Usage

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class MyView : MonoView
{
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _age;
    
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```

### How MonoView Works

* <mark style="color:$warning;">`MonoView`</mark> defines class members (see the [Binder Members](../view/binder-members/) section for details). The most convenient approach is to declare serializable fields of type <mark style="color:$warning;">`MonoBinder`</mark>.
* It is recommended to define fields as arrays to allow attaching an unlimited number of binders.
* The <mark style="color:$warning;">`[RequireBinder]`</mark> attribute is used for validation, ensuring that only <mark style="color:$warning;">`MonoBinder`</mark> descendants implementing <mark style="color:$warning;">`IBinder<T>`</mark> (where <mark style="color:$warning;">`T`</mark> is the specified type in <mark style="color:$warning;">`RequireBinder`</mark>) can be assigned to the field.

***

## <i class="fa-bug">:bug:</i> Debug

Using <mark style="color:$warning;">`MonoView`</mark> provides additional debugging benefits:

### Display of Unassigned Binders

Shows all unattached binders. If child objects of the View contain unattached binders, they will be displayed.

<table><thead><tr><th valign="top">All Binders Attached</th><th valign="top">Some Binders Unattached</th></tr></thead><tbody><tr><td valign="top"><div><figure><img src="../../.gitbook/assets/image (40).png" alt=""><figcaption></figcaption></figure></div></td><td valign="top"><div><figure><img src="../../.gitbook/assets/image (41).png" alt=""><figcaption></figcaption></figure></div></td></tr></tbody></table>

### Debugging the Bound ViewModel

After initializing a <mark style="color:$warning;">`MonoView`</mark> or <mark style="color:$warning;">`ScriptableView`</mark> with a ViewModel, you can debug the ViewModel through the Unity Inspector (view and modify values).&#x20;

<figure><img src="../../.gitbook/assets/image (69).png" alt=""><figcaption></figcaption></figure>

{% hint style="info" %}
#### Field Labels in ViewModel Inspection:

* <mark style="color:$primary;">**Field**</mark>: Represents an object’s field.
* <mark style="color:$primary;">**Bind**</mark>: Represents a bindable property of the object.
{% endhint %}

---
icon: wand-sparkles
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

# \[AsBinder]

<mark style="color:$primary;">**The \[AsBinder] attribute**</mark> is used to generate a Binder based on a field or property of a regular component, eliminating the need to manually create binder objects.

It enables binding between a ViewModel and a View through standard fields or properties of components such as <mark style="color:$warning;">`Text`</mark>, <mark style="color:$warning;">`Slider`</mark>, <mark style="color:$warning;">`Image`</mark>, etc., using a pre-existing binder class.

### When to Use

Use <mark style="color:$warning;">`[AsBinder]`</mark> when you want to work directly with a component rather than its binder.

```csharp
using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class MyView : MonoView
{
    // Constructor: ImageSpriteBinder => (Image target).
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image _image;
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image[] _images;
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image Image => GetComponent<Image>();
    
    [AsBinder(typeof(ImageSpriteBinder))]
    private Image[] Images => GetComponentInChildren<Image>();
}
```

The Source Generator automatically creates an <mark style="color:$warning;">`ImageSpriteBinder`</mark>, using <mark style="color:$warning;">`this._image`</mark> as the constructor argument.

{% hint style="info" %}
#### Additional Notes

The <mark style="color:$warning;">`[AsBinder]`</mark> attribute has an overload <mark style="color:$warning;">`[AsBinder(Type binderType, params object[] args)]`</mark>, which allows you to pass additional parameters for creating the Binder instance.
{% endhint %}

```csharp
using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
​
[View]
public partial class MyView : MonoView
{
    [SerializeField] private bool _isActive;
​
    // Constructor: MyBinder => (Image, int, string, float, bool, bool)
    [AsBinder(typeof(MyBinder), 0, "some", 1.34f, nameof(_isActive), false)]
    [SerializeField] private Image _image;
}
```

### What the Generator Does

* Creates a hidden field of the specified Binder type (e.g., <mark style="color:$warning;">`ImageSpriteBinder`</mark>).
* Initializes it in the <mark style="color:$warning;">`InstantiateBinders()`</mark> method.
* Binds and unbinds the binder during initialization and deinitialization.

### \[AsBinder] for Arrays

The <mark style="color:$warning;">`[AsBinder]`</mark> attribute also works with arrays. In this case, a separate binder is created for each element in the array.

```csharp
[AsBinder(typeof(TextBinder))]
private Text[] _texts; // -> TextBinder[]
```

{% hint style="danger" %}
#### Current Limitations

<mark style="color:$warning;">`[AsBinder]`</mark> for arrays creates a binder for each individual element. Currently, it is not possible to use <mark style="color:$warning;">`[AsBinder]`</mark> to create a binder that accepts an array as a component.&#x20;
{% endhint %}

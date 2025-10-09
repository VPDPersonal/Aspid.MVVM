---
icon: pen-field
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

# Property

In a View class, all properties implementing the <mark style="color:$warning;">`IBinder`</mark> interface are analyzed by the Source Generator in the same way as fields.

{% hint style="warning" %}
#### Important Limitation

The result of each property is cached during generation, and direct access to the property from code is not allowed. In the future, the analyzer will prohibit such usage.
{% endhint %}

```csharp
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;

[View]
public partial class MyView : MonoView
{
    // Do not use directly in code!
    // GameObjectVisibleBinder is a binder that enables or disables
    // a GameObject based on a bool value.
    private GameObjectVisibleBinder IsVisible => new(gameObject);
}
```

### What the Generator Does

* The Source Generator analyzes the property.
* During View initialization, the property’s value is computed once and cached.
* Binding between the ViewModel and this binder occurs through the cached property, not directly through the property itself.

### Why You Can’t Use the Property Directly

Each call to the property creates a new binder instance. If you attempt to access it directly:

* It creates a new object that is not bound to the ViewModel.
* This object will not receive the necessary binder, which may lead to unexpected behavior.

In the future, the analyzer will throw an error if you use the property directly outside of generation.

Use properties only as proxies for the generator. If you need to manually control an instance, always use a [field](field.md).

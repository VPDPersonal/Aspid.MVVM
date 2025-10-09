---
icon: shoe-prints
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
    visible: false
  metadata:
    visible: true
---

# First steps

This section provides information to help you take your first steps with <mark style="color:$primary;">**Aspid.MVVM**</mark>.

***

<h2 align="center">1. Simple Example</h2>

### 1.1. Integrate Aspid.MVVM

[Integrate **Aspid.MVVM**](integration.md) into a Unity project (version 2022.3 or later) or create a new project.

### 1.2. Create a Scene

Create a new scene, add a [Canvas](https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/UICanvas.html) to it, and place a [TextMeshProUGUI](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/TMPObjectUIText.html) omponent on the Canvas.

<figure><img src="../../.gitbook/assets/image (24).png" alt=""><figcaption></figcaption></figure>

### 1.3. Create a Model

Create a static class to store an array of names — <mark style="color:$warning;">`Names`</mark>.

```csharp
using System.Collections.Generic;

public static class Names
{
    public static IReadOnlyList<string> Values { get; } = new[]
    {
        "Vladislav",
        "Alexander",
        "Denis",
    };
}
```

### 1.4. Create a ViewModel

Create <mark style="color:$warning;">`RandomNameViewModel`</mark>, which selects a random name from <mark style="color:$warning;">`Names`</mark> for display in the View.

```csharp
using Aspid.MVVM;
using UnityEngine;

// Each ViewModel is marked with the [ViewModel] attribute
// and must be partial for correct code generation.
[ViewModel]
public partial class RandomNameViewModel
{
    // A field displayed in the View is marked with a binding attribute.
    // Valid field names for generating the Name property:
    // _name, m_name, s_name, name.
    [Bind] private readonly string _name;
    
    public RandomNameViewModel()
    {
        var names = Names.Values;
        _name = names[Random.Range(0, names.Count)];
    }
}
```

### 1.5. Create View

Create <mark style="color:$warning;">`NameView`</mark>, which includes a field for binders that connect string data to View components.

```csharp
using Aspid.MVVM;
using UnityEngine;

// Each View is marked with the [View] attribute
// and must be partial for correct code generation.
// Typically, it inherits from MonoView, a MonoBehaviour descendant,
// to work with MonoBinder.
[View]
public partial class NameView : MonoView
{
    // [RequireBinder] is an optional attribute that restricts
    // allowed binders by data type (in this case, string).
    // The field name must match the field in the ViewModel.
    // Valid fields: _name, m_name, s_name, name are equivalent.
    // MonoBinder is the base class for all binders that are MonoBehaviours.
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```

### 1.6. Configure the View

Add the <mark style="color:$warning;">`NameView`</mark> component to the Canvas object.

<figure><img src="../../.gitbook/assets/image (25).png" alt=""><figcaption></figcaption></figure>

On the object with <mark style="color:$warning;">`TextMeshProUGUI`</mark> add a text binder: <mark style="color:$warning;">**`Text Binder - Text`**</mark> from the StarterKit via the component’s context menu or another method.

<figure><img src="../../.gitbook/assets/image (27).png" alt=""><figcaption></figcaption></figure>

The component will appear red, indicating the binder is not attached. In the dropdown, select <mark style="color:$primary;">View - "Canvas (NameView)" and Id - "Name"</mark>.

<figure><img src="../../.gitbook/assets/image (28).png" alt=""><figcaption></figcaption></figure>

A valid binder looks like this:

<figure><img src="../../.gitbook/assets/image (29).png" alt=""><figcaption></figcaption></figure>

Note how the View changes after setting the binder.

<figure><img src="../../.gitbook/assets/image (30).png" alt=""><figcaption></figcaption></figure>

### 1.7. Create a Bootstrap

Create a new C# class to bind <mark style="color:$warning;">`NameView`</mark> with <mark style="color:$warning;">`RandomNameViewModel`</mark> in <mark style="color:$warning;">`Awake`</mark>, and name it <mark style="color:$warning;">`Bootstrap`</mark>.

```csharp
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private NameView _view;

    private void Awake()
    {
        var viewModel = new RandomNameViewModel();
        
        // The View can be initialized with any ViewModel.
        _view.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        // To unbind the ViewModel from the View, call Deinitialize().
        _view.Deinitialize();
    }      
}
```

### 1.8. Configure the Bootstrap

Create an empty GameObject in the scene, add the <mark style="color:$warning;">`Bootstrap`</mark> component to it, and assign a reference to your <mark style="color:$warning;">`NameView`</mark>.

<figure><img src="../../.gitbook/assets/image (72).png" alt=""><figcaption></figcaption></figure>

### 1.9. Run and Test

{% tabs %}
{% tab title="Result" %}
### Each time you run the scene, a random name will be displayed.

<div><figure><img src="../../.gitbook/assets/image (33).png" alt=""><figcaption><p>First start</p></figcaption></figure> <figure><img src="../../.gitbook/assets/image (31).png" alt=""><figcaption><p>Second start</p></figcaption></figure></div>
{% endtab %}

{% tab title="C#" %}
### Model

```csharp
using System.Collections.Generic;

public static class Names
{
    public static IReadOnlyList<string> Values { get; } = new[]
    {
        "Vladislav",
        "Alexander",
        "Denis",
    };
}
```

### ViewModel

```csharp
using Aspid.MVVM;
using UnityEngine;

[ViewModel]
public partial class RandomNameViewModel
{
    [Bind] private readonly string _name;
    
    public RandomNameViewModel()
    {
        var names = Names.Values;
        _name = names[Random.Range(0, names.Count)];
    }
}
```

### View

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class NameView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```

### Bootstrap

```csharp
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private NameView _view;

    private void Awake()
    {
        var viewModel = new RandomNameViewModel();
        _view.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        _view.Deinitialize();
    }      
}
```
{% endtab %}

{% tab title="Editor" %}
<figure><img src="../../.gitbook/assets/image (24).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (30).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (29).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (73).png" alt=""><figcaption></figcaption></figure>
{% endtab %}
{% endtabs %}

***

<h2 align="center">2. Simplification and Enhancement</h2>

In the second version, we’ll remove the <mark style="color:$warning;">`Bootstrap`</mark> and add another ViewModel.

### 2.1.  Enhance **RandomNameViewModel**

To serialize <mark style="color:$warning;">`RandomNameViewModel`</mark> in the Inspector, add the <mark style="color:$warning;">`[Serializable]`</mark> attribute. Since <mark style="color:$warning;">`Random.Range`</mark> cannot be used in the constructor during serialization, select a random name during object initialization using the <mark style="color:$warning;">`Aspid.MVVM.StarterKit.IComponentInitializable`</mark> interface. This interface is designed for <mark style="color:$warning;">`ViewInitializer`</mark> components, which call <mark style="color:$warning;">`Initialize`</mark> at the appropriate time.

```csharp
using System;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit.Unity;
using Random = UnityEngine.Random;

[ViewModel]
[Serializable]
public partial class RandomNameViewModel : IComponentInitializable
{
    // Since field initialization no longer occurs in the constructor,
    // remove the readonly keyword.
    [Bind] private string _name;

    public void Initialize()
    {
        var names = Names.Values;
        
        // Outside the constructor, use the generated Name property
        // to ensure binding works correctly.
        Name = names[Random.Range(0, names.Count)];
    }
}
```

### 2.2. Add Another ViewModel

Create a ViewModel that always displays the first name from <mark style="color:$warning;">`Names`</mark> — <mark style="color:$warning;">`FirstNameViewModel`</mark>.

```csharp
using System;
using Aspid.MVVM;

[ViewModel]
[Serializable]
public partial class FirstNameViewModel
{
    [Bind] private readonly string _name = Names.Values[0];
}
```

### 2.3. Add ViewInitializer and Remove Bootstrap

Remove the <mark style="color:$warning;">`Bootstrap`</mark> object and add a <mark style="color:$warning;">`ViewInitializer`</mark> component alongside your <mark style="color:$warning;">`NameView`</mark>.

<figure><img src="../../.gitbook/assets/image (74).png" alt=""><figcaption></figcaption></figure>

### 2.4. Configure ViewInitializer

1. Drag the <mark style="color:$warning;">`NameView`</mark> into the View field of the <mark style="color:$warning;">`ViewInitializer`</mark>.
2. In the ViewModel block, set <mark style="color:$primary;">**Resolve = References**</mark> and select <mark style="color:$warning;">`RandomNameViewModel`</mark>.

{% hint style="warning" %}
Selecting a type via Resolve = References requires integration with [SerializeReferenceDropdown](integration.md#serializereference).&#x20;
{% endhint %}

<figure><img src="../../.gitbook/assets/image (75).png" alt=""><figcaption></figcaption></figure>

### 2.5. Run and Test

{% tabs %}
{% tab title="Result" %}
### Each time you run the scene, a random name will be displayed.&#x20;

<div><figure><img src="../../.gitbook/assets/image (33).png" alt=""><figcaption><p>First start</p></figcaption></figure> <figure><img src="../../.gitbook/assets/image (20).png" alt=""><figcaption><p>Second start</p></figcaption></figure></div>
{% endtab %}

{% tab title="C#" %}
### Model

```csharp
using System.Collections.Generic;

public static class Names
{
    public static IReadOnlyList<string> Values { get; } = new[]
    {
        "Vladislav",
        "Alexander",
        "Denis",
    };
}
```

### ViewModel

```csharp
using System;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;
using Random = UnityEngine.Random;

[ViewModel]
[Serializable]
public partial class RandomNameViewModel : IComponentInitializable
{
    [Bind] private string _name;

    public void Initialize()
    {
        var names = Names.Values;
        Name = names[Random.Range(0, names.Count)];
    }
}
```

### View

```csharp
using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

[View]
public partial class NameView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```
{% endtab %}

{% tab title="Editor" %}
<figure><img src="../../.gitbook/assets/image (75).png" alt=""><figcaption></figcaption></figure>
{% endtab %}
{% endtabs %}

### 2.6. Change the ViewModel

In the <mark style="color:$warning;">`ViewInitializer`</mark>, select <mark style="color:$warning;">`FirstNameViewModel`</mark> instead of <mark style="color:$warning;">`RandomNameViewModel`</mark>.

<figure><img src="../../.gitbook/assets/image (76).png" alt=""><figcaption></figcaption></figure>

### 2.7. Run and Test

{% tabs %}
{% tab title="Result" %}
### Each time you run the scene, the displayed name will always be the first element from the array in the Names class.

<div><figure><img src="../../.gitbook/assets/image (33).png" alt=""><figcaption><p>Первый запуск</p></figcaption></figure> <figure><img src="../../.gitbook/assets/image (33).png" alt=""><figcaption><p>Второй запуск</p></figcaption></figure></div>
{% endtab %}

{% tab title="C#" %}
### Model

```csharp
using System.Collections.Generic;

public static class Names
{
    public static IReadOnlyList<string> Values { get; } = new[]
    {
        "Vladislav",
        "Alexander",
        "Denis",
    };
}
```

### ViewModel

```csharp
using System;
using Aspid.MVVM;

[ViewModel]
[Serializable]
public partial class FirstViewModel
{
    [Bind] private readonly string _name = Names.Values[0];
}
```

### View

```csharp
using Aspid.MVVM;
using UnityEngine;

[View]
public partial class NameView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;
}
```
{% endtab %}

{% tab title="Editor" %}
<figure><img src="../../.gitbook/assets/image (76).png" alt=""><figcaption></figcaption></figure>
{% endtab %}
{% endtabs %}

***

<h2 align="center">What’s Next</h2>

We recommend exploring more complex examples next:

<table data-card-size="large" data-view="cards"><thead><tr><th align="center"></th><th data-hidden data-card-cover data-type="files"></th><th data-hidden data-card-target data-type="content-ref"></th></tr></thead><tbody><tr><td align="center"><i class="fa-hand-wave">:hand-wave:</i> Hello World</td><td><a href="../../.gitbook/assets/image (34).png">image (34).png</a></td><td><a href="../../tutorials/hello-world/">hello-world</a></td></tr></tbody></table>

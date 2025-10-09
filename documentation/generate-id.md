---
icon: fingerprint
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

# Generate Id

Во всех элементах системы Aspid.MVVM ([ViewModel](viewmodel/), [View](view/), [Custom ViewModel Interface](viewmodel/custom-viewmodel-interface.md)), the <mark style="color:$warning;">`Id`</mark> is a string used to establish binding between a ViewModel and a View.

By default, the <mark style="color:$warning;">`Id`</mark> is generated automatically but can be manually overridden using the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute.

### <i class="fa-file-code">:file-code:</i> Id Generation in ViewModel

A bindable field or constant generates an <mark style="color:$warning;">`Id`</mark> according to the following algorithm:

1. Removes the prefix (<mark style="color:$warning;">`_`</mark>, <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>), if present.
2. Converts the first character to uppercase.
3. <mark style="color:$primary;">**Example**</mark>: <mark style="color:$warning;">`_age ⇒ Age`</mark>&#x20;

For methods with the <mark style="color:$warning;">`[RelayCommand]`</mark> attribute, the <mark style="color:$warning;">`Id`</mark> is generated as follows:

1. Appends the word "Command" to the method name.
2. <mark style="color:$primary;">**Example**</mark>:  <mark style="color:$warning;">`Do() ⇒ DoCommand`</mark>;

If you are satisfied with the name of the bindable member but not with the generated <mark style="color:$warning;">`Id`</mark>, you can override the <mark style="color:$warning;">`Id`</mark> using the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [BindId("NewAge")]
    [Bind] private int _age;
    
    [RelayCommand]
    [BindId("NewCommand")]
    private void Do() { }
} 
```

### <i class="fa-sidebar">:sidebar:</i> Id Generation in View

Each field analyzed by the Source Generator generates an <mark style="color:$warning;">`Id`</mark> according to the following algorithm:

1. Removes the prefix (<mark style="color:$warning;">`_`</mark>, <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>), if present.
2. Converts the first character to uppercase.
3. <mark style="color:$primary;">**Example**</mark>: <mark style="color:$warning;">`_age ⇒ Age`</mark>&#x20;

Each property analyzed by the Source Generator generates an Id as follows:

1. Converts the first character to uppercase.
2. <mark style="color:$primary;">**Example**</mark>: <mark style="color:$warning;">`Age ⇒ Age`</mark>&#x20;

If you are satisfied with the binder's name but not with the generated <mark style="color:$warning;">`Id`</mark>, you can override the <mark style="color:$warning;">`Id`</mark> using the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[View]
public partial class MyView
{
    private Text _text;    

    [BindId("NewAge")]
    private TextBinder _age;
    
    [BindId("Name")]
    private TextBinder Text => _text;
} 
```

### <i class="fa-crosshairs">:crosshairs:</i> Id Generation in Custom ViewModel Interface

Each property of type <mark style="color:$warning;">`IBinderAdder`</mark>, <mark style="color:$warning;">`IReadOnlyBindableMember<T>`</mark>, or <mark style="color:$warning;">`IReadOnlyValueBindableMember<T>`</mark> generates an <mark style="color:$warning;">`Id`</mark> as follows:

1. Converts the first character to uppercase.
2. <mark style="color:$primary;">**Example**</mark>: <mark style="color:$warning;">`Age ⇒ Age`</mark>&#x20;

If you are satisfied with the property name but not with the generated <mark style="color:$warning;">`Id`</mark>, you can override the <mark style="color:$warning;">`Id`</mark> using the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute:

```csharp
using Aspid.MVVM;

public interface IMyViewModel
{   
    [BindId("FirstName")]
    public IBinderAdder AnyType { get; }

    // Supports all binding modes except OneTime.
    // The bindable field must be of type int.
    [BindId("NewAge")]
    public IReadOnlyBindableMember<int> Age { get; }
    
    // Supports all binding modes.
    // The bindable field must be of type string.
    [BindId("LastName")]
    public IReadOnlyValueBindableMember<string> Text { get; }
} 
```

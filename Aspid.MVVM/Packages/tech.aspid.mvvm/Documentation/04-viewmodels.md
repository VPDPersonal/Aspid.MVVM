# ViewModels

The ViewModel is the centre of Aspid.MVVM. The Source Generator emits the binding code for every field marked with an attribute.

## Contents

- [Creating a ViewModel](#creating-a-viewmodel)
- [The \[Bind\] attribute](#the-bind-attribute)
- [Shorthand attributes](#shorthand-attributes)
- [The \[BindAlso\] attribute](#the-bindalso-attribute)
- [The \[BindId\] attribute](#the-bindid-attribute)
- [The \[Access\] attribute](#the-access-attribute)
- [Change handlers](#change-handlers)
- [MonoViewModel](#monoviewmodel)
- [ScriptableViewModel](#scriptableviewmodel)
- [NotifyAll](#notifyall)

---

## Creating a ViewModel

The smallest ViewModel:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private string _name;
    [OneWayBind] private int _health;
}
```

**Requirements:**
1. The class is `partial`: the Source Generator completes it
2. The `[ViewModel]` attribute marks it for the generator
3. At least one field with a `[Bind]` attribute

The Source Generator implements `IViewModel` and emits:
- A property for every marked field
- `BindableMember<T>` for every binding
- `FindBindableMember()` for dispatch
- `NotifyAll()` for bulk notification

### Three flavours of ViewModel

| Type | Base class | When to use |
|-----|--------------|-------------------|
| **POCO** | None | The default. Plain C# without Unity dependencies |
| **MonoViewModel** | `MonoBehaviour` | When the ViewModel must be editable in the Inspector |
| **ScriptableViewModel** | `ScriptableObject` | Data shared between scenes |

---

## The [Bind] attribute

Marks a field for binding generation. The mode is detected or set explicitly:

```csharp
[ViewModel]
public partial class ExampleViewModel
{
    // Automatic mode:
    [Bind] private const string Title = "Hello";  // → OneTime (const)
    [Bind] private readonly int _id;                // → OneTime (readonly)
    [Bind] private string _name;                    // → TwoWay  (mutable)

    // Explicit mode:
    [Bind(BindMode.OneWay)] private int _score;
    [Bind(BindMode.TwoWay)] private string _input;
}
```

### Naming rules

The Source Generator understands the common field naming styles:

| Field | Generated property |
|------|------------------------|
| `_outText` | `OutText` |
| `m_outText` | `OutText` |
| `s_outText` | `OutText` |
| `outText` | `OutText` |

---

## Shorthand attributes

A shorter way to set the mode:

```csharp
[ViewModel]
public partial class ShortcutExample
{
    [OneWayBind] private string _label;          // BindMode.OneWay
    [TwoWayBind] private float _volume;          // BindMode.TwoWay
    [OneTimeBind] private IRelayCommand _save;   // BindMode.OneTime
    [OneWayToSourceBind] private string _input;  // BindMode.OneWayToSource
}
```

---

## The [BindAlso] attribute

When the field changes, the named property is notified as well. Used for computed properties:

```csharp
[ViewModel]
public partial class PersonViewModel
{
    [BindAlso(nameof(Nickname))]
    [BindAlso(nameof(FullName))]
    [Bind] private string _name;

    [BindAlso(nameof(FullName))]
    [Bind] private string _family;

    // Computed properties, refreshed when _name or _family changes
    private string Nickname => Name.ToLower();
    private string FullName => $"{Name} {Family}";
}
```

When `Name` changes, binders attached to `Nickname` and `FullName` are notified too.

---

## The [BindId] attribute

Overrides the binding id (by default the generated property name):

```csharp
[ViewModel]
public partial class CustomIdViewModel
{
    // The _text1 field binds under the id "Text2"
    [BindId("Text2")]
    [Bind] private string _text1;

    // The Do method binds as the command "OtherDoCommand"
    [RelayCommand]
    [BindId("OtherDoCommand")]
    private void Do() { }
}
```

Useful when the View field name does not match the ViewModel member name.

---

## The [Access] attribute

Controls the visibility of the generated property:

```csharp
[ViewModel]
public partial class AccessExample
{
    // private string Text1 { get; set; }  (default)
    [Bind] private string _text1;

    // public string Text2 { get; set; }
    [Access(Access.Public)]
    [Bind] private string _text2;

    // protected string Text3 { get; set; }
    [Access(Access.Protected)]
    [Bind] private string _text3;

    // public string Text4 { get; private set; }
    [Access(Get = Access.Public)]
    [Bind] private string _text4;

    // public string Text5 { get; protected set; }
    [Access(Get = Access.Public, Set = Access.Protected)]
    [Bind] private string _text5;

    // protected string Text6 { private get; set; }
    [Access(Get = Access.Protected, Set = Access.Public)]
    [Bind] private string _text6;
}
```

### Access levels

- `Access.Private`: the default for get and set
- `Access.Protected`: visible to subclasses
- `Access.Public`: visible to everyone

`Get` and `Set` are configured independently, so properties like `public get / private set` are possible.

---

## Change handlers

The Source Generator declares `partial` methods called when a property changes:

```csharp
[ViewModel]
public partial class HandlerExample
{
    [Bind] private string _name;

    // Called BEFORE Name changes
    partial void OnNameChanging(string oldValue, string newValue)
    {
        // Validation or logging
    }

    // Called AFTER Name changes
    partial void OnNameChanged(string newValue)
    {
        // Update dependent data
    }
}
```

### Pattern: react to input immediately

```csharp
[ViewModel]
public partial class MomentSpeakerViewModel
{
    [TwoWayBind] private string _inputText;

    private readonly Speaker _speaker;

    // Every change of the InputField text
    // updates the model at once
    partial void OnInputTextChanged(string newValue)
    {
        _speaker.Say(newValue);
    }
}
```

---

## MonoViewModel

For ViewModels edited in the Inspector:

```csharp
using UnityEngine;
using Aspid.MVVM;

[ViewModel]
public partial class SettingsViewModel : MonoViewModel
{
    [SerializeField] [OneWayBind] private float _musicVolume = 0.8f;
    [SerializeField] [OneWayBind] private float _sfxVolume = 1.0f;
}
```

**Details:**
- Inherits `MonoBehaviour`
- `OnValidate()` calls `NotifyAll()`, so Inspector edits show up immediately
- `Dispose()` calls `Destroy(this)`

---

## ScriptableViewModel

For ViewModels shared between scenes:

```csharp
using UnityEngine;
using Aspid.MVVM;

[ViewModel]
public partial class GameConfigViewModel : ScriptableViewModel
{
    [SerializeField] [OneWayBind] private string _gameName;
    [SerializeField] [OneWayBind] private int _maxPlayers;
}
```

**Details:**
- Inherits `ScriptableObject`
- `OnValidate()` calls `NotifyAll()`
- Can be created through `CreateAssetMenu`

---

## NotifyAll

A generated method that pushes the current values to every binding:

```csharp
var viewModel = new PlayerViewModel();
viewModel.Health = 100;
viewModel.Name = "Hero";
viewModel.Armor = 50;

// Notify every binder of the current values at once
viewModel.NotifyAll();
```

**When to use:**
- After a bulk update of several fields
- After deserialization / loading data
- In `MonoViewModel.OnValidate()` (called automatically)

---

## See also

- [Binding Modes](03-binding-modes.md), details of `BindMode`
- [Commands](07-commands.md), the `[RelayCommand]` attribute
- [Views](05-views.md), creating a View for a ViewModel
- [Dynamic ViewModel](10-dynamic-viewmodel.md), a ViewModel without code generation

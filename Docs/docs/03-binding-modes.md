# Binding Modes

Aspid.MVVM supports four data binding modes between the ViewModel and the View.

## Contents

- [Overview](#overview)
- [OneWay](#oneway)
- [TwoWay](#twoway)
- [OneTime](#onetime)
- [OneWayToSource](#onewaytosource)
- [Automatic mode detection](#automatic-mode-detection)
- [Explicit mode](#explicit-mode)
- [Restricting modes in the Inspector](#restricting-modes-in-the-inspector)

---

## Overview

```csharp
public enum BindMode
{
    None = 0,
    OneWay = 1,
    TwoWay = 2,
    OneTime = 3,
    OneWayToSource = 4
}
```

| Mode | Direction | When to use |
|-------|-------------|-------------------|
| **OneWay** | ViewModel → View | Displaying data (text, progress bar, icon) |
| **TwoWay** | ViewModel ↔ View | Input fields, sliders, toggles |
| **OneTime** | ViewModel → View (once) | Static data, commands |
| **OneWayToSource** | View → ViewModel | Component references, UI events |

---

## OneWay

Data flows from the ViewModel to the View only. When the property changes, the binder updates the UI. UI changes never reach the ViewModel.

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private int _health;
    [OneWayBind] private string _playerName;
}
```

**Implementation:** `OneWayBindableMember<T>` stores the value and a `Changed` event. On `Add()` the binder immediately receives the current value and subscribes to `Changed`.

**Typical binders:** `TextBinder`, `ImageSpriteBinder`, `ImageFillBinder`, `GraphicColorBinder`.

---

## TwoWay

Two-way synchronization. A ViewModel change updates the View, and a View change updates the ViewModel.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [TwoWayBind] private string _inputText;
    [TwoWayBind] private bool _isEnabled;
    [TwoWayBind] private float _volume;
}
```

**Implementation:** `TwoWayBindableMember<T>` supports all four modes. For TwoWay/OneWayToSource binders `Add()` also subscribes to `IReverseBinder<T>.ValueChanged`.

**Loop protection:** binders such as `InputFieldBinder` keep an `_isNotifyValueChanged` flag to prevent infinite recursion.

**Typical binders:** `InputFieldBinder`, `SliderValueBinder`, `ToggleIsOnBinder`.

---

## OneTime

The value is delivered once, when binding. Later changes are not tracked.

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    // OneTime automatically for const
    [Bind] private const string Title = "Player Stats";

    // OneTime automatically for readonly
    [Bind] private readonly int _maxHealth;

    // Explicit OneTime
    [OneTimeBind] private IRelayCommand _saveCommand;
}
```

**Implementation:** `OneTimeBindableMember<T>` is a singleton per T. `Add()` calls `SetValue` once and returns `null` (no `IBinderRemover` is needed).

**When to use:** commands (`IRelayCommand`), configuration data, static labels.

---

## OneWayToSource

Data flows from the View to the ViewModel only. The ViewModel cannot push values to the View.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [OneWayToSourceBind] private string _userInput;
}
```

**Implementation:** `OneWayToSourceBindableMember<T>` stores no value. It subscribes to `IReverseBinder<T>.ValueChanged` and forwards changes to the ViewModel.

**When to use:** user input without an initial value, component references through `ComponentToSourceMonoBinder`.

---

## Automatic mode detection

`[Bind]` without arguments picks the mode itself:

| Field | Detected mode |
|----------|-------------------|
| `const` | OneTime |
| `readonly` | OneTime |
| Mutable field | TwoWay |

```csharp
[ViewModel]
public partial class ExampleViewModel
{
    [Bind] private const string Title = "Hello";     // → OneTime
    [Bind] private readonly int _id;                  // → OneTime
    [Bind] private string _name;                      // → TwoWay
    [Bind] private float _value;                      // → TwoWay
}
```

> [!TIP]
> Prefer the explicit attributes (`[OneWayBind]`, `[TwoWayBind]`, …) for readability.

---

## Explicit mode

### Through the `[Bind]` argument

```csharp
[Bind(BindMode.OneWay)] private string _text;
[Bind(BindMode.TwoWay)] private float _slider;
[Bind(BindMode.OneTime)] private IRelayCommand _command;
[Bind(BindMode.OneWayToSource)] private string _userInput;
```

### Through the shorthand attributes

```csharp
[OneWayBind] private string _text;
[TwoWayBind] private float _slider;
[OneTimeBind] private IRelayCommand _command;
[OneWayToSourceBind] private string _userInput;
```

Both forms are equivalent. The shorthand attributes are just shorter.

---

## Restricting modes in the Inspector

On the binder side the allowed modes can be limited with `[BindModeOverride]`:

```csharp
// OneWay and OneTime only
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public class TransformPositionBinder : TargetBinder<Transform, Vector3>, IVector3Binder
{
    // TwoWay and OneWayToSource are not offered in the Inspector
}
```

Some binders support every mode:

```csharp
[BindModeOverride(IsAll = true)]
public class DebugLogBinder : MonoBinder
{
    // All modes available
}
```

---

## Summary

| Mode | ViewModel → View | View → ViewModel | Updates | Good for |
|-------|:---:|:---:|-----------|-------------|
| OneWay | ✅ | ❌ | On every change | Displaying data |
| TwoWay | ✅ | ✅ | On every change (both sides) | Interactive elements |
| OneTime | ✅ | ❌ | Only when binding | Static data, commands |
| OneWayToSource | ❌ | ✅ | On View change | Input, component references |

---

## See also

- [Architecture](02-architecture.md), how the binding pipeline works
- [ViewModels](04-viewmodels.md), declaring bindable members
- [Binders](06-binders.md), writing binders with specific modes

# Commands

Commands (`IRelayCommand`) encapsulate actions with `CanExecute` support. The `[RelayCommand]` attribute generates a command from an ordinary method.

## Contents

- [Overview](#overview)
- [IRelayCommand](#irelaycommand)
- [The \[RelayCommand\] attribute](#the-relaycommand-attribute)
- [CanExecute](#canexecute)
- [Parameterized commands](#parameterized-commands)
- [Manual creation](#manual-creation)
- [RelayCommand.Empty](#relaycommandempty)

---

## Overview

A command is an object that:
- Performs an action (`Execute`)
- Tells whether the action is available (`CanExecute`)
- Notifies when availability changes (`CanExecuteChanged`)

In Aspid.MVVM commands are bound to the UI through `ButtonCommandBinder`.

---

## IRelayCommand

```csharp
public interface IRelayCommand
{
    event Action<IRelayCommand>? CanExecuteChanged;
    bool CanExecute();
    void Execute();
    void NotifyCanExecuteChanged();
}
```

Parameterized variants take up to four parameters:

| Interface | Execute signature |
|-----------|------------------|
| `IRelayCommand` | `void Execute()` |
| `IRelayCommand<T>` | `void Execute(T arg)` |
| `IRelayCommand<T1, T2>` | `void Execute(T1, T2)` |
| `IRelayCommand<T1, T2, T3>` | `void Execute(T1, T2, T3)` |
| `IRelayCommand<T1, T2, T3, T4>` | `void Execute(T1, T2, T3, T4)` |

---

## The [RelayCommand] attribute

Generates an `IRelayCommand` property from a method:

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [RelayCommand]
    private void Attack()
    {
        _player.Attack();
    }
    // → Generated: IRelayCommand AttackCommand { get; }

    [RelayCommand]
    private void Heal(int amount)
    {
        _player.Heal(amount);
    }
    // → Generated: IRelayCommand<int> HealCommand { get; }
}
```

**Convention:** the method `DoSomething()` produces the property `DoSomethingCommand`.

---

## CanExecute

Three ways to define the availability condition:

### 1. A bool method

```csharp
[ViewModel]
public partial class FormViewModel
{
    [Bind] private string _text;

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit() { /* ... */ }

    private bool CanSubmit() => !string.IsNullOrEmpty(Text);
}
```

### 2. A bool method with the same parameters

```csharp
[ViewModel]
public partial class MathViewModel
{
    [RelayCommand(CanExecute = nameof(CanDivide))]
    private void Divide(int a, int b)
    {
        Result = a / b;
    }

    private bool CanDivide(int a, int b) => b != 0;
}
```

### 3. A bool property or field

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private bool _isDraft;

    [RelayCommand(CanExecute = nameof(IsDraft))]
    private void Confirm() { /* ... */ }

    [RelayCommand(CanExecute = nameof(IsDraft))]
    private void ResetToDefault() { /* ... */ }

    // When IsDraft changes, refresh command availability
    partial void OnIsDraftChanged(bool newValue)
    {
        ConfirmCommand.NotifyCanExecuteChanged();
        ResetToDefaultCommand.NotifyCanExecuteChanged();
    }
}
```

> [!IMPORTANT]
> Call `NotifyCanExecuteChanged()` whenever the condition changes. Without it `ButtonCommandBinder` will not update the button state.

---

## Parameterized commands

Up to four parameters are supported:

```csharp
[ViewModel]
public partial class CommandsExample
{
    // 0 parameters → IRelayCommand
    [RelayCommand]
    private void Do0() { }

    // 1 parameter → IRelayCommand<int>
    [RelayCommand]
    private void Do1(int arg1) { }

    // 2 parameters → IRelayCommand<int, string>
    [RelayCommand]
    private void Do2(int arg1, string arg2) { }

    // 3 parameters → IRelayCommand<int, string, float>
    [RelayCommand]
    private void Do3(int arg1, string arg2, float arg3) { }

    // 4 parameters → IRelayCommand<int, string, float, bool>
    [RelayCommand]
    private void Do4(int arg1, string arg2, float arg3, bool arg4) { }
}
```

Parameters come from `ButtonCommandBinder<T>` through serialized fields in the Inspector.

---

## Manual creation

When `[RelayCommand]` does not fit:

```csharp
[ViewModel]
public partial class ManualCommandViewModel
{
    // Created by hand, bound through [Bind]
    [Bind] private readonly IRelayCommand _saveCommand;
    [Bind] private readonly IRelayCommand _deleteCommand;

    public ManualCommandViewModel(IStorage storage)
    {
        _saveCommand = new RelayCommand(
            execute: () => storage.Save(),
            canExecute: () => storage.HasChanges
        );

        _deleteCommand = new RelayCommand(
            execute: () => storage.Delete(),
            canExecute: () => storage.CanDelete
        );
    }
}
```

> [!NOTE]
> A `readonly` field with `[Bind]` gets the **OneTime** mode automatically.

---

## RelayCommand.Empty

Static stubs for when a command is not needed:

```csharp
// A command that cannot execute (CanExecute = false)
IRelayCommand disabled = RelayCommand.Empty;

// A command that can execute but does nothing
IRelayCommand noop = RelayCommand.EmptyExecution;
```

---

## Relation to ButtonCommandBinder

`ButtonCommandBinder` binds an `IRelayCommand` to `Button.onClick`:

```csharp
// ViewModel:
[RelayCommand]
private void Save() { /* ... */ }

// View:
[SerializeField] private MonoBinder _saveCommand;

// Inspector: put a ButtonCommandBinder on the button
// and drag it into the _saveCommand field
```

More: [ButtonCommandBinder](StarterKit/button-command-binders.md).

---

## See also

- [ViewModels](04-viewmodels.md), `[RelayCommand]` in the ViewModel context
- [ButtonCommandBinder](StarterKit/button-command-binders.md), binding to a button
- [Binding Modes](03-binding-modes.md), OneTime for commands

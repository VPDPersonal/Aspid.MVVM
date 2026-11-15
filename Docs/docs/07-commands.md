# Commands

Commands (`IRelayCommand`) encapsulate actions with `CanExecute` support. The `[RelayCommand]` attribute generates a command from an ordinary method.

## Contents

- [Overview](#overview)
- [IRelayCommand](#irelaycommand)
- [The \[RelayCommand\] attribute](#the-relaycommand-attribute)
- [CanExecute](#canexecute)
- [Parameterized commands](#parameterized-commands)
- [Manual creation](#manual-creation)
- [Binding command parameters](#binding-command-parameters)
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

An ordinary `bool` field does not need `[Bind]`. Accessible fields (including `readonly`, `const`, `static` and inherited fields), readable properties and properties generated from `[Bind]` are supported. The command reads the current value on each check.

```csharp
private bool _canSubmit;

[RelayCommand(CanExecute = nameof(_canSubmit))]
private void Submit() { }
```

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

## Binding command parameters

Use `CreateCommandWithoutParameters` to bind arguments before assigning a command to a parameterless button binder:

```csharp
bool available = true;
var select = new RelayCommand<int>(id => Debug.Log(id), _ => available);
IRelayCommand selectFirst = select.CreateCommandWithoutParameters(1);

available = false;
select.NotifyCanExecuteChanged();
```

The adapter forwards `CanExecuteChanged` with itself as the sender. A button bound to `selectFirst` receives changes from `select`. The adapter subscribes to its source when its first listener is added and detaches when the last listener is removed. Custom listeners must unsubscribe when they are no longer needed.

`Execute` forwards directly to the source with the bound arguments; the source command controls whether execution checks `CanExecute`. Wrapping a `RelayCommand` evaluates its predicate once per execution.

A missing source throws `ArgumentNullException` when the adapter is created. Use `CreateCommandWithoutParametersOrEmpty` or `CreateCommandWithoutParametersOrEmptyExecution` for an optional source.

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

A method accepts only one `[RelayCommand]` and must be a non-generic `void` method with up to four value parameters (`ASPIDVM005`). An explicitly named `CanExecute` must resolve to an accessible bool field, bool getter or unambiguous compatible bool method; otherwise generation fails with `ASPIDVM004`. A misspelled predicate never silently enables the command.

Method predicates follow C# conversion rules for `Func<…, bool>`, including overload resolution, generic type inference and constraints. If no method matches the command parameters, a parameterless method may be used.

## See also

- [ViewModels](04-viewmodels.md), `[RelayCommand]` in the ViewModel context
- [ButtonCommandBinder](StarterKit/button-command-binders.md), binding to a button
- [Binding Modes](03-binding-modes.md), OneTime for commands

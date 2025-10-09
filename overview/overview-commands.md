---
icon: command
---

# Overview - Commands

<h2 align="center"><i class="fa-command">:command:</i> <a href="../documentation/commands/">Commands</a></h2>

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides a powerful and flexible command mechanism based on the <mark style="color:$warning;">`IRelayCommand`</mark> interface and the <mark style="color:$warning;">`RelayCommand`</mark> class:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneWayBind] private readonly IRelayCommand _command;
    
    public MyViewModel()
    {
        _command = new RelayCommand(Execute, CanExecute);
    }
    
    private void Execute() { } 
    
    private bool CanExecute() => true;
}
```

***

### <i class="fa-overline">:overline:</i> [Overloads](../documentation/commands/#overloads)

<table><thead><tr><th width="267.77734375">Overload</th><th>Execute</th><th>CanExecute</th></tr></thead><tbody><tr><td><mark style="color:$warning;"><code>RelayCommand</code></mark></td><td>No parameters</td><td>No parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1></code></mark></td><td>1 parameter</td><td>1 parameter</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2></code></mark></td><td>2 parameters</td><td>2 parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2, T3></code></mark></td><td>3 parameters</td><td>3 parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2, T3, T4></code></mark></td><td>4 parameters</td><td>4 parameters</td></tr></tbody></table>

***

### <i class="fa-code-branch">:code-branch:</i> [Execution Conditions](../documentation/commands/#execution-conditions)

If the <mark style="color:$warning;">`CanExecute`</mark> condition changes, call <mark style="color:$warning;">`NotifyCanExecuteChanged()`</mark>:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneWayBind] private readonly IRelayCommand _command;
  
    private bool _isActive;
    
    public bool IsActive
    {
        get => _isActive;
        set 
        {
            _isActive = value;
            Command.NotifyCanExecuteChanged();
        }
    }
    
    public MyViewModel()
    {
        _command = new RelayCommand(Execute, () => IsActive);
    }
    
    private void Execute() { } 
}
```

***

<h2 align="center"><i class="fa-maximize">:maximize:</i> <a href="../documentation/commands/extensions.md">Extensions</a></h2>

<mark style="color:$primary;">**Aspid.MVVM**</mark> includes extension methods for common command-related scenarios:

***

### [GetSelfOrEmpty](../documentation/commands/extensions.md#getselforempty)

An extension method that returns the command if it is not null; otherwise, it returns an empty command:

```csharp
#nullable enable
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;

    public MyViewModel(IRelayCommand? command)
    {
        _command = command.GetSelfOrEmpty();
    }
}
```

***

### [CreateCommand](../documentation/commands/extensions.md#createcommand)

An extension method that creates a command based on a <mark style="color:$warning;">`System.Action`</mark>:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    [OneTimeBind] private readonly IRelayCommand _commandWithCanExecute;
    
    public MyViewModel(Action command, Func<bool> canExecute)
    {
        _command = command.CreateCommand();
        _commandWithCanExecute = command.CreateCommand(canExecute);
    }
}
```

***

### [CreateCommandOrEmpty](../documentation/commands/extensions.md#createcommandorempty)

An extension method that creates a command based on a <mark style="color:$warning;">`System.Action`</mark> if it is not null; otherwise, it returns an empty command:

```csharp
#nullable enable
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    [OneTimeBind] private readonly IRelayCommand _commandWithCanExecute;
    
    public MyViewModel(Action? command, Func<bool>? canExecute)
    {
        _command = command.CreateCommandOrEmpty();
        _commandWithCanExecute = command.CreateCommandOrEmpty(canExecute);
    }
}
```

***

### [CreateCommandWithoutParameters](../documentation/commands/extensions.md#createcommandwithoutparameters)

An extension method that creates a parameterless command based on a command with parameters:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    
    public MyViewModel(IRelayCommand<MyViewModel> command)
    {
        _command = command.CreateCommandWithoutParameters(this);
    }
}
```

***

### [CreateCommandWithoutParametersOrEmpty](../documentation/commands/extensions.md#createcommandwithoutparametersorempty)

An extension method that creates a parameterless command based on a command with parameters if it is not null; otherwise, it returns an empty command:

```csharp
#nullable enable
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    
    public MyViewModel(IRelayCommand<MyViewModel>? command)
    {
        _command = command.CreateCommandWithoutParametersOrEmpty(this);
    }
}
```

***

<h2 align="center"><i class="fa-wand-sparkles">:wand-sparkles:</i> <a href="../documentation/commands/relaycommand.md">[RelayCommand]</a></h2>

You can create a command based on a method using code generation:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [RelayCommand]
    private void Execute() { }
}
```

You can also specify a <mark style="color:$warning;">`CanExecute`</mark> method for the command:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [RelayCommand(CanExecute = nameof(CanExecute))]
    private void Execute() { }
    
    private void CanExecute() => true;
}
```

The <mark style="color:$warning;">`CanExecute`</mark> in the attribute can also reference a property:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    private bool IsActive => true;

    [RelayCommand(CanExecute = nameof(IsActive))]
    private void Execute() { }
}
```

If your method accepts parameters (up to 4 parameters), <mark style="color:$warning;">`CanExecute`</mark> can optionally accept those parameters:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    private bool IsActive => true;

    [RelayCommand(CanExecute = nameof(CanExecute1))]
    private void Execute1(int a) { }
    
    private bool CanExecute1(int a) => true;
    
    [RelayCommand(CanExecute = nameof(CanExecute2))]
    private void Execute2(int a) { }
    
    private bool CanExecute2() => true;
    
    [RelayCommand(CanExecute = nameof(IsActive))]
    private void Execute3(int a) { }
}
```

***

<h2 align="center"><i class="fa-fingerprint">:fingerprint:</i> <a href="../documentation/generate-id.md#id-generation-in-viewmodel">Generate Id</a></h2>

By default, all commands generated with the <mark style="color:$warning;">`[RelayCommand]`</mark> attribute generate their <mark style="color:$warning;">`Id`</mark> according to the following rule:

* Method name + "Command" postfix.

To specify a custom <mark style="color:$warning;">`Id`</mark> for a command, mark the generated command with the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [RelayCommand]
    [BindId("MyCommand")]
    private void Execute() { }
}
```

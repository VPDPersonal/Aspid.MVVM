---
icon: maximize
---

# Extensions

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides extension methods to simplify working with commands.

## GetSelfOrEmpty

Binders may not handle null commands. Therefore, it’s recommended to use empty commands when the desired implementation is unavailable.

```csharp
#nullable enable
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;

    public MyViewModel(IRelayCommand? command)
    {
        if (command is null) _command = RelayCommand.Empty;
        else _command = command;
    }
}
```

A shorter syntax is available using the extension method:

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

This extension method works with all <mark style="color:$warning;">`IRelayCommand`</mark> overloads.

## CreateCommand

Each command (regardless of overload) is created based on <mark style="color:$warning;">`System.Action`</mark>:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    [OneTimeBind] private readonly IRelayCommand _commandWithCanExecute;
    
    public MyViewModel(Action command, Func<bool> canExecute)
    {
        _command = new RelayCommand(command);
        _commandWithCanExecute = new RelayCommand(command, canExecute);
    }
}
```

A shorter syntax is available using the extension method:

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

## CreateCommandOrEmpty

The <mark style="color:$warning;">`CreateCommandOrEmpty`</mark> method creates a command based on <mark style="color:$warning;">`System.Action`</mark>, but returns an empty command if the <mark style="color:$warning;">`Action`</mark> is null. Without the extension method, it looks like this:

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
        if (command is null) _command = RelayCommand.Empty;
        else _command = new RelayCommand(command);
        
        if (command is null) _commandWithCanExecute = RelayCommand.Empty;
        else _commandWithCanExecute = new RelayCommand(command, canExecute);   
    }
}
```

Using the extension method:

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

## CreateCommandWithoutParameters

Sometimes, you need to convert a command with parameters into a command without parameters:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    
    public MyViewModel(IRelayCommand<MyViewModel> command)
    {
        _command = new RelayCommand(
            () => command.Execute(this), 
            () => command.CanExecute(this));
    }
}
```

A shorter syntax is available using the extension method:

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

## CreateCommandWithoutParametersOrEmpty

The <mark style="color:$warning;">`CreateCommandWithoutParametersOrEmpty`</mark> method creates a parameterless command from a command with parameters, but returns an empty command if the input command is null. Without the extension method, it looks like this:

```csharp
#nullable enable
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [OneTimeBind] private readonly IRelayCommand _command;
    
    public MyViewModel(IRelayCommand<MyViewModel>? command)
    {
        if (command is null) _command = RelayCommand.Empty;
        else _command = new RelayCommand(
            () => command.Execute(this), 
            () => command.CanExecute(this)); 
    }
}
```

Using the extension method:

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

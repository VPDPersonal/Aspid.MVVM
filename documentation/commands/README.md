---
icon: command
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

# Commands

<mark style="color:$primary;">**Commands**</mark> are the primary way to handle user interactions with the View in the MVVM pattern. They connect actions in the View (e.g., button clicks) to logic implemented in the ViewModel.

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, commands are implemented through the <mark style="color:$warning;">`RelayCommand`</mark> mechanism, which encapsulates:

* The method executed when the command is triggered.
* The method determining whether the command can be executed (<mark style="color:$warning;">`CanExecute`</mark>).

<h2 align="center"><i class="fa-question">:question:</i> How Commands Work</h2>

* A View element (e.g., a button) is bound to a command.
* When the user interacts with the View, the framework invokes the corresponding method in the ViewModel.
* The View element is enabled or disabled based on the result of <mark style="color:$warning;">`CanExecute`</mark>.

<h2 align="center"><i class="fa-overline">:overline:</i> Overloads</h2>

<mark style="color:$primary;">**Aspid.MVVM**</mark> supports five variants of <mark style="color:$warning;">`RelayCommand`</mark>, depending on the number of parameters:

<table><thead><tr><th width="265.2578125">Перегрузка</th><th>Execute</th><th>CanExecute</th></tr></thead><tbody><tr><td><mark style="color:$warning;"><code>RelayCommand</code></mark></td><td>0 parameters</td><td>0 parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1></code></mark></td><td>1 parameter</td><td>1 parameter</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2></code></mark></td><td>2 parameters</td><td>2 parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2, T3></code></mark></td><td>3 parameters</td><td>3 parameters</td></tr><tr><td><mark style="color:$warning;"><code>RelayCommand&#x3C;T1, T2, T3, T4></code></mark></td><td>4 parameters</td><td>4 parameters</td></tr></tbody></table>

### <i class="fa-square-code">:square-code:</i> Example Using All Overloads

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private string _text;

    [Bind] private readonly IRelayCommand _command1;
    [Bind] private readonly IRelayCommand<int> _command2;
    [Bind] private readonly IRelayCommand<int, int> _command3;
    [Bind] private readonly IRelayCommand<int, int, int> _command4;
    [Bind] private readonly IRelayCommand<int, int, int, int> _command5;

    public MyViewModel()
    {
        _command1 = new RelayCommand(Do1, CanDo1);
        _command2 = new RelayCommand<int>(Do2, CanDo2);
        _command3 = new RelayCommand<int, int>(Do3, CanDo3);
        _command4 = new RelayCommand<int, int, int>(Do4, CanDo4);
        _command5 = new RelayCommand<int, int, int, int>(Do5, CanDo5);
    }

    private void Do1() => Text = "Command1";
    private bool CanDo1() => true;

    private void Do2(int a) => Text = $"Command2 {a}";
    private bool CanDo2(int a) => a > 0;

    private void Do3(int a, int b) => Text = $"Command3 {a}, {b}";
    private bool CanDo3(int a, int b) => a + b < 100;

    private void Do4(int a, int b, int c) => Text = $"Command4 {a}, {b}, {c}";
    private bool CanDo4(int a, int b, int c) => c > 0;

    private void Do5(int a, int b, int c, int d) => Text = $"Command5 {a}, {b}, {c}, {d}";
    private bool CanDo5(int a, int b, int c, int d) => true;
}
```

<h2 align="center"><i class="fa-code-branch">:code-branch:</i> Execution Conditions</h2>

The <mark style="color:$warning;">`CanExecute`</mark> method can change dynamically. To notify the View of changes, call:

```csharp
MyCommand.NotifyCanExecuteChanged()
```

This is particularly useful when data affecting the command's availability changes. Example:

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

{% hint style="info" %}
#### Features

* <mark style="color:$primary;">**Encapsulated Logic**</mark>: Execution and validation logic is separated from the View.
* <mark style="color:$primary;">**Flexibility**</mark>: Supports 0 to 4 parameters and works with any View element.
* <mark style="color:$primary;">**Easy Testing**</mark>: Commands can be invoked and tested independently of the View.
* <mark style="color:$primary;">**Clean View Code**</mark>: Eliminates subscriptions and conditions in the View, keeping logic in the ViewModel.
{% endhint %}

* Learn more about the <mark style="color:$warning;">`ICommand`</mark> concept in the [official Microsoft documentation](https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/may/mvvm-commands-relaycommands-and-eventtocommand).&#x20;
* In <mark style="color:$primary;">**Aspid.MVVM**</mark>, <mark style="color:$warning;">`RelayCommand`</mark> implements this interface and extends it with parameter support and generation from the [\[RelayCommand\]](relaycommand.md) attribute.

<h2 align="center"><i class="fa-page">:page:</i> Empty Commands</h2>

If you need an empty command, you can obtain it through the static <mark style="color:$warning;">`Empty`</mark> property of <mark style="color:$warning;">`RelayCommand`</mark> (regardless of the overload).

{% hint style="info" %}
The <mark style="color:$warning;">`Empty`</mark> static property does not create a new instance for each request, improving performance by maintaining a single instance of the empty command throughout the project.
{% endhint %}

{% hint style="warning" %}
The <mark style="color:$warning;">`CanExecute`</mark> method of an empty command always returns <mark style="color:$warning;">`false`</mark>.
{% endhint %}

```csharp
var emptyCommand1 = RelayCommand.Empty;
var emptyCommand2 = RelayCommand<string>.Empty;
```

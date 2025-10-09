---
icon: wand-sparkles
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

# \[RelayCommand]

<mark style="color:$primary;">**The \[RelayCommand] attribute**</mark> is used to automatically generate commands based on methods in the ViewModel. This approach eliminates the need to write boilerplate code manually, simplifying development and reducing the likelihood of errors.

<h2 align="center"><i class="fa-square-code">:square-code:</i> Example</h2>

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private string _text;

    [RelayCommand(CanExecute = nameof(CanDo1))]
    private void Do1() => Text = "Command1";
    private bool CanDo1() => true;

    [RelayCommand(CanExecute = nameof(CanDo2))]
    private void Do2(int arg1) => Text = $"Command2 {arg1}";
    private bool CanDo2(int arg1) => arg1 > 0;

    [RelayCommand(CanExecute = nameof(CanDo3))]
    private void Do3(int a, int b) => Text = $"Command3 {a}, {b}";
    private bool CanDo3(int a, int b) => a + b < 100;

    [RelayCommand(CanExecute = nameof(CanDo4))]
    private void Do4(int a, int b, int c) => Text = $"Command4 {a}, {b}, {c}";
    private bool CanDo4(int a, int b, int c) => c > 0;

    [RelayCommand(CanExecute = nameof(CanDo5))]
    private void Do5(int a, int b, int c, int d) => Text = $"Command5 {a}, {b}, {c}, {d}";
    private bool CanDo5(int a, int b, int c, int d) => true;
}
```

### <i class="fa-gear-code">:gear-code:</i> Abstract Example of Generated Code

Based on the provided code, the generator creates commands that are automatically initialized and linked to the methods defined in the ViewModel. Below is a simplified example of the generated code:

```csharp
public partial class MyViewModel
{
    private RelayCommand _do1Command;
    private RelayCommand Do1Command => _do1Command ??= new(Do1);
        
    private RelayCommand<int> _do2Command;
    private RelayCommand<int> Do2Command => _do2Command ??= new(Do2, () => CanDo2);
        
    private RelayCommand<int, int> _do3Command;
    private RelayCommand<int, int> Do3Command => _do3Command ??= new(Do3, CanDo3);
          
    private RelayCommand<int, int, int> _do4Command;
    private RelayCommand<int, int, int> Do4Command => _do4Command ??= new(Do4, CanDo4);      
 
    private RelayCommand<int, int, int, int> _do5Command;
    private RelayCommand<int, int, int, int> Do5Command => _do5Command ??= new(Do5, CanDo5);
}  
```

<h2 align="center"><i class="fa-octagon-plus">:octagon-plus:</i> Additional Notes</h2>

The <mark style="color:$warning;">`CanExecute`</mark> parameter can also reference properties or parameterless methods, even if the command requires parameters:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    private bool IsActive => true;
    
    private bool CanDo() => true;
    
    [RelayCommand(CanExecute = nameof(IsActive))]
    private void Do1(int a) 
    {
        // Other Code
    }
    
    [RelayCommand(CanExecute = nameof(CanDo))]
    private void Do2(int a) 
    {
        // Other Code
    }
}
```

{% hint style="info" %}
#### Features

* <mark style="color:$primary;">**Automatic Command Generation**</mark>: The <mark style="color:$warning;">`[RelayCommand]`</mark> attribute automatically generates private commands.
* <mark style="color:$primary;">**CanExecute Parameter**</mark>: An optional parameter that specifies a method to check whether the command can be executed. If provided, the generator adds the corresponding logic.
* <mark style="color:$primary;">**Parameterized Commands**</mark>: Supports generating commands with parameters. For example, <mark style="color:$warning;">`Do2Command`</mark> accepts a single <mark style="color:$warning;">`int`</mark> parameter.
{% endhint %}

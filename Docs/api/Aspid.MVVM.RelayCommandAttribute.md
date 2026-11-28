---
title: "Class RelayCommandAttribute"
sidebar_label: "RelayCommandAttribute"
description: "Class RelayCommandAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommandAttribute {#Aspid_MVVM_RelayCommandAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Generates a relay command for the marked method of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class RelayCommandAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[RelayCommandAttribute](Aspid.MVVM.RelayCommandAttribute.md)



## Examples


```csharp
[RelayCommand]
private void Save() { }
```


```csharp
[RelayCommand(CanExecute = nameof(CanBuy))]
private void Buy(int count) { }

private bool CanBuy(int count) => count <= _stock;
```


## Remarks

The generated property uses the method name with a <code>Command</code> suffix. The method must be non-generic,
return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/void">void</a> and take up to four value parameters.

## Fields

### CanExecute {#Aspid_MVVM_RelayCommandAttribute_CanExecute}

The name of a boolean field, readable property or predicate method that allows execution.
The default, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, always allows execution.

```csharp
public string? CanExecute
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

#### Remarks

Method overload resolution, generic type inference and constraints follow C# delegate conversion rules; an invalid predicate reports generator error <code>ASPIDVM004</code>.

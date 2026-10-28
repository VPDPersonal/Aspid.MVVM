---
title: "Class RelayCommandAttribute"
sidebar_label: "RelayCommandAttribute"
description: "Class RelayCommandAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommandAttribute {#Aspid_MVVM_RelayCommandAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Generates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) property, named after the method with a <code>Command</code> suffix, for the method of
a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
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

The method returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/void">void</a> and takes up to four parameters.

## Fields

### CanExecute {#Aspid_MVVM_RelayCommandAttribute_CanExecute}

The name of a [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) method or property of the same class that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to
always allow it.

```csharp
public string? CanExecute
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

#### Remarks

A method takes either the command's parameters or none.


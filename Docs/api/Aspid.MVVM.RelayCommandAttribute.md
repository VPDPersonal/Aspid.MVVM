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

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to methods of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a matching [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) (or one of its generic
overloads, picked by the method's parameter count) that wraps the decorated method.

```csharp
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class RelayCommandAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[RelayCommandAttribute](Aspid.MVVM.RelayCommandAttribute.md)



## Fields

### CanExecute {#Aspid_MVVM_RelayCommandAttribute_CanExecute}

The name of the method that determines whether the command can be executed (CanExecute).
This method must return a value of type <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>.
If not specified, the command can always be executed.

```csharp
public string? CanExecute
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?


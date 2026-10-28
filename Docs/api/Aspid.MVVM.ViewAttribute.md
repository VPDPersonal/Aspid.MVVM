---
title: "Class ViewAttribute"
sidebar_label: "ViewAttribute"
description: "Class ViewAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewAttribute {#Aspid_MVVM_ViewAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Generates the [`IView`](Aspid.MVVM.IView.md) implementation of the class, which binds its binder fields and properties.

```csharp
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct, Inherited = false)]
public sealed class ViewAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[ViewAttribute](Aspid.MVVM.ViewAttribute.md)



## Examples


```csharp
[View]
public partial class PlayerView : MonoView { }
```


## Remarks

The class must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method">partial</a>; a struct is ignored.

## Properties

### AutoBinderFields {#Aspid_MVVM_ViewAttribute_AutoBinderFields}

Gets or sets a value indicating whether a View that implements [`IView<T>`](Aspid.MVVM.IView-1.md) gets a serialized
<code>MonoBinder</code> array for each bindable member of <code>T</code> that it does not declare. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> by default.

```csharp
public bool AutoBinderFields { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Has no effect when <code>T</code> is an interface or the View derives from <code>ScriptableObject</code>.


---
title: "Class ShowDesignViewModelAttribute"
sidebar_label: "ShowDesignViewModelAttribute"
description: "Class ShowDesignViewModelAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ShowDesignViewModelAttribute {#Aspid_MVVM_ShowDesignViewModelAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Configures the design ViewModel selector of a [`MonoView`](Aspid.MVVM.MonoView.md) in the Editor.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ShowDesignViewModelAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[ShowDesignViewModelAttribute](Aspid.MVVM.ShowDesignViewModelAttribute.md)



## Examples


```csharp
[ShowDesignViewModel(typeof(PlayerViewModel), strictType: true)]
public partial class PlayerView : MonoView { }
```


## Constructors

### ShowDesignViewModelAttribute\(\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor}

Creates the attribute allowing any [`IViewModel`](Aspid.MVVM.IViewModel.md) implementation
to be used as a design ViewModel.

```csharp
public ShowDesignViewModelAttribute()
```

### ShowDesignViewModelAttribute\(params Type\[\]\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type___}

Creates a selector filtered by the supplied ViewModel types.

```csharp
public ShowDesignViewModelAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The allowed types; adds [`IViewModel`](Aspid.MVVM.IViewModel.md) if none implements it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

An element of <code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### ShowDesignViewModelAttribute\(Type, bool\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type_System_Boolean_}

Creates the attribute with a single ViewModel type.

```csharp
public ShowDesignViewModelAttribute(Type type, bool strictType = false)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The ViewModel type to show in the design ViewModel selector.

`strictType` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, selects the concrete type and locks the selector.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">type</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Strict mode is requested for a type that is abstract, open generic or does not implement [`IViewModel`](Aspid.MVVM.IViewModel.md).

## Fields

### StrictType {#Aspid_MVVM_ShowDesignViewModelAttribute_StrictType}

Whether the selector is locked to the supplied concrete ViewModel type.

```csharp
public readonly bool StrictType
```

#### Field Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Types {#Aspid_MVVM_ShowDesignViewModelAttribute_Types}

The selectable ViewModel types, including [`IViewModel`](Aspid.MVVM.IViewModel.md) when none implements it.

```csharp
public readonly Type[] Types
```

#### Field Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

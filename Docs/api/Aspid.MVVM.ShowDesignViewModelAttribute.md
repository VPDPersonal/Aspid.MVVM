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

Shows and configures the design ViewModel selector on a [`MonoView`](Aspid.MVVM.MonoView.md) Inspector.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ShowDesignViewModelAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[ShowDesignViewModelAttribute](Aspid.MVVM.ShowDesignViewModelAttribute.md)



## Constructors

### ShowDesignViewModelAttribute\(\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor}

Creates an attribute that allows any [`IViewModel`](Aspid.MVVM.IViewModel.md) implementation.

```csharp
public ShowDesignViewModelAttribute()
```

### ShowDesignViewModelAttribute\(params Type\[\]\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type___}

Creates an attribute with filters for selectable types.

```csharp
public ShowDesignViewModelAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The type filters. Adds [`IViewModel`](Aspid.MVVM.IViewModel.md) if no filter is assignable to it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

An element of <code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### ShowDesignViewModelAttribute\(Type, bool\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type_System_Boolean_}

Creates an attribute with one type filter and an optional lock.

```csharp
public ShowDesignViewModelAttribute(Type type, bool strictType = false)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type filter. Adds [`IViewModel`](Aspid.MVVM.IViewModel.md) if the filter is not assignable to it.

`strictType` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, selects the concrete type and locks the selector.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">type</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">strictType</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and <code class="paramref">type</code> is not a concrete, closed [`IViewModel`](Aspid.MVVM.IViewModel.md) type.

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

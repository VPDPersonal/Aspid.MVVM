---
title: "Class AccessAttribute"
sidebar_label: "AccessAttribute"
description: "Class AccessAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AccessAttribute {#Aspid_MVVM_AccessAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sets the access modifiers of the property generated for a bind field of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
[AttributeUsage(AttributeTargets.Field)]
public sealed class AccessAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AccessAttribute](Aspid.MVVM.AccessAttribute.md)



## Examples


```csharp
[Bind]
[Access(Access.Public)]
private int _health;
```


```csharp
[Bind]
[Access(Get = Access.Public, Set = Access.Protected)]
private int _health;
```


## Remarks

Without it, both accessors are <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/private">private</a>; on a field without a bind attribute, it is ignored.

## Constructors

### AccessAttribute\(Access\) {#Aspid_MVVM_AccessAttribute__ctor_Aspid_MVVM_Access_}

Creates an attribute that applies <code class="paramref">access</code> to both accessors.

```csharp
public AccessAttribute(Access access = Access.Private)
```

#### Parameters

`access` [Access](Aspid.MVVM.Access.md)

The access modifier of the getter and the setter.

## Properties

### Get {#Aspid_MVVM_AccessAttribute_Get}

Gets or sets the access modifier of the getter, overriding the constructor argument.

```csharp
public Access Get { get; set; }
```

#### Property Value

 [Access](Aspid.MVVM.Access.md)

### Set {#Aspid_MVVM_AccessAttribute_Set}

Gets or sets the access modifier of the setter, overriding the constructor argument.

```csharp
public Access Set { get; set; }
```

#### Property Value

 [Access](Aspid.MVVM.Access.md)


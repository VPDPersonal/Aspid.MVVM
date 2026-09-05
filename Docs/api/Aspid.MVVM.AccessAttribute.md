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

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
overrides the default <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/private">private</a> access modifier of the generated property's get and set
accessors. Requires a companion [`BindAttribute`](Aspid.MVVM.BindAttribute.md), [`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md),
[`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), [`OneTimeBindAttribute`](Aspid.MVVM.OneTimeBindAttribute.md), or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md)
on the same field.

```csharp
[AttributeUsage(AttributeTargets.Field)]
public sealed class AccessAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AccessAttribute](Aspid.MVVM.AccessAttribute.md)



## Constructors

### AccessAttribute\(Access\) {#Aspid_MVVM_AccessAttribute__ctor_Aspid_MVVM_Access_}

Sets the access modifier for generated properties. Defaults to [`Access.Private`](Aspid.MVVM.Access.md).

```csharp
public AccessAttribute(Access access = Access.Private)
```

#### Parameters

`access` [Access](Aspid.MVVM.Access.md)

Access modifier for the get and set accessors.

## Properties

### Get {#Aspid_MVVM_AccessAttribute_Get}

Gets or sets the access modifier for the get accessor.

```csharp
public Access Get { get; set; }
```

#### Property Value

 [Access](Aspid.MVVM.Access.md)

### Set {#Aspid_MVVM_AccessAttribute_Set}

Gets or sets the access modifier for the set accessor.

```csharp
public Access Set { get; set; }
```

#### Property Value

 [Access](Aspid.MVVM.Access.md)


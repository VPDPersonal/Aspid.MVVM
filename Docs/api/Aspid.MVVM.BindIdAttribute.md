---
title: "Class BindIdAttribute"
sidebar_label: "BindIdAttribute"
description: "Class BindIdAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindIdAttribute {#Aspid_MVVM_BindIdAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Attribute used to override the binding ID for fields, properties, or [RelayCommand] in a ViewModel and View.

```csharp
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class BindIdAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BindIdAttribute](Aspid.MVVM.BindIdAttribute.md)



## Constructors

### BindIdAttribute\(string\) {#Aspid_MVVM_BindIdAttribute__ctor_System_String_}

Initializes a new instance of the [`BindIdAttribute`](Aspid.MVVM.BindIdAttribute.md) class with a specified ID.

```csharp
public BindIdAttribute(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The binding ID to be associated with the target field, property, or [RelayCommand].

## Fields

### Id {#Aspid_MVVM_BindIdAttribute_Id}

Gets the binding ID associated with the target field, property, or <code>[RelayCommand]</code>.

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)


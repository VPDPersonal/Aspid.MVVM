---
title: "Class HeaderGroupStartAttribute"
sidebar_label: "HeaderGroupStartAttribute"
description: "Class HeaderGroupStartAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupStartAttribute {#Aspid_MVVM_HeaderGroupStartAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only marker that opens a collapsible foldout starting at the decorated binder field.
The foldout continues until either [`HeaderGroupEndAttribute`](Aspid.MVVM.HeaderGroupEndAttribute.md), another
[`HeaderGroupAttribute`](Aspid.MVVM.HeaderGroupAttribute.md) / [`HeaderGroupStartAttribute`](Aspid.MVVM.HeaderGroupStartAttribute.md),
or the end of the inspector list is reached.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class HeaderGroupStartAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[HeaderGroupStartAttribute](Aspid.MVVM.HeaderGroupStartAttribute.md)



## Constructors

### HeaderGroupStartAttribute\(string\) {#Aspid_MVVM_HeaderGroupStartAttribute__ctor_System_String_}

```csharp
public HeaderGroupStartAttribute(string title)
```

#### Parameters

`title` [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### Title {#Aspid_MVVM_HeaderGroupStartAttribute_Title}

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)


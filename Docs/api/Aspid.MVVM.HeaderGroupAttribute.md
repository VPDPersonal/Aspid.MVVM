---
title: "Class HeaderGroupAttribute"
sidebar_label: "HeaderGroupAttribute"
description: "Class HeaderGroupAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupAttribute {#Aspid_MVVM_HeaderGroupAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only marker that places the decorated binder field into a collapsible foldout with
the supplied title. Fields decorated with the same title merge into a single foldout
regardless of declaration order. Unlike [`HeaderGroupStartAttribute`](Aspid.MVVM.HeaderGroupStartAttribute.md), this
attribute does not open a range — subsequent fields without their own grouping fall back to
the surrounding range (or the root, if no enclosing range is open).
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class HeaderGroupAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[HeaderGroupAttribute](Aspid.MVVM.HeaderGroupAttribute.md)



## Constructors

### HeaderGroupAttribute\(string\) {#Aspid_MVVM_HeaderGroupAttribute__ctor_System_String_}

```csharp
public HeaderGroupAttribute(string title)
```

#### Parameters

`title` [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### Title {#Aspid_MVVM_HeaderGroupAttribute_Title}

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)


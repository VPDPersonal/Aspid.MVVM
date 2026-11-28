---
title: "Class HeaderGroupAttribute"
sidebar_label: "HeaderGroupAttribute"
description: "Class HeaderGroupAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupAttribute {#Aspid_MVVM_HeaderGroupAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Places only the decorated member in a named Inspector foldout.

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



## Remarks

Members with the same title share a foldout, including members in a foldout range.

## Constructors

### HeaderGroupAttribute\(string\) {#Aspid_MVVM_HeaderGroupAttribute__ctor_System_String_}

Creates a foldout group for one member.

```csharp
public HeaderGroupAttribute(string title)
```

#### Parameters

`title` [string](https://learn.microsoft.com/dotnet/api/system.string)

The foldout title. Blank titles are ignored.

## Properties

### Title {#Aspid_MVVM_HeaderGroupAttribute_Title}

Gets the foldout title.

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

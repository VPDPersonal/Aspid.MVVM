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

Groups the decorated member under the supplied foldout title.

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



## Examples


```csharp
[HeaderGroup("Stats")]
[Bind] private int _health;
```


## Remarks

Affects only this member; subsequent members keep the current range. Equal titles share a foldout.
Attribute applications are omitted unless <code>DEBUG</code> or <code>UNITY_EDITOR</code> is defined.

## Constructors

### HeaderGroupAttribute\(string\) {#Aspid_MVVM_HeaderGroupAttribute__ctor_System_String_}

Creates a grouping attribute with the supplied title.

```csharp
public HeaderGroupAttribute(string title)
```

#### Parameters

`title` [string](https://learn.microsoft.com/dotnet/api/system.string)

The foldout title; blank titles are ignored.

## Properties

### Title {#Aspid_MVVM_HeaderGroupAttribute_Title}

Gets the foldout title.

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

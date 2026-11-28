---
title: "Class HeaderGroupStartAttribute"
sidebar_label: "HeaderGroupStartAttribute"
description: "Class HeaderGroupStartAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupStartAttribute {#Aspid_MVVM_HeaderGroupStartAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Starts an Inspector foldout range at the decorated member.

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



## Examples


```csharp
[HeaderGroupStart("Stats")]
[Bind] private int _health;
[Bind] private int _stamina;
[HeaderGroupEnd]
[Bind] private string _playerName;
```


## Remarks

The range ends before the next [`HeaderGroupEndAttribute`](Aspid.MVVM.HeaderGroupEndAttribute.md) or [`HeaderGroupStartAttribute`](Aspid.MVVM.HeaderGroupStartAttribute.md) with a title.

## Constructors

### HeaderGroupStartAttribute\(string\) {#Aspid_MVVM_HeaderGroupStartAttribute__ctor_System_String_}

Creates a foldout range with the given title.

```csharp
public HeaderGroupStartAttribute(string title)
```

#### Parameters

`title` [string](https://learn.microsoft.com/dotnet/api/system.string)

The foldout title. Blank titles are ignored.

## Properties

### Title {#Aspid_MVVM_HeaderGroupStartAttribute_Title}

Gets the foldout title.

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

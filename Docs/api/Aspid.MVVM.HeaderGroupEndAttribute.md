---
title: "Class HeaderGroupEndAttribute"
sidebar_label: "HeaderGroupEndAttribute"
description: "Class HeaderGroupEndAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupEndAttribute {#Aspid_MVVM_HeaderGroupEndAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Ends the current foldout range before the decorated member.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class HeaderGroupEndAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[HeaderGroupEndAttribute](Aspid.MVVM.HeaderGroupEndAttribute.md)



## Examples


```csharp
[HeaderGroupEnd]
[Bind] private int _health;
```


## Remarks

The decorated member is displayed outside the closed range.
Attribute applications are omitted unless <code>DEBUG</code> or <code>UNITY_EDITOR</code> is defined.

---
title: "Enum EnumNameSource"
sidebar_label: "EnumNameSource"
description: "Enum EnumNameSource — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum EnumNameSource {#Aspid_MVVM_StarterKit_EnumNameSource}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Where the text naming an enum member comes from.

```csharp
public enum EnumNameSource
```


## Fields

`Name = 0` 

The member name as written in code.



`InspectorName = 1` 

The [`InspectorNameAttribute`](https://docs.unity3d.com/ScriptReference/InspectorNameAttribute.html) on the member, falling back to its name.



`Description = 2` 

The [`DescriptionAttribute`](https://learn.microsoft.com/dotnet/api/system.componentmodel.descriptionattribute) on the member, falling back
to its name.



`Raw = 3` 

The value's own <code>ToString</code>: a flag combination reads as a comma-separated list, an
undeclared value as its number.



## Remarks

Members are appended, never inserted: the order is the serialized value.


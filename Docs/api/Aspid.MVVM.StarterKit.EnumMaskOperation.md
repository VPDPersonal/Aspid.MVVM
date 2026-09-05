---
title: "Enum EnumMaskOperation"
sidebar_label: "EnumMaskOperation"
description: "Enum EnumMaskOperation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum EnumMaskOperation {#Aspid_MVVM_StarterKit_EnumMaskOperation}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`EnumMaskConverter<T>`](Aspid.MVVM.StarterKit.EnumMaskConverter-1.md) does with the flags it is given.

```csharp
public enum EnumMaskOperation
```


## Fields

`And = 0` 

Keep only the flags the mask names.



`Or = 1` 

Add the flags the mask names.



`Xor = 2` 

Flip the flags the mask names.



`Clear = 3` 

Remove the flags the mask names.



## Remarks

Members are appended, never inserted: the order is the serialized value.


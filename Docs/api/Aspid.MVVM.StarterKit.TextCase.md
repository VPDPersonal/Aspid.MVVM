---
title: "Enum TextCase"
sidebar_label: "TextCase"
description: "Enum TextCase — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum TextCase {#Aspid_MVVM_StarterKit_TextCase}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The casing [`TextCaseConverter`](Aspid.MVVM.StarterKit.TextCaseConverter.md) applies.

```csharp
public enum TextCase
```


## Fields

`Upper = 0` 

Every letter upper case.



`Lower = 1` 

Every letter lower case.



`FirstUpper = 2` 

The first letter of the string upper case, the rest untouched.



`Title = 3` 

The first letter of every word upper case, the rest lower.



`Sentence = 4` 

The first letter of every sentence upper case, the rest lower. A sentence ends at <code>.</code>, <code>!</code> or <code>?</code>.



`Invert = 5` 

Every upper-case letter lowered and every lower-case letter raised.



## Remarks

Members are appended, never inserted: the order is the serialized value.


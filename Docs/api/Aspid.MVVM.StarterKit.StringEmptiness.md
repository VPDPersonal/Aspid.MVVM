---
title: "Enum StringEmptiness"
sidebar_label: "StringEmptiness"
description: "Enum StringEmptiness — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum StringEmptiness {#Aspid_MVVM_StarterKit_StringEmptiness}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`StringEmptyToBoolConverter`](Aspid.MVVM.StarterKit.StringEmptyToBoolConverter.md) counts as an absent string.

```csharp
public enum StringEmptiness
```


## Fields

`NullOrEmpty = 0` 

The string is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or has no characters.



`Null = 1` 

The string is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; an empty string counts as present.



`NullOrWhiteSpace = 2` 

The string is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, empty, or made up of whitespace only.



## Remarks

Members are appended, never inserted: the order is the serialized value.


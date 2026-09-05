---
title: "Enum RichTextSanitize"
sidebar_label: "RichTextSanitize"
description: "Enum RichTextSanitize — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum RichTextSanitize {#Aspid_MVVM_StarterKit_RichTextSanitize}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`RichTextSanitizeConverter`](Aspid.MVVM.StarterKit.RichTextSanitizeConverter.md) does with markup it will not let through.

```csharp
public enum RichTextSanitize
```


## Fields

`Strip = 0` 

Remove the tag and keep the surrounding text.



`Escape = 1` 

Keep the tag but show it as text, by wrapping it in <code>&lt;noparse&gt;</code>.



## Remarks

Members are appended, never inserted: the order is the serialized value.


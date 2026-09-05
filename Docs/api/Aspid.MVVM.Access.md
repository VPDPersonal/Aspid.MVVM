---
title: "Enum Access"
sidebar_label: "Access"
description: "Enum Access — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum Access {#Aspid_MVVM_Access}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Defines access modifiers for properties generated from fields decorated with [`BindAttribute`](Aspid.MVVM.BindAttribute.md),
[`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md), [`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), [`OneTimeBindAttribute`](Aspid.MVVM.OneTimeBindAttribute.md),
or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md).
Each value corresponds to a value from <code>Microsoft.CodeAnalysis.CSharp.SyntaxKind</code>.

```csharp
public enum Access
```


## Fields

`Private = 8344` 

The generated property has <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/private">private</a> access.



`Protected = 8346` 

The generated property has <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/protected">protected</a> access.



`Public = 8343` 

The generated property has <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/public">public</a> access.




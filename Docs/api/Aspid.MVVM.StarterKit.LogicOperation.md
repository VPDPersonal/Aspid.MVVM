---
title: "Enum LogicOperation"
sidebar_label: "LogicOperation"
description: "Enum LogicOperation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum LogicOperation {#Aspid_MVVM_StarterKit_LogicOperation}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The boolean operations [`BoolLogicConverter`](Aspid.MVVM.StarterKit.BoolLogicConverter.md) can apply.

```csharp
public enum LogicOperation
```


## Fields

`And = 0` 

Both the bound value and the operand must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.



`Or = 1` 

At least one of the two must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.



`Xor = 2` 

Exactly one of the two must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.



`Nand = 3` 

At least one of the two must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.



`Nor = 4` 

Both must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.



`Xnor = 5` 

The two must be the same.



## Remarks

The order is the serialized value — append new members, never insert or move them.


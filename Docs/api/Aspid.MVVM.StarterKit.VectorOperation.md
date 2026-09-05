---
title: "Enum VectorOperation"
sidebar_label: "VectorOperation"
description: "Enum VectorOperation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum VectorOperation {#Aspid_MVVM_StarterKit_VectorOperation}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The arithmetic [`VectorArithmeticConverter`](Aspid.MVVM.StarterKit.VectorArithmeticConverter.md) can apply.

```csharp
public enum VectorOperation
```


## Fields

`Add = 0` 

Add the operand.



`Subtract = 1` 

Subtract the operand.



`Scale = 2` 

Multiply each axis by the operand's.



`Divide = 3` 

Divide each axis by the operand's. A zero axis is left alone.



`Reflect = 4` 

Reflect off the operand as a normal. The normal is used raw, so one longer than unit
scales the reflected part and a zero one leaves the vector alone.




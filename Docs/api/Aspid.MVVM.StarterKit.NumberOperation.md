---
title: "Enum NumberOperation"
sidebar_label: "NumberOperation"
description: "Enum NumberOperation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum NumberOperation {#Aspid_MVVM_StarterKit_NumberOperation}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The arithmetic [`ArithmeticNumberConverter`](Aspid.MVVM.StarterKit.ArithmeticNumberConverter.md) can apply.

```csharp
public enum NumberOperation
```


## Fields

`Add = 0` 

Add the coefficient.



`Subtract = 1` 

Subtract the coefficient.



`Divide = 2` 

Divide by the coefficient. A zero coefficient falls back.



`Multiply = 3` 

Multiply by the coefficient.



`Modulo = 4` 

The non-negative remainder after dividing by the coefficient. Cannot be undone.



`Power = 5` 

Raise to the power of the coefficient.



`ReverseSubtract = 6` 

Subtract from the coefficient: <code>c - x</code>.



`ReverseDivide = 7` 

Divide the coefficient by the value: <code>c / x</code>. A zero value falls back.



## Remarks

Members are appended, never inserted: the order is the serialized value.


---
title: "Enum UnaryMathOperation"
sidebar_label: "UnaryMathOperation"
description: "Enum UnaryMathOperation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum UnaryMathOperation {#Aspid_MVVM_StarterKit_UnaryMathOperation}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The single-argument functions [`UnaryMathConverter`](Aspid.MVVM.StarterKit.UnaryMathConverter.md) can apply.

```csharp
public enum UnaryMathOperation
```


## Fields

`Abs = 0` 

The distance from zero.



`Negate = 1` 

The value with its sign flipped.



`Sign = 2` 

-1, 0 or 1.



`Sqrt = 3` 

The square root. A negative value yields zero rather than NaN.



`Reciprocal = 4` 

One divided by the value. Zero yields zero rather than infinity.



`Log = 5` 

The natural logarithm. A non-positive value yields zero.



`Log10 = 6` 

The base-10 logarithm. A non-positive value yields zero.



`Exp = 7` 

e raised to the value.



`Sin = 8` 

The sine, in radians.



`Cos = 9` 

The cosine, in radians.



`Tan = 10` 

The tangent, in radians.



`Log2 = 11` 

The base-2 logarithm. A non-positive value yields zero.



`Asin = 12` 

The arcsine, in radians. The value is clamped to -1..1 first.



`Acos = 13` 

The arccosine, in radians. The value is clamped to -1..1 first.



`Atan = 14` 

The arctangent, in radians.




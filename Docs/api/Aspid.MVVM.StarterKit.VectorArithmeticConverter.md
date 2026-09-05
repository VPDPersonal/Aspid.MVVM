---
title: "Class VectorArithmeticConverter"
sidebar_label: "VectorArithmeticConverter"
description: "Class VectorArithmeticConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorArithmeticConverter {#Aspid_MVVM_StarterKit_VectorArithmeticConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Combines a bound vector with an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Arithmetic", Tooltip = "Combines a bound vector with an authored one")]
public sealed class VectorArithmeticConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorArithmeticConverter](Aspid.MVVM.StarterKit.VectorArithmeticConverter.md)

#### Implements

[IConverter\<Vector2, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector3, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector4, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### VectorArithmeticConverter\(\) {#Aspid_MVVM_StarterKit_VectorArithmeticConverter__ctor}

```csharp
public VectorArithmeticConverter()
```

#### Remarks

Default: adds a zero vector, leaving the value unchanged.

### VectorArithmeticConverter\(VectorOperation, Vector4\) {#Aspid_MVVM_StarterKit_VectorArithmeticConverter__ctor_Aspid_MVVM_StarterKit_VectorOperation_UnityEngine_Vector4_}

```csharp
public VectorArithmeticConverter(VectorOperation operation, Vector4 operand)
```

#### Parameters

`operation` [VectorOperation](Aspid.MVVM.StarterKit.VectorOperation.md)

What to do with the operand.

`operand` Vector4

The vector the bound one is combined with. Only the components the bound vector carries
are read.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorArithmeticConverter_Convert_UnityEngine_Vector3_}

Combines the specified vector with the authored operand.

```csharp
public Vector3 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to combine.

#### Returns

 Vector3

The combined vector. Reports an error and returns the value unchanged when the operation
is not a declared value.


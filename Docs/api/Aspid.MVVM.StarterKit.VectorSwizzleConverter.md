---
title: "Class VectorSwizzleConverter"
sidebar_label: "VectorSwizzleConverter"
description: "Class VectorSwizzleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorSwizzleConverter {#Aspid_MVVM_StarterKit_VectorSwizzleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reorders the components of a vector.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Swizzle", Tooltip = "Reorders the components of a vector")]
public sealed class VectorSwizzleConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorSwizzleConverter](Aspid.MVVM.StarterKit.VectorSwizzleConverter.md)

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

## Remarks

A narrower vector reads only the slots it has, and a slot naming a component that width does
not carry is reported and passed through unchanged.

## Constructors

### VectorSwizzleConverter\(\) {#Aspid_MVVM_StarterKit_VectorSwizzleConverter__ctor}

```csharp
public VectorSwizzleConverter()
```

#### Remarks

Default: identity, every component keeps its slot.

### VectorSwizzleConverter\(Vector4Component, Vector4Component, Vector4Component, Vector4Component\) {#Aspid_MVVM_StarterKit_VectorSwizzleConverter__ctor_Aspid_MVVM_StarterKit_Vector4Component_Aspid_MVVM_StarterKit_Vector4Component_Aspid_MVVM_StarterKit_Vector4Component_Aspid_MVVM_StarterKit_Vector4Component_}

```csharp
public VectorSwizzleConverter(Vector4Component x, Vector4Component y, Vector4Component z, Vector4Component w)
```

#### Parameters

`x` [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which incoming component the X of the result is read from. A component the bound vector
does not carry is reported and X passes through unchanged.

`y` [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which incoming component the Y of the result is read from. A component the bound vector
does not carry is reported and Y passes through unchanged.

`z` [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which incoming component the Z of the result is read from. A component the bound vector
does not carry is reported and Z passes through unchanged.

`w` [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which incoming component the W of the result is read from. A component the bound vector
does not carry is reported and W passes through unchanged.

## Methods

### Convert\(Vector4\) {#Aspid_MVVM_StarterKit_VectorSwizzleConverter_Convert_UnityEngine_Vector4_}

Reorders the specified vector.

```csharp
public Vector4 Convert(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to reorder.

#### Returns

 Vector4

The reordered vector. A slot naming a component that is not a declared
[`Vector4Component`](Aspid.MVVM.StarterKit.Vector4Component.md) value reports an error and passes its own component
through unchanged.


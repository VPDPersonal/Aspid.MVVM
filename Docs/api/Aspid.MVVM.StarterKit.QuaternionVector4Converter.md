---
title: "Class QuaternionVector4Converter"
sidebar_label: "QuaternionVector4Converter"
description: "Class QuaternionVector4Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class QuaternionVector4Converter {#Aspid_MVVM_StarterKit_QuaternionVector4Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a rotation as its four raw numbers, and builds one back out of them.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Quaternion/To Vector", Name = "To Vector4", Tooltip = "Reads a rotation as its four raw numbers, and builds one back out of them")]
public sealed class QuaternionVector4Converter : ITwoWayConverter<Quaternion, Vector4>, IConverter<Quaternion, Vector4>, ITwoWayConverter<Vector4, Quaternion>, IConverter<Vector4, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[QuaternionVector4Converter](Aspid.MVVM.StarterKit.QuaternionVector4Converter.md)

#### Implements

[ITwoWayConverter\<Quaternion, Vector4\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Quaternion, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector4, Quaternion\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector4, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### QuaternionVector4Converter\(\) {#Aspid_MVVM_StarterKit_QuaternionVector4Converter__ctor}

```csharp
public QuaternionVector4Converter()
```

#### Remarks

Default: normalizing what it builds.

### QuaternionVector4Converter\(bool\) {#Aspid_MVVM_StarterKit_QuaternionVector4Converter__ctor_System_Boolean_}

```csharp
public QuaternionVector4Converter(bool normalize)
```

#### Parameters

`normalize` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to scale a rotation built from four numbers back to unit length.

## Methods

### Convert\(Quaternion\) {#Aspid_MVVM_StarterKit_QuaternionVector4Converter_Convert_UnityEngine_Quaternion_}

Reads the specified rotation as four numbers.

```csharp
public Vector4 Convert(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to read.

#### Returns

 Vector4

The four numbers, in x, y, z, w order.

### ConvertBack\(Vector4\) {#Aspid_MVVM_StarterKit_QuaternionVector4Converter_ConvertBack_UnityEngine_Vector4_}

Builds a rotation out of the specified numbers.

```csharp
public Quaternion ConvertBack(Vector4 value)
```

#### Parameters

`value` Vector4

The four numbers, in x, y, z, w order.

#### Returns

 Quaternion

The rotation, or the identity for four zeroes when normalizing.


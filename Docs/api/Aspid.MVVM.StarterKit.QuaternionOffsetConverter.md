---
title: "Class QuaternionOffsetConverter"
sidebar_label: "QuaternionOffsetConverter"
description: "Class QuaternionOffsetConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class QuaternionOffsetConverter {#Aspid_MVVM_StarterKit_QuaternionOffsetConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies a fixed rotation on top of a bound one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Quaternion", Name = "Offset", Tooltip = "Applies a fixed rotation on top of a bound one")]
public sealed class QuaternionOffsetConverter : ITwoWayConverter<Quaternion, Quaternion>, IConverter<Quaternion, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[QuaternionOffsetConverter](Aspid.MVVM.StarterKit.QuaternionOffsetConverter.md)

#### Implements

[ITwoWayConverter\<Quaternion, Quaternion\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Quaternion, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### QuaternionOffsetConverter\(\) {#Aspid_MVVM_StarterKit_QuaternionOffsetConverter__ctor}

```csharp
public QuaternionOffsetConverter()
```

#### Remarks

Default: no offset.

### QuaternionOffsetConverter\(Vector3, bool\) {#Aspid_MVVM_StarterKit_QuaternionOffsetConverter__ctor_UnityEngine_Vector3_System_Boolean_}

```csharp
public QuaternionOffsetConverter(Vector3 offsetEuler, bool applyFirst = false)
```

#### Parameters

`offsetEuler` Vector3

The rotation applied on top of the bound one, in Euler degrees.

`applyFirst` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to apply the offset before the bound rotation.

## Methods

### Convert\(Quaternion\) {#Aspid_MVVM_StarterKit_QuaternionOffsetConverter_Convert_UnityEngine_Quaternion_}

Applies the offset to the specified rotation.

```csharp
public Quaternion Convert(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to adjust.

#### Returns

 Quaternion

The adjusted rotation.

### ConvertBack\(Quaternion\) {#Aspid_MVVM_StarterKit_QuaternionOffsetConverter_ConvertBack_UnityEngine_Quaternion_}

Removes the offset from the specified rotation.

```csharp
public Quaternion ConvertBack(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to adjust.

#### Returns

 Quaternion

The rotation without the offset.


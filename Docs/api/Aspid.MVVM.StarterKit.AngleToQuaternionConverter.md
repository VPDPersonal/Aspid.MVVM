---
title: "Class AngleToQuaternionConverter"
sidebar_label: "AngleToQuaternionConverter"
description: "Class AngleToQuaternionConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AngleToQuaternionConverter {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Turns a single angle into a rotation.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Quaternion", Name = "Angle To Quaternion", Tooltip = "Turns a single angle into a rotation")]
public sealed class AngleToQuaternionConverter : ITwoWayConverter<float, Quaternion>, IConverter<float, Quaternion>, ITwoWayConverter<double, Quaternion>, IConverter<double, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AngleToQuaternionConverter](Aspid.MVVM.StarterKit.AngleToQuaternionConverter.md)

#### Implements

[ITwoWayConverter\<float, Quaternion\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, Quaternion\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### AngleToQuaternionConverter\(\) {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter__ctor}

```csharp
public AngleToQuaternionConverter()
```

#### Remarks

Default: turning around Z.

### AngleToQuaternionConverter\(RotationAxis, float, bool\) {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter__ctor_Aspid_MVVM_StarterKit_RotationAxis_System_Single_System_Boolean_}

```csharp
public AngleToQuaternionConverter(RotationAxis axis, float offset = 0, bool clockwise = false)
```

#### Parameters

`axis` [RotationAxis](Aspid.MVVM.StarterKit.RotationAxis.md)

The axis the angle turns around.

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Added to the angle before it is applied.

`clockwise` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, turns the other way.

### AngleToQuaternionConverter\(Vector3, float, bool\) {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter__ctor_UnityEngine_Vector3_System_Single_System_Boolean_}

```csharp
public AngleToQuaternionConverter(Vector3 customAxis, float offset = 0, bool clockwise = false)
```

#### Parameters

`customAxis` Vector3

The axis the angle turns around. A zero vector reports an error and the rotation turns nowhere.

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Added to the angle before it is applied.

`clockwise` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, turns the other way.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter_Convert_System_Single_}

Turns the specified angle into a rotation.

```csharp
public Quaternion Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees.

#### Returns

 Quaternion

The rotation. An undeclared axis reports an error and turns around Z; a zero custom axis
reports an error and turns nowhere.

### ConvertBack\(Quaternion\) {#Aspid_MVVM_StarterKit_AngleToQuaternionConverter_ConvertBack_UnityEngine_Quaternion_}

Reads the angle back off a rotation.

```csharp
public float ConvertBack(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to read.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees. An undeclared axis reports an error and reads the angle off Z; a
zero custom axis reports an error and reads the angle as zero.


---
title: "Class DirectionAngleConverter"
sidebar_label: "DirectionAngleConverter"
description: "Class DirectionAngleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DirectionAngleConverter {#Aspid_MVVM_StarterKit_DirectionAngleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads the angle a direction points in, and turns an angle back into a direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Number", Name = "Direction To Angle", Tooltip = "Reads the angle a direction points in, and turns an angle back into a direction")]
public sealed class DirectionAngleConverter : ITwoWayConverter<Vector2, float>, IConverter<Vector2, float>, IConverter<Vector2, double>, ITwoWayConverter<float, Vector2>, IConverter<float, Vector2>, IConverter<double, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DirectionAngleConverter](Aspid.MVVM.StarterKit.DirectionAngleConverter.md)

#### Implements

[ITwoWayConverter\<Vector2, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector2, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector2, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<float, Vector2\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Both directions read the same unit, offset and winding, so the round trip returns the angle it
was given; only the length of the direction is the reverse pass's own setting.

## Constructors

### DirectionAngleConverter\(\) {#Aspid_MVVM_StarterKit_DirectionAngleConverter__ctor}

```csharp
public DirectionAngleConverter()
```

#### Remarks

Default: reporting degrees.

### DirectionAngleConverter\(float, bool, bool, float\) {#Aspid_MVVM_StarterKit_DirectionAngleConverter__ctor_System_Single_System_Boolean_System_Boolean_System_Single_}

```csharp
public DirectionAngleConverter(float offset, bool clockwise = false, bool degrees = true, float magnitude = 1)
```

#### Parameters

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Added to the angle, in the unit the angle is reported in.

`clockwise` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to measure clockwise.

`degrees` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to report the angle in degrees rather than radians. When omitted, degrees.

`magnitude` [float](https://learn.microsoft.com/dotnet/api/system.single)

How long a direction built from an angle is. When omitted, one.

## Methods

### Convert\(Vector2\) {#Aspid_MVVM_StarterKit_DirectionAngleConverter_Convert_UnityEngine_Vector2_}

Reads the angle of the specified direction.

```csharp
public float Convert(Vector2 value)
```

#### Parameters

`value` Vector2

The direction to read.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle. A direction shorter than Unity's 1e-5 length floor reads as the offset alone.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_DirectionAngleConverter_ConvertBack_System_Single_}

Turns the specified angle back into a direction.

```csharp
public Vector2 ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle.

#### Returns

 Vector2

The direction, as long as the authored magnitude.


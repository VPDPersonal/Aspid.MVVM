---
title: "Class Vector3Vector4Converter"
sidebar_label: "Vector3Vector4Converter"
description: "Class Vector3Vector4Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector3Vector4Converter {#Aspid_MVVM_StarterKit_Vector3Vector4Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Widens a vector to four components, and narrows one back by dropping a component.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 To Vector4", Tooltip = "Widens a vector to four components, and narrows one back by dropping a component")]
public sealed class Vector3Vector4Converter : ITwoWayConverter<Vector3, Vector4>, IConverter<Vector3, Vector4>, ITwoWayConverter<Vector4, Vector3>, IConverter<Vector4, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector3Vector4Converter](Aspid.MVVM.StarterKit.Vector3Vector4Converter.md)

#### Implements

[ITwoWayConverter\<Vector3, Vector4\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector3, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector4, Vector3\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector4, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The round trip returns the vector it was given only while the dropped component is the one the
widening wrote.

## Constructors

### Vector3Vector4Converter\(\) {#Aspid_MVVM_StarterKit_Vector3Vector4Converter__ctor}

```csharp
public Vector3Vector4Converter()
```

#### Remarks

Default: writing a zero fourth component and dropping it again.

### Vector3Vector4Converter\(float, Vector4Component\) {#Aspid_MVVM_StarterKit_Vector3Vector4Converter__ctor_System_Single_Aspid_MVVM_StarterKit_Vector4Component_}

```csharp
public Vector3Vector4Converter(float w, Vector4Component drop = Vector4Component.W)
```

#### Parameters

`w` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value written into the fourth component.

`drop` [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which component is left out on the way back. When omitted, the fourth one.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_Vector3Vector4Converter_Convert_UnityEngine_Vector3_}

Widens the specified vector.

```csharp
public Vector4 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to widen.

#### Returns

 Vector4

The four-component vector.

### ConvertBack\(Vector4\) {#Aspid_MVVM_StarterKit_Vector3Vector4Converter_ConvertBack_UnityEngine_Vector4_}

Narrows the specified vector by dropping the configured component.

```csharp
public Vector3 ConvertBack(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to narrow.

#### Returns

 Vector3

The three-component vector. Reports an error and drops W when the component is not a
declared [`Vector4Component`](Aspid.MVVM.StarterKit.Vector4Component.md) value.


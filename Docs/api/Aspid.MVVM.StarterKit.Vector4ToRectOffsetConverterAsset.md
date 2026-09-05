---
title: "Class Vector4ToRectOffsetConverterAsset"
sidebar_label: "Vector4ToRectOffsetConverterAsset"
description: "Class Vector4ToRectOffsetConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector4ToRectOffsetConverterAsset {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Rect Offset Converter", fileName = "Vector4ToRectOffsetConverter")]
public sealed class Vector4ToRectOffsetConverterAsset : ConverterAsset<Vector4, RectOffset?>, IConverter<Vector4, RectOffset?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Vector4, RectOffset?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Vector4ToRectOffsetConverterAsset](Aspid.MVVM.StarterKit.Vector4ToRectOffsetConverterAsset.md)

#### Implements

[IConverter\<Vector4, RectOffset?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


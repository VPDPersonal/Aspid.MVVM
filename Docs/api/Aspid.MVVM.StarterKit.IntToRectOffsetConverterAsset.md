---
title: "Class IntToRectOffsetConverterAsset"
sidebar_label: "IntToRectOffsetConverterAsset"
description: "Class IntToRectOffsetConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class IntToRectOffsetConverterAsset {#Aspid_MVVM_StarterKit_IntToRectOffsetConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) to [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Int To Rect Offset Converter", fileName = "IntToRectOffsetConverter")]
public sealed class IntToRectOffsetConverterAsset : ConverterAsset<int, RectOffset?>, IConverter<int, RectOffset?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<int, RectOffset?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[IntToRectOffsetConverterAsset](Aspid.MVVM.StarterKit.IntToRectOffsetConverterAsset.md)

#### Implements

[IConverter\<int, RectOffset?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


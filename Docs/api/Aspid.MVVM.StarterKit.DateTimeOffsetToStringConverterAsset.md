---
title: "Class DateTimeOffsetToStringConverterAsset"
sidebar_label: "DateTimeOffsetToStringConverterAsset"
description: "Class DateTimeOffsetToStringConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeOffsetToStringConverterAsset {#Aspid_MVVM_StarterKit_DateTimeOffsetToStringConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time Offset To String Converter", fileName = "DateTimeOffsetToStringConverter")]
public sealed class DateTimeOffsetToStringConverterAsset : ConverterAsset<DateTimeOffset, string?>, IConverter<DateTimeOffset, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<DateTimeOffset, string?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[DateTimeOffsetToStringConverterAsset](Aspid.MVVM.StarterKit.DateTimeOffsetToStringConverterAsset.md)

#### Implements

[IConverter\<DateTimeOffset, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


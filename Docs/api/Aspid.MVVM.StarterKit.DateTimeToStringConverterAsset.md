---
title: "Class DateTimeToStringConverterAsset"
sidebar_label: "DateTimeToStringConverterAsset"
description: "Class DateTimeToStringConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeToStringConverterAsset {#Aspid_MVVM_StarterKit_DateTimeToStringConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To String Converter", fileName = "DateTimeToStringConverter")]
public sealed class DateTimeToStringConverterAsset : ConverterAsset<DateTime, string?>, IConverter<DateTime, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<DateTime, string?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[DateTimeToStringConverterAsset](Aspid.MVVM.StarterKit.DateTimeToStringConverterAsset.md)

#### Implements

[IConverter\<DateTime, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


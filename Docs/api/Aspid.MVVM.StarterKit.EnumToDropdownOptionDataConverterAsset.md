---
title: "Class EnumToDropdownOptionDataConverterAsset"
sidebar_label: "EnumToDropdownOptionDataConverterAsset"
description: "Class EnumToDropdownOptionDataConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToDropdownOptionDataConverterAsset {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for boxed [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) values turned into
dropdown options.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Enum/Enum To Dropdown Options Converter", fileName = "EnumToDropdownOptionDataConverter")]
public sealed class EnumToDropdownOptionDataConverterAsset : ConverterAsset<Enum?, IEnumerable<TMP_Dropdown.OptionData>?>, IConverter<Enum?, IEnumerable<TMP_Dropdown.OptionData>?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Enum?, IEnumerable\<TMP\_Dropdown.OptionData\>?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[EnumToDropdownOptionDataConverterAsset](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverterAsset.md)

#### Implements

[IConverter\<Enum?, IEnumerable\<TMP\_Dropdown.OptionData\>?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


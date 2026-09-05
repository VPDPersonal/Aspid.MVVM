---
title: "Class EnumToDropdownOptionDataConverterAsset<T>"
sidebar_label: "EnumToDropdownOptionDataConverterAsset<T>"
description: "Class EnumToDropdownOptionDataConverterAsset<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToDropdownOptionDataConverterAsset\<T\> {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverterAsset_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) base for a concrete enum type turned into dropdown
options. Unity cannot create an asset of an open generic, so subclass with
<code class="typeparamref">T</code> closed.

```csharp
public abstract class EnumToDropdownOptionDataConverterAsset<T> : ConverterAsset<T, IEnumerable<TMP_Dropdown.OptionData>?>, IConverter<T, IEnumerable<TMP_Dropdown.OptionData>?>, IConverter where T : struct, Enum
```

#### Type Parameters

`T` 

The enum type the converter works over.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<T, IEnumerable\<TMP\_Dropdown.OptionData\>?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[EnumToDropdownOptionDataConverterAsset\<T\>](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverterAsset-1.md)

#### Implements

[IConverter\<T, IEnumerable\<TMP\_Dropdown.OptionData\>?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)


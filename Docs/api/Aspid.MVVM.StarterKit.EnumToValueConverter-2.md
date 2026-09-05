---
title: "Class EnumToValueConverter<TEnum, T>"
sidebar_label: "EnumToValueConverter<TEnum, T>"
description: "Class EnumToValueConverter<TEnum, T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToValueConverter\<TEnum, T\> {#Aspid_MVVM_StarterKit_EnumToValueConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Maps an enum value to an authored value.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To Value", Name = "To Value", Tooltip = "Maps an enum value to an authored value")]
public class EnumToValueConverter<TEnum, T> : DictionaryLookupConverter<TEnum, T>, IConverter<TEnum, T?>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being mapped from.

`T` 

The type being mapped to.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DictionaryLookupConverter\<TEnum, T\>](Aspid.MVVM.StarterKit.DictionaryLookupConverter-2.md) ← 
[EnumToValueConverter\<TEnum, T\>](Aspid.MVVM.StarterKit.EnumToValueConverter-2.md)

#### Implements

[IConverter\<TEnum, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### EnumToValueConverter\(\) {#Aspid_MVVM_StarterKit_EnumToValueConverter_2__ctor}

```csharp
public EnumToValueConverter()
```

#### Remarks

Default: an empty map.

### EnumToValueConverter\(LookupEntry\<TEnum, T?\>\[\]?, T?\) {#Aspid_MVVM_StarterKit_EnumToValueConverter_2__ctor_Aspid_MVVM_StarterKit_LookupEntry__0__1_____1_}

```csharp
public EnumToValueConverter(LookupEntry<TEnum, T?>[]? map, T? fallback = default)
```

#### Parameters

`map` [LookupEntry](Aspid.MVVM.StarterKit.LookupEntry-2.md)\<TEnum, T?\>\[\]?

The value for each member. A duplicate member is reported, its first row wins. The array is copied.

`fallback` T?

Returned for a member <code class="paramref">map</code> does not list.


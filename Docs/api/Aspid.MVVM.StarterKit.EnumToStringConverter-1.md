---
title: "Class EnumToStringConverter<TEnum>"
sidebar_label: "EnumToStringConverter<TEnum>"
description: "Class EnumToStringConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToStringConverter\<TEnum\> {#Aspid_MVVM_StarterKit_EnumToStringConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts an enum value to text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To String", Name = "To String", Tooltip = "Converts an enum value to text")]
public class EnumToStringConverter<TEnum> : IConverter<TEnum, string>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being converted.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumToStringConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumToStringConverter-1.md)

#### Implements

[IConverter\<TEnum, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

There is no culture setting: <code>Enum.ToString</code> ignores any format provider.

## Constructors

### EnumToStringConverter\(\) {#Aspid_MVVM_StarterKit_EnumToStringConverter_1__ctor}

```csharp
public EnumToStringConverter()
```

#### Remarks

Default: the member name as written in code.

### EnumToStringConverter\(EnumNameSource, string?\) {#Aspid_MVVM_StarterKit_EnumToStringConverter_1__ctor_Aspid_MVVM_StarterKit_EnumNameSource_System_String_}

```csharp
public EnumToStringConverter(EnumNameSource source, string? fallback = null)
```

#### Parameters

`source` [EnumNameSource](Aspid.MVVM.StarterKit.EnumNameSource.md)

Where the text comes from.

`fallback` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Returned for an undeclared member, flag combinations included. Unused under [`EnumNameSource.Raw`](Aspid.MVVM.StarterKit.EnumNameSource.md).
When omitted, an empty string.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_EnumToStringConverter_1_Convert__0_}

Converts the specified enum value to text.

```csharp
public string Convert(TEnum value)
```

#### Parameters

`value` TEnum

The enum value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The member's text, or the fallback for an undeclared member. [`EnumNameSource.Raw`](Aspid.MVVM.StarterKit.EnumNameSource.md) writes any value as <code>ToString</code> does.


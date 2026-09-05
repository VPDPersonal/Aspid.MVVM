---
title: "Class StringToDateTimeConverter"
sidebar_label: "StringToDateTimeConverter"
description: "Class StringToDateTimeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToDateTimeConverter {#Aspid_MVVM_StarterKit_StringToDateTimeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a date out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Time", Name = "Parse Date Time", Tooltip = "Reads a date out of text")]
public sealed class StringToDateTimeConverter : ITwoWayConverter<string?, DateTime>, IConverter<string?, DateTime>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToDateTimeConverter](Aspid.MVVM.StarterKit.StringToDateTimeConverter.md)

#### Implements

[ITwoWayConverter\<string?, DateTime\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, DateTime\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### StringToDateTimeConverter\(\) {#Aspid_MVVM_StarterKit_StringToDateTimeConverter__ctor}

```csharp
public StringToDateTimeConverter()
```

#### Remarks

Default: accepting any format.

### StringToDateTimeConverter\(string, DateTime?\) {#Aspid_MVVM_StarterKit_StringToDateTimeConverter__ctor_System_String_System_Nullable_System_DateTime__}

```csharp
public StringToDateTimeConverter(string format, DateTime? fallback = null)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exact format for reading and writing. Empty accepts any format the culture understands.

`fallback` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

Returned when the text is not a date. When omitted, [`MinValue`](https://learn.microsoft.com/dotnet/api/system.datetime.minvalue).

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToDateTimeConverter_Convert_System_String_}

Reads a date out of the specified text.

```csharp
public DateTime Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date, or the fallback when the text is not one.

### ConvertBack\(DateTime\) {#Aspid_MVVM_StarterKit_StringToDateTimeConverter_ConvertBack_System_DateTime_}

Writes the specified date as text.

```csharp
public string ConvertBack(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The date in the authored format, or in the culture's general format when none is authored, or it is unusable.


---
title: "Class StringToEnumConverter<TEnum>"
sidebar_label: "StringToEnumConverter<TEnum>"
description: "Class StringToEnumConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToEnumConverter\<TEnum\> {#Aspid_MVVM_StarterKit_StringToEnumConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads an enum member out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Enum", Name = "Parse Enum", Tooltip = "Reads an enum member out of text")]
public class StringToEnumConverter<TEnum> : ITwoWayConverter<string?, TEnum>, IConverter<string?, TEnum>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being read.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.StringToEnumConverter-1.md)

#### Implements

[ITwoWayConverter\<string?, TEnum\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### StringToEnumConverter\(\) {#Aspid_MVVM_StarterKit_StringToEnumConverter_1__ctor}

```csharp
public StringToEnumConverter()
```

#### Remarks

Default: falling back to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

### StringToEnumConverter\(TEnum?, bool\) {#Aspid_MVVM_StarterKit_StringToEnumConverter_1__ctor_System_Nullable__0__System_Boolean_}

```csharp
public StringToEnumConverter(TEnum? fallback = null, bool ignoreCase = true)
```

#### Parameters

`fallback` TEnum?

Returned when the text names no member. When omitted, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

`ignoreCase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to match without regard to case.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToEnumConverter_1_Convert_System_String_}

Reads an enum member out of the specified text.

```csharp
public TEnum Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 TEnum

The member, a combination of declared flags for a flags enum, or the fallback when the text names none.

### ConvertBack\(TEnum\) {#Aspid_MVVM_StarterKit_StringToEnumConverter_1_ConvertBack__0_}

Writes the specified member as text.

```csharp
public string ConvertBack(TEnum value)
```

#### Parameters

`value` TEnum

The member to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

Its name, or the comma-separated names of its flags. An undeclared value writes as its number.


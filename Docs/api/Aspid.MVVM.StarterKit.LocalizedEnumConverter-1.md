---
title: "Class LocalizedEnumConverter<TEnum>"
sidebar_label: "LocalizedEnumConverter<TEnum>"
description: "Class LocalizedEnumConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocalizedEnumConverter\<TEnum\> {#Aspid_MVVM_StarterKit_LocalizedEnumConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Looks an enum member's name up in a localization table.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Localization", Name = "Localized Enum", Tooltip = "Looks an enum member's name up in a localization table")]
public class LocalizedEnumConverter<TEnum> : IConverter<TEnum, string>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being localized.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LocalizedEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.LocalizedEnumConverter-1.md)

#### Implements

[IConverter\<TEnum, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### LocalizedEnumConverter\(\) {#Aspid_MVVM_StarterKit_LocalizedEnumConverter_1__ctor}

```csharp
protected LocalizedEnumConverter()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### LocalizedEnumConverter\(LocalizedStringTable, string?, bool\) {#Aspid_MVVM_StarterKit_LocalizedEnumConverter_1__ctor_UnityEngine_Localization_LocalizedStringTable_System_String_System_Boolean_}

```csharp
public LocalizedEnumConverter(LocalizedStringTable table, string? keyPrefix = null, bool fallbackToName = true)
```

#### Parameters

`table` LocalizedStringTable

The string table the keys are looked up in.

`keyPrefix` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Placed before the member name to form the key.

`fallbackToName` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, a member with no entry shows its name; otherwise it shows the
key. Either way the miss is reported as an error.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">table</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_LocalizedEnumConverter_1_Convert__0_}

Looks the specified member up.

```csharp
public string Convert(TEnum value)
```

#### Parameters

`value` TEnum

The member to localize.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The localized text; otherwise the member name, or the key when the fallback to the name is
off. Every miss is reported as an error, including the one where no table is assigned.


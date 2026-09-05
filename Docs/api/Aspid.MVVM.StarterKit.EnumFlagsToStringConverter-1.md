---
title: "Class EnumFlagsToStringConverter<TEnum>"
sidebar_label: "EnumFlagsToStringConverter<TEnum>"
description: "Class EnumFlagsToStringConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumFlagsToStringConverter\<TEnum\> {#Aspid_MVVM_StarterKit_EnumFlagsToStringConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Names the flags a value carries.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To String", Name = "Flags", Tooltip = "Names the flags a value carries")]
public class EnumFlagsToStringConverter<TEnum> : IConverter<TEnum, string>, IConverter, ISerializationCallbackReceiver where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being named.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumFlagsToStringConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumFlagsToStringConverter-1.md)

#### Implements

[IConverter\<TEnum, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md), 
ISerializationCallbackReceiver


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A composite member is named only when its parts are not declared members. A non-flags enum is named whole.

## Constructors

### EnumFlagsToStringConverter\(\) {#Aspid_MVVM_StarterKit_EnumFlagsToStringConverter_1__ctor}

```csharp
public EnumFlagsToStringConverter()
```

#### Remarks

Default: joining with commas.

### EnumFlagsToStringConverter\(string, EnumNameSource, string\) {#Aspid_MVVM_StarterKit_EnumFlagsToStringConverter_1__ctor_System_String_Aspid_MVVM_StarterKit_EnumNameSource_System_String_}

```csharp
public EnumFlagsToStringConverter(string separator, EnumNameSource source = EnumNameSource.Name, string noneText = "")
```

#### Parameters

`separator` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between the named flags. Unused on an enum not marked [`FlagsAttribute`](https://learn.microsoft.com/dotnet/api/system.flagsattribute).

`source` [EnumNameSource](Aspid.MVVM.StarterKit.EnumNameSource.md)

Where the name of each flag comes from.

`noneText` [string](https://learn.microsoft.com/dotnet/api/system.string)

Shown when the value names no flags.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_EnumFlagsToStringConverter_1_Convert__0_}

Names the flags the specified value carries.

```csharp
public string Convert(TEnum value)
```

#### Parameters

`value` TEnum

The value to take apart.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The flag names joined by the separator, or the none text. Undeclared bits are dropped.


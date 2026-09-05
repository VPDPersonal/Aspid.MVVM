---
title: "Class NumberToEnumConverter<TEnum>"
sidebar_label: "NumberToEnumConverter<TEnum>"
description: "Class NumberToEnumConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NumberToEnumConverter\<TEnum\> {#Aspid_MVVM_StarterKit_NumberToEnumConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a number to the enum value it stands for.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Enum", Name = "Number To Enum", Tooltip = "Converts a number to the enum value it stands for")]
public class NumberToEnumConverter<TEnum> : IConverter<int, TEnum>, IConverter<long, TEnum>, IConverter<float, TEnum>, IConverter<double, TEnum>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being converted to.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberToEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.NumberToEnumConverter-1.md)

#### Implements

[IConverter\<int, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A number naming no member is refused; a flags enum accepts any combination of declared flags.

## Constructors

### NumberToEnumConverter\(\) {#Aspid_MVVM_StarterKit_NumberToEnumConverter_1__ctor}

```csharp
public NumberToEnumConverter()
```

#### Remarks

Default: reading the underlying value.

### NumberToEnumConverter\(bool, TEnum\) {#Aspid_MVVM_StarterKit_NumberToEnumConverter_1__ctor_System_Boolean__0_}

```csharp
public NumberToEnumConverter(bool byIndexNotValue, TEnum fallback = default)
```

#### Parameters

`byIndexNotValue` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, reads the number as a member's position.

`fallback` TEnum

Returned for a number that names no member.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_NumberToEnumConverter_1_Convert_System_Int32_}

Converts the specified number to an enum value.

```csharp
public TEnum Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to convert.

#### Returns

 TEnum

The enum value, or the fallback for a number that names no member.


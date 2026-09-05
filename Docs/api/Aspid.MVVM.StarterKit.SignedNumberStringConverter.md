---
title: "Class SignedNumberStringConverter"
sidebar_label: "SignedNumberStringConverter"
description: "Class SignedNumberStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SignedNumberStringConverter {#Aspid_MVVM_StarterKit_SignedNumberStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number with an explicit sign: "+15", "-3".

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Signed", Tooltip = "Formats a number with an explicit sign: '+15', '-3'")]
public sealed class SignedNumberStringConverter : IConverter<float, string>, IConverter<int, string>, IConverter<long, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SignedNumberStringConverter](Aspid.MVVM.StarterKit.SignedNumberStringConverter.md)

#### Implements

[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### SignedNumberStringConverter\(\) {#Aspid_MVVM_StarterKit_SignedNumberStringConverter__ctor}

```csharp
public SignedNumberStringConverter()
```

#### Remarks

Default: showing a plus on positive numbers.

### SignedNumberStringConverter\(string, bool\) {#Aspid_MVVM_StarterKit_SignedNumberStringConverter__ctor_System_String_System_Boolean_}

```csharp
public SignedNumberStringConverter(string format, bool hideZero = false)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A numeric format string for the magnitude. One .NET refuses falls back to the general format.

`hideZero` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, returns an empty string for zero.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_SignedNumberStringConverter_Convert_System_Single_}

Formats the specified number with its sign.

```csharp
public string Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted number, or the general rendering when the format is unusable.


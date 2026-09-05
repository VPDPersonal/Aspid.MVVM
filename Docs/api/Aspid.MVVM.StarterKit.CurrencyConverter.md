---
title: "Class CurrencyConverter"
sidebar_label: "CurrencyConverter"
description: "Class CurrencyConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CurrencyConverter {#Aspid_MVVM_StarterKit_CurrencyConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number as an amount of currency.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Currency", Tooltip = "Formats a number as an amount of currency")]
public sealed class CurrencyConverter : IConverter<double, string>, IConverter<int, string>, IConverter<long, string>, IConverter<float, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CurrencyConverter](Aspid.MVVM.StarterKit.CurrencyConverter.md)

#### Implements

[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### CurrencyConverter\(\) {#Aspid_MVVM_StarterKit_CurrencyConverter__ctor}

```csharp
public CurrencyConverter()
```

#### Remarks

Default: with a leading dollar sign.

### CurrencyConverter\(string, SymbolPosition, int\) {#Aspid_MVVM_StarterKit_CurrencyConverter__ctor_System_String_Aspid_MVVM_StarterKit_SymbolPosition_System_Int32_}

```csharp
public CurrencyConverter(string symbol, SymbolPosition position = SymbolPosition.Before, int decimals = 0)
```

#### Parameters

`symbol` [string](https://learn.microsoft.com/dotnet/api/system.string)

The symbol placed beside the amount.

`position` [SymbolPosition](Aspid.MVVM.StarterKit.SymbolPosition.md)

Which side of the amount the symbol goes on.

`decimals` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many decimals to show.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">decimals</code> is negative.

## Methods

### Convert\(double\) {#Aspid_MVVM_StarterKit_CurrencyConverter_Convert_System_Double_}

Formats the specified amount.

```csharp
public string Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The amount.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted amount with its symbol; a negative keeps the sign in front.


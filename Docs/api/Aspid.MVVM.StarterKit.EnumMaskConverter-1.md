---
title: "Class EnumMaskConverter<TEnum>"
sidebar_label: "EnumMaskConverter<TEnum>"
description: "Class EnumMaskConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumMaskConverter\<TEnum\> {#Aspid_MVVM_StarterKit_EnumMaskConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Combines a bound flags value with an authored mask.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Mask", Tooltip = "Combines a bound flags value with an authored mask")]
public class EnumMaskConverter<TEnum> : IConverter<TEnum, TEnum>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being combined.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumMaskConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumMaskConverter-1.md)

#### Implements

[IConverter\<TEnum, TEnum\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### EnumMaskConverter\(\) {#Aspid_MVVM_StarterKit_EnumMaskConverter_1__ctor}

```csharp
protected EnumMaskConverter()
```

### EnumMaskConverter\(TEnum, EnumMaskOperation, ConverterFallback\<TEnum\>?\) {#Aspid_MVVM_StarterKit_EnumMaskConverter_1__ctor__0_Aspid_MVVM_StarterKit_EnumMaskOperation_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback__0___}

```csharp
public EnumMaskConverter(TEnum mask, EnumMaskOperation operation = EnumMaskOperation.And, ConverterFallback<TEnum>? fallback = null)
```

#### Parameters

`mask` TEnum

The flags the bound value is combined with.

`operation` [EnumMaskOperation](Aspid.MVVM.StarterKit.EnumMaskOperation.md)

What is done with the flags <code class="paramref">mask</code> names.

`fallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<TEnum\>?

Returned when the enum is not marked [`FlagsAttribute`](https://learn.microsoft.com/dotnet/api/system.flagsattribute) or the operation is
undeclared. When omitted, returns the value unchanged.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_EnumMaskConverter_1_Convert__0_}

Applies the authored mask to the specified value.

```csharp
public TEnum Convert(TEnum value)
```

#### Parameters

`value` TEnum

The value to combine with the mask.

#### Returns

 TEnum

The combined value, not necessarily a declared member, or the fallback for a non-flags enum or an undeclared operation.


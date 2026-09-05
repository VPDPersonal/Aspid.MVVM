---
title: "Class EnumToNumberConverter<TEnum>"
sidebar_label: "EnumToNumberConverter<TEnum>"
description: "Class EnumToNumberConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToNumberConverter\<TEnum\> {#Aspid_MVVM_StarterKit_EnumToNumberConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts an enum value to a number and back.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To Number", Name = "To Number", Tooltip = "Converts an enum value to a number and back")]
public class EnumToNumberConverter<TEnum> : IConverter<TEnum, float>, IConverter<TEnum, double>, ITwoWayConverter<TEnum, int>, IConverter<TEnum, int>, ITwoWayConverter<TEnum, long>, IConverter<TEnum, long>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being converted.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumToNumberConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumToNumberConverter-1.md)

#### Implements

[IConverter\<TEnum, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<TEnum, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<TEnum, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<TEnum, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<TEnum, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<TEnum, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Read as a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>: the int overloads saturate, float and double lose precision past their range.

## Constructors

### EnumToNumberConverter\(\) {#Aspid_MVVM_StarterKit_EnumToNumberConverter_1__ctor}

```csharp
public EnumToNumberConverter()
```

#### Remarks

Default: reading the underlying value.

### EnumToNumberConverter\(bool, TEnum?, int?\) {#Aspid_MVVM_StarterKit_EnumToNumberConverter_1__ctor_System_Boolean_System_Nullable__0__System_Nullable_System_Int32__}

```csharp
public EnumToNumberConverter(bool byIndexNotValue, TEnum? fallback = null, int? indexFallback = null)
```

#### Parameters

`byIndexNotValue` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, uses the member's position in the enum instead of its
underlying value.

`fallback` TEnum?

Returned for a position outside the enum. Unused while the position mode is off. When
omitted, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

`indexFallback` [int](https://learn.microsoft.com/dotnet/api/system.int32)?

Returned for a value that is not a declared member. Unused while the position mode is
off. When omitted, <code>-1</code>.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_EnumToNumberConverter_1_Convert__0_}

Converts the specified enum value to an integer.

```csharp
public int Convert(TEnum value)
```

#### Parameters

`value` TEnum

The enum value to convert.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The underlying number or the member's position; the index fallback for an undeclared member. Saturates to [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32).

### ConvertBack\(int\) {#Aspid_MVVM_StarterKit_EnumToNumberConverter_1_ConvertBack_System_Int32_}

Converts an integer back to the enum value it stands for.

```csharp
public TEnum ConvertBack(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The integer to convert.

#### Returns

 TEnum

The enum value, not necessarily a declared member, or the fallback for a position outside the enum.


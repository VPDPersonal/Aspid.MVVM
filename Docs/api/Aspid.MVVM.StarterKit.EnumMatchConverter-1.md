---
title: "Class EnumMatchConverter<TEnum>"
sidebar_label: "EnumMatchConverter<TEnum>"
description: "Class EnumMatchConverter<TEnum> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumMatchConverter\<TEnum\> {#Aspid_MVVM_StarterKit_EnumMatchConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Tests an enum value against an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To Bool", Name = "Match", Tooltip = "Tests an enum value against an authored one")]
public class EnumMatchConverter<TEnum> : IConverter<TEnum, bool>, IConverter where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type being tested.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumMatchConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumMatchConverter-1.md)

#### Implements

[IConverter\<TEnum, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The flag tests are pure bit math: on an enum not marked [`FlagsAttribute`](https://learn.microsoft.com/dotnet/api/system.flagsattribute) they
compare bit patterns nobody authored as flags.

## Constructors

### EnumMatchConverter\(\) {#Aspid_MVVM_StarterKit_EnumMatchConverter_1__ctor}

```csharp
public EnumMatchConverter()
```

#### Remarks

Default: testing equality against the enum's default value.

### EnumMatchConverter\(TEnum, EnumMatchMode, bool, bool\) {#Aspid_MVVM_StarterKit_EnumMatchConverter_1__ctor__0_Aspid_MVVM_StarterKit_EnumMatchMode_System_Boolean_System_Boolean_}

```csharp
public EnumMatchConverter(TEnum target, EnumMatchMode mode = EnumMatchMode.Equal, bool isInvert = false, bool fallback = false)
```

#### Parameters

`target` TEnum

The enum value the bound one is tested against.

`mode` [EnumMatchMode](Aspid.MVVM.StarterKit.EnumMatchMode.md)

How the bound value is tested against <code class="paramref">target</code>.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

`fallback` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Returned, without inverting, when the mode is undeclared. When omitted, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

## Methods

### Convert\(TEnum\) {#Aspid_MVVM_StarterKit_EnumMatchConverter_1_Convert__0_}

Tests the specified enum value against the authored one.

```csharp
public bool Convert(TEnum value)
```

#### Parameters

`value` TEnum

The enum value to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result, inverted when configured; an undeclared mode returns the fallback without inverting it.


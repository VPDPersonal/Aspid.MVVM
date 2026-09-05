---
title: "Class BoolToValueConverter<T>"
sidebar_label: "BoolToValueConverter<T>"
description: "Class BoolToValueConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoolToValueConverter\<T\> {#Aspid_MVVM_StarterKit_BoolToValueConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Picks one of two authored values based on a boolean, and reads the boolean back out of them.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Bool/To Value", Name = "Bool To Value", Tooltip = "Picks one of two authored values based on a boolean")]
public class BoolToValueConverter<T> : ITwoWayConverter<bool, T?>, IConverter<bool, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the values to pick between.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BoolToValueConverter\<T\>](Aspid.MVVM.StarterKit.BoolToValueConverter-1.md)

#### Implements

[ITwoWayConverter\<bool, T?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<bool, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The reverse direction matches by default equality, so the two branches have to differ.

## Constructors

### BoolToValueConverter\(\) {#Aspid_MVVM_StarterKit_BoolToValueConverter_1__ctor}

```csharp
protected BoolToValueConverter()
```

### BoolToValueConverter\(T, T, bool\) {#Aspid_MVVM_StarterKit_BoolToValueConverter_1__ctor__0__0_System_Boolean_}

```csharp
public BoolToValueConverter(T trueValue, T falseValue, bool convertBackFallback = false)
```

#### Parameters

`trueValue` T

Returned when the bound value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

`falseValue` T

Returned when the bound value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

`convertBackFallback` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Returned when [`BoolToValueConverter<T>.ConvertBack`](Aspid.MVVM.StarterKit.BoolToValueConverter-1.md#Aspid_MVVM_StarterKit_BoolToValueConverter_1_ConvertBack__0_) meets a value matching neither branch, nor when both
branches hold the same value. When omitted, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

## Methods

### Convert\(bool\) {#Aspid_MVVM_StarterKit_BoolToValueConverter_1_Convert_System_Boolean_}

Picks the value authored for the specified boolean.

```csharp
public T? Convert(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The bound boolean.

#### Returns

 T?

The value authored for that branch.

### ConvertBack\(T?\) {#Aspid_MVVM_StarterKit_BoolToValueConverter_1_ConvertBack__0_}

Reads the boolean back out of the specified value.

```csharp
public bool ConvertBack(T? value)
```

#### Parameters

`value` T?

The value to match against the two authored ones.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> when the value matches the branch
authored for it; otherwise, the fallback.


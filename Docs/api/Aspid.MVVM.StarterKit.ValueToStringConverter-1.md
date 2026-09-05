---
title: "Class ValueToStringConverter<T>"
sidebar_label: "ValueToStringConverter<T>"
description: "Class ValueToStringConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueToStringConverter\<T\> {#Aspid_MVVM_StarterKit_ValueToStringConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes a value as text, with optional formatting.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Object/To String", Name = "Value To String", Tooltip = "Writes a value as text, with optional formatting")]
public class ValueToStringConverter<T> : IConverter<T?, string?>, IConverter
```

#### Type Parameters

`T` 

The type of the value to convert.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ValueToStringConverter\<T\>](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md)

#### Implements

[IConverter\<T?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The format is a composite format string: <code>"\{0:F2\}"</code> formats the value, a bare <code>"F2"</code> is a literal.

## Constructors

### ValueToStringConverter\(\) {#Aspid_MVVM_StarterKit_ValueToStringConverter_1__ctor}

```csharp
public ValueToStringConverter()
```

#### Remarks

Default: [`ToString`](https://learn.microsoft.com/dotnet/api/system.object.tostring) in the device locale.

### ValueToStringConverter\(string?, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ValueToStringConverter_1__ctor_System_String_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public ValueToStringConverter(string? format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Composite format string such as <code>"\{0:F2\}"</code>. A bare <code>"F2"</code> is a literal.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture numbers and dates are formatted with.

## Properties

### Culture {#Aspid_MVVM_StarterKit_ValueToStringConverter_1_Culture}

Gets the culture the value is formatted with.

```csharp
protected CultureInfo Culture { get; }
```

#### Property Value

 [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

### FormatString {#Aspid_MVVM_StarterKit_ValueToStringConverter_1_FormatString}

Gets the composite format string, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected string? FormatString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### Convert\(T?\) {#Aspid_MVVM_StarterKit_ValueToStringConverter_1_Convert__0_}

Converts the specified value to a string using the configured format.

```csharp
public virtual string? Convert(T? value)
```

#### Parameters

`value` T?

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The formatted value, [`ToString`](https://learn.microsoft.com/dotnet/api/system.object.tostring) when the format is blank or invalid,
or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value.

### Format\(T, string\) {#Aspid_MVVM_StarterKit_ValueToStringConverter_1_Format__0_System_String_}

Applies the format. Called only when the format is not blank; override to change how it is applied.

```csharp
protected virtual string Format(T value, string format)
```

#### Parameters

`value` T

The non-null value to format.

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

The composite format string, never blank.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted string.

#### Remarks

A [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception) thrown here is routed to [`ValueToStringConverter<T>.HandleFormatError`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md);
every other exception propagates.

### HandleFormatError\(T, Exception\) {#Aspid_MVVM_StarterKit_ValueToStringConverter_1_HandleFormatError__0_System_Exception_}

Called when [`ValueToStringConverter<T>.Format`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md) throws a [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). Override to change the fallback.

```csharp
protected virtual string? HandleFormatError(T value, Exception exception)
```

#### Parameters

`value` T

The non-null value that failed to format.

`exception` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception thrown by [`ValueToStringConverter<T>.Format`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md).

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

[`ToString`](https://learn.microsoft.com/dotnet/api/system.object.tostring), or the type name when that throws too.


---
title: "Class CachedConverter<TFrom, TTo>"
sidebar_label: "CachedConverter<TFrom, TTo>"
description: "Class CachedConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CachedConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_CachedConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Remembers the last conversion and reuses it while the input is unchanged.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Cached", Tooltip = "Remembers the last conversion and reuses it while the input is unchanged")]
public class CachedConverter<TFrom, TTo> : ITwoWayConverter<TFrom?, TTo?>, IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CachedConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CachedConverter-2.md)

#### Implements

[ITwoWayConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Wrap only a pure converter. Inputs are compared by default equality, so a reference mutated
in place counts as unchanged. The two directions cache separately.

## Constructors

### CachedConverter\(\) {#Aspid_MVVM_StarterKit_CachedConverter_2__ctor}

```csharp
protected CachedConverter()
```

### CachedConverter\(IConverter\<TFrom?, TTo?\>\) {#Aspid_MVVM_StarterKit_CachedConverter_2__ctor_Aspid_MVVM_StarterKit_IConverter__0__1__}

```csharp
public CachedConverter(IConverter<TFrom?, TTo?> inner)
```

#### Parameters

`inner` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TTo?\>

The converter to memoize.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">inner</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_CachedConverter_2_Convert__0_}

Converts the specified value, reusing the previous result when the input is unchanged.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value, or the default value when the inner converter is missing.

### ConvertBack\(TTo?\) {#Aspid_MVVM_StarterKit_CachedConverter_2_ConvertBack__1_}

Converts the specified value back, reusing the previous result when the input is unchanged.

```csharp
public TFrom? ConvertBack(TTo? value)
```

#### Parameters

`value` TTo?

The value to convert back.

#### Returns

 TFrom?

The value converted back, or the default value when the inner converter is missing or
converts one way only.


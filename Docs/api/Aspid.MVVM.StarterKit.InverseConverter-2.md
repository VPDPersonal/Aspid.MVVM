---
title: "Class InverseConverter<TFrom, TTo>"
sidebar_label: "InverseConverter<TFrom, TTo>"
description: "Class InverseConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InverseConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_InverseConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Runs a two-way converter in the opposite direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Inverse", Tooltip = "Runs a two-way converter in the opposite direction")]
public class InverseConverter<TFrom, TTo> : ITwoWayConverter<TTo?, TFrom?>, IConverter<TTo?, TFrom?>, IConverter
```

#### Type Parameters

`TFrom` 

The type the wrapped converter converts from.

`TTo` 

The type the wrapped converter converts to.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InverseConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.InverseConverter-2.md)

#### Implements

[ITwoWayConverter\<TTo?, TFrom?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<TTo?, TFrom?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### InverseConverter\(\) {#Aspid_MVVM_StarterKit_InverseConverter_2__ctor}

```csharp
protected InverseConverter()
```

### InverseConverter\(ITwoWayConverter\<TFrom?, TTo?\>\) {#Aspid_MVVM_StarterKit_InverseConverter_2__ctor_Aspid_MVVM_StarterKit_ITwoWayConverter__0__1__}

```csharp
public InverseConverter(ITwoWayConverter<TFrom?, TTo?> converter)
```

#### Parameters

`converter` [ITwoWayConverter](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md)\<TFrom?, TTo?\>

The two-way converter to run in the opposite direction.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TTo?\) {#Aspid_MVVM_StarterKit_InverseConverter_2_Convert__1_}

Converts the specified value with the wrapped converter's reverse direction.

```csharp
public TFrom? Convert(TTo? value)
```

#### Parameters

`value` TTo?

The value to convert.

#### Returns

 TFrom?

What the wrapped converter's <code>ConvertBack</code> answers, or the default value when the
converter is missing.

### ConvertBack\(TFrom?\) {#Aspid_MVVM_StarterKit_InverseConverter_2_ConvertBack__0_}

Converts a value back with the wrapped converter's forward direction.

```csharp
public TTo? ConvertBack(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert back.

#### Returns

 TTo?

What the wrapped converter's <code>Convert</code> answers, or the default value when the
converter is missing.


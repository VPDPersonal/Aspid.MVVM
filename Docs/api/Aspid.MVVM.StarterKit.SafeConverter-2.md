---
title: "Class SafeConverter<TFrom, TTo>"
sidebar_label: "SafeConverter<TFrom, TTo>"
description: "Class SafeConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SafeConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_SafeConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Runs another converter and substitutes a fallback value if it throws.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Safe", Tooltip = "Runs another converter and substitutes a fallback value if it throws")]
public class SafeConverter<TFrom, TTo> : ITwoWayConverter<TFrom?, TTo?>, IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SafeConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.SafeConverter-2.md)

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

Catches every exception: a throwing converter would stop the binders queued behind it.

## Constructors

### SafeConverter\(\) {#Aspid_MVVM_StarterKit_SafeConverter_2__ctor}

```csharp
protected SafeConverter()
```

### SafeConverter\(IConverter\<TFrom?, TTo?\>, TTo?, TFrom?\) {#Aspid_MVVM_StarterKit_SafeConverter_2__ctor_Aspid_MVVM_StarterKit_IConverter__0__1___1__0_}

```csharp
public SafeConverter(IConverter<TFrom?, TTo?> inner, TTo? fallback = default, TFrom? convertBackFallback = default)
```

#### Parameters

`inner` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TTo?\>

The converter to run.

`fallback` TTo?

Returned from [`SafeConverter<T1, T2>.Convert`](Aspid.MVVM.StarterKit.SafeConverter-2.md#Aspid_MVVM_StarterKit_SafeConverter_2_Convert__0_) when <code class="paramref">inner</code> throws.

`convertBackFallback` TFrom?

Returned from [`SafeConverter<T1, T2>.ConvertBack`](Aspid.MVVM.StarterKit.SafeConverter-2.md#Aspid_MVVM_StarterKit_SafeConverter_2_ConvertBack__1_) when <code class="paramref">inner</code> throws or converts one way only.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">inner</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_SafeConverter_2_Convert__0_}

Converts the specified value, substituting the fallback if the wrapped converter throws.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value, or the fallback.

### ConvertBack\(TTo?\) {#Aspid_MVVM_StarterKit_SafeConverter_2_ConvertBack__1_}

Converts the specified value back, substituting the reverse fallback if the wrapped
converter throws.

```csharp
public TFrom? ConvertBack(TTo? value)
```

#### Parameters

`value` TTo?

The value to convert back.

#### Returns

 TFrom?

The value converted back, or the reverse fallback when the converter throws or converts one way only.


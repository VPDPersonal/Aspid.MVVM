---
title: "Class ComposeConverter<TFrom, TMid, TTo>"
sidebar_label: "ComposeConverter<TFrom, TMid, TTo>"
description: "Class ComposeConverter<TFrom, TMid, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ComposeConverter\<TFrom, TMid, TTo\> {#Aspid_MVVM_StarterKit_ComposeConverter_3}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies two converters in sequence, converting through an intermediate type.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Compose", Tooltip = "Applies two converters in sequence, converting through an intermediate type")]
public class ComposeConverter<TFrom, TMid, TTo> : ITwoWayConverter<TFrom?, TTo?>, IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TMid` 

The intermediate type the first converter produces.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ComposeConverter\<TFrom, TMid, TTo\>](Aspid.MVVM.StarterKit.ComposeConverter-3.md)

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

Both links are required: the types on either side need not match, so a missing link leaves
nothing meaningful to return.

## Constructors

### ComposeConverter\(\) {#Aspid_MVVM_StarterKit_ComposeConverter_3__ctor}

```csharp
protected ComposeConverter()
```

### ComposeConverter\(IConverter\<TFrom?, TMid?\>, IConverter\<TMid?, TTo?\>, ConverterFallback\<TFrom?\>?\) {#Aspid_MVVM_StarterKit_ComposeConverter_3__ctor_Aspid_MVVM_StarterKit_IConverter__0__1__Aspid_MVVM_StarterKit_IConverter__1__2__System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback__0___}

```csharp
public ComposeConverter(IConverter<TFrom?, TMid?> first, IConverter<TMid?, TTo?> second, ConverterFallback<TFrom?>? convertBackFallback = null)
```

#### Parameters

`first` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TMid?\>

The converter applied to the input value. Both links are required.

`second` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TMid?, TTo?\>

The converter applied to the result of <code class="paramref">first</code>. Both links are required.

`convertBackFallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<TFrom?\>?

Returned from [`ComposeConverter<T1, T2, T3>.ConvertBack`](Aspid.MVVM.StarterKit.ComposeConverter-3.md#Aspid_MVVM_StarterKit_ComposeConverter_3_ConvertBack__2_) when either link converts one way only.
When omitted, returns the input value unchanged.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">first</code> or <code class="paramref">second</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_ComposeConverter_3_Convert__0_}

Converts the specified value through both links.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The result of the second converter, or the default value when either link is missing.

### ConvertBack\(TTo?\) {#Aspid_MVVM_StarterKit_ComposeConverter_3_ConvertBack__2_}

Undoes the second link, then the first.

```csharp
public TFrom? ConvertBack(TTo? value)
```

#### Parameters

`value` TTo?

The value to convert back.

#### Returns

 TFrom?

The value with both links undone, or the fallback when either one converts one way only
or is missing.

#### Remarks

A one-way link is reported and neither link is undone.


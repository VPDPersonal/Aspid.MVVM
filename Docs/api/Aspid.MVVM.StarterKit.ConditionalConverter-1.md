---
title: "Class ConditionalConverter<T>"
sidebar_label: "ConditionalConverter<T>"
description: "Class ConditionalConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConditionalConverter\<T\> {#Aspid_MVVM_StarterKit_ConditionalConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Routes a value to one of two converters based on a predicate.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Conditional", Tooltip = "Routes a value to one of two converters based on a predicate")]
public class ConditionalConverter<T> : IConverter<T?, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the value being converted.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConditionalConverter\<T\>](Aspid.MVVM.StarterKit.ConditionalConverter-1.md)

#### Implements

[IConverter\<T?, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

An empty branch passes the value through; a branch without a predicate is reported.

## Constructors

### ConditionalConverter\(\) {#Aspid_MVVM_StarterKit_ConditionalConverter_1__ctor}

```csharp
protected ConditionalConverter()
```

### ConditionalConverter\(IConverter\<T?, bool\>, IConverter\<T?, T?\>?, IConverter\<T?, T?\>?\) {#Aspid_MVVM_StarterKit_ConditionalConverter_1__ctor_Aspid_MVVM_StarterKit_IConverter__0_System_Boolean__Aspid_MVVM_StarterKit_IConverter__0__0__Aspid_MVVM_StarterKit_IConverter__0__0__}

```csharp
public ConditionalConverter(IConverter<T?, bool> predicate, IConverter<T?, T?>? then, IConverter<T?, T?>? @else)
```

#### Parameters

`predicate` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

Decides which branch a value takes.

`then` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>?

Applied when the predicate is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. When empty, the value passes through.

`else` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>?

Applied when the predicate is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. When empty, the value passes through.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">predicate</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(T?\) {#Aspid_MVVM_StarterKit_ConditionalConverter_1_Convert__0_}

Converts the specified value using the branch the predicate selects.

```csharp
public T? Convert(T? value)
```

#### Parameters

`value` T?

The value to convert.

#### Returns

 T?

The result of the selected branch, or the value unchanged when that branch or the predicate is empty.


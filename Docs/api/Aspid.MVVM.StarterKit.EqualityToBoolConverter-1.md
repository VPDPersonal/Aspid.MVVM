---
title: "Class EqualityToBoolConverter<T>"
sidebar_label: "EqualityToBoolConverter<T>"
description: "Class EqualityToBoolConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EqualityToBoolConverter\<T\> {#Aspid_MVVM_StarterKit_EqualityToBoolConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Tests a bound value against an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Object/To Bool", Name = "Equals", Tooltip = "Tests a bound value against an authored one")]
public class EqualityToBoolConverter<T> : IConverter<T?, bool>, IConverter
```

#### Type Parameters

`T` 

The type of the values being compared.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EqualityToBoolConverter\<T\>](Aspid.MVVM.StarterKit.EqualityToBoolConverter-1.md)

#### Implements

[IConverter\<T?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Under value equality an empty operand also matches a destroyed [`Object`](https://docs.unity3d.com/ScriptReference/Object.html),
so the converter doubles as an is-null test. Reference equality compares the instances raw.

## Constructors

### EqualityToBoolConverter\(\) {#Aspid_MVVM_StarterKit_EqualityToBoolConverter_1__ctor}

```csharp
public EqualityToBoolConverter()
```

#### Remarks

Default: comparing by value against an empty operand.

### EqualityToBoolConverter\(T?, bool, bool\) {#Aspid_MVVM_StarterKit_EqualityToBoolConverter_1__ctor__0_System_Boolean_System_Boolean_}

```csharp
public EqualityToBoolConverter(T? operand, bool isInvert = false, bool referenceEquality = false)
```

#### Parameters

`operand` T?

Value to compare with. Empty also matches a destroyed Unity object.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

`referenceEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, compares by instance instead of by value. Ignored for value types.

## Methods

### Convert\(T?\) {#Aspid_MVVM_StarterKit_EqualityToBoolConverter_1_Convert__0_}

Compares the specified value with the authored one.

```csharp
public bool Convert(T? value)
```

#### Parameters

`value` T?

The value to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the two are equal, inverted when configured.


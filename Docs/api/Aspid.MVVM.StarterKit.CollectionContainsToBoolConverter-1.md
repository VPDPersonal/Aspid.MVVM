---
title: "Class CollectionContainsToBoolConverter<T>"
sidebar_label: "CollectionContainsToBoolConverter<T>"
description: "Class CollectionContainsToBoolConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionContainsToBoolConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionContainsToBoolConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reports whether a collection holds a matching item.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Bool", Name = "Contains", Tooltip = "Reports whether a collection holds a matching item")]
public class CollectionContainsToBoolConverter<T> : IConverter<IEnumerable<T?>?, bool>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionContainsToBoolConverter\<T\>](Aspid.MVVM.StarterKit.CollectionContainsToBoolConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### CollectionContainsToBoolConverter\(\) {#Aspid_MVVM_StarterKit_CollectionContainsToBoolConverter_1__ctor}

```csharp
public CollectionContainsToBoolConverter()
```

#### Remarks

Default: looking for the type default, without inverting.

### CollectionContainsToBoolConverter\(T?, bool\) {#Aspid_MVVM_StarterKit_CollectionContainsToBoolConverter_1__ctor__0_System_Boolean_}

```csharp
public CollectionContainsToBoolConverter(T? value, bool isInvert = false)
```

#### Parameters

`value` T?

The item looked for, by equality.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

### CollectionContainsToBoolConverter\(IConverter\<T?, bool\>, bool\) {#Aspid_MVVM_StarterKit_CollectionContainsToBoolConverter_1__ctor_Aspid_MVVM_StarterKit_IConverter__0_System_Boolean__System_Boolean_}

```csharp
public CollectionContainsToBoolConverter(IConverter<T?, bool> match, bool isInvert = false)
```

#### Parameters

`match` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

Decides whether an item counts as a match.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">match</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(IEnumerable\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionContainsToBoolConverter_1_Convert_System_Collections_Generic_IEnumerable__0__}

Looks for a matching item.

```csharp
public bool Convert(IEnumerable<T?>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The collection to search.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether any item matches, inverted when configured. A missing match converter counts as no match.


---
title: "Class ConverterAssetReference<TFrom, TTo>"
sidebar_label: "ConverterAssetReference<TFrom, TTo>"
description: "Class ConverterAssetReference<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConverterAssetReference\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_ConverterAssetReference_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Forwards conversion to a shared [`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Asset", Name = "Converter Asset Reference", Tooltip = "Forwards conversion to a shared ConverterAsset")]
public class ConverterAssetReference<TFrom, TTo> : IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConverterAssetReference\<TFrom, TTo\>](Aspid.MVVM.StarterKit.ConverterAssetReference-2.md)

#### Implements

[IConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A managed reference cannot hold a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), so a converter field points
at the asset through this ordinary converter instead.

## Constructors

### ConverterAssetReference\(\) {#Aspid_MVVM_StarterKit_ConverterAssetReference_2__ctor}

```csharp
protected ConverterAssetReference()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### ConverterAssetReference\(ConverterAsset\<TFrom, TTo\>\) {#Aspid_MVVM_StarterKit_ConverterAssetReference_2__ctor_Aspid_MVVM_StarterKit_ConverterAsset__0__1__}

```csharp
public ConverterAssetReference(ConverterAsset<TFrom, TTo> asset)
```

#### Parameters

`asset` [ConverterAsset](Aspid.MVVM.StarterKit.ConverterAsset-2.md)\<TFrom, TTo\>

The shared converter asset.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">asset</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_ConverterAssetReference_2_Convert__0_}

Converts the specified value using the referenced asset.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value, or the default value when no asset is assigned or the assigned asset
has been destroyed. Both are reported as errors, every time.


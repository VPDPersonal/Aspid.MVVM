---
title: "Class ConverterAsset<TFrom, TTo>"
sidebar_label: "ConverterAsset<TFrom, TTo>"
description: "Class ConverterAsset<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConverterAsset\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_ConverterAsset_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

A converter authored once as an asset and shared by every field that references it.

```csharp
public abstract class ConverterAsset<TFrom, TTo> : ScriptableObject, IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<TFrom, TTo\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md)

#### Implements

[IConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A converter field points at the asset through [`ConverterAssetReference<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAssetReference-2.md),
because a managed reference cannot hold a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html). Each usable asset is
a sealed subclass closing the type arguments: Unity cannot create an asset of an open generic.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_ConverterAsset_2_Convert__0_}

Converts the specified value using the shared converter.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value, or the default value when the asset holds no converter or its
converter leads back to this asset. Both are reported as errors, every time.


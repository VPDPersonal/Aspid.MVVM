---
title: "Class MaterialInstanceConverter"
sidebar_label: "MaterialInstanceConverter"
description: "Class MaterialInstanceConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MaterialInstanceConverter {#Aspid_MVVM_StarterKit_MaterialInstanceConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Hands a renderer its own copy of a material instead of the shared asset.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Material", Name = "Material Instance", Tooltip = "Hands a renderer its own copy of a material instead of the shared asset")]
public sealed class MaterialInstanceConverter : IConverter<Material?, Material?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MaterialInstanceConverter](Aspid.MVVM.StarterKit.MaterialInstanceConverter.md)

#### Implements

[IConverter\<Material?, Material?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The copy is owned by the converter: it is cached while the source is unchanged and destroyed
when the source changes, creating one per push would leak.

## Constructors

### MaterialInstanceConverter\(\) {#Aspid_MVVM_StarterKit_MaterialInstanceConverter__ctor}

```csharp
public MaterialInstanceConverter()
```

#### Remarks

Default: handing out a copy.

### MaterialInstanceConverter\(bool\) {#Aspid_MVVM_StarterKit_MaterialInstanceConverter__ctor_System_Boolean_}

```csharp
public MaterialInstanceConverter(bool instantiate)
```

#### Parameters

`instantiate` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to return a copy rather than the shared asset.

## Methods

### Convert\(Material?\) {#Aspid_MVVM_StarterKit_MaterialInstanceConverter_Convert_UnityEngine_Material_}

Returns a copy of the specified material.

```csharp
public Material? Convert(Material? value)
```

#### Parameters

`value` Material?

The material to copy.

#### Returns

 Material?

A copy owned by this converter, reused while the source is unchanged; the material itself
when copying is off; or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the material is missing or destroyed.
The previously returned copy is destroyed on the way, so a caller holding on to it is left
with nothing.


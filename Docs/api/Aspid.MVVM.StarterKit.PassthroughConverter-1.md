---
title: "Class PassthroughConverter<T>"
sidebar_label: "PassthroughConverter<T>"
description: "Class PassthroughConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PassthroughConverter\<T\> {#Aspid_MVVM_StarterKit_PassthroughConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Returns its input unchanged.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Passthrough", Tooltip = "Returns its input unchanged")]
public class PassthroughConverter<T> : ITwoWayConverter<T, T>, IConverter<T, T>, IConverter
```

#### Type Parameters

`T` 

The type of the value passing through.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PassthroughConverter\<T\>](Aspid.MVVM.StarterKit.PassthroughConverter-1.md)

#### Implements

[ITwoWayConverter\<T, T\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<T, T\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Methods

### Convert\(T\) {#Aspid_MVVM_StarterKit_PassthroughConverter_1_Convert__0_}

Returns the specified value unchanged.

```csharp
public T Convert(T value)
```

#### Parameters

`value` T

The value to pass through.

#### Returns

 T

The same value.

### ConvertBack\(T\) {#Aspid_MVVM_StarterKit_PassthroughConverter_1_ConvertBack__0_}

Returns the specified value unchanged.

```csharp
public T ConvertBack(T value)
```

#### Parameters

`value` T

The value to pass through.

#### Returns

 T

The same value.


---
title: "Class ObjectNameConverter"
sidebar_label: "ObjectNameConverter"
description: "Class ObjectNameConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObjectNameConverter {#Aspid_MVVM_StarterKit_ObjectNameConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads the name of a Unity object.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Object/To String", Name = "Object Name", Tooltip = "Reads the name of a Unity object")]
public sealed class ObjectNameConverter : IConverter<Object?, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ObjectNameConverter](Aspid.MVVM.StarterKit.ObjectNameConverter.md)

#### Implements

[IConverter\<Object?, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ObjectNameConverter\(\) {#Aspid_MVVM_StarterKit_ObjectNameConverter__ctor}

```csharp
public ObjectNameConverter()
```

#### Remarks

Default: an empty name for a missing object, with the "(Clone)" suffix dropped.

### ObjectNameConverter\(bool, string?\) {#Aspid_MVVM_StarterKit_ObjectNameConverter__ctor_System_Boolean_System_String_}

```csharp
public ObjectNameConverter(bool stripCloneSuffix = true, string? fallback = null)
```

#### Parameters

`stripCloneSuffix` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to drop the "(Clone)" suffix.

`fallback` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Shown when the object is missing, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to show nothing.

## Methods

### Convert\(Object?\) {#Aspid_MVVM_StarterKit_ObjectNameConverter_Convert_UnityEngine_Object_}

Reads the name of the specified object.

```csharp
public string Convert(Object? value)
```

#### Parameters

`value` Object?

The object to name.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

Its name, or the fallback when it is missing or destroyed.


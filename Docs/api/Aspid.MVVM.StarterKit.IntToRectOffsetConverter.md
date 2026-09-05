---
title: "Class IntToRectOffsetConverter"
sidebar_label: "IntToRectOffsetConverter"
description: "Class IntToRectOffsetConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class IntToRectOffsetConverter {#Aspid_MVVM_StarterKit_IntToRectOffsetConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes one number into the chosen sides of a padding.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Rect Offset", Name = "Int To Rect Offset", Tooltip = "Writes one number into the chosen sides of a padding")]
public sealed class IntToRectOffsetConverter : IConverter<int, RectOffset>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[IntToRectOffsetConverter](Aspid.MVVM.StarterKit.IntToRectOffsetConverter.md)

#### Implements

[IConverter\<int, RectOffset\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

One [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) instance is rewritten on every push to avoid allocating, so the
result must not be held onto.

## Constructors

### IntToRectOffsetConverter\(\) {#Aspid_MVVM_StarterKit_IntToRectOffsetConverter__ctor}

```csharp
public IntToRectOffsetConverter()
```

#### Remarks

Default: writing every side.

### IntToRectOffsetConverter\(RectSides\) {#Aspid_MVVM_StarterKit_IntToRectOffsetConverter__ctor_Aspid_MVVM_StarterKit_RectSides_}

```csharp
public IntToRectOffsetConverter(RectSides sides)
```

#### Parameters

`sides` [RectSides](Aspid.MVVM.StarterKit.RectSides.md)

Which sides the number is written into.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_IntToRectOffsetConverter_Convert_System_Int32_}

Writes the specified number into the chosen sides.

```csharp
public RectOffset Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to write.

#### Returns

 RectOffset

The padding. The same instance is returned every call, so copy it if it must outlive the
next push.


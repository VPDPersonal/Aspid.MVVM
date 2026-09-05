---
title: "Class BoolLogicConverter"
sidebar_label: "BoolLogicConverter"
description: "Class BoolLogicConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoolLogicConverter {#Aspid_MVVM_StarterKit_BoolLogicConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Combines a bound boolean with an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Bool", Name = "Logic", Tooltip = "Combines a bound boolean with an authored one")]
public sealed class BoolLogicConverter : ITwoWayConverter<bool, bool>, IConverter<bool, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BoolLogicConverter](Aspid.MVVM.StarterKit.BoolLogicConverter.md)

#### Implements

[ITwoWayConverter\<bool, bool\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<bool, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### BoolLogicConverter\(LogicOperation, bool, ConverterFallback\<bool\>?\) {#Aspid_MVVM_StarterKit_BoolLogicConverter__ctor_Aspid_MVVM_StarterKit_LogicOperation_System_Boolean_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_Boolean___}

```csharp
public BoolLogicConverter(LogicOperation operation, bool operand, ConverterFallback<bool>? fallback = null)
```

#### Parameters

`operation` [LogicOperation](Aspid.MVVM.StarterKit.LogicOperation.md)

How the bound value combines with the operand.

`operand` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The authored value the bound one combines with.

`fallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Returned when the operation is undeclared or cannot be undone.
When omitted, returns the input value unchanged.

## Methods

### Convert\(bool\) {#Aspid_MVVM_StarterKit_BoolLogicConverter_Convert_System_Boolean_}

Combines the specified value with the authored operand.

```csharp
public bool Convert(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The bound boolean.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result of the operation, or the fallback when the operation is undeclared.

### ConvertBack\(bool\) {#Aspid_MVVM_StarterKit_BoolLogicConverter_ConvertBack_System_Boolean_}

Restores the bound value from the combined one.

```csharp
public bool ConvertBack(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The combined boolean coming back from the View.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value the forward pass was given, or the fallback where the operation discards it.

#### Remarks

Only [`LogicOperation.Xor`](Aspid.MVVM.StarterKit.LogicOperation.md) and [`LogicOperation.Xnor`](Aspid.MVVM.StarterKit.LogicOperation.md) undo for either
operand; the other four fall back for one of the two.


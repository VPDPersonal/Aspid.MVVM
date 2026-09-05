---
title: "Class Vector2CombineConverter"
sidebar_label: "Vector2CombineConverter"
description: "Class Vector2CombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector2CombineConverter {#Aspid_MVVM_StarterKit_Vector2CombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Base class for converters that combine a bound 2D vector with one read from a scene
component, taking each axis from one side or the other.

```csharp
[Serializable]
public abstract class Vector2CombineConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md)

#### Derived

[BoxCollider2DOffsetCombineConverter](Aspid.MVVM.StarterKit.BoxCollider2DOffsetCombineConverter.md), 
[BoxCollider2DSizeCombineConverter](Aspid.MVVM.StarterKit.BoxCollider2DSizeCombineConverter.md), 
[RectTransformAnchoredPosition2DCombineConverter](Aspid.MVVM.StarterKit.RectTransformAnchoredPosition2DCombineConverter.md), 
[RectTransformSizeDeltaCombineConverter](Aspid.MVVM.StarterKit.RectTransformSizeDeltaCombineConverter.md), 
[TransformPosition2DCombineConverter](Aspid.MVVM.StarterKit.TransformPosition2DCombineConverter.md)

#### Implements

[IConverter\<Vector2, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector3, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The reference vector is re-read on every conversion, so the unbound axes keep tracking the
component even when something else moves it.

## Constructors

### Vector2CombineConverter\(\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter__ctor}

```csharp
public Vector2CombineConverter()
```

#### Remarks

Default: both components from the bound vector, with no pre- or post-converter.

### Vector2CombineConverter\(Mode\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector2CombineConverter_Mode_}

```csharp
public Vector2CombineConverter(Vector2CombineConverter.Mode mode)
```

#### Parameters

`mode` [Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md)

Which components come from the bound vector.

### Vector2CombineConverter\(Mode, Func\<Vector2, Vector2\>, Func\<Vector2, Vector2\>\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector2CombineConverter_Mode_System_Func_UnityEngine_Vector2_UnityEngine_Vector2__System_Func_UnityEngine_Vector2_UnityEngine_Vector2__}

```csharp
public Vector2CombineConverter(Vector2CombineConverter.Mode mode, Func<Vector2, Vector2> preConverter, Func<Vector2, Vector2> postConverter)
```

#### Parameters

`mode` [Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md)

Which components come from the bound vector.

`preConverter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<Vector2, Vector2\>

Applied to the bound vector before the components are selected.

`postConverter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<Vector2, Vector2\>

Applied to the combined result.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when either function is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>. Use the converter overload with
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave a stage out.

### Vector2CombineConverter\(Mode, IConverter\<Vector2, Vector2\>?, IConverter\<Vector2, Vector2\>?\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector2CombineConverter_Mode_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector2_UnityEngine_Vector2__Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector2_UnityEngine_Vector2__}

```csharp
public Vector2CombineConverter(Vector2CombineConverter.Mode mode, IConverter<Vector2, Vector2>? preConverter, IConverter<Vector2, Vector2>? postConverter)
```

#### Parameters

`mode` [Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md)

Which components come from the bound vector.

`preConverter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector2, Vector2\>?

Applied to the bound vector before the components are selected, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to
leave that stage out.

`postConverter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector2, Vector2\>?

Applied to the combined result, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave that stage out.

## Properties

### Target {#Aspid_MVVM_StarterKit_Vector2CombineConverter_Target}

Gets the scene component [`Vector2CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected abstract Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_Vector2CombineConverter_VectorTo}

Gets the reference vector to combine with. Derived classes must provide this value.

```csharp
protected abstract Vector2 VectorTo { get; }
```

#### Property Value

 Vector2

#### Remarks

Only read once [`Vector2CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_Target) is known to be alive.

## Methods

### Convert\(Vector2\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter_Convert_UnityEngine_Vector2_}

Combines a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) with the reference vector by selecting components.

```csharp
public Vector2 Convert(Vector2 value)
```

#### Parameters

`value` Vector2

The vector to convert.

#### Returns

 Vector2

The combined vector, or the input unchanged when [`Vector2CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_Target) is missing or the
mode is not a declared [`Mode`](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md) value, an error is reported either way.

#### Remarks

The pre-converter never sees the reference vector, and the post-converter runs after the
axis selection, so it can still move an axis the mode took from the reference.

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_Vector2CombineConverter_Convert_UnityEngine_Vector3_}

Combines a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) with the reference vector, dropping its Z.

```csharp
public Vector2 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The 3D vector to convert.

#### Returns

 Vector2

The combined vector, or the narrowed input when [`Vector2CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_Target) is missing or the
mode is not a declared [`Mode`](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md) value, an error is reported either way.


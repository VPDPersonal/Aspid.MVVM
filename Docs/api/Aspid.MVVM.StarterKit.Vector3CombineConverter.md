---
title: "Class Vector3CombineConverter"
sidebar_label: "Vector3CombineConverter"
description: "Class Vector3CombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector3CombineConverter {#Aspid_MVVM_StarterKit_Vector3CombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Base class for converters that combine a bound vector with one read from a scene component,
taking each axis from whichever of the two the configured [`Mode`](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md) names.

```csharp
[Serializable]
public abstract class Vector3CombineConverter : IConverter<Vector3, Vector3>, IConverter<Vector2, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md)

#### Derived

[BoxColliderCenterCombineConverter](Aspid.MVVM.StarterKit.BoxColliderCenterCombineConverter.md), 
[BoxColliderSizeCombineConverter](Aspid.MVVM.StarterKit.BoxColliderSizeCombineConverter.md), 
[CapsuleColliderCenterCombineConverter](Aspid.MVVM.StarterKit.CapsuleColliderCenterCombineConverter.md), 
[RectTransformAnchoredPositionCombineConverter](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionCombineConverter.md), 
[SphereColliderCenterCombineConverter](Aspid.MVVM.StarterKit.SphereColliderCenterCombineConverter.md), 
[TransformEulerAnglesCombineConverter](Aspid.MVVM.StarterKit.TransformEulerAnglesCombineConverter.md), 
[TransformPositionCombineConverter](Aspid.MVVM.StarterKit.TransformPositionCombineConverter.md), 
[TransformScaleCombineConverter](Aspid.MVVM.StarterKit.TransformScaleCombineConverter.md)

#### Implements

[IConverter\<Vector3, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector2, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
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

### Vector3CombineConverter\(\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter__ctor}

```csharp
public Vector3CombineConverter()
```

#### Remarks

Default: all three components from the bound vector, with no pre- or post-converter.

### Vector3CombineConverter\(Mode\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector3CombineConverter_Mode_}

```csharp
public Vector3CombineConverter(Vector3CombineConverter.Mode mode)
```

#### Parameters

`mode` [Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md)

Which components come from the bound vector.

### Vector3CombineConverter\(Mode, Func\<Vector3, Vector3\>, Func\<Vector3, Vector3\>\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector3CombineConverter_Mode_System_Func_UnityEngine_Vector3_UnityEngine_Vector3__System_Func_UnityEngine_Vector3_UnityEngine_Vector3__}

```csharp
public Vector3CombineConverter(Vector3CombineConverter.Mode mode, Func<Vector3, Vector3> preConverter, Func<Vector3, Vector3> postConverter)
```

#### Parameters

`mode` [Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md)

Which components come from the bound vector.

`preConverter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<Vector3, Vector3\>

Applied to the bound vector before the components are selected.

`postConverter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<Vector3, Vector3\>

Applied to the combined result.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when either function is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>. Use the converter overload with
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave a stage out.

### Vector3CombineConverter\(Mode, IConverter\<Vector3, Vector3\>?, IConverter\<Vector3, Vector3\>?\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter__ctor_Aspid_MVVM_StarterKit_Vector3CombineConverter_Mode_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector3_UnityEngine_Vector3__Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector3_UnityEngine_Vector3__}

```csharp
public Vector3CombineConverter(Vector3CombineConverter.Mode mode, IConverter<Vector3, Vector3>? preConverter, IConverter<Vector3, Vector3>? postConverter)
```

#### Parameters

`mode` [Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md).[Mode](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md)

Which components come from the bound vector.

`preConverter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector3, Vector3\>?

Applied to the bound vector before the components are selected, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to
leave that stage out.

`postConverter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector3, Vector3\>?

Applied to the combined result, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave that stage out.

## Properties

### Target {#Aspid_MVVM_StarterKit_Vector3CombineConverter_Target}

Gets the scene component [`Vector3CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected abstract Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_Vector3CombineConverter_VectorTo}

Gets the reference vector to combine with. Derived classes must provide this value.

```csharp
protected abstract Vector3 VectorTo { get; }
```

#### Property Value

 Vector3

#### Remarks

Only read once [`Vector3CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_Target) is known to be alive.

## Methods

### Convert\(Vector2\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter_Convert_UnityEngine_Vector2_}

Converts a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) by combining with the reference vector.

```csharp
public Vector3 Convert(Vector2 value)
```

#### Parameters

`value` Vector2

The 2D vector to convert.

#### Returns

 Vector3

The converted 3D vector, or the widened input when [`Vector3CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_Target) is missing or the
mode is not a declared [`Mode`](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md) value, an error is reported either way.

#### Remarks

The argument widens to <code>(x, y, 0)</code> before the axis selection runs, so a mode naming Z
takes that zero rather than the reference vector's depth.

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_Vector3CombineConverter_Convert_UnityEngine_Vector3_}

Combines a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) with the reference vector by selecting components.

```csharp
public Vector3 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to convert.

#### Returns

 Vector3

The combined vector, or the input unchanged when [`Vector3CombineConverter.Target`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_Target) is missing or the
mode is not a declared [`Mode`](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md) value, an error is reported either way.

#### Remarks

The pre-converter never sees the reference vector, and the post-converter runs after the
axis selection, so it can still move an axis the mode took from the reference.


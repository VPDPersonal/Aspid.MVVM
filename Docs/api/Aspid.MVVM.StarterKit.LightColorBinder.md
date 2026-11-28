---
title: "Class LightColorBinder"
sidebar_label: "LightColorBinder"
description: "Class LightColorBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LightColorBinder {#Aspid_MVVM_StarterKit_LightColorBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/Light-color.html).

```csharp
[Serializable]
public class LightColorBinder : TargetBinder<Light, Color>, IReverseBinder<Color>, IColorBinder, IBinder<Color>, IBinder<string>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<Light\>](Aspid.MVVM.TargetBinder-1.md) ←
[TargetBinder\<Light, Color\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ←
[LightColorBinder](Aspid.MVVM.StarterKit.LightColorBinder.md)

#### Implements

[IReverseBinder\<Color\>](Aspid.MVVM.IReverseBinder-1.md),
[IColorBinder](Aspid.MVVM.StarterKit.IColorBinder.md),
[IBinder\<Color\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\(IBinder?, in BindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\(IBinder?, in UnbindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__)

## Constructors

### LightColorBinder\(\) {#Aspid_MVVM_StarterKit_LightColorBinder__ctor}

```csharp
protected LightColorBinder()
```

#### Remarks

For deserialization only.

### LightColorBinder\(Light, IConverter\<Color, Color\>, BindMode\) {#Aspid_MVVM_StarterKit_LightColorBinder__ctor_UnityEngine_Light_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Color_UnityEngine_Color__Aspid_MVVM_BindMode_}

```csharp
public LightColorBinder(Light target, IConverter<Color, Color> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Light

The target object that exposes the property.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Color, Color\>

The converter applied before the value is written, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Property {#Aspid_MVVM_StarterKit_LightColorBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Color Property { get; set; }
```

#### Property Value

 Color

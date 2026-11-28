---
title: "Class RendererMaterialsColorSwitcherBinder"
sidebar_label: "RendererMaterialsColorSwitcherBinder"
description: "Class RendererMaterialsColorSwitcherBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererMaterialsColorSwitcherBinder {#Aspid_MVVM_StarterKit_RendererMaterialsColorSwitcherBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

```csharp
[Serializable]
public sealed class RendererMaterialsColorSwitcherBinder : SwitcherBinder<Renderer, Color>, IBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<Renderer\>](Aspid.MVVM.TargetBinder-1.md) ←
[SwitcherBinder\<Renderer, Color\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) ←
[RendererMaterialsColorSwitcherBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorSwitcherBinder.md)

#### Implements

[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md),
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

## Remarks

Writes to [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html), so the materials are instanced for this renderer.

## Constructors

### RendererMaterialsColorSwitcherBinder\(Renderer, Color, Color, string, IConverter\<Color, Color\>?, BindMode\) {#Aspid_MVVM_StarterKit_RendererMaterialsColorSwitcherBinder__ctor_UnityEngine_Renderer_UnityEngine_Color_UnityEngine_Color_System_String_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Color_UnityEngine_Color__Aspid_MVVM_BindMode_}

```csharp
public RendererMaterialsColorSwitcherBinder(Renderer target, Color trueValue, Color falseValue, string colorPropertyName = "_BaseColor", IConverter<Color, Color>? converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Renderer

The renderer to bind.

`trueValue` Color

The color applied when the bound value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

`falseValue` Color

The color applied when the bound value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

`colorPropertyName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The shader color property set on every material.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Color, Color\>?

The converter applied to the chosen value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it as-is.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Methods

### OnDetach\(\) {#Aspid_MVVM_StarterKit_RendererMaterialsColorSwitcherBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### SetValue\(Color\) {#Aspid_MVVM_StarterKit_RendererMaterialsColorSwitcherBinder_SetValue_UnityEngine_Color_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected override void SetValue(Color value)
```

#### Parameters

`value` Color

The value to apply.

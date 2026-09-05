---
title: "Class SpriteRendererSpriteBinder"
sidebar_label: "SpriteRendererSpriteBinder"
description: "Class SpriteRendererSpriteBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SpriteRendererSpriteBinder {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html).

```csharp
[Serializable]
public class SpriteRendererSpriteBinder : TargetBinder<SpriteRenderer, Sprite>, IRebindableBinder, IBinder<Sprite>, IReverseBinder<Sprite>, IBinder<Texture2D>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<SpriteRenderer\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<SpriteRenderer, Sprite\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[SpriteRendererSpriteBinder](Aspid.MVVM.StarterKit.SpriteRendererSpriteBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Sprite\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Sprite\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder\<Texture2D\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SpriteRendererSpriteBinder\>\(SpriteRendererSpriteBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SpriteRendererSpriteBinder\>\(SpriteRendererSpriteBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<SpriteRendererSpriteBinder\>\(SpriteRendererSpriteBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

A [`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) is wrapped in a sprite owned by the binder and destroyed on unbind.

## Constructors

### SpriteRendererSpriteBinder\(\) {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder__ctor}

```csharp
protected SpriteRendererSpriteBinder()
```

#### Remarks

For deserialization only.

### SpriteRendererSpriteBinder\(SpriteRenderer, IConverter\<Sprite, Sprite\>, BindMode\) {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder__ctor_UnityEngine_SpriteRenderer_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Sprite_UnityEngine_Sprite__Aspid_MVVM_BindMode_}

```csharp
public SpriteRendererSpriteBinder(SpriteRenderer target, IConverter<Sprite, Sprite> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` SpriteRenderer

The target object that exposes the property.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Sprite, Sprite\>

The converter applied before the value is written, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Property {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Sprite Property { get; set; }
```

#### Property Value

 Sprite

## Methods

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(Texture2D\) {#Aspid_MVVM_StarterKit_SpriteRendererSpriteBinder_SetValue_UnityEngine_Texture2D_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(Texture2D value)
```

#### Parameters

`value` Texture2D

The value received from the ViewModel.


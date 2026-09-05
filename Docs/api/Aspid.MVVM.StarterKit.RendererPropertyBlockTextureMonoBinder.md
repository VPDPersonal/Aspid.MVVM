---
title: "Class RendererPropertyBlockTextureMonoBinder"
sidebar_label: "RendererPropertyBlockTextureMonoBinder"
description: "Class RendererPropertyBlockTextureMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererPropertyBlockTextureMonoBinder {#Aspid_MVVM_StarterKit_RendererPropertyBlockTextureMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a texture shader property, also from
a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

```csharp
[AddBinderContextMenu(typeof(Renderer), new string[] { })]
[AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Texture")]
public class RendererPropertyBlockTextureMonoBinder : RendererPropertyBlockMonoBinder<Texture>, IMonoBinderValidatable, IRebindableBinder, IBinder<Texture>, IBinder<Sprite>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Renderer\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[RendererPropertyBlockMonoBinder\<Texture\>](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) ← 
[RendererPropertyBlockTextureMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockTextureMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Texture\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<Sprite\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RendererPropertyBlockTextureMonoBinder\>\(RendererPropertyBlockTextureMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RendererPropertyBlockTextureMonoBinder\>\(RendererPropertyBlockTextureMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<RendererPropertyBlockTextureMonoBinder\>\(RendererPropertyBlockTextureMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

A property block cannot clear an override, so a missing texture is not written and the previous one
stays.

## Methods

### SetValue\(Sprite\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockTextureMonoBinder_SetValue_UnityEngine_Sprite_}

Writes the texture of <code class="paramref">value</code>.

```csharp
public void SetValue(Sprite value)
```

#### Parameters

`value` Sprite

The sprite received from the ViewModel.

### Write\(Texture\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockTextureMonoBinder_Write_UnityEngine_Texture_}

Writes <code class="paramref">value</code> into [`RendererPropertyBlockMonoBinder<T>.Block`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_Block) under [`RendererPropertyBlockMonoBinder<T>.PropertyId`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_PropertyId).

```csharp
protected override void Write(Texture value)
```

#### Parameters

`value` Texture

The value to write.


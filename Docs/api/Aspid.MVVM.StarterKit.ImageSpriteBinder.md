---
title: "Class ImageSpriteBinder"
sidebar_label: "ImageSpriteBinder"
description: "Class ImageSpriteBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ImageSpriteBinder {#Aspid_MVVM_StarterKit_ImageSpriteBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html), also from a
[`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html).

```csharp
[Serializable]
public class ImageSpriteBinder : TargetBinder<Image, Sprite>, IRebindableBinder, IBinder<Sprite>, IReverseBinder<Sprite>, IBinder<Texture2D>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Image\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<Image, Sprite\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[ImageSpriteBinder](Aspid.MVVM.StarterKit.ImageSpriteBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Sprite\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Sprite\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder\<Texture2D\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ImageSpriteBinder\>\(ImageSpriteBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ImageSpriteBinder\>\(ImageSpriteBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ImageSpriteBinder\>\(ImageSpriteBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

A texture is wrapped in a sprite owned by the binder and destroyed on unbind. Optionally disables the
[`Image`](https://docs.unity3d.com/ScriptReference/UI-Image.html) while the sprite is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Constructors

### ImageSpriteBinder\(\) {#Aspid_MVVM_StarterKit_ImageSpriteBinder__ctor}

```csharp
protected ImageSpriteBinder()
```

#### Remarks

For deserialization only.

### ImageSpriteBinder\(Image, bool, IConverter\<Sprite, Sprite\>, BindMode\) {#Aspid_MVVM_StarterKit_ImageSpriteBinder__ctor_UnityEngine_UI_Image_System_Boolean_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Sprite_UnityEngine_Sprite__Aspid_MVVM_BindMode_}

```csharp
public ImageSpriteBinder(Image target, bool disabledWhenNull = true, IConverter<Sprite, Sprite> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Image

`disabledWhenNull` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Sprite, Sprite\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Properties

### Property {#Aspid_MVVM_StarterKit_ImageSpriteBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Sprite Property { get; set; }
```

#### Property Value

 Sprite

## Methods

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ImageSpriteBinder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(Texture2D\) {#Aspid_MVVM_StarterKit_ImageSpriteBinder_SetValue_UnityEngine_Texture2D_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(Texture2D value)
```

#### Parameters

`value` Texture2D

The value received from the ViewModel.


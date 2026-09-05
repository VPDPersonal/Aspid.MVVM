---
title: "Class ImageSpriteSwitcherBinder"
sidebar_label: "ImageSpriteSwitcherBinder"
description: "Class ImageSpriteSwitcherBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ImageSpriteSwitcherBinder {#Aspid_MVVM_StarterKit_ImageSpriteSwitcherBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html).

```csharp
[Serializable]
public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Image, Sprite>, IRebindableBinder, IBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Image\>](Aspid.MVVM.TargetBinder-1.md) ← 
[SwitcherBinder\<Image, Sprite\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) ← 
[ImageSpriteSwitcherBinder](Aspid.MVVM.StarterKit.ImageSpriteSwitcherBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ImageSpriteSwitcherBinder\>\(ImageSpriteSwitcherBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ImageSpriteSwitcherBinder\>\(ImageSpriteSwitcherBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ImageSpriteSwitcherBinder\>\(ImageSpriteSwitcherBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Optionally disables the [`Image`](https://docs.unity3d.com/ScriptReference/UI-Image.html) while the sprite is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Constructors

### ImageSpriteSwitcherBinder\(Image, bool, Sprite, Sprite, IConverter\<Sprite, Sprite\>, BindMode\) {#Aspid_MVVM_StarterKit_ImageSpriteSwitcherBinder__ctor_UnityEngine_UI_Image_System_Boolean_UnityEngine_Sprite_UnityEngine_Sprite_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Sprite_UnityEngine_Sprite__Aspid_MVVM_BindMode_}

```csharp
public ImageSpriteSwitcherBinder(Image target, bool disabledWhenNull = true, Sprite trueValue = null, Sprite falseValue = null, IConverter<Sprite, Sprite> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Image

`disabledWhenNull` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`trueValue` Sprite

`falseValue` Sprite

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Sprite, Sprite\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### SetValue\(Sprite\) {#Aspid_MVVM_StarterKit_ImageSpriteSwitcherBinder_SetValue_UnityEngine_Sprite_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected override void SetValue(Sprite value)
```

#### Parameters

`value` Sprite

The value to apply.


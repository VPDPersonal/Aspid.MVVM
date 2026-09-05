---
title: "Class RawImageTextureBinder"
sidebar_label: "RawImageTextureBinder"
description: "Class RawImageTextureBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RawImageTextureBinder {#Aspid_MVVM_StarterKit_RawImageTextureBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html), also from a
[`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

```csharp
[Serializable]
public class RawImageTextureBinder : TargetBinder<RawImage, Texture>, IRebindableBinder, IBinder<Texture>, IReverseBinder<Texture>, IBinder<Sprite>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<RawImage\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<RawImage, Texture\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[RawImageTextureBinder](Aspid.MVVM.StarterKit.RawImageTextureBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Texture\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Texture\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder\<Sprite\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RawImageTextureBinder\>\(RawImageTextureBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RawImageTextureBinder\>\(RawImageTextureBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<RawImageTextureBinder\>\(RawImageTextureBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Optionally disables the [`RawImage`](https://docs.unity3d.com/ScriptReference/UI-RawImage.html) while the texture is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Constructors

### RawImageTextureBinder\(\) {#Aspid_MVVM_StarterKit_RawImageTextureBinder__ctor}

```csharp
protected RawImageTextureBinder()
```

#### Remarks

For deserialization only.

### RawImageTextureBinder\(RawImage, bool, IConverter\<Texture, Texture\>, BindMode\) {#Aspid_MVVM_StarterKit_RawImageTextureBinder__ctor_UnityEngine_UI_RawImage_System_Boolean_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Texture_UnityEngine_Texture__Aspid_MVVM_BindMode_}

```csharp
public RawImageTextureBinder(RawImage target, bool disabledWhenNull = true, IConverter<Texture, Texture> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` RawImage

`disabledWhenNull` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Texture, Texture\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Properties

### Property {#Aspid_MVVM_StarterKit_RawImageTextureBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Texture Property { get; set; }
```

#### Property Value

 Texture

## Methods

### SetValue\(Sprite\) {#Aspid_MVVM_StarterKit_RawImageTextureBinder_SetValue_UnityEngine_Sprite_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(Sprite value)
```

#### Parameters

`value` Sprite

The value received from the ViewModel.


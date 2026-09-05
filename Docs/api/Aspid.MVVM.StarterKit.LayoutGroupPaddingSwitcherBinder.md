---
title: "Class LayoutGroupPaddingSwitcherBinder"
sidebar_label: "LayoutGroupPaddingSwitcherBinder"
description: "Class LayoutGroupPaddingSwitcherBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LayoutGroupPaddingSwitcherBinder {#Aspid_MVVM_StarterKit_LayoutGroupPaddingSwitcherBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html).

```csharp
[Serializable]
public sealed class LayoutGroupPaddingSwitcherBinder : SwitcherBinder<LayoutGroup, RectOffset>, IRebindableBinder, IBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<LayoutGroup\>](Aspid.MVVM.TargetBinder-1.md) ← 
[SwitcherBinder\<LayoutGroup, RectOffset\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) ← 
[LayoutGroupPaddingSwitcherBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingSwitcherBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<LayoutGroupPaddingSwitcherBinder\>\(LayoutGroupPaddingSwitcherBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<LayoutGroupPaddingSwitcherBinder\>\(LayoutGroupPaddingSwitcherBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<LayoutGroupPaddingSwitcherBinder\>\(LayoutGroupPaddingSwitcherBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### LayoutGroupPaddingSwitcherBinder\(LayoutGroup, RectSides, RectOffset, RectOffset, IConverter\<RectOffset, RectOffset\>, BindMode\) {#Aspid_MVVM_StarterKit_LayoutGroupPaddingSwitcherBinder__ctor_UnityEngine_UI_LayoutGroup_Aspid_MVVM_StarterKit_RectSides_UnityEngine_RectOffset_UnityEngine_RectOffset_Aspid_MVVM_StarterKit_IConverter_UnityEngine_RectOffset_UnityEngine_RectOffset__Aspid_MVVM_BindMode_}

```csharp
public LayoutGroupPaddingSwitcherBinder(LayoutGroup target, RectSides sides = RectSides.All, RectOffset trueValue = null, RectOffset falseValue = null, IConverter<RectOffset, RectOffset> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` LayoutGroup

`sides` [RectSides](Aspid.MVVM.StarterKit.RectSides.md)

`trueValue` RectOffset

`falseValue` RectOffset

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<RectOffset, RectOffset\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### SetValue\(RectOffset\) {#Aspid_MVVM_StarterKit_LayoutGroupPaddingSwitcherBinder_SetValue_UnityEngine_RectOffset_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected override void SetValue(RectOffset value)
```

#### Parameters

`value` RectOffset

The value to apply.


---
title: "Class LayoutGroupPaddingMonoBinder"
sidebar_label: "LayoutGroupPaddingMonoBinder"
description: "Class LayoutGroupPaddingMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LayoutGroupPaddingMonoBinder {#Aspid_MVVM_StarterKit_LayoutGroupPaddingMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html), also
from a number applied to every selected side.

```csharp
[AddBinderContextMenu(typeof(LayoutGroup), new string[] { "m_Padding" })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding")]
public class LayoutGroupPaddingMonoBinder : ComponentMonoBinder<LayoutGroup, RectOffset>, IMonoBinderValidatable, IBinder<RectOffset>, IReverseBinder<RectOffset>, IIntBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<LayoutGroup\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[ComponentMonoBinder\<LayoutGroup, RectOffset\>](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) ←
[LayoutGroupPaddingMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<RectOffset\>](Aspid.MVVM.IBinder-1.md),
[IReverseBinder\<RectOffset\>](Aspid.MVVM.IReverseBinder-1.md),
[IIntBinder](Aspid.MVVM.StarterKit.IIntBinder.md),
[INumberBinder](Aspid.MVVM.StarterKit.INumberBinder.md),
[IBinder\<int\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<uint\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<long\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<ulong\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<byte\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<sbyte\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<short\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<ushort\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<float\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<double\>](Aspid.MVVM.IBinder-1.md),
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

## Properties

### Property {#Aspid_MVVM_StarterKit_LayoutGroupPaddingMonoBinder_Property}

Gets or sets the bound property.

```csharp
protected override sealed RectOffset Property { get; set; }
```

#### Property Value

 RectOffset

## Methods

### SetValue\(int\) {#Aspid_MVVM_StarterKit_LayoutGroupPaddingMonoBinder_SetValue_System_Int32_}

Applies <code class="paramref">value</code> to every selected side.

```csharp
public void SetValue(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The padding received from the ViewModel.

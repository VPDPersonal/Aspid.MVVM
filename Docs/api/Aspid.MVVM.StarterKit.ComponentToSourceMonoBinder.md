---
title: "Class ComponentToSourceMonoBinder"
sidebar_label: "ComponentToSourceMonoBinder"
description: "Class ComponentToSourceMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ComponentToSourceMonoBinder {#Aspid_MVVM_StarterKit_ComponentToSourceMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for any [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) that also reports it as
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">object</a> via [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Components/Component To Source Binder")]
[AddBinderContextMenu(typeof(Component), new string[] { }, Path = "Add General Binder/Component/Component To Source Binder")]
public sealed class ComponentToSourceMonoBinder : ComponentToSourceMonoBinder<Component>, IMonoBinderValidatable, IRebindableBinder, IReverseBinder<Component>, IAnyReverseBinder, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Component\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[ComponentToSourceMonoBinder\<Component\>](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) ← 
[ComponentToSourceMonoBinder](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Component\>](Aspid.MVVM.IReverseBinder-1.md), 
[IAnyReverseBinder](Aspid.MVVM.IAnyReverseBinder.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ComponentToSourceMonoBinder\>\(ComponentToSourceMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ComponentToSourceMonoBinder\>\(ComponentToSourceMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ComponentToSourceMonoBinder\>\(ComponentToSourceMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">object</a> channel is not type-checked at compile time: a mismatch with the ViewModel member surfaces only at runtime.

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_ComponentToSourceMonoBinder_OnBound}

Raises [`ComponentToSourceMonoBinder<T>.ValueChanged`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md#Aspid_MVVM_StarterKit_ComponentToSourceMonoBinder_1_ValueChanged) with the target component.

```csharp
protected override void OnBound()
```

#### Remarks

When overriding, always call <code>base.OnBound()</code>.

### ValueChanged {#Aspid_MVVM_StarterKit_ComponentToSourceMonoBinder_ValueChanged}

Raised when the View's value changes and needs to be propagated back to the ViewModel.

```csharp
public event Action<object> ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[object](https://learn.microsoft.com/dotnet/api/system.object)\>


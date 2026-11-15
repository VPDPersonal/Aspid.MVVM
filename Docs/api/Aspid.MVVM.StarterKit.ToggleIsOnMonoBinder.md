---
title: "Class ToggleIsOnMonoBinder"
sidebar_label: "ToggleIsOnMonoBinder"
description: "Class ToggleIsOnMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ToggleIsOnMonoBinder {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and reports user changes
back.

```csharp
[BindModeOverride(new BindMode[] { }, IsAll = true)]
[AddBinderContextMenu(typeof(Toggle), new string[] { "m_IsOn" })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
public sealed class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>, IMonoBinderValidatable, IBinder<bool>, IReverseBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Toggle\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[ToggleIsOnMonoBinder](Aspid.MVVM.StarterKit.ToggleIsOnMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<bool\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ToggleIsOnMonoBinder\>\(ToggleIsOnMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<ToggleIsOnMonoBinder\>\(ToggleIsOnMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<ToggleIsOnMonoBinder\>\(ToggleIsOnMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnBound\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### Reset\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_Reset}

Called by Unity when the component is added or reset in the Editor to set [`NotifyMode.Never`](Aspid.MVVM.StarterKit.NotifyMode.md).

```csharp
protected override void Reset()
```

#### Remarks

Existing components keep [`NotifyMode.Always`](Aspid.MVVM.StarterKit.NotifyMode.md), the value of the field before it was serialized.

### SetValue\(bool\) {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_SetValue_System_Boolean_}

Sets [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) without reporting the write back to the ViewModel.

```csharp
public void SetValue(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_ToggleIsOnMonoBinder_ValueChanged}

Occurs when the View produces a value for the ViewModel.

```csharp
public event Action<bool> ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>


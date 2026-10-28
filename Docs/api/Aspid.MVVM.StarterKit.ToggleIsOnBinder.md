---
title: "Class ToggleIsOnBinder"
sidebar_label: "ToggleIsOnBinder"
description: "Class ToggleIsOnBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ToggleIsOnBinder {#Aspid_MVVM_StarterKit_ToggleIsOnBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and reports user changes back.

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { }, IsAll = true)]
public sealed class ToggleIsOnBinder : TargetBinder<Toggle>, IRebindableBinder, IBinder<bool>, IReverseBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Toggle\>](Aspid.MVVM.TargetBinder-1.md) ← 
[ToggleIsOnBinder](Aspid.MVVM.StarterKit.ToggleIsOnBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<bool\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ToggleIsOnBinder\>\(ToggleIsOnBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ToggleIsOnBinder\>\(ToggleIsOnBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ToggleIsOnBinder\>\(ToggleIsOnBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Writes raise [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) for other listeners; only the binder's own echo is suppressed.

## Constructors

### ToggleIsOnBinder\(Toggle, IConverter\<bool, bool\>?, BindMode\) {#Aspid_MVVM_StarterKit_ToggleIsOnBinder__ctor_UnityEngine_UI_Toggle_Aspid_MVVM_StarterKit_IConverter_System_Boolean_System_Boolean__Aspid_MVVM_BindMode_}

```csharp
public ToggleIsOnBinder(Toggle target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.TwoWay)
```

#### Parameters

`target` Toggle

The toggle to bind.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean), [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The converter applied to the bound value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it as-is; reverse only via
[`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnBinder_OnBound}

Called after the binder attaches and receives the first value. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ToggleIsOnBinder_OnUnbound}

Called after the binder detaches. Override to release what [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) subscribed to.

```csharp
protected override void OnUnbound()
```

### SetValue\(bool\) {#Aspid_MVVM_StarterKit_ToggleIsOnBinder_SetValue_System_Boolean_}

Sets [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) without reporting the write back to the ViewModel.

```csharp
public void SetValue(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_ToggleIsOnBinder_ValueChanged}

Occurs when the View produces a value for the ViewModel.

```csharp
public event Action<bool>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?


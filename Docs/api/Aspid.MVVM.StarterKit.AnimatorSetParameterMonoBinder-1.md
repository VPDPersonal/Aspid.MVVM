---
title: "Class AnimatorSetParameterMonoBinder<T>"
sidebar_label: "AnimatorSetParameterMonoBinder<T>"
description: "Class AnimatorSetParameterMonoBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimatorSetParameterMonoBinder\<T\> {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that sets a typed [`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) parameter.

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource })]
public abstract class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, IMonoBinderValidatable, IRebindableBinder, IBinder<T>, IReverseBinder<Action<T>>, IReverseBinder<IRelayCommand<T>>, IBinder
```

#### Type Parameters

`T` 

The parameter value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Animator\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[AnimatorSetParameterMonoBinder\<T\>](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<T\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Action\<T\>\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand\<T\>\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AnimatorSetParameterMonoBinder\<T\>\>\(AnimatorSetParameterMonoBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AnimatorSetParameterMonoBinder\<T\>\>\(AnimatorSetParameterMonoBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AnimatorSetParameterMonoBinder\<T\>\>\(AnimatorSetParameterMonoBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) the setter is handed to the ViewModel as an
[`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) or an [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1). The last value is re-applied on enable.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### ParameterName {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_ParameterName}

```csharp
protected string ParameterName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### ParameterType {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_ParameterType}

The parameter type inferred from <code class="typeparamref">T</code>, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to match by name only.

```csharp
protected virtual AnimatorControllerParameterType? ParameterType { get; }
```

#### Property Value

 AnimatorControllerParameterType?

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### CanExecute\(T\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_CanExecute__0_}

Whether the parameter may be set: the animator is active and its controller has the parameter.

```csharp
protected virtual bool CanExecute(T value)
```

#### Parameters

`value` T

The value that would be written.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the parameter may be set.

### OnBound\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override sealed void OnBound()
```

### OnDisable\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_OnDisable}

Refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnDisable()
```

#### Remarks

When overriding, always call <code>base.OnDisable()</code>.

### OnEnable\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_OnEnable}

Re-applies the last value and refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnEnable()
```

#### Remarks

When overriding, always call <code>base.OnEnable()</code>.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_OnUnbinding}

Called before unbinding, while the binder is still attached to the ViewModel. Override to add pre-unbinding logic.

```csharp
protected override sealed void OnUnbinding()
```

### SetParameter\(T\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_SetParameter__0_}

Writes <code class="paramref">value</code> to the parameter named [`AnimatorSetParameterMonoBinder<T>.ParameterName`](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_ParameterName).

```csharp
protected abstract void SetParameter(T value)
```

#### Parameters

`value` T

The value to write.

### SetValue\(T\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_SetValue__0_}

Sets the parameter when [`AnimatorSetParameterMonoBinder<T>.CanExecute`](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md#Aspid_MVVM_StarterKit_AnimatorSetParameterMonoBinder_1_CanExecute__0_) allows it.

```csharp
public void SetValue(T value)
```

#### Parameters

`value` T

The value received from the ViewModel.


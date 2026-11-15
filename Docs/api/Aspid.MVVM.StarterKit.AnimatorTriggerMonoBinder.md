---
title: "Class AnimatorTriggerMonoBinder"
sidebar_label: "AnimatorTriggerMonoBinder"
description: "Class AnimatorTriggerMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimatorTriggerMonoBinder {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that hands the ViewModel one operation on an
[`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) trigger as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWayToSource })]
public abstract class AnimatorTriggerMonoBinder : ComponentMonoBinder<Animator>, IMonoBinderValidatable, IReverseBinder<Action>, IReverseBinder<IRelayCommand>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Animator\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[AnimatorTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorTriggerMonoBinder.md)

#### Derived

[AnimatorResetTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorResetTriggerMonoBinder.md), 
[AnimatorSetTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorSetTriggerMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IReverseBinder\<Action\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AnimatorTriggerMonoBinder\>\(AnimatorTriggerMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AnimatorTriggerMonoBinder\>\(AnimatorTriggerMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_), 
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
[BinderExtensions.UnbindSafely\<AnimatorTriggerMonoBinder\>\(AnimatorTriggerMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Properties

### DefaultMode {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_DefaultMode}

Gets the binding mode a freshly added binder starts in. The default is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
Override in binders whose <code>[BindModeOverride]</code> excludes it.

```csharp
protected override BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### TriggerName {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_TriggerName}

The trigger parameter to fire.

```csharp
protected string TriggerName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Apply\(string\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_Apply_System_String_}

Performs the operation on the trigger named <code class="paramref">triggerName</code>.

```csharp
protected abstract void Apply(string triggerName)
```

#### Parameters

`triggerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The trigger, already checked to exist.

### CanExecute\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_CanExecute}

Whether the trigger may be fired: the animator is active and its controller has the trigger.

```csharp
protected virtual bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the trigger may be fired.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_NotifyCanExecuteChanged}

Notifies the command handed to the ViewModel that [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
public void NotifyCanExecuteChanged()
```

### OnAttach\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override sealed void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnBound\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override sealed void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnDisable\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_OnDisable}

Refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnDisable()
```

#### Remarks

When overriding, always call <code>base.OnDisable()</code>.

### OnEnable\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_OnEnable}

Refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnEnable()
```

#### Remarks

When overriding, always call <code>base.OnEnable()</code>.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerMonoBinder_OnUnbinding}

Called when cleanup starts, while reported values still reach the ViewModel. Override to send a last value.

```csharp
protected override sealed void OnUnbinding()
```

#### Remarks

[`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) is already <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>; the owner is retained until cleanup finishes.


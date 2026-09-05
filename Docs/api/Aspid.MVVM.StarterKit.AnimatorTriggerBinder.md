---
title: "Class AnimatorTriggerBinder"
sidebar_label: "AnimatorTriggerBinder"
description: "Class AnimatorTriggerBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimatorTriggerBinder {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that hands the ViewModel one operation on an
[`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) trigger as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { BindMode.OneWayToSource })]
public abstract class AnimatorTriggerBinder : TargetBinder<Animator>, IRebindableBinder, IReverseBinder<Action?>, IReverseBinder<IRelayCommand?>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Animator\>](Aspid.MVVM.TargetBinder-1.md) ← 
[AnimatorTriggerBinder](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md)

#### Derived

[AnimatorResetTriggerBinder](Aspid.MVVM.StarterKit.AnimatorResetTriggerBinder.md), 
[AnimatorSetTriggerBinder](Aspid.MVVM.StarterKit.AnimatorSetTriggerBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Action?\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand?\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AnimatorTriggerBinder\>\(AnimatorTriggerBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AnimatorTriggerBinder\>\(AnimatorTriggerBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AnimatorTriggerBinder\>\(AnimatorTriggerBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### AnimatorTriggerBinder\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder__ctor}

```csharp
protected AnimatorTriggerBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### AnimatorTriggerBinder\(Animator, string\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder__ctor_UnityEngine_Animator_System_String_}

```csharp
protected AnimatorTriggerBinder(Animator target, string triggerName)
```

#### Parameters

`target` Animator

The animator to bind.

`triggerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The trigger parameter.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">triggerName</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### TriggerName {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_TriggerName}

```csharp
protected string TriggerName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Apply\(string\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_Apply_System_String_}

Performs the operation on the trigger named <code class="paramref">triggerName</code>.

```csharp
protected abstract void Apply(string triggerName)
```

#### Parameters

`triggerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The trigger, already checked to exist.

### CanExecute\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_CanExecute}

Whether the trigger may be fired: the animator is active and its controller has the trigger.

```csharp
protected virtual bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the trigger may be fired.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_NotifyCanExecuteChanged}

Notifies the command handed to the ViewModel that [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
public void NotifyCanExecuteChanged()
```

### OnBound\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override sealed void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_AnimatorTriggerBinder_OnUnbinding}

Called before unbinding. Override to add pre-unbinding logic.

```csharp
protected override sealed void OnUnbinding()
```

#### Remarks

Runs while [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is still <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and the binder is still
attached to the ViewModel, so anything sent from here still arrives.


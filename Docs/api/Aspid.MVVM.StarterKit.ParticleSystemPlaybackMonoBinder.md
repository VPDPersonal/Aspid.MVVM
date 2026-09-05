---
title: "Class ParticleSystemPlaybackMonoBinder"
sidebar_label: "ParticleSystemPlaybackMonoBinder"
description: "Class ParticleSystemPlaybackMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ParticleSystemPlaybackMonoBinder {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that hands the ViewModel one playback operation on a
[`ParticleSystem`](https://docs.unity3d.com/ScriptReference/ParticleSystem.html) as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWayToSource })]
public abstract class ParticleSystemPlaybackMonoBinder : ComponentMonoBinder<ParticleSystem>, IMonoBinderValidatable, IRebindableBinder, IReverseBinder<Action>, IReverseBinder<IRelayCommand>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<ParticleSystem\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[ParticleSystemPlaybackMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md)

#### Derived

[ParticleSystemClearMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemClearMonoBinder.md), 
[ParticleSystemPauseMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPauseMonoBinder.md), 
[ParticleSystemPlayMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPlayMonoBinder.md), 
[ParticleSystemStopMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemStopMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Action\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ParticleSystemPlaybackMonoBinder\>\(ParticleSystemPlaybackMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ParticleSystemPlaybackMonoBinder\>\(ParticleSystemPlaybackMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ParticleSystemPlaybackMonoBinder\>\(ParticleSystemPlaybackMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Properties

### DefaultMode {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_DefaultMode}

Gets the binding mode a freshly added binder starts in. The default is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
Override in binders whose <code>[BindModeOverride]</code> excludes it.

```csharp
protected override BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### CanExecute\(\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_CanExecute}

Whether the operation may run: the system exists and is active in the hierarchy.

```csharp
protected virtual bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the operation may run.

### OnBound\(\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override sealed void OnBound()
```

### OnDisable\(\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_OnDisable}

Refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnDisable()
```

#### Remarks

When overriding, always call <code>base.OnDisable()</code>.

### OnEnable\(\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_OnEnable}

Refreshes the command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute).

```csharp
protected virtual void OnEnable()
```

#### Remarks

When overriding, always call <code>base.OnEnable()</code>.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_OnUnbinding}

Called before unbinding, while the binder is still attached to the ViewModel. Override to add pre-unbinding logic.

```csharp
protected override sealed void OnUnbinding()
```

### Perform\(ParticleSystem\) {#Aspid_MVVM_StarterKit_ParticleSystemPlaybackMonoBinder_Perform_UnityEngine_ParticleSystem_}

Performs the operation on <code class="paramref">particleSystem</code>.

```csharp
protected abstract void Perform(ParticleSystem particleSystem)
```

#### Parameters

`particleSystem` ParticleSystem

The system to act on.


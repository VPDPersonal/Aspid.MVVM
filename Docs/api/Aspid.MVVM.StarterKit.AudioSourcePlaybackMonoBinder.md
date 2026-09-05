---
title: "Class AudioSourcePlaybackMonoBinder"
sidebar_label: "AudioSourcePlaybackMonoBinder"
description: "Class AudioSourcePlaybackMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioSourcePlaybackMonoBinder {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that exposes one playback operation on an
[`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html) to the ViewModel as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWayToSource })]
public abstract class AudioSourcePlaybackMonoBinder : ComponentMonoBinder<AudioSource>, IMonoBinderValidatable, IRebindableBinder, IReverseBinder<Action>, IReverseBinder<IRelayCommand>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<AudioSource\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[AudioSourcePlaybackMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md)

#### Derived

[AudioSourcePauseMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePauseMonoBinder.md), 
[AudioSourcePlayMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePlayMonoBinder.md), 
[AudioSourceStopMonoBinder](Aspid.MVVM.StarterKit.AudioSourceStopMonoBinder.md), 
[AudioSourceUnPauseMonoBinder](Aspid.MVVM.StarterKit.AudioSourceUnPauseMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Action\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AudioSourcePlaybackMonoBinder\>\(AudioSourcePlaybackMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AudioSourcePlaybackMonoBinder\>\(AudioSourcePlaybackMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AudioSourcePlaybackMonoBinder\>\(AudioSourcePlaybackMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The command's [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) mirrors [`AudioSourcePlaybackMonoBinder.CanExecute`](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_CanExecute).

## Properties

### DefaultMode {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_DefaultMode}

Gets the binding mode a freshly added binder starts in. The default is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
Override in binders whose <code>[BindModeOverride]</code> excludes it.

```csharp
protected override BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### CanExecute\(\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_CanExecute}

Determines whether the operation may run: the source exists and is active in the hierarchy.

```csharp
protected virtual bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the operation may run; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

A clip is not required, since Stop and Pause are meaningful without one.

### OnBound\(\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override sealed void OnBound()
```

### OnDisable\(\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_OnDisable}

Notifies the bound command that [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
protected virtual void OnDisable()
```

#### Remarks

When overriding, always call <code>base.OnDisable()</code>.

### OnEnable\(\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_OnEnable}

Notifies the bound command that [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
protected virtual void OnEnable()
```

#### Remarks

When overriding, always call <code>base.OnEnable()</code>.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_OnUnbinding}

Called before unbinding, while the binder is still attached to the ViewModel. Override to add pre-unbinding logic.

```csharp
protected override sealed void OnUnbinding()
```

### Perform\(AudioSource\) {#Aspid_MVVM_StarterKit_AudioSourcePlaybackMonoBinder_Perform_UnityEngine_AudioSource_}

Performs the operation on <code class="paramref">audioSource</code>.

```csharp
protected abstract void Perform(AudioSource audioSource)
```

#### Parameters

`audioSource` AudioSource

The source to act on; never <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.


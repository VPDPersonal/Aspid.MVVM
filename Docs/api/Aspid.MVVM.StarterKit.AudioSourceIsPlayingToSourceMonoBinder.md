---
title: "Class AudioSourceIsPlayingToSourceMonoBinder"
sidebar_label: "AudioSourceIsPlayingToSourceMonoBinder"
description: "Class AudioSourceIsPlayingToSourceMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioSourceIsPlayingToSourceMonoBinder {#Aspid_MVVM_StarterKit_AudioSourceIsPlayingToSourceMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that reports [`isPlaying`](https://docs.unity3d.com/ScriptReference/AudioSource-isPlaying.html) to the ViewModel.

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWayToSource })]
[AddBinderContextMenu(typeof(AudioSource), new string[] { })]
[AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Is Playing To Source")]
public sealed class AudioSourceIsPlayingToSourceMonoBinder : ComponentMonoBinder<AudioSource>, IMonoBinderValidatable, IReverseBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<AudioSource\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[AudioSourceIsPlayingToSourceMonoBinder](Aspid.MVVM.StarterKit.AudioSourceIsPlayingToSourceMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IReverseBinder\<bool\>](Aspid.MVVM.IReverseBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AudioSourceIsPlayingToSourceMonoBinder\>\(AudioSourceIsPlayingToSourceMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<AudioSourceIsPlayingToSourceMonoBinder\>\(AudioSourceIsPlayingToSourceMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<AudioSourceIsPlayingToSourceMonoBinder\>\(AudioSourceIsPlayingToSourceMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

[`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html) raises no event when playback ends, so the state is polled once per frame while the binder is enabled.

## Properties

### DefaultMode {#Aspid_MVVM_StarterKit_AudioSourceIsPlayingToSourceMonoBinder_DefaultMode}

Gets the mode for a freshly added binder; [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) by default.

```csharp
protected override BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

#### Remarks

Override when [`BindModeOverrideAttribute`](Aspid.MVVM.BindModeOverrideAttribute.md) excludes the default mode.

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_AudioSourceIsPlayingToSourceMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnBound\(\) {#Aspid_MVVM_StarterKit_AudioSourceIsPlayingToSourceMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### ValueChanged {#Aspid_MVVM_StarterKit_AudioSourceIsPlayingToSourceMonoBinder_ValueChanged}

Occurs when the View produces a value for the ViewModel.

```csharp
public event Action<bool> ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

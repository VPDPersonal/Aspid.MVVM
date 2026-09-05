---
title: "Class AudioMixerFloatBinder"
sidebar_label: "AudioMixerFloatBinder"
description: "Class AudioMixerFloatBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioMixerFloatBinder {#Aspid_MVVM_StarterKit_AudioMixerFloatBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds an exposed [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html) parameter.

```csharp
[Serializable]
public class AudioMixerFloatBinder : TargetFloatBinder<AudioMixer>, IRebindableBinder, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, INumberReverseBinder, IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<AudioMixer\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<AudioMixer, float\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[TargetFloatBinder\<AudioMixer\>](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) ← 
[AudioMixerFloatBinder](Aspid.MVVM.StarterKit.AudioMixerFloatBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IFloatBinder](Aspid.MVVM.StarterKit.IFloatBinder.md), 
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
[INumberReverseBinder](Aspid.MVVM.StarterKit.INumberReverseBinder.md), 
[IReverseBinder\<int\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<long\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<float\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<double\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AudioMixerFloatBinder\>\(AudioMixerFloatBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AudioMixerFloatBinder\>\(AudioMixerFloatBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AudioMixerFloatBinder\>\(AudioMixerFloatBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The value is written as-is; mixer volumes are usually decibels. A blank name, an unexposed parameter and a
non-finite value are reported and skipped.

## Constructors

### AudioMixerFloatBinder\(AudioMixer, string, IConverter\<float, float\>?, BindMode\) {#Aspid_MVVM_StarterKit_AudioMixerFloatBinder__ctor_UnityEngine_Audio_AudioMixer_System_String_Aspid_MVVM_StarterKit_IConverter_System_Single_System_Single__Aspid_MVVM_BindMode_}

```csharp
public AudioMixerFloatBinder(AudioMixer target, string parameter, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` AudioMixer

The mixer that exposes the parameter.

`parameter` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exposed parameter name.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single), [float](https://learn.microsoft.com/dotnet/api/system.single)\>?

The converter applied to the bound value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it as-is.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

## Properties

### Property {#Aspid_MVVM_StarterKit_AudioMixerFloatBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed float Property { get; set; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

## Methods

### SendInitialValueToSource\(\) {#Aspid_MVVM_StarterKit_AudioMixerFloatBinder_SendInitialValueToSource}

Sends the current value on every numeric channel.

```csharp
protected override void SendInitialValueToSource()
```

#### Remarks

Also calls the base implementation: [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) for the property's own type resolves to
the class-level [`TargetBinder<T1, T2>.ValueChanged`](Aspid.MVVM.StarterKit.TargetBinder-2.md#Aspid_MVVM_StarterKit_TargetBinder_2_ValueChanged), not to the channel.


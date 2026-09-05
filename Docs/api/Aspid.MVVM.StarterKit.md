---
title: "Namespace Aspid.MVVM.StarterKit"
sidebar_label: "Aspid.MVVM.StarterKit"
description: "Namespace Aspid.MVVM.StarterKit — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Namespace Aspid.MVVM.StarterKit {#Aspid_MVVM_StarterKit}

### Classes

 [AbbreviatedNumberConverter](Aspid.MVVM.StarterKit.AbbreviatedNumberConverter.md)

Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M.

 [AddressableMonoBinder\<TAsset, TComponent\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that loads an Addressable asset by key or [`IKeyEvaluator`](https://docs.unity3d.com/ScriptReference/AddressableAssets-IKeyEvaluator.html)
and applies it to the component once loaded. An empty key applies [`AddressableMonoBinder<T1, T2>.GetDefaultAsset`](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_GetDefaultAsset).

 [AddressableMonoBinder\<TAsset\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that loads an Addressable asset by key or [`IKeyEvaluator`](https://docs.unity3d.com/ScriptReference/AddressableAssets-IKeyEvaluator.html)
and applies it once loaded. An empty key applies [`AddressableMonoBinder<T>.GetDefaultAsset`](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_GetDefaultAsset).

 [AggregatorInputMonoBinder\<TInput, TResult\>](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md)

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that feeds one value into an
[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md).

 [AggregatorMonoBinder\<TInput, TResult\>](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md)

Abstract [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) that combines the values of several
[`AggregatorInputMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md) components into one [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html).

 [AndBoolMonoBinder](Aspid.MVVM.StarterKit.AndBoolMonoBinder.md)

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that forwards <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when every input
is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

 [AndCollectionFilter\<T\>](Aspid.MVVM.StarterKit.AndCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element only when every nested filter passes it.
Empty slots are skipped.

 [AngleDifferenceConverter](Aspid.MVVM.StarterKit.AngleDifferenceConverter.md)

Measures how far an angle is from a fixed one.

 [AngleToQuaternionConverter](Aspid.MVVM.StarterKit.AngleToQuaternionConverter.md)

Turns a single angle into a rotation.

 [AngleWrapConverter](Aspid.MVVM.StarterKit.AngleWrapConverter.md)

Folds an angle into a standard range.

 [AnimationCurveConverter](Aspid.MVVM.StarterKit.AnimationCurveConverter.md)

Passes a number through an [`AnimationCurve`](https://docs.unity3d.com/ScriptReference/AnimationCurve.html).

 [AnimatorControllerBinder](Aspid.MVVM.StarterKit.AnimatorControllerBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds
[`runtimeAnimatorController`](https://docs.unity3d.com/ScriptReference/Animator-runtimeAnimatorController.html).

 [AnimatorControllerMonoBinder](Aspid.MVVM.StarterKit.AnimatorControllerMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds
[`runtimeAnimatorController`](https://docs.unity3d.com/ScriptReference/Animator-runtimeAnimatorController.html).

 [AnimatorLayerWeightBinder](Aspid.MVVM.StarterKit.AnimatorLayerWeightBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds the weight of one animator layer.

 [AnimatorLayerWeightMonoBinder](Aspid.MVVM.StarterKit.AnimatorLayerWeightMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds the weight of one animator layer.

 [AnimatorPlayStateBinder](Aspid.MVVM.StarterKit.AnimatorPlayStateBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that plays the animator state named by the bound string.

 [AnimatorPlayStateMonoBinder](Aspid.MVVM.StarterKit.AnimatorPlayStateMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that plays the animator state named by the bound string.

 [AnimatorResetTriggerBinder](Aspid.MVVM.StarterKit.AnimatorResetTriggerBinder.md)

[`AnimatorTriggerBinder`](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md) that calls [`ResetTrigger`](https://docs.unity3d.com/ScriptReference/Animator-ResetTrigger.html).

 [AnimatorResetTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorResetTriggerMonoBinder.md)

[`AnimatorTriggerMonoBinder`](Aspid.MVVM.StarterKit.AnimatorTriggerMonoBinder.md) that calls [`ResetTrigger`](https://docs.unity3d.com/ScriptReference/Animator-ResetTrigger.html).

 [AnimatorSetBoolBinder](Aspid.MVVM.StarterKit.AnimatorSetBoolBinder.md)

[`AnimatorSetParameterBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> parameter.

 [AnimatorSetBoolMonoBinder](Aspid.MVVM.StarterKit.AnimatorSetBoolMonoBinder.md)

[`AnimatorSetParameterMonoBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> parameter.

 [AnimatorSetFloatBinder](Aspid.MVVM.StarterKit.AnimatorSetFloatBinder.md)

[`AnimatorSetParameterBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> parameter.

 [AnimatorSetFloatMonoBinder](Aspid.MVVM.StarterKit.AnimatorSetFloatMonoBinder.md)

[`AnimatorSetParameterMonoBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> parameter.

 [AnimatorSetIntBinder](Aspid.MVVM.StarterKit.AnimatorSetIntBinder.md)

[`AnimatorSetParameterBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> parameter.

 [AnimatorSetIntMonoBinder](Aspid.MVVM.StarterKit.AnimatorSetIntMonoBinder.md)

[`AnimatorSetParameterMonoBinder<T>`](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md) that sets a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> parameter.

 [AnimatorSetParameterBinder\<T\>](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md)

Abstract [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that sets a typed [`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) parameter.

 [AnimatorSetParameterMonoBinder\<T\>](Aspid.MVVM.StarterKit.AnimatorSetParameterMonoBinder-1.md)

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that sets a typed [`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) parameter.

 [AnimatorSetTriggerBinder](Aspid.MVVM.StarterKit.AnimatorSetTriggerBinder.md)

[`AnimatorTriggerBinder`](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md) that calls [`SetTrigger`](https://docs.unity3d.com/ScriptReference/Animator-SetTrigger.html).

 [AnimatorSetTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorSetTriggerMonoBinder.md)

[`AnimatorTriggerMonoBinder`](Aspid.MVVM.StarterKit.AnimatorTriggerMonoBinder.md) that calls [`SetTrigger`](https://docs.unity3d.com/ScriptReference/Animator-SetTrigger.html).

 [AnimatorSpeedBinder](Aspid.MVVM.StarterKit.AnimatorSpeedBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`speed`](https://docs.unity3d.com/ScriptReference/Animator-speed.html).

 [AnimatorSpeedMonoBinder](Aspid.MVVM.StarterKit.AnimatorSpeedMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`speed`](https://docs.unity3d.com/ScriptReference/Animator-speed.html).

 [AnimatorToSourceMonoBinder](Aspid.MVVM.StarterKit.AnimatorToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html).

 [AnimatorTriggerBinder](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md)

Abstract [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that hands the ViewModel one operation on an
[`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) trigger as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

 [AnimatorTriggerMonoBinder](Aspid.MVVM.StarterKit.AnimatorTriggerMonoBinder.md)

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that hands the ViewModel one operation on an
[`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) trigger as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

 [AnyToStringCasterBinder](Aspid.MVVM.StarterKit.AnyToStringCasterBinder.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) that converts any bound value to a [`String`](https://learn.microsoft.com/dotnet/api/system.string)
and forwards it to a target setter.

 [AnyToStringCasterMonoBinder](Aspid.MVVM.StarterKit.AnyToStringCasterMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) implementing [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) that converts any bound value to a [`String`](https://learn.microsoft.com/dotnet/api/system.string)
with a serialized converter and forwards it to a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html). Defaults to [`ValueToStringConverter<T>`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md).

 [ArabicPluralRule](Aspid.MVVM.StarterKit.ArabicPluralRule.md)

Six words, the widest grammar CLDR declares: Arabic.

 [ArithmeticNumberConverter](Aspid.MVVM.StarterKit.ArithmeticNumberConverter.md)

Applies an arithmetic operation with an authored coefficient.

 [AspectRatioFitterAspectModeBinder](Aspid.MVVM.StarterKit.AspectRatioFitterAspectModeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`aspectMode`](https://docs.unity3d.com/ScriptReference/UI-AspectRatioFitter-aspectMode.html).

 [AspectRatioFitterAspectModeMonoBinder](Aspid.MVVM.StarterKit.AspectRatioFitterAspectModeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`aspectMode`](https://docs.unity3d.com/ScriptReference/UI-AspectRatioFitter-aspectMode.html).

 [AspectRatioFitterAspectRatioBinder](Aspid.MVVM.StarterKit.AspectRatioFitterAspectRatioBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`aspectRatio`](https://docs.unity3d.com/ScriptReference/UI-AspectRatioFitter-aspectRatio.html).

 [AspectRatioFitterAspectRatioMonoBinder](Aspid.MVVM.StarterKit.AspectRatioFitterAspectRatioMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`aspectRatio`](https://docs.unity3d.com/ScriptReference/UI-AspectRatioFitter-aspectRatio.html).

 [AudioLinearDecibelConverter](Aspid.MVVM.StarterKit.AudioLinearDecibelConverter.md)

Converts a 0..1 slider position to the decibels an [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html)
expects, or the other way around.

 [AudioListenerPauseBinder](Aspid.MVVM.StarterKit.AudioListenerPauseBinder.md)

[`Binder<T>`](Aspid.MVVM.StarterKit.Binder-1.md) that binds [`pause`](https://docs.unity3d.com/ScriptReference/AudioListener-pause.html).

 [AudioListenerPauseMonoBinder](Aspid.MVVM.StarterKit.AudioListenerPauseMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that binds [`pause`](https://docs.unity3d.com/ScriptReference/AudioListener-pause.html).

 [AudioListenerVolumeBinder](Aspid.MVVM.StarterKit.AudioListenerVolumeBinder.md)

[`FloatBinder`](Aspid.MVVM.StarterKit.FloatBinder.md) that binds [`volume`](https://docs.unity3d.com/ScriptReference/AudioListener-volume.html).

 [AudioListenerVolumeMonoBinder](Aspid.MVVM.StarterKit.AudioListenerVolumeMonoBinder.md)

[`FloatMonoBinder`](Aspid.MVVM.StarterKit.FloatMonoBinder.md) that binds [`volume`](https://docs.unity3d.com/ScriptReference/AudioListener-volume.html).

 [AudioMixerFloatBinder](Aspid.MVVM.StarterKit.AudioMixerFloatBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds an exposed [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html) parameter.

 [AudioMixerFloatMonoBinder](Aspid.MVVM.StarterKit.AudioMixerFloatMonoBinder.md)

[`FloatMonoBinder`](Aspid.MVVM.StarterKit.FloatMonoBinder.md) that binds an exposed [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html) parameter.

 [AudioMixerSnapshotBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotBinder.md)

[`Binder`](Aspid.MVVM.Binder.md) that transitions an [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html) to one of the listed snapshots, chosen by
index or by name.

 [AudioMixerSnapshotMonoBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that transitions an [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html) to one of the listed snapshots, chosen by
index or by name.

 [AudioSourceBypassEffectsBinder](Aspid.MVVM.StarterKit.AudioSourceBypassEffectsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`bypassEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassEffects.html).

 [AudioSourceBypassEffectsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassEffectsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`bypassEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassEffects.html) on each element.

 [AudioSourceBypassEffectsEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassEffectsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`bypassEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassEffects.html).

 [AudioSourceBypassEffectsMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassEffectsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`bypassEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassEffects.html).

 [AudioSourceBypassListenerEffectsBinder](Aspid.MVVM.StarterKit.AudioSourceBypassListenerEffectsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`bypassListenerEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassListenerEffects.html).

 [AudioSourceBypassListenerEffectsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassListenerEffectsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`bypassListenerEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassListenerEffects.html) on each element.

 [AudioSourceBypassListenerEffectsEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassListenerEffectsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`bypassListenerEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassListenerEffects.html).

 [AudioSourceBypassListenerEffectsMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassListenerEffectsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`bypassListenerEffects`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassListenerEffects.html).

 [AudioSourceBypassReverbZonesBinder](Aspid.MVVM.StarterKit.AudioSourceBypassReverbZonesBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`bypassReverbZones`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassReverbZones.html).

 [AudioSourceBypassReverbZonesEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassReverbZonesEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`bypassReverbZones`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassReverbZones.html) on each element.

 [AudioSourceBypassReverbZonesEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassReverbZonesEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`bypassReverbZones`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassReverbZones.html).

 [AudioSourceBypassReverbZonesMonoBinder](Aspid.MVVM.StarterKit.AudioSourceBypassReverbZonesMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`bypassReverbZones`](https://docs.unity3d.com/ScriptReference/AudioSource-bypassReverbZones.html).

 [AudioSourceClipBinder](Aspid.MVVM.StarterKit.AudioSourceClipBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html).

 [AudioSourceClipEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceClipEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html) on each element.

 [AudioSourceClipEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceClipEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html).

 [AudioSourceClipMonoBinder](Aspid.MVVM.StarterKit.AudioSourceClipMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html).

 [AudioSourceClipSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceClipSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html).

 [AudioSourceClipSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceClipSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`clip`](https://docs.unity3d.com/ScriptReference/AudioSource-clip.html).

 [AudioSourceDopplerLevelBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html).

 [AudioSourceDopplerLevelEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html) on each element.

 [AudioSourceDopplerLevelEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html).

 [AudioSourceDopplerLevelMonoBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html).

 [AudioSourceDopplerLevelSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html).

 [AudioSourceDopplerLevelSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceDopplerLevelSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`dopplerLevel`](https://docs.unity3d.com/ScriptReference/AudioSource-dopplerLevel.html).

 [AudioSourceExtensions](Aspid.MVVM.StarterKit.AudioSourceExtensions.md)

Extension methods that write validated values to an [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html).

 [AudioSourceIsPlayingToSourceMonoBinder](Aspid.MVVM.StarterKit.AudioSourceIsPlayingToSourceMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that reports [`isPlaying`](https://docs.unity3d.com/ScriptReference/AudioSource-isPlaying.html) to the ViewModel.

 [AudioSourceLoopBinder](Aspid.MVVM.StarterKit.AudioSourceLoopBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`loop`](https://docs.unity3d.com/ScriptReference/AudioSource-loop.html).

 [AudioSourceLoopEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceLoopEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`loop`](https://docs.unity3d.com/ScriptReference/AudioSource-loop.html) on each element.

 [AudioSourceLoopEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceLoopEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`loop`](https://docs.unity3d.com/ScriptReference/AudioSource-loop.html).

 [AudioSourceLoopMonoBinder](Aspid.MVVM.StarterKit.AudioSourceLoopMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`loop`](https://docs.unity3d.com/ScriptReference/AudioSource-loop.html).

 [AudioSourceMinMaxDistanceBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html), or a single number written to both.

 [AudioSourceMinMaxDistanceEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) on each element as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html).

 [AudioSourceMinMaxDistanceEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html).

 [AudioSourceMinMaxDistanceMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html), or a single number written to both.

 [AudioSourceMinMaxDistanceSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html).

 [AudioSourceMinMaxDistanceSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html).

 [AudioSourceMuteBinder](Aspid.MVVM.StarterKit.AudioSourceMuteBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`mute`](https://docs.unity3d.com/ScriptReference/AudioSource-mute.html).

 [AudioSourceMuteEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMuteEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`mute`](https://docs.unity3d.com/ScriptReference/AudioSource-mute.html) on each element.

 [AudioSourceMuteEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMuteEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`mute`](https://docs.unity3d.com/ScriptReference/AudioSource-mute.html).

 [AudioSourceMuteMonoBinder](Aspid.MVVM.StarterKit.AudioSourceMuteMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`mute`](https://docs.unity3d.com/ScriptReference/AudioSource-mute.html).

 [AudioSourceOutputAudioMixerGroupBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html).

 [AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html) on each element.

 [AudioSourceOutputAudioMixerGroupEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html).

 [AudioSourceOutputAudioMixerGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html).

 [AudioSourceOutputAudioMixerGroupSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html).

 [AudioSourceOutputAudioMixerGroupSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceOutputAudioMixerGroupSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`outputAudioMixerGroup`](https://docs.unity3d.com/ScriptReference/AudioSource-outputAudioMixerGroup.html).

 [AudioSourcePanStereoBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html).

 [AudioSourcePanStereoEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html) on each element.

 [AudioSourcePanStereoEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html).

 [AudioSourcePanStereoMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html).

 [AudioSourcePanStereoSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html).

 [AudioSourcePanStereoSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePanStereoSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`panStereo`](https://docs.unity3d.com/ScriptReference/AudioSource-panStereo.html).

 [AudioSourcePauseMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePauseMonoBinder.md)

[`AudioSourcePlaybackMonoBinder`](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md) that calls [`Pause`](https://docs.unity3d.com/ScriptReference/AudioSource-Pause.html).

 [AudioSourcePitchBinder](Aspid.MVVM.StarterKit.AudioSourcePitchBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html).

 [AudioSourcePitchEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePitchEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html) on each element.

 [AudioSourcePitchEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePitchEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html).

 [AudioSourcePitchMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePitchMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html).

 [AudioSourcePitchSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourcePitchSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html).

 [AudioSourcePitchSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePitchSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`pitch`](https://docs.unity3d.com/ScriptReference/AudioSource-pitch.html).

 [AudioSourcePlayMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePlayMonoBinder.md)

[`AudioSourcePlaybackMonoBinder`](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md) that calls [`Play`](https://docs.unity3d.com/ScriptReference/AudioSource-Play.html).

 [AudioSourcePlayOneShotMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePlayOneShotMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that plays each bound [`AudioClip`](https://docs.unity3d.com/ScriptReference/AudioClip.html) once.

 [AudioSourcePlaybackMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that exposes one playback operation on an
[`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html) to the ViewModel as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

 [AudioSourcePriorityBinder](Aspid.MVVM.StarterKit.AudioSourcePriorityBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html).

 [AudioSourcePriorityEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePriorityEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html) on each element.

 [AudioSourcePriorityEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePriorityEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html).

 [AudioSourcePriorityMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePriorityMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html).

 [AudioSourcePrioritySwitcherBinder](Aspid.MVVM.StarterKit.AudioSourcePrioritySwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html).

 [AudioSourcePrioritySwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourcePrioritySwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`priority`](https://docs.unity3d.com/ScriptReference/AudioSource-priority.html).

 [AudioSourceReverbZoneMixBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html).

 [AudioSourceReverbZoneMixEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html) on each element.

 [AudioSourceReverbZoneMixEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html).

 [AudioSourceReverbZoneMixMonoBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html).

 [AudioSourceReverbZoneMixSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html).

 [AudioSourceReverbZoneMixSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceReverbZoneMixSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`reverbZoneMix`](https://docs.unity3d.com/ScriptReference/AudioSource-reverbZoneMix.html).

 [AudioSourceSpatialBlendBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html).

 [AudioSourceSpatialBlendEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html) on each element.

 [AudioSourceSpatialBlendEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html).

 [AudioSourceSpatialBlendMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html).

 [AudioSourceSpatialBlendSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html).

 [AudioSourceSpatialBlendSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpatialBlendSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`spatialBlend`](https://docs.unity3d.com/ScriptReference/AudioSource-spatialBlend.html).

 [AudioSourceSpreadBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html).

 [AudioSourceSpreadEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html) on each element.

 [AudioSourceSpreadEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html).

 [AudioSourceSpreadMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html).

 [AudioSourceSpreadSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html).

 [AudioSourceSpreadSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceSpreadSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`spread`](https://docs.unity3d.com/ScriptReference/AudioSource-spread.html).

 [AudioSourceStopMonoBinder](Aspid.MVVM.StarterKit.AudioSourceStopMonoBinder.md)

[`AudioSourcePlaybackMonoBinder`](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md) that calls [`Stop`](https://docs.unity3d.com/ScriptReference/AudioSource-Stop.html).

 [AudioSourceTimeBinder](Aspid.MVVM.StarterKit.AudioSourceTimeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html).

 [AudioSourceTimeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html) on each element.

 [AudioSourceTimeEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html).

 [AudioSourceTimeMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html).

 [AudioSourceTimeSamplesBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html).

 [AudioSourceTimeSamplesEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html) on each element.

 [AudioSourceTimeSamplesEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html).

 [AudioSourceTimeSamplesMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html).

 [AudioSourceTimeSamplesSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html).

 [AudioSourceTimeSamplesSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSamplesSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html).

 [AudioSourceTimeSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html).

 [AudioSourceTimeSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceTimeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html).

 [AudioSourceToSourceMonoBinder](Aspid.MVVM.StarterKit.AudioSourceToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html).

 [AudioSourceUnPauseMonoBinder](Aspid.MVVM.StarterKit.AudioSourceUnPauseMonoBinder.md)

[`AudioSourcePlaybackMonoBinder`](Aspid.MVVM.StarterKit.AudioSourcePlaybackMonoBinder.md) that calls [`UnPause`](https://docs.unity3d.com/ScriptReference/AudioSource-UnPause.html).

 [AudioSourceVolumeBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html).

 [AudioSourceVolumeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html) on each element.

 [AudioSourceVolumeEnumMonoBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html).

 [AudioSourceVolumeMonoBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html).

 [AudioSourceVolumeSwitcherBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html).

 [AudioSourceVolumeSwitcherMonoBinder](Aspid.MVVM.StarterKit.AudioSourceVolumeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`volume`](https://docs.unity3d.com/ScriptReference/AudioSource-volume.html).

 [BehaviourEnabledBinder](Aspid.MVVM.StarterKit.BehaviourEnabledBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html).

 [BehaviourEnabledByBindMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledByBindMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that stays enabled while a binding exists and disables itself otherwise.

 [BehaviourEnabledEnumGroupMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html) on each element.

 [BehaviourEnabledEnumMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html).

 [BehaviourEnabledMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html).

 [BehaviourToSourceMonoBinder](Aspid.MVVM.StarterKit.BehaviourToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Behaviour`](https://docs.unity3d.com/ScriptReference/Behaviour.html).

 [Binder\<TProperty\>](Aspid.MVVM.StarterKit.Binder-1.md)

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that binds a single property through its accessors, applying an optional
converter in both directions. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md), the current property value is sent to the ViewModel on binding.

 [BinderLogger](Aspid.MVVM.StarterKit.BinderLogger.md)

Writes binder messages in one shape shared by all binders.

 [BinderMath](Aspid.MVVM.StarterKit.BinderMath.md)

Sanitizing helpers that report the value they had to replace.

 [BoolAggregatorInputMonoBinder](Aspid.MVVM.StarterKit.BoolAggregatorInputMonoBinder.md)

[`AggregatorInputMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md) that feeds one <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> into an
[`AndBoolMonoBinder`](Aspid.MVVM.StarterKit.AndBoolMonoBinder.md) or [`OrBoolMonoBinder`](Aspid.MVVM.StarterKit.OrBoolMonoBinder.md).

 [BoolConverterAsset](Aspid.MVVM.StarterKit.BoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) values.

 [BoolInvertConverter](Aspid.MVVM.StarterKit.BoolInvertConverter.md)

Negates a boolean.

 [BoolLogicConverter](Aspid.MVVM.StarterKit.BoolLogicConverter.md)

Combines a bound boolean with an authored one.

 [BoolToValueConverter\<T\>](Aspid.MVVM.StarterKit.BoolToValueConverter-1.md)

Picks one of two authored values based on a boolean, and reads the boolean back out of them.

 [BoundsConverterAsset](Aspid.MVVM.StarterKit.BoundsConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Bounds`](https://docs.unity3d.com/ScriptReference/Bounds.html) values.

 [BoundsToRectConverter](Aspid.MVVM.StarterKit.BoundsToRectConverter.md)

Flattens a bounding box onto a plane.

 [BoundsToRectConverterAsset](Aspid.MVVM.StarterKit.BoundsToRectConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Bounds`](https://docs.unity3d.com/ScriptReference/Bounds.html) to [`Rect`](https://docs.unity3d.com/ScriptReference/Rect.html) conversions.

 [BoundsToVector3ConverterAsset](Aspid.MVVM.StarterKit.BoundsToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Bounds`](https://docs.unity3d.com/ScriptReference/Bounds.html) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [BoundsToVectorConverter](Aspid.MVVM.StarterKit.BoundsToVectorConverter.md)

Reads one vector of a bounding box: its middle, its size or its half-size.

 [BoxCollider2DOffsetCombineConverter](Aspid.MVVM.StarterKit.BoxCollider2DOffsetCombineConverter.md)

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`BoxCollider2D`](https://docs.unity3d.com/ScriptReference/BoxCollider2D.html)'s offset.

 [BoxCollider2DSizeBinder](Aspid.MVVM.StarterKit.BoxCollider2DSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider2D-size.html).

 [BoxCollider2DSizeCombineConverter](Aspid.MVVM.StarterKit.BoxCollider2DSizeCombineConverter.md)

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`BoxCollider2D`](https://docs.unity3d.com/ScriptReference/BoxCollider2D.html)'s size.

 [BoxCollider2DSizeMonoBinder](Aspid.MVVM.StarterKit.BoxCollider2DSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider2D-size.html).

 [BoxColliderCenterBinder](Aspid.MVVM.StarterKit.BoxColliderCenterBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

 [BoxColliderCenterCombineConverter](Aspid.MVVM.StarterKit.BoxColliderCenterCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`BoxCollider`](https://docs.unity3d.com/ScriptReference/BoxCollider.html)'s center.

 [BoxColliderCenterEnumGroupMonoBinder](Aspid.MVVM.StarterKit.BoxColliderCenterEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html) on each element.

 [BoxColliderCenterEnumMonoBinder](Aspid.MVVM.StarterKit.BoxColliderCenterEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

 [BoxColliderCenterMonoBinder](Aspid.MVVM.StarterKit.BoxColliderCenterMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

 [BoxColliderCenterSwitcherBinder](Aspid.MVVM.StarterKit.BoxColliderCenterSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

 [BoxColliderCenterSwitcherMonoBinder](Aspid.MVVM.StarterKit.BoxColliderCenterSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

 [BoxColliderSizeBinder](Aspid.MVVM.StarterKit.BoxColliderSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html).

 [BoxColliderSizeCombineConverter](Aspid.MVVM.StarterKit.BoxColliderSizeCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`BoxCollider`](https://docs.unity3d.com/ScriptReference/BoxCollider.html)'s size.

 [BoxColliderSizeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.BoxColliderSizeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html) on each element.

 [BoxColliderSizeEnumMonoBinder](Aspid.MVVM.StarterKit.BoxColliderSizeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html).

 [BoxColliderSizeMonoBinder](Aspid.MVVM.StarterKit.BoxColliderSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html).

 [BoxColliderSizeSwitcherBinder](Aspid.MVVM.StarterKit.BoxColliderSizeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html).

 [BoxColliderSizeSwitcherMonoBinder](Aspid.MVVM.StarterKit.BoxColliderSizeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider-size.html).

 [BoxColliderToSourceMonoBinder](Aspid.MVVM.StarterKit.BoxColliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`BoxCollider`](https://docs.unity3d.com/ScriptReference/BoxCollider.html).

 [ButtonCommandBinder\<T\>](Aspid.MVVM.StarterKit.ButtonCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.ButtonCommandBinder-1.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_1_Param).

 [ButtonCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ButtonCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandBinder-2.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_2_Param1), [`ButtonCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandBinder-2.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_2_Param2).

 [ButtonCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ButtonCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_3_Param1), [`ButtonCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_3_Param2), [`ButtonCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ButtonCommandBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_3_Param3).

 [ButtonCommandBinder\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.ButtonCommandBinder-4.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandBinder<T1, T2, T3, T4>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_4_Param1), [`ButtonCommandBinder<T1, T2, T3, T4>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_4_Param2),
[`ButtonCommandBinder<T1, T2, T3, T4>.Param3`](Aspid.MVVM.StarterKit.ButtonCommandBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_4_Param3), [`ButtonCommandBinder<T1, T2, T3, T4>.Param4`](Aspid.MVVM.StarterKit.ButtonCommandBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandBinder_4_Param4).

 [ButtonCommandBinder](Aspid.MVVM.StarterKit.ButtonCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html).

 [ButtonCommandBoolMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandBoolMonoBinder.md)

[`ButtonCommandMonoBinder<T>`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md) with a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> parameter.

 [ButtonCommandFloatMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandFloatMonoBinder.md)

[`ButtonCommandMonoBinder<T>`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md) with a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> parameter.

 [ButtonCommandIntMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandIntMonoBinder.md)

[`ButtonCommandMonoBinder<T>`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md) with an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> parameter.

 [ButtonCommandMonoBinder\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-4.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandMonoBinder<T1, T2, T3, T4>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_4_Param1), [`ButtonCommandMonoBinder<T1, T2, T3, T4>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_4_Param2),
[`ButtonCommandMonoBinder<T1, T2, T3, T4>.Param3`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_4_Param3), [`ButtonCommandMonoBinder<T1, T2, T3, T4>.Param4`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-4.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_4_Param4).

 [ButtonCommandMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html).

 [ButtonCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_1_Param).

 [ButtonCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_3_Param1), [`ButtonCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_3_Param2), [`ButtonCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_3_Param3).

 [ButtonCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on [`onClick`](https://docs.unity3d.com/ScriptReference/UI-Button-onClick.html)
with [`ButtonCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_2_Param1), [`ButtonCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ButtonCommandMonoBinder_2_Param2).

 [ButtonCommandObjectMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandObjectMonoBinder.md)

[`ButtonCommandMonoBinder<T>`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md) with an [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) parameter.

 [ButtonCommandStringMonoBinder](Aspid.MVVM.StarterKit.ButtonCommandStringMonoBinder.md)

[`ButtonCommandMonoBinder<T>`](Aspid.MVVM.StarterKit.ButtonCommandMonoBinder-1.md) with a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> parameter.

 [ButtonToSourceMonoBinder](Aspid.MVVM.StarterKit.ButtonToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Button`](https://docs.unity3d.com/ScriptReference/UI-Button.html).

 [ByteSizeConverter](Aspid.MVVM.StarterKit.ByteSizeConverter.md)

Formats a byte count as a readable size.

 [CachedConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CachedConverter-2.md)

Remembers the last conversion and reuses it while the input is unchanged.

 [CameraBackgroundColorBinder](Aspid.MVVM.StarterKit.CameraBackgroundColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`backgroundColor`](https://docs.unity3d.com/ScriptReference/Camera-backgroundColor.html).

 [CameraBackgroundColorMonoBinder](Aspid.MVVM.StarterKit.CameraBackgroundColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`backgroundColor`](https://docs.unity3d.com/ScriptReference/Camera-backgroundColor.html).

 [CameraFieldOfViewBinder](Aspid.MVVM.StarterKit.CameraFieldOfViewBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`fieldOfView`](https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html).

 [CameraFieldOfViewMonoBinder](Aspid.MVVM.StarterKit.CameraFieldOfViewMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`fieldOfView`](https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html).

 [CameraOrthographicBinder](Aspid.MVVM.StarterKit.CameraOrthographicBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`orthographic`](https://docs.unity3d.com/ScriptReference/Camera-orthographic.html).

 [CameraOrthographicMonoBinder](Aspid.MVVM.StarterKit.CameraOrthographicMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`orthographic`](https://docs.unity3d.com/ScriptReference/Camera-orthographic.html).

 [CameraOrthographicSizeBinder](Aspid.MVVM.StarterKit.CameraOrthographicSizeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`orthographicSize`](https://docs.unity3d.com/ScriptReference/Camera-orthographicSize.html).

 [CameraOrthographicSizeMonoBinder](Aspid.MVVM.StarterKit.CameraOrthographicSizeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`orthographicSize`](https://docs.unity3d.com/ScriptReference/Camera-orthographicSize.html).

 [CanvasGroupAlphaBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html).

 [CanvasGroupAlphaEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html) on each element.

 [CanvasGroupAlphaEnumMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html).

 [CanvasGroupAlphaMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html).

 [CanvasGroupAlphaSwitcherBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html).

 [CanvasGroupAlphaSwitcherMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupAlphaSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`alpha`](https://docs.unity3d.com/ScriptReference/CanvasGroup-alpha.html).

 [CanvasGroupBlocksRaycastsBinder](Aspid.MVVM.StarterKit.CanvasGroupBlocksRaycastsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`blocksRaycasts`](https://docs.unity3d.com/ScriptReference/CanvasGroup-blocksRaycasts.html).

 [CanvasGroupBlocksRaycastsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupBlocksRaycastsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`blocksRaycasts`](https://docs.unity3d.com/ScriptReference/CanvasGroup-blocksRaycasts.html) on each element.

 [CanvasGroupBlocksRaycastsEnumMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupBlocksRaycastsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`blocksRaycasts`](https://docs.unity3d.com/ScriptReference/CanvasGroup-blocksRaycasts.html).

 [CanvasGroupBlocksRaycastsMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupBlocksRaycastsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`blocksRaycasts`](https://docs.unity3d.com/ScriptReference/CanvasGroup-blocksRaycasts.html).

 [CanvasGroupIgnoreParentGroupsBinder](Aspid.MVVM.StarterKit.CanvasGroupIgnoreParentGroupsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`ignoreParentGroups`](https://docs.unity3d.com/ScriptReference/CanvasGroup-ignoreParentGroups.html).

 [CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupIgnoreParentGroupsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`ignoreParentGroups`](https://docs.unity3d.com/ScriptReference/CanvasGroup-ignoreParentGroups.html) on each element.

 [CanvasGroupIgnoreParentGroupsEnumMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupIgnoreParentGroupsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`ignoreParentGroups`](https://docs.unity3d.com/ScriptReference/CanvasGroup-ignoreParentGroups.html).

 [CanvasGroupIgnoreParentGroupsMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupIgnoreParentGroupsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`ignoreParentGroups`](https://docs.unity3d.com/ScriptReference/CanvasGroup-ignoreParentGroups.html).

 [CanvasGroupInteractableBinder](Aspid.MVVM.StarterKit.CanvasGroupInteractableBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`interactable`](https://docs.unity3d.com/ScriptReference/CanvasGroup-interactable.html).

 [CanvasGroupInteractableEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupInteractableEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`interactable`](https://docs.unity3d.com/ScriptReference/CanvasGroup-interactable.html) on each element.

 [CanvasGroupInteractableEnumMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupInteractableEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`interactable`](https://docs.unity3d.com/ScriptReference/CanvasGroup-interactable.html).

 [CanvasGroupInteractableMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupInteractableMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`interactable`](https://docs.unity3d.com/ScriptReference/CanvasGroup-interactable.html).

 [CanvasGroupToSourceMonoBinder](Aspid.MVVM.StarterKit.CanvasGroupToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`CanvasGroup`](https://docs.unity3d.com/ScriptReference/CanvasGroup.html).

 [CanvasOverrideSortingBinder](Aspid.MVVM.StarterKit.CanvasOverrideSortingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`overrideSorting`](https://docs.unity3d.com/ScriptReference/Canvas-overrideSorting.html).

 [CanvasOverrideSortingMonoBinder](Aspid.MVVM.StarterKit.CanvasOverrideSortingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`overrideSorting`](https://docs.unity3d.com/ScriptReference/Canvas-overrideSorting.html).

 [CanvasScalerMatchWidthOrHeightBinder](Aspid.MVVM.StarterKit.CanvasScalerMatchWidthOrHeightBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`matchWidthOrHeight`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-matchWidthOrHeight.html).

 [CanvasScalerMatchWidthOrHeightMonoBinder](Aspid.MVVM.StarterKit.CanvasScalerMatchWidthOrHeightMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`matchWidthOrHeight`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-matchWidthOrHeight.html).

 [CanvasScalerReferenceResolutionBinder](Aspid.MVVM.StarterKit.CanvasScalerReferenceResolutionBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`referenceResolution`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-referenceResolution.html).

 [CanvasScalerReferenceResolutionMonoBinder](Aspid.MVVM.StarterKit.CanvasScalerReferenceResolutionMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`referenceResolution`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-referenceResolution.html).

 [CanvasScalerScaleFactorBinder](Aspid.MVVM.StarterKit.CanvasScalerScaleFactorBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`scaleFactor`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-scaleFactor.html).

 [CanvasScalerScaleFactorMonoBinder](Aspid.MVVM.StarterKit.CanvasScalerScaleFactorMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`scaleFactor`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-scaleFactor.html).

 [CanvasScalerUiScaleModeBinder](Aspid.MVVM.StarterKit.CanvasScalerUiScaleModeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`uiScaleMode`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-uiScaleMode.html).

 [CanvasScalerUiScaleModeMonoBinder](Aspid.MVVM.StarterKit.CanvasScalerUiScaleModeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`uiScaleMode`](https://docs.unity3d.com/ScriptReference/UI-CanvasScaler-uiScaleMode.html).

 [CanvasSortingOrderBinder](Aspid.MVVM.StarterKit.CanvasSortingOrderBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Canvas-sortingOrder.html).

 [CanvasSortingOrderMonoBinder](Aspid.MVVM.StarterKit.CanvasSortingOrderMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Canvas-sortingOrder.html).

 [CapsuleCollider2DSizeBinder](Aspid.MVVM.StarterKit.CapsuleCollider2DSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/CapsuleCollider2D-size.html).

 [CapsuleCollider2DSizeMonoBinder](Aspid.MVVM.StarterKit.CapsuleCollider2DSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/CapsuleCollider2D-size.html).

 [CapsuleColliderCenterBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

 [CapsuleColliderCenterCombineConverter](Aspid.MVVM.StarterKit.CapsuleColliderCenterCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`CapsuleCollider`](https://docs.unity3d.com/ScriptReference/CapsuleCollider.html)'s center.

 [CapsuleColliderCenterEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html) on each element.

 [CapsuleColliderCenterEnumMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

 [CapsuleColliderCenterMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

 [CapsuleColliderCenterSwitcherBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

 [CapsuleColliderCenterSwitcherMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderCenterSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

 [CapsuleColliderDirectionBinder](Aspid.MVVM.StarterKit.CapsuleColliderDirectionBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`direction`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-direction.html).

 [CapsuleColliderDirectionMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderDirectionMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`direction`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-direction.html).

 [CapsuleColliderHeightBinder](Aspid.MVVM.StarterKit.CapsuleColliderHeightBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`height`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-height.html).

 [CapsuleColliderHeightMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderHeightMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`height`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-height.html).

 [CapsuleColliderRadiusBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html).

 [CapsuleColliderRadiusEnumGroupMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html) on each element.

 [CapsuleColliderRadiusEnumMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html).

 [CapsuleColliderRadiusMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html).

 [CapsuleColliderRadiusSwitcherBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html).

 [CapsuleColliderRadiusSwitcherMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderRadiusSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`radius`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-radius.html).

 [CapsuleColliderToSourceMonoBinder](Aspid.MVVM.StarterKit.CapsuleColliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`CapsuleCollider`](https://docs.unity3d.com/ScriptReference/CapsuleCollider.html).

 [CasterBinder\<TTarget, TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-3.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that converts a <code class="typeparamref">TFrom</code> value
to <code class="typeparamref">TTo</code> and forwards it, together with the stored <code class="typeparamref">TTarget</code>, to a target setter.

 [CasterBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-2.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that converts a <code class="typeparamref">TFrom</code> value
to <code class="typeparamref">TTo</code> and forwards it to a target setter.

 [CasterMonoBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that converts a bound <code class="typeparamref">TFrom</code> to <code class="typeparamref">TTo</code>
with a serialized converter and forwards the result to a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html).

 [CircleCollider2DRadiusBinder](Aspid.MVVM.StarterKit.CircleCollider2DRadiusBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/CircleCollider2D-radius.html).

 [CircleCollider2DRadiusMonoBinder](Aspid.MVVM.StarterKit.CircleCollider2DRadiusMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/CircleCollider2D-radius.html).

 [ClampNumberConverter](Aspid.MVVM.StarterKit.ClampNumberConverter.md)

Keeps a number inside a range.

 [CollectionAggregateConverter](Aspid.MVVM.StarterKit.CollectionAggregateConverter.md)

Reduces a collection of numbers to one.

 [CollectionBinder\<T\>](Aspid.MVVM.StarterKit.CollectionBinder-1.md)

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that receives a read-only collection and reflects its changes onto a View.
Observable and filtered lists are followed through their change notifications.

 [CollectionContainsToBoolConverter\<T\>](Aspid.MVVM.StarterKit.CollectionContainsToBoolConverter-1.md)

Reports whether a collection holds a matching item.

 [CollectionCountConverter\<T\>](Aspid.MVVM.StarterKit.CollectionCountConverter-1.md)

Counts the items in a collection.

 [CollectionCountMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionCountMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that reports how many items a bound collection holds and whether it is empty.
Observable and filtered lists are followed; a plain list is read once; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> reports zero.

 [CollectionCountToStringConverter\<T\>](Aspid.MVVM.StarterKit.CollectionCountToStringConverter-1.md)

Writes how many items a collection holds, in words.

 [CollectionElementAtConverter\<T\>](Aspid.MVVM.StarterKit.CollectionElementAtConverter-1.md)

Takes one item out of a list by index.

 [CollectionEmptyToBoolConverter\<T\>](Aspid.MVVM.StarterKit.CollectionEmptyToBoolConverter-1.md)

Reports whether a collection has anything in it.

 [CollectionFirstConverter\<T\>](Aspid.MVVM.StarterKit.CollectionFirstConverter-1.md)

Takes the first item of a sequence.

 [CollectionJoinToStringConverter\<T\>](Aspid.MVVM.StarterKit.CollectionJoinToStringConverter-1.md)

Joins a collection into one string.

 [CollectionLastConverter\<T\>](Aspid.MVVM.StarterKit.CollectionLastConverter-1.md)

Takes the last item of a sequence.

 [CollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that receives a read-only collection and reflects it onto a View.
Observable and filtered lists are rebuilt on every change: [`CollectionMonoBinder<T>.OnReset`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnReset), then [`CollectionMonoBinder<T>.OnAdded`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__) with the current items.

 [CollectionTakeConverter\<T\>](Aspid.MVVM.StarterKit.CollectionTakeConverter-1.md)

Keeps a few items off one end of a sequence.

 [CollectionViewModelBinder\<TView\>](Aspid.MVVM.StarterKit.CollectionViewModelBinder-1.md)

[`CollectionBinder<T>`](Aspid.MVVM.StarterKit.CollectionBinder-1.md) that shows bound ViewModels in a fixed set of pre-placed views, in order.
Views beyond the item count are deactivated; items beyond the view count are not shown. Every change rebuilds the whole set.

 [CollectionViewModelBinder](Aspid.MVVM.StarterKit.CollectionViewModelBinder.md)

[`CollectionViewModelBinder<T>`](Aspid.MVVM.StarterKit.CollectionViewModelBinder-1.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [CollectionViewModelMonoBinder](Aspid.MVVM.StarterKit.CollectionViewModelMonoBinder.md)

[`CollectionViewModelMonoBinder<T>`](Aspid.MVVM.StarterKit.CollectionViewModelMonoBinder-1.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [CollectionViewModelMonoBinder\<TView\>](Aspid.MVVM.StarterKit.CollectionViewModelMonoBinder-1.md)

[`CollectionMonoBinder<T>`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md) that shows bound ViewModels in a fixed set of pre-placed views, in order.
Views beyond the item count are deactivated; items beyond the view count are not shown.

 [Collider2DDensityBinder](Aspid.MVVM.StarterKit.Collider2DDensityBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`density`](https://docs.unity3d.com/ScriptReference/Collider2D-density.html).

 [Collider2DDensityMonoBinder](Aspid.MVVM.StarterKit.Collider2DDensityMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`density`](https://docs.unity3d.com/ScriptReference/Collider2D-density.html).

 [Collider2DIsTriggerBinder](Aspid.MVVM.StarterKit.Collider2DIsTriggerBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider2D-isTrigger.html).

 [Collider2DIsTriggerMonoBinder](Aspid.MVVM.StarterKit.Collider2DIsTriggerMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider2D-isTrigger.html).

 [Collider2DMaterialBinder](Aspid.MVVM.StarterKit.Collider2DMaterialBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`sharedMaterial`](https://docs.unity3d.com/ScriptReference/Collider2D-sharedMaterial.html).

 [Collider2DMaterialMonoBinder](Aspid.MVVM.StarterKit.Collider2DMaterialMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`sharedMaterial`](https://docs.unity3d.com/ScriptReference/Collider2D-sharedMaterial.html).

 [Collider2DOffsetBinder](Aspid.MVVM.StarterKit.Collider2DOffsetBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`offset`](https://docs.unity3d.com/ScriptReference/Collider2D-offset.html).

 [Collider2DOffsetMonoBinder](Aspid.MVVM.StarterKit.Collider2DOffsetMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`offset`](https://docs.unity3d.com/ScriptReference/Collider2D-offset.html).

 [ColliderContactOffsetBinder](Aspid.MVVM.StarterKit.ColliderContactOffsetBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`contactOffset`](https://docs.unity3d.com/ScriptReference/Collider-contactOffset.html).

 [ColliderContactOffsetMonoBinder](Aspid.MVVM.StarterKit.ColliderContactOffsetMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`contactOffset`](https://docs.unity3d.com/ScriptReference/Collider-contactOffset.html).

 [ColliderEnabledBinder](Aspid.MVVM.StarterKit.ColliderEnabledBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Collider-enabled.html).

 [ColliderEnabledEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ColliderEnabledEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Collider-enabled.html) on each element.

 [ColliderEnabledEnumMonoBinder](Aspid.MVVM.StarterKit.ColliderEnabledEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Collider-enabled.html).

 [ColliderEnabledMonoBinder](Aspid.MVVM.StarterKit.ColliderEnabledMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Collider-enabled.html).

 [ColliderExcludeLayersBinder](Aspid.MVVM.StarterKit.ColliderExcludeLayersBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`excludeLayers`](https://docs.unity3d.com/ScriptReference/Collider-excludeLayers.html).

 [ColliderExcludeLayersMonoBinder](Aspid.MVVM.StarterKit.ColliderExcludeLayersMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`excludeLayers`](https://docs.unity3d.com/ScriptReference/Collider-excludeLayers.html).

 [ColliderIncludeLayersBinder](Aspid.MVVM.StarterKit.ColliderIncludeLayersBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`includeLayers`](https://docs.unity3d.com/ScriptReference/Collider-includeLayers.html).

 [ColliderIncludeLayersMonoBinder](Aspid.MVVM.StarterKit.ColliderIncludeLayersMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`includeLayers`](https://docs.unity3d.com/ScriptReference/Collider-includeLayers.html).

 [ColliderIsTriggerBinder](Aspid.MVVM.StarterKit.ColliderIsTriggerBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider-isTrigger.html).

 [ColliderIsTriggerEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ColliderIsTriggerEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider-isTrigger.html) on each element.

 [ColliderIsTriggerEnumMonoBinder](Aspid.MVVM.StarterKit.ColliderIsTriggerEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider-isTrigger.html).

 [ColliderIsTriggerMonoBinder](Aspid.MVVM.StarterKit.ColliderIsTriggerMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`isTrigger`](https://docs.unity3d.com/ScriptReference/Collider-isTrigger.html).

 [ColliderMaterialBinder](Aspid.MVVM.StarterKit.ColliderMaterialBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html).

 [ColliderMaterialEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ColliderMaterialEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html) on each element.

 [ColliderMaterialEnumMonoBinder](Aspid.MVVM.StarterKit.ColliderMaterialEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html).

 [ColliderMaterialMonoBinder](Aspid.MVVM.StarterKit.ColliderMaterialMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html).

 [ColliderMaterialSwitcherBinder](Aspid.MVVM.StarterKit.ColliderMaterialSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html).

 [ColliderMaterialSwitcherMonoBinder](Aspid.MVVM.StarterKit.ColliderMaterialSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`material`](https://docs.unity3d.com/ScriptReference/Collider-material.html).

 [ColliderProvidesContactsBinder](Aspid.MVVM.StarterKit.ColliderProvidesContactsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`providesContacts`](https://docs.unity3d.com/ScriptReference/Collider-providesContacts.html).

 [ColliderProvidesContactsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ColliderProvidesContactsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`providesContacts`](https://docs.unity3d.com/ScriptReference/Collider-providesContacts.html) on each element.

 [ColliderProvidesContactsEnumMonoBinder](Aspid.MVVM.StarterKit.ColliderProvidesContactsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`providesContacts`](https://docs.unity3d.com/ScriptReference/Collider-providesContacts.html).

 [ColliderProvidesContactsMonoBinder](Aspid.MVVM.StarterKit.ColliderProvidesContactsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`providesContacts`](https://docs.unity3d.com/ScriptReference/Collider-providesContacts.html).

 [ColliderToSourceMonoBinder](Aspid.MVVM.StarterKit.ColliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Collider`](https://docs.unity3d.com/ScriptReference/Collider.html).

 [Color32ToColorConverterAsset](Aspid.MVVM.StarterKit.Color32ToColorConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html) to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) conversions.

 [ColorAlphaConverter](Aspid.MVVM.StarterKit.ColorAlphaConverter.md)

Changes the alpha of a color, leaving its hue alone.

 [ColorBlockAlphaConverter](Aspid.MVVM.StarterKit.ColorBlockAlphaConverter.md)

Changes the alpha of every color in a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

 [ColorBlockConverterAsset](Aspid.MVVM.StarterKit.ColorBlockConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) values.

 [ColorBlockFadeDurationConverter](Aspid.MVVM.StarterKit.ColorBlockFadeDurationConverter.md)

Sets how long a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) takes to fade between states.

 [ColorBlockStateConverter](Aspid.MVVM.StarterKit.ColorBlockStateConverter.md)

Writes one authored color into the chosen states of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

 [ColorBlockTintConverter](Aspid.MVVM.StarterKit.ColorBlockTintConverter.md)

Tints the chosen colors of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

 [ColorCanExecuteHandler](Aspid.MVVM.StarterKit.ColorCanExecuteHandler.md)

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that switches a [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html) between two colors by the command state.

 [ColorChannelConverter](Aspid.MVVM.StarterKit.ColorChannelConverter.md)

Applies one arithmetic operation to the chosen channels of a color.

 [ColorChannelsExtensions](Aspid.MVVM.StarterKit.ColorChannelsExtensions.md)

Per-channel access to a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) by [`ColorChannels`](Aspid.MVVM.StarterKit.ColorChannels.md).

 [ColorColor32Converter](Aspid.MVVM.StarterKit.ColorColor32Converter.md)

Converts between a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) and a [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html), in either direction.

 [ColorConverterAsset](Aspid.MVVM.StarterKit.ColorConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) values.

 [ColorGrayscaleConverter](Aspid.MVVM.StarterKit.ColorGrayscaleConverter.md)

Desaturates a color.

 [ColorHsvConverter](Aspid.MVVM.StarterKit.ColorHsvConverter.md)

Shifts a color in HSV space.

 [ColorLerpConverter](Aspid.MVVM.StarterKit.ColorLerpConverter.md)

Moves between two colors by a 0..1 amount.

 [ColorTintConverter](Aspid.MVVM.StarterKit.ColorTintConverter.md)

Combines a bound color with an authored one.

 [ColorToColor32ConverterAsset](Aspid.MVVM.StarterKit.ColorToColor32ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html) conversions.

 [ColorToColorBlockConverter](Aspid.MVVM.StarterKit.ColorToColorBlockConverter.md)

Builds a full [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) out of one color.

 [ColorToColorBlockConverterAsset](Aspid.MVVM.StarterKit.ColorToColorBlockConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) conversions.

 [ColorToHtmlStringConverter](Aspid.MVVM.StarterKit.ColorToHtmlStringConverter.md)

Writes a color as an HTML string.

 [ColorToStringConverterAsset](Aspid.MVVM.StarterKit.ColorToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [ColorToVector4ConverterAsset](Aspid.MVVM.StarterKit.ColorToVector4ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

 [ColorVector4Converter](Aspid.MVVM.StarterKit.ColorVector4Converter.md)

Converts between a color and a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html), in either direction.

 [CommandBinderExtensions](Aspid.MVVM.StarterKit.CommandBinderExtensions.md)

Helpers for command binders: swapping the bound command while keeping the
[`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) subscription, and reflecting <code>CanExecute</code> on a [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html).

 [CommandMonoBinder](Aspid.MVVM.StarterKit.CommandMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) and exposes [`CommandMonoBinder.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder.md#Aspid_MVVM_StarterKit_CommandMonoBinder_CanExecute) and [`CommandMonoBinder.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder.md#Aspid_MVVM_StarterKit_CommandMonoBinder_Execute) for it.

 [CommandMonoBinder\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.CommandMonoBinder-4.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) and exposes [`CommandMonoBinder<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-4.md) and [`CommandMonoBinder<T1, T2, T3, T4>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-4.md) for it.

 [CommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.CommandMonoBinder-3.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) and exposes [`CommandMonoBinder<T1, T2, T3>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-3.md) and [`CommandMonoBinder<T1, T2, T3>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-3.md) for it.

 [CommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) and exposes [`CommandMonoBinder<T1, T2>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md) and [`CommandMonoBinder<T1, T2>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md) for it.

 [CommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) and exposes [`CommandMonoBinder<T>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md#Aspid_MVVM_StarterKit_CommandMonoBinder_1_CanExecute__0_) and [`CommandMonoBinder<T>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md#Aspid_MVVM_StarterKit_CommandMonoBinder_1_Execute__0_) for it.

 [ComparisonCollectionOrder\<T\>](Aspid.MVVM.StarterKit.ComparisonCollectionOrder-1.md)

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that wraps a [`Comparison<T>`](https://learn.microsoft.com/dotnet/api/system.comparison-1) or an
[`IComparer<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1) for code-built sort orders.

 [ComponentFloatMonoBinder\<TComponent\>](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md)

Abstract base [`StarterKit.ComponentMonoBinder<T1, T2>?text=ComponentMonoBinder%3cTComponent%2c+float%3e`](Aspid.MVVM.StarterKit.md) that binds a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> property,
accepting every numeric type via [`IFloatBinder`](Aspid.MVVM.StarterKit.IFloatBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [ComponentIntMonoBinder\<TComponent\>](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md)

Abstract base [`StarterKit.ComponentMonoBinder<T1, T2>?text=ComponentMonoBinder%3cTComponent%2c+int%3e`](Aspid.MVVM.StarterKit.md) that binds an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> property,
accepting every numeric type via [`IIntBinder`](Aspid.MVVM.StarterKit.IIntBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [ComponentMonoBinder\<TComponent, TProperty\>](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds a single component property through its accessors,
applying an optional converter in both directions. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md), the current property value is sent to the ViewModel on binding.

 [ComponentObjectMonoBinder\<TComponent, TObject\>](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md)

Abstract base [`StarterKit.ComponentMonoBinder<T1, T2>?text=ComponentMonoBinder%3cTComponent%2c+TObject%3e`](Aspid.MVVM.StarterKit.md) that binds
a [`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html) reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

 [ComponentToSourceMonoBinder](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for any [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) that also reports it as
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">object</a> via [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).

 [ComponentToSourceMonoBinder\<TComponent\>](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that sends the target <code class="typeparamref">TComponent</code>
reference to the ViewModel on binding.

 [ComposeConverter\<TFrom, TMid, TTo\>](Aspid.MVVM.StarterKit.ComposeConverter-3.md)

Applies two converters in sequence, converting through an intermediate type.

 [ConcatStringConverter](Aspid.MVVM.StarterKit.ConcatStringConverter.md)

Wraps a string in authored text, and takes that text back off.

 [ConditionalCollectionFilter\<T\>](Aspid.MVVM.StarterKit.ConditionalCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that applies the nested filter only while enabled.
When disabled, or with an empty slot, everything passes.

 [ConditionalColorMonoBinder](Aspid.MVVM.StarterKit.ConditionalColorMonoBinder.md)

[`ConditionalMonoBinder<T>`](Aspid.MVVM.StarterKit.ConditionalMonoBinder-1.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) values.

 [ConditionalConverter\<T\>](Aspid.MVVM.StarterKit.ConditionalConverter-1.md)

Routes a value to one of two converters based on a predicate.

 [ConditionalFloatMonoBinder](Aspid.MVVM.StarterKit.ConditionalFloatMonoBinder.md)

[`ConditionalMonoBinder<T>`](Aspid.MVVM.StarterKit.ConditionalMonoBinder-1.md) for <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> values.

 [ConditionalMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.ConditionalMonoBinder-1.md)

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that forwards one of two configured values depending on the bound
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>.

 [ConditionalStringMonoBinder](Aspid.MVVM.StarterKit.ConditionalStringMonoBinder.md)

[`ConditionalMonoBinder<T>`](Aspid.MVVM.StarterKit.ConditionalMonoBinder-1.md) for <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> values.

 [ContentSizeFitterHorizontalFitBinder](Aspid.MVVM.StarterKit.ContentSizeFitterHorizontalFitBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`horizontalFit`](https://docs.unity3d.com/ScriptReference/UI-ContentSizeFitter-horizontalFit.html).

 [ContentSizeFitterHorizontalFitMonoBinder](Aspid.MVVM.StarterKit.ContentSizeFitterHorizontalFitMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`horizontalFit`](https://docs.unity3d.com/ScriptReference/UI-ContentSizeFitter-horizontalFit.html).

 [ContentSizeFitterVerticalFitBinder](Aspid.MVVM.StarterKit.ContentSizeFitterVerticalFitBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`verticalFit`](https://docs.unity3d.com/ScriptReference/UI-ContentSizeFitter-verticalFit.html).

 [ContentSizeFitterVerticalFitMonoBinder](Aspid.MVVM.StarterKit.ContentSizeFitterVerticalFitMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`verticalFit`](https://docs.unity3d.com/ScriptReference/UI-ContentSizeFitter-verticalFit.html).

 [ConverterAsset\<TFrom, TTo\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md)

A converter authored once as an asset and shared by every field that references it.

 [ConverterAssetReference\<TFrom, TTo\>](Aspid.MVVM.StarterKit.ConverterAssetReference-2.md)

Forwards conversion to a shared [`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md).

 [ConverterCollectionFilter\<T\>](Aspid.MVVM.StarterKit.ConverterCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element when an [`IConverter<T1, T2>`](Aspid.MVVM.StarterKit.IConverter-2.md)
to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) answers <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for it. An empty slot passes everything.

 [ConverterFallbackExtensions](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md)

Reports a failure and hands back the fallback in one call.

 [ConverterLogger](Aspid.MVVM.StarterKit.ConverterLogger.md)

Writes converter messages in one shape shared by all converters.

 [CountdownProgressConverter](Aspid.MVVM.StarterKit.CountdownProgressConverter.md)

Converts seconds remaining to a 0..1 progress value.

 [CurrencyConverter](Aspid.MVVM.StarterKit.CurrencyConverter.md)

Formats a number as an amount of currency.

 [CzechPluralRule](Aspid.MVVM.StarterKit.CzechPluralRule.md)

A word for one, a word for two to four, a word for the rest: Czech, Slovak.

 [DateTimeCompareConverter](Aspid.MVVM.StarterKit.DateTimeCompareConverter.md)

Compares a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) with a reference moment.

 [DateTimeFormatConverter](Aspid.MVVM.StarterKit.DateTimeFormatConverter.md)

Formats a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime).

 [DateTimeOffsetFormatConverter](Aspid.MVVM.StarterKit.DateTimeOffsetFormatConverter.md)

Formats a [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset).

 [DateTimeOffsetToStringConverterAsset](Aspid.MVVM.StarterKit.DateTimeOffsetToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [DateTimeToBoolConverterAsset](Aspid.MVVM.StarterKit.DateTimeToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [DateTimeToLongConverterAsset](Aspid.MVVM.StarterKit.DateTimeToLongConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) conversions.

 [DateTimeToStringConverterAsset](Aspid.MVVM.StarterKit.DateTimeToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [DateTimeToTimeSpanConverterAsset](Aspid.MVVM.StarterKit.DateTimeToTimeSpanConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) conversions.

 [DateTimeToUnixTimestampConverter](Aspid.MVVM.StarterKit.DateTimeToUnixTimestampConverter.md)

Converts a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to a Unix timestamp.

 [DebounceFloatMonoBinder](Aspid.MVVM.StarterKit.DebounceFloatMonoBinder.md)

[`DebounceMonoBinder<T>`](Aspid.MVVM.StarterKit.DebounceMonoBinder-1.md) that forwards the last <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> once the values stop
arriving.

 [DebounceMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.DebounceMonoBinder-1.md)

Abstract [`RateLimitedMonoBinder<T>`](Aspid.MVVM.StarterKit.RateLimitedMonoBinder-1.md) that forwards the last value once no new value has
arrived for the interval.

 [DebounceStringMonoBinder](Aspid.MVVM.StarterKit.DebounceStringMonoBinder.md)

[`DebounceMonoBinder<T>`](Aspid.MVVM.StarterKit.DebounceMonoBinder-1.md) that forwards the last <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> once the values stop
arriving.

 [DebugLogBinder](Aspid.MVVM.StarterKit.DebugLogBinder.md)

[`Binder`](Aspid.MVVM.Binder.md) that logs every bound value and reverse subscription to the console.

 [DebugLogMonoBinder](Aspid.MVVM.StarterKit.DebugLogMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that logs every bound value and reverse subscription to the console.

 [DecimalToStringConverterAsset](Aspid.MVVM.StarterKit.DecimalToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [DefaultStringConverter](Aspid.MVVM.StarterKit.DefaultStringConverter.md)

Substitutes a placeholder for a blank string.

 [DegreesRadiansConverter](Aspid.MVVM.StarterKit.DegreesRadiansConverter.md)

Converts between degrees and radians.

 [DelayFloatMonoBinder](Aspid.MVVM.StarterKit.DelayFloatMonoBinder.md)

[`DelayMonoBinder<T>`](Aspid.MVVM.StarterKit.DelayMonoBinder-1.md) that forwards every <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> after the interval.

 [DelayMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.DelayMonoBinder-1.md)

Abstract [`RateLimitedMonoBinder<T>`](Aspid.MVVM.StarterKit.RateLimitedMonoBinder-1.md) that forwards every value after the interval, in arrival
order.

 [DelayStringMonoBinder](Aspid.MVVM.StarterKit.DelayStringMonoBinder.md)

[`DelayMonoBinder<T>`](Aspid.MVVM.StarterKit.DelayMonoBinder-1.md) that forwards every <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> after the interval.

 [DelegateOneTimeBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneTimeBinder-1.md)

[`DelegateOneWayBinder<T>`](Aspid.MVVM.StarterKit.DelegateOneWayBinder-1.md) fixed to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md): the setter runs once, for the first value.

 [DelegateOneTimeBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneTimeBinder-2.md)

[`DelegateOneWayBinder<T1, T2>`](Aspid.MVVM.StarterKit.DelegateOneWayBinder-2.md) fixed to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md): the setter runs once, for the first value.

 [DelegateOneWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that forwards each ViewModel value to a setter action.

 [DelegateOneWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-2.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that forwards each ViewModel value, together with
the stored <code class="typeparamref">TTarget</code>, to a setter action.

 [DelegateOneWayToSourceBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that propagates View values back to the ViewModel.

 [DelegateOneWayToSourceBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-2.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that propagates View values back to the ViewModel,
passing the stored <code class="typeparamref">TTarget</code> to every callback.

 [DelegateTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that synchronises
a value in both directions between the ViewModel and the View.

 [DelegateTwoWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-2.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that synchronises
a value in both directions between the ViewModel and the View, passing the stored <code class="typeparamref">TTarget</code> to every callback.

 [DictionaryLookupConverter\<TKey, TValue\>](Aspid.MVVM.StarterKit.DictionaryLookupConverter-2.md)

Looks a key up in an authored table.

 [DirectionAngleConverter](Aspid.MVVM.StarterKit.DirectionAngleConverter.md)

Reads the angle a direction points in, and turns an angle back into a direction.

 [DoubleConverterAsset](Aspid.MVVM.StarterKit.DoubleConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Double`](https://learn.microsoft.com/dotnet/api/system.double) values.

 [DoubleToBoolConverterAsset](Aspid.MVVM.StarterKit.DoubleToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Double`](https://learn.microsoft.com/dotnet/api/system.double) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [DoubleToStringConverterAsset](Aspid.MVVM.StarterKit.DoubleToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Double`](https://learn.microsoft.com/dotnet/api/system.double) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [DropdownAlphaFadeSpeedBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds `alphaFadeSpeed`.

 [DropdownAlphaFadeSpeedEnumGroupMonoBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `alphaFadeSpeed`
on each element.

 [DropdownAlphaFadeSpeedEnumMonoBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `alphaFadeSpeed`.

 [DropdownAlphaFadeSpeedMonoBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds `alphaFadeSpeed`.

 [DropdownAlphaFadeSpeedSwitcherBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `alphaFadeSpeed`.

 [DropdownAlphaFadeSpeedSwitcherMonoBinder](Aspid.MVVM.StarterKit.DropdownAlphaFadeSpeedSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `alphaFadeSpeed`.

 [DropdownCommandBinder\<T\>](Aspid.MVVM.StarterKit.DropdownCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on `onValueChanged` with
the selected index and [`DropdownCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.DropdownCommandBinder-1.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_1_Param).

 [DropdownCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.DropdownCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on `onValueChanged` with
the selected index and [`DropdownCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.DropdownCommandBinder-2.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_2_Param1), [`DropdownCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.DropdownCommandBinder-2.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_2_Param2).

 [DropdownCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.DropdownCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on `onValueChanged` with
the selected index and [`DropdownCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.DropdownCommandBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_3_Param1), [`DropdownCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.DropdownCommandBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_3_Param2),
[`DropdownCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.DropdownCommandBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandBinder_3_Param3).

 [DropdownCommandBinder](Aspid.MVVM.StarterKit.DropdownCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on `onValueChanged` with
the selected index.

 [DropdownCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
`onValueChanged` with the selected index and [`DropdownCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_2_Param1),
[`DropdownCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_2_Param2).

 [DropdownCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
`onValueChanged` with the selected index and [`DropdownCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_3_Param1),
[`DropdownCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_3_Param2), [`DropdownCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_3_Param3).

 [DropdownCommandMonoBinder](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
`onValueChanged` with the selected index.

 [DropdownCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
`onValueChanged` with the selected index and [`DropdownCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.DropdownCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_DropdownCommandMonoBinder_1_Param).

 [DropdownExtensions](Aspid.MVVM.StarterKit.DropdownExtensions.md)

Extension methods that write validated values to a `TMP_Dropdown`.

 [DropdownOptionsBinder](Aspid.MVVM.StarterKit.DropdownOptionsBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds `options` from labels, sprites or option
data.

 [DropdownOptionsByEnumMonoBinder](Aspid.MVVM.StarterKit.DropdownOptionsByEnumMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that fills `options` with the values of
the bound enum type.

 [DropdownOptionsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.DropdownOptionsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `options`
on each element.

 [DropdownOptionsEnumMonoBinder](Aspid.MVVM.StarterKit.DropdownOptionsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `options`.

 [DropdownOptionsMonoBinder](Aspid.MVVM.StarterKit.DropdownOptionsMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds `options` from labels, sprites
or option data.

 [DropdownOptionsSwitcherBinder](Aspid.MVVM.StarterKit.DropdownOptionsSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `options`.

 [DropdownOptionsSwitcherMonoBinder](Aspid.MVVM.StarterKit.DropdownOptionsSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `options`.

 [DropdownToSourceMonoBinder](Aspid.MVVM.StarterKit.DropdownToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for `TMP_Dropdown`.

 [DropdownValueBinder](Aspid.MVVM.StarterKit.DropdownValueBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds `value`.

 [DropdownValueEnumGroupMonoBinder](Aspid.MVVM.StarterKit.DropdownValueEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `value` on each element.

 [DropdownValueEnumMonoBinder](Aspid.MVVM.StarterKit.DropdownValueEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `value`.

 [DropdownValueMonoBinder](Aspid.MVVM.StarterKit.DropdownValueMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds `value`.

 [DropdownValueSwitcherBinder](Aspid.MVVM.StarterKit.DropdownValueSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `value`.

 [DropdownValueSwitcherMonoBinder](Aspid.MVVM.StarterKit.DropdownValueSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `value`.

 [DynamicProperty\<T\>](Aspid.MVVM.StarterKit.DynamicProperty-1.md)

A typed, observable property that can be added to a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

 [DynamicViewModel](Aspid.MVVM.StarterKit.DynamicViewModel.md)

An [`IViewModel`](Aspid.MVVM.IViewModel.md) whose typed properties are composed at runtime.

 [EasingConverter](Aspid.MVVM.StarterKit.EasingConverter.md)

Reshapes a 0..1 value along an easing curve.

 [EastSlavicPluralRule](Aspid.MVVM.StarterKit.EastSlavicPluralRule.md)

Three words picked by the last digit, with the teens excepted: Russian, Ukrainian, Belarusian.

 [ElementButtonCommandMonoBinder](Aspid.MVVM.StarterKit.ElementButtonCommandMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that executes a command when the [`Button`](https://docs.unity3d.com/ScriptReference/UIElements-Button.html) is
clicked.

 [ElementClassMonoBinder](Aspid.MVVM.StarterKit.ElementClassMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that adds or removes one USS class by the bound
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>.

 [ElementDisplayMonoBinder](Aspid.MVVM.StarterKit.ElementDisplayMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that shows or hides the element via
[`display`](https://docs.unity3d.com/ScriptReference/UIElements-IStyle-display.html).

 [ElementEnabledMonoBinder](Aspid.MVVM.StarterKit.ElementEnabledMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`SetEnabled`](https://docs.unity3d.com/ScriptReference/UIElements-VisualElement-SetEnabled.html).

 [ElementLabelTextMonoBinder](Aspid.MVVM.StarterKit.ElementLabelTextMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`text`](https://docs.unity3d.com/ScriptReference/UIElements-TextElement-text.html) of a
[`Label`](https://docs.unity3d.com/ScriptReference/UIElements-Label.html) from any value.

 [ElementListViewItemsSourceMonoBinder](Aspid.MVVM.StarterKit.ElementListViewItemsSourceMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds ListView.itemsSource to a read-only
collection.

 [ElementSliderValueMonoBinder](Aspid.MVVM.StarterKit.ElementSliderValueMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UIElements-BaseSlider-value.html) of a
[`Slider`](https://docs.unity3d.com/ScriptReference/UIElements-Slider.html) and reports user changes back.

 [ElementTextFieldValueMonoBinder](Aspid.MVVM.StarterKit.ElementTextFieldValueMonoBinder.md)

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds the text of a [`TextField`](https://docs.unity3d.com/ScriptReference/UIElements-TextField.html) and reports
user changes back.

 [EmptyViewModel](Aspid.MVVM.StarterKit.EmptyViewModel.md)

[`IViewModel`](Aspid.MVVM.IViewModel.md) with no bindable members: every lookup fails, so bound binders stay at their defaults.

 [EnglishPluralRule](Aspid.MVVM.StarterKit.EnglishPluralRule.md)

One word for one, another for everything else: English, German, Dutch, Spanish, Italian,
Swedish, Greek.

 [EnumConverterAsset](Aspid.MVVM.StarterKit.EnumConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for boxed [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) values.

 [EnumConverterAsset\<T\>](Aspid.MVVM.StarterKit.EnumConverterAsset-1.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) base for a concrete enum type. Unity cannot create
an asset of an open generic, so subclass with <code class="typeparamref">T</code> closed.

 [EnumFlagsToStringConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumFlagsToStringConverter-1.md)

Names the flags a value carries.

 [EnumGroupMonoBinder\<TElement, TValue\>](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md)

Abstract base [`EnumGroupMonoBinder<T>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md) whose selected and default states are two preset
<code class="typeparamref">TValue</code>s, each passed through an optional converter before [`EnumGroupMonoBinder<T1, T2>.SetValue`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md).

 [EnumGroupMonoBinder\<TElement\>](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that maps a bound [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) to a group of elements: the matching entry
receives [`EnumGroupMonoBinder<T>.SetSelectedValue`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md#Aspid_MVVM_StarterKit_EnumGroupMonoBinder_1_SetSelectedValue__0_), every other entry receives [`EnumGroupMonoBinder<T>.SetDefaultValue`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md#Aspid_MVVM_StarterKit_EnumGroupMonoBinder_1_SetDefaultValue__0_).

 [EnumMaskConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumMaskConverter-1.md)

Combines a bound flags value with an authored mask.

 [EnumMatchConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumMatchConverter-1.md)

Tests an enum value against an authored one.

 [EnumMonoBinder\<TComponent, TValue\>](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that resolves a bound [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) to a <code class="typeparamref">TValue</code>
through an `EnumValues<T>` table, passing the result through an optional converter first.

 [EnumMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.EnumMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that resolves a bound [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) to a <code class="typeparamref">TValue</code>
through an `EnumValues<T>` table, passing the result through an optional converter first.

 [EnumToDropdownOptionDataConverter](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverter.md)

Builds the option list of a dropdown out of an enum's members.

 [EnumToDropdownOptionDataConverterAsset](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for boxed [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) values turned into
dropdown options.

 [EnumToDropdownOptionDataConverterAsset\<T\>](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverterAsset-1.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) base for a concrete enum type turned into dropdown
options. Unity cannot create an asset of an open generic, so subclass with
<code class="typeparamref">T</code> closed.

 [EnumToNumberConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumToNumberConverter-1.md)

Converts an enum value to a number and back.

 [EnumToStringConverter\<TEnum\>](Aspid.MVVM.StarterKit.EnumToStringConverter-1.md)

Converts an enum value to text.

 [EnumToStringConverterAsset\<T\>](Aspid.MVVM.StarterKit.EnumToStringConverterAsset-1.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) base for a concrete enum type rendered as text.
Unity cannot create an asset of an open generic, so subclass with <code class="typeparamref">T</code>
closed.

 [EnumToStringConverterAsset](Aspid.MVVM.StarterKit.EnumToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for boxed [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum) values rendered as text.

 [EnumToValueConverter\<TEnum, T\>](Aspid.MVVM.StarterKit.EnumToValueConverter-2.md)

Maps an enum value to an authored value.

 [EqualityToBoolConverter\<T\>](Aspid.MVVM.StarterKit.EqualityToBoolConverter-1.md)

Tests a bound value against an authored one.

 [EulerToQuaternionConverter](Aspid.MVVM.StarterKit.EulerToQuaternionConverter.md)

Turns Euler angles into a rotation.

 [EventMonoView](Aspid.MVVM.StarterKit.EventMonoView.md)

[`MonoView`](Aspid.MVVM.MonoView.md) that raises [`UnityEvent`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html)s when it is initialized and deinitialized.

 [EventTriggerCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command when the selected [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event
fires with [`EventTriggerCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-2.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_2_Param1), [`EventTriggerCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-2.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_2_Param2).

 [EventTriggerCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command when the selected [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event
fires with [`EventTriggerCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_3_Param1), [`EventTriggerCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_3_Param2), [`EventTriggerCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_3_Param3).

 [EventTriggerCommandBinder\<T\>](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command when the selected [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event
fires with [`EventTriggerCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-1.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_Param).

 [EventTriggerCommandBinder](Aspid.MVVM.StarterKit.EventTriggerCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command when the selected [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event
fires.

 [EventTriggerCommandMonoBinder](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires.

 [EventTriggerCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires with [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param1), [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param2), [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param3).

 [EventTriggerCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires with [`EventTriggerCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_1_Param).

 [EventTriggerCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires with [`EventTriggerCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_2_Param1), [`EventTriggerCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_2_Param2).

 [EventTriggerToSourceMonoBinder](Aspid.MVVM.StarterKit.EventTriggerToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html).

 [FloatBinder](Aspid.MVVM.StarterKit.FloatBinder.md)

Abstract base [`StarterKit.Binder<T>?text=Binder%3cfloat%3e`](Aspid.MVVM.StarterKit.md) that binds a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> property,
accepting every numeric type via [`IFloatBinder`](Aspid.MVVM.StarterKit.IFloatBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [FloatConverterAsset](Aspid.MVVM.StarterKit.FloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) values.

 [FloatMonoBinder](Aspid.MVVM.StarterKit.FloatMonoBinder.md)

Abstract base [`StarterKit.MonoBinder<T>?text=MonoBinder%3cfloat%3e`](Aspid.MVVM.StarterKit.md) that binds a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> property,
accepting every numeric type via [`IFloatBinder`](Aspid.MVVM.StarterKit.IFloatBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [FloatToBoolConverterAsset](Aspid.MVVM.StarterKit.FloatToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [FloatToColorConverterAsset](Aspid.MVVM.StarterKit.FloatToColorConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) conversions.

 [FloatToQuaternionConverterAsset](Aspid.MVVM.StarterKit.FloatToQuaternionConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) conversions.

 [FloatToSpriteConverterAsset](Aspid.MVVM.StarterKit.FloatToSpriteConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) conversions.

 [FloatToStringConverterAsset](Aspid.MVVM.StarterKit.FloatToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [FloatToTimeSpanConverterAsset](Aspid.MVVM.StarterKit.FloatToTimeSpanConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) conversions.

 [FloatToVector2ConverterAsset](Aspid.MVVM.StarterKit.FloatToVector2ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) conversions.

 [FloatToVector3ConverterAsset](Aspid.MVVM.StarterKit.FloatToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [FloatToVectorConverter](Aspid.MVVM.StarterKit.FloatToVectorConverter.md)

Writes one number into the chosen axes of a vector.

 [FormatStringMonoBinder](Aspid.MVVM.StarterKit.FormatStringMonoBinder.md)

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that formats the input strings into one line.

 [FrenchPluralRule](Aspid.MVVM.StarterKit.FrenchPluralRule.md)

Zero and one share a word: French, Brazilian Portuguese, Hindi.

 [FuncConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.FuncConverter-2.md)

Wraps a function, or another converter's <code>Convert</code>, as an [`IConverter<T1, T2>`](Aspid.MVVM.StarterKit.IConverter-2.md).

 [FuncConverterExtensions](Aspid.MVVM.StarterKit.FuncConverterExtensions.md)

Turns a function into a converter.

 [GameObjectExtensions](Aspid.MVVM.StarterKit.GameObjectExtensions.md)

Extension methods for [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) used by the game object binders.

 [GameObjectInstantiateAddressableMonoBinder](Aspid.MVVM.StarterKit.GameObjectInstantiateAddressableMonoBinder.md)

[`AddressableMonoBinder<T>`](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md) that instantiates the loaded prefab into a container, replacing
the previous instance.

 [GameObjectLayerBinder](Aspid.MVVM.StarterKit.GameObjectLayerBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`layer`](https://docs.unity3d.com/ScriptReference/GameObject-layer.html).

 [GameObjectLayerMonoBinder](Aspid.MVVM.StarterKit.GameObjectLayerMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that binds [`layer`](https://docs.unity3d.com/ScriptReference/GameObject-layer.html) of the object it is attached to.

 [GameObjectNameMonoBinder](Aspid.MVVM.StarterKit.GameObjectNameMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that binds the name of the [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) it is attached to.

 [GameObjectTagBinder](Aspid.MVVM.StarterKit.GameObjectTagBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html).

 [GameObjectTagEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GameObjectTagEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html) of each element.

 [GameObjectTagEnumMonoBinder](Aspid.MVVM.StarterKit.GameObjectTagEnumMonoBinder.md)

[`EnumMonoBinder<T>`](Aspid.MVVM.StarterKit.EnumMonoBinder-1.md) that sets [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html) of the object it is attached to.

 [GameObjectTagMonoBinder](Aspid.MVVM.StarterKit.GameObjectTagMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that binds [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html) of the object it is attached to.

 [GameObjectTagSwitcherBinder](Aspid.MVVM.StarterKit.GameObjectTagSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html).

 [GameObjectTagSwitcherMonoBinder](Aspid.MVVM.StarterKit.GameObjectTagSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-1.md) that switches [`tag`](https://docs.unity3d.com/ScriptReference/GameObject-tag.html) of the object it is attached to.

 [GameObjectToSourceMonoBinder](Aspid.MVVM.StarterKit.GameObjectToSourceMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that hands the ViewModel the [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) it is attached to.

 [GameObjectVisibleBinder](Aspid.MVVM.StarterKit.GameObjectVisibleBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that shows or hides the object via
[`SetActive`](https://docs.unity3d.com/ScriptReference/GameObject-SetActive.html).

 [GameObjectVisibleByBindMonoBinder](Aspid.MVVM.StarterKit.GameObjectVisibleByBindMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that shows the object it is attached to while a binding exists and hides it otherwise.

 [GameObjectVisibleCanExecuteHandler](Aspid.MVVM.StarterKit.GameObjectVisibleCanExecuteHandler.md)

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that toggles a [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) active by the command state.

 [GameObjectVisibleEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GameObjectVisibleEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets the active state of each element.

 [GameObjectVisibleEnumMonoBinder](Aspid.MVVM.StarterKit.GameObjectVisibleEnumMonoBinder.md)

[`EnumMonoBinder<T>`](Aspid.MVVM.StarterKit.EnumMonoBinder-1.md) that sets the active state of the object it is attached to.

 [GameObjectVisibleMonoBinder](Aspid.MVVM.StarterKit.GameObjectVisibleMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that shows or hides the object it is attached to.

 [GenerateSerializableBinderAttribute](Aspid.MVVM.StarterKit.GenerateSerializableBinderAttribute.md)

Declares that the serializable half of this binder family is generated from the MonoBehaviour half it is
applied to.

 [GradientEvaluateConverter](Aspid.MVVM.StarterKit.GradientEvaluateConverter.md)

Reads a color off a [`Gradient`](https://docs.unity3d.com/ScriptReference/Gradient.html).

 [GraphicColorBinder](Aspid.MVVM.StarterKit.GraphicColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorChannelBinder](Aspid.MVVM.StarterKit.GraphicColorChannelBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorChannelEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GraphicColorChannelEnumGroupMonoBinder.md)

[`StarterKit.EnumGroupMonoBinder<T1, T2>?text=EnumGroupMonoBinder%3cGraphic%2c+float%3e`](Aspid.MVVM.StarterKit.md) that sets the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html) per group element.

 [GraphicColorChannelEnumMonoBinder](Aspid.MVVM.StarterKit.GraphicColorChannelEnumMonoBinder.md)

[`StarterKit.EnumMonoBinder<T1, T2>?text=EnumMonoBinder%3cGraphic%2c+float%3e`](Aspid.MVVM.StarterKit.md) that sets the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorChannelMonoBinder](Aspid.MVVM.StarterKit.GraphicColorChannelMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorChannelSwitcherBinder](Aspid.MVVM.StarterKit.GraphicColorChannelSwitcherBinder.md)

[`StarterKit.SwitcherBinder<T1, T2>?text=SwitcherBinder%3cGraphic%2c+float%3e`](Aspid.MVVM.StarterKit.md) that switches the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorChannelSwitcherMonoBinder](Aspid.MVVM.StarterKit.GraphicColorChannelSwitcherMonoBinder.md)

[`StarterKit.SwitcherMonoBinder<T1, T2>?text=SwitcherMonoBinder%3cGraphic%2c+float%3e`](Aspid.MVVM.StarterKit.md) that switches the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GraphicColorEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html) per group element.

 [GraphicColorEnumMonoBinder](Aspid.MVVM.StarterKit.GraphicColorEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorMonoBinder](Aspid.MVVM.StarterKit.GraphicColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorSwitcherBinder](Aspid.MVVM.StarterKit.GraphicColorSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicColorSwitcherMonoBinder](Aspid.MVVM.StarterKit.GraphicColorSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html).

 [GraphicExtensions](Aspid.MVVM.StarterKit.GraphicExtensions.md)

Per-channel access to [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html). An empty channel mask is reported as a configuration error.

 [GraphicMaskableBinder](Aspid.MVVM.StarterKit.GraphicMaskableBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`maskable`](https://docs.unity3d.com/ScriptReference/UI-MaskableGraphic-maskable.html).

 [GraphicMaskableEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GraphicMaskableEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`maskable`](https://docs.unity3d.com/ScriptReference/UI-MaskableGraphic-maskable.html) per group element.

 [GraphicMaskableEnumMonoBinder](Aspid.MVVM.StarterKit.GraphicMaskableEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`maskable`](https://docs.unity3d.com/ScriptReference/UI-MaskableGraphic-maskable.html).

 [GraphicMaskableMonoBinder](Aspid.MVVM.StarterKit.GraphicMaskableMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`maskable`](https://docs.unity3d.com/ScriptReference/UI-MaskableGraphic-maskable.html).

 [GraphicMaterialBinder](Aspid.MVVM.StarterKit.GraphicMaterialBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html).

 [GraphicMaterialEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GraphicMaterialEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html) per group element.

 [GraphicMaterialEnumMonoBinder](Aspid.MVVM.StarterKit.GraphicMaterialEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html).

 [GraphicMaterialMonoBinder](Aspid.MVVM.StarterKit.GraphicMaterialMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html).

 [GraphicMaterialSwitcherBinder](Aspid.MVVM.StarterKit.GraphicMaterialSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html).

 [GraphicMaterialSwitcherMonoBinder](Aspid.MVVM.StarterKit.GraphicMaterialSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`material`](https://docs.unity3d.com/ScriptReference/UI-Graphic-material.html).

 [GraphicRaycastTargetBinder](Aspid.MVVM.StarterKit.GraphicRaycastTargetBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`raycastTarget`](https://docs.unity3d.com/ScriptReference/UI-Graphic-raycastTarget.html).

 [GraphicRaycastTargetEnumGroupMonoBinder](Aspid.MVVM.StarterKit.GraphicRaycastTargetEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`raycastTarget`](https://docs.unity3d.com/ScriptReference/UI-Graphic-raycastTarget.html) per group element.

 [GraphicRaycastTargetEnumMonoBinder](Aspid.MVVM.StarterKit.GraphicRaycastTargetEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`raycastTarget`](https://docs.unity3d.com/ScriptReference/UI-Graphic-raycastTarget.html).

 [GraphicRaycastTargetMonoBinder](Aspid.MVVM.StarterKit.GraphicRaycastTargetMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`raycastTarget`](https://docs.unity3d.com/ScriptReference/UI-Graphic-raycastTarget.html).

 [GraphicToSourceMonoBinder](Aspid.MVVM.StarterKit.GraphicToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Graphic`](https://docs.unity3d.com/ScriptReference/UI-Graphic.html).

 [GridLayoutGroupCellSizeBinder](Aspid.MVVM.StarterKit.GridLayoutGroupCellSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`cellSize`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-cellSize.html).

 [GridLayoutGroupCellSizeMonoBinder](Aspid.MVVM.StarterKit.GridLayoutGroupCellSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`cellSize`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-cellSize.html).

 [GridLayoutGroupConstraintBinder](Aspid.MVVM.StarterKit.GridLayoutGroupConstraintBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`constraint`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-constraint.html).

 [GridLayoutGroupConstraintCountBinder](Aspid.MVVM.StarterKit.GridLayoutGroupConstraintCountBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`constraintCount`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-constraintCount.html).

 [GridLayoutGroupConstraintCountMonoBinder](Aspid.MVVM.StarterKit.GridLayoutGroupConstraintCountMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`constraintCount`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-constraintCount.html).

 [GridLayoutGroupConstraintMonoBinder](Aspid.MVVM.StarterKit.GridLayoutGroupConstraintMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`constraint`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-constraint.html).

 [GridLayoutGroupSpacingBinder](Aspid.MVVM.StarterKit.GridLayoutGroupSpacingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`spacing`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-spacing.html).

 [GridLayoutGroupSpacingMonoBinder](Aspid.MVVM.StarterKit.GridLayoutGroupSpacingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`spacing`](https://docs.unity3d.com/ScriptReference/UI-GridLayoutGroup-spacing.html).

 [HashToColorConverter](Aspid.MVVM.StarterKit.HashToColorConverter.md)

Derives a stable color from a string.

 [HdrIntensityConverter](Aspid.MVVM.StarterKit.HdrIntensityConverter.md)

Pushes a color above white by an exposure value.

 [HorizontalOrVerticalLayoutGroupSpacingBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html).

 [HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html) on each element.

 [HorizontalOrVerticalLayoutGroupSpacingEnumMonoBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html).

 [HorizontalOrVerticalLayoutGroupSpacingMonoBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html).

 [HorizontalOrVerticalLayoutGroupSpacingSwitcherBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html).

 [HorizontalOrVerticalLayoutGroupSpacingSwitcherMonoBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupSpacingSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches
[`spacing`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup-spacing.html).

 [HorizontalOrVerticalLayoutGroupToSourceMonoBinder](Aspid.MVVM.StarterKit.HorizontalOrVerticalLayoutGroupToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`HorizontalOrVerticalLayoutGroup`](https://docs.unity3d.com/ScriptReference/UI-HorizontalOrVerticalLayoutGroup.html).

 [ImageExtensions](Aspid.MVVM.StarterKit.ImageExtensions.md)

Extension methods for [`Image`](https://docs.unity3d.com/ScriptReference/UI-Image.html) used by the image binders.

 [ImageFillBinder](Aspid.MVVM.StarterKit.ImageFillBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html).

 [ImageFillClockwiseBinder](Aspid.MVVM.StarterKit.ImageFillClockwiseBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`fillClockwise`](https://docs.unity3d.com/ScriptReference/UI-Image-fillClockwise.html).

 [ImageFillClockwiseMonoBinder](Aspid.MVVM.StarterKit.ImageFillClockwiseMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`fillClockwise`](https://docs.unity3d.com/ScriptReference/UI-Image-fillClockwise.html).

 [ImageFillEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ImageFillEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html) on each element.

 [ImageFillEnumMonoBinder](Aspid.MVVM.StarterKit.ImageFillEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html).

 [ImageFillMonoBinder](Aspid.MVVM.StarterKit.ImageFillMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html).

 [ImageFillOriginBinder](Aspid.MVVM.StarterKit.ImageFillOriginBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`fillOrigin`](https://docs.unity3d.com/ScriptReference/UI-Image-fillOrigin.html).

 [ImageFillOriginMonoBinder](Aspid.MVVM.StarterKit.ImageFillOriginMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`fillOrigin`](https://docs.unity3d.com/ScriptReference/UI-Image-fillOrigin.html).

 [ImageFillSwitcherBinder](Aspid.MVVM.StarterKit.ImageFillSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html).

 [ImageFillSwitcherMonoBinder](Aspid.MVVM.StarterKit.ImageFillSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`fillAmount`](https://docs.unity3d.com/ScriptReference/UI-Image-fillAmount.html).

 [ImagePreserveAspectBinder](Aspid.MVVM.StarterKit.ImagePreserveAspectBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`preserveAspect`](https://docs.unity3d.com/ScriptReference/UI-Image-preserveAspect.html).

 [ImagePreserveAspectMonoBinder](Aspid.MVVM.StarterKit.ImagePreserveAspectMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`preserveAspect`](https://docs.unity3d.com/ScriptReference/UI-Image-preserveAspect.html).

 [ImageSpriteAddressableMonoBinder](Aspid.MVVM.StarterKit.ImageSpriteAddressableMonoBinder.md)

[`AddressableMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md) that loads a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) into
[`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html).

 [ImageSpriteBinder](Aspid.MVVM.StarterKit.ImageSpriteBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html), also from a
[`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html).

 [ImageSpriteEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ImageSpriteEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html) on each element.

 [ImageSpriteEnumMonoBinder](Aspid.MVVM.StarterKit.ImageSpriteEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html).

 [ImageSpriteMonoBinder](Aspid.MVVM.StarterKit.ImageSpriteMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html), also from a
[`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html).

 [ImageSpriteSwitcherBinder](Aspid.MVVM.StarterKit.ImageSpriteSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html).

 [ImageSpriteSwitcherMonoBinder](Aspid.MVVM.StarterKit.ImageSpriteSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html).

 [ImageToSourceMonoBinder](Aspid.MVVM.StarterKit.ImageToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Image`](https://docs.unity3d.com/ScriptReference/UI-Image.html).

 [ImageTypeBinder](Aspid.MVVM.StarterKit.ImageTypeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`type`](https://docs.unity3d.com/ScriptReference/UI-Image-type.html).

 [ImageTypeMonoBinder](Aspid.MVVM.StarterKit.ImageTypeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`type`](https://docs.unity3d.com/ScriptReference/UI-Image-type.html).

 [IndexToValueConverter\<T\>](Aspid.MVVM.StarterKit.IndexToValueConverter-1.md)

Picks a value out of an authored array by index.

 [InitializeComponent\<T\>](Aspid.MVVM.StarterKit.InitializeComponent-1.md)

Serializable slot that resolves a <code class="typeparamref">T</code> from a component, a plain reference,
a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) or the DI container, as chosen by [`ResolveType`](Aspid.MVVM.StarterKit.ResolveType.md).

 [InputFieldBinder](Aspid.MVVM.StarterKit.InputFieldBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds `text`, also from numbers, and reports
edits back as text and, for numeric fields, as numbers.

 [InputFieldCaretPositionBinder](Aspid.MVVM.StarterKit.InputFieldCaretPositionBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds `caretPosition`.

 [InputFieldCaretPositionMonoBinder](Aspid.MVVM.StarterKit.InputFieldCaretPositionMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds `caretPosition`.

 [InputFieldCharacterLimitBinder](Aspid.MVVM.StarterKit.InputFieldCharacterLimitBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds `characterLimit`.

 [InputFieldCharacterLimitMonoBinder](Aspid.MVVM.StarterKit.InputFieldCharacterLimitMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds `characterLimit`.

 [InputFieldCharacterValidationBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
`characterValidation`.

 [InputFieldCharacterValidationEnumGroupMonoBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `characterValidation`
on each element.

 [InputFieldCharacterValidationEnumMonoBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `characterValidation`.

 [InputFieldCharacterValidationMonoBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
`characterValidation`.

 [InputFieldCharacterValidationSwitcherBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `characterValidation`.

 [InputFieldCharacterValidationSwitcherMonoBinder](Aspid.MVVM.StarterKit.InputFieldCharacterValidationSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `characterValidation`.

 [InputFieldCommandBinder\<T\>](Aspid.MVVM.StarterKit.InputFieldCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-1.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_1_Param).

 [InputFieldCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.InputFieldCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_3_Param1), [`InputFieldCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_3_Param2),
[`InputFieldCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_3_Param3).

 [InputFieldCommandBinder](Aspid.MVVM.StarterKit.InputFieldCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on the selected field event with the text.

 [InputFieldCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.InputFieldCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-2.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_2_Param1), [`InputFieldCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.InputFieldCommandBinder-2.md#Aspid_MVVM_StarterKit_InputFieldCommandBinder_2_Param2).

 [InputFieldCommandMonoBinder](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text.

 [InputFieldCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_3_Param1), [`InputFieldCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_3_Param2),
[`InputFieldCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_3_Param3).

 [InputFieldCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_2_Param1), [`InputFieldCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_2_Param2).

 [InputFieldCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_Param).

 [InputFieldContentTypeBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `contentType`.

 [InputFieldContentTypeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `contentType`
on each element.

 [InputFieldContentTypeEnumMonoBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `contentType`.

 [InputFieldContentTypeMonoBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `contentType`.

 [InputFieldContentTypeSwitcherBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `contentType`.

 [InputFieldContentTypeSwitcherMonoBinder](Aspid.MVVM.StarterKit.InputFieldContentTypeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `contentType`.

 [InputFieldExtensions](Aspid.MVVM.StarterKit.InputFieldExtensions.md)

Event and number helpers shared by the `TMP_InputField` binders.

 [InputFieldInputTypeBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `inputType`.

 [InputFieldInputTypeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `inputType`
on each element.

 [InputFieldInputTypeEnumMonoBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `inputType`.

 [InputFieldInputTypeMonoBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `inputType`.

 [InputFieldInputTypeSwitcherBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `inputType`.

 [InputFieldInputTypeSwitcherMonoBinder](Aspid.MVVM.StarterKit.InputFieldInputTypeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `inputType`.

 [InputFieldLineTypeBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `lineType`.

 [InputFieldLineTypeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `lineType`
on each element.

 [InputFieldLineTypeEnumMonoBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `lineType`.

 [InputFieldLineTypeMonoBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `lineType`.

 [InputFieldLineTypeSwitcherBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `lineType`.

 [InputFieldLineTypeSwitcherMonoBinder](Aspid.MVVM.StarterKit.InputFieldLineTypeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `lineType`.

 [InputFieldMonoBinder](Aspid.MVVM.StarterKit.InputFieldMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds `text`, also from numbers,
and reports edits back as text and, for numeric fields, as numbers.

 [InputFieldPlaceholderBinder](Aspid.MVVM.StarterKit.InputFieldPlaceholderBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds
`placeholder`.

 [InputFieldPlaceholderMonoBinder](Aspid.MVVM.StarterKit.InputFieldPlaceholderMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds
`placeholder`.

 [InputFieldReadOnlyBinder](Aspid.MVVM.StarterKit.InputFieldReadOnlyBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `readOnly`.

 [InputFieldReadOnlyMonoBinder](Aspid.MVVM.StarterKit.InputFieldReadOnlyMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `readOnly`.

 [InputFieldToSourceMonoBinder](Aspid.MVVM.StarterKit.InputFieldToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for `TMP_InputField`.

 [IntBinder](Aspid.MVVM.StarterKit.IntBinder.md)

Abstract base [`StarterKit.Binder<T>?text=Binder%3cint%3e`](Aspid.MVVM.StarterKit.md) that binds an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> property,
accepting every numeric type via [`IIntBinder`](Aspid.MVVM.StarterKit.IIntBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [IntConverterAsset](Aspid.MVVM.StarterKit.IntConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) values.

 [IntMonoBinder](Aspid.MVVM.StarterKit.IntMonoBinder.md)

Abstract base [`StarterKit.MonoBinder<T>?text=MonoBinder%3cint%3e`](Aspid.MVVM.StarterKit.md) that binds an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> property,
accepting every numeric type via [`IIntBinder`](Aspid.MVVM.StarterKit.IIntBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [IntToBoolConverterAsset](Aspid.MVVM.StarterKit.IntToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [IntToRectOffsetConverter](Aspid.MVVM.StarterKit.IntToRectOffsetConverter.md)

Writes one number into the chosen sides of a padding.

 [IntToRectOffsetConverterAsset](Aspid.MVVM.StarterKit.IntToRectOffsetConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) to [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) conversions.

 [IntToStringConverterAsset](Aspid.MVVM.StarterKit.IntToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [InverseCollectionOrder\<T\>](Aspid.MVVM.StarterKit.InverseCollectionOrder-1.md)

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that runs the nested order in the opposite direction.
An empty slot keeps the source order.

 [InverseConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.InverseConverter-2.md)

Runs a two-way converter in the opposite direction.

 [InverseLerpConverter](Aspid.MVVM.StarterKit.InverseLerpConverter.md)

Converts a value in a range to its 0..1 position within it.

 [LayoutElementFlexibleHeightBinder](Aspid.MVVM.StarterKit.LayoutElementFlexibleHeightBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`flexibleHeight`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-flexibleHeight.html).

 [LayoutElementFlexibleHeightMonoBinder](Aspid.MVVM.StarterKit.LayoutElementFlexibleHeightMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`flexibleHeight`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-flexibleHeight.html).

 [LayoutElementFlexibleWidthBinder](Aspid.MVVM.StarterKit.LayoutElementFlexibleWidthBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`flexibleWidth`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-flexibleWidth.html).

 [LayoutElementFlexibleWidthMonoBinder](Aspid.MVVM.StarterKit.LayoutElementFlexibleWidthMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`flexibleWidth`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-flexibleWidth.html).

 [LayoutElementIgnoreLayoutBinder](Aspid.MVVM.StarterKit.LayoutElementIgnoreLayoutBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`ignoreLayout`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-ignoreLayout.html).

 [LayoutElementIgnoreLayoutMonoBinder](Aspid.MVVM.StarterKit.LayoutElementIgnoreLayoutMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`ignoreLayout`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-ignoreLayout.html).

 [LayoutElementPreferredHeightBinder](Aspid.MVVM.StarterKit.LayoutElementPreferredHeightBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`preferredHeight`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-preferredHeight.html).

 [LayoutElementPreferredHeightMonoBinder](Aspid.MVVM.StarterKit.LayoutElementPreferredHeightMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`preferredHeight`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-preferredHeight.html).

 [LayoutElementPreferredWidthBinder](Aspid.MVVM.StarterKit.LayoutElementPreferredWidthBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`preferredWidth`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-preferredWidth.html).

 [LayoutElementPreferredWidthMonoBinder](Aspid.MVVM.StarterKit.LayoutElementPreferredWidthMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`preferredWidth`](https://docs.unity3d.com/ScriptReference/UI-LayoutElement-preferredWidth.html).

 [LayoutGroupExtensions](Aspid.MVVM.StarterKit.LayoutGroupExtensions.md)

Extension methods for [`LayoutGroup`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup.html) used by the layout group binders.

 [LayoutGroupPaddingBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html), also
from a number applied to every selected side.

 [LayoutGroupPaddingEnumGroupMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html) on each element.

 [LayoutGroupPaddingEnumMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html).

 [LayoutGroupPaddingMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html), also
from a number applied to every selected side.

 [LayoutGroupPaddingSwitcherBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html).

 [LayoutGroupPaddingSwitcherMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupPaddingSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html).

 [LayoutGroupToSourceMonoBinder](Aspid.MVVM.StarterKit.LayoutGroupToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`LayoutGroup`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup.html).

 [LerpNumberConverter](Aspid.MVVM.StarterKit.LerpNumberConverter.md)

Converts a 0..1 position to a value in a range.

 [LightColorBinder](Aspid.MVVM.StarterKit.LightColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/Light-color.html).

 [LightColorMonoBinder](Aspid.MVVM.StarterKit.LightColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/Light-color.html).

 [LightIntensityBinder](Aspid.MVVM.StarterKit.LightIntensityBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`intensity`](https://docs.unity3d.com/ScriptReference/Light-intensity.html).

 [LightIntensityMonoBinder](Aspid.MVVM.StarterKit.LightIntensityMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`intensity`](https://docs.unity3d.com/ScriptReference/Light-intensity.html).

 [LightRangeBinder](Aspid.MVVM.StarterKit.LightRangeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`range`](https://docs.unity3d.com/ScriptReference/Light-range.html).

 [LightRangeMonoBinder](Aspid.MVVM.StarterKit.LightRangeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`range`](https://docs.unity3d.com/ScriptReference/Light-range.html).

 [LightSpotAngleBinder](Aspid.MVVM.StarterKit.LightSpotAngleBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`spotAngle`](https://docs.unity3d.com/ScriptReference/Light-spotAngle.html).

 [LightSpotAngleMonoBinder](Aspid.MVVM.StarterKit.LightSpotAngleMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`spotAngle`](https://docs.unity3d.com/ScriptReference/Light-spotAngle.html).

 [LineRendererColorBinder](Aspid.MVVM.StarterKit.LineRendererColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds the start and/or end color of a
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererColorEnumGroupMonoBinder](Aspid.MVVM.StarterKit.LineRendererColorEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets the start and/or end color of each
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererColorEnumMonoBinder](Aspid.MVVM.StarterKit.LineRendererColorEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets the start and/or end color of a
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererColorMonoBinder](Aspid.MVVM.StarterKit.LineRendererColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds the start and/or end color of a
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererColorSwitcherBinder](Aspid.MVVM.StarterKit.LineRendererColorSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches the start and/or end color of a
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererColorSwitcherMonoBinder](Aspid.MVVM.StarterKit.LineRendererColorSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches the start and/or end color of a
[`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererExtensions](Aspid.MVVM.StarterKit.LineRendererExtensions.md)

Extension methods for [`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html) used by the line renderer binders.

 [LineRendererLoopBinder](Aspid.MVVM.StarterKit.LineRendererLoopBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`loop`](https://docs.unity3d.com/ScriptReference/LineRenderer-loop.html).

 [LineRendererLoopMonoBinder](Aspid.MVVM.StarterKit.LineRendererLoopMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`loop`](https://docs.unity3d.com/ScriptReference/LineRenderer-loop.html).

 [LineRendererToSourceMonoBinder](Aspid.MVVM.StarterKit.LineRendererToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html).

 [LineRendererWidthMultiplierBinder](Aspid.MVVM.StarterKit.LineRendererWidthMultiplierBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`widthMultiplier`](https://docs.unity3d.com/ScriptReference/LineRenderer-widthMultiplier.html).

 [LineRendererWidthMultiplierMonoBinder](Aspid.MVVM.StarterKit.LineRendererWidthMultiplierMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`widthMultiplier`](https://docs.unity3d.com/ScriptReference/LineRenderer-widthMultiplier.html).

 [ListSourceExtensions](Aspid.MVVM.StarterKit.ListSourceExtensions.md)

Wraps a read-only collection as the [`IList`](https://learn.microsoft.com/dotnet/api/system.collections.ilist) a [`ListView`](https://docs.unity3d.com/ScriptReference/UIElements-ListView.html) takes.

 [LocaleConverterAsset](Aspid.MVVM.StarterKit.LocaleConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Locale`](https://docs.unity3d.com/ScriptReference/Localization-Locale.html) values.

 [LocaleToStringConverter](Aspid.MVVM.StarterKit.LocaleToStringConverter.md)

Writes the name of a locale.

 [LocaleToStringConverterAsset](Aspid.MVVM.StarterKit.LocaleToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Locale`](https://docs.unity3d.com/ScriptReference/Localization-Locale.html) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [LocalizeStringEventEntryBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntryBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name.

 [LocalizeStringEventEntryEnumGroupMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntryEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name on each element.

 [LocalizeStringEventEntryEnumMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntryEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name.

 [LocalizeStringEventEntryMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntryMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name.

 [LocalizeStringEventEntrySwitcherBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntrySwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name.

 [LocalizeStringEventEntrySwitcherMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventEntrySwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches
the LocalizedString.TableEntryReference of [`StringReference`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent-StringReference.html)
by key name.

 [LocalizeStringEventToSourceMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`LocalizeStringEvent`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent.html).

 [LocalizeStringEventVariableBinder](Aspid.MVVM.StarterKit.LocalizeStringEventVariableBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that writes the bound value into a named Smart String variable of
a [`LocalizeStringEvent`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent.html) and refreshes the string.

 [LocalizeStringEventVariableMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventVariableMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that writes the bound value into a named Smart String variable of
a [`LocalizeStringEvent`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent.html) and refreshes the string.

 [LocalizedEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.LocalizedEnumConverter-1.md)

Looks an enum member's name up in a localization table.

 [LocalizedNumberConverter](Aspid.MVVM.StarterKit.LocalizedNumberConverter.md)

Formats a number with the culture of the selected locale.

 [LocalizedStringConverter](Aspid.MVVM.StarterKit.LocalizedStringConverter.md)

Looks a key up in a localization table.

 [LogMessageText](Aspid.MVVM.StarterKit.LogMessageText.md)

Writes types and values the way they read inside a logged message; shared by binders and converters.

 [LongConverterAsset](Aspid.MVVM.StarterKit.LongConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) values.

 [LongToBoolConverterAsset](Aspid.MVVM.StarterKit.LongToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [LongToDateTimeConverterAsset](Aspid.MVVM.StarterKit.LongToDateTimeConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) to [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) conversions.

 [LongToStringConverterAsset](Aspid.MVVM.StarterKit.LongToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [LookRotationConverter](Aspid.MVVM.StarterKit.LookRotationConverter.md)

Builds a rotation that looks along a direction.

 [MaskShowMaskGraphicBinder](Aspid.MVVM.StarterKit.MaskShowMaskGraphicBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`showMaskGraphic`](https://docs.unity3d.com/ScriptReference/UI-Mask-showMaskGraphic.html).

 [MaskShowMaskGraphicMonoBinder](Aspid.MVVM.StarterKit.MaskShowMaskGraphicMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`showMaskGraphic`](https://docs.unity3d.com/ScriptReference/UI-Mask-showMaskGraphic.html).

 [MaskStringConverter](Aspid.MVVM.StarterKit.MaskStringConverter.md)

Hides the middle of a string, keeping a few characters at each end.

 [MaterialConverterAsset](Aspid.MVVM.StarterKit.MaterialConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Material`](https://docs.unity3d.com/ScriptReference/Material.html) values.

 [MaterialInstanceConverter](Aspid.MVVM.StarterKit.MaterialInstanceConverter.md)

Hands a renderer its own copy of a material instead of the shared asset.

 [MeshColliderConvexBinder](Aspid.MVVM.StarterKit.MeshColliderConvexBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`convex`](https://docs.unity3d.com/ScriptReference/MeshCollider-convex.html).

 [MeshColliderConvexEnumGroupMonoBinder](Aspid.MVVM.StarterKit.MeshColliderConvexEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`convex`](https://docs.unity3d.com/ScriptReference/MeshCollider-convex.html) on each element.

 [MeshColliderConvexEnumMonoBinder](Aspid.MVVM.StarterKit.MeshColliderConvexEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`convex`](https://docs.unity3d.com/ScriptReference/MeshCollider-convex.html).

 [MeshColliderConvexMonoBinder](Aspid.MVVM.StarterKit.MeshColliderConvexMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`convex`](https://docs.unity3d.com/ScriptReference/MeshCollider-convex.html).

 [MeshColliderCookingOptionsBinder](Aspid.MVVM.StarterKit.MeshColliderCookingOptionsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`cookingOptions`](https://docs.unity3d.com/ScriptReference/MeshCollider-cookingOptions.html).

 [MeshColliderCookingOptionsMonoBinder](Aspid.MVVM.StarterKit.MeshColliderCookingOptionsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`cookingOptions`](https://docs.unity3d.com/ScriptReference/MeshCollider-cookingOptions.html).

 [MeshColliderMeshBinder](Aspid.MVVM.StarterKit.MeshColliderMeshBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

 [MeshColliderMeshEnumGroupMonoBinder](Aspid.MVVM.StarterKit.MeshColliderMeshEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html) on each element.

 [MeshColliderMeshEnumMonoBinder](Aspid.MVVM.StarterKit.MeshColliderMeshEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

 [MeshColliderMeshMonoBinder](Aspid.MVVM.StarterKit.MeshColliderMeshMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

 [MeshColliderMeshSwitcherBinder](Aspid.MVVM.StarterKit.MeshColliderMeshSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

 [MeshColliderMeshSwitcherMonoBinder](Aspid.MVVM.StarterKit.MeshColliderMeshSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

 [MeshColliderToSourceMonoBinder](Aspid.MVVM.StarterKit.MeshColliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`MeshCollider`](https://docs.unity3d.com/ScriptReference/MeshCollider.html).

 [ModuloNumberConverter](Aspid.MVVM.StarterKit.ModuloNumberConverter.md)

Returns the remainder of a number divided by an authored divisor.

 [MonoBinder\<TProperty\>](Aspid.MVVM.StarterKit.MonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that binds a single property through its accessors, applying an optional
converter in both directions. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md), the current property value is sent to the ViewModel on binding.

 [NavMeshAgentIsStoppedBinder](Aspid.MVVM.StarterKit.NavMeshAgentIsStoppedBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`isStopped`](https://docs.unity3d.com/ScriptReference/AI-NavMeshAgent-isStopped.html).

 [NavMeshAgentIsStoppedMonoBinder](Aspid.MVVM.StarterKit.NavMeshAgentIsStoppedMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`isStopped`](https://docs.unity3d.com/ScriptReference/AI-NavMeshAgent-isStopped.html).

 [NavMeshAgentSpeedBinder](Aspid.MVVM.StarterKit.NavMeshAgentSpeedBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`speed`](https://docs.unity3d.com/ScriptReference/AI-NavMeshAgent-speed.html).

 [NavMeshAgentSpeedMonoBinder](Aspid.MVVM.StarterKit.NavMeshAgentSpeedMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`speed`](https://docs.unity3d.com/ScriptReference/AI-NavMeshAgent-speed.html).

 [NormalizedPercentConverter](Aspid.MVVM.StarterKit.NormalizedPercentConverter.md)

Converts a 0..1 fraction to a percentage, or the other way round.

 [NormalizedToSpriteConverter](Aspid.MVVM.StarterKit.NormalizedToSpriteConverter.md)

Picks one of a list of sprites by a 0..1 amount.

 [NotCollectionFilter\<T\>](Aspid.MVVM.StarterKit.NotCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element only when the nested filter rejects it.
An empty slot passes everything.

 [NullCoalesceConverter\<T\>](Aspid.MVVM.StarterKit.NullCoalesceConverter-1.md)

Substitutes an authored value for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> one.

 [NullGuardConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.NullGuardConverter-2.md)

Substitutes a fixed result for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> input instead of passing it on.

 [NumberCompareConverter](Aspid.MVVM.StarterKit.NumberCompareConverter.md)

Converts numeric values to boolean based on comparison operations.

 [NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md)

Abstract base for a converter that transforms a number and accepts every numeric type.

 [NumberFormatConverter](Aspid.MVVM.StarterKit.NumberFormatConverter.md)

Formats a number with a standard .NET format string.

 [NumberToEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.NumberToEnumConverter-1.md)

Converts a number to the enum value it stands for.

 [NumericCastConverter](Aspid.MVVM.StarterKit.NumericCastConverter.md)

Converts a number to another numeric type under a chosen overflow policy.

 [ObjectBinder\<TObject\>](Aspid.MVVM.StarterKit.ObjectBinder-1.md)

Abstract base [`StarterKit.Binder<T>?text=Binder%3cTObject%3e`](Aspid.MVVM.StarterKit.md) that binds a [`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html)
reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

 [ObjectCollectionCountMonoBinder](Aspid.MVVM.StarterKit.ObjectCollectionCountMonoBinder.md)

[`StarterKit.CollectionCountMonoBinder<T>?text=CollectionCountMonoBinder%3cobject%3e`](Aspid.MVVM.StarterKit.md) for a collection of any reference type.

 [ObjectMonoBinder\<TObject\>](Aspid.MVVM.StarterKit.ObjectMonoBinder-1.md)

Abstract base [`StarterKit.MonoBinder<T>?text=MonoBinder%3cTObject%3e`](Aspid.MVVM.StarterKit.md) that binds a [`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html)
reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

 [ObjectNameBinder](Aspid.MVVM.StarterKit.ObjectNameBinder.md)

[`StarterKit.TargetBinder<T1, T2>?text=TargetBinder%3cObject%2c+string%3e`](Aspid.MVVM.StarterKit.md) that binds [`name`](https://docs.unity3d.com/ScriptReference/Object-name.html) of the target.

 [ObjectNameConverter](Aspid.MVVM.StarterKit.ObjectNameConverter.md)

Reads the name of a Unity object.

 [ObjectNameMonoBinder](Aspid.MVVM.StarterKit.ObjectNameMonoBinder.md)

[`StarterKit.MonoBinder<T>?text=MonoBinder%3cstring%3e`](Aspid.MVVM.StarterKit.md) that binds [`name`](https://docs.unity3d.com/ScriptReference/Object-name.html) of the target object.

 [ObjectToBoolConverterAsset](Aspid.MVVM.StarterKit.ObjectToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Object`](https://learn.microsoft.com/dotnet/api/system.object) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [ObjectToStringConverterAsset](Aspid.MVVM.StarterKit.ObjectToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Object`](https://learn.microsoft.com/dotnet/api/system.object) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [ObservableCollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.ObservableCollectionMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that follows any `IObservableCollection<T>` (a set, a queue, a stack)
and reflects its changes onto a View. The hooks carry items only: these collections have no stable index.

 [ObservableCollectionViewModelMonoBinder\<TView\>](Aspid.MVVM.StarterKit.ObservableCollectionViewModelMonoBinder-1.md)

[`ObservableCollectionMonoBinder<T>`](Aspid.MVVM.StarterKit.ObservableCollectionMonoBinder-1.md) that creates a view per ViewModel and releases it when the ViewModel leaves.
Views are keyed by ViewModel, so a duplicate member is shown once.

 [ObservableCollectionViewModelMonoBinder](Aspid.MVVM.StarterKit.ObservableCollectionViewModelMonoBinder.md)

[`ObservableCollectionViewModelMonoBinder<T>`](Aspid.MVVM.StarterKit.ObservableCollectionViewModelMonoBinder-1.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [ObservableDictionaryBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryBinder-2.md)

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that follows an `IReadOnlyObservableDictionary<T1, T2>`
and reflects its changes onto a View.

 [ObservableDictionaryMonoBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that follows an `IReadOnlyObservableDictionary<T1, T2>`
and reflects its changes onto a View.

 [ObservableDictionaryViewModelBinder\<TKey, TViewModel, TView\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-3.md)

[`ObservableDictionaryBinder<T1, T2>`](Aspid.MVVM.StarterKit.ObservableDictionaryBinder-2.md) that creates a view per entry through a keyed factory
and releases it when the entry leaves. A replacement releases the old view and creates a new one.

 [ObservableDictionaryViewModelBinder\<TKey, TViewModel\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-2.md)

[`ObservableDictionaryViewModelBinder<T1, T2, T3>`](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-3.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [ObservableDictionaryViewModelMonoBinder](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelMonoBinder.md)

[`ObservableDictionaryViewModelMonoBinder<T1, T2, T3>`](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelMonoBinder-3.md) over [`MonoView`](Aspid.MVVM.MonoView.md), keyed by <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a>.

 [ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelMonoBinder-3.md)

[`ObservableDictionaryMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md) that creates a view per entry through a keyed factory
and releases it when the entry leaves. A replacement releases the old view and creates a new one.

 [ObservableListBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListBinder-1.md)

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that follows a plain, observable or filtered list and reflects its
add, remove, replace, move and reset changes onto a View.

 [ObservableListMonoBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that follows a plain, observable or filtered list and reflects its
add, remove, replace, move and reset changes onto a View.

 [ObservableListViewModelBinder\<TView\>](Aspid.MVVM.StarterKit.ObservableListViewModelBinder-1.md)

[`ObservableListBinder<T>`](Aspid.MVVM.StarterKit.ObservableListBinder-1.md) that creates a view per ViewModel in list order, with an optional filter and sort order.

 [ObservableListViewModelBinder](Aspid.MVVM.StarterKit.ObservableListViewModelBinder.md)

[`ObservableListViewModelBinder<T>`](Aspid.MVVM.StarterKit.ObservableListViewModelBinder-1.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [ObservableListViewModelMonoBinder](Aspid.MVVM.StarterKit.ObservableListViewModelMonoBinder.md)

[`ObservableListViewModelMonoBinder<T>`](Aspid.MVVM.StarterKit.ObservableListViewModelMonoBinder-1.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

 [ObservableListViewModelMonoBinder\<TView\>](Aspid.MVVM.StarterKit.ObservableListViewModelMonoBinder-1.md)

[`ObservableListMonoBinder<T>`](Aspid.MVVM.StarterKit.ObservableListMonoBinder-1.md) that creates a view per ViewModel in list order, with an optional filter and sort order.

 [OffsetThenScaleConverter](Aspid.MVVM.StarterKit.OffsetThenScaleConverter.md)

Adds a constant to a number and scales the sum.

 [OrBoolMonoBinder](Aspid.MVVM.StarterKit.OrBoolMonoBinder.md)

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that forwards <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when any input is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

 [OrCollectionFilter\<T\>](Aspid.MVVM.StarterKit.OrCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element when at least one nested filter passes it.
Empty slots are skipped; with no filter at all, everything passes.

 [OrdinalConverter](Aspid.MVVM.StarterKit.OrdinalConverter.md)

Formats a number as an English ordinal: 1 becomes "1st".

 [PadStringConverter](Aspid.MVVM.StarterKit.PadStringConverter.md)

Pads a string to a fixed width.

 [PaddedNumberConverter](Aspid.MVVM.StarterKit.PaddedNumberConverter.md)

Pads a number to a fixed width: 7 becomes "007".

 [ParseHtmlStringConverter](Aspid.MVVM.StarterKit.ParseHtmlStringConverter.md)

Converts HTML color strings (e.g., "#FF0000") to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) values.

 [ParticleSystemClearMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemClearMonoBinder.md)

[`ParticleSystemPlaybackMonoBinder`](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md) that calls [`Clear`](https://docs.unity3d.com/ScriptReference/ParticleSystem-Clear.html).

 [ParticleSystemEmissionEnabledBinder](Aspid.MVVM.StarterKit.ParticleSystemEmissionEnabledBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`enabled`](https://docs.unity3d.com/ScriptReference/ParticleSystem-EmissionModule-enabled.html).

 [ParticleSystemEmissionEnabledMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemEmissionEnabledMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`enabled`](https://docs.unity3d.com/ScriptReference/ParticleSystem-EmissionModule-enabled.html).

 [ParticleSystemEmissionRateBinder](Aspid.MVVM.StarterKit.ParticleSystemEmissionRateBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds
[`rateOverTimeMultiplier`](https://docs.unity3d.com/ScriptReference/ParticleSystem-EmissionModule-rateOverTimeMultiplier.html).

 [ParticleSystemEmissionRateMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemEmissionRateMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds
[`rateOverTimeMultiplier`](https://docs.unity3d.com/ScriptReference/ParticleSystem-EmissionModule-rateOverTimeMultiplier.html).

 [ParticleSystemPauseMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPauseMonoBinder.md)

[`ParticleSystemPlaybackMonoBinder`](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md) that calls [`Pause`](https://docs.unity3d.com/ScriptReference/ParticleSystem-Pause.html).

 [ParticleSystemPlayMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPlayMonoBinder.md)

[`ParticleSystemPlaybackMonoBinder`](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md) that calls [`Play`](https://docs.unity3d.com/ScriptReference/ParticleSystem-Play.html).

 [ParticleSystemPlaybackMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md)

Abstract [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that hands the ViewModel one playback operation on a
[`ParticleSystem`](https://docs.unity3d.com/ScriptReference/ParticleSystem.html) as an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) or an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md).

 [ParticleSystemStartColorBinder](Aspid.MVVM.StarterKit.ParticleSystemStartColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`startColor`](https://docs.unity3d.com/ScriptReference/ParticleSystem-MainModule-startColor.html).

 [ParticleSystemStartColorMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemStartColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`startColor`](https://docs.unity3d.com/ScriptReference/ParticleSystem-MainModule-startColor.html).

 [ParticleSystemStopMonoBinder](Aspid.MVVM.StarterKit.ParticleSystemStopMonoBinder.md)

[`ParticleSystemPlaybackMonoBinder`](Aspid.MVVM.StarterKit.ParticleSystemPlaybackMonoBinder.md) that calls [`Stop`](https://docs.unity3d.com/ScriptReference/ParticleSystem-Stop.html).

 [PassthroughConverter\<T\>](Aspid.MVVM.StarterKit.PassthroughConverter-1.md)

Returns its input unchanged.

 [PluralRule](Aspid.MVVM.StarterKit.PluralRule.md)

Words a count in one language: the grammar and the words it picks between.

 [PluralizeConverter](Aspid.MVVM.StarterKit.PluralizeConverter.md)

Picks the right word form for a count.

 [PolishPluralRule](Aspid.MVVM.StarterKit.PolishPluralRule.md)

Three words where only a bare one is singular: Polish.

 [PowerNumberConverter](Aspid.MVVM.StarterKit.PowerNumberConverter.md)

Raises a number to an authored exponent.

 [PredicateCollectionFilter\<T\>](Aspid.MVVM.StarterKit.PredicateCollectionFilter-1.md)

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that wraps a [`Predicate<T>`](https://learn.microsoft.com/dotnet/api/system.predicate-1) for code-built filters.

 [PrefabViewFactory\<T\>](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md)

[`IViewFactory<T>`](Aspid.MVVM.StarterKit.IViewFactory-1.md) that instantiates a prefab per view and destroys it on release.

 [PrefabViewPool\<T\>](Aspid.MVVM.StarterKit.PrefabViewPool-1.md)

[`PrefabViewFactory<T>`](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md) that keeps released views in an [`ObjectPool<T>`](https://docs.unity3d.com/ScriptReference/Pool-ObjectPool.html) and reuses them.

 [QualityLevelBinder](Aspid.MVVM.StarterKit.QualityLevelBinder.md)

[`IntBinder`](Aspid.MVVM.StarterKit.IntBinder.md) that binds [`GetQualityLevel`](https://docs.unity3d.com/ScriptReference/QualitySettings-GetQualityLevel.html).

 [QualityLevelMonoBinder](Aspid.MVVM.StarterKit.QualityLevelMonoBinder.md)

[`IntMonoBinder`](Aspid.MVVM.StarterKit.IntMonoBinder.md) that binds [`GetQualityLevel`](https://docs.unity3d.com/ScriptReference/QualitySettings-GetQualityLevel.html).

 [QuaternionConverterAsset](Aspid.MVVM.StarterKit.QuaternionConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) values.

 [QuaternionOffsetConverter](Aspid.MVVM.StarterKit.QuaternionOffsetConverter.md)

Applies a fixed rotation on top of a bound one.

 [QuaternionSlerpConverter](Aspid.MVVM.StarterKit.QuaternionSlerpConverter.md)

Turns between two rotations by a 0..1 amount.

 [QuaternionToAngleConverter](Aspid.MVVM.StarterKit.QuaternionToAngleConverter.md)

Reads the angle a rotation carries around one axis.

 [QuaternionToEulerConverter](Aspid.MVVM.StarterKit.QuaternionToEulerConverter.md)

Reads Euler angles off a rotation.

 [QuaternionToFloatConverterAsset](Aspid.MVVM.StarterKit.QuaternionToFloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

 [QuaternionToVector3ConverterAsset](Aspid.MVVM.StarterKit.QuaternionToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [QuaternionToVector4ConverterAsset](Aspid.MVVM.StarterKit.QuaternionToVector4ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

 [QuaternionVector4Converter](Aspid.MVVM.StarterKit.QuaternionVector4Converter.md)

Reads a rotation as its four raw numbers, and builds one back out of them.

 [RateLimitedMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.RateLimitedMonoBinder-1.md)

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that decides when a received value is forwarded to a
[`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html).

 [RatioToStringConverter](Aspid.MVVM.StarterKit.RatioToStringConverter.md)

Formats a number against a maximum: "35 / 100".

 [RawImageExtensions](Aspid.MVVM.StarterKit.RawImageExtensions.md)

Extension methods for [`RawImage`](https://docs.unity3d.com/ScriptReference/UI-RawImage.html) used by the raw image binders.

 [RawImageTextureAddressableMonoBinder](Aspid.MVVM.StarterKit.RawImageTextureAddressableMonoBinder.md)

[`AddressableMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md) that loads a [`Texture`](https://docs.unity3d.com/ScriptReference/Texture.html) into
[`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html).

 [RawImageTextureBinder](Aspid.MVVM.StarterKit.RawImageTextureBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html), also from a
[`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

 [RawImageTextureEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RawImageTextureEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html) on each element.

 [RawImageTextureEnumMonoBinder](Aspid.MVVM.StarterKit.RawImageTextureEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html).

 [RawImageTextureMonoBinder](Aspid.MVVM.StarterKit.RawImageTextureMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html), also from a
[`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

 [RawImageTextureSwitcherBinder](Aspid.MVVM.StarterKit.RawImageTextureSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html).

 [RawImageTextureSwitcherMonoBinder](Aspid.MVVM.StarterKit.RawImageTextureSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html).

 [RawImageToSourceMonoBinder](Aspid.MVVM.StarterKit.RawImageToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`RawImage`](https://docs.unity3d.com/ScriptReference/UI-RawImage.html).

 [RawImageUvRectBinder](Aspid.MVVM.StarterKit.RawImageUvRectBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`uvRect`](https://docs.unity3d.com/ScriptReference/UI-RawImage-uvRect.html).

 [RawImageUvRectMonoBinder](Aspid.MVVM.StarterKit.RawImageUvRectMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`uvRect`](https://docs.unity3d.com/ScriptReference/UI-RawImage-uvRect.html).

 [RectMask2DPaddingBinder](Aspid.MVVM.StarterKit.RectMask2DPaddingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`padding`](https://docs.unity3d.com/ScriptReference/UI-RectMask2D-padding.html) as
<code>(left, bottom, right)</code>; the top padding keeps its value.

 [RectMask2DPaddingMonoBinder](Aspid.MVVM.StarterKit.RectMask2DPaddingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`padding`](https://docs.unity3d.com/ScriptReference/UI-RectMask2D-padding.html) as
<code>(left, bottom, right)</code>; the top padding keeps its value.

 [RectOffsetConverterAsset](Aspid.MVVM.StarterKit.RectOffsetConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) values.

 [RectOffsetScaleConverter](Aspid.MVVM.StarterKit.RectOffsetScaleConverter.md)

Scales a padding.

 [RectToVector4ConverterAsset](Aspid.MVVM.StarterKit.RectToVector4ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Rect`](https://docs.unity3d.com/ScriptReference/Rect.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

 [RectTransformAnchorMaxBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html).

 [RectTransformAnchorMaxEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html) on each element.

 [RectTransformAnchorMaxEnumMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html).

 [RectTransformAnchorMaxMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html).

 [RectTransformAnchorMaxSwitcherBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html).

 [RectTransformAnchorMaxSwitcherMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMaxSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`anchorMax`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMax.html).

 [RectTransformAnchorMinBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html).

 [RectTransformAnchorMinEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html) on each element.

 [RectTransformAnchorMinEnumMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html).

 [RectTransformAnchorMinMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html).

 [RectTransformAnchorMinSwitcherBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html).

 [RectTransformAnchorMinSwitcherMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchorMinSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`anchorMin`](https://docs.unity3d.com/ScriptReference/RectTransform-anchorMin.html).

 [RectTransformAnchoredPosition2DCombineConverter](Aspid.MVVM.StarterKit.RectTransformAnchoredPosition2DCombineConverter.md)

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html)'s anchored position.

 [RectTransformAnchoredPositionBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds
[`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or [`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

 [RectTransformAnchoredPositionCombineConverter](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html)'s anchored position.

 [RectTransformAnchoredPositionEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or
[`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html) on each element.

 [RectTransformAnchoredPositionEnumMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or
[`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

 [RectTransformAnchoredPositionMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
[`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or [`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

 [RectTransformAnchoredPositionSwitcherBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or
[`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

 [RectTransformAnchoredPositionSwitcherMonoBinder](Aspid.MVVM.StarterKit.RectTransformAnchoredPositionSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html) or
[`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

 [RectTransformGettersAndSetters](Aspid.MVVM.StarterKit.RectTransformGettersAndSetters.md)

Extension methods that read and write the anchored position of a [`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html) by [`Space`](https://docs.unity3d.com/ScriptReference/Space.html)
and its size delta by [`SizeDeltaMode`](Aspid.MVVM.StarterKit.SizeDeltaMode.md).

 [RectTransformOffsetMaxBinder](Aspid.MVVM.StarterKit.RectTransformOffsetMaxBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`offsetMax`](https://docs.unity3d.com/ScriptReference/RectTransform-offsetMax.html).

 [RectTransformOffsetMaxMonoBinder](Aspid.MVVM.StarterKit.RectTransformOffsetMaxMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`offsetMax`](https://docs.unity3d.com/ScriptReference/RectTransform-offsetMax.html).

 [RectTransformOffsetMinBinder](Aspid.MVVM.StarterKit.RectTransformOffsetMinBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`offsetMin`](https://docs.unity3d.com/ScriptReference/RectTransform-offsetMin.html).

 [RectTransformOffsetMinMonoBinder](Aspid.MVVM.StarterKit.RectTransformOffsetMinMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`offsetMin`](https://docs.unity3d.com/ScriptReference/RectTransform-offsetMin.html).

 [RectTransformPivotBinder](Aspid.MVVM.StarterKit.RectTransformPivotBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html).

 [RectTransformPivotEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RectTransformPivotEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html) on each element.

 [RectTransformPivotEnumMonoBinder](Aspid.MVVM.StarterKit.RectTransformPivotEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html).

 [RectTransformPivotMonoBinder](Aspid.MVVM.StarterKit.RectTransformPivotMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html).

 [RectTransformPivotSwitcherBinder](Aspid.MVVM.StarterKit.RectTransformPivotSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html).

 [RectTransformPivotSwitcherMonoBinder](Aspid.MVVM.StarterKit.RectTransformPivotSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`pivot`](https://docs.unity3d.com/ScriptReference/RectTransform-pivot.html).

 [RectTransformSizeDeltaBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

 [RectTransformSizeDeltaCombineConverter](Aspid.MVVM.StarterKit.RectTransformSizeDeltaCombineConverter.md)

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html)'s size delta.

 [RectTransformSizeDeltaEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html)
on each element.

 [RectTransformSizeDeltaEnumMonoBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

 [RectTransformSizeDeltaMonoBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

 [RectTransformSizeDeltaSwitcherBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

 [RectTransformSizeDeltaSwitcherMonoBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

 [RectTransformToSourceMonoBinder](Aspid.MVVM.StarterKit.RectTransformToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html).

 [RectVector4Converter](Aspid.MVVM.StarterKit.RectVector4Converter.md)

Converts between a rectangle and a four-component vector, in either direction.

 [RelativeTimeConverter](Aspid.MVVM.StarterKit.RelativeTimeConverter.md)

Writes how long ago, or how far ahead, a moment is.

 [RemapNumberConverter](Aspid.MVVM.StarterKit.RemapNumberConverter.md)

Maps a number from one range onto another.

 [RendererEnabledBinder](Aspid.MVVM.StarterKit.RendererEnabledBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Renderer-enabled.html).

 [RendererEnabledEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RendererEnabledEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Renderer-enabled.html) on each element.

 [RendererEnabledEnumMonoBinder](Aspid.MVVM.StarterKit.RendererEnabledEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`enabled`](https://docs.unity3d.com/ScriptReference/Renderer-enabled.html).

 [RendererEnabledMonoBinder](Aspid.MVVM.StarterKit.RendererEnabledMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`enabled`](https://docs.unity3d.com/ScriptReference/Renderer-enabled.html).

 [RendererExtensions](Aspid.MVVM.StarterKit.RendererExtensions.md)

Extension methods that write validated values to a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsBinder](Aspid.MVVM.StarterKit.RendererMaterialsBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Renderer-material.html) or
[`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

 [RendererMaterialsColorBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsColorEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets a color property on all materials of each element.

 [RendererMaterialsColorEnumMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsColorMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsColorSwitcherBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsColorSwitcherMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsColorSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches a color property on all materials of
a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RendererMaterialsEnumGroupMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md) that sets [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html) on each element.

 [RendererMaterialsEnumMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

 [RendererMaterialsMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Renderer-material.html) or
[`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

 [RendererMaterialsSwitcherBinder](Aspid.MVVM.StarterKit.RendererMaterialsSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

 [RendererMaterialsSwitcherMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

 [RendererPropertyBlockColorMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockColorMonoBinder.md)

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a color shader property.

 [RendererPropertyBlockFloatMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockFloatMonoBinder.md)

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a float shader property.

 [RendererPropertyBlockMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that writes one shader property through a
[`MaterialPropertyBlock`](https://docs.unity3d.com/ScriptReference/MaterialPropertyBlock.html).

 [RendererPropertyBlockTextureMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockTextureMonoBinder.md)

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a texture shader property, also from
a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

 [RendererPropertyBlockVectorMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockVectorMonoBinder.md)

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a vector shader property, also from
[`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) and [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html).

 [RendererShadowCastingBinder](Aspid.MVVM.StarterKit.RendererShadowCastingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`shadowCastingMode`](https://docs.unity3d.com/ScriptReference/Renderer-shadowCastingMode.html).

 [RendererShadowCastingMonoBinder](Aspid.MVVM.StarterKit.RendererShadowCastingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`shadowCastingMode`](https://docs.unity3d.com/ScriptReference/Renderer-shadowCastingMode.html).

 [RendererSortingLayerNameBinder](Aspid.MVVM.StarterKit.RendererSortingLayerNameBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sortingLayerName`](https://docs.unity3d.com/ScriptReference/Renderer-sortingLayerName.html).

 [RendererSortingLayerNameMonoBinder](Aspid.MVVM.StarterKit.RendererSortingLayerNameMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`sortingLayerName`](https://docs.unity3d.com/ScriptReference/Renderer-sortingLayerName.html).

 [RendererSortingOrderBinder](Aspid.MVVM.StarterKit.RendererSortingOrderBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html).

 [RendererSortingOrderMonoBinder](Aspid.MVVM.StarterKit.RendererSortingOrderMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html).

 [RendererToSourceMonoBinder](Aspid.MVVM.StarterKit.RendererToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

 [RepeatStringConverter](Aspid.MVVM.StarterKit.RepeatStringConverter.md)

Repeats a piece of text once per count.

 [ReplaceStringConverter](Aspid.MVVM.StarterKit.ReplaceStringConverter.md)

Replaces occurrences of one piece of text with another.

 [ReverseStringConverter](Aspid.MVVM.StarterKit.ReverseStringConverter.md)

Writes a string back to front.

 [RichTextColorConverter](Aspid.MVVM.StarterKit.RichTextColorConverter.md)

Wraps a string in a rich-text color tag.

 [RichTextNoParseConverter](Aspid.MVVM.StarterKit.RichTextNoParseConverter.md)

Stops rich-text markup in a string from being interpreted.

 [RichTextSanitizeConverter](Aspid.MVVM.StarterKit.RichTextSanitizeConverter.md)

Takes rich-text markup out of a string, or shows it as text instead of obeying it.

 [RichTextSizeConverter](Aspid.MVVM.StarterKit.RichTextSizeConverter.md)

Wraps a string in a rich-text size tag.

 [RichTextStyleConverter](Aspid.MVVM.StarterKit.RichTextStyleConverter.md)

Wraps a string in rich-text style tags.

 [Rigidbody2DBodyTypeBinder](Aspid.MVVM.StarterKit.Rigidbody2DBodyTypeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`bodyType`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-bodyType.html).

 [Rigidbody2DBodyTypeMonoBinder](Aspid.MVVM.StarterKit.Rigidbody2DBodyTypeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`bodyType`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-bodyType.html).

 [Rigidbody2DGravityScaleBinder](Aspid.MVVM.StarterKit.Rigidbody2DGravityScaleBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`gravityScale`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-gravityScale.html).

 [Rigidbody2DGravityScaleMonoBinder](Aspid.MVVM.StarterKit.Rigidbody2DGravityScaleMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`gravityScale`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-gravityScale.html).

 [Rigidbody2DMassBinder](Aspid.MVVM.StarterKit.Rigidbody2DMassBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`mass`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-mass.html).

 [Rigidbody2DMassMonoBinder](Aspid.MVVM.StarterKit.Rigidbody2DMassMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`mass`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-mass.html).

 [Rigidbody2DSimulatedBinder](Aspid.MVVM.StarterKit.Rigidbody2DSimulatedBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`simulated`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-simulated.html).

 [Rigidbody2DSimulatedMonoBinder](Aspid.MVVM.StarterKit.Rigidbody2DSimulatedMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`simulated`](https://docs.unity3d.com/ScriptReference/Rigidbody2D-simulated.html).

 [RigidbodyConstraintsBinder](Aspid.MVVM.StarterKit.RigidbodyConstraintsBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`constraints`](https://docs.unity3d.com/ScriptReference/Rigidbody-constraints.html).

 [RigidbodyConstraintsMonoBinder](Aspid.MVVM.StarterKit.RigidbodyConstraintsMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`constraints`](https://docs.unity3d.com/ScriptReference/Rigidbody-constraints.html).

 [RigidbodyIsKinematicBinder](Aspid.MVVM.StarterKit.RigidbodyIsKinematicBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`isKinematic`](https://docs.unity3d.com/ScriptReference/Rigidbody-isKinematic.html).

 [RigidbodyIsKinematicMonoBinder](Aspid.MVVM.StarterKit.RigidbodyIsKinematicMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`isKinematic`](https://docs.unity3d.com/ScriptReference/Rigidbody-isKinematic.html).

 [RigidbodyMassBinder](Aspid.MVVM.StarterKit.RigidbodyMassBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`mass`](https://docs.unity3d.com/ScriptReference/Rigidbody-mass.html).

 [RigidbodyMassMonoBinder](Aspid.MVVM.StarterKit.RigidbodyMassMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`mass`](https://docs.unity3d.com/ScriptReference/Rigidbody-mass.html).

 [RigidbodyUseGravityBinder](Aspid.MVVM.StarterKit.RigidbodyUseGravityBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`useGravity`](https://docs.unity3d.com/ScriptReference/Rigidbody-useGravity.html).

 [RigidbodyUseGravityMonoBinder](Aspid.MVVM.StarterKit.RigidbodyUseGravityMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`useGravity`](https://docs.unity3d.com/ScriptReference/Rigidbody-useGravity.html).

 [RomanNumeralConverter](Aspid.MVVM.StarterKit.RomanNumeralConverter.md)

Formats a number as a Roman numeral.

 [RoundNumberConverter](Aspid.MVVM.StarterKit.RoundNumberConverter.md)

Rounds a number, in a way the caller chooses.

 [SafeConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.SafeConverter-2.md)

Runs another converter and substitutes a fallback value if it throws.

 [ScreenFullScreenBinder](Aspid.MVVM.StarterKit.ScreenFullScreenBinder.md)

[`Binder<T>`](Aspid.MVVM.StarterKit.Binder-1.md) that binds [`fullScreen`](https://docs.unity3d.com/ScriptReference/Screen-fullScreen.html).

 [ScreenFullScreenMonoBinder](Aspid.MVVM.StarterKit.ScreenFullScreenMonoBinder.md)

[`MonoBinder<T>`](Aspid.MVVM.StarterKit.MonoBinder-1.md) that binds [`fullScreen`](https://docs.unity3d.com/ScriptReference/Screen-fullScreen.html).

 [ScrollRectCommandBinder](Aspid.MVVM.StarterKit.ScrollRectCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with
the normalized position.

 [ScrollRectCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with
the normalized position and [`ScrollRectCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_3_Param1),
[`ScrollRectCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_3_Param2), [`ScrollRectCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_3_Param3).

 [ScrollRectCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with
the normalized position and [`ScrollRectCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_2_Param1), [`ScrollRectCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_2_Param2).

 [ScrollRectCommandBinder\<T\>](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with
the normalized position and [`ScrollRectCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-1.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_Param).

 [ScrollRectCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position and [`ScrollRectCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_1_Param).

 [ScrollRectCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position and [`ScrollRectCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param1),
[`ScrollRectCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param2), [`ScrollRectCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param3).

 [ScrollRectCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position and [`ScrollRectCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param1),
[`ScrollRectCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param2).

 [ScrollRectCommandMonoBinder](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position.

 [ScrollRectHorizontalBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`horizontal`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontal.html).

 [ScrollRectHorizontalEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`horizontal`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontal.html) on each
element.

 [ScrollRectHorizontalEnumMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`horizontal`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontal.html).

 [ScrollRectHorizontalMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`horizontal`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontal.html).

 [ScrollRectHorizontalNormalizedPositionBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalNormalizedPositionBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds
[`horizontalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontalNormalizedPosition.html).

 [ScrollRectHorizontalNormalizedPositionEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalNormalizedPositionEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`horizontalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontalNormalizedPosition.html) on each element.

 [ScrollRectHorizontalNormalizedPositionEnumMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalNormalizedPositionEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets
[`horizontalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontalNormalizedPosition.html).

 [ScrollRectHorizontalNormalizedPositionMonoBinder](Aspid.MVVM.StarterKit.ScrollRectHorizontalNormalizedPositionMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds
[`horizontalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-horizontalNormalizedPosition.html).

 [ScrollRectNormalizedPositionBinder](Aspid.MVVM.StarterKit.ScrollRectNormalizedPositionBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`normalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-normalizedPosition.html).

 [ScrollRectNormalizedPositionMonoBinder](Aspid.MVVM.StarterKit.ScrollRectNormalizedPositionMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`normalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-normalizedPosition.html).

 [ScrollRectToSourceMonoBinder](Aspid.MVVM.StarterKit.ScrollRectToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`ScrollRect`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect.html).

 [ScrollRectVerticalBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`vertical`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-vertical.html).

 [ScrollRectVerticalEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`vertical`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-vertical.html) on each
element.

 [ScrollRectVerticalEnumMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`vertical`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-vertical.html).

 [ScrollRectVerticalMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`vertical`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-vertical.html).

 [ScrollRectVerticalNormalizedPositionBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalNormalizedPositionBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds
[`verticalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-verticalNormalizedPosition.html).

 [ScrollRectVerticalNormalizedPositionEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalNormalizedPositionEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets
[`verticalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-verticalNormalizedPosition.html) on each element.

 [ScrollRectVerticalNormalizedPositionEnumMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalNormalizedPositionEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets
[`verticalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-verticalNormalizedPosition.html).

 [ScrollRectVerticalNormalizedPositionMonoBinder](Aspid.MVVM.StarterKit.ScrollRectVerticalNormalizedPositionMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds
[`verticalNormalizedPosition`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-verticalNormalizedPosition.html).

 [ScrollbarCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-2.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_2_Param1), [`ScrollbarCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-2.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_2_Param2).

 [ScrollbarCommandBinder](Aspid.MVVM.StarterKit.ScrollbarCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value.

 [ScrollbarCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_3_Param1),
[`ScrollbarCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_3_Param2), [`ScrollbarCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_3_Param3).

 [ScrollbarCommandBinder\<T\>](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.ScrollbarCommandBinder-1.md#Aspid_MVVM_StarterKit_ScrollbarCommandBinder_1_Param).

 [ScrollbarCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_1_Param).

 [ScrollbarCommandMonoBinder](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value.

 [ScrollbarCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_3_Param1),
[`ScrollbarCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_3_Param2), [`ScrollbarCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_3_Param3).

 [ScrollbarCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value and [`ScrollbarCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_2_Param1), [`ScrollbarCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_2_Param2).

 [ScrollbarSizeBinder](Aspid.MVVM.StarterKit.ScrollbarSizeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-size.html).

 [ScrollbarSizeMonoBinder](Aspid.MVVM.StarterKit.ScrollbarSizeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-size.html).

 [ScrollbarToSourceMonoBinder](Aspid.MVVM.StarterKit.ScrollbarToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Scrollbar`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar.html).

 [ScrollbarValueBinder](Aspid.MVVM.StarterKit.ScrollbarValueBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-value.html), also from other numbers,
and reports user changes back as numbers.

 [ScrollbarValueEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ScrollbarValueEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`value`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-value.html) on each element.

 [ScrollbarValueEnumMonoBinder](Aspid.MVVM.StarterKit.ScrollbarValueEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`value`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-value.html).

 [ScrollbarValueMonoBinder](Aspid.MVVM.StarterKit.ScrollbarValueMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-value.html), also from other numbers,
and reports user changes back as numbers.

 [ScrollbarValueSwitcherMonoBinder](Aspid.MVVM.StarterKit.ScrollbarValueSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`value`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-value.html).

 [SecondsToTimeSpanConverter](Aspid.MVVM.StarterKit.SecondsToTimeSpanConverter.md)

Converts a number of seconds to a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan).

 [SecondsToTimeStringConverter](Aspid.MVVM.StarterKit.SecondsToTimeStringConverter.md)

Writes a number of seconds as a clock reading.

 [SelectableColorBlockBinder](Aspid.MVVM.StarterKit.SelectableColorBlockBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html).

 [SelectableColorBlockEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SelectableColorBlockEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html) on each element.

 [SelectableColorBlockEnumMonoBinder](Aspid.MVVM.StarterKit.SelectableColorBlockEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html).

 [SelectableColorBlockMonoBinder](Aspid.MVVM.StarterKit.SelectableColorBlockMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html).

 [SelectableColorBlockSwitcherBinder](Aspid.MVVM.StarterKit.SelectableColorBlockSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html).

 [SelectableColorBlockSwitcherMonoBinder](Aspid.MVVM.StarterKit.SelectableColorBlockSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`colors`](https://docs.unity3d.com/ScriptReference/UI-Selectable-colors.html).

 [SelectableInteractableBinder](Aspid.MVVM.StarterKit.SelectableInteractableBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`interactable`](https://docs.unity3d.com/ScriptReference/UI-Selectable-interactable.html).

 [SelectableInteractableEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SelectableInteractableEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`interactable`](https://docs.unity3d.com/ScriptReference/UI-Selectable-interactable.html) on each
element.

 [SelectableInteractableEnumMonoBinder](Aspid.MVVM.StarterKit.SelectableInteractableEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`interactable`](https://docs.unity3d.com/ScriptReference/UI-Selectable-interactable.html).

 [SelectableInteractableMonoBinder](Aspid.MVVM.StarterKit.SelectableInteractableMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`interactable`](https://docs.unity3d.com/ScriptReference/UI-Selectable-interactable.html).

 [SelectableTargetGraphicBinder](Aspid.MVVM.StarterKit.SelectableTargetGraphicBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`targetGraphic`](https://docs.unity3d.com/ScriptReference/UI-Selectable-targetGraphic.html).

 [SelectableTargetGraphicMonoBinder](Aspid.MVVM.StarterKit.SelectableTargetGraphicMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`targetGraphic`](https://docs.unity3d.com/ScriptReference/UI-Selectable-targetGraphic.html).

 [SelectableToSourceMonoBinder](Aspid.MVVM.StarterKit.SelectableToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html).

 [SelectableTransitionBinder](Aspid.MVVM.StarterKit.SelectableTransitionBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`transition`](https://docs.unity3d.com/ScriptReference/UI-Selectable-transition.html).

 [SelectableTransitionMonoBinder](Aspid.MVVM.StarterKit.SelectableTransitionMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`transition`](https://docs.unity3d.com/ScriptReference/UI-Selectable-transition.html).

 [SequenceCanExecuteHandler](Aspid.MVVM.StarterKit.SequenceCanExecuteHandler.md)

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that forwards the state to every nested handler in order.

 [SequenceCollectionOrder\<T\>](Aspid.MVVM.StarterKit.SequenceCollectionOrder-1.md)

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that applies multiple orders in sequence: the first one that tells
two elements apart decides. Empty slots are skipped.

 [SequenceConverter\<T\>](Aspid.MVVM.StarterKit.SequenceConverter-1.md)

Applies multiple converters to a value in sequence.

 [ShadowEffectColorBinder](Aspid.MVVM.StarterKit.ShadowEffectColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`effectColor`](https://docs.unity3d.com/ScriptReference/UI-Shadow-effectColor.html).

 [ShadowEffectColorMonoBinder](Aspid.MVVM.StarterKit.ShadowEffectColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`effectColor`](https://docs.unity3d.com/ScriptReference/UI-Shadow-effectColor.html).

 [ShadowEffectDistanceBinder](Aspid.MVVM.StarterKit.ShadowEffectDistanceBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`effectDistance`](https://docs.unity3d.com/ScriptReference/UI-Shadow-effectDistance.html).

 [ShadowEffectDistanceMonoBinder](Aspid.MVVM.StarterKit.ShadowEffectDistanceMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`effectDistance`](https://docs.unity3d.com/ScriptReference/UI-Shadow-effectDistance.html).

 [SignedNumberStringConverter](Aspid.MVVM.StarterKit.SignedNumberStringConverter.md)

Formats a number with an explicit sign: "+15", "-3".

 [SingleFormPluralRule](Aspid.MVVM.StarterKit.SingleFormPluralRule.md)

One word for every count: Chinese, Japanese, Korean, Thai, Vietnamese, Turkish, Indonesian.

 [SliderCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.SliderCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value and [`SliderCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.SliderCommandBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandBinder_3_Param1),
[`SliderCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.SliderCommandBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandBinder_3_Param2), [`SliderCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.SliderCommandBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandBinder_3_Param3).

 [SliderCommandBinder](Aspid.MVVM.StarterKit.SliderCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value.

 [SliderCommandBinder\<T\>](Aspid.MVVM.StarterKit.SliderCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value and [`SliderCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.SliderCommandBinder-1.md#Aspid_MVVM_StarterKit_SliderCommandBinder_1_Param).

 [SliderCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.SliderCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value and [`SliderCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.SliderCommandBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandBinder_2_Param1), [`SliderCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.SliderCommandBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandBinder_2_Param2).

 [SliderCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with the slider value and [`SliderCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_1_Param).

 [SliderCommandMonoBinder](Aspid.MVVM.StarterKit.SliderCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with the slider value.

 [SliderCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with the slider value and [`SliderCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_3_Param1),
[`SliderCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_3_Param2), [`SliderCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_3_Param3).

 [SliderCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with the slider value and [`SliderCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param1), [`SliderCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param2).

 [SliderExtensions](Aspid.MVVM.StarterKit.SliderExtensions.md)

Extension methods for [`Slider`](https://docs.unity3d.com/ScriptReference/UI-Slider.html) used by the slider binders.

 [SliderMinMaxBinder](Aspid.MVVM.StarterKit.SliderMinMaxBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html) as <code>(min, max)</code>.

 [SliderMinMaxEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SliderMinMaxEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html) on each element.

 [SliderMinMaxEnumMonoBinder](Aspid.MVVM.StarterKit.SliderMinMaxEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html).

 [SliderMinMaxMonoBinder](Aspid.MVVM.StarterKit.SliderMinMaxMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html) as <code>(min, max)</code>.

 [SliderMinMaxSwitcherBinder](Aspid.MVVM.StarterKit.SliderMinMaxSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html).

 [SliderMinMaxSwitcherMonoBinder](Aspid.MVVM.StarterKit.SliderMinMaxSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html).

 [SliderToSourceMonoBinder](Aspid.MVVM.StarterKit.SliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Slider`](https://docs.unity3d.com/ScriptReference/UI-Slider.html).

 [SliderValueBinder](Aspid.MVVM.StarterKit.SliderValueBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UI-Slider-value.html), also from other numbers, and
reports user changes back as numbers.

 [SliderValueEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SliderValueEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`value`](https://docs.unity3d.com/ScriptReference/UI-Slider-value.html) on each element.

 [SliderValueEnumMonoBinder](Aspid.MVVM.StarterKit.SliderValueEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`value`](https://docs.unity3d.com/ScriptReference/UI-Slider-value.html).

 [SliderValueMonoBinder](Aspid.MVVM.StarterKit.SliderValueMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UI-Slider-value.html), also from other numbers,
and reports user changes back as numbers.

 [SliderValueSwitcherMonoBinder](Aspid.MVVM.StarterKit.SliderValueSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`value`](https://docs.unity3d.com/ScriptReference/UI-Slider-value.html).

 [SmoothStepConverter](Aspid.MVVM.StarterKit.SmoothStepConverter.md)

Converts a 0..1 position to a value in a range, eased in and out at the ends.

 [SnapToStepConverter](Aspid.MVVM.StarterKit.SnapToStepConverter.md)

Snaps a number to the nearest multiple of a step.

 [SphereColliderCenterBinder](Aspid.MVVM.StarterKit.SphereColliderCenterBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html).

 [SphereColliderCenterCombineConverter](Aspid.MVVM.StarterKit.SphereColliderCenterCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`SphereCollider`](https://docs.unity3d.com/ScriptReference/SphereCollider.html)'s center.

 [SphereColliderCenterEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SphereColliderCenterEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html) on each element.

 [SphereColliderCenterEnumMonoBinder](Aspid.MVVM.StarterKit.SphereColliderCenterEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html).

 [SphereColliderCenterMonoBinder](Aspid.MVVM.StarterKit.SphereColliderCenterMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html).

 [SphereColliderCenterSwitcherBinder](Aspid.MVVM.StarterKit.SphereColliderCenterSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html).

 [SphereColliderCenterSwitcherMonoBinder](Aspid.MVVM.StarterKit.SphereColliderCenterSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`center`](https://docs.unity3d.com/ScriptReference/SphereCollider-center.html).

 [SphereColliderRadiusBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html).

 [SphereColliderRadiusEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html) on each element.

 [SphereColliderRadiusEnumMonoBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html).

 [SphereColliderRadiusMonoBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html).

 [SphereColliderRadiusSwitcherBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html).

 [SphereColliderRadiusSwitcherMonoBinder](Aspid.MVVM.StarterKit.SphereColliderRadiusSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`radius`](https://docs.unity3d.com/ScriptReference/SphereCollider-radius.html).

 [SphereColliderToSourceMonoBinder](Aspid.MVVM.StarterKit.SphereColliderToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`SphereCollider`](https://docs.unity3d.com/ScriptReference/SphereCollider.html).

 [SplitJoinStringConverter](Aspid.MVVM.StarterKit.SplitJoinStringConverter.md)

Splits a string and joins the parts back together with different text.

 [SpriteRendererColorBinder](Aspid.MVVM.StarterKit.SpriteRendererColorBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-color.html).

 [SpriteRendererColorEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererColorEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`color`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-color.html) on each
element.

 [SpriteRendererColorEnumMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererColorEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`color`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-color.html).

 [SpriteRendererColorMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererColorMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`color`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-color.html).

 [SpriteRendererFlipXBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipXBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`flipX`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipX.html).

 [SpriteRendererFlipXEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipXEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`flipX`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipX.html) on each
element.

 [SpriteRendererFlipXEnumMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipXEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`flipX`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipX.html).

 [SpriteRendererFlipXMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipXMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`flipX`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipX.html).

 [SpriteRendererFlipYBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipYBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`flipY`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipY.html).

 [SpriteRendererFlipYEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipYEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`flipY`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipY.html) on each
element.

 [SpriteRendererFlipYEnumMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipYEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`flipY`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipY.html).

 [SpriteRendererFlipYMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererFlipYMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`flipY`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-flipY.html).

 [SpriteRendererSizeBinder](Aspid.MVVM.StarterKit.SpriteRendererSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-size.html).

 [SpriteRendererSizeMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`size`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-size.html).

 [SpriteRendererSortingOrderBinder](Aspid.MVVM.StarterKit.SpriteRendererSortingOrderBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html).

 [SpriteRendererSortingOrderEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSortingOrderEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html) on each
element.

 [SpriteRendererSortingOrderEnumMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSortingOrderEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html).

 [SpriteRendererSortingOrderMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSortingOrderMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`sortingOrder`](https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html).

 [SpriteRendererSpriteBinder](Aspid.MVVM.StarterKit.SpriteRendererSpriteBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html).

 [SpriteRendererSpriteEnumGroupMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSpriteEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`sprite`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html) on each
element.

 [SpriteRendererSpriteEnumMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSpriteEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`sprite`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html).

 [SpriteRendererSpriteMonoBinder](Aspid.MVVM.StarterKit.SpriteRendererSpriteMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`sprite`](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html).

 [SpriteToTextureConverter](Aspid.MVVM.StarterKit.SpriteToTextureConverter.md)

Takes the texture a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) is drawn from.

 [SpriteToTextureConverterAsset](Aspid.MVVM.StarterKit.SpriteToTextureConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) to [`Texture`](https://docs.unity3d.com/ScriptReference/Texture.html) conversions.

 [StringAggregatorInputMonoBinder](Aspid.MVVM.StarterKit.StringAggregatorInputMonoBinder.md)

[`AggregatorInputMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md) that feeds one <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> into a
[`FormatStringMonoBinder`](Aspid.MVVM.StarterKit.FormatStringMonoBinder.md).

 [StringConverterAsset](Aspid.MVVM.StarterKit.StringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) values.

 [StringEmptyToBoolConverter](Aspid.MVVM.StarterKit.StringEmptyToBoolConverter.md)

Tests whether a string is absent.

 [StringFormatConverter](Aspid.MVVM.StarterKit.StringFormatConverter.md)

[`ValueToStringConverter<T>`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md) for strings, with optional handling of empty values.

 [StringMatchToBoolConverter](Aspid.MVVM.StarterKit.StringMatchToBoolConverter.md)

Tests a bound string against an authored one.

 [StringToBoolCasterMonoBinder](Aspid.MVVM.StarterKit.StringToBoolCasterMonoBinder.md)

[`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`String`](https://learn.microsoft.com/dotnet/api/system.string) to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>. Defaults to [`StringEmptyToBoolConverter`](Aspid.MVVM.StarterKit.StringEmptyToBoolConverter.md).

 [StringToBoolConverter](Aspid.MVVM.StarterKit.StringToBoolConverter.md)

Reads a boolean out of text.

 [StringToBoolConverterAsset](Aspid.MVVM.StarterKit.StringToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [StringToColorConverterAsset](Aspid.MVVM.StarterKit.StringToColorConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) conversions.

 [StringToDateTimeConverter](Aspid.MVVM.StarterKit.StringToDateTimeConverter.md)

Reads a date out of text.

 [StringToDateTimeConverterAsset](Aspid.MVVM.StarterKit.StringToDateTimeConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) conversions.

 [StringToDecimalConverter](Aspid.MVVM.StarterKit.StringToDecimalConverter.md)

Reads an exact decimal number out of text.

 [StringToDecimalConverterAsset](Aspid.MVVM.StarterKit.StringToDecimalConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) conversions.

 [StringToDoubleConverter](Aspid.MVVM.StarterKit.StringToDoubleConverter.md)

Reads a decimal number out of text, keeping the precision a float would lose.

 [StringToDoubleConverterAsset](Aspid.MVVM.StarterKit.StringToDoubleConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Double`](https://learn.microsoft.com/dotnet/api/system.double) conversions.

 [StringToEnumCasterMonoBinder\<TEnum\>](Aspid.MVVM.StarterKit.StringToEnumCasterMonoBinder-1.md)

Abstract base [`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`String`](https://learn.microsoft.com/dotnet/api/system.string) to <code class="typeparamref">TEnum</code>.
Defaults to [`StringToEnumConverter<T>`](Aspid.MVVM.StarterKit.StringToEnumConverter-1.md). Close over a concrete enum to make it addable as a component.

 [StringToEnumConverter\<TEnum\>](Aspid.MVVM.StarterKit.StringToEnumConverter-1.md)

Reads an enum member out of text.

 [StringToFloatCasterMonoBinder](Aspid.MVVM.StarterKit.StringToFloatCasterMonoBinder.md)

[`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`String`](https://learn.microsoft.com/dotnet/api/system.string) to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a>. Defaults to [`StringToFloatConverter`](Aspid.MVVM.StarterKit.StringToFloatConverter.md).

 [StringToFloatConverter](Aspid.MVVM.StarterKit.StringToFloatConverter.md)

Reads a decimal number out of text.

 [StringToFloatConverterAsset](Aspid.MVVM.StarterKit.StringToFloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

 [StringToIntCasterMonoBinder](Aspid.MVVM.StarterKit.StringToIntCasterMonoBinder.md)

[`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`String`](https://learn.microsoft.com/dotnet/api/system.string) to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>. Defaults to [`StringToIntConverter`](Aspid.MVVM.StarterKit.StringToIntConverter.md).

 [StringToIntConverter](Aspid.MVVM.StarterKit.StringToIntConverter.md)

Reads a whole number out of text.

 [StringToIntConverterAsset](Aspid.MVVM.StarterKit.StringToIntConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) conversions.

 [StringToLongConverter](Aspid.MVVM.StarterKit.StringToLongConverter.md)

Reads a whole number out of text, past the range an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> can hold.

 [StringToLongConverterAsset](Aspid.MVVM.StarterKit.StringToLongConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) conversions.

 [StringToNumberConverter\<T\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md)

Abstract base for a converter that reads a number out of text and writes it back.

 [StringToSpriteConverter](Aspid.MVVM.StarterKit.StringToSpriteConverter.md)

Looks a sprite up by name.

 [StringToSpriteConverterAsset](Aspid.MVVM.StarterKit.StringToSpriteConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) conversions.

 [StringToTimeSpanConverter](Aspid.MVVM.StarterKit.StringToTimeSpanConverter.md)

Reads a duration out of text.

 [StringToTimeSpanConverterAsset](Aspid.MVVM.StarterKit.StringToTimeSpanConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) conversions.

 [StringToVector2Converter](Aspid.MVVM.StarterKit.StringToVector2Converter.md)

Reads a 2D vector out of text.

 [StringToVector2ConverterAsset](Aspid.MVVM.StarterKit.StringToVector2ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) conversions.

 [StringToVector3Converter](Aspid.MVVM.StarterKit.StringToVector3Converter.md)

Reads a 3D vector out of text.

 [StringToVector3ConverterAsset](Aspid.MVVM.StarterKit.StringToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [SubstringConverter](Aspid.MVVM.StarterKit.SubstringConverter.md)

Takes a slice out of a string.

 [SwitcherBinder\<T\>](Aspid.MVVM.StarterKit.SwitcherBinder-1.md)

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that applies one of two preset values depending on a bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>,
passing the chosen value through an optional converter first.

 [SwitcherBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md)

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that applies one of two preset values depending on a bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>,
passing the chosen value through an optional converter first.

 [SwitcherMonoBinder\<T\>](Aspid.MVVM.StarterKit.SwitcherMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that applies one of two preset values depending on a bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>,
passing the chosen value through an optional converter first.

 [SwitcherMonoBinder\<TComponent, T\>](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that applies one of two preset values depending on a bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>,
passing the chosen value through an optional converter first.

 [TargetBinder\<TTarget, TProperty\>](Aspid.MVVM.StarterKit.TargetBinder-2.md)

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds a single property of the target through its accessors,
applying an optional converter in both directions. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md), the current property value
is sent to the ViewModel on binding.

 [TargetFloatBinder\<TTarget\>](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md)

Abstract base [`StarterKit.TargetBinder<T1, T2>?text=TargetBinder%3cTTarget%2c+float%3e`](Aspid.MVVM.StarterKit.md) that binds a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> property,
accepting every numeric type via [`IFloatBinder`](Aspid.MVVM.StarterKit.IFloatBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [TargetFrameRateBinder](Aspid.MVVM.StarterKit.TargetFrameRateBinder.md)

[`IntBinder`](Aspid.MVVM.StarterKit.IntBinder.md) that binds [`targetFrameRate`](https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html).

 [TargetFrameRateMonoBinder](Aspid.MVVM.StarterKit.TargetFrameRateMonoBinder.md)

[`IntMonoBinder`](Aspid.MVVM.StarterKit.IntMonoBinder.md) that binds [`targetFrameRate`](https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html).

 [TargetIntBinder\<TTarget\>](Aspid.MVVM.StarterKit.TargetIntBinder-1.md)

Abstract base [`StarterKit.TargetBinder<T1, T2>?text=TargetBinder%3cTTarget%2c+int%3e`](Aspid.MVVM.StarterKit.md) that binds an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> property,
accepting every numeric type via [`IIntBinder`](Aspid.MVVM.StarterKit.IIntBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

 [TargetObjectBinder\<TTarget, TObject\>](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md)

Abstract base [`StarterKit.TargetBinder<T1, T2>?text=TargetBinder%3cTTarget%2c+TObject%3e`](Aspid.MVVM.StarterKit.md) that binds a
[`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html) reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

 [TextAlignmentBinder](Aspid.MVVM.StarterKit.TextAlignmentBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `alignment`.

 [TextAlignmentEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TextAlignmentEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `alignment` on each element.

 [TextAlignmentEnumMonoBinder](Aspid.MVVM.StarterKit.TextAlignmentEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `alignment`.

 [TextAlignmentMonoBinder](Aspid.MVVM.StarterKit.TextAlignmentMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `alignment`.

 [TextAlignmentSwitcherBinder](Aspid.MVVM.StarterKit.TextAlignmentSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `alignment`.

 [TextAlignmentSwitcherMonoBinder](Aspid.MVVM.StarterKit.TextAlignmentSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `alignment`.

 [TextAutoSizeBinder](Aspid.MVVM.StarterKit.TextAutoSizeBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `enableAutoSizing`.

 [TextAutoSizeMonoBinder](Aspid.MVVM.StarterKit.TextAutoSizeMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `enableAutoSizing`.

 [TextBinder](Aspid.MVVM.StarterKit.TextBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `text`, also from
numbers.

 [TextCaseConverter](Aspid.MVVM.StarterKit.TextCaseConverter.md)

Changes the casing of a string.

 [TextCharacterSpacingBinder](Aspid.MVVM.StarterKit.TextCharacterSpacingBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds `characterSpacing`.

 [TextCharacterSpacingMonoBinder](Aspid.MVVM.StarterKit.TextCharacterSpacingMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds `characterSpacing`.

 [TextEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TextEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `text` on each element.

 [TextEnumMonoBinder](Aspid.MVVM.StarterKit.TextEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `text`.

 [TextFontBinder](Aspid.MVVM.StarterKit.TextFontBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds `font`.

 [TextFontEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TextFontEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `font` on each element.

 [TextFontEnumMonoBinder](Aspid.MVVM.StarterKit.TextFontEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `font`.

 [TextFontMonoBinder](Aspid.MVVM.StarterKit.TextFontMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds `font`.

 [TextFontSizeBinder](Aspid.MVVM.StarterKit.TextFontSizeBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds `fontSize`.

 [TextFontSizeEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TextFontSizeEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets `fontSize` on each element.

 [TextFontSizeEnumMonoBinder](Aspid.MVVM.StarterKit.TextFontSizeEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets `fontSize`.

 [TextFontSizeMonoBinder](Aspid.MVVM.StarterKit.TextFontSizeMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds `fontSize`.

 [TextFontSizeSwitcherBinder](Aspid.MVVM.StarterKit.TextFontSizeSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `fontSize`.

 [TextFontSizeSwitcherMonoBinder](Aspid.MVVM.StarterKit.TextFontSizeSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `fontSize`.

 [TextFontStyleBinder](Aspid.MVVM.StarterKit.TextFontStyleBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `fontStyle`.

 [TextFontStyleMonoBinder](Aspid.MVVM.StarterKit.TextFontStyleMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `fontStyle`.

 [TextFontSwitcherBinder](Aspid.MVVM.StarterKit.TextFontSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `font`.

 [TextFontSwitcherMonoBinder](Aspid.MVVM.StarterKit.TextFontSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `font`.

 [TextLineSpacingBinder](Aspid.MVVM.StarterKit.TextLineSpacingBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds `lineSpacing`.

 [TextLineSpacingMonoBinder](Aspid.MVVM.StarterKit.TextLineSpacingMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds `lineSpacing`.

 [TextLocalizationEntryBinder](Aspid.MVVM.StarterKit.TextLocalizationEntryBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `text` to a Unity
Localization entry.

 [TextLocalizationEntryEnumMonoBinder](Aspid.MVVM.StarterKit.TextLocalizationEntryEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets the Unity Localization entry written to
`text`.

 [TextLocalizationEntryMonoBinder](Aspid.MVVM.StarterKit.TextLocalizationEntryMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `text` to a Unity
Localization entry.

 [TextLocalizationEntrySwitcherBinder](Aspid.MVVM.StarterKit.TextLocalizationEntrySwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches the Unity Localization entry written to
`text`.

 [TextLocalizationEntrySwitcherMonoBinder](Aspid.MVVM.StarterKit.TextLocalizationEntrySwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches the Unity Localization entry written to
`text`.

 [TextMarginBinder](Aspid.MVVM.StarterKit.TextMarginBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `margin`.

 [TextMarginMonoBinder](Aspid.MVVM.StarterKit.TextMarginMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `margin`.

 [TextMaxVisibleCharactersBinder](Aspid.MVVM.StarterKit.TextMaxVisibleCharactersBinder.md)

[`TargetIntBinder<T>`](Aspid.MVVM.StarterKit.TargetIntBinder-1.md) that binds `maxVisibleCharacters`.

 [TextMaxVisibleCharactersMonoBinder](Aspid.MVVM.StarterKit.TextMaxVisibleCharactersMonoBinder.md)

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds `maxVisibleCharacters`.

 [TextMonoBinder](Aspid.MVVM.StarterKit.TextMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `text`, also from
numbers.

 [TextRichTextBinder](Aspid.MVVM.StarterKit.TextRichTextBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `richText`.

 [TextRichTextMonoBinder](Aspid.MVVM.StarterKit.TextRichTextMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds `richText`.

 [TextSwitcherBinder](Aspid.MVVM.StarterKit.TextSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `text`.

 [TextSwitcherMonoBinder](Aspid.MVVM.StarterKit.TextSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches `text`.

 [TextToSourceMonoBinder](Aspid.MVVM.StarterKit.TextToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for `TMP_Text`.

 [Texture2DToSpriteConverter](Aspid.MVVM.StarterKit.Texture2DToSpriteConverter.md)

Wraps a [`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) in a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

 [Texture2DToSpriteConverterAsset](Aspid.MVVM.StarterKit.Texture2DToSpriteConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) to [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) conversions.

 [TextureToRectConverterAsset](Aspid.MVVM.StarterKit.TextureToRectConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Texture`](https://docs.unity3d.com/ScriptReference/Texture.html) to [`Rect`](https://docs.unity3d.com/ScriptReference/Rect.html) conversions.

 [TextureToSpriteRectConverter](Aspid.MVVM.StarterKit.TextureToSpriteRectConverter.md)

Measures the pixel rect of a texture.

 [ThousandsSeparatorConverter](Aspid.MVVM.StarterKit.ThousandsSeparatorConverter.md)

Groups the digits of a whole number: 1234567 becomes "1,234,567".

 [ThresholdColorConverter](Aspid.MVVM.StarterKit.ThresholdColorConverter.md)

Picks a color by which threshold a number has passed.

 [ThresholdRichTextColorConverter](Aspid.MVVM.StarterKit.ThresholdRichTextColorConverter.md)

Writes a number as colored text, the color chosen by how large it is.

 [ThrottleFloatMonoBinder](Aspid.MVVM.StarterKit.ThrottleFloatMonoBinder.md)

[`ThrottleMonoBinder<T>`](Aspid.MVVM.StarterKit.ThrottleMonoBinder-1.md) that forwards at most one <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> per interval.

 [ThrottleMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.ThrottleMonoBinder-1.md)

Abstract [`RateLimitedMonoBinder<T>`](Aspid.MVVM.StarterKit.RateLimitedMonoBinder-1.md) that forwards at most one value per interval.

 [ThrottleStringMonoBinder](Aspid.MVVM.StarterKit.ThrottleStringMonoBinder.md)

[`ThrottleMonoBinder<T>`](Aspid.MVVM.StarterKit.ThrottleMonoBinder-1.md) that forwards at most one <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a> per interval.

 [TimeScaleBinder](Aspid.MVVM.StarterKit.TimeScaleBinder.md)

[`FloatBinder`](Aspid.MVVM.StarterKit.FloatBinder.md) that binds [`timeScale`](https://docs.unity3d.com/ScriptReference/Time-timeScale.html).

 [TimeScaleMonoBinder](Aspid.MVVM.StarterKit.TimeScaleMonoBinder.md)

[`FloatMonoBinder`](Aspid.MVVM.StarterKit.FloatMonoBinder.md) that binds [`timeScale`](https://docs.unity3d.com/ScriptReference/Time-timeScale.html).

 [TimeSpanArithmeticConverter](Aspid.MVVM.StarterKit.TimeSpanArithmeticConverter.md)

Applies arithmetic to a duration.

 [TimeSpanConverterAsset](Aspid.MVVM.StarterKit.TimeSpanConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) values.

 [TimeSpanFormatConverter](Aspid.MVVM.StarterKit.TimeSpanFormatConverter.md)

Formats a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) with a real [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) format string.

 [TimeSpanToFloatConverterAsset](Aspid.MVVM.StarterKit.TimeSpanToFloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

 [TimeSpanToNumberConverter](Aspid.MVVM.StarterKit.TimeSpanToNumberConverter.md)

Measures a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) as a number.

 [TimeSpanToStringCasterMonoBinder](Aspid.MVVM.StarterKit.TimeSpanToStringCasterMonoBinder.md)

[`ValueToStringCasterMonoBinder<T>`](Aspid.MVVM.StarterKit.ValueToStringCasterMonoBinder-1.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan).

 [TimeSpanToStringConverterAsset](Aspid.MVVM.StarterKit.TimeSpanToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [TimeUntilConverter](Aspid.MVVM.StarterKit.TimeUntilConverter.md)

Measures how long there is until a moment.

 [ToCultureStringExtensions](Aspid.MVVM.StarterKit.ToCultureStringExtensions.md)

Provides extension methods for [`CultureInfoMode`](Aspid.MVVM.StarterKit.CultureInfoMode.md).

 [ToggleCommandBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ToggleCommandBinder-2.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with
the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ToggleCommandBinder-2.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_2_Param1), [`ToggleCommandBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ToggleCommandBinder-2.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_2_Param2).

 [ToggleCommandBinder\<T\>](Aspid.MVVM.StarterKit.ToggleCommandBinder-1.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with
the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.ToggleCommandBinder-1.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_1_Param).

 [ToggleCommandBinder](Aspid.MVVM.StarterKit.ToggleCommandBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with
the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html).

 [ToggleCommandBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ToggleCommandBinder-3.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with
the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ToggleCommandBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_3_Param1), [`ToggleCommandBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ToggleCommandBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_3_Param2),
[`ToggleCommandBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ToggleCommandBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandBinder_3_Param3).

 [ToggleCommandMonoBinder](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html).

 [ToggleCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-3.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_3_Param1),
[`ToggleCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_3_Param2), [`ToggleCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_3_Param3).

 [ToggleCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-2.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_2_Param1),
[`ToggleCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_2_Param2).

 [ToggleCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Toggle-onValueChanged.html) with the new [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and [`ToggleCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.ToggleCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_ToggleCommandMonoBinder_1_Param).

 [ToggleGroupAllowSwitchOffBinder](Aspid.MVVM.StarterKit.ToggleGroupAllowSwitchOffBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`allowSwitchOff`](https://docs.unity3d.com/ScriptReference/UI-ToggleGroup-allowSwitchOff.html).

 [ToggleGroupAllowSwitchOffMonoBinder](Aspid.MVVM.StarterKit.ToggleGroupAllowSwitchOffMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`allowSwitchOff`](https://docs.unity3d.com/ScriptReference/UI-ToggleGroup-allowSwitchOff.html).

 [ToggleIsOnBinder](Aspid.MVVM.StarterKit.ToggleIsOnBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and reports user changes back.

 [ToggleIsOnEnumGroupMonoBinder](Aspid.MVVM.StarterKit.ToggleIsOnEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) on each element.

 [ToggleIsOnEnumMonoBinder](Aspid.MVVM.StarterKit.ToggleIsOnEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html).

 [ToggleIsOnMonoBinder](Aspid.MVVM.StarterKit.ToggleIsOnMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`isOn`](https://docs.unity3d.com/ScriptReference/UI-Toggle-isOn.html) and reports user changes
back.

 [ToggleToSourceMonoBinder](Aspid.MVVM.StarterKit.ToggleToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Toggle`](https://docs.unity3d.com/ScriptReference/UI-Toggle.html).

 [TransformEulerAnglesBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

 [TransformEulerAnglesCombineConverter](Aspid.MVVM.StarterKit.TransformEulerAnglesCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)'s Euler angles.

 [TransformEulerAnglesEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html) on each element.

 [TransformEulerAnglesEnumMonoBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

 [TransformEulerAnglesMonoBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

 [TransformEulerAnglesSwitcherBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

 [TransformEulerAnglesSwitcherMonoBinder](Aspid.MVVM.StarterKit.TransformEulerAnglesSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html) or
[`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

 [TransformGettersAndSetters](Aspid.MVVM.StarterKit.TransformGettersAndSetters.md)

Provides extension methods for getting and setting position, rotation, and euler angles on a [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)
in either world or local space.

 [TransformParentBinder](Aspid.MVVM.StarterKit.TransformParentBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`parent`](https://docs.unity3d.com/ScriptReference/Transform-parent.html).

 [TransformParentMonoBinder](Aspid.MVVM.StarterKit.TransformParentMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`parent`](https://docs.unity3d.com/ScriptReference/Transform-parent.html).

 [TransformPosition2DCombineConverter](Aspid.MVVM.StarterKit.TransformPosition2DCombineConverter.md)

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)'s current position, dropping its depth.

 [TransformPositionBinder](Aspid.MVVM.StarterKit.TransformPositionBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

 [TransformPositionCombineConverter](Aspid.MVVM.StarterKit.TransformPositionCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)'s current position.

 [TransformPositionEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TransformPositionEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html) on each element.

 [TransformPositionEnumMonoBinder](Aspid.MVVM.StarterKit.TransformPositionEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

 [TransformPositionMonoBinder](Aspid.MVVM.StarterKit.TransformPositionMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

 [TransformPositionSwitcherBinder](Aspid.MVVM.StarterKit.TransformPositionSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

 [TransformPositionSwitcherMonoBinder](Aspid.MVVM.StarterKit.TransformPositionSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html) or
[`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

 [TransformRotationBinder](Aspid.MVVM.StarterKit.TransformRotationBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

 [TransformRotationEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TransformRotationEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html) on each element.

 [TransformRotationEnumMonoBinder](Aspid.MVVM.StarterKit.TransformRotationEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

 [TransformRotationMonoBinder](Aspid.MVVM.StarterKit.TransformRotationMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

 [TransformRotationSwitcherBinder](Aspid.MVVM.StarterKit.TransformRotationSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

 [TransformRotationSwitcherMonoBinder](Aspid.MVVM.StarterKit.TransformRotationSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) or
[`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

 [TransformScaleBinder](Aspid.MVVM.StarterKit.TransformScaleBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html),
as a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) or a single number applied to all three axes.

 [TransformScaleCombineConverter](Aspid.MVVM.StarterKit.TransformScaleCombineConverter.md)

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)'s local scale.

 [TransformScaleEnumGroupMonoBinder](Aspid.MVVM.StarterKit.TransformScaleEnumGroupMonoBinder.md)

[`EnumGroupMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-2.md) that sets [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html)
on each element.

 [TransformScaleEnumMonoBinder](Aspid.MVVM.StarterKit.TransformScaleEnumMonoBinder.md)

[`EnumMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.EnumMonoBinder-2.md) that sets [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html).

 [TransformScaleMonoBinder](Aspid.MVVM.StarterKit.TransformScaleMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html),
as a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) or a single number applied to all three axes.

 [TransformScaleSwitcherBinder](Aspid.MVVM.StarterKit.TransformScaleSwitcherBinder.md)

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html).

 [TransformScaleSwitcherMonoBinder](Aspid.MVVM.StarterKit.TransformScaleSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-2.md) that switches [`localScale`](https://docs.unity3d.com/ScriptReference/Transform-localScale.html).

 [TransformSiblingIndexBinder](Aspid.MVVM.StarterKit.TransformSiblingIndexBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds the sibling index of a [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html).

 [TransformSiblingIndexMonoBinder](Aspid.MVVM.StarterKit.TransformSiblingIndexMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds the sibling index of a [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html).

 [TransformToSourceMonoBinder](Aspid.MVVM.StarterKit.TransformToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html).

 [TrimStringConverter](Aspid.MVVM.StarterKit.TrimStringConverter.md)

Removes surrounding characters from a string.

 [TruncateStringConverter](Aspid.MVVM.StarterKit.TruncateStringConverter.md)

Shortens a string that is too long to fit.

 [TweenColorMonoBinder](Aspid.MVVM.StarterKit.TweenColorMonoBinder.md)

[`TweenMonoBinder<T>`](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md) that eases a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html).

 [TweenFloatMonoBinder](Aspid.MVVM.StarterKit.TweenFloatMonoBinder.md)

[`TweenMonoBinder<T>`](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md) that eases a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a>.

 [TweenMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md)

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that eases toward each received value and forwards every intermediate value
to a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html).

 [TweenVector3MonoBinder](Aspid.MVVM.StarterKit.TweenVector3MonoBinder.md)

[`TweenMonoBinder<T>`](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md) that eases a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html).

 [TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md)

Abstract base [`NumberConverter`](Aspid.MVVM.StarterKit.NumberConverter.md) that also converts back within the same numeric type.

 [UnaryMathConverter](Aspid.MVVM.StarterKit.UnaryMathConverter.md)

Applies a single-argument mathematical function.

 [UnityEventBoolByBindMonoBinder](Aspid.MVVM.StarterKit.UnityEventBoolByBindMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with whether a binding exists.

 [UnityEventBoolMonoBinder](Aspid.MVVM.StarterKit.UnityEventBoolMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>.

 [UnityEventColorMonoBinder](Aspid.MVVM.StarterKit.UnityEventColorMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound [`Color`](https://docs.unity3d.com/ScriptReference/Color.html).

 [UnityEventDoubleMonoBinder](Aspid.MVVM.StarterKit.UnityEventDoubleMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a>.

 [UnityEventEnumMonoBinder](Aspid.MVVM.StarterKit.UnityEventEnumMonoBinder.md)

[`EnumMonoBinder<T>`](Aspid.MVVM.StarterKit.EnumMonoBinder-1.md) that invokes the [`UnityEvent`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) mapped to the bound enum value.

 [UnityEventFloatMonoBinder](Aspid.MVVM.StarterKit.UnityEventFloatMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a>.

 [UnityEventIntMonoBinder](Aspid.MVVM.StarterKit.UnityEventIntMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>.

 [UnityEventLongMonoBinder](Aspid.MVVM.StarterKit.UnityEventLongMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>.

 [UnityEventNumberConditionMonoBinder](Aspid.MVVM.StarterKit.UnityEventNumberConditionMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that turns the bound number into a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> through a converter and
invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with it.

 [UnityEventNumberConditionSwitcherMonoBinder](Aspid.MVVM.StarterKit.UnityEventNumberConditionSwitcherMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that turns the bound number into a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> through a converter and
invokes one of two [`UnityEvent`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html)s.

 [UnityEventQuaternionMonoBinder](Aspid.MVVM.StarterKit.UnityEventQuaternionMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html).

 [UnityEventStringMonoBinder](Aspid.MVVM.StarterKit.UnityEventStringMonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound value as a string.

 [UnityEventSwitcherMonoBinder](Aspid.MVVM.StarterKit.UnityEventSwitcherMonoBinder.md)

[`SwitcherMonoBinder<T>`](Aspid.MVVM.StarterKit.SwitcherMonoBinder-1.md) that invokes one of two [`UnityEvent`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html)s by the bound
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>.

 [UnityEventVector2MonoBinder](Aspid.MVVM.StarterKit.UnityEventVector2MonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html).

 [UnityEventVector3MonoBinder](Aspid.MVVM.StarterKit.UnityEventVector3MonoBinder.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that invokes a [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html) with the bound [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html).

 [UnityObjectToBoolConverterAsset](Aspid.MVVM.StarterKit.UnityObjectToBoolConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

 [UnityObjectToStringConverterAsset](Aspid.MVVM.StarterKit.UnityObjectToStringConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

 [UnixTimestampToDateTimeConverter](Aspid.MVVM.StarterKit.UnixTimestampToDateTimeConverter.md)

Converts a Unix timestamp to a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime).

 [ValueOneTimeBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneTimeBinder-1.md)

[`ValueOneWayBinder<T>`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md) fixed to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md): accepts a ViewModel value once.

 [ValueOneWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that stores the latest ViewModel value and raises [`ValueOneWayBinder<T>.Changed`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Changed).

 [ValueOneWayToSourceBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneWayToSourceBinder-1.md)

[`ValueTwoWayBinder<T>`](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md) fixed to [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md): pushes the current value to the ViewModel on binding.

 [ValueToStringCasterBinder\<T\>](Aspid.MVVM.StarterKit.ValueToStringCasterBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that converts a <code class="typeparamref">T</code> value to a [`String`](https://learn.microsoft.com/dotnet/api/system.string)
and forwards it to a target setter.

 [ValueToStringCasterMonoBinder\<T\>](Aspid.MVVM.StarterKit.ValueToStringCasterMonoBinder-1.md)

Abstract base [`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from <code class="typeparamref">T</code> to [`String`](https://learn.microsoft.com/dotnet/api/system.string).
Defaults to [`ValueToStringConverter<T>`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md). Close over a concrete type to make it addable as a component.

 [ValueToStringConverter\<T\>](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md)

Writes a value as text, with optional formatting.

 [ValueTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that stores a value
and synchronizes it in both directions. Supports every binding mode; in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md),
the current value is pushed to the ViewModel on binding.

 [ValueViewModel\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.ValueViewModel-4.md)

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds four independent bindable values of types <code class="typeparamref">T1</code>, <code class="typeparamref">T2</code>, <code class="typeparamref">T3</code>, and <code class="typeparamref">T4</code>.

 [ValueViewModel\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ValueViewModel-3.md)

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds three independent bindable values of types <code class="typeparamref">T1</code>, <code class="typeparamref">T2</code>, and <code class="typeparamref">T3</code>.

 [ValueViewModel\<T1, T2\>](Aspid.MVVM.StarterKit.ValueViewModel-2.md)

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds two independent bindable values of types <code class="typeparamref">T1</code> and <code class="typeparamref">T2</code>.

 [ValueViewModel\<T\>](Aspid.MVVM.StarterKit.ValueViewModel-1.md)

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds a single bindable value of type <code class="typeparamref">T</code>.

 [Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md)

Base class for converters that combine a bound 2D vector with one read from a scene
component, taking each axis from one side or the other.

 [Vector2ConverterAsset](Aspid.MVVM.StarterKit.Vector2ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) values.

 [Vector2ToFloatConverterAsset](Aspid.MVVM.StarterKit.Vector2ToFloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

 [Vector2ToVector2IntConverterAsset](Aspid.MVVM.StarterKit.Vector2ToVector2IntConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to [`Vector2Int`](https://docs.unity3d.com/ScriptReference/Vector2Int.html) conversions.

 [Vector2ToVector3CasterMonoBinder](Aspid.MVVM.StarterKit.Vector2ToVector3CasterMonoBinder.md)

[`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html). Defaults to [`Vector2Vector3Converter`](Aspid.MVVM.StarterKit.Vector2Vector3Converter.md).

 [Vector2ToVector3ConverterAsset](Aspid.MVVM.StarterKit.Vector2ToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [Vector2Vector3Converter](Aspid.MVVM.StarterKit.Vector2Vector3Converter.md)

Maps a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html)'s components onto two axes of a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html), filling
the third with a constant, and reads the same two back.

 [Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md)

Base class for converters that combine a bound vector with one read from a scene component,
taking each axis from whichever of the two the configured [`Mode`](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md) names.

 [Vector3ConverterAsset](Aspid.MVVM.StarterKit.Vector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) values.

 [Vector3ToFloatConverterAsset](Aspid.MVVM.StarterKit.Vector3ToFloatConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

 [Vector3ToQuaternionConverterAsset](Aspid.MVVM.StarterKit.Vector3ToQuaternionConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) conversions.

 [Vector3ToVector2CasterMonoBinder](Aspid.MVVM.StarterKit.Vector3ToVector2CasterMonoBinder.md)

[`CasterMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md) from [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html). Defaults to [`Vector2Vector3Converter`](Aspid.MVVM.StarterKit.Vector2Vector3Converter.md).

 [Vector3ToVector2ConverterAsset](Aspid.MVVM.StarterKit.Vector3ToVector2ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) conversions.

 [Vector3ToVector3IntConverterAsset](Aspid.MVVM.StarterKit.Vector3ToVector3IntConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Vector3Int`](https://docs.unity3d.com/ScriptReference/Vector3Int.html) conversions.

 [Vector3ToVector4ConverterAsset](Aspid.MVVM.StarterKit.Vector3ToVector4ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

 [Vector3Vector4Converter](Aspid.MVVM.StarterKit.Vector3Vector4Converter.md)

Widens a vector to four components, and narrows one back by dropping a component.

 [Vector4ConverterAsset](Aspid.MVVM.StarterKit.Vector4ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) values.

 [Vector4ToColorConverterAsset](Aspid.MVVM.StarterKit.Vector4ToColorConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) conversions.

 [Vector4ToQuaternionConverterAsset](Aspid.MVVM.StarterKit.Vector4ToQuaternionConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) conversions.

 [Vector4ToRectConverterAsset](Aspid.MVVM.StarterKit.Vector4ToRectConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`Rect`](https://docs.unity3d.com/ScriptReference/Rect.html) conversions.

 [Vector4ToRectOffsetConverter](Aspid.MVVM.StarterKit.Vector4ToRectOffsetConverter.md)

Turns the four numbers of a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) into a padding.

 [Vector4ToRectOffsetConverterAsset](Aspid.MVVM.StarterKit.Vector4ToRectOffsetConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) conversions.

 [Vector4ToVector3ConverterAsset](Aspid.MVVM.StarterKit.Vector4ToVector3ConverterAsset.md)

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

 [VectorArithmeticConverter](Aspid.MVVM.StarterKit.VectorArithmeticConverter.md)

Combines a bound vector with an authored one.

 [VectorClampComponentsConverter](Aspid.MVVM.StarterKit.VectorClampComponentsConverter.md)

Keeps every axis of a vector between two bounds.

 [VectorClampMagnitudeConverter](Aspid.MVVM.StarterKit.VectorClampMagnitudeConverter.md)

Keeps a vector inside a length.

 [VectorDistanceConverter](Aspid.MVVM.StarterKit.VectorDistanceConverter.md)

Measures how far a position is from a target.

 [VectorLerpConverter](Aspid.MVVM.StarterKit.VectorLerpConverter.md)

Moves between two vectors by a 0..1 amount.

 [VectorNormalizeConverter](Aspid.MVVM.StarterKit.VectorNormalizeConverter.md)

Reduces a vector to its direction.

 [VectorRoundConverter](Aspid.MVVM.StarterKit.VectorRoundConverter.md)

Rounds every axis of a vector.

 [VectorSwizzleConverter](Aspid.MVVM.StarterKit.VectorSwizzleConverter.md)

Reorders the components of a vector.

 [VectorToFloatConverter](Aspid.MVVM.StarterKit.VectorToFloatConverter.md)

Measures one number out of a vector.

 [VectorToVectorIntConverter](Aspid.MVVM.StarterKit.VectorToVectorIntConverter.md)

Converts a vector to its integer form.

 [VideoPlayerClipBinder](Aspid.MVVM.StarterKit.VideoPlayerClipBinder.md)

[`TargetObjectBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetObjectBinder-2.md) that binds [`clip`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-clip.html).

 [VideoPlayerClipMonoBinder](Aspid.MVVM.StarterKit.VideoPlayerClipMonoBinder.md)

[`ComponentObjectMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md) that binds [`clip`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-clip.html).

 [VideoPlayerIsLoopingBinder](Aspid.MVVM.StarterKit.VideoPlayerIsLoopingBinder.md)

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`isLooping`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-isLooping.html).

 [VideoPlayerIsLoopingMonoBinder](Aspid.MVVM.StarterKit.VideoPlayerIsLoopingMonoBinder.md)

[`ComponentMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds [`isLooping`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-isLooping.html).

 [VideoPlayerPlaybackSpeedBinder](Aspid.MVVM.StarterKit.VideoPlayerPlaybackSpeedBinder.md)

[`TargetFloatBinder<T>`](Aspid.MVVM.StarterKit.TargetFloatBinder-1.md) that binds [`playbackSpeed`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-playbackSpeed.html).

 [VideoPlayerPlaybackSpeedMonoBinder](Aspid.MVVM.StarterKit.VideoPlayerPlaybackSpeedMonoBinder.md)

[`ComponentFloatMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentFloatMonoBinder-1.md) that binds [`playbackSpeed`](https://docs.unity3d.com/ScriptReference/Video-VideoPlayer-playbackSpeed.html).

 [ViewInitializeComponent](Aspid.MVVM.StarterKit.ViewInitializeComponent.md)

[`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) that resolves an [`IView`](Aspid.MVVM.IView.md).

 [ViewInitializer](Aspid.MVVM.StarterKit.ViewInitializer.md)

[`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) that resolves its ViewModel from a serialized slot and
initializes the views at the chosen lifecycle stage.

 [ViewInitializerBase](Aspid.MVVM.StarterKit.ViewInitializerBase.md)

Abstract base [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) that resolves a set of views and initializes them with a ViewModel.

 [ViewInitializerManual](Aspid.MVVM.StarterKit.ViewInitializerManual.md)

[`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) that takes its ViewModel from an explicit [`ViewInitializerManual.Initialize`](Aspid.MVVM.StarterKit.ViewInitializerManual.md#Aspid_MVVM_StarterKit_ViewInitializerManual_Initialize_Aspid_MVVM_IViewModel_) call.

 [ViewModelInitializeComponent](Aspid.MVVM.StarterKit.ViewModelInitializeComponent.md)

[`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) that resolves an [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [VirtualizedList](Aspid.MVVM.StarterKit.VirtualizedList.md)

[`ScrollRect`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect.html) that shows a list of ViewModels through a fixed set of recycled views,
instantiating only as many as fit the viewport.

 [VirtualizedListItemSourceBinder](Aspid.MVVM.StarterKit.VirtualizedListItemSourceBinder.md)

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that sets [`VirtualizedList.ItemsSource`](Aspid.MVVM.StarterKit.VirtualizedList.md#Aspid_MVVM_StarterKit_VirtualizedList_ItemsSource)
to the bound list, optionally filtered and sorted.

 [VirtualizedListItemSourceMonoBinder](Aspid.MVVM.StarterKit.VirtualizedListItemSourceMonoBinder.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that sets [`VirtualizedList.ItemsSource`](Aspid.MVVM.StarterKit.VirtualizedList.md#Aspid_MVVM_StarterKit_VirtualizedList_ItemsSource)
to the bound list, optionally filtered and sorted.

 [VirtualizedListToSourceMonoBinder](Aspid.MVVM.StarterKit.VirtualizedListToSourceMonoBinder.md)

[`ComponentToSourceMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentToSourceMonoBinder-1.md) for [`VirtualizedList`](Aspid.MVVM.StarterKit.VirtualizedList.md).

 [VisualElementMonoBinder\<TElement\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md)

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that resolves one <code class="typeparamref">TElement</code> in a
[`UIDocument`](https://docs.unity3d.com/ScriptReference/UIElements-UIDocument.html) by name or USS class.

 [WrapNumberConverter](Aspid.MVVM.StarterKit.WrapNumberConverter.md)

Folds a number back into a range instead of clamping it.

### Structs

 [ColorStop](Aspid.MVVM.StarterKit.ColorStop.md)

One color of a threshold color scale.

 [ConverterFallback\<T\>](Aspid.MVVM.StarterKit.ConverterFallback-1.md)

What a converter does with a value it cannot convert, and what it returns instead.

 [LookupEntry\<TKey, TValue\>](Aspid.MVVM.StarterKit.LookupEntry-2.md)

One row of a [`DictionaryLookupConverter<T1, T2>`](Aspid.MVVM.StarterKit.DictionaryLookupConverter-2.md) table.

 [NumberReverseChannel](Aspid.MVVM.StarterKit.NumberReverseChannel.md)

Holds the subscriptions behind the four numeric events of an [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md)
and raises them together.

 [EnumToDropdownOptionDataConverter.OptionEntry](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverter.OptionEntry.md)

The label and icon authored for one enum member.

 [PoolSettings](Aspid.MVVM.StarterKit.PoolSettings.md)

Size limits of a [`PrefabViewPool<T>`](Aspid.MVVM.StarterKit.PrefabViewPool-1.md).

 [ShaderPropertyId](Aspid.MVVM.StarterKit.ShaderPropertyId.md)

Caches the id a shader property name resolves to.

 [SpriteMapEntry](Aspid.MVVM.StarterKit.SpriteMapEntry.md)

One key of a [`StringToSpriteConverter`](Aspid.MVVM.StarterKit.StringToSpriteConverter.md) map, with the sprite it names.

### Interfaces

 [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

Reacts to a command's <code>CanExecute</code> state on behalf of a command binder whose interactable mode is <code>Custom</code>.

 [ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)

Serializable filter for collection binders.

 [ICollectionOrder\<T\>](Aspid.MVVM.StarterKit.ICollectionOrder-1.md)

Serializable sort order for collection binders.

 [IColorBinder](Aspid.MVVM.StarterKit.IColorBinder.md)

[`MVVM.IBinder<T>?text=IBinder%3cColor%3e`](Aspid.MVVM.md) that also accepts an HTML color string such as <code>#FF0000</code> or <code>red</code>.

 [IComponentInitializable](Aspid.MVVM.StarterKit.IComponentInitializable.md)

A View or ViewModel that needs a setup call after a [`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) resolves it.

 [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

Marks a type as a converter.

 [IConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.IConverter-2.md)

Converts a value of type <code class="typeparamref">TFrom</code> into a value of type <code class="typeparamref">TTo</code>.

 [IDoubleBinder](Aspid.MVVM.StarterKit.IDoubleBinder.md)

[`INumberBinder`](Aspid.MVVM.StarterKit.INumberBinder.md) whose implementors bind a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a>; every other numeric type widens to it.

 [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)

Provides non-generic access to a property stored by a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

 [IDynamicProperty\<T\>](Aspid.MVVM.StarterKit.IDynamicProperty-1.md)

Provides typed access to a property stored by a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

 [IFloatBinder](Aspid.MVVM.StarterKit.IFloatBinder.md)

[`INumberBinder`](Aspid.MVVM.StarterKit.INumberBinder.md) whose implementors bind a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a>; a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> saturates at the bounds.

 [IIntBinder](Aspid.MVVM.StarterKit.IIntBinder.md)

[`INumberBinder`](Aspid.MVVM.StarterKit.INumberBinder.md) whose implementors bind an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>; wider types saturate at the bounds instead of wrapping.

 [ILongBinder](Aspid.MVVM.StarterKit.ILongBinder.md)

[`INumberBinder`](Aspid.MVVM.StarterKit.INumberBinder.md) whose implementors bind a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>; wider types saturate at the bounds instead of wrapping.

 [INumberBinder](Aspid.MVVM.StarterKit.INumberBinder.md)

Composite [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that accepts every common numeric primitive.
Implementors provide only the [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32), [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64), [`Single`](https://learn.microsoft.com/dotnet/api/system.single) and [`Double`](https://learn.microsoft.com/dotnet/api/system.double)
overloads; the rest are routed to them here.

 [INumberReverseBinder](Aspid.MVVM.StarterKit.INumberReverseBinder.md)

Composite [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that reports a numeric View value to the ViewModel as
[`Int32`](https://learn.microsoft.com/dotnet/api/system.int32), [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64), [`Single`](https://learn.microsoft.com/dotnet/api/system.single) or [`Double`](https://learn.microsoft.com/dotnet/api/system.double).
Implementors provide only [`INumberReverseBinder.Channel`](Aspid.MVVM.StarterKit.INumberReverseBinder.md#Aspid_MVVM_StarterKit_INumberReverseBinder_Channel); the four events are bridged to it here.

 [IRotationBinder](Aspid.MVVM.StarterKit.IRotationBinder.md)

[`IVectorBinder`](Aspid.MVVM.StarterKit.IVectorBinder.md) whose implementors bind a [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html): vectors are read as Euler angles,
a scalar as the same angle on all three axes.

 [IRotationReverseBinder](Aspid.MVVM.StarterKit.IRotationReverseBinder.md)

[`IVectorReverseBinder`](Aspid.MVVM.StarterKit.IVectorReverseBinder.md) that also reports a [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html).

 [ITwoWayConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md)

Converts values back as well, for the trip from the View to the ViewModel.

 [IVector2Binder](Aspid.MVVM.StarterKit.IVector2Binder.md)

[`IVectorBinder`](Aspid.MVVM.StarterKit.IVectorBinder.md) whose implementors bind a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html): a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) is
accepted by dropping its Z component, and a scalar is applied to both components.

 [IVector3Binder](Aspid.MVVM.StarterKit.IVector3Binder.md)

[`IVectorBinder`](Aspid.MVVM.StarterKit.IVectorBinder.md) whose implementors bind a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html): a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) is
promoted with Z set to zero, and a scalar is applied to all three components.

 [IVectorBinder](Aspid.MVVM.StarterKit.IVectorBinder.md)

[`IFloatBinder`](Aspid.MVVM.StarterKit.IFloatBinder.md) that also accepts [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) and [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) values,
applying a scalar to every component.

 [IVectorReverseBinder](Aspid.MVVM.StarterKit.IVectorReverseBinder.md)

Reverse binder that reports a vector to both [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) and [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) members of the ViewModel.

 [IViewFactory\<T1, T2, TView\>](Aspid.MVVM.StarterKit.IViewFactory-3.md)

Creates views for a ViewModel with two extra arguments. Keyed creation ignores the key.

 [IViewFactory\<T1, T2, T3, TView\>](Aspid.MVVM.StarterKit.IViewFactory-4.md)

Creates views for a ViewModel with three extra arguments. Keyed creation ignores the key.

 [IViewFactory\<T, TView\>](Aspid.MVVM.StarterKit.IViewFactory-2.md)

Creates views for a ViewModel with one extra argument. Keyed creation ignores the key.

 [IViewFactory\<TView\>](Aspid.MVVM.StarterKit.IViewFactory-1.md)

Creates views for a ViewModel. Keyed creation ignores the key.

 [IViewFactoryRelease\<T\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)

Returns views produced by a factory back to it.

 [IViewFactoryWithKey\<T, TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-2.md)

Creates views for a ViewModel identified by a key, with one extra argument.

 [IViewFactoryWithKey\<T1, T2, T3, TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-4.md)

Creates views for a ViewModel identified by a key, with three extra arguments.

 [IViewFactoryWithKey\<T1, T2, TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-3.md)

Creates views for a ViewModel identified by a key, with two extra arguments.

 [IViewFactoryWithKey\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-1.md)

Creates views for a ViewModel identified by a key.

### Enums

 [AggregateOperation](Aspid.MVVM.StarterKit.AggregateOperation.md)

What [`CollectionAggregateConverter`](Aspid.MVVM.StarterKit.CollectionAggregateConverter.md) computes.

 [AlphaMode](Aspid.MVVM.StarterKit.AlphaMode.md)

How [`ColorAlphaConverter`](Aspid.MVVM.StarterKit.ColorAlphaConverter.md) applies its alpha.

 [AngleRange](Aspid.MVVM.StarterKit.AngleRange.md)

The range [`AngleWrapConverter`](Aspid.MVVM.StarterKit.AngleWrapConverter.md) reports angles in.

 [AudioSourceDistanceMode](Aspid.MVVM.StarterKit.AudioSourceDistanceMode.md)

Specifies which [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html) distances a bound [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) writes.

 [AxisMask](Aspid.MVVM.StarterKit.AxisMask.md)

Which axes a converter writes a number into.

 [BoundsPlane](Aspid.MVVM.StarterKit.BoundsPlane.md)

Which pair of axes a bounding box is flattened onto.

 [BoundsVector](Aspid.MVVM.StarterKit.BoundsVector.md)

Which vector of a bounding box [`BoundsToVectorConverter`](Aspid.MVVM.StarterKit.BoundsToVectorConverter.md) reads.

 [ChannelOperation](Aspid.MVVM.StarterKit.ChannelOperation.md)

What [`ColorChannelConverter`](Aspid.MVVM.StarterKit.ColorChannelConverter.md) does to each channel it writes.

 [ClampMode](Aspid.MVVM.StarterKit.ClampMode.md)

Which bound [`ClampNumberConverter`](Aspid.MVVM.StarterKit.ClampNumberConverter.md) applies.

 [ColorBlendMode](Aspid.MVVM.StarterKit.ColorBlendMode.md)

How [`ColorTintConverter`](Aspid.MVVM.StarterKit.ColorTintConverter.md) and [`ColorBlockTintConverter`](Aspid.MVVM.StarterKit.ColorBlockTintConverter.md) combine two colors.

 [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

Channels of a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) a binder or converter writes.

 [ComparisonMode](Aspid.MVVM.StarterKit.ComparisonMode.md)

How a converter compares the bound value with the one it is configured with.

 [ConverterFailureMode](Aspid.MVVM.StarterKit.ConverterFailureMode.md)

What a converter does with a value it cannot convert.

 [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

Which culture a value is formatted and parsed with.

 [EaseType](Aspid.MVVM.StarterKit.EaseType.md)

The easing curve [`EasingConverter`](Aspid.MVVM.StarterKit.EasingConverter.md) applies.

 [EnumMaskOperation](Aspid.MVVM.StarterKit.EnumMaskOperation.md)

What [`EnumMaskConverter<T>`](Aspid.MVVM.StarterKit.EnumMaskConverter-1.md) does with the flags it is given.

 [EnumMatchMode](Aspid.MVVM.StarterKit.EnumMatchMode.md)

How [`EnumMatchConverter<T>`](Aspid.MVVM.StarterKit.EnumMatchConverter-1.md) tests a bound enum value.

 [EnumNameSource](Aspid.MVVM.StarterKit.EnumNameSource.md)

Where the text naming an enum member comes from.

 [IndexOutOfRangeMode](Aspid.MVVM.StarterKit.IndexOutOfRangeMode.md)

What [`IndexToValueConverter<T>`](Aspid.MVVM.StarterKit.IndexToValueConverter-1.md) does with an index outside the array.

 [InteractableMode](Aspid.MVVM.StarterKit.InteractableMode.md)

How a command binder reflects the command's <code>CanExecute</code> state on its target.

 [LineRendererColorMode](Aspid.MVVM.StarterKit.LineRendererColorMode.md)

Specifies which end colors of a [`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html) a bound color writes.

 [LogicOperation](Aspid.MVVM.StarterKit.LogicOperation.md)

The boolean operations [`BoolLogicConverter`](Aspid.MVVM.StarterKit.BoolLogicConverter.md) can apply.

 [Vector3CombineConverter.Mode](Aspid.MVVM.StarterKit.Vector3CombineConverter.Mode.md)

Specifies which components to take from the first vector when combining.

 [Vector2Vector3Converter.Mode](Aspid.MVVM.StarterKit.Vector2Vector3Converter.Mode.md)

Specifies which components of the 2D vector to map to the 3D vector. The letters name the
destination axes, in the order the 2D components are read.

 [Vector2CombineConverter.Mode](Aspid.MVVM.StarterKit.Vector2CombineConverter.Mode.md)

Specifies which components to take from the first vector when combining.

 [NumberOperation](Aspid.MVVM.StarterKit.NumberOperation.md)

The arithmetic [`ArithmeticNumberConverter`](Aspid.MVVM.StarterKit.ArithmeticNumberConverter.md) can apply.

 [NumberWrapMode](Aspid.MVVM.StarterKit.NumberWrapMode.md)

How [`WrapNumberConverter`](Aspid.MVVM.StarterKit.WrapNumberConverter.md) folds a value back into its range.

 [OffsetSource](Aspid.MVVM.StarterKit.OffsetSource.md)

The offset [`DateTimeOffsetFormatConverter`](Aspid.MVVM.StarterKit.DateTimeOffsetFormatConverter.md) shows a moment at.

 [OverflowMode](Aspid.MVVM.StarterKit.OverflowMode.md)

What [`NumericCastConverter`](Aspid.MVVM.StarterKit.NumericCastConverter.md) does with a number the target type cannot hold.

 [RectSides](Aspid.MVVM.StarterKit.RectSides.md)

Which sides of a [`RectOffset`](https://docs.unity3d.com/ScriptReference/RectOffset.html) a converter writes.

 [ReferenceSource](Aspid.MVVM.StarterKit.ReferenceSource.md)

The moment [`DateTimeCompareConverter`](Aspid.MVVM.StarterKit.DateTimeCompareConverter.md) compares against.

 [ResolveType](Aspid.MVVM.StarterKit.ResolveType.md)

Where an [`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) takes its instance from.

 [RichTextSanitize](Aspid.MVVM.StarterKit.RichTextSanitize.md)

What [`RichTextSanitizeConverter`](Aspid.MVVM.StarterKit.RichTextSanitizeConverter.md) does with markup it will not let through.

 [RotationAxis](Aspid.MVVM.StarterKit.RotationAxis.md)

Which axis a rotation converter turns around.

 [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

How [`RoundNumberConverter`](Aspid.MVVM.StarterKit.RoundNumberConverter.md) drops the fraction.

 [SelectableStates](Aspid.MVVM.StarterKit.SelectableStates.md)

Which states of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) a converter writes.

 [SizeDeltaMode](Aspid.MVVM.StarterKit.SizeDeltaMode.md)

Determines which axes of [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html) are modified when setting the size.

 [SliderRangeMode](Aspid.MVVM.StarterKit.SliderRangeMode.md)

Specifies which [`Slider`](https://docs.unity3d.com/ScriptReference/UI-Slider.html) endpoints a bound [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) writes.

 [StringEmptiness](Aspid.MVVM.StarterKit.StringEmptiness.md)

What [`StringEmptyToBoolConverter`](Aspid.MVVM.StarterKit.StringEmptyToBoolConverter.md) counts as an absent string.

 [StringMatchMode](Aspid.MVVM.StarterKit.StringMatchMode.md)

How [`StringMatchToBoolConverter`](Aspid.MVVM.StarterKit.StringMatchToBoolConverter.md) compares a bound string with the authored one.

 [SymbolPosition](Aspid.MVVM.StarterKit.SymbolPosition.md)

Where [`CurrencyConverter`](Aspid.MVVM.StarterKit.CurrencyConverter.md) puts the symbol.

 [TextCase](Aspid.MVVM.StarterKit.TextCase.md)

The casing [`TextCaseConverter`](Aspid.MVVM.StarterKit.TextCaseConverter.md) applies.

 [TimeLayout](Aspid.MVVM.StarterKit.TimeLayout.md)

The shape [`SecondsToTimeStringConverter`](Aspid.MVVM.StarterKit.SecondsToTimeStringConverter.md) writes a duration in.

 [TimeUnit](Aspid.MVVM.StarterKit.TimeUnit.md)

The unit [`TimeSpanToNumberConverter`](Aspid.MVVM.StarterKit.TimeSpanToNumberConverter.md) measures a duration in.

 [TrimSide](Aspid.MVVM.StarterKit.TrimSide.md)

Which ends [`TrimStringConverter`](Aspid.MVVM.StarterKit.TrimStringConverter.md) trims.

 [TruncateSide](Aspid.MVVM.StarterKit.TruncateSide.md)

Which end [`TruncateStringConverter`](Aspid.MVVM.StarterKit.TruncateStringConverter.md) cuts from.

 [UnaryMathOperation](Aspid.MVVM.StarterKit.UnaryMathOperation.md)

The single-argument functions [`UnaryMathConverter`](Aspid.MVVM.StarterKit.UnaryMathConverter.md) can apply.

 [UpdateInputFieldEvent](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md)

Specifies which `TMP_InputField` event a binder listens to.

 [Vector4Component](Aspid.MVVM.StarterKit.Vector4Component.md)

Which component of a four-component vector a converter reads.

 [VectorComponent](Aspid.MVVM.StarterKit.VectorComponent.md)

What [`VectorToFloatConverter`](Aspid.MVVM.StarterKit.VectorToFloatConverter.md) measures. A narrower vector carries fewer of them.

 [VectorOperation](Aspid.MVVM.StarterKit.VectorOperation.md)

The arithmetic [`VectorArithmeticConverter`](Aspid.MVVM.StarterKit.VectorArithmeticConverter.md) can apply.


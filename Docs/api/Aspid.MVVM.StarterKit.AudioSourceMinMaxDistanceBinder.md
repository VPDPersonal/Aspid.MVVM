---
title: "Class AudioSourceMinMaxDistanceBinder"
sidebar_label: "AudioSourceMinMaxDistanceBinder"
description: "Class AudioSourceMinMaxDistanceBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioSourceMinMaxDistanceBinder {#Aspid_MVVM_StarterKit_AudioSourceMinMaxDistanceBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html) and
[`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) as a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html), or a single number written to both.

```csharp
[Serializable]
public class AudioSourceMinMaxDistanceBinder : TargetBinder<AudioSource, Vector2>, IRebindableBinder, IBinder<Vector2>, IReverseBinder<Vector2>, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<AudioSource\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<AudioSource, Vector2\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[AudioSourceMinMaxDistanceBinder](Aspid.MVVM.StarterKit.AudioSourceMinMaxDistanceBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Vector2\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Vector2\>](Aspid.MVVM.IReverseBinder-1.md), 
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
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AudioSourceMinMaxDistanceBinder\>\(AudioSourceMinMaxDistanceBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AudioSourceMinMaxDistanceBinder\>\(AudioSourceMinMaxDistanceBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AudioSourceMinMaxDistanceBinder\>\(AudioSourceMinMaxDistanceBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### AudioSourceMinMaxDistanceBinder\(\) {#Aspid_MVVM_StarterKit_AudioSourceMinMaxDistanceBinder__ctor}

```csharp
protected AudioSourceMinMaxDistanceBinder()
```

#### Remarks

For deserialization only.

### AudioSourceMinMaxDistanceBinder\(AudioSource, AudioSourceDistanceMode, IConverter\<Vector2, Vector2\>, BindMode\) {#Aspid_MVVM_StarterKit_AudioSourceMinMaxDistanceBinder__ctor_UnityEngine_AudioSource_Aspid_MVVM_StarterKit_AudioSourceDistanceMode_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector2_UnityEngine_Vector2__Aspid_MVVM_BindMode_}

```csharp
public AudioSourceMinMaxDistanceBinder(AudioSource target, AudioSourceDistanceMode distanceMode = AudioSourceDistanceMode.Range, IConverter<Vector2, Vector2> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` AudioSource

`distanceMode` [AudioSourceDistanceMode](Aspid.MVVM.StarterKit.AudioSourceDistanceMode.md)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector2, Vector2\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Properties

### Property {#Aspid_MVVM_StarterKit_AudioSourceMinMaxDistanceBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Vector2 Property { get; set; }
```

#### Property Value

 Vector2

## Methods

### SetValue\(float\) {#Aspid_MVVM_StarterKit_AudioSourceMinMaxDistanceBinder_SetValue_System_Single_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value received from the ViewModel.


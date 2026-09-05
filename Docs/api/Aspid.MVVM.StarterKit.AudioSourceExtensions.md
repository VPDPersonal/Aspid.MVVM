---
title: "Class AudioSourceExtensions"
sidebar_label: "AudioSourceExtensions"
description: "Class AudioSourceExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioSourceExtensions {#Aspid_MVVM_StarterKit_AudioSourceExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods that write validated values to an [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html).

```csharp
public static class AudioSourceExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AudioSourceExtensions](Aspid.MVVM.StarterKit.AudioSourceExtensions.md)



## Methods

### SetMinMaxDistance\(AudioSource, Vector2, AudioSourceDistanceMode\) {#Aspid_MVVM_StarterKit_AudioSourceExtensions_SetMinMaxDistance_UnityEngine_AudioSource_UnityEngine_Vector2_Aspid_MVVM_StarterKit_AudioSourceDistanceMode_}

Writes [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html), [`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html) or both from <code class="paramref">value</code>.

```csharp
public static void SetMinMaxDistance(this AudioSource audioSource, Vector2 value, AudioSourceDistanceMode mode)
```

#### Parameters

`audioSource` AudioSource

The source whose distances are set.

`value` Vector2

The distances; [`x`](https://docs.unity3d.com/ScriptReference/Vector2-x.html) is the minimum, [`y`](https://docs.unity3d.com/ScriptReference/Vector2-y.html) the maximum.

`mode` [AudioSourceDistanceMode](Aspid.MVVM.StarterKit.AudioSourceDistanceMode.md)

Which distances <code class="paramref">value</code> writes.

#### Remarks

Unity validates neither distance: a negative nor inverted pair silences the source. Negative distances are
raised to zero, an inverted pair is swapped, and a non-finite pair is reported and not applied.

### SetTime\(AudioSource, float\) {#Aspid_MVVM_StarterKit_AudioSourceExtensions_SetTime_UnityEngine_AudioSource_System_Single_}

Sets [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html), keeping the position inside the current clip.

```csharp
public static void SetTime(this AudioSource audioSource, float value)
```

#### Parameters

`audioSource` AudioSource

The source whose playback position is set.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The position, in seconds, to seek to.

#### Remarks

Unity logs an error and rewinds for an out-of-range [`time`](https://docs.unity3d.com/ScriptReference/AudioSource-time.html) instead of clamping it.
Without a clip to write is skipped.

### SetTimeSamples\(AudioSource, int\) {#Aspid_MVVM_StarterKit_AudioSourceExtensions_SetTimeSamples_UnityEngine_AudioSource_System_Int32_}

Sets [`timeSamples`](https://docs.unity3d.com/ScriptReference/AudioSource-timeSamples.html), keeping the position inside the current clip.

```csharp
public static void SetTimeSamples(this AudioSource audioSource, int value)
```

#### Parameters

`audioSource` AudioSource

The source whose playback position is set.

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sample index to seek to.

#### Remarks

Without a clip to write is skipped.


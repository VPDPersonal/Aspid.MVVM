---
title: "Enum AudioSourceDistanceMode"
sidebar_label: "AudioSourceDistanceMode"
description: "Enum AudioSourceDistanceMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum AudioSourceDistanceMode {#Aspid_MVVM_StarterKit_AudioSourceDistanceMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html) distances a bound [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) writes.

```csharp
public enum AudioSourceDistanceMode
```


## Fields

`Min = 0` 

Only [`minDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-minDistance.html), from <code>x</code>.



`Max = 1` 

Only [`maxDistance`](https://docs.unity3d.com/ScriptReference/AudioSource-maxDistance.html), from <code>y</code>.



`Range = 2` 

Both distances.




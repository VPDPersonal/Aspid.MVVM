---
title: "Enum EaseType"
sidebar_label: "EaseType"
description: "Enum EaseType — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum EaseType {#Aspid_MVVM_StarterKit_EaseType}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The easing curve [`EasingConverter`](Aspid.MVVM.StarterKit.EasingConverter.md) applies.

```csharp
public enum EaseType
```


## Fields

`Linear = 0` 

No easing: the value passes through unchanged.



`SineIn = 1` 

A quarter sine wave, starting slowly.



`SineOut = 2` 

A quarter sine wave, ending slowly.



`SineInOut = 3` 

A half sine wave: slow at both ends, fastest in the middle.



`QuadIn = 4` 

The square, starting slowly.



`QuadOut = 5` 

The square, ending slowly.



`QuadInOut = 6` 

The square, slow at both ends.



`CubicIn = 7` 

The cube, starting slowly.



`CubicOut = 8` 

The cube, ending slowly.



`CubicInOut = 9` 

The cube, slow at both ends.



`QuartIn = 10` 

The fourth power, starting slowly.



`QuartOut = 11` 

The fourth power, ending slowly.



`QuartInOut = 12` 

The fourth power, slow at both ends.



`QuintIn = 13` 

The fifth power, starting slowly.



`QuintOut = 14` 

The fifth power, ending slowly.



`QuintInOut = 15` 

The fifth power, slow at both ends.



`ExpoIn = 16` 

A doubling curve, starting almost flat.



`ExpoOut = 17` 

A doubling curve, ending almost flat.



`ExpoInOut = 18` 

A doubling curve, almost flat at both ends.



`CircIn = 19` 

A quarter circle, starting slowly and ending vertically.



`CircOut = 20` 

A quarter circle, starting vertically and ending slowly.



`CircInOut = 21` 

A half circle, vertical through the middle.



`BackIn = 22` 

Pulls back below 0 before moving forward.



`BackOut = 23` 

Overshoots past 1 before settling.



`BackInOut = 24` 

Pulls back at the start and overshoots at the end.



`ElasticIn = 25` 

Oscillates with a growing amplitude, then snaps to 1.



`ElasticOut = 26` 

Snaps past 1 and oscillates to a stop.



`ElasticInOut = 27` 

Oscillates at both ends.



`BounceIn = 28` 

Bounces toward the start.



`BounceOut = 29` 

Lands on 1 and bounces to a stop.



`BounceInOut = 30` 

Bounces at both ends.



## Remarks

<code>In</code> starts slowly, <code>Out</code> ends slowly, <code>InOut</code> does both; only Back and Elastic
leave the 0..1 range.


---
title: "Enum TimeLayout"
sidebar_label: "TimeLayout"
description: "Enum TimeLayout — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum TimeLayout {#Aspid_MVVM_StarterKit_TimeLayout}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The shape [`SecondsToTimeStringConverter`](Aspid.MVVM.StarterKit.SecondsToTimeStringConverter.md) writes a duration in.

```csharp
public enum TimeLayout
```


## Fields

`Seconds = 0` 

Seconds only.



`MinutesSeconds = 1` 

mm:ss.



`HoursMinutesSeconds = 2` 

h:mm:ss.



`DaysHoursMinutesSeconds = 3` 

d:hh:mm:ss.



`Auto = 4` 

The shortest layout that fits the value, down to mm:ss.




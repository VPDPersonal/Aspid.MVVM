---
title: "Class SliderExtensions"
sidebar_label: "SliderExtensions"
description: "Class SliderExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SliderExtensions {#Aspid_MVVM_StarterKit_SliderExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`Slider`](https://docs.unity3d.com/ScriptReference/UI-Slider.html) used by the slider binders.

```csharp
public static class SliderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SliderExtensions](Aspid.MVVM.StarterKit.SliderExtensions.md)



## Methods

### SetMinMax\(Slider, Vector2, SliderRangeMode\) {#Aspid_MVVM_StarterKit_SliderExtensions_SetMinMax_UnityEngine_UI_Slider_UnityEngine_Vector2_Aspid_MVVM_StarterKit_SliderRangeMode_}

Writes [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html), [`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html) or both from <code class="paramref">value</code>.

```csharp
public static void SetMinMax(this Slider slider, Vector2 value, SliderRangeMode mode)
```

#### Parameters

`slider` Slider

The slider whose range is set.

`value` Vector2

The range; [`x`](https://docs.unity3d.com/ScriptReference/Vector2-x.html) is the minimum, [`y`](https://docs.unity3d.com/ScriptReference/Vector2-y.html) the maximum.

`mode` [SliderRangeMode](Aspid.MVVM.StarterKit.SliderRangeMode.md)

Which endpoints <code class="paramref">value</code> writes.

#### Remarks

Unity does not keep <code>minValue &lt;= maxValue</code>: an inverted pair is reported and swapped, a non-finite
pair is reported and not applied.


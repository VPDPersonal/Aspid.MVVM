---
title: "Class AudioLinearDecibelConverter"
sidebar_label: "AudioLinearDecibelConverter"
description: "Class AudioLinearDecibelConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AudioLinearDecibelConverter {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a 0..1 slider position to the decibels an [`AudioMixer`](https://docs.unity3d.com/ScriptReference/Audio-AudioMixer.html)
expects, or the other way around.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Linear To Decibel", Tooltip = "Converts a 0..1 slider position to the decibels an AudioMixer expects, or the other way around")]
public sealed class AudioLinearDecibelConverter : ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AudioLinearDecibelConverter](Aspid.MVVM.StarterKit.AudioLinearDecibelConverter.md)

#### Implements

[ITwoWayConverter\<float, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The mixer's attenuation is logarithmic, so the mapping is a log curve rather than a lerp.

## Constructors

### AudioLinearDecibelConverter\(\) {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter__ctor}

```csharp
public AudioLinearDecibelConverter()
```

#### Remarks

Default: slider position to decibels, over -80..0 dB.

### AudioLinearDecibelConverter\(bool\) {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter__ctor_System_Boolean_}

```csharp
public AudioLinearDecibelConverter(bool isInvert)
```

#### Parameters

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, converts decibels to a slider position instead.

### AudioLinearDecibelConverter\(float, float, bool\) {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter__ctor_System_Single_System_Single_System_Boolean_}

```csharp
public AudioLinearDecibelConverter(float minDecibels, float maxDecibels = 0, bool isInvert = false)
```

#### Parameters

`minDecibels` [float](https://learn.microsoft.com/dotnet/api/system.single)

The decibel value silence maps to. It must be below <code class="paramref">maxDecibels</code>; a pair
that is not a range is reported as an error and -80..0 dB is used instead.

`maxDecibels` [float](https://learn.microsoft.com/dotnet/api/system.single)

The decibel value full volume maps to. It must be above <code class="paramref">minDecibels</code>.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, converts decibels to a slider position instead.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter_Convert_System_Single_}

Converts the specified value in the authored direction.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 slider position, or the decibel value when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The decibel value, or the 0..1 slider position when inverted.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_AudioLinearDecibelConverter_ConvertBack_System_Single_}

Converts a value back in the opposite direction.

```csharp
public float ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The decibel value, or the 0..1 slider position when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 slider position, or the decibel value when inverted.


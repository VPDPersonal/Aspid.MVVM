# AudioSource Binders

Binders for every property of the `AudioSource` component.

---

## Overview

Every `AudioSource` property has its own binder, a Switcher variant and MonoBinder wrappers (plain, Enum, EnumGroup).

| Binder | Property | Data type |
|--------|---------|-----------|
| `AudioSourceVolumeBinder` | `volume` | `float` |
| `AudioSourcePitchBinder` | `pitch` | `float` |
| `AudioSourceClipBinder` | `clip` | `AudioClip` |
| `AudioSourceLoopBinder` | `loop` | `bool` |
| `AudioSourceMuteBinder` | `mute` | `bool` |
| `AudioSourcePanStereoBinder` | `panStereo` | `float` |
| `AudioSourceSpatialBlendBinder` | `spatialBlend` | `float` |
| `AudioSourceSpreadBinder` | `spread` | `float` |
| `AudioSourceDopplerLevelBinder` | `dopplerLevel` | `float` |
| `AudioSourcePriorityBinder` | `priority` | `int` |
| `AudioSourceTimeBinder` | `time` | `float` |
| `AudioSourceTimeSamplesBinder` | `timeSamples` | `int` |
| `AudioSourceReverbZoneMixBinder` | `reverbZoneMix` | `float` |
| `AudioSourceMinMaxDistanceBinder` | `minDistance` / `maxDistance` | `Vector2` |
| `AudioSourceBypassEffectsBinder` | `bypassEffects` | `bool` |
| `AudioSourceBypassListenerEffectsBinder` | `bypassListenerEffects` | `bool` |
| `AudioSourceBypassReverbZonesBinder` | `bypassReverbZones` | `bool` |
| `AudioSourceOutputAudioMixerGroupBinder` | `outputAudioMixerGroup` | `AudioMixerGroup` |

**All binders:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## Float binders

The numeric binders (`Volume`, `Pitch`, `PanStereo` and others) implement `INumberBinder` and accept `int`, `float`, `long`, `double`.

Ranges the value is clamped to (NaN and infinity are logged and replaced with the lower bound):

| Binder | Range |
|--------|----------|
| `Volume`, `SpatialBlend` | 0..1 |
| `PanStereo` | -1..1 |
| `Pitch` | -3..3 |
| `DopplerLevel` | 0..5 |
| `ReverbZoneMix` | 0..1.1 |
| `Spread` | 0..360 |
| `Priority` | 0..256 |
| `Time`, `TimeSamples` | inside the current clip; without a clip the write is skipped |
| `MinMaxDistance` | negatives are raised to 0, an inverted pair is swapped |

```csharp
[ViewModel]
public partial class AudioViewModel
{
    [OneWayBind] private float _volume;
    [OneWayBind] private float _pitch;
    [OneWayBind] private bool _mute;
}
```

---

## AudioSourceMinMaxDistanceBinder

Binds `AudioSource.minDistance` / `AudioSource.maxDistance` through a `Vector2` (like `SliderMinMaxBinder`).

### AudioSourceDistanceMode

| Mode | Behaviour |
|-------|----------|
| `Min` | Updates `minDistance` only |
| `Max` | Updates `maxDistance` only |
| `Range` | Updates both |

---

## AudioSourceClipBinder

Binds `AudioSource.clip` (`AudioClip`).

```csharp
[ViewModel]
public partial class MusicPlayerViewModel
{
    [OneWayBind] private AudioClip _currentTrack;
}
```

---

## Switcher variants

Every binder has a Switcher variant (`bool` → one of two values):

- `AudioSourceVolumeSwitcherBinder`: `bool` → `float`
- `AudioSourcePitchSwitcherBinder`: `bool` → `float`
- `AudioSourceClipSwitcherBinder`: `bool` → `AudioClip`
- `AudioSourceMinMaxDistanceSwitcherBinder`: `bool` → `Vector2`
- `AudioSourceOutputAudioMixerGroupSwitcherBinder`: `bool` → `AudioMixerGroup`
- and so on

---

## AudioSourceToSourceMonoBinder

A MonoBinder for OneWayToSource binding of the `AudioSource` as a component. Inherits `ComponentToSourceMonoBinder<AudioSource>`.

---

## See also

- [Slider Binders](slider-binders.md), the same min/max pattern
- [Switcher Binders](switcher-binders.md), the Switcher pattern
- [StarterKit overview](README.md)

# AudioSource Binders

Биндеры для всех свойств компонента `AudioSource`.

---

## Обзор

Каждое свойство `AudioSource` имеет отдельный биндер, Switcher-вариант и MonoBinder-обёртки (обычные, Enum, EnumGroup).

| Биндер | Свойство | Тип данных |
|--------|---------|-----------|
| `AudioSourceVolumeBinder` | `volume` | `float` (clamp 0-1) |
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

**Все биндеры:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## Float-биндеры

Числовые биндеры (`Volume`, `Pitch`, `PanStereo` и др.) реализуют `INumberBinder` и принимают `int`, `float`, `long`, `double`.

Некоторые ограничивают значения:
- `Volume` — `Mathf.Clamp(value, 0, 1)`
- Остальные — без ограничений или с конвертером

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

Привязка `AudioSource.minDistance` / `AudioSource.maxDistance` через `Vector2` (аналогично `SliderMinMaxBinder`).

### AudioSourceDistanceMode

| Режим | Поведение |
|-------|----------|
| `Min` | Обновляет только `minDistance` |
| `Max` | Обновляет только `maxDistance` |
| `Range` | Обновляет оба значения |

---

## AudioSourceClipBinder

Привязка `AudioSource.clip` (`AudioClip`).

```csharp
[ViewModel]
public partial class MusicPlayerViewModel
{
    [OneWayBind] private AudioClip _currentTrack;
}
```

---

## Switcher-варианты

Каждый биндер имеет Switcher-вариант (`bool` → выбор между двумя значениями):

- `AudioSourceVolumeSwitcherBinder` — `bool` → `float`
- `AudioSourcePitchSwitcherBinder` — `bool` → `float`
- `AudioSourceClipSwitcherBinder` — `bool` → `AudioClip`
- `AudioSourceMinMaxDistanceSwitcherBinder` — `bool` → `Vector2`
- `AudioSourceOutputAudioMixerGroupSwitcherBinder` — `bool` → `AudioMixerGroup`
- и т.д.

---

## AudioSourceToSourceMonoBinder

MonoBinder для OneWayToSource-привязки `AudioSource` как компонента. Наследует `ComponentToSourceMonoBinder<AudioSource>`.

---

## См. также

- [Slider Binders](slider-binders.md) — аналогичный паттерн с min/max
- [Switcher Binders](switcher-binders.md) — паттерн Switcher
- [Обзор StarterKit](README.md)

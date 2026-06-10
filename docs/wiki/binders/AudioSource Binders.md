---
title: AudioSource Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/AudioSourceVolumeBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/AudioSourceVolumeSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/Mono/AudioSourceVolumeMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/Mono/AudioSourceVolumeEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Loop/AudioSourceLoopBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/MinMaxDistance/AudioSourceDistanceMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/OneWayToSource/Mono/AudioSourceToSourceMonoBinder.cs
tags: [binders, starterkit, audiosource, unity]
updated_at: 2026-05-31
---

# AudioSource Binders

> StarterKit binders that drive individual `UnityEngine.AudioSource` properties (volume, pitch, loop, distances, mixer group...) from ViewModel state, so audio behavior reacts to data instead of manual code.

## Why it exists

`AudioSource` exposes ~20 tweakable fields. Rather than hand-write code to push ViewModel values into each one, this family supplies a ready binder per property. Each subfolder targets exactly one `AudioSource` member, and every binder simply overrides a `Property` getter/setter (or a `SetValue`) pointing at that member. The shared logic — conversion, [[BindMode]], target caching — lives in the [[Binder Base Classes]], so these classes stay tiny.

## How it works

Each property folder picks the base class matching its value type (`Float`, `Bool`, `Object`, enum). Two physical shapes exist:

- **Plain binders** (`[Serializable]`, e.g. `AudioSourceVolumeBinder : TargetFloatBinder<AudioSource>`) — constructed in code with a target + [[BindMode]], used as serialized binder fields inside a [[View]].
- **MonoBinders** (e.g. `AudioSourceVolumeMonoBinder : ComponentFloatMonoBinder<AudioSource>`) — MonoBehaviour components dropped on a GameObject; they cache the component (`CachedComponent`) and carry `[AddComponentMenu]` / `[AddBinderContextMenu]` so the Editor can add them. See [[Mono Binders]].

Notable behavior: ranged properties self-clamp. `AudioSourceVolumeBinder` overrides `GetConvertedValue` to `Mathf.Clamp(..., 0, 1)`; the docs warn overrides must call `base`. Plain binders also call `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — `TwoWay` is rejected because `AudioSource` raises no change events.

## Variant axes

| Variant | Example | Role |
|---------|---------|------|
| Plain | `AudioSourceVolumeBinder` | code/serialized, one value -> property |
| Mono | `AudioSourceVolumeMonoBinder` | component, same via `CachedComponent` |
| Switcher | `AudioSourceVolumeSwitcherBinder` | picks one of two values from a bound `bool` |
| Enum | `AudioSourceVolumeEnumMonoBinder` | maps a bound enum to a value |
| EnumGroup | `AudioSourceVolumeEnumGroupMonoBinder` | enum-driven, group form |
| OneWayToSource | `AudioSourceToSourceMonoBinder` | pushes the `AudioSource` reference back to the ViewModel |

Not every property has all variants — `Loop` (a `bool`) ships only plain/Mono; `Volume`/`AudioClip` add Switchers; numeric props add Enum/EnumGroup Mono forms. `MinMaxDistance` carries an `AudioSourceDistanceMode` enum (`Min`/`Max`/`Range`) selecting which distance field to write. The single `OneWayToSource` binder is reference-only (no per-property folder).

## Key relationships

- Base classes: [[Binder Base Classes]] (`TargetFloatBinder`, `SwitcherFloatBinder`, `ComponentFloatMonoBinder`, `EnumFloatMonoBinder`, `ComponentToSourceMonoBinder`).
- Concepts: [[BindMode]], [[Data Binding]], [[IBinder]], [[Converters]].
- Sibling families: [[Renderer Binders]], [[Animator Binders]], [[Behaviour Binders]] follow the identical per-property pattern.

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/` — one folder per `AudioSource` property, each with plain + `Mono/` variants. See [[Binders Catalog]].

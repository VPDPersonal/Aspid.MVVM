# Slider Binders

Binders for the Unity UI `Slider` component.

---

## SliderValueBinder

Binds the slider value (`Slider.value`).

| Interface | Description |
|-----------|----------|
| `INumberBinder` | Accepts `int`, `float`, `long`, `double` |
| `INumberReverseBinder` | Sends changes back (events) |

### Inspector properties

| Property | Description |
|----------|----------|
| Converter | `IConverter<float, float>` (optional) |

### Loop protection

A write from the ViewModel raises `onValueChanged` for the other listeners, but the binder does not send it back to the ViewModel. The value is clamped to the slider range; if the clamp changed it, the changed value is sent to the ViewModel.

**Modes:** OneWay, TwoWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class VolumeViewModel
{
    [TwoWayBind] private float _volume;  // 0.0 - 1.0
}
```

---

## SliderMinMaxBinder

Binds the slider minimum and maximum (`Slider.minValue`, `Slider.maxValue`).

| Interface | Description |
|-----------|----------|
| `IBinder<Vector2>` | `x` = minValue, `y` = maxValue |

An inverted range is logged and swapped, a non-finite one is logged and not applied.

### SliderRangeMode

Defines which part of min/max is updated:

| Mode | Behaviour |
|-------|----------|
| `Min` | Updates `minValue` only |
| `Max` | Updates `maxValue` only |
| `Range` | Updates both `minValue` and `maxValue` |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class DifficultyViewModel
{
    [OneWayBind] private Vector2 _damageRange;  // (min, max) for the slider
}
```

---

## SliderMinMaxSwitcherBinder

`bool` → one of two `Vector2` values for min/max. Supports `SliderRangeMode`.

**Modes:** OneWay, OneTime.

---

## SliderCommandBinder

Binds an `IRelayCommand<float>` to `Slider.onValueChanged`. When the slider value changes it calls `command.Execute(value)`.

Accepts numeric commands: `IRelayCommand<int>`, `IRelayCommand<long>`, `IRelayCommand<float>`, `IRelayCommand<double>`.

### InteractableMode

The reaction to `CanExecute`, as in `ButtonCommandBinder`:

| Mode | Behaviour |
|-------|----------|
| `Interactable` | `slider.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Ignores it |
| `Custom` | Calls `ICanExecuteHandler.SetCanExecute(bool)` |

### Parameterized variants

| Binder | Command | Extra parameters |
|--------|---------|----------------|
| `SliderCommandBinder` | `IRelayCommand<float>` | — |
| `SliderCommandBinder<T>` | `IRelayCommand<float, T>` | 1 parameter |
| `SliderCommandBinder<T1, T2>` | `IRelayCommand<float, T1, T2>` | 2 parameters |
| `SliderCommandBinder<T1, T2, T3>` | `IRelayCommand<float, T1, T2, T3>` | 3 parameters |

The first command parameter is always the current slider value.

**Modes:** OneWay, OneTime.

```csharp
[ViewModel]
public partial class AudioViewModel
{
    [RelayCommand]
    private void SetVolume(float value) { /* ... */ }
    // → IRelayCommand<float> SetVolumeCommand
}
```

---

## SliderToSourceMonoBinder

A MonoBinder for OneWayToSource binding of the `Slider` as a component. Inherits `ComponentToSourceMonoBinder<Slider>`.

---

## See also

- [Toggle Binders](toggle-binders.md), binding a Toggle
- [Button Command Binders](button-command-binders.md), InteractableMode
- [StarterKit overview](README.md)

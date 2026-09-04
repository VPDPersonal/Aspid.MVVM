# Toggle Binders

Binders for the Unity UI `Toggle` component.

---

## ToggleIsOnBinder

Binds the `Toggle.isOn` state.

| Interface | Description |
|-----------|----------|
| `IBinder<bool>` | Sets `isOn` from the ViewModel |
| `IReverseBinder<bool>` | Sends changes back |

### Inspector properties

| Property | Description |
|----------|----------|
| `_converter` | Optional value converter; the reverse direction works through `ITwoWayConverter` |

### Loop protection

A write from the ViewModel raises `onValueChanged` for the other listeners, but the binder does not send it back to the ViewModel.

**Modes:** OneWay, TwoWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class SettingsViewModel
{
    [TwoWayBind] private bool _musicEnabled;
    [TwoWayBind] private bool _soundEnabled;
}
```

---

## ToggleCommandBinder

Binds a command to `Toggle.onValueChanged`.

| Interface | Description |
|-----------|----------|
| `IBinder<IRelayCommand>` | Calls `Execute()` on toggle |
| `IBinder<IRelayCommand<bool>>` | Calls `Execute(isOn)` with the current state |

### InteractableMode

The reaction to `CanExecute`, as in `ButtonCommandBinder`:

| Mode | Behaviour |
|-------|----------|
| `Interactable` | `toggle.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Ignores it |
| `Custom` | Calls `ICanExecuteHandler.SetCanExecute(bool)` |

### Parameterized variants

| Binder | Command | Extra parameters |
|--------|---------|----------------|
| `ToggleCommandBinder` | `IRelayCommand` / `IRelayCommand<bool>` | — |
| `ToggleCommandBinder<T>` | `IRelayCommand<bool, T>` | 1 parameter |
| `ToggleCommandBinder<T1, T2>` | `IRelayCommand<bool, T1, T2>` | 2 parameters |
| `ToggleCommandBinder<T1, T2, T3>` | `IRelayCommand<bool, T1, T2, T3>` | 3 parameters |

The first command parameter is always the current `isOn` state.

**Modes:** OneWay, OneTime.

```csharp
[ViewModel]
public partial class SettingsViewModel
{
    [RelayCommand]
    private void ToggleMusic(bool isOn) { /* ... */ }
    // → IRelayCommand<bool> ToggleMusicCommand
}
```

---

## ToggleIsOnEnumBinder / ToggleIsOnEnumGroupBinder

Set `isOn` from an enum value through `SetIsOnWithoutNotify`: the `Enum` variant for a single Toggle, `EnumGroup` for a set of Toggles where every enum member maps to its own element.

**Modes:** OneWay, OneTime.

---

## ToggleGroupAllowSwitchOffBinder

Binds `ToggleGroup.allowSwitchOff`. Turning it off selects nothing: an empty group stays empty until the user clicks.

**Modes:** OneWay, OneTime.

---

## Example: inverted settings

```csharp
[ViewModel]
public partial class NotificationViewModel
{
    [TwoWayBind] private bool _doNotDisturb;
}
```

In the Inspector set the `BoolInvertConverter` on `ToggleIsOnBinder`, so the Toggle shows "Notifications on" (`!doNotDisturb`).

---

## See also

- [Slider Binders](slider-binders.md)
- [Button Command Binders](button-command-binders.md), InteractableMode
- [StarterKit overview](README.md)

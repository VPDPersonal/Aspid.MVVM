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
| `_notifyMode` | Which writes from the ViewModel raise `onValueChanged`, see [Notify mode](#notify-mode) |

### Notify mode {#notify-mode}

`Notify Mode` decides which writes from the ViewModel raise `onValueChanged` for the other listeners. User input raises it in every mode.

| Mode | Value written on bind | Later changes from the ViewModel |
|---|---|---|
| `Always` | raises `onValueChanged` | raises `onValueChanged` |
| `AfterBind` | silent | raises `onValueChanged` |
| `Never` | silent | silent |

A binder added in the Editor starts with `Never`, so `onValueChanged` means user input only. A binder saved before the setting existed keeps `Always`.

`AfterBind` fits a list item with a click sound: a reused `VirtualizedList` item is bound again, and that write stays silent. `Never` also silences changes the ViewModel makes itself, for example a "reset to defaults" button.

The same setting is on the Slider, Scrollbar, InputField and Dropdown value binders and on their `Enum`, `EnumGroup` and `Switcher` variants.

### Loop protection

The binder does not send its own write back to the ViewModel, whatever the notify mode.

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

Set `isOn` from an enum value: the `Enum` variant for a single Toggle, `EnumGroup` for a set of Toggles where every enum member maps to its own element. [Notify mode](#notify-mode) defaults to `Never`.

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

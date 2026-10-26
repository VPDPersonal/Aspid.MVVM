# Dropdown Binders

Binders for the `TMP_Dropdown` (TextMeshPro) component.

---

## DropdownValueBinder

Binds the selected index, `TMP_Dropdown.value`.

| Interface | Description |
|-----------|----------|
| `IBinder<int>` | Sets the index from the ViewModel |
| `INumberBinder` | Accepts `int`, `float`, `long`, `double` |

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class LanguageViewModel
{
    [OneWayBind] private int _selectedLanguageIndex;
}
```

---

## DropdownValueSwitcherBinder

`bool` → one of two indices.

**Modes:** OneWay, OneTime.

---

## DropdownOptionsBinder

Binds the `TMP_Dropdown` option list.

| Interface | Description |
|-----------|----------|
| `IBinder<List<string>>` | Sets text options |
| `IBinder<List<Sprite>>` | Sets sprite options |
| `IBinder<IEnumerable<TMP_Dropdown.OptionData>>` | Sets options with the full data set |
| `IReverseBinder<List<TMP_Dropdown.OptionData>>` | Sends the current options back (OneWayToSource) |

On set the old options are cleared and the new ones added; `null` clears the list. The selected index is kept when the new list still contains it.

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

```csharp
[ViewModel]
public partial class LanguageViewModel
{
    [OneWayBind] private List<string> _languages;

    public LanguageViewModel()
    {
        _languages = new List<string> { "English", "Русский", "日本語" };
    }
}
```

---

## DropdownOptionsSwitcherBinder

`bool` → one of two option sets.

---

## DropdownOptionsByEnumMonoBinder

Fills the options with the values of the bound value's enum type. The options are rebuilt only when the type changes; `null` clears the list. An optional `IConverter<Enum, IEnumerable<OptionData>>` (for example `EnumToDropdownOptionDataConverter`) provides the labels; without it the value names are used.

---

## DropdownAlphaFadeSpeedBinder

Binds the fade speed, `TMP_Dropdown.alphaFadeSpeed`.

A negative value is raised to 0, NaN is logged.

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## DropdownAlphaFadeSpeedSwitcherBinder

`bool` → one of two fade speeds.

---

## DropdownCommandBinder

Binds a command to `TMP_Dropdown.onValueChanged`. When an item is selected it calls `command.Execute(selectedIndex)`.

| Interface | Description |
|-----------|----------|
| `IBinder<IRelayCommand<int>>` | Passes the index as `int` |
| `IBinder<IRelayCommand<long>>` | Passes the index as `long` |

### InteractableMode

As in `ButtonCommandBinder`, the reaction to `CanExecute`:

| Mode | Behaviour |
|-------|----------|
| `Interactable` | `dropdown.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Ignores it |
| `Custom` | Calls `ICanExecuteHandler.SetCanExecute(bool)` |

### Parameterized variants

| Binder | Command | Extra parameters |
|--------|---------|----------------|
| `DropdownCommandBinder` | `IRelayCommand<int>` / `IRelayCommand<long>` | — |
| `DropdownCommandBinder<T>` | `IRelayCommand<int, T>` | 1 parameter |
| `DropdownCommandBinder<T1, T2>` | `IRelayCommand<int, T1, T2>` | 2 parameters |
| `DropdownCommandBinder<T1, T2, T3>` | `IRelayCommand<int, T1, T2, T3>` | 3 parameters |

The first command parameter is always the selected index.

**Modes:** OneWay, OneTime.

```csharp
[ViewModel]
public partial class LanguageViewModel
{
    [RelayCommand]
    private void SelectLanguage(int index) { /* ... */ }
    // → IRelayCommand<int> SelectLanguageCommand
}
```

---

## DropdownToSourceMonoBinder

A MonoBinder for OneWayToSource binding of the `TMP_Dropdown` as a component. Inherits `ComponentToSourceMonoBinder<TMP_Dropdown>`.

---

## See also

- [Slider Binders](slider-binders.md)
- [Button Command Binders](button-command-binders.md), InteractableMode
- [StarterKit overview](README.md)

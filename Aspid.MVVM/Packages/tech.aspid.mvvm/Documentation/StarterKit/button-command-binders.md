# Button Command Binders

Binding an `IRelayCommand` to a `Button`.

---

## ButtonCommandBinder

The main binder connecting a command to `Button.onClick`.

### How it works

1. On bind it subscribes to `Button.onClick` → calls `command.Execute()`
2. Subscribes to `command.CanExecuteChanged` → updates the button availability
3. On unbind it unsubscribes from everything

### InteractableMode

Defines the reaction to `CanExecute`:

| Mode | Behaviour |
|-------|----------|
| `Interactable` | `button.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Ignores `CanExecute` |
| `Custom` | Raises the `CanExecuteChanged(bool)` event for manual handling |

**Binding modes:** OneWay, OneTime.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit() { /* ... */ }

    private bool CanSubmit() => !string.IsNullOrEmpty(Text);
}
```

---

## Parameterized variants

For commands with parameters the parameters are set in the Inspector:

| Binder | Command | Parameters |
|--------|---------|-----------|
| `ButtonCommandBinder` | `IRelayCommand` | — |
| `ButtonCommandBinder<T>` | `IRelayCommand<T>` | 1 parameter |
| `ButtonCommandBinder<T1, T2>` | `IRelayCommand<T1, T2>` | 2 parameters |
| `ButtonCommandBinder<T1, T2, T3>` | `IRelayCommand<T1, T2, T3>` | 3 parameters |
| `ButtonCommandBinder<T1, T2, T3, T4>` | `IRelayCommand<T1, T2, T3, T4>` | 4 parameters |

`ButtonCommandBinder` also accepts `IRelayCommand<bool>` and passes it `true` on click.

The generic variants are abstract for Mono; ready-made single-parameter components: `ButtonCommandIntMonoBinder`, `ButtonCommandFloatMonoBinder`, `ButtonCommandBoolMonoBinder`, `ButtonCommandStringMonoBinder`, `ButtonCommandObjectMonoBinder`.

### Example: a command with a parameter from Stats

```csharp
// ViewModel:
[RelayCommand]
private void AddSkillPointTo(Skill skill) { /* ... */ }
// → IRelayCommand<Skill> AddSkillPointToCommand

// Inspector:
// ButtonCommandBinder<Skill> with the parameter Skill.Strength
```

---

## See also

- [Commands](../07-commands.md): IRelayCommand, `[RelayCommand]`
- [StarterKit overview](README.md)

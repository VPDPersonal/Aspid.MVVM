# Button Command Binders

Привязка `IRelayCommand` к кнопке `Button`.

---

## ButtonCommandBinder

Основной биндер для связи команды с `Button.onClick`.

### Принцип работы

1. При привязке — подписывается на `Button.onClick` → вызывает `command.Execute()`
2. Подписывается на `command.CanExecuteChanged` → обновляет доступность кнопки
3. При отвязке — отписывается от всех событий

### InteractableMode

Определяет реакцию на `CanExecute`:

| Режим | Поведение |
|-------|----------|
| `Interactable` | `button.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Не реагирует на `CanExecute` |
| `Custom` | Вызывает event `CanExecuteChanged(bool)` для ручной обработки |

**Режимы привязки:** OneWay, OneTime.

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

## Параметризованные варианты

Для команд с параметрами — параметры задаются в Inspector:

| Биндер | Команда | Параметры |
|--------|---------|-----------|
| `ButtonCommandBinder` | `IRelayCommand` | — |
| `ButtonCommandBinder<T>` | `IRelayCommand<T>` | 1 параметр |
| `ButtonCommandBinder<T1, T2>` | `IRelayCommand<T1, T2>` | 2 параметра |
| `ButtonCommandBinder<T1, T2, T3>` | `IRelayCommand<T1, T2, T3>` | 3 параметра |
| `ButtonCommandBinder<T1, T2, T3, T4>` | `IRelayCommand<T1, T2, T3, T4>` | 4 параметра |

### Пример: команда с параметром из Stats

```csharp
// ViewModel:
[RelayCommand]
private void AddSkillPointTo(Skill skill) { /* ... */ }
// → IRelayCommand<Skill> AddSkillPointToCommand

// В Inspector:
// ButtonCommandBinder<Skill> с параметром Skill.Strength
```

---

## См. также

- [Команды](../07-commands.md) — IRelayCommand, `[RelayCommand]`
- [Обзор StarterKit](README.md)

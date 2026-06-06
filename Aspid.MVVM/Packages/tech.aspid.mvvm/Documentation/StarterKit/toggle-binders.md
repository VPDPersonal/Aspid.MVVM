# Toggle Binders

Биндеры для компонента `Toggle` Unity UI.

---

## ToggleIsOnBinder

Привязка состояния `Toggle.isOn`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<bool>` | Устанавливает `isOn` из ViewModel |
| `IReverseBinder<bool>` | Отправляет изменения обратно |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_isInvert` | Инвертирует значение: `true` в ViewModel → `false` в Toggle |

### Защита от циклов

Флаг `_isNotifyValueChanged` предотвращает рекурсию при TwoWay-привязке: когда ViewModel обновляет Toggle, обратное событие блокируется.

**Режимы:** OneWay, TwoWay, OneTime, OneWayToSource.

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

Привязка команды к `Toggle.onValueChanged`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<IRelayCommand>` | Вызывает `Execute()` при переключении |
| `IBinder<IRelayCommand<bool>>` | Вызывает `Execute(isOn)` с текущим состоянием |

### InteractableMode

Реакция на `CanExecute` — аналогично `ButtonCommandBinder`:

| Режим | Поведение |
|-------|----------|
| `Interactable` | `toggle.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Не реагирует |
| `Custom` | Вызывает `ICanExecuteView.SetCanExecute(bool)` |

### Параметризованные варианты

| Биндер | Команда | Доп. параметры |
|--------|---------|----------------|
| `ToggleCommandBinder` | `IRelayCommand` / `IRelayCommand<bool>` | — |
| `ToggleCommandBinder<T>` | `IRelayCommand<bool, T>` | 1 параметр |
| `ToggleCommandBinder<T1, T2>` | `IRelayCommand<bool, T1, T2>` | 2 параметра |
| `ToggleCommandBinder<T1, T2, T3>` | `IRelayCommand<bool, T1, T2, T3>` | 3 параметра |

Первый параметр команды — всегда текущее состояние `isOn`.

**Режимы:** OneWay, OneTime.

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

## Пример: настройки с инверсией

```csharp
[ViewModel]
public partial class NotificationViewModel
{
    // isInvert = true → Toggle ON = "не беспокоить выключен"
    [TwoWayBind] private bool _doNotDisturb;
}
```

В Inspector на `ToggleIsOnBinder` установите `Is Invert = true`, чтобы Toggle показывал "Уведомления включены" (`!doNotDisturb`).

---

## См. также

- [Slider Binders](slider-binders.md)
- [Button Command Binders](button-command-binders.md) — InteractableMode
- [Обзор StarterKit](README.md)

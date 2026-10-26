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
| `_converter` | Опциональный конвертер значения; в обратную сторону работает через `ITwoWayConverter` |

### Защита от циклов

Запись из ViewModel вызывает `onValueChanged` для остальных слушателей, но биндер не отправляет её обратно в ViewModel.

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
| `Custom` | Вызывает `ICanExecuteHandler.SetCanExecute(bool)` |

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

## ToggleIsOnEnumBinder / ToggleIsOnEnumGroupBinder

Устанавливают `isOn` по значению enum через `SetIsOnWithoutNotify`: `Enum`-вариант для одного Toggle, `EnumGroup` для набора Toggle, где каждому члену enum сопоставлен свой элемент.

**Режимы:** OneWay, OneTime.

---

## ToggleGroupAllowSwitchOffBinder

Привязка `ToggleGroup.allowSwitchOff`. Выключение не выбирает ничего: пустая группа остаётся пустой до нажатия пользователя.

**Режимы:** OneWay, OneTime.

---

## Пример: настройки с инверсией

```csharp
[ViewModel]
public partial class NotificationViewModel
{
    [TwoWayBind] private bool _doNotDisturb;
}
```

В Inspector на `ToggleIsOnBinder` задайте конвертер `BoolInvertConverter`, чтобы Toggle показывал "Уведомления включены" (`!doNotDisturb`).

---

## См. также

- [Slider Binders](slider-binders.md)
- [Button Command Binders](button-command-binders.md) — InteractableMode
- [Обзор StarterKit](README.md)

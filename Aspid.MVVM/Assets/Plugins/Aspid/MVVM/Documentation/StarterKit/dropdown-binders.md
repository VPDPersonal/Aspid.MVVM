# Dropdown Binders

Биндеры для компонента `TMP_Dropdown` (TextMeshPro).

---

## DropdownValueBinder

Привязка выбранного индекса `TMP_Dropdown.value`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<int>` | Устанавливает индекс из ViewModel |
| `INumberBinder` | Принимает `int`, `float`, `long`, `double` |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class LanguageViewModel
{
    [OneWayBind] private int _selectedLanguageIndex;
}
```

---

## DropdownValueSwitcherBinder

`bool` → выбор между двумя индексами.

**Режимы:** OneWay, OneTime.

---

## DropdownOptionsBinder

Привязка списка опций `TMP_Dropdown`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<List<string>>` | Устанавливает текстовые опции |
| `IBinder<List<Sprite>>` | Устанавливает опции-спрайты |
| `IBinder<IEnumerable<TMP_Dropdown.OptionData>>` | Устанавливает опции с полным набором данных |
| `IReverseBinder<List<TMP_Dropdown.OptionData>>` | Отправляет текущие опции обратно (OneWayToSource) |

При установке значения — старые опции очищаются (`ClearOptions`), затем добавляются новые.

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

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

`bool` → выбор между двумя наборами опций.

---

## DropdownAlphaFadeSpeedBinder

Привязка скорости затухания `TMP_Dropdown.alphaFadeSpeed`.

Значение ограничивается снизу: `Mathf.Max(value, 0)`.

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## DropdownAlphaFadeSpeedSwitcherBinder

`bool` → выбор между двумя значениями скорости затухания.

---

## DropdownCommandBinder

Привязка команды к `TMP_Dropdown.onValueChanged`. При выборе элемента вызывает `command.Execute(selectedIndex)`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<IRelayCommand<int>>` | Передаёт индекс как `int` |
| `IBinder<IRelayCommand<long>>` | Передаёт индекс как `long` |

### InteractableMode

Аналогично `ButtonCommandBinder` — реакция на `CanExecute`:

| Режим | Поведение |
|-------|----------|
| `Interactable` | `dropdown.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Не реагирует |
| `Custom` | Вызывает `ICanExecuteView.SetCanExecute(bool)` |

### Параметризованные варианты

| Биндер | Команда | Доп. параметры |
|--------|---------|----------------|
| `DropdownCommandBinder` | `IRelayCommand<int>` / `IRelayCommand<long>` | — |
| `DropdownCommandBinder<T>` | `IRelayCommand<int, T>` | 1 параметр |
| `DropdownCommandBinder<T1, T2>` | `IRelayCommand<int, T1, T2>` | 2 параметра |
| `DropdownCommandBinder<T1, T2, T3>` | `IRelayCommand<int, T1, T2, T3>` | 3 параметра |

Первый параметр команды — всегда выбранный индекс.

**Режимы:** OneWay, OneTime.

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

MonoBinder для OneWayToSource-привязки `TMP_Dropdown` как компонента. Наследует `ComponentToSourceMonoBinder<TMP_Dropdown>`.

---

## См. также

- [Slider Binders](slider-binders.md)
- [Button Command Binders](button-command-binders.md) — InteractableMode
- [Обзор StarterKit](README.md)

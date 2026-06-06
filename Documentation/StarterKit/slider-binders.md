# Slider Binders

Биндеры для компонента `Slider` Unity UI.

---

## SliderValueBinder

Привязка значения слайдера (`Slider.value`).

| Интерфейс | Описание |
|-----------|----------|
| `INumberBinder` | Принимает `int`, `float`, `long`, `double` |
| `INumberReverseBinder` | Отправляет изменения обратно (events) |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| Converter | `IConverter<float, float>` (опционально) |

### Защита от циклов

Флаг `_isNotifyValueChanged` предотвращает рекурсию при TwoWay-привязке: когда ViewModel обновляет `Slider.value`, обратное событие блокируется.

**Режимы:** OneWay, TwoWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class VolumeViewModel
{
    [TwoWayBind] private float _volume;  // 0.0 - 1.0
}
```

---

## SliderMinMaxBinder

Привязка минимального и максимального значений слайдера (`Slider.minValue`, `Slider.maxValue`).

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<Vector2>` | `x` = minValue, `y` = maxValue |
| `INumberBinder` | При числовом типе — устанавливает оба значения одинаково |

### SliderValueMode

Определяет, какую часть min/max обновлять:

| Режим | Поведение |
|-------|----------|
| `Min` | Обновляет только `minValue` |
| `Max` | Обновляет только `maxValue` |
| `Range` | Обновляет и `minValue`, и `maxValue` |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class DifficultyViewModel
{
    [OneWayBind] private Vector2 _damageRange;  // (min, max) для слайдера
}
```

---

## SliderMinMaxSwitcherBinder

`bool` → выбор между двумя значениями `Vector2` для min/max. Поддерживает `SliderValueMode`.

**Режимы:** OneWay, OneTime.

---

## SliderCommandBinder

Привязка `IRelayCommand<float>` к `Slider.onValueChanged`. При изменении значения слайдера вызывает `command.Execute(value)`.

Принимает числовые команды: `IRelayCommand<int>`, `IRelayCommand<long>`, `IRelayCommand<float>`, `IRelayCommand<double>`.

### InteractableMode

Реакция на `CanExecute` — аналогично `ButtonCommandBinder`:

| Режим | Поведение |
|-------|----------|
| `Interactable` | `slider.interactable = canExecute` |
| `Visible` | `gameObject.SetActive(canExecute)` |
| `None` | Не реагирует |
| `Custom` | Вызывает `ICanExecuteView.SetCanExecute(bool)` |

### Параметризованные варианты

| Биндер | Команда | Доп. параметры |
|--------|---------|----------------|
| `SliderCommandBinder` | `IRelayCommand<float>` | — |
| `SliderCommandBinder<T>` | `IRelayCommand<float, T>` | 1 параметр |
| `SliderCommandBinder<T1, T2>` | `IRelayCommand<float, T1, T2>` | 2 параметра |
| `SliderCommandBinder<T1, T2, T3>` | `IRelayCommand<float, T1, T2, T3>` | 3 параметра |

Первый параметр команды — всегда текущее значение слайдера.

**Режимы:** OneWay, OneTime.

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

MonoBinder для OneWayToSource-привязки `Slider` как компонента. Наследует `ComponentToSourceMonoBinder<Slider>`.

---

## См. также

- [Toggle Binders](toggle-binders.md) — привязка Toggle
- [Button Command Binders](button-command-binders.md) — InteractableMode
- [Обзор StarterKit](README.md)

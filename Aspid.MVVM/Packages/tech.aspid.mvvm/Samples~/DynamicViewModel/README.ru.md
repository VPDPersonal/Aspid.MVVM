# Туториал: Dynamic ViewModel

Разбор сэмпла `Feature: Dynamic ViewModel` — MVVM без генерации кода, для данных, форма которых известна только в рантайме.

**Предполагается знание:** [Counter](../01.%20Counter/README.ru.md).

---

## Когда это нужно

Свойства приходят из конфига, с сервера или из редактора уровней, и `[ViewModel]`-класса под них написать нельзя. `DynamicViewModel` объявляет свойства по строковому идентификатору, а биндеры находят их по тому же идентификатору.

Файлы: `Samples~/DynamicViewModel/`.

---

## Секундомер

```csharp
public sealed class StopwatchBootstrap : MonoBehaviour
{
    [SerializeField] private MonoView _view;

    private IDynamicProperty<string> _elapsed;
    private IDynamicProperty<int> _laps;

    private void Awake()
    {
        var viewModel = new DynamicViewModel();

        viewModel.Add("Title", "Stopwatch", BindMode.OneTime);
        _elapsed = viewModel.Add("Elapsed", Format(0f));
        _laps = viewModel.Add("Laps", 0);

        viewModel.Add<IRelayCommand>("LapCommand", new RelayCommand(() => _laps.Value++), BindMode.OneTime);

        _view.Initialize(viewModel);
    }

    private void Update()
    {
        if (!_isRunning) return;

        _seconds += Time.deltaTime;
        _elapsed.Value = Format(_seconds);
    }
}
```

- `Add<T>(id, value, mode)` возвращает `IDynamicProperty<T>` — дескриптор, через который значение читают и меняют. Биндеры следуют за ним.
- Режим по умолчанию — `OneWay`. Для команд и констант хватает `OneTime`.
- Команда — тоже значение: `Add<IRelayCommand>(...)`.

---

## View без класса

В сцене стоит обычный `MonoView`. В его списке **Binders** каждому идентификатору (`Title`, `Elapsed`, `Laps`, `LapCommand`, …) сопоставлены биндеры и тип значения. Больше ничего: ни `[View]`, ни `[ViewModel]`, ни генератора.

---

## Чего здесь нет

- Анализатора: опечатка в `"Elapsd"` не найдётся на компиляции, только в рантайме.
- Проверки типов на границе: `Add("Laps", 0)` и биндер с типом `string` разойдутся при первом же значении.

Поэтому `DynamicViewModel` — инструмент для действительно динамических данных, а не замена `[ViewModel]` ради экономии одного класса.

Подробнее — в [Dynamic ViewModel](../../Documentation/ru/10-dynamic-viewmodel.md).

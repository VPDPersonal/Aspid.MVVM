# Туториал: Counter

Первый пример обучающего маршрута. Кнопка увеличивает счётчик — число отображается в тексте.

**Изучаем:** `[ViewModel]`, `[OneWayBind]`, `[RelayCommand]`

---

## Что строим

```
[ + ]   →   Count: 5
кнопка       текст
```

При нажатии кнопки число увеличивается на 1.

---

## Шаг 1: ViewModel

```csharp
using Aspid.MVVM;

[ViewModel]
public sealed partial class CounterViewModel
{
    [OneWayBind] private int _count;

    [RelayCommand]
    private void Increment() => SetCount(Count + 1);
}
```

### Что генерирует Source Generator

**Из `[ViewModel]`:**
- Реализация `IViewModel`
- Метод `FindBindableMember(string id)` — поиск BindableMember по ID без рефлексии

**Из `[OneWayBind] int _count`:**
- Свойство `Count { get; }` — текущее значение
- Метод `SetCount(int value)` — обновление с уведомлением биндеров
- Событие `CountChanged` — для подписчиков вне биндинга
- `OneWayBindableMember<int>` — внутренний объект для системы привязки

**Из `[RelayCommand] void Increment()`:**
- Свойство `IncrementCommand` типа `IRelayCommand` — готовая команда для привязки к кнопке

### Почему `SetCount(Count + 1)`, а не `_count + 1`

`SetCount` уведомляет биндеры об изменении. Прямое присваивание `_count++` сработает, но UI не обновится. Анализатор кода предупредит, если вы используете `_count` там, где следует использовать `Count`.

---

## Шаг 2: View

```csharp
using UnityEngine;
using Aspid.MVVM;

[View]
public sealed partial class CounterView : MonoView
{
    [SerializeField] private MonoBinder _count;
    [SerializeField] private MonoBinder[] _increment;
}
```

### Правило именования

| Поле View | Соответствие в ViewModel |
|-----------|--------------------------|
| `_count` | `Count` (из `_count`) |
| `_increment` | `IncrementCommand` (из `Increment()`) |

Префиксы `_`, `m_`, `s_` игнорируются при сопоставлении. `_count` и `m_count` — одно и то же.

### MonoBinder vs MonoBinder[]

- `MonoBinder _count` — один биндер. Удобно, когда UI-элемент ровно один.
- `MonoBinder[] _increment` — массив биндеров. Несколько кнопок могут вызывать одну команду.

### Опционально: RequireBinder

```csharp
[RequireBinder(typeof(int))]
[SerializeField] private MonoBinder _count;

[RequireBinder(typeof(IRelayCommand))]
[SerializeField] private MonoBinder[] _increment;
```

`[RequireBinder]` фильтрует биндеры в Inspector — нельзя случайно подключить несовместимый тип.

---

## Шаг 3: Bootstrap

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private CounterView _counterView;

    private void Awake()
    {
        var viewModel = new CounterViewModel();
        _counterView.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        _counterView.DeinitializeView()?.DisposeViewModel();
    }
}
```

`DeinitializeView()` отвязывает биндеры и возвращает ViewModel. `DisposeViewModel()` вызывает `Dispose()` если ViewModel реализует `IDisposable`. Для Counter это необязательно, но это хорошая привычка.

---

## Шаг 4: Настройка сцены

### Иерархия объектов

```
Counter Scene
├── Bootstrap          (Bootstrap.cs)
├── Counter UI
│   ├── CounterView    (CounterView.cs)
│   ├── Count Text     (TextMonoBinder)
│   └── Increment Btn  (Button + ButtonCommandMonoBinder)
```

### В Inspector

1. **Count Text** → добавьте компонент `TextMonoBinder`
2. **Increment Btn** → добавьте компонент `ButtonCommandMonoBinder`, подключите `Button`
3. **CounterView** → перетащите `TextMonoBinder` в поле `Count`, `ButtonCommandMonoBinder` в массив `Increment`
4. **Bootstrap** → перетащите объект с `CounterView` в поле `_counterView`

---

## Расширение: декремент и сброс

```csharp
[ViewModel]
public sealed partial class CounterViewModel
{
    [OneWayBind] private int _count;

    [RelayCommand] private void Increment() => SetCount(Count + 1);
    [RelayCommand] private void Decrement() => SetCount(Count - 1);
    [RelayCommand] private void Reset()     => SetCount(0);
}
```

```csharp
[View]
public sealed partial class CounterView : MonoView
{
    [SerializeField] private MonoBinder   _count;
    [SerializeField] private MonoBinder[] _increment;
    [SerializeField] private MonoBinder[] _decrement;
    [SerializeField] private MonoBinder[] _reset;
}
```

Каждая команда добавляется одной строкой — никакого боilerplate.

---

## Резюме

| Концепция | Что сделали |
|-----------|-------------|
| `[ViewModel]` | Отметили класс для генерации |
| `[OneWayBind]` | Поле `_count` → свойство + биндинг в UI |
| `[RelayCommand]` | Метод `Increment` → команда для кнопки |
| `MonoView` | View описывает биндеры декларативно |
| Bootstrap | Создал ViewModel, передал в View |

---

## Следующий шаг

[Greeter →](greeter.md) — двусторонний биндинг: InputField обновляет приветствие в реальном времени.

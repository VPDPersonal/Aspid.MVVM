# Туториал: Greeter

Второй шаг обучающего маршрута. Пользователь вводит имя — приветствие обновляется в реальном времени.

**Изучаем:** `[TwoWayBind]`, реактивность через `On*Changed`

**Предполагается знание:** [Counter](counter.md) — `[OneWayBind]`, `[RelayCommand]`

---

## Что строим

```
[ Введите имя... ]   →   Привет, Владислав!
    InputField               Text
```

Текст обновляется при каждом нажатии клавиши — без кнопки.

---

## Отличие от Counter

В Counter данные шли только в одну сторону: `ViewModel → UI`.

Здесь добавляется обратный поток: `UI → ViewModel`. Когда пользователь печатает в InputField, значение записывается обратно в ViewModel, которая реагирует и обновляет приветствие.

```
ViewModel ──────────────────────► UI
              (OneWay: Greeting)

ViewModel ◄──────────────────────► UI
              (TwoWay: Name)
```

---

## Шаг 1: ViewModel

```csharp
using Aspid.MVVM;

[ViewModel]
public sealed partial class GreeterViewModel
{
    // TwoWay — UI может читать и записывать это поле.
    // Когда InputField изменится, Name обновится автоматически.
    [TwoWayBind] private string _name = "";

    // OneWay — только чтение для UI. Мы сами управляем значением.
    [OneWayBind] private string _greeting = "Введите имя";

    // Partial-метод, который Source Generator вызывает при каждом изменении Name.
    // Имя формируется по правилу: On + {PropertyName} + Changed.
    partial void OnNameChanged(string newValue) =>
        SetGreeting(string.IsNullOrEmpty(newValue)
            ? "Введите имя"
            : $"Привет, {newValue}!");
}
```

### Как работает `partial void On*Changed`

Source Generator генерирует пустой `partial void OnNameChanged(string newValue)` в своей части класса. Ваша реализация вызывается внутри сгенерированного сеттера `Name` каждый раз при изменении значения.

```
InputField изменился
    → TwoWayBinder записывает значение в Name
        → сгенерированный SetName вызывает OnNameChanged
            → OnNameChanged вызывает SetGreeting
                → TextBinder обновляет Text
```

Вся цепочка — без рефлексии, прямые вызовы.

### Разница между TwoWay и OneWay

| | `[OneWayBind]` | `[TwoWayBind]` |
|---|---|---|
| ViewModel → UI | Да | Да |
| UI → ViewModel | Нет | Да |
| Генерирует | `Set*`, property, event | То же + внутренний обратный канал |
| Типичное применение | Текст, счётчики, состояния | InputField, Toggle, Slider |

---

## Шаг 2: View

```csharp
using UnityEngine;
using Aspid.MVVM;

[View]
public sealed partial class GreeterView : MonoView
{
    // InputFieldMonoBinder — читает ввод из InputField и записывает в Name (TwoWay).
    [SerializeField] private MonoBinder _name;

    // TextMonoBinder — отображает Greeting (OneWay).
    [SerializeField] private MonoBinder _greeting;
}
```

Никаких кнопок — всё реактивно.

---

## Шаг 3: Bootstrap

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private GreeterView _greeterView;

    private void Awake()
    {
        _greeterView.Initialize(new GreeterViewModel());
    }

    private void OnDestroy()
    {
        _greeterView.DeinitializeView()?.DisposeViewModel();
    }
}
```

---

## Шаг 4: Настройка сцены

### Иерархия объектов

```
Greeter Scene
├── Bootstrap          (Bootstrap.cs)
└── Greeter UI
    ├── GreeterView    (GreeterView.cs)
    ├── Name Input     (InputField + InputFieldMonoBinder)
    └── Greeting Text  (TextMonoBinder)
```

### В Inspector

1. **Name Input** → добавьте `InputField` (UI Toolkit или uGUI) и компонент `InputFieldMonoBinder`
2. **Greeting Text** → добавьте `TextMeshPro` и компонент `TextMonoBinder`
3. **GreeterView** → перетащите `InputFieldMonoBinder` в поле `Name`, `TextMonoBinder` в поле `Greeting`
4. **Bootstrap** → назначьте ссылку на `GreeterView`

---

## Расширение: кнопка сброса

Можно совместить реактивность с командой:

```csharp
[ViewModel]
public sealed partial class GreeterViewModel
{
    [TwoWayBind] private string _name = "";
    [OneWayBind] private string _greeting = "Введите имя";

    partial void OnNameChanged(string newValue) =>
        SetGreeting(string.IsNullOrEmpty(newValue)
            ? "Введите имя"
            : $"Привет, {newValue}!");

    [RelayCommand]
    private void Clear() => SetName("");
}
```

```csharp
[View]
public sealed partial class GreeterView : MonoView
{
    [SerializeField] private MonoBinder   _name;
    [SerializeField] private MonoBinder   _greeting;
    [SerializeField] private MonoBinder[] _clear;
}
```

`SetName("")` очистит InputField — потому что `Name` — это TwoWay, и изменение в ViewModel отразится в UI.

---

## Резюме

| Концепция | Что сделали |
|-----------|-------------|
| `[TwoWayBind]` | `_name` синхронизируется с InputField в обе стороны |
| `partial void On*Changed` | Реакция на изменение без явной подписки |
| Реактивность | Нет кнопки — UI обновляется при каждом вводе |

---

## Следующий шаг

[TodoItem →](todo-item.md) — вводим отдельную Model, `IDisposable` и несколько типов биндинга на одном экране.

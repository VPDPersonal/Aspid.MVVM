# Туториал: Hello World

Пошаговый разбор Sample-проекта HelloWorld — базовые паттерны MVVM.

---

## Что мы разбираем

Приложение «Speaker» — поле ввода и кнопка «Say». Пользователь вводит текст и нажимает кнопку — текст отображается в другом View.

Файлы: `Samples/HelloWorld/`

---

## Шаг 1: Модель (Model)

Модель содержит бизнес-логику, не зависящую от UI:

```csharp
public class Speaker
{
    public event Action<string> TextChanged;

    private string _text;

    public string Text
    {
        get => _text;
        private set
        {
            _text = value;
            TextChanged?.Invoke(value);
        }
    }

    public void Say(string text) => Text = text;
}
```

Модель ничего не знает о ViewModel или View.

---

## Шаг 2: ViewModel

ViewModel связывает модель с UI:

```csharp
[ViewModel]
public sealed partial class SpeakerViewModel : IDisposable
{
    [OneWayBind] private string _outText;
    [TwoWayBind] private string _inputText;

    private readonly Speaker _speaker;

    public SpeakerViewModel(Speaker speaker)
    {
        _speaker = speaker;
        _outText = speaker.Text;
        _inputText = speaker.Text;
        _speaker.TextChanged += SetOutText;
    }

    [RelayCommand]
    private void Say()
    {
        _speaker.Say(InputText);
    }

    public void Dispose() =>
        _speaker.TextChanged -= SetOutText;
}
```

### Что генерирует Source Generator

Из `[ViewModel]` и атрибутов привязки генерируется:

| Поле | Атрибут | Генерируется |
|------|---------|-------------|
| `_outText` | `[OneWayBind]` | Свойство `OutText`, метод `SetOutText`, event `OutTextChanged` |
| `_inputText` | `[TwoWayBind]` | Свойство `InputText`, метод `SetInputText`, event `InputTextChanged` |
| `Say()` | `[RelayCommand]` | Свойство `SayCommand` типа `IRelayCommand` |

### Альтернативный подход: Moment-паттерн

Вместо кнопки можно реагировать на каждое изменение текста:

```csharp
[ViewModel]
public sealed partial class MomentSpeakerViewModel : IDisposable
{
    [OneWayBind] private string _outText;
    [TwoWayBind] private string _inputText;

    private readonly Speaker _speaker;

    public MomentSpeakerViewModel(Speaker speaker)
    {
        _speaker = speaker;
        _outText = speaker.Text;
        _inputText = speaker.Text;
        _speaker.TextChanged += SetOutText;
    }

    // Вызывается автоматически при изменении InputText из View
    partial void OnInputTextChanged(string newValue) =>
        _speaker.Say(newValue);

    public void Dispose() =>
        _speaker.TextChanged -= SetOutText;
}
```

---

## Шаг 3: View

View описывает привязку полей к биндерам:

```csharp
[View]
public sealed partial class OutView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _outText;
}
```

```csharp
[View]
public sealed partial class InputView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _inputText;

    [RequireBinder(typeof(IRelayCommand))]
    [SerializeField] private MonoBinder[] _sayCommand;
}
```

### Ключевые моменты

- Имена полей View **совпадают** с именами полей ViewModel (`_outText`, `_inputText`, `_sayCommand`)
- `MonoBinder` — одиночный биндер; `MonoBinder[]` — массив биндеров
- `[RequireBinder]` — опциональная фильтрация допустимых типов в Inspector
- Source Generator из `[View]` генерирует привязку/отвязку биндеров

---

## Шаг 4: Bootstrap

Bootstrap создаёт зависимости и связывает всё вместе:

```csharp
public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private OutView _outView;
    [SerializeField] private InputView _inputView;

    private Speaker _speaker;

    private void Awake()
    {
        _speaker = new Speaker();
        var viewModel = new SpeakerViewModel(_speaker);

        _outView.Initialize(viewModel);
        _inputView.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        _outView.DeinitializeView()?.DisposeViewModel();
        _inputView.DeinitializeView()?.DisposeViewModel();
    }
}
```

### Жизненный цикл

1. `Awake` → создание Model → создание ViewModel → `Initialize(viewModel)` для каждого View
2. `OnDestroy` → `DeinitializeView()` отвязывает биндеры → `DisposeViewModel()` вызывает `Dispose()` на ViewModel

---

## Шаг 5: Настройка в Inspector

1. На GameObject с `OutView` добавьте `TextMonoBinder` (или другой MonoBinder) и перетащите его в поле `_outText`
2. На GameObject с `InputView` добавьте `InputFieldMonoBinder` и перетащите в `_inputText`
3. Добавьте `ButtonCommandMonoBinder` и перетащите в `_sayCommand`
4. На Bootstrap-объекте назначьте ссылки на OutView и InputView

---

## Резюме

| Компонент | Ответственность |
|-----------|----------------|
| `Speaker` (Model) | Бизнес-логика |
| `SpeakerViewModel` | Связь Model ↔ View через привязки |
| `OutView` / `InputView` | Описание биндеров для UI |
| `Bootstrap` | Создание зависимостей, инициализация |

---

## См. также

- [Быстрый старт](../01-getting-started.md) — установка и первый проект
- [Архитектура](../02-architecture.md) — MVVM-паттерн
- [Туториал Stats](stats.md) — продвинутые команды

<img src="../../../../docs/Images/Aspid.MVVMHeaderImage.png" alt="Aspid.MVVM" />

[![Unity 6000.0+](../../../../docs/Images/status-badge-unity.svg)](https://assetstore.unity.com/packages/slug/298463)
[![Preview 1.1.0-beta.1](../../../../docs/Images/status-badge-preview.svg)](https://github.com/VPDPersonal/Aspid.MVVM/releases/tag/v1.1.0-beta.1)
[![MIT License](../../../../docs/Images/status-badge-license.svg)](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE)

Aspid.MVVM — высокопроизводительный MVVM-фреймворк для Unity на Source Generator: он отделяет бизнес-логику от представления без рефлексии, боксинга и шаблонного кода.

[Документация](https://vpdpersonal.github.io/Aspid.MVVM/ru/docs)

## Установка

В **Window → Package Manager** выберите **+ → Install package from git URL…**, вставьте этот URL и нажмите **Install**:

```text
https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview
```

URL ставит последнюю preview-версию; **Update** в Package Manager ставит следующую.

Нужен Unity 6000.0 или новее. Если пакет куплен в Asset Store, git URL не нужен: установите его из **My Assets** в Package Manager. Подробный путь — в разделе [Быстрый старт](01-getting-started.md).

## Возможности

### Ядро

#### [ViewModel](04-viewmodels.md)

Source Generator превращает поле с `[Bind]` в свойство с уведомлением. Без базового класса, обёрток и шаблонного кода.

```csharp
[ViewModel]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;
}
```

#### [Режимы привязки](03-binding-modes.md)

OneWay, TwoWay, OneTime и OneWayToSource. ViewModel задаёт верхнюю границу, каждый биндер выбирает свой режим в инспекторе.

```csharp
[OneWayBind] private int _health;
[TwoWayBind] private string _inputText;
```

#### [Команды](07-commands.md)

`[RelayCommand]` превращает метод в команду с поддержкой `CanExecute` и до четырёх параметров.

```csharp
[RelayCommand(CanExecute = nameof(CanSubmit))]
private void Submit() { }

private bool CanSubmit() => !string.IsNullOrEmpty(Text);
```

#### [View](05-views.md)

View — это MonoBehaviour, который передаёт члены ViewModel биндерам. Код инициализации пишет генератор.

```csharp
[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder[] _health;
}
```

### StarterKit

#### [Биндеры](06-binders.md)

Готовые биндеры для uGUI, TextMeshPro, Transform, Animator, AudioSource и других связывают член ViewModel со свойством компонента.

```csharp
public interface IBinder<in T> : IBinder
{
    void SetValue(T value);
}
```

#### [Конвертеры](08-converters.md)

Преобразуют значение по пути во View, не меняя ViewModel. Выберите готовый в инспекторе или напишите свой.

```csharp
public interface IConverter<in TFrom, out TTo> : IConverter
{
    TTo Convert(TFrom value);
}
```

#### [Коллекции](09-collections.md)

Наблюдаемые списки, словари, множества, стеки и очереди с фильтрацией, сортировкой и синхронизацией зависимых коллекций.

```csharp
IReadOnlyObservableListSync<TodoItemViewModel> items =
    todos.CreateSync(item => new TodoItemViewModel(item));
```

#### [Dynamic ViewModel](10-dynamic-viewmodel.md)

Собирает типизированную ViewModel в рантайме для простых структурированных данных, без отдельного класса.

```csharp
var viewModel = new DynamicViewModel();
IDynamicProperty<int> health = viewModel.Add("Health", 100);
view.Initialize(viewModel);
```

### Интеграция и инструменты

#### [View Initializers](11-view-initializers.md)

Инициализируют View из инспектора, без кода запуска. [Интеграция с DI](12-di-integration.md) получает ViewModel из Zenject или VContainer.

```csharp
// Без ViewInitializer нужен скрипт:
private void Awake() => _view.Initialize(new PlayerViewModel());
```

#### [Анализаторы](13-analyzers.md)

Анализаторы Roslyn находят пропущенный `partial`, неверную сигнатуру `CanExecute` или поле там, где нужно свойство, и предлагают исправление.

```text
AMVVM001  A class with [ViewModel] must be partial
AMVVM005  The CanExecute method/property was not found
```

## Ресурсы

- [Уроки](../../docusaurus-plugin-content-docs-tutorials/current/01.%20Counter/README.md) — шесть примеров, по одной идее на каждый, от счётчика до своего биндера.
- [Справочник API](https://vpdpersonal.github.io/Aspid.MVVM/api/Aspid.MVVM) — типы, методы и свойства пакета.
- [Изменения](https://vpdpersonal.github.io/Aspid.MVVM/ru/changelog) — изменения и исправления по версиям.
- [Миграция](https://vpdpersonal.github.io/Aspid.MVVM/ru/migration/1.1) — что поменять в проекте при переходе на новую версию.
- [Генераторы кода](https://github.com/VPDPersonal/Aspid.MVVM.Generators) — генераторы и анализаторы Roslyn, на которых работают атрибуты.

## Помощь и поддержка

Сообщайте об ошибках и задавайте вопросы в [GitHub Issues](https://github.com/VPDPersonal/Aspid.MVVM/issues). Укажите версию Unity, версию пакета и шаги воспроизведения.

Если пакет вам помогает, поставьте звезду на [GitHub](https://github.com/VPDPersonal/Aspid.MVVM).

Распространяется по [лицензии MIT](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE).

## Поддержать проект

Aspid.MVVM развивается в свободное время. Если он вам помогает, поддержите разработку в [Unity Asset Store](https://assetstore.unity.com/packages/slug/298463).

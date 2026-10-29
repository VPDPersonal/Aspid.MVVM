# DynamicViewModel

`DynamicViewModel` создаёт типизированную ViewModel во время выполнения, без отдельного класса и
Source Generator. Он предназначен для тестов View, прототипов, отладочных экранов и интерфейсов,
схема которых определяется конфигурацией.

Для обычных production-экранов используйте `[ViewModel]`: сгенерированный тип лучше выражает
фиксированную схему, проверяет идентификаторы во время компиляции и поддерживает `[RelayCommand]`.

## Быстрый старт

Метод `Add<T>` возвращает типизированный handle свойства:

```csharp
var viewModel = new DynamicViewModel();

IDynamicProperty<int> health = viewModel.Add("Health", 100);
IDynamicProperty<string> name = viewModel.Add("Name", "Hero", BindMode.TwoWay);
IDynamicProperty<Sprite> icon = viewModel.Add("Icon", heroSprite, BindMode.OneTime);

view.Initialize(viewModel);

health.Value = 75;             // View получает 75
Debug.Log(name.Value);         // актуальное значение, включая ввод из View
```

По умолчанию создаётся `OneWay`-свойство.

## Инициализатор коллекции

`DynamicViewModel` поддерживает компактный синтаксис для случаев, когда handles не нужны:

```csharp
var viewModel = new DynamicViewModel
{
    { "Title", "Settings" },
    { "Volume", 0.8f, BindMode.TwoWay },
    { "Icon", settingsIcon, BindMode.OneTime }
};
```

Количество свойств не ограничено. В отличие от старого `Create<T1, ..., T8>`, новые типы свойств
не требуют дополнительных перегрузок.

## Чтение и изменение

### Через типизированный handle

```csharp
var score = viewModel.Add("Score", 0);

score.ValueChanged += value => Debug.Log($"Score: {value}");
score.Value = 10;
```

Повторная установка равного значения ничего не отправляет и не вызывает `ValueChanged`.

### Через ViewModel

```csharp
IDynamicProperty<int> property = viewModel.Get<int>("Score");
property.Value = 20;

if (viewModel.TryGet<int>("Score", out var scoreProperty))
    scoreProperty.Value = 30;

viewModel["Score"].UntypedValue = 40;   // без generic-параметра, для config-driven кода
```

`Get<T>` бросает:

- `KeyNotFoundException`, если ID отсутствует;
- `ArgumentException`, если свойство существует, но имеет другой тип.

`TryGet<T>` возвращает `false` в обоих случаях.

## Режимы

| Режим | Поведение |
|---|---|
| `OneWay` | Начальное значение и последующие изменения передаются из ViewModel во View |
| `TwoWay` | Значение синхронизируется в обе стороны; `Value` всегда содержит актуальный ввод View |
| `OneTime` | Каждый новый binder получает текущее значение один раз; уже подключённые binder-ы не обновляются |
| `OneWayToSource` | Принимает изменения из View, не подписывая View на последующие изменения |
| `None` | Не поддерживается и отклоняется конструктором |

Режим свойства определяет возможности bindable member. Режим конкретного binder-а по-прежнему
задаётся в самом binder-е и должен быть совместим с этими возможностями.

## Нетипизированный доступ

Для конфигураций, где тип становится известен только во время выполнения, доступны
`IDynamicProperty`, `Properties` и индексатор:

```csharp
foreach (IDynamicProperty property in viewModel.Properties)
{
    Debug.Log($"{property.Id}: {property.ValueType.Name} = {property.UntypedValue}");
}

viewModel["Title"].UntypedValue = "Profile";
```

Запись значения несовместимого типа бросает `ArgumentException`. Запись `null` устанавливает
`default(T)`.

Пользовательское свойство можно реализовать через `IDynamicProperty` и добавить тем же методом:

```csharp
viewModel.Add(customProperty);
```

## Проверка идентификаторов

Пустые ID и дубликаты отклоняются сразу. По умолчанию регистр учитывается (`StringComparer.Ordinal`),
но comparer можно заменить:

```csharp
var viewModel = new DynamicViewModel(
    idComparer: StringComparer.OrdinalIgnoreCase);
```

Обычно отсутствующий ID binder-а означает, что привязка просто не создаётся. В тестах и при
разработке экрана удобно включить строгий режим:

```csharp
var viewModel = new DynamicViewModel(throwOnMissingMember: true);
```

Тогда запрос неизвестного ID бросает `KeyNotFoundException` с именем отсутствующего свойства.

## Добавление свойств и жизненный цикл View

Свойства следует добавлять до `view.Initialize(viewModel)`. `Add` после инициализации делает
свойство доступным для будущих запросов, но уже инициализированная View не выполняет поиск binder-ов
повторно автоматически.

## Когда использовать

Подходящие сценарии:

- изолированные тесты View;
- быстрый прототип;
- debug/admin UI;
- свойства, составленные из JSON или конфигурации;
- небольшие переиспользуемые View с runtime-набором полей.

Не используйте `DynamicViewModel` как замену обычной ViewModel с фиксированной схемой. Строковые ID
переносят часть ошибок из compile time в runtime, а бизнес-логика, команды и зависимости лучше
выражаются отдельным типом.

## Сравнение со сгенерированной ViewModel

| Аспект | Source Generator | `DynamicViewModel` |
|---|:---:|:---:|
| Фиксированная схема | Оптимально | Избыточно |
| Runtime-схема | Требует нового типа | Поддерживается |
| Проверка ID при компиляции | Да | Нет |
| Типизированное чтение значений | Да | Да |
| Обновление и наблюдение | Да | Да |
| `[RelayCommand]` и generated hooks | Да | Нет |
| Разрешение binder-а | Сгенерированный код | Один поиск в словаре |

Словарный поиск выполняется при подключении binder-а, а не при каждом изменении значения.

## См. также

- [ViewModel](04-viewmodels.md) — сгенерированные ViewModel
- [Режимы привязки](03-binding-modes.md) — OneWay, TwoWay, OneTime и OneWayToSource

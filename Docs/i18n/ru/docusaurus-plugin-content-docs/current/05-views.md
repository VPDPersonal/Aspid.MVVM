# View

View связывает UI-элементы с данными ViewModel через биндеры; в Unity она обычно наследуется от `MonoView`. Source Generator автоматически генерирует код инициализации и привязки.

## Содержание

- [Создание View](#создание-view)
- [Объявление биндеров](#объявление-биндеров)
- [Атрибут \[AsBinder\]](#атрибут-asbinder)
- [Атрибут \[BindId\]](#атрибут-bindid)
- [Атрибут \[IgnoreBind\]](#атрибут-ignorebind)
- [Жизненный цикл](#жизненный-цикл)
- [IView\<T\> — строго типизированный View](#iviewt--строго-типизированный-view)
- [EventMonoView](#eventmonoview)
- [Instantiate](#instantiate)

---

## Создание View

```csharp
using UnityEngine;
using Aspid.MVVM;

[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder _health;
    [SerializeField] private MonoBinder[] _name;
    [SerializeField] private MonoBinder[] _attackCommand;
}
```

**Обязательные условия:**
1. Класс должен быть нестатическим и `partial`
2. Атрибут `[View]`

Наследуйтесь от `MonoView` для интеграции с Unity-компонентами; обычный класс также поддерживается.

Вложенный View сохраняет исходный контейнер и generic-параметры. Каждый содержащий тип должен быть `partial`; иначе выдаётся `ASPIDGEN001`. [Пример и правила вложенности](04-viewmodels.md#вложенные-типы).

### Правило именования

Имя поля (без префикса `_`, `m_`, `s_`) должно совпадать с именем свойства в ViewModel:

| Поле в View | Привязывается к свойству ViewModel |
|---|---|
| `_health` | `Health` |
| `_name` | `Name` |
| `_attackCommand` | `AttackCommand` |

### Одиночный vs массив

- `MonoBinder _field` — один биндер. Удобно, когда точно один UI-элемент.
- `MonoBinder[] _field` — массив биндеров. Позволяет привязать несколько UI-элементов к одному свойству.

---

## Объявление биндеров

Три способа объявления:

### 1. Через поля

```csharp
[View]
public partial class ExampleView : MonoView
{
    [SerializeField] private MonoBinder _name;        // одиночный
    [SerializeField] private MonoBinder[] _items;     // массив
}
```

### 2. Через свойства

```csharp
[View]
public partial class ExampleView : MonoView
{
    private MonoBinder NameBinder { get; }
    private MonoBinder[] ItemBinders { get; }
}
```

### 3. Через [AsBinder]

Оборачивает Unity-компонент в указанный тип биндера (см. ниже).

---

## Атрибут [AsBinder]

Позволяет использовать Unity-компоненты напрямую как биндеры без добавления отдельного MonoBinder-компонента:

```csharp
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

[View]
public partial class ExampleView : MonoView
{
    // Image оборачивается в ImageSpriteBinder
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image _icon;

    // Массив Image оборачивается в ImageSpriteBinder[]
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image[] _images;
}
```

При первой инициализации Source Generator один раз создаёт биндер и кеширует его: `new ImageSpriteBinder(_icon)`,
а для массива — по биндеру на каждый элемент. Компонент передаётся первым параметром конструктора биндера, поэтому он
не должен быть `null`: пометьте поле `[SerializeField]` и назначьте его в инспекторе.

### Аргументы конструктора

Аргументы после типа передаются в конструктор биндера после компонента, по порядку. Это аргументы атрибута, поэтому
подходят только константы: строки, числа, `bool`, перечисления, `typeof`, `null`. Именованные аргументы не
поддерживаются: чтобы дойти до дальнего параметра, передайте значения по умолчанию всех параметров перед ним.

```csharp
// new AnimatorSetFloatBinder(_speed, "Speed")
[AsBinder(typeof(AnimatorSetFloatBinder), "Speed")]
[SerializeField] private Animator _speed;

// new AnimatorSetFloatBinder(_jumpHeight, "JumpHeight", null, BindMode.OneTime)
// null заполняет параметр конвертера, который стоит перед mode
[AsBinder(typeof(AnimatorSetFloatBinder), "JumpHeight", null, BindMode.OneTime)]
[SerializeField] private Animator _jumpHeight;
```

Тип должен быть доступным, неабстрактным, закрытым и реализовывать `IBinder` (`ASPIDVIEW001`). Несовместимый конструктор, нечитаемая цель или null-массив аргументов дают `ASPIDVIEW002`. Для одного null-аргумента конструктора используйте `(object)null`. Строки и числа сохраняют экранирование и C#-типы независимо от текущей культуры.

### nameof: передача члена View

Аргумент, записанный как `nameof(X)`, попадает в сгенерированный код как сам член `X`, а не как строка `"X"`.
Так в конструктор передаётся значение, которое атрибут хранить не может, например конвертер:

```csharp
private static readonly IConverter<bool, bool> Invert = new BoolInvertConverter();

// new GameObjectVisibleBinder(_isDead, Invert)
[AsBinder(typeof(GameObjectVisibleBinder), nameof(Invert))]
[SerializeField] private GameObject _isDead;
```

Биндеры создаются внутри View, поэтому `X` может быть любым видимым там членом, статическим или экземплярным
(например, сериализованным полем конвертера). От имени остаётся только последняя часть: `nameof(Converters.Invert)`
превращается в `Invert`, и это имя должно разрешаться внутри View.

> [!NOTE]
> `[AsBinder]` фиксирует тип биндера и его аргументы в коде. Если биндер, конвертер или режим должен выбирать
> дизайнер в инспекторе, используйте компонент `MonoBinder`.

---

## Атрибут [BindId]

Переопределяет ID привязки для поля View:

```csharp
[View]
public partial class ItemView : MonoView
{
    // Привязка к свойству "Number" вместо "Name"
    [BindId("Number")]
    [SerializeField] private MonoBinder _name;
}
```

Можно комбинировать с `[AsBinder]`:

```csharp
[View]
public partial class CustomView : MonoView
{
    [BindId("PlayerIcon")]
    [AsBinder(typeof(ImageSpriteBinder))]
    [SerializeField] private Image _icon;
}
```

---

## Атрибут [IgnoreBind]

Исключает поле из автоматической привязки:

```csharp
[View]
public partial class MixedView : MonoView
{
    [SerializeField] private MonoBinder _name;

    // Этот биндер НЕ будет привязан автоматически
    [IgnoreBind]
    [SerializeField] private MonoBinder _customBinder;

    partial void OnInitializedInternal(IViewModel viewModel)
    {
        // Ручная привязка
    }
}
```

---

## Жизненный цикл

Source Generator создаёт `partial`-методы для каждого этапа:

```csharp
[View]
public partial class LifecycleView : MonoView
{
    [SerializeField] private MonoBinder[] _data;

    [AsBinder(typeof(Aspid.MVVM.StarterKit.ImageSpriteBinder))]
    [SerializeField] private UnityEngine.UI.Image[] _icons;

    // Initialization

    // Before binders are bound
    partial void OnInitializingInternal(IViewModel viewModel) { }

    // After binders are bound
    partial void OnInitializedInternal(IViewModel viewModel) { }

    // Deinitialization

    // Before binders are unbound
    partial void OnDeinitializingInternal() { }

    // After binders are unbound
    partial void OnDeinitializedInternal() { }

    // Binder creation

    // Before binders are cached
    partial void OnInstantiatingBinders() { }

    // After binders are cached
    partial void OnInstantiatedBinders() { }
}
```

### Порядок вызова

1. `OnInitializingInternal(viewModel)`: перед привязкой
2. `OnInstantiatingBinders()` / `OnInstantiatedBinders()`: однократно, при первом кэшировании обёрток или свойств-биндеров
3. Привязка каждого биндера через `FindBindableMember` + `Bind`
4. `OnInitializedInternal(viewModel)`: после привязки
5. *(работа)*
6. `OnDeinitializingInternal()` — перед отвязкой
7. Отвязка каждого биндера через `Unbind`
8. `OnDeinitializedInternal()` — после отвязки

Сгенерированные View игнорируют повторный `Deinitialize()` во время очистки и отклоняют вызовы во время инициализации через `InvalidOperationException`. После ошибки биндера или хука выполняется остальная очистка. Одно исключение сохраняет свой экземпляр и stack trace; несколько ошибок возвращаются в `AggregateException`. Успешные вложенные привязки остаются под управлением View, даже если внешняя попытка завершилась ошибкой.

Состав биндеров фиксирован между `Initialize` и `Deinitialize`: ссылки в полях, кешированные binder-свойства и содержимое коллекций должны сохранять те же экземпляры. Генератор освобождает их напрямую через `UnbindContext(this)`, без отдельного списка привязок. Кеши свойств и обёрток `[AsBinder]` сохраняются. При очистке чужой владелец игнорируется; повторная привязка той же View считается продолжением её владения. Динамическая замена состава не поддерживается.

`Reinitialize`, `DeinitializeView` и обе перегрузки `DisposeView` требуют ненулевую View и иначе выбрасывают `ArgumentNullException`. Эти методы также отклоняют уничтоженные Unity-объекты. Возвращаемый null означает отсутствие прежней ViewModel. `DisposeView<T>` требует ссылочный тип. Null в качестве новой ViewModel для `Reinitialize` только деинициализирует View. Освобождение ViewModel остаётся ответственностью вызывающего кода. Если инициализация через `Reinitialize` завершается ошибкой, сгенерированная View остаётся отвязанной и не восстанавливает прежнюю ViewModel.

`ReinitializeSafely`, `DeinitializeViewSafely` и `DisposeViewSafely` пропускают null View и уничтоженные Unity-объекты. Исключения попадают в консоль Unity. При успехе методы возвращают прежнюю ViewModel. Если ViewModel отсутствовала, вызов пропущен или произошла ошибка, методы возвращают null.

---

## IView\<T\> — строго типизированный View

Для типобезопасной инициализации:

```csharp
[View]
public partial class StrongView : MonoView, IView<PlayerViewModel>
{
    [SerializeField] private MonoBinder[] _health;

    partial void OnInitializedInternal(IViewModel viewModel)
    {
        // Access the assigned ViewModel after binding.
    }
}
```

Позволяет вызывать `view.Initialize(playerVM)` вместо `view.Initialize((IViewModel)playerVM)`.

---

## EventMonoView

Расширение MonoView с UnityEvent-ами для подписки через Inspector:

```csharp
// В коде — просто наследуйте от EventMonoView
[View]
public partial class NotifyView : EventMonoView
{
    [SerializeField] private MonoBinder _status;
}
```

В Inspector доступны события:
- `Initialized(IViewModel)` — вызывается после инициализации
- `Deinitialized()` — вызывается после деинициализации

---

## Instantiate

Статический helper для создания View из префаба с немедленной инициализацией:

```csharp
// Создать и инициализировать View из префаба
var view = MonoView.Instantiate(prefab, viewModel);

// С указанием родительского Transform
var view = MonoView.Instantiate(prefab, viewModel, parentTransform);
```

---

## Инициализация и деинициализация

### Из кода (Bootstrap-паттерн)

```csharp
// Инициализация
_view.Initialize(viewModel);

// Деинициализация с освобождением ViewModel
_view.DeinitializeView()?.DisposeViewModel();
```

### Со стороны ViewModel

Расширения из `Aspid.MVVM` позволяют инициализировать один или несколько View:

```csharp
viewModel?.InitializeView(view);
viewModel?.InitializeViews(viewA, viewB);
viewModel?.InitializeViews(views);
```

`InitializeViews` принимает массивы, `List<T>` с ограничением `T : IView` и `IEnumerable<IView>` (включая коллекции только для чтения и ленивые последовательности). Поддерживаются массивы и списки конкретных типов View. Передача готового массива или списка не выделяет enumerator; вызов через `params` создаёт массив.

`InitializeViewSafely` и `InitializeViewsSafely` пропускают отсутствующие и уничтоженные ViewModel и View. Если отдельная View выбрасывает исключение, метод пишет его в лог и переходит к следующей View. Ошибка перебора коллекции попадает в лог и завершает пакетный вызов.

При `?.` null-ViewModel пропускает вызов и вычисление аргументов. Прямой вызов отклоняет null и уничтоженные Unity-объекты. Если оба аргумента недопустимы, он сообщает об обоих через `AggregateException`. Коллекции обрабатываются по порядку до первого исключения; уже инициализированные View сохраняют своё состояние. Каждый вызов передаётся в `IView.Initialize`, сохраняя правила жизненного цикла View.

### Через ViewInitializer (Inspector)

Без написания кода — через компонент [ViewInitializer](11-view-initializers.md).

---

## См. также

- [ViewModel](04-viewmodels.md) — создание ViewModel
- [Биндеры](06-binders.md) — типы биндеров
- [ViewInitializer](11-view-initializers.md) — инициализация через Inspector

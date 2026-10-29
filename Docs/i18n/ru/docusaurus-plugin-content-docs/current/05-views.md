# View

View — MonoBehaviour-компонент, связывающий UI-элементы с данными ViewModel через биндеры. Source Generator автоматически генерирует код инициализации и привязки.

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
1. Класс должен быть `partial`
2. Атрибут `[View]`
3. Наследование от `MonoView`

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

    protected override void OnInitializedInternal()
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

    // ── Инициализация ──

    // Перед привязкой биндеров
    partial void OnInitializingInternal() { }

    // После привязки всех биндеров
    partial void OnInitializedInternal() { }

    // ── Деинициализация ──

    // Перед отвязкой биндеров
    partial void OnDeinitializingInternal() { }

    // После отвязки всех биндеров
    partial void OnDeinitializedInternal() { }

    // ── Создание массива биндеров ──

    // Перед кэшированием биндеров
    partial void OnInstantiatingBinders() { }

    // После кэширования биндеров
    partial void OnInstantiatedBinders() { }
}
```

### Порядок вызова

1. `OnInstantiatingBinders()` / `OnInstantiatedBinders()` — однократно при первом Initialize
2. `OnInitializingInternal()` — перед привязкой
3. Привязка каждого биндера через `FindBindableMember` + `Bind`
4. `OnInitializedInternal()` — после привязки
5. *(работа)*
6. `OnDeinitializingInternal()` — перед отвязкой
7. Отвязка каждого биндера через `Unbind`
8. `OnDeinitializedInternal()` — после отвязки

---

## IView\<T\> — строго типизированный View

Для типобезопасной инициализации:

```csharp
[View]
public partial class StrongView : MonoView, IView<PlayerViewModel>
{
    [SerializeField] private MonoBinder[] _health;

    public void Initialize(PlayerViewModel viewModel)
    {
        // Source Generator генерирует реализацию
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

### Через ViewInitializer (Inspector)

Без написания кода — через компонент [ViewInitializer](11-view-initializers.md).

---

## См. также

- [ViewModel](04-viewmodels.md) — создание ViewModel
- [Биндеры](06-binders.md) — типы биндеров
- [ViewInitializer](11-view-initializers.md) — инициализация через Inspector

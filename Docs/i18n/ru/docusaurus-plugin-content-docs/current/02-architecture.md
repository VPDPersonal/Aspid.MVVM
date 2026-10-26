# Архитектура

Как устроен Aspid.MVVM: паттерн MVVM в Unity, роль Source Generator и конвейер привязки данных.

## Содержание

- [Паттерн MVVM в Unity](#паттерн-mvvm-в-unity)
- [Source Generation](#source-generation)
- [Конвейер привязки](#конвейер-привязки)
- [Обратная привязка](#обратная-привязка)
- [Архитектурная диаграмма](#архитектурная-диаграмма)
- [Ключевые интерфейсы](#ключевые-интерфейсы)

---

## Паттерн MVVM в Unity

MVVM (Model-View-ViewModel) разделяет приложение на три слоя:

| Слой | Роль | В Aspid.MVVM |
|------|------|-------------|
| **Model** | Бизнес-логика и данные | Обычные C#-классы (POCO) |
| **ViewModel** | Адаптация данных для отображения | Класс с `[ViewModel]` |
| **View** | Отображение и пользовательский ввод | `MonoView` + биндеры (MonoBehaviour) |

**Ключевой принцип:** ViewModel не знает о View. View подписывается на изменения ViewModel через систему привязок. Это позволяет:
- Тестировать ViewModel без Unity
- Менять View (UI) без изменения логики
- Нескольким View отображать один ViewModel

---

## Source Generation

Aspid.MVVM использует Roslyn Incremental Source Generators для генерации кода на этапе компиляции. Это обеспечивает **нулевую рефлексию** в runtime.

### Что генерируется

Из `partial`-класса с `[ViewModel]`:

```csharp
// Ваш код:
[ViewModel]
public sealed partial class PlayerViewModel
{
    [OneWayBind] private int _health;
    [TwoWayBind] private string _name;
    [RelayCommand] private void Attack() { /* ... */ }
}
```

Source Generator создаёт:

```csharp
// Сгенерированный код (упрощённо):
partial class PlayerViewModel : IViewModel
{
    private OneWayBindableMember<int> _healthBindableMember;
    private TwoWayBindableMember<string> _nameBindableMember;
    private readonly IRelayCommand _attackCommand;

    public int Health
    {
        get => _health;
        private set { /* обновление + уведомление */ }
    }

    public string Name
    {
        get => _name;
        set { /* обновление + уведомление */ }
    }

    public IRelayCommand AttackCommand => _attackCommand;

    public FindBindableMemberResult FindBindableMember(
        in FindBindableMemberParameters parameters)
    {
        // Диспетчеризация по ID — прямые сравнения строк
        if (parameters.Id == "Health") return new(healthAdder);
        if (parameters.Id == "Name") return new(nameAdder);
        if (parameters.Id == "AttackCommand") return new(attackAdder);
        return default;
    }

    public void NotifyAll() { /* уведомляет все привязки */ }
}
```

### Генерация для View

```csharp
// Ваш код:
[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder _health;
    [SerializeField] private MonoBinder[] _name;
}
```

Source Generator реализует `IView`: методы `Initialize`, `Deinitialize`, перечисление и привязку всех объявленных биндеров.

---

## Конвейер привязки

Пошагово, что происходит при вызове `view.Initialize(viewModel)`:

### 1. View запрашивает BindableMember

Для каждого поля-биндера View вызывает:
```csharp
var result = viewModel.FindBindableMember(
    new FindBindableMemberParameters("Health"));
```

`FindBindableMemberParameters` — это `ref struct` (нулевые аллокации).

### 2. ViewModel возвращает IBinderAdder

`FindBindableMemberResult` содержит `IBinderAdder` — интерфейс для подключения биндера:

```csharp
public interface IBinderAdder
{
    BindMode Mode { get; }
    IBinderRemover? Add(IBinder binder);
}
```

### 3. Биндер регистрируется

`Binder.Bind(IBinderAdder)` вызывает `binderAdder.Add(this)`:
- Биндер подписывается на событие `Changed` у `BindableMember`
- Биндер сразу получает текущее значение через `SetValue`

### 4. Обновление данных

При изменении свойства ViewModel:
```
ViewModel.Health = 50
  → _healthBindableMember.Value = 50
    → Changed?.Invoke(50)
      → каждый IBinder<int>.SetValue(50)
        → UI обновляется
```

---

## Обратная привязка

Для режимов **TwoWay** и **OneWayToSource** данные могут передаваться от View к ViewModel:

```
UI изменяется (пользователь вводит текст)
  → IReverseBinder<string>.ValueChanged?.Invoke("новый текст")
    → TwoWayBindableMember.OnValueChanged("новый текст")
      → _setValue("новый текст")
        → ViewModel._name = "новый текст"
```

`TwoWayBindableMember` подписывается на `IReverseBinder<T>.ValueChanged` при вызове `Add()`.

---

## Архитектурная диаграмма

```
┌─────────┐     ┌──────────────┐     ┌─────────────────┐     ┌────────┐     ┌────┐
│  Model  │◄───►│  ViewModel   │◄───►│ BindableMember  │◄───►│ Binder │◄───►│ UI │
│ (C#)    │     │ [ViewModel]  │     │ (OneWay/TwoWay) │     │ (Mono) │     │    │
└─────────┘     └──────────────┘     └─────────────────┘     └────────┘     └────┘
                                                                  ▲
                                                                  │
                                                          ┌───────┴───────┐
                                                          │ IConverter    │
                                                          │ (опционально) │
                                                          └───────────────┘
```

**Поток данных:**
- **OneWay:** Model → ViewModel → BindableMember → Binder → UI
- **TwoWay:** То же + UI → Binder → BindableMember → ViewModel
- **OneTime:** Однократная передача значения при привязке
- **OneWayToSource:** UI → Binder → BindableMember → ViewModel

---

## Ключевые интерфейсы

| Интерфейс | Назначение |
|-----------|-----------|
| `IViewModel` | Единственный метод `FindBindableMember` — точка входа для привязки |
| `IView` / `IView<T>` | `Initialize(viewModel)`, `Deinitialize()`, `ViewModel` — управление жизненным циклом |
| `IBinder<T>` | `SetValue(T)` — получение значения от ViewModel |
| `IReverseBinder<T>` | `ValueChanged` event — отправка значения обратно в ViewModel |
| `IAnyBinder` | `SetValue<T>(T)` — принимает любой тип (для отладки и универсальных биндеров) |
| `IBinderAdder` | `Add(IBinder)` — подключение биндера к BindableMember |
| `IBinderRemover` | `Remove(IBinder)` — отключение биндера |

### BindableMember-ы

| Класс | Режим | Описание |
|-------|-------|----------|
| `OneWayBindableMember<T>` | OneWay | Хранит значение, event `Changed`, push при подписке |
| `TwoWayBindableMember<T>` | TwoWay | + подписка на `IReverseBinder.ValueChanged` |
| `OneTimeBindableMember<T>` | OneTime | Singleton-per-T, одноразовый push, `Add` возвращает `null` |
| `OneWayToSourceBindableMember<T>` | OneWayToSource | Только обратная привязка, нет push в View |

---

## См. также

- [Быстрый старт](01-getting-started.md) — пример от начала до конца
- [Режимы привязки](03-binding-modes.md) — подробности о каждом режиме
- [ViewModel](04-viewmodels.md) — все атрибуты генерации
- [Биндеры](06-binders.md) — создание кастомных биндеров

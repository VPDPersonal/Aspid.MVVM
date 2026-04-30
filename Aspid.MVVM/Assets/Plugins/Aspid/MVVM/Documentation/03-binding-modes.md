# Режимы привязки

Aspid.MVVM поддерживает четыре режима привязки данных между ViewModel и View.

## Содержание

- [Обзор](#обзор)
- [OneWay](#oneway)
- [TwoWay](#twoway)
- [OneTime](#onetime)
- [OneWayToSource](#onewaytosource)
- [Автоматическое определение режима](#автоматическое-определение-режима)
- [Явное указание режима](#явное-указание-режима)
- [Ограничение режимов в Inspector](#ограничение-режимов-в-inspector)

---

## Обзор

```csharp
public enum BindMode
{
    None = 0,
    OneWay = 1,
    TwoWay = 2,
    OneTime = 3,
    OneWayToSource = 4
}
```

| Режим | Направление | Когда использовать |
|-------|-------------|-------------------|
| **OneWay** | ViewModel → View | Отображение данных (текст, прогресс-бар, иконка) |
| **TwoWay** | ViewModel ↔ View | Поля ввода, слайдеры, тогглы |
| **OneTime** | ViewModel → View (1 раз) | Статические данные, команды |
| **OneWayToSource** | View → ViewModel | Получение ссылки на компонент, события UI |

---

## OneWay

Данные передаются только из ViewModel во View. При изменении свойства — биндер обновляет UI. Изменения UI не влияют на ViewModel.

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private int _health;
    [OneWayBind] private string _playerName;
}
```

**Внутренняя реализация:** `OneWayBindableMember<T>` хранит значение и event `Changed`. При `Add()` биндер сразу получает текущее значение и подписывается на `Changed`.

**Типичные биндеры:** `TextBinder`, `ImageSpriteBinder`, `ImageFillBinder`, `GraphicColorBinder`.

---

## TwoWay

Двусторонняя синхронизация. Изменение ViewModel обновляет View, и наоборот — изменение View обновляет ViewModel.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [TwoWayBind] private string _inputText;
    [TwoWayBind] private bool _isEnabled;
    [TwoWayBind] private float _volume;
}
```

**Внутренняя реализация:** `TwoWayBindableMember<T>` поддерживает все 4 режима. При `Add()` для TwoWay/OneWayToSource также подписывается на `IReverseBinder<T>.ValueChanged`.

**Защита от цикла:** Биндеры (например, `InputFieldBinder`) содержат флаг `_isNotifyValueChanged` для предотвращения бесконечной рекурсии.

**Типичные биндеры:** `InputFieldBinder`, `SliderValueBinder`, `ToggleIsOnBinder`.

---

## OneTime

Значение передаётся один раз — при привязке. Дальнейшие изменения не отслеживаются.

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    // Автоматически OneTime для const
    [Bind] private const string Title = "Player Stats";

    // Автоматически OneTime для readonly
    [Bind] private readonly int _maxHealth;

    // Явно OneTime
    [OneTimeBind] private IRelayCommand _saveCommand;
}
```

**Внутренняя реализация:** `OneTimeBindableMember<T>` — singleton-per-T. Метод `Add()` вызывает `SetValue` один раз и возвращает `null` (нет необходимости в `IBinderRemover`).

**Когда использовать:** Команды (`IRelayCommand`), конфигурационные данные, статические метки.

---

## OneWayToSource

Данные передаются только из View в ViewModel. ViewModel не может push-ить значения во View.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [OneWayToSourceBind] private string _userInput;
}
```

**Внутренняя реализация:** `OneWayToSourceBindableMember<T>` — не хранит значение. Подписывается на `IReverseBinder<T>.ValueChanged` и передаёт изменения в ViewModel.

**Когда использовать:** Получение ввода пользователя без начального значения, получение ссылок на компоненты через `ComponentToSourceMonoBinder`.

---

## Автоматическое определение режима

Атрибут `[Bind]` без параметров автоматически выбирает режим:

| Тип поля | Определяемый режим |
|----------|-------------------|
| `const` | OneTime |
| `readonly` | OneTime |
| Мутабельное поле | TwoWay |

```csharp
[ViewModel]
public partial class ExampleViewModel
{
    [Bind] private const string Title = "Hello";     // → OneTime
    [Bind] private readonly int _id;                  // → OneTime
    [Bind] private string _name;                      // → TwoWay
    [Bind] private float _value;                      // → TwoWay
}
```

> **Рекомендация:** Используйте явные атрибуты (`[OneWayBind]`, `[TwoWayBind]` и т.д.) для лучшей читаемости кода.

---

## Явное указание режима

### Через параметр `[Bind]`

```csharp
[Bind(BindMode.OneWay)] private string _text;
[Bind(BindMode.TwoWay)] private float _slider;
[Bind(BindMode.OneTime)] private IRelayCommand _command;
[Bind(BindMode.OneWayToSource)] private string _userInput;
```

### Через атрибуты-ярлыки

```csharp
[OneWayBind] private string _text;
[TwoWayBind] private float _slider;
[OneTimeBind] private IRelayCommand _command;
[OneWayToSourceBind] private string _userInput;
```

Оба варианта эквивалентны. Атрибуты-ярлыки — сокращённая запись.

---

## Ограничение режимов в Inspector

На стороне биндера можно ограничить допустимые режимы привязки с помощью `[BindModeOverride]`:

```csharp
// Только OneWay и OneTime
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public class TransformPositionBinder : TargetVector3Binder<Transform>
{
    // TwoWay и OneWayToSource недоступны в Inspector
}
```

Некоторые биндеры поддерживают все режимы:

```csharp
[BindModeOverride(IsAll = true)]
public class DebugLogBinder : MonoBinder
{
    // Все режимы доступны
}
```

---

## Сводная таблица

| Режим | ViewModel → View | View → ViewModel | Обновляется | Подходит для |
|-------|:---:|:---:|-----------|-------------|
| OneWay | ✅ | ❌ | При каждом изменении | Отображение данных |
| TwoWay | ✅ | ✅ | При каждом изменении (обе стороны) | Интерактивные элементы |
| OneTime | ✅ | ❌ | Только при привязке | Статика, команды |
| OneWayToSource | ❌ | ✅ | При изменении View | Ввод данных, получение ссылок |

---

## См. также

- [Архитектура](02-architecture.md) — как работает конвейер привязки
- [ViewModel](04-viewmodels.md) — объявление привязываемых полей
- [Биндеры](06-binders.md) — создание биндеров с конкретными режимами

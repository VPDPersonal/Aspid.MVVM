# ViewModel

ViewModel — центральный элемент Aspid.MVVM. Source Generator автоматически генерирует код привязки для полей, помеченных атрибутами.

## Содержание

- [Создание ViewModel](#создание-viewmodel)
- [Атрибут \[Bind\]](#атрибут-bind)
- [Атрибуты-ярлыки](#атрибуты-ярлыки)
- [Атрибут \[BindAlso\]](#атрибут-bindalso)
- [Атрибут \[BindId\]](#атрибут-bindid)
- [Атрибут \[Access\]](#атрибут-access)
- [Обработчики изменений](#обработчики-изменений)
- [MonoViewModel](#monoviewmodel)
- [ScriptableViewModel](#scriptableviewmodel)
- [NotifyAll](#notifyall)

---

## Создание ViewModel

Минимальный ViewModel:

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private string _name;
    [OneWayBind] private int _health;
}
```

**Обязательные условия:**
1. Класс должен быть `partial` — Source Generator дополняет его
2. Атрибут `[ViewModel]` — маркер для генератора
3. Хотя бы одно поле с `[Bind]`-атрибутом

Source Generator реализует интерфейс `IViewModel` и генерирует:
- Свойства для каждого помеченного поля
- Методы `SetXxx(value)` для обновления значений
- `BindableMember<T>` для каждой привязки
- Метод `FindBindableMember()` для диспетчеризации
- Метод `NotifyAll()` для массового уведомления

### Три варианта ViewModel

| Тип | Базовый класс | Когда использовать |
|-----|--------------|-------------------|
| **POCO** | Нет | Основной вариант. Чистый C# без Unity-зависимостей |
| **MonoViewModel** | `MonoBehaviour` | Когда нужна редактируемость в Inspector |
| **ScriptableViewModel** | `ScriptableObject` | Для разделяемых данных между сценами |

---

## Атрибут [Bind]

Маркирует поле для генерации привязки. Режим определяется автоматически или явно:

```csharp
[ViewModel]
public partial class ExampleViewModel
{
    // Автоматический режим:
    [Bind] private const string Title = "Hello";  // → OneTime (const)
    [Bind] private readonly int _id;                // → OneTime (readonly)
    [Bind] private string _name;                    // → TwoWay  (мутабельное)

    // Явный режим:
    [Bind(BindMode.OneWay)] private int _score;
    [Bind(BindMode.TwoWay)] private string _input;
}
```

### Правила именования

Source Generator поддерживает стили именования полей:

| Поле | Сгенерированное свойство |
|------|------------------------|
| `_outText` | `OutText` |
| `m_outText` | `OutText` |
| `s_outText` | `OutText` |
| `outText` | `OutText` |

---

## Атрибуты-ярлыки

Сокращённая запись для указания режима:

```csharp
[ViewModel]
public partial class ShortcutExample
{
    [OneWayBind] private string _label;          // BindMode.OneWay
    [TwoWayBind] private float _volume;          // BindMode.TwoWay
    [OneTimeBind] private IRelayCommand _save;   // BindMode.OneTime
    [OneWayToSourceBind] private string _input;  // BindMode.OneWayToSource
}
```

---

## Атрибут [BindAlso]

При изменении поля дополнительно уведомляет указанное свойство. Используется для вычисляемых свойств:

```csharp
[ViewModel]
public partial class PersonViewModel
{
    [BindAlso(nameof(Nickname))]
    [BindAlso(nameof(FullName))]
    [Bind] private string _name;

    [BindAlso(nameof(FullName))]
    [Bind] private string _family;

    // Вычисляемые свойства — обновляются при изменении _name или _family
    private string Nickname => Name.ToLower();
    private string FullName => $"{Name} {Family}";
}
```

Когда `Name` изменяется — биндеры, привязанные к `Nickname` и `FullName`, также получают уведомление.

---

## Атрибут [BindId]

Переопределяет ID привязки (по умолчанию — имя сгенерированного свойства):

```csharp
[ViewModel]
public partial class CustomIdViewModel
{
    // Поле _text1 привязывается под ID "Text2"
    [BindId("Text2")]
    [Bind] private string _text1;

    // Метод Do привязывается как команда "OtherDoCommand"
    [RelayCommand]
    [BindId("OtherDoCommand")]
    private void Do() { }
}
```

Это полезно, когда имя поля в View не совпадает с именем в ViewModel.

---

## Атрибут [Access]

Управляет видимостью сгенерированного свойства:

```csharp
[ViewModel]
public partial class AccessExample
{
    // private string Text1 { get; set; }  (по умолчанию)
    [Bind] private string _text1;

    // public string Text2 { get; set; }
    [Access(Access.Public)]
    [Bind] private string _text2;

    // protected string Text3 { get; set; }
    [Access(Access.Protected)]
    [Bind] private string _text3;

    // public string Text4 { get; private set; }
    [Access(Get = Access.Public)]
    [Bind] private string _text4;

    // public string Text5 { get; protected set; }
    [Access(Get = Access.Public, Set = Access.Protected)]
    [Bind] private string _text5;

    // protected string Text6 { private get; set; }
    [Access(Get = Access.Protected, Set = Access.Public)]
    [Bind] private string _text6;
}
```

### Уровни доступа

- `Access.Private` — по умолчанию для get и set
- `Access.Protected` — виден в наследниках
- `Access.Public` — виден всем

Независимая настройка `Get` и `Set` позволяет создавать свойства вроде `public get / private set`.

---

## Обработчики изменений

Source Generator создаёт `partial`-методы, которые вызываются при изменении свойства:

```csharp
[ViewModel]
public partial class HandlerExample
{
    [Bind] private string _name;

    // Вызывается ПЕРЕД изменением Name
    partial void OnNameChanging(string oldValue, string newValue)
    {
        // Можно выполнить валидацию или логирование
    }

    // Вызывается ПОСЛЕ изменения Name
    partial void OnNameChanged(string newValue)
    {
        // Можно обновить зависимые данные
    }
}
```

### Паттерн: мгновенная реакция на ввод

```csharp
[ViewModel]
public partial class MomentSpeakerViewModel
{
    [TwoWayBind] private string _inputText;

    private readonly Speaker _speaker;

    // При каждом изменении текста в InputField —
    // мгновенно обновляем модель
    partial void OnInputTextChanged(string newValue)
    {
        _speaker.Say(newValue);
    }
}
```

---

## MonoViewModel

Для ViewModel, редактируемых через Inspector:

```csharp
using UnityEngine;
using Aspid.MVVM;

[ViewModel]
public partial class SettingsViewModel : MonoViewModel
{
    [SerializeField] [OneWayBind] private float _musicVolume = 0.8f;
    [SerializeField] [OneWayBind] private float _sfxVolume = 1.0f;
}
```

**Особенности:**
- Наследует `MonoBehaviour`
- `OnValidate()` вызывает `NotifyAll()` — изменения в Inspector сразу отображаются
- `Dispose()` вызывает `Destroy(this)`

---

## ScriptableViewModel

Для ViewModel-ов, общих между сценами:

```csharp
using UnityEngine;
using Aspid.MVVM;

[ViewModel]
public partial class GameConfigViewModel : ScriptableViewModel
{
    [SerializeField] [OneWayBind] private string _gameName;
    [SerializeField] [OneWayBind] private int _maxPlayers;
}
```

**Особенности:**
- Наследует `ScriptableObject`
- `OnValidate()` вызывает `NotifyAll()`
- Можно создавать через `CreateAssetMenu`

---

## NotifyAll

Генерируемый метод для уведомления всех привязок о текущих значениях:

```csharp
var viewModel = new PlayerViewModel();
viewModel.Health = 100;
viewModel.Name = "Hero";
viewModel.Armor = 50;

// Уведомить все биндеры о текущих значениях разом
viewModel.NotifyAll();
```

**Когда использовать:**
- После массового обновления нескольких полей
- После десериализации / загрузки данных
- В `MonoViewModel.OnValidate()` (вызывается автоматически)

---

## См. также

- [Режимы привязки](03-binding-modes.md) — подробности о BindMode
- [Команды](07-commands.md) — атрибут `[RelayCommand]`
- [View](05-views.md) — создание View для ViewModel
- [DynamicViewModel](10-dynamic-viewmodel.md) — ViewModel без кодогенерации

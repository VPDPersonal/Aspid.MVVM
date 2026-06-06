# DynamicViewModel

DynamicViewModel позволяет создавать ViewModel в runtime без Source Generator. Удобно для прототипирования, простых случаев и тестов.

## Содержание

- [Обзор](#обзор)
- [EmptyViewModel](#emptyviewmodel)
- [Типы динамических свойств](#типы-динамических-свойств)
- [Создание DynamicViewModel](#создание-dynamicviewmodel)
- [Фабричные методы Create](#фабричные-методы-create)
- [Обновление значений](#обновление-значений)
- [Сравнение с генерируемым ViewModel](#сравнение-с-генерируемым-viewmodel)

---

## Обзор

`DynamicViewModel` хранит словарь `Dictionary<string, IDynamicProperty>`. При запросе привязки ищет свойство по ID в словаре.

**Когда использовать:**
- Быстрое прототипирование UI
- Простые экраны с 1-5 свойствами
- Unit-тесты View
- Данные, определяемые в runtime (JSON, конфигурации)

---

## EmptyViewModel

Заглушка без свойств:

```csharp
// FindBindableMember всегда возвращает "не найдено"
IViewModel empty = new EmptyViewModel();
view.Initialize(empty); // Безопасно — биндеры просто не привяжутся
```

Используйте как null-safe замену вместо `null`.

---

## Типы динамических свойств

| Класс | Режим | Описание |
|-------|-------|----------|
| `OneWayDynamicProperty<T>` | OneWay | Только ViewModel → View |
| `TwoWayDynamicProperty<T>` | TwoWay | Двусторонняя, дедупликация одинаковых значений |
| `OneTimeDynamicProperty<T>` | OneTime | Устанавливается однократно |

Все реализуют `IDynamicProperty`:
```csharp
public interface IDynamicProperty
{
    IBinderAdder GetAdder();
}
```

---

## Создание DynamicViewModel

### Через словарь

```csharp
var viewModel = new DynamicViewModel(
    new Dictionary<string, IDynamicProperty>
    {
        ["Health"] = new OneWayDynamicProperty<int>(100),
        ["Name"] = new TwoWayDynamicProperty<string>("Hero"),
        ["Icon"] = new OneTimeDynamicProperty<Sprite>(heroSprite)
    }
);

view.Initialize(viewModel);
```

### Через неявное приведение

```csharp
DynamicViewModel viewModel = new Dictionary<string, IDynamicProperty>
{
    ["Title"] = new OneWayDynamicProperty<string>("Settings"),
    ["Volume"] = new TwoWayDynamicProperty<float>(0.8f)
};
```

---

## Фабричные методы Create

Типобезопасное создание до 8 свойств за один вызов:

```csharp
// DynamicPropertyData<T> — описание свойства:
//   string Id, T Value, BindMode Mode

var viewModel = DynamicViewModel.Create(
    new DynamicPropertyData<string>("Title", "Hello", BindMode.OneWay),
    new DynamicPropertyData<int>("Score", 0, BindMode.OneWay),
    new DynamicPropertyData<float>("Volume", 0.5f, BindMode.TwoWay)
);
```

Перегрузки от 1 до 8 параметров: `Create<T1>`, `Create<T1, T2>`, ..., `Create<T1, ..., T8>`.

> **Ограничение:** `DynamicPropertyData` отклоняет `BindMode.None` и `BindMode.OneWayToSource`.

---

## Обновление значений

Для обновления значений после создания используйте типизированные свойства:

```csharp
var health = new OneWayDynamicProperty<int>(100);
var name = new TwoWayDynamicProperty<string>("Hero");

var viewModel = new DynamicViewModel(
    new Dictionary<string, IDynamicProperty>
    {
        ["Health"] = health,
        ["Name"] = name
    }
);

view.Initialize(viewModel);

// Обновление — биндеры автоматически получат новое значение
health.Value = 75;

// TwoWay — значение может быть изменено из View
// name.Value уже содержит актуальное значение из InputField
Debug.Log(name.Value);
```

---

## Сравнение с генерируемым ViewModel

| Аспект | Source Generator | DynamicViewModel |
|--------|:---:|:---:|
| Производительность | ✅ Оптимальная (прямые вызовы) | ⚠️ Словарный поиск |
| Типобезопасность | ✅ Compile-time | ⚠️ Runtime |
| Boilerplate | ✅ Минимальный (атрибуты) | ⚠️ Ручное создание свойств |
| Гибкость | ⚠️ Требует перекомпиляции | ✅ Runtime-конфигурация |
| Тестирование | ✅ Полное | ✅ Полное |
| OnXxxChanged хуки | ✅ Есть | ❌ Нет |
| [RelayCommand] | ✅ Есть | ❌ Ручное создание |

**Рекомендация:** Используйте Source Generator для production-кода. DynamicViewModel — для прототипов, тестов и динамических данных.

---

## См. также

- [ViewModel](04-viewmodels.md) — генерируемый ViewModel
- [Режимы привязки](03-binding-modes.md) — OneWay, TwoWay, OneTime

# Биндеры

Биндер — мост между свойством ViewModel и UI-элементом. Он получает данные из ViewModel и обновляет UI, а в режимах TwoWay/OneWayToSource — отправляет изменения обратно.

## Содержание

- [Иерархия классов](#иерархия-классов)
- [Интерфейсы биндеров](#интерфейсы-биндеров)
- [Binder — базовый класс](#binder--базовый-класс)
- [MonoBinder](#monobinder)
- [ComponentMonoBinder](#componentmonobinder)
- [TargetBinder](#targetbinder)
- [Создание кастомного биндера](#создание-кастомного-биндера)
- [\[BindModeOverride\]](#bindmodeoverride)
- [\[UsedInModes\]](#usedinmodes)
- [DebugLogBinder](#debuglogbinder)

---

## Иерархия классов

```
Binder (абстрактный, не MonoBehaviour)
  └── MonoBinder (MonoBehaviour, абстрактный)
        └── ComponentMonoBinder<TComponent>
              └── ComponentMonoBinder<TComponent, TProperty>
                    └── TargetBinder<TTarget, TProperty>
                          └── TargetBinder<TTarget, TProperty, TConverter>
                                └── Конкретные биндеры (TextBinder, ImageSpriteBinder, ...)
```

---

## Интерфейсы биндеров

| Интерфейс | Назначение |
|-----------|-----------|
| `IBinder<T>` | `void SetValue(T value)` — получение значения от ViewModel |
| `IReverseBinder<T>` | `event Action<T> ValueChanged` — отправка изменений из View |
| `IAnyBinder` | `void SetValue<T>(T value)` — принимает любой тип |
| `INumberBinder` | `SetValue(int)`, `SetValue(float)`, `SetValue(long)`, `SetValue(double)` |
| `IColorBinder` | `SetValue(Color)` |
| `IVectorBinder` | `SetValue(Vector3)` |
| `INumberReverseBinder` | Обратная привязка для числовых типов |

### IBinder\<T\> — основной интерфейс

```csharp
public interface IBinder<in T> : IBinder
{
    void SetValue(T value);
}
```

Вызывается при каждом изменении свойства ViewModel (в режимах OneWay/TwoWay).

### IReverseBinder\<T\> — обратная привязка

```csharp
public interface IReverseBinder<T> : IBinder
{
    event Action<T>? ValueChanged;
}
```

UI-элемент вызывает `ValueChanged?.Invoke(newValue)` при изменении (например, ввод текста, перемещение слайдера).

---

## Binder — базовый класс

Не наследует `MonoBehaviour`. Содержит базовую логику привязки:

```csharp
public abstract class Binder
{
    public BindMode Mode { get; }        // Режим привязки (сериализуется)
    public virtual bool CanBind => true;  // Можно отключить привязку
    public bool IsBound { get; }         // Привязан ли сейчас

    public void Bind(IBinderAdder binderAdder);   // Привязка
    public void Unbind();                          // Отвязка

    // Виртуальные хуки:
    protected virtual void OnBinding() { }
    protected virtual void OnBound() { }
    protected virtual void OnUnbinding() { }
    protected virtual void OnUnbound() { }
}
```

---

## MonoBinder

MonoBehaviour-обёртка над `Binder`. Базовый класс для всех Inspector-биндеров:

```csharp
public abstract class MonoBinder : MonoBehaviour
{
    // Сериализованный режим привязки — выбирается в Inspector
    [SerializeField] private BindMode _mode;
}
```

Все готовые биндеры из StarterKit наследуют `MonoBinder`.

---

## ComponentMonoBinder

Добавляет автоматический `GetComponent<T>()`:

```csharp
// Один generic-параметр: автоматически находит компонент
public abstract class ComponentMonoBinder<TComponent> : MonoBinder
{
    protected TComponent CachedComponent { get; } // Ленивый GetComponent
}

// Два generic-параметра: + свойство для привязки
public abstract class ComponentMonoBinder<TComponent, TProperty> : ...
{
    // Переопределите для привязки конкретного свойства
    protected abstract TProperty Property { get; set; }
}
```

---

## TargetBinder

Базовый класс StarterKit с поддержкой конвертеров:

```csharp
public abstract class TargetBinder<TTarget, TProperty> : MonoBinder
{
    protected TTarget Target { get; }    // Целевой компонент
    protected abstract TProperty Property { get; set; }
}

// С конвертером:
public abstract class TargetBinder<TTarget, TProperty, TConverter> : TargetBinder<TTarget, TProperty>
    where TConverter : IConverter<TProperty?, TProperty?>
{
    // Конвертер назначается через Inspector ([SerializeReference])
    [SerializeReference] private TConverter? _converter;

    // ViewModel → View
    protected override TProperty? GetConvertedValue(TProperty? value) => ...

    // View → ViewModel: срабатывает, только если конвертер реализует ITwoWayConverter
    protected override TProperty? GetConvertedBackValue(TProperty? value) => ...
}
```

Конвертер хранится в приватном поле — производный класс переопределяет не его, а `GetConvertedValue` /
`GetConvertedBackValue`. Ограничение `TProperty → TProperty` намеренное: конвертер на биндере меняет
значение, а не его тип. Кросс-типовые преобразования (`float → string`) делает сам биндер.

**Специализированные базовые классы:**

| Класс | Тип Property | Доп. возможности |
|-------|-------------|-----------------|
| `TargetBinder<T, bool>` | `bool` | `_converter` — опциональный `IConverter<bool, bool>` |
| `TargetBinder<T, string>` | `string` | `_converter` — опциональный `IConverter<string, string>` |
| `TargetFloatBinder<T>` | `float` | `IFloatBinder` — принимает int/long/double |
| `TargetIntBinder<T>` | `int` | `IIntBinder` |
| `TargetBinder<T, Vector3>` + `IVector3Binder` | `Vector3` | принимает `Vector2` (Z = 0) и скаляр (во все три компоненты) |
| `TargetBinder<T, Vector2>` + `IVector2Binder` | `Vector2` | принимает `Vector3` (отбрасывает Z) и скаляр (в обе компоненты) |
| `TargetBinder<T, Color>` + `IColorBinder` | `Color` | принимает hex/HTML-строку цвета |
| `TargetBinder<T, Quaternion>` + `IRotationBinder` | `Quaternion` | читает `Vector2`/`Vector3` как углы Эйлера, скаляр — как одинаковый угол по трём осям |

---

## Создание кастомного биндера

### Пример: биндер для Text.color

```csharp
using TMPro;
using UnityEngine;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;

// Ограничиваем режимы: только OneWay и OneTime
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public sealed class TextColorBinder : TargetBinder<TMP_Text, Color>, IColorBinder
{
    // Читаем и пишем цвет текста
    protected override Color Property
    {
        get => Target.color;
        set => Target.color = value;
    }
}
```

### Пример: биндер с обратной привязкой

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class CustomToggleBinder : MonoBinder, IBinder<bool>, IReverseBinder<bool>
{
    [SerializeField] private GameObject _indicator;

    // IBinder<bool> — получаем значение от ViewModel
    public void SetValue(bool value)
    {
        _indicator.SetActive(value);
    }

    // IReverseBinder<bool> — отправляем изменения в ViewModel
    public event Action<bool>? ValueChanged;

    // Вызвать при клике пользователя
    public void OnClick()
    {
        var newValue = !_indicator.activeSelf;
        _indicator.SetActive(newValue);
        ValueChanged?.Invoke(newValue);
    }
}
```

### Пример: генерик-биндер из кода

```csharp
using Aspid.MVVM.StarterKit;

// Без MonoBehaviour — для привязки из кода
var binder = new GenericOneWayBinder<string>(value =>
{
    Debug.Log($"Значение изменилось: {value}");
});
```

---

## [BindModeOverride]

Ограничивает режимы привязки, доступные в Inspector:

```csharp
// Только OneWay и OneTime
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public class MyBinder : MonoBinder { }

// Все режимы
[BindModeOverride(IsAll = true)]
public class UniversalBinder : MonoBinder { }
```

Если биндер не поддерживает обратную привязку (нет `IReverseBinder<T>`), ограничьте TwoWay и OneWayToSource.

---

## [UsedInModes]

Помечает сериализованное поле как используемое только в перечисленных режимах — в Inspector оно
становится серым, когда биндер привязан в другом режиме, и получает подсказку
`Not used in the current Mode.`:

```csharp
public class MyBinder : MonoBinder, IBinder<string>, IReverseBinder<string>
{
    [Tooltip("Возвращается, когда обратное преобразование не удалось.")]
    [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
    [SerializeField] private string _convertBackFallback = string.Empty;
}
```

Поле может лежать и на самом биндере, и внутри любого сериализуемого объекта, который биндер
держит — вложенного класса, конвертера, элемента массива. Режим берётся у **ближайшего** биндера
над полем: если биндер вложен в другой биндер, решает вложенный. Вне биндера поле остаётся
активным.

Атрибут ничего не меняет в рантайме — он только для Inspector, и в сборку без `UNITY_EDITOR`
не попадает.

---

## DebugLogBinder

Утилитарный биндер для отладки — логирует все получаемые значения:

```csharp
// DebugLogBinder поддерживает все режимы и все типы данных
// Добавьте его в Inspector рядом с обычным биндером
// для мониторинга значений
```

Реализует `IAnyBinder` и `IAnyReverseBinder`, принимает любой тип данных.

---

## См. также

- [View](05-views.md) — объявление биндеров в View
- [ViewModel](04-viewmodels.md) — свойства для привязки
- [StarterKit](StarterKit/README.md) — все готовые биндеры
- [Конвертеры](08-converters.md) — преобразование значений

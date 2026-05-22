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
    public virtual bool IsBind => true;  // Можно отключить привязку
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
public abstract class TargetBinder<TTarget, TProperty, TConverter> : ...
{
    // Конвертер назначается через Inspector ([SerializeReference])
    protected TConverter Converter { get; }
}
```

**Специализированные базовые классы:**

| Класс | Тип Property | Доп. возможности |
|-------|-------------|-----------------|
| `TargetBoolBinder<T>` | `bool` | `_isInvert` — инверсия |
| `TargetFloatBinder<T>` | `float` | `INumberBinder` — принимает int/long/double |
| `TargetIntBinder<T>` | `int` | `INumberBinder` |
| `TargetVector3Binder<T>` | `Vector3` | `IVectorBinder`, `INumberBinder` (scalar → Vector3) |
| `TargetColorBinder<T>` | `Color` | `IColorBinder` |

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
public sealed class TextColorBinder : TargetColorBinder<TMP_Text>
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

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

Все интерфейсы биндеров наследуют `IBinder.IsBound`. Собственная реализация устанавливает его при успешной привязке, оставляет false при пропуске или откате попытки и сбрасывает при очистке. Привязка `OneTime` остаётся логически активной до `Unbind()`, даже без постоянной подписки. `Binder` и `MonoBinder` управляют этим состоянием автоматически.

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

    public void Bind(in BindContext context);
    public void Unbind(in UnbindContext context);

    // Виртуальные хуки:
    protected virtual void OnBinding() { }
    protected virtual void OnAttach() { }
    protected virtual void OnBound() { }
    protected virtual void OnUnbinding() { }
    protected virtual void OnDetach() { }
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

Runtime `AddComponent` использует `DefaultMode` биндера без Editor `Reset`. Настройте `binder.Mode` до привязки; setter запрещает изменения во время привязки и в привязанном состоянии. Сохранённые режимы не меняются.

`Binder.Bind` и `MonoBinder.Bind` выбрасывают `InvalidOperationException` при повторной или рекурсивной привязке, сохраняя существующее подключение. Все перегрузки `BindSafely` / `UnbindSafely`, включая одиночные биндеры, логируют исключения без повторного выбрасывания. Генерируемые View продолжают инициализацию после ошибки отдельного биндера; ошибка в lifecycle-hook самой View по-прежнему вызывает откат.

Жизненный цикл использует четыре отдельных состояния: `Unbound`, `Binding`, `Bound` и `Unbinding`. Установка проходит через `OnBinding → Adder.Add → OnAttach → Bound → OnBound`. `OnAttach` устанавливает обратные подписки после первого значения из ViewModel, пока `IsBound == false`. `OnBound` уведомляет о готовности и отправляет начальное значение для `OneWayToSource`, когда `IsBound == true`.

Ошибка установки до `Bound` запускает очистку частично приобретённых ресурсов. `OnUnbinding` и `OnDetach` должны допускать отсутствие ресурсов. Ошибка из `OnBound` распространяется выше без отката завершённой привязки.

Освобождение проходит через `Unbinding → OnUnbinding → Remove → OnDetach → Unbound → OnUnbound`. С начала очистки `IsBound == false`, но из `OnUnbinding` ещё можно отправить последнее значение. `OnDetach` освобождает подписки и ресурсы до сброса `Owner`; `OnUnbound` вызывается уже без владельца. Все шаги очистки выполняются даже после ошибки, затем одна ошибка пробрасывается исходной, а несколько собираются в `AggregateException`.

Каждый активный binder принадлежит одному владельцу. `BindContext` содержит `IBinderAdder Adder` и `object Owner`; `UnbindContext` содержит `object Owner`. Оба контекста являются `readonly struct`. Владельцем может быть View, составной binder или другой объект, отвечающий за очистку. `Unbind` сравнивает владельца через `ReferenceEquals` и ничего не делает для другого экземпляра: `Equals` и `GetHashCode` не используются.

```csharp
binder.Bind(new BindContext(member, owner));
binder.Unbind(new UnbindContext(owner));
```

`BindSafely` принимает `in BindContext` для найденного члена. Перегрузки с `FindBindableMemberResult` принимают результат и `owner`, создавая контекст только при успешном поиске. `UnbindSafely` принимает `in UnbindContext`; для диагностики используется владелец из контекста. Наследники могут использовать защищённое свойство `Owner`. Новый `Bind` разрешён только из `Unbound`; менять `Mode` можно только в этом состоянии. `Unbind` владельцем из `OnBinding`, первоначального `SetValue` или `OnAttach` запрещён, пока установка не завершена. Из `OnBound` можно отвязать и снова привязать биндер, а из `OnUnbound` сразу выполнить новый `Bind`. После вызова внешнего кода уведомление не должно менять ресурсы предыдущей привязки. Debug Rebind сохраняет владельца; уничтожение `MonoBinder` автоматически освобождает его подписки.

Один контекст можно передать всей коллекции:

```csharp
var context = new BindContext(member, owner);
binders.BindSafely(in context, memberName: "_values");

var unbindContext = new UnbindContext(this);
binders.UnbindSafely(in unbindContext, memberName: "_values");
```

Сгенерированные View вызывают `IBinder.Unbind(in context)` напрямую для одиночного биндера и `BinderExtensions.Unbind(collection, in context)` для коллекций. Для одиночного биндера расширение `Unbind` не требуется. Очистка пропускает null-ссылки, обрабатывает остальные биндеры после ошибок и затем пробрасывает исходное исключение или `AggregateException` при нескольких ошибках. Список привязок не хранится.

Ошибки очистки собираются на уровнях `Binder` / `MonoBinder`, коллекций, групп `MonoView`, сгенерированной View и инициализаторов. Одна ошибка сохраняет свой экземпляр и stack trace; несколько возвращаются в `AggregateException`. Для вложенных агрегатов используйте `exception.Flatten().InnerExceptions`. Список ошибок создаётся только при второй ошибке; успешный путь не получает дополнительных аллокаций от агрегации. При неудачной установке `Bind` и инициализации View первоначальная ошибка сохраняет приоритет над ошибками отката.

В `BindSafely(IEnumerable<T>)` и `UnbindSafely(IEnumerable<T>)` ошибки обхода и освобождения перечислителя также передаются вызывающему коду: одна сохраняет экземпляр и stack trace, одновременные ошибки обхода и `Dispose` собираются в `AggregateException`. Ошибки самих биндеров остаются отдельными записями в логе и не входят в этот агрегат. Успешные операции над предыдущими элементами не откатываются. При неудачном поиске члена коллекция не перечисляется.

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
var binder = new DelegateOneWayBinder<string>(value =>
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

`IsOne = true` добавляет `OneWay` и `OneTime`, а `IsTwo = true` добавляет `TwoWay` и `OneWayToSource`. Обе группы объединяются с явным списком. `[GenerateSerializableBinder]` применяет то же ограничение к сгенерированному конструктору. Null-список, `None` и неопределённые режимы недопустимы; для некорректного объявления override генератор сообщает `ASPIDSB006`.

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


## Вложенные биндеры

`[BinderLog]` и `[GenerateSerializableBinder]` поддерживают вложенные типы, если все контейнеры объявлены `partial`. Сериализуемая пара создаётся рядом с исходным MonoBinder, с теми же generic-параметрами и ограничениями; совпадение имени проверяется в том же контейнере и с тем же числом generic-параметров. Глобальное пространство имён также поддерживается.

`[AddBinderContextMenu]` допускает вложенный тип, доступный из текущей сборки, без открытых generic-параметров у него или его контейнеров. `private`/`protected` типы и открытые generic-типы вызывают `ASPIDMENU001`: перенесите атрибут на доступный компонент без generic-параметров или уберите атрибут. Ограничения Unity на компоненты и сериализацию продолжают действовать.

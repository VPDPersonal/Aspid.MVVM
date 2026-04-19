# Generic Binders

Делегат-биндеры для создания привязок из кода без MonoBehaviour.

---

## Обзор

Generic-биндеры принимают делегаты (`Action`, `Func`) и создают привязку программно. Используйте их когда нужен кастомный биндер без создания отдельного класса.

---

## GenericOneWayBinder\<T\>

Простейший биндер — принимает `Action<T>` для установки значения:

```csharp
var binder = new GenericOneWayBinder<string>(value =>
{
    Debug.Log($"Name changed: {value}");
});

view.BindCustomBinder("Name", binder);
```

### С таргетом: GenericOneWayBinder\<TTarget, T\>

```csharp
var label = GetComponent<TMP_Text>();
var binder = new GenericOneWayBinder<TMP_Text, string>(
    label,
    (target, value) => target.text = value
);
```

**Режимы:** OneWay, OneTime (TwoWay запрещён).

---

## GenericTwoWayBinder\<T\>

Двусторонний биндер — принимает `Action<T>` для установки и возвращает изменения через `ValueChanged`:

```csharp
var binder = new GenericTwoWayBinder<string>(
    initialize: callback =>
    {
        // Подписаться на изменения UI → вызвать callback(newValue)
        inputField.onEndEdit.AddListener(text => callback(text));
    },
    setValue: value =>
    {
        inputField.text = value;
    }
);
```

Дополнительные параметры:
- `onBoundValueChanged` — вызывается при привязке, возвращает начальное значение
- `onUnboundValueChanged` — вызывается при отвязке

**Режим:** TwoWay.

---

## GenericOneWayToSourceBinder\<T\>

Обратный биндер — отправляет значения из View в ViewModel:

```csharp
var binder = new GenericOneWayToSourceBinder<float>(
    initialize: callback =>
    {
        slider.onValueChanged.AddListener(v => callback(v));
    },
    onBoundValueChanged: () => slider.value  // начальное значение
);
```

**Режим:** OneWayToSource.

---

## GenericCasterBinder\<TFrom, TTo\>

Биндер с конвертацией типа через `IConverter<TFrom, TTo>`:

```csharp
var binder = new GenericCasterBinder<int, string>(
    setValue: text => label.text = text,
    converter: new IntToStringConverter()
);
```

**Режимы:** OneWay, OneTime.

---

## GenericOneTimeBinder\<T\>

Биндер, получающий значение один раз:

```csharp
var binder = new GenericOneTimeBinder<Config>(config =>
{
    ApplyConfig(config);
});
```

**Режим:** OneTime.

---

## Когда использовать

| Сценарий | Биндер |
|----------|--------|
| Простая реакция на изменение | `GenericOneWayBinder<T>` |
| Двусторонняя связь с кастомным компонентом | `GenericTwoWayBinder<T>` |
| Передача данных из UI в ViewModel | `GenericOneWayToSourceBinder<T>` |
| Конвертация типа при привязке | `GenericCasterBinder<TFrom, TTo>` |
| Одноразовая инициализация | `GenericOneTimeBinder<T>` |

---

## См. также

- [Value Binders](value-binders.md) — хранение привязанного значения
- [Биндеры](../06-binders.md) — обзор системы биндеров
- [Обзор StarterKit](README.md)

# Debug Binder

Биндер для отладки привязок.

---

## DebugLogBinder

Универсальный биндер, который логирует все операции в `Debug.Log`. Реализует `IAnyBinder` и `IAnyReverseBinder` — принимает **любой** тип данных.

### Что логирует

| Событие | Сообщение |
|---------|----------|
| `SetValue(value)` | `SetValue: {converted}` |
| Подписка `ValueChanged` | `Add ValueChanged: {callback}` |
| Отписка `ValueChanged` | `Remove ValueChanged: {callback}` |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_converter` | `IConverter<object, string>` — конвертер для отображения (по умолчанию `ObjectToStringConverter`) |

**Режимы:** все (OneWay, TwoWay, OneTime, OneWayToSource).

### Пример использования

Добавьте `DebugLogMonoBinder` на любой `MonoView` и привяжите к нужному свойству ViewModel через Inspector. Все изменения свойства будут логироваться в Console.

```csharp
// Или из кода:
var debugBinder = new DebugLogBinder();
view.BindCustomBinder("PlayerName", debugBinder);
// Console: "SetValue: John"
```

### Когда использовать

- Проверить, что привязка работает и значения доходят
- Отладить порядок вызовов SetValue
- Убедиться, что обратная привязка (Reverse) подписывается корректно

---

## См. также

- [Биндеры](../06-binders.md) — `[BinderLog]` для логирования существующих биндеров
- [Обзор StarterKit](README.md)

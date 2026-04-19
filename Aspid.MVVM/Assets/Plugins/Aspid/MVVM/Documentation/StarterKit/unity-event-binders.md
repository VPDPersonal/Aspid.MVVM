# UnityEvent Binders

Биндеры, которые транслируют значения из ViewModel в `UnityEvent`. Позволяют подписаться на изменения через Inspector.

---

## Обзор

UnityEvent-биндеры принимают значение от ViewModel и вызывают соответствующий `UnityEvent<T>`. Это позволяет реагировать на изменения ViewModel без написания кода — подписки настраиваются в Inspector.

---

## Типизированные биндеры

| Биндер | UnityEvent | Тип данных |
|--------|-----------|-----------|
| `UnityEventBoolMonoBinder` | `UnityEvent<bool>` | `bool` |
| `UnityEventFloatMonoBinder` | `UnityEvent<float>` | `float` |
| `UnityEventIntMonoBinder` | `UnityEvent<int>` | `int` |
| `UnityEventLongMonoBinder` | `UnityEvent<long>` | `long` |
| `UnityEventDoubleMonoBinder` | `UnityEvent<double>` | `double` |
| `UnityEventStringMonoBinder` | `UnityEvent<string>` | `string` |
| `UnityEventColorMonoBinder` | `UnityEvent<Color>` | `Color` |
| `UnityEventVector2MonoBinder` | `UnityEvent<Vector2>` | `Vector2` |
| `UnityEventVector3MonoBinder` | `UnityEvent<Vector3>` | `Vector3` |
| `UnityEventQuaternionMonoBinder` | `UnityEvent<Quaternion>` | `Quaternion` |
| `UnityEventEnumMonoBinder` | `UnityEvent<int>` | `enum` (как int) |

---

## Специальные биндеры

### UnityEventBoolByBindMonoBinder

Вызывает `UnityEvent<bool>` при **привязке/отвязке** биндера, а не при изменении значения:

- `OnBound` → `UnityEvent<bool>(true)`
- `OnUnbound` → `UnityEvent<bool>(false)`

Реализует `IAnyBinder` — принимает любой тип данных (тип значения игнорируется).

**Свойства:**
- `_isInvert` — инверсия: `true` при отвязке, `false` при привязке

**Когда использовать:** Показать/скрыть UI-элемент в зависимости от того, привязан ли биндер.

### UnityEventSwitcherMonoBinder

`bool` → выбор между двумя значениями → `UnityEvent<T>`.

### UnityEventNumberConditionMonoBinder

Числовое условие → `UnityEvent<bool>`. Сравнивает число с порогом.

### UnityEventNumberConditionSwitcherMonoBinder

Числовое условие → выбор между двумя значениями.

---

## Поддерживаемые режимы

Все UnityEvent-биндеры поддерживают **OneWay** и **OneTime**.

---

## Пример использования

```csharp
[ViewModel]
public partial class NotificationViewModel
{
    [OneWayBind] private bool _hasNewMessages;
    [OneWayBind] private string _message;
}
```

В Inspector:
1. Добавьте `UnityEventBoolMonoBinder` → привяжите к `HasNewMessages`
2. В UnityEvent подпишите метод, например `NotificationPanel.SetActive(bool)`
3. Добавьте `UnityEventStringMonoBinder` → привяжите к `Message`
4. В UnityEvent подпишите метод отображения сообщения

---

## См. также

- [Биндеры](../06-binders.md) — базовые понятия
- [Обзор StarterKit](README.md) — таблица всех компонентов

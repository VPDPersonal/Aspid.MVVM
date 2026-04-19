# Misc Binders

Прочие биндеры, не относящиеся к конкретной категории UI-компонентов.

---

## ObjectNameBinder

Привязка имени объекта `Object.name`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<string>` | Устанавливает имя |
| `IReverseBinder<string>` | Отправляет текущее имя (OneWayToSource) |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| Converter | `IConverter<string?, string?>` (опционально) |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class ItemViewModel
{
    [OneWayBind] private string _itemName;
    // ObjectNameBinder установит gameObject.name = itemName
}
```

---

## ComponentToSourceMonoBinder\<T\>

Базовый MonoBinder для отправки компонента из View в ViewModel. При привязке отправляет ссылку на компонент через `IReverseBinder<TComponent>`.

Используется для передачи Unity-компонентов в ViewModel:

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayToSourceBind] private Rigidbody _rigidbody;
}
// В View — ComponentToSourceMonoBinder<Rigidbody> привяжется к полю
```

### Готовые специализации

| Класс | Компонент |
|-------|----------|
| `SliderToSourceMonoBinder` | `Slider` |
| `DropdownToSourceMonoBinder` | `TMP_Dropdown` |
| `AudioSourceToSourceMonoBinder` | `AudioSource` |
| `RendererToSourceMonoBinder` | `Renderer` |
| `RectTransformToSourceMonoBinder` | `RectTransform` |

### Универсальный ComponentToSourceMonoBinder

Нетипизированный вариант — отправляет `Component` как `object` через `IAnyReverseBinder`. Подходит для любого компонента.

**Режим:** только OneWayToSource.

---

## ByBindMonoBinder

Паттерн «привязка по биндеру» — MonoBinder-обёртки, которые управляют целевым компонентом **другого** GameObject. Примеры:

| Класс | Описание |
|-------|----------|
| `GameObjectVisibleByBindMonoBinder` | Показывает/скрывает указанный GameObject |

---

## См. также

- [GameObject Binders](gameobject-binders.md)
- [Биндеры](../06-binders.md) — создание кастомных биндеров
- [Обзор StarterKit](README.md)

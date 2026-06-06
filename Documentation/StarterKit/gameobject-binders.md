# GameObject Binders

Биндеры для управления свойствами `GameObject`.

---

## GameObjectVisibleBinder

Привязка видимости через `GameObject.SetActive(bool)`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<bool>` | Устанавливает активность объекта |
| `IReverseBinder<bool>` | Отправляет текущее состояние (OneWayToSource) |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_isInvert` | Инвертирует значение: `true` → `SetActive(false)` |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

```csharp
[ViewModel]
public partial class PanelViewModel
{
    [OneWayBind] private bool _isVisible;
}
```

### Пример с инверсией

Если нужно скрыть объект когда значение `true`:

```csharp
[ViewModel]
public partial class LoadingViewModel
{
    [OneWayBind] private bool _isLoading;
    // isInvert=true → GameObject скрыт при isLoading=true
}
```

---

## GameObjectTagBinder

Привязка тега `GameObject.tag`.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<string>` | Устанавливает тег |
| `IReverseBinder<string>` | Отправляет текущий тег (OneWayToSource) |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| Converter | `IConverter<string?, string?>` (опционально) |

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## GameObjectTagSwitcherBinder

`bool` → выбор между двумя тегами.

**Режимы:** OneWay, OneTime.

---

## GameObjectVisibleByBindMonoBinder

MonoBinder-обёртка для `GameObjectVisibleBinder`. Позволяет привязать видимость целевого `GameObject` через Inspector.

Отличается от обычного `GameObjectVisibleBinder` тем, что позволяет управлять активностью **другого** `GameObject`, а не того, на котором находится биндер.

---

## Пример: показ/скрытие панелей

```csharp
[ViewModel]
public partial class UIViewModel
{
    [OneWayBind] private bool _showInventory;
    [OneWayBind] private bool _showMap;
    [OneWayBind] private bool _showSettings;
}
```

В View привяжите каждый `GameObjectVisibleBinder` к соответствующей панели. Панели будут автоматически показываться/скрываться при изменении свойств ViewModel.

---

## См. также

- [Canvas Group Binders](canvas-group-binders.md) — альтернатива через alpha/interactable
- [Switcher Binders](switcher-binders.md) — паттерн Switcher
- [Обзор StarterKit](README.md)

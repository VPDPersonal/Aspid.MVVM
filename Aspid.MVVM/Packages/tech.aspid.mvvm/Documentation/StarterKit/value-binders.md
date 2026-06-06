# Value Binders

Обёртки для хранения привязанного значения в коде (без MonoBehaviour).

---

## Обзор

Value-биндеры — это не-MonoBehaviour классы для получения значений из ViewModel в коде. Полезны когда нужно прочитать значение ViewModel программно, без UI.

---

## Типы

| Класс | Режим | Описание |
|-------|-------|----------|
| `OneWayValue<T>` | OneWay / OneTime | Хранит значение, event `Changed` |
| `TwoWayValue<T>` | TwoWay | Двусторонняя — можно менять из кода |
| `OneTimeValue<T>` | OneTime | Read-only после первой установки |
| `OneWayToSourceValue<T>` | OneWayToSource | Push из кода в ViewModel |

---

## OneWayValue\<T\>

```csharp
var healthValue = new OneWayValue<int>();

// Привязка к ViewModel
view.BindCustomBinder("Health", healthValue);

// Чтение значения
int current = healthValue.Value;

// Подписка на изменения
healthValue.Changed += newValue =>
{
    Debug.Log($"Health changed: {newValue}");
};

// Неявное приведение
int hp = healthValue; // implicit cast to T?
```

---

## TwoWayValue\<T\>

```csharp
var nameValue = new TwoWayValue<string>();

// Привязка...

// Чтение
string name = nameValue.Value;

// Запись — уведомляет ViewModel
nameValue.Value = "New Name";
```

При записи `Value` вызывается `ValueChanged`, который передаёт изменение обратно в ViewModel.

---

## Пример: использование в кастомном компоненте

```csharp
public class CustomComponent : MonoBehaviour
{
    private OneWayValue<bool> _isActive = new();

    public void Bind(IViewModel viewModel)
    {
        var result = viewModel.FindBindableMember(
            new FindBindableMemberParameters("IsActive"));

        if (result.IsFound)
            _isActive.Bind(result.Adder);
    }

    private void Update()
    {
        // Используем значение из ViewModel
        if (_isActive.Value)
            DoSomething();
    }
}
```

---

## См. также

- [Generic Binders](generic-binders.md) — делегат-биндеры из кода
- [Биндеры](../06-binders.md) — обзор системы биндеров

# Value Binders

Обёртки для хранения привязанного значения в коде (без MonoBehaviour).

---

## Обзор

Value-биндеры — это не-MonoBehaviour классы для получения значений из ViewModel в коде. Полезны когда нужно прочитать значение ViewModel программно, без UI.

---

## Типы

| Класс | Режим | Описание |
|-------|-------|----------|
| `ValueOneWayBinder<T>` | OneWay / OneTime | Хранит значение, event `Changed` |
| `ValueTwoWayBinder<T>` | TwoWay | Двусторонняя — можно менять из кода |
| `ValueOneTimeBinder<T>` | OneTime | Read-only после первой установки |
| `ValueOneWayToSourceBinder<T>` | OneWayToSource | Push из кода в ViewModel |

---

## ValueOneWayBinder\<T\>

```csharp
var healthValue = new ValueOneWayBinder<int>();

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

## ValueTwoWayBinder\<T\>

```csharp
var nameValue = new ValueTwoWayBinder<string>();

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
    private ValueOneWayBinder<bool> _isActive = new();

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

- [Delegate Binders](delegate-binders.md) — делегат-биндеры из кода
- [Биндеры](../06-binders.md) — обзор системы биндеров

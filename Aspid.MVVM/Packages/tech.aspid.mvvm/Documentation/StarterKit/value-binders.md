# Value Binders

Wrappers that hold a bound value in code (no MonoBehaviour).

---

## Overview

Value binders are non-MonoBehaviour classes for reading ViewModel values from code. Useful when a ViewModel value is needed programmatically, without UI.

---

## Types

| Class | Mode | Description |
|-------|-------|----------|
| `ValueOneWayBinder<T>` | OneWay / OneTime | Holds the value, `Changed` event |
| `ValueTwoWayBinder<T>` | TwoWay | Two-way, can be changed from code |
| `ValueOneTimeBinder<T>` | OneTime | Read-only after the first set |
| `ValueOneWayToSourceBinder<T>` | OneWayToSource | Push from code into the ViewModel |

---

## ValueOneWayBinder\<T\>

```csharp
var healthValue = new ValueOneWayBinder<int>();

// Bind to the ViewModel
view.BindCustomBinder("Health", healthValue);

// Read the value
int current = healthValue.Value;

// Subscribe to changes
healthValue.Changed += newValue =>
{
    Debug.Log($"Health changed: {newValue}");
};

// Implicit conversion
int hp = healthValue; // implicit cast to T?
```

---

## ValueTwoWayBinder\<T\>

```csharp
var nameValue = new ValueTwoWayBinder<string>();

// Bind...

// Read
string name = nameValue.Value;

// Write: notifies the ViewModel
nameValue.Value = "New Name";
```

Writing `Value` raises `ValueChanged`, which passes the change back to the ViewModel.

---

## Example: use in a custom component

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
        // Use the value from the ViewModel
        if (_isActive.Value)
            DoSomething();
    }
}
```

---

## See also

- [Delegate Binders](delegate-binders.md), delegate binders from code
- [Binders](../06-binders.md), the binder system overview

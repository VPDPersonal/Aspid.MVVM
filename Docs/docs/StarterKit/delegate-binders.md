# Delegate Binders

Delegate binders create bindings from code without a MonoBehaviour.

---

## Overview

Delegate binders take delegates (`Action`, `Func`) and build a binding programmatically. Use them when a custom binder is needed without a dedicated class.

---

## DelegateOneWayBinder\<T\>

The simplest binder: takes an `Action<T>` that applies the value:

```csharp
var binder = new DelegateOneWayBinder<string>(value =>
{
    Debug.Log($"Name changed: {value}");
});

view.BindCustomBinder("Name", binder);
```

### With a target: DelegateOneWayBinder\<TTarget, T\>

```csharp
var label = GetComponent<TMP_Text>();
var binder = new DelegateOneWayBinder<TMP_Text, string>(
    label,
    (target, value) => target.text = value
);
```

**Modes:** OneWay, OneTime (TwoWay is not allowed).

---

## DelegateTwoWayBinder\<T\>

A two-way binder: takes an `Action<T>` to apply the value and reports changes through `ValueChanged`:

```csharp
var binder = new DelegateTwoWayBinder<string>(
    subscribe: callback =>
    {
        // Subscribe to UI changes → call callback(newValue)
        inputField.onEndEdit.AddListener(text => callback(text));
    },
    setValue: value =>
    {
        inputField.text = value;
    }
);
```

Extra parameters:
- `getValueOnBound`: called on bind, returns the initial value
- `getValueOnUnbinding`: called on unbind

**Mode:** TwoWay.

---

## DelegateOneWayToSourceBinder\<T\>

A reverse binder: sends values from the View to the ViewModel:

```csharp
var binder = new DelegateOneWayToSourceBinder<float>(
    subscribe: callback =>
    {
        slider.onValueChanged.AddListener(v => callback(v));
    },
    getValueOnBound: () => slider.value  // initial value
);
```

**Mode:** OneWayToSource.

---

## CasterBinder\<TFrom, TTo\>

A binder with a type conversion through `IConverter<TFrom, TTo>`:

```csharp
var binder = new CasterBinder<int, string>(
    setValue: text => label.text = text,
    converter: new IntToStringConverter()
);
```

**Modes:** OneWay, OneTime.

---

## DelegateOneTimeBinder\<T\>

A binder that receives the value once:

```csharp
var binder = new DelegateOneTimeBinder<Config>(config =>
{
    ApplyConfig(config);
});
```

**Mode:** OneTime.

---

## When to use

| Case | Binder |
|----------|--------|
| A simple reaction to a change | `DelegateOneWayBinder<T>` |
| Two-way link with a custom component | `DelegateTwoWayBinder<T>` |
| Passing data from the UI to the ViewModel | `DelegateOneWayToSourceBinder<T>` |
| Type conversion while binding | `CasterBinder<TFrom, TTo>` |
| One-off initialization | `DelegateOneTimeBinder<T>` |

---

## See also

- [Value Binders](value-binders.md), holding the bound value
- [Binders](../06-binders.md), the binder system overview
- [StarterKit overview](README.md)

# Debug Binder

A binder for debugging bindings.

---

## DebugLogBinder

A universal binder that logs every operation to `Debug.Log`. Implements `IAnyBinder` and `IAnyReverseBinder`, so it accepts **any** data type.

### What it logs

| Event | Message |
|---------|----------|
| `SetValue(value)` | `SetValue: {converted}` |
| `ValueChanged` subscribed | `Add ValueChanged: {callback}` |
| `ValueChanged` unsubscribed | `Remove ValueChanged: {callback}` |

### Inspector properties

| Property | Description |
|----------|----------|
| `_converter` | `IConverter<object, string>` used for display (default `ObjectToStringConverter`) |

**Modes:** all (OneWay, TwoWay, OneTime, OneWayToSource).

### Usage

Add `DebugLogMonoBinder` to any `MonoView` and bind it to the ViewModel property in the Inspector. Every change of the property is logged to the Console.

```csharp
// Or from code:
var debugBinder = new DebugLogBinder();
view.BindCustomBinder("PlayerName", debugBinder);
// Console: "SetValue: John"
```

### When to use

- Check that a binding works and values arrive
- Debug the order of `SetValue` calls
- Make sure reverse binding subscribes correctly

---

## See also

- [Binders](../06-binders.md), `[BinderLog]` for logging existing binders
- [StarterKit overview](README.md)

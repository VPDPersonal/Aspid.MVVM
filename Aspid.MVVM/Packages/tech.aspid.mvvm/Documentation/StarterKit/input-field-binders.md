# InputField Binders

Binders for `TMP_InputField` with two-way binding, numeric types and the various field events.

---

## InputFieldBinder

The main text input binder.

| Interface | Description |
|-----------|----------|
| `IBinder<string?>` | Receives text from the ViewModel |
| `IReverseBinder<string>` | Sends changes back |
| `INumberBinder` | Accepts numeric types |
| `INumberReverseBinder` | Sends numbers back |

### Inspector properties

| Property | Description |
|----------|----------|
| UpdateEvent | Update event: `OnValueChanged`, `OnEndEdit`, `OnSubmit`, `OnSelect`, `OnDeselect` |
| Converter | `IConverter<string?, string?>` (optional) |

### Loop protection

The `_isNotifyValueChanged` flag prevents infinite recursion in TwoWay binding: while the ViewModel updates the InputField, the reverse event is blocked.

### Numeric modes

With ContentType `IntegerNumber` or `DecimalNumber` the string is parsed into a number and sent through `INumberReverseBinder`.

**Modes:** OneWay, TwoWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [TwoWayBind] private string _userName;
}
```

---

## Additional binders

| Binder | Binds | Type |
|--------|------------|-----|
| `InputFieldCharacterValidationBinder` | `characterValidation` | `CharacterValidation` |
| `InputFieldContentTypeBinder` | `contentType` | `ContentType` |
| `InputFieldInputTypeBinder` | `inputType` | `InputType` |
| `InputFieldLineTypeBinder` | `lineType` | `LineType` |

Each has a Switcher variant (`bool` → one of two values). `ForceLabelUpdate` is called after the write.

| Binder | Binds | Type |
|--------|------------|-----|
| `InputFieldCharacterLimitBinder` | `characterLimit` | `int`, `0` for no limit |
| `InputFieldCaretPositionBinder` | `caretPosition` | `int`, clamped to the text length |
| `InputFieldReadOnlyBinder` | `readOnly` | `bool` |
| `InputFieldPlaceholderBinder` | `placeholder` | `Graphic` |

---

## InputFieldCommandBinder

Executes a command on the chosen field event (`UpdateInputFieldEvent`): an `IRelayCommand` without arguments or an `IRelayCommand<string>` with the text. The `<T>`, `<T1, T2>`, `<T1, T2, T3>` variants pass extra parameters after the text. `InteractableMode` works as in `ButtonCommandBinder`.

---

## See also

- [Text Binders](text-binders.md)
- [Binding Modes](../03-binding-modes.md), TwoWay
- [StarterKit overview](README.md)

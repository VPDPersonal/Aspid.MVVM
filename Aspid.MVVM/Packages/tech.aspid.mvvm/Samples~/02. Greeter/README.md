# Greeter

The user types a name and the greeting updates on every keystroke. No buttons, no bootstrap code.

**You learn:** `MonoViewModel`, `[TwoWayBind]`, `[BindAlso]`, `On*Changed` hooks.

**Assumes:** [Counter](../01.%20Counter/README.md).

Scene: `Scenes/Greeter.unity`. Script: `Scripts/GreeterMonoViewModel.cs`.

## What we build

```
[ Type a name... ]   →   Hi, Vlad!     [ Clear ]
    InputField             Text
```

## Two directions of data

Counter moved data one way: ViewModel → UI. Here the input field writes back:

```
ViewModel ──────────► UI      OneWay:  Greeting
ViewModel ◄─────────► UI      TwoWay:  Name
```

## ViewModel on a GameObject

```csharp
[ViewModel]
public sealed partial class GreeterMonoViewModel : MonoViewModel
{
    [BindAlso(nameof(Greeting))]
    [TwoWayBind]
    [SerializeField] private string _name;

    private string Greeting =>
        string.IsNullOrEmpty(Name)
            ? string.Empty
            : $"Hi, {Name}!";

    [RelayCommand]
    private void Clear() =>
        Name = string.Empty;
}
```

- `MonoViewModel` is a component. `ViewInitializer` in the scene connects it to the `MonoView`, so there is no `Bootstrap`.
- `[TwoWayBind]` lets the `InputFieldMonoBinder` write into `Name`.
- `[BindAlso(nameof(Greeting))]` re-sends the computed `Greeting` whenever `Name` changes. The property itself needs no attribute.
- `Clear` sets `Name` from the ViewModel side; because the binding is two-way, the input field empties too.

| | `[OneWayBind]` | `[TwoWayBind]` |
|---|---|---|
| ViewModel → UI | yes | yes |
| UI → ViewModel | no | yes |
| Typical binders | Text, counters, state | InputField, Toggle, Slider |

## The other way: `On*Changed`

When the reaction is an action rather than a derived value, use the generated hook instead of `[BindAlso]`:

```csharp
[ViewModel]
public sealed partial class GreeterViewModel
{
    [TwoWayBind] private string _name = "";
    [OneWayBind] private string _greeting = "Type a name";

    partial void OnNameChanged(string newValue) =>
        Greeting = string.IsNullOrEmpty(newValue) ? "Type a name" : $"Hi, {newValue}!";
}
```

The generator declares `partial void OnNameChanged(string newValue)` and calls it from the `Name` setter. The whole chain is direct calls:

```
InputField changed → binder sets Name → OnNameChanged → Greeting set → TextMonoBinder updates
```

Both styles are fine. `[BindAlso]` reads better for derived values, `On*Changed` for side effects.

## Scene

```
Greeter
├── GreeterMonoViewModel   (GreeterMonoViewModel.cs)
└── Greeter UI
    ├── MonoView + ViewInitializer
    ├── Name Input         (TMP_InputField + InputFieldMonoBinder)
    ├── Greeting Text      (TextMonoBinder)
    └── Clear Button       (Button + ButtonCommandMonoBinder)
```

`ViewInitializer` points at the `GreeterMonoViewModel` component and the `MonoView`. The `MonoView` lists binders by id (`Name`, `Greeting`, `ClearCommand`).

## Styling the greeting with converters

Coloring the name is a View concern, so it does not belong in the ViewModel. `TextMonoBinder` has a **Converter** slot. Pick **Aspid → Composition → Sequence** and add two links:

1. **Aspid → String → Rich Text Color** wraps the text in a `<color>` tag.
2. **Aspid → String → String Format** with `{0}!` appends an exclamation mark outside the tag.

> [!WARNING]
> TextMeshPro executes markup inside any string. If `Name` can be seen by other players, put `RichTextSanitizeConverter` or `RichTextNoParseConverter` first in the chain.

## Summary

| Concept | What we did |
|---|---|
| `MonoViewModel` | ViewModel as a component, wired by `ViewInitializer` |
| `[TwoWayBind]` | `Name` syncs with the input field in both directions |
| `[BindAlso]` | `Greeting` re-sent on every `Name` change |
| `On*Changed` | Reaction hook without explicit subscriptions |
| Converters | Formatting stays in the View |

Next: [Bind Modes](../03.%20BindModes/README.md), all four modes on one screen.

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.

# Dynamic ViewModel

`DynamicViewModel` builds a typed ViewModel at runtime, without a dedicated class or the Source
Generator. It is meant for View tests, prototypes, debug screens and interfaces whose shape comes
from configuration.

For regular production screens use `[ViewModel]`: the generated type expresses a fixed schema
better, checks identifiers at compile time and supports `[RelayCommand]`.

## Quick start

`Add<T>` returns a typed property handle:

```csharp
var viewModel = new DynamicViewModel();

IDynamicProperty<int> health = viewModel.Add("Health", 100);
IDynamicProperty<string> name = viewModel.Add("Name", "Hero", BindMode.TwoWay);
IDynamicProperty<Sprite> icon = viewModel.Add("Icon", heroSprite, BindMode.OneTime);

view.Initialize(viewModel);

health.Value = 75;             // The View receives 75
Debug.Log(name.Value);         // Current value, including input from the View
```

The default is a `OneWay` property.

## Collection initializer

`DynamicViewModel` supports a compact syntax when handles are not needed:

```csharp
var viewModel = new DynamicViewModel
{
    { "Title", "Settings" },
    { "Volume", 0.8f, BindMode.TwoWay },
    { "Icon", settingsIcon, BindMode.OneTime }
};
```

The number of properties is unlimited. Unlike the old `Create<T1, ..., T8>`, new property types need
no extra overloads.

## Reading and writing

### Through a typed handle

```csharp
var score = viewModel.Add("Score", 0);

score.ValueChanged += value => Debug.Log($"Score: {value}");
score.Value = 10;
```

Setting an equal value sends nothing and does not raise `ValueChanged`.

### Through the ViewModel

```csharp
IDynamicProperty<int> property = viewModel.Get<int>("Score");
property.Value = 20;

if (viewModel.TryGet<int>("Score", out var scoreProperty))
    scoreProperty.Value = 30;

viewModel["Score"].UntypedValue = 40;   // no generic parameter, for config-driven code
```

`Get<T>` throws:

- `KeyNotFoundException` when the id is missing;
- `ArgumentException` when the property exists but has another type.

`TryGet<T>` returns `false` in both cases.

## Modes

| Mode | Behaviour |
|---|---|
| `OneWay` | The initial value and later changes go from the ViewModel to the View |
| `TwoWay` | The value syncs both ways; `Value` always holds the current View input |
| `OneTime` | Every new binder receives the current value once; already attached binders are not updated |
| `OneWayToSource` | Accepts changes from the View without subscribing the View to later changes |
| `None` | Not supported, rejected by the constructor |

The property mode defines what the bindable member can do. The mode of a concrete binder is still
set on the binder and must be compatible with those capabilities.

## Untyped access

For configurations where the type is only known at runtime there are `IDynamicProperty`,
`Properties` and the indexer:

```csharp
foreach (IDynamicProperty property in viewModel.Properties)
{
    Debug.Log($"{property.Id}: {property.ValueType.Name} = {property.UntypedValue}");
}

viewModel["Title"].UntypedValue = "Profile";
```

Writing a value of an incompatible type throws `ArgumentException`. Writing `null` sets `default(T)`.

A custom property can implement `IDynamicProperty` and be added with the same method:

```csharp
viewModel.Add(customProperty);
```

## Identifier checks

Empty ids and duplicates are rejected immediately. Ids are case-sensitive by default
(`StringComparer.Ordinal`), but the comparer can be replaced:

```csharp
var viewModel = new DynamicViewModel(
    idComparer: StringComparer.OrdinalIgnoreCase);
```

Normally a binder id that is missing means the binding is simply not created. In tests and while
building a screen a strict mode is handy:

```csharp
var viewModel = new DynamicViewModel(throwOnMissingMember: true);
```

Then a request for an unknown id throws `KeyNotFoundException` naming the missing property.

## Adding properties and the View lifecycle

Add properties before `view.Initialize(viewModel)`. `Add` after initialization makes the property
available to later lookups, but an already initialized View does not re-run its binder lookup on its own.

## When to use

Good fits:

- isolated View tests;
- a quick prototype;
- debug/admin UI;
- properties assembled from JSON or configuration;
- small reusable Views with a runtime set of fields.

Do not use `DynamicViewModel` as a replacement for a regular ViewModel with a fixed schema. String ids
move some errors from compile time to runtime, and business logic, commands and dependencies are
better expressed by a dedicated type.

## Compared to a generated ViewModel

| Aspect | Source Generator | `DynamicViewModel` |
|---|:---:|:---:|
| Fixed schema | Best fit | Overkill |
| Runtime schema | Needs a new type | Supported |
| Compile-time id check | Yes | No |
| Typed value reads | Yes | Yes |
| Update and observe | Yes | Yes |
| `[RelayCommand]` and generated hooks | Yes | No |
| Binder resolution | Generated code | One dictionary lookup |

The dictionary lookup happens when a binder is attached, not on every value change.

## See also

- [ViewModels](04-viewmodels.md), generated ViewModels
- [Binding Modes](03-binding-modes.md): OneWay, TwoWay, OneTime and OneWayToSource

---
name: starterkit-layout
description: Where a new file goes inside `Packages/tech.aspid.mvvm/StarterKit/` and how to name it — the Core/General/Helpers/thematic folder taxonomy, singular binder folders, `ToBool/ToString/ToValue` subfolders, where extensions and shared helpers live, type and enum naming, `.meta` files. Use when creating a binder, converter or helper, or moving a file inside StarterKit, when the user asks "where does this go", "what do I call it", or adds a folder.
---

# StarterKit layout and naming

One assembly, `Aspid.MVVM.StarterKit` (`StarterKit/Runtime`), with editor code next to it in `StarterKit/Editor`. Every family (`Binders/`, `Converters/`, `Commands/`, `Collections/`, `Views/`) follows the same structure.

## Taxonomy inside a family

| Folder | Content | Examples |
|---|---|---|
| `Core/` | Infrastructure the family stands on: base interfaces, declarative generation attributes | `IConverter`, `ITwoWayConverter`, `ICanExecuteHandler`, `GenerateSerializableBinderAttribute`, `IViewFactory` |
| `General/` | Types with logic shared across the family: abstract bases, helper interfaces that cut duplication | `Binder`, `TargetBinder`, `INumberBinder`, `IColorBinder`, `Fallback/ConverterFallback`, `Numbers/NumberConverter` |
| `General/Mono/` | Abstract Mono bases, kept apart from the serializable ones | `MonoBinder`, `ComponentMonoBinder`, `Enum/EnumMonoBinder`, `Addressable/AddressableMonoBinder` |
| `Helpers/` | Plain helpers: `*Mode` enums, loggers, math | `ComparisonMode`, `ConverterLogger`, `BinderLogger`, `BinderMath`, `ShaderPropertyId` |
| Thematic | Concrete implementations only; subfolders by target type `ToBool/`, `ToString/`, `ToValue/`, `ToVector/` | `Objects/ToBool/EqualityToBoolConverter`, `Graphic/Color/GraphicColorMonoBinder` |

`INumberBinder`/`IColorBinder` are not `Core`: they cut duplication for some binders rather than holding the family up.

## Placement rules

- **Shared by the whole assembly** (binders and converters alike) goes to the root `Runtime/Helpers/<Topic>/`: `Globalization/CultureInfoMode`, `Logging/LogMessageText`, `Numeric/NumericSaturation`, `Collections/EnumerableCountExtensions`, `Time/UnixTime`, `Transforms/`, `Colors/ColorChannels`.
- "Shared" means **potential** applicability, not current callers. An `IEnumerable` count helper is shared even if only converters use it today. `ComparisonMode` is converter-only → `Converters/Helpers`.
- **Extensions sit next to the extended type**, not in `Helpers`: `ConverterFallbackExtensions` in `General/Fallback/`, `FuncConverterExtensions` beside `FuncConverter`, `AudioSourceExtensions` at the root of `Binders/AudioSource/`.
- An enum or extension needed by one binder group lives in that group's subfolder, beside its consumers.
- **No `Mono/` subfolder in thematic binder folders**: a `Binder` and its `MonoBinder` sit side by side (`Object/ObjectNameBinder`, `Object/ObjectNameMonoBinder`).
- **No `OneWayToSource/` folder**: `*ToSourceMonoBinder` sits at the root of its subfolder.
- Abstract bases of concrete families (`SwitcherMonoBinder`, `EnumMonoBinder`, `CasterMonoBinder`) go to `General/` or the root of their family (`Caster/CasterMonoBinder`), never into a thematic subfolder.
- Editor code (drawers, editors) goes to `StarterKit/Editor/<Family>/…`, mirroring runtime.

## Folder names

- Binders: **singular** (`Binders/Command`, `Caster`, `Collection`, `Graphic`, `AudioSource`, `Collider2D`). Exceptions: `General`, `Helpers`.
- Converters: plural (`Converters/Numbers`, `Strings`, `Vectors`, `Bounds`).
- Inside a group, a subfolder per property or operation: `AudioSource/Clip`, `Collider/Box`, `Graphic/ColorChannel`, `Numbers/ToString/Plural`.

## Type names

- The name says what the type does. No `Generic` prefix: `CasterBinder`, `ValueToStringConverter`.
- The name does not lie about scope: text used by binders too is `LogMessageText`, not `ConverterMessageText`.
- Short `XxxText`-style names are fine only for internal helpers; a public type explains itself fully.
- Settings enums take a role suffix: `*Mode` (`ComparisonMode`, `ColorBlendMode`, `SizeDeltaMode`), `*Operation` (`LogicOperation`, `ChannelOperation`), flags in plural (`ColorChannels`). Members are consistent (`Equal`/`NotEqual`, not `Equal`/`NotEquals`).
- Nested record-like types are named by meaning (`OptionEntry`, not `Entry`).
- Converter naming ("To", the numeric family): skill `starterkit-converter-authoring`; binder naming (`Mono`, `ToSource`, `Switcher`): `starterkit-binder-authoring`.

## `.meta` and git

- Every new file and folder gets a `.meta` (folder: `folderAsset: yes`, fresh guid). Move with `git mv` together with the `.meta`.
- Do not stage new files: the user reviews them as untracked first.
- Rename serialized types and fields directly, without `[FormerlySerializedAs]`: the user has a tool that repairs serialized references.
- After a move, update references: other scripts, tests, `Documentation/*.md`, `Samples~`.

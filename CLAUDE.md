# Aspid.MVVM

A high-performance Source Generator-based MVVM framework for Unity. Enables clean separation between View, ViewModel, and business logic with zero reflection in bindings and minimal memory allocations.

## Project Structure

```
Projects/Aspid.MVVM/
├── Aspid.MVVM/                        # Main Unity project
│   └── Assets/Plugins/Aspid/
│       ├── MVVM/
│       │   ├── Source/                # Core framework implementation
│       │   │   ├── ViewModels/        # IViewModel interface, attributes, extensions
│       │   │   ├── BindableMembers/   # Binding member resolution
│       │   │   ├── Binders/           # Data binding implementations
│       │   │   ├── Commands/          # IRelayCommand interfaces and implementations
│       │   │   ├── Mode/              # BindMode enum (OneWay, TwoWay, OneTime, OneWayToSource)
│       │   │   ├── Views/             # View components and initialization
│       │   │   ├── Generation/        # Attributes for source generation
│       │   │   ├── Exceptions/        # Framework-specific exceptions
│       │   │   └── Helpers/           # Utility functions
│       │   ├── StarterKit/Runtime/    # Ready-to-use components
│       │   │   ├── Binders/           # UI binders (Text, Image, Toggle, Slider, etc.)
│       │   │   ├── Converters/        # Value converters
│       │   │   ├── Collections/       # Virtualized lists, dynamic collections
│       │   │   ├── Commands/          # Command implementations
│       │   │   └── ViewModels/        # DynamicViewModel
│       │   ├── Unity/                 # Unity integration layer
│       │   └── Samples/               # HelloWorld, Stats, TodoList, VirtualizedList
│       ├── Collections/Observable/    # ObservableList, ObservableDict, etc. (submodule)
│       ├── Internal/Unity/            # Internal editor utilities (submodule)
│       └── UnityFastTools/            # Performance utilities
├── Aspid.MVVM.Generators/             # Roslyn source generator (submodule)
│   ├── Aspid.MVVM.Generators/         # Generator implementation
│   └── Aspid.MVVM.Generators.Tests/   # Generator unit tests
├── Aspid.MVVM.Analyzers/              # Roslyn code analyzer (submodule)
│   ├── Aspid.MVVM.Analyzers/          # Analyzer implementation
│   └── Aspid.MVVM.Analyzers.Tests/    # Analyzer unit tests
├── Aspid.MVVM.Unity.Generators/       # Unity-specific generators (submodule)
└── Readme.md
```

## Key Technologies

- **C# / .NET Standard 2.0** — core framework
- **Roslyn Incremental Source Generators** — code generation at build time
- **Unity 2022.3+** — target runtime
- **Zenject / VContainer** — DI framework integrations
- **UniTask** — async utilities
- **.NET 9.0 SDK** — for generator/analyzer projects (`global.json`)

## Architecture

The framework uses Source Generators to eliminate boilerplate and reflection:

1. **`[ViewModel]`** — marks a `partial` class for source generation; generates `IViewModel` implementation
2. **`[Bind]`** — marks fields for binding generation; supports `BindMode` argument
3. **`[RelayCommand]`** — transforms a method into an `IRelayCommand` property
4. **Binders** — MonoBehaviour components that connect View UI elements to ViewModel bindable members
5. **Observable Collections** — thread-safe, change-notifying collections with filtering/sorting

## Code Conventions

**Naming:**
- `PascalCase` for classes, interfaces, methods, properties
- `camelCase` for local variables and parameters
- `_camelCase` prefix for private fields
- `I` prefix for interfaces (`IViewModel`, `IRelayCommand`)
- `Binder` suffix for binding components, `Attribute` suffix for attributes

**Namespaces:**
- Root: `Aspid.MVVM`
- Sub-namespaces: `Aspid.MVVM.StarterKit`, `Aspid.Collections.Observable`
- Editor code lives in `.Editor` sub-namespaces

**Code Style:**
- All classes requiring source generation must be `partial`
- Explicit access modifiers on all members
- Nullable reference types (`T?` for optional values)
- XML documentation comments on all public APIs
- ReSharper suppression comments on generated/special code

## Solutions

| Solution | Purpose |
|----------|---------|
| `Aspid.MVVM.sln` | Main Unity project (73 projects) |
| `Aspid.MVVM.Generators/Aspid.MVVM.Generators.sln` | Source generator |
| `Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers.sln` | Roslyn analyzer |
| `Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators.sln` | Unity generators |

## Testing

- **Generator tests**: `Aspid.MVVM.Generators/Aspid.MVVM.Generators.Tests/`
- **Analyzer tests**: `Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers.Tests/`
- **Runtime tests**: Unity Test Framework, Zenject integration tests
- **Manual validation**: Sample projects (HelloWorld, TodoList, Stats, VirtualizedList)

## Git Submodules

The project uses 5 git submodules. After cloning, initialize with:

```bash
git submodule update --init --recursive
```

Submodules:
- `Aspid.MVVM.Generators`
- `Aspid.MVVM.Analyzers`
- `Aspid.MVVM.Unity.Generators`
- `Aspid.Internal.Unity`
- `Aspid.Collections`

## Branch Strategy

- `main` — stable releases; target for PRs
- `Version/Aspid.MVVM-x.x.x` — version development branches
- `Feature/*` — feature branches

## Documentation

- Full docs: https://vpd-inc.gitbook.io/aspid.mvvm/
- Unity Asset Store: https://assetstore.unity.com/packages/slug/298463
- Key entry points for new contributors:
  - `Readme.md` — project overview
  - `Aspid.MVVM/Assets/Plugins/Aspid/MVVM/Samples/HelloWorld/` — minimal examples
  - `Aspid.MVVM.Generators/Aspid.MVVM.Generators/ViewModelGenerator.cs` — source generator pipeline
  - `Source/ViewModels/IViewModel.cs` — core interface

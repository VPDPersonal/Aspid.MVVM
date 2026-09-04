# Aspid.MVVM Documentation

The complete guide to the Aspid.MVVM framework for Unity. Rendered at https://vpdpersonal.github.io/Aspid.MVVM/.

## Contents

### Basics

1. [Getting Started](01-getting-started.md): installation, the first ViewModel and View
2. [Architecture](02-architecture.md): the MVVM pattern, Source Generation, the binding pipeline
3. [Binding Modes](03-binding-modes.md): OneWay, TwoWay, OneTime, OneWayToSource

### Core concepts

4. [ViewModels](04-viewmodels.md): creating a ViewModel, `[Bind]`, `[BindAlso]`, `[Access]`, change handlers
5. [Views](05-views.md): creating a View, MonoView, `[AsBinder]`, lifecycle
6. [Binders](06-binders.md): IBinder, MonoBinder, custom binders
7. [Commands](07-commands.md): IRelayCommand, `[RelayCommand]`, CanExecute, parameters
8. [Converters](08-converters.md): IConverter, the built-in converter catalogue
9. [Collections](09-collections.md): ObservableList, ObservableDictionary, FilteredList, synchronization

### Advanced

10. [Dynamic ViewModel](10-dynamic-viewmodel.md): a ViewModel built at runtime without code generation
11. [View Initializers](11-view-initializers.md): View initialization from the Inspector
12. [DI Integration](12-di-integration.md): Zenject and VContainer
13. [Analyzers](13-analyzers.md): Roslyn analyzers and code fixes
14. [Best Practices](14-best-practices.md): recommendations and common mistakes

### StarterKit: ready-made components

- [StarterKit overview](StarterKit/README.md): every component in one table
- [Text](StarterKit/text-binders.md): TextBinder, TextSwitcherBinder, fonts, sizes
- [InputField](StarterKit/input-field-binders.md): InputFieldBinder, validation, events
- [Image](StarterKit/image-binders.md): ImageSpriteBinder, ImageFillBinder
- [Button / Command](StarterKit/button-command-binders.md): ButtonCommandBinder, InteractableMode
- [Slider](StarterKit/slider-binders.md): SliderValueBinder, SliderMinMaxBinder
- [Toggle](StarterKit/toggle-binders.md): ToggleIsOnBinder
- [Dropdown](StarterKit/dropdown-binders.md): DropdownValueBinder, DropdownOptionsBinder
- [GameObject](StarterKit/gameobject-binders.md): GameObjectVisibleBinder
- [Transform](StarterKit/transform-binders.md): position, rotation, scale, RectTransform
- [CanvasGroup](StarterKit/canvas-group-binders.md): Alpha, BlocksRaycasts, Interactable
- [Animator](StarterKit/animator-binders.md): SetBool, SetFloat, SetInt, SetTrigger
- [Graphic / Renderer](StarterKit/graphic-binders.md): color, materials
- [AudioSource](StarterKit/audio-source-binders.md): volume, clip and other properties
- [Collider](StarterKit/collider-binders.md): Box, Capsule, Sphere, Mesh
- [UnityEvent](StarterKit/unity-event-binders.md): UnityEvent binders for every type
- [Collections](StarterKit/collection-binders.md): Observable/Virtualized list binders
- [Switcher](StarterKit/switcher-binders.md): the true/false → value pattern
- [Caster](StarterKit/caster-binders.md): converting binders
- [Value](StarterKit/value-binders.md): ValueOneWayBinder, ValueTwoWayBinder
- [Delegate](StarterKit/delegate-binders.md): code binders through delegates
- [Debug](StarterKit/debug-binder.md): DebugLogBinder for debugging
- [View Factories](StarterKit/view-factories.md): PrefabViewFactory, PrefabViewPool
- [Misc](StarterKit/misc-binders.md): ObjectNameBinder, ComponentToSourceMonoBinder

### Tutorials

Every sample's `README.md` is its tutorial.

#### Path (samples 1–6)

- [1. Counter](../Samples~/01.%20Counter/README.md): `[ViewModel]`, `[Bind]`, `[RelayCommand]`, `ViewInitializer`
- [2. Greeter](../Samples~/02.%20Greeter/README.md): `MonoViewModel`, `[TwoWayBind]`, `[BindAlso]`, `On*Changed`
- [3. Bind Modes](../Samples~/03.%20BindModes/README.md): four modes on one screen, your own `ITwoWayConverter`
- [4. Stats](../Samples~/04.%20Stats/README.md): commands with a parameter, `CanExecute`, draft → model
- [5. Todo List](../Samples~/05.%20TodoList/README.md): `ObservableList`, `CreateSync`, collection binders
- [6. Custom Binder](../Samples~/06.%20CustomBinder/README.md): a binder for your own component, `[GenerateSerializableBinder]`

#### Feature showcases

- [Virtualized List](../Samples~/VirtualizedList/README.md): virtualization, `FilteredList`
- [Dynamic ViewModel](../Samples~/DynamicViewModel/README.md): properties by id without generation
- [DI Integration](../Samples~/DiIntegration/README.md): Zenject and VContainer through `ViewInitializer`
- [API Reference Scripts](../Samples~/ExampleScripts/README.md): compilable snippets for every attribute

## Links

- [Unity Asset Store](https://assetstore.unity.com/packages/slug/298463)
- [Documentation site](https://vpdpersonal.github.io/Aspid.MVVM/)

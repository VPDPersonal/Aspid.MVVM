---
title: Aspid.MVVM Wiki
type: index
status: active
last_commit: "fd79935291c747db1c6a60b7ee8943fbd4a056a2"
submodule_commits:
  generators: "56d54513be83725300bd35c55aa1a2ec63503d54"
  unity_generators: "d0235ae55fbcab459e79bafdd4981a0083cd5d49"
  analyzers: "ca683bb433811b02227c6180502d297587d0f878"
updated_at: 2026-05-31
---

# Aspid.MVVM Wiki

> Navigable knowledge base for the Aspid.MVVM framework. **Start here. Read the wiki before reading source.** English is the source of truth; the Russian translation lives in `docs/wiki-ru/`.

## Orientation — the 8 questions

1. **What is this?** A Source-Generator-based MVVM framework for Unity: clean View / ViewModel / business-logic separation, zero reflection in bindings, minimal allocations. → [[Architecture]]
2. **How do I get started?** → [[Getting Started]] · the [[Samples]] (Counter, Greeter, HelloWorld, Stats, TodoList, VirtualizedList)
3. **Why does it exist?** To remove MVVM boilerplate and runtime reflection cost in Unity via build-time code generation. → [[Architecture]], [[ViewModel Generation]]
4. **What happens at build & runtime?** `[ViewModel]`/`[Bind]`/`[RelayCommand]` generate the other half of each `partial` type at build time; at runtime [[Binders Catalog|binders]] connect View UI to ViewModel members. → [[ViewModel to Generated Code]], [[Runtime Binding Resolution]]
5. **Where does state live?** In a [[ViewModel]]'s fields marked `[Bind]`; bindings propagate per [[BindMode]].
6. **What are the important moving parts?** Core contracts ([[IViewModel]], [[IRelayCommand]], [[IBinder]]), the generation pipeline ([[ViewModel Generation]]), the [[Binders Catalog|binder catalog]], and [[Converters]].
7. **What should I avoid breaking?** → [[Must Be Partial]], [[Committed DLLs]], [[Submodule Init]], [[NET 9 SDK Pin]]
8. **Where do I look first?** This index, then the relevant `concepts/` page, then `source_paths` on that page.

## Map

- **overview/** — [[Architecture]] · [[Getting Started]]
- **concepts/** — [[ViewModel Generation]] · [[BindMode]] · [[Data Binding]] · [[Bindable Members]] · [[Relay Commands]] · [[Source Generation Pipeline]] · [[DI Integration]] · [[Binder Base Classes]]
- **entities/** — [[ViewModel]] · [[IViewModel]] · [[IRelayCommand]] · [[IBinder]] · [[View]]
- **flows/** — [[ViewModel to Generated Code]] · [[Runtime Binding Resolution]] · [[View Initialization]]
- **generation/** — [[Source Generator]] · [[Unity Generators]] · [[Analyzer]]
- **converters/** — [[Converters]] · [[Bool Converters]] · [[Number Converters]] · [[String Converters]] · [[Specific Converters]]
- **risks/** — [[Must Be Partial]] · [[Committed DLLs]] · [[Submodule Init]] · [[NET 9 SDK Pin]]
- **reference/** — [[Samples]] · [[Unity Editor Tooling]] · [[External Dependencies]] · [[StarterKit ViewModels]]
- **binders/** — [[Binders Catalog]] (33 categories): [[Text Binders]] · [[Image Binders]] · [[RawImage Binders]] · [[Toggle Binders]] · [[Slider Binders]] · [[Scrollbar Binders]] · [[Scrollrect Binders]] · [[Dropdown Binders]] · [[InputField Binders]] · [[Button Binders]] · [[Selectable Binders]] · [[Transform Binders]] · [[Animator Binders]] · [[AudioSource Binders]] · [[CanvasGroup Binders]] · [[Graphic Binders]] · [[Renderer Binders]] · [[LineRenderer Binders]] · [[Layout Binders]] · [[GameObject Binders]] · [[Object Binders]] · [[Collider Binders]] · [[Behaviour Binders]] · [[EventTrigger Binders]] · [[UnityEvent Binders]] · [[LocalizeStringEvent Binders]] · [[VirtualizedList Binders]] · [[Collection Binders]] · [[Caster Binders]] · [[Generic Binders]] · [[Debug Binders]] · [[Mono Binders]]

## Maintenance

Built and kept in sync by the **`aspid-wiki`** skill (Ingest / Query / Lint / Export). The checkpoint above (`last_commit` + `submodule_commits`) drives incremental re-ingest. See [[log]] for history.

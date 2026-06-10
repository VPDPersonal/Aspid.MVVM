---
title: ViewModel to Generated Code
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberParameters.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberResult.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Generation/BindIdAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Generation/IgnoreBindAttribute.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/Body/FindBindableMembersBody.cs
tags: [flow, viewmodel, source-generation, binding]
updated_at: 2026-05-31
---

# ViewModel to Generated Code

> How an annotated `partial` class becomes a runtime [[IViewModel]] with bindable members — the path no one hand-writes, because the [[Source Generator]] writes it for you.

The point of this flow: you describe *intent* (fields + attributes), and a build-time generator produces the boilerplate `IViewModel` plumbing. No reflection at runtime, so binding lookups stay fast and allocation-free.

1. **You annotate a `partial` class.** Put `[ViewModel]` on the class and `[Bind]` on fields you want exposed. `[ViewModelAttribute]` targets a class or struct, is not inherited, and is purely a marker. The `partial` modifier is mandatory — the generator emits a *second* partial half (see [[Must Be Partial]]).

2. **Generator discovers the type.** `ViewModelGenerator` is an `IIncrementalGenerator`. It uses `ForAttributeWithMetadataName` to find candidates, then `SyntacticPredicate` filters to non-static `partial class` declarations with at least one attribute. Static classes are not yet supported (per a `// TODO` in source).

3. **Generator models the type.** `FindViewModels` reads the `INamedTypeSymbol`, detects whether a base already carries `[ViewModel]` (sets `Inheritor.Inheritor` vs `Inheritor.None`), collects bindable members via a factory, groups them by ID length, and gathers custom ViewModel interfaces into a `ViewModelData`.

4. **Generator emits members.** `GenerateCode` runs seven emitters, including `BindableMembers` (a property per `[Bind]` field — see [[Bindable Members]]), `RelayCommandBody` (for `[RelayCommand]` methods — see [[Relay Commands]]), `GeneratedProperties`/`PropertyNotification` bodies, and `FindBindableMembersBody`.

5. **A binding-lookup table is generated.** `FindBindableMembersBody` writes `IViewModel.FindBindableMember`. It nests a `switch` on `parameters.Id.Length` then a `switch` on the ID string, returning `new FindBindableMemberResult(<PropertyName>)`. This length-then-string dispatch is an O(1)-ish lookup with no reflection. When the type derives from another ViewModel it emits `public override` and falls back to `base.FindBindableMember(parameters)`; otherwise it returns `default`. A `ProfilerMarker` wraps the body unless the profiler define is disabled.

6. **IDs are resolvable.** By default a member's ID is its name; `[BindId("custom")]` overrides it (field/property/method), and `[IgnoreBind]` excludes a member from View-side binding. [[IViewModel|FindBindableMemberParameters]] carries the requested `Id`; [[IViewModel|FindBindableMemberResult]] reports `IsFound` plus an `IBinderAdder`.

7. **Runtime uses it.** At runtime a [[View]] passes an ID to `FindBindableMember`; the generated switch hands back the adder that wires an [[IBinder]] to the property per its [[BindMode]]. See [[Runtime Binding Resolution]] and [[View Initialization]].

> Note: generator code lives in a submodule; Unity consumes the committed DLL ([[Committed DLLs]]), not source. For the full pipeline see [[Source Generation Pipeline]] and [[ViewModel Generation]].

# Types — Editor

Editor side of `SerializableType` and `[TypeSelector]`: the drawers, the picker window, and the
constraint resolution behind them. Runtime contracts live in `Unity/Runtime/Types/`.

## `[TypeSelector]` drives two different field shapes

| Field shape | Meaning | Drawn by |
|---|---|---|
| `string` | Assembly-qualified name; also what backs `SerializableType` | this folder (`Drawers/`) |
| `[SerializeReference]` managed reference | Picking a type **instantiates** it | `../SerializeReferences/` |

`TypeSelectorPropertyDrawer` dispatches on `SerializedProperty.propertyType`, so the same attribute
lands in two different code paths. **The managed-reference path is not in this folder** — look under
`Unity/Editor/Scripts/SerializeReferences/` for it.

The candidate list defaults to the field's declared type; a base type narrows it —
`[TypeSelector(typeof(IMelee))]`. Correct usage is enforced at compile time by the analyzer's
`AFT*` rules, so a wrong constraint is a build error, not a silent empty picker.

## Layout

```
Types/
├── TypeSelectorConstraintResolver.cs   ← resolves the attribute's constraint to a candidate set
├── TypeSelectorHelpers.cs / TypeUtility.cs / SerializableTypeUtility.cs
├── Drawers/
│   ├── TypeSelectorPropertyDrawer.cs           ← entry point, dispatches on propertyType
│   ├── SerializableTypePropertyDrawer.cs
│   ├── ComponentTypeSelectorPropertyDrawer.cs
│   ├── TypeIMGUIPropertyDrawer.cs              ← static IMGUI body
│   └── TypeUIToolkitPropertyDrawer.cs          ← static UIToolkit body
├── Selectors/
│   ├── TypeSelectorWindow.cs                   ← the picker window
│   ├── TypeSelectorView*.cs                    ← partials: View / Rows / Input / Navigation / Generics
│   ├── HierarchyBuilder.cs, NamespaceNode.cs, TreeNode.cs, NavigationController.cs
│   ├── GenericTypeResolver.cs, TypeSelectorFilter.cs, TypeSelectorIconResolver.cs
│   └── Settings/
│       ├── TypeSelectorSettings.cs             ← project settings
│       ├── TypeSelectorPreferences.cs          ← per-user EditorPrefs (favorites / recents)
│       └── TypeSelectorSettingsView.cs
├── VisualElements/  ← TypeField, InspectorTypeField
└── Extensions/      ← TypeExtensions
```

The `Drawers/` + `Selectors/` + `VisualElements/` split mirrors the sibling `Ids/` feature — see
`../Ids/CLAUDE.md`, which follows the same shape.

# Types Sample

A tiny ability system that demonstrates polymorphic type selection in the Unity Inspector using `SerializableType<T>`, `TypeSelectorAttribute`, and `ComponentTypeSelector`. The player picks an `Ability` subclass and a list of `AbilityModifier` subclasses; enemies use `ComponentTypeSelector` so the concrete enemy script can be hot-swapped from the Inspector.

Look at:

- `Scripts/AbilitySelector.cs:21` — `SerializableType<Ability>` field, constrained picker for a single subtype.
- `Scripts/AbilitySelector.cs:26` — `[TypeSelector(typeof(AbilityModifier), AllowAbstractTypes = false)]` on a `string[]` field.
- `Scripts/Enemies/EnemyBase.cs:18` — `ComponentTypeSelector` declaration that swaps the attached script in place.

## How to run

Open `Scenes/Types.unity` — it contains two prefab instances:

- **Types** (`Prefabs/Types.prefab`) — an `AbilitySelector` with `Heal` pre-picked and all three modifiers filled in. Enter Play Mode to see the Console log the activated ability and each applied modifier.
- **Enemy** (`Prefabs/Enemy.prefab`) — a `FastEnemy` wired up through `ComponentTypeSelector`. Select it in the Hierarchy and use the type dropdown at the top of the Inspector to swap between `FastEnemy` and `TankEnemy` in place; the `Health` field persists across the swap.

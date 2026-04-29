# Ids Sample

Demonstrates the `IId` / `StringIdRegistry` / `[UniqueId]` trio: fields show a human-readable string in the Inspector while serializing as a stable integer, and the Inspector catches collisions at edit-time.

## How it works

- `IId` — a marker interface declaring the `int Id { get; }` property.
- `StringIdRegistry` — a `ScriptableObject` that binds a struct type to a list of `(Id, Name)` entries and keeps the name ↔ int map available at runtime. The property drawer renders a dropdown sourced from this registry. (Use `IdRegistry` instead when names are only needed in the editor — it strips them from player builds.)
- `[UniqueId]` — validates at edit-time that no two `ScriptableObject` assets share the same resolved integer ID.

## Scenario

An enemy catalog. Each `EnemyDefinition` asset holds a unique `EnemyId` plus display data (`_displayName`, `_maxHealth`, `_moveSpeed`). An `EnemySpawner` picks a target `EnemyId` via dropdown and looks the matching asset up in its catalog on `Start()`.

## Look here

- `Scripts/EnemyId.cs` — `partial struct : IId`. `IdStructGenerator` emits `__stringId`, `_id`, and the `Id` property.
- `Scripts/EnemyDefinition.cs:8` — `[UniqueId]` on a serialized `EnemyId` field prevents duplicate IDs across assets.
- `ScriptableObjects/EnemyId StringIdRegistry.asset` — the registry binding names (`Goblin`, `Orc`, `Dragon`, `Skeleton`) to stable ints.
- `Scripts/EnemySpawner.cs:8` — dropdown-selected `EnemyId` resolved to `int` at runtime via `.Id`.

Drop `EnemySpawner` on a GameObject, fill its catalog with the four enemy assets, pick a target from the dropdown, and enter Play Mode.

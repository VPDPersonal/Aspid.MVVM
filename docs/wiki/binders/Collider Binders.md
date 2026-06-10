---
title: Collider Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/ColliderIsTriggerBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/Mono/ColliderIsTriggerMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/Mono/ColliderIsTriggerEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/Material/ColliderMaterialBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/OneWayToSource/Mono/ColliderToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/BoxColliders/Center/BoxColliderCenterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/BoxColliders/Center/BoxColliderCenterSwitcherBinder.cs
tags: [binder, starterkit, unity, physics, collider]
updated_at: 2026-05-31
---

# Collider Binders

> StarterKit binders that drive Unity 3D `Collider` properties (and shape colliders like `BoxCollider`) from ViewModel values — toggle triggers, swap physics materials, resize bounds, all without hand-written glue.

## Family

| Folder | Target | Bound property |
|--------|--------|----------------|
| `Colliders/Enabled` | `Collider` | `enabled` (bool) |
| `Colliders/IsTrigger` | `Collider` | `isTrigger` (bool) |
| `Colliders/ProvidesContacts` | `Collider` | `providesContacts` (bool) |
| `Colliders/Material` | `Collider` | `material` (`PhysicsMaterial`) |
| `Colliders/OneWayToSource` | `Collider` | the component reference itself |
| `BoxColliders/Center`, `Size` | `BoxCollider` | `center`, `size` (Vector3) |
| `CapsuleColliders`, `SphereColliders`, `MeshColliders` | shape colliders | shape-specific fields |

## Variant axes

Each property folder repeats the same StarterKit shape, so learning one teaches all:

- **Plain (`[Serializable]`)** — e.g. `ColliderIsTriggerBinder : TargetBoolBinder<Collider>`. Constructed in code, holds an explicit `Target`, overrides `Property` get/set. See [[Binder Base Classes]].
- **MonoBinder** — e.g. `ColliderIsTriggerMonoBinder : ComponentBoolMonoBinder<Collider>`. A MonoBehaviour added to the GameObject; resolves its target via `CachedComponent`. Carries `[AddComponentMenu]` + `[AddBinderContextMenu]` for Inspector wiring. See [[Mono Binders]].
- **Switcher** — `...SwitcherBinder` / `...SwitcherMonoBinder` flip between two preset values (e.g. two `Vector3` sizes) driven by a *bool* from the VM.
- **Enum / EnumGroup** — `...EnumMonoBinder` maps an enum case to a value; `...EnumGroupMonoBinder` selects across a group. Boolean targets use `EnumMonoBinder<Collider, bool>`.
- **OneWayToSource** — `ColliderToSourceMonoBinder : ComponentToSourceMonoBinder<Collider>` pushes the cached `Collider` *up* to the ViewModel when binding is established (see [[BindMode]] `OneWayToSource`).

## Notable behavior

- **TwoWay is rejected.** Most setters call `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` in their constructor — these properties are write-only sinks, not editable sources. Bool MonoBinders *do* support `OneWayToSource`, echoing the current value back on bind.
- **Material is version-aware.** `ColliderMaterialBinder` aliases `PhysicsMaterial` (Unity 2023.1+) vs the older `PhysicMaterial`, with a matching converter type per branch.
- These bind concrete Unity properties, so the ViewModel side is just an ordinary [[Bindable Members|bindable member]]; no generator output lives here — generation happens on the [[ViewModel]], not the binder.

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/`. Base classes: [[Binder Base Classes]], [[Mono Binders]]. Catalog: [[Binders Catalog]]. Concepts: [[Data Binding]], [[IBinder]].

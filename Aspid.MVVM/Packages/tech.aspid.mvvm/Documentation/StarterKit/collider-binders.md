# Collider Binders

Binders that drive collider properties from the ViewModel.

---

## Common binders

| Binder | Data type | Description |
|--------|-----------|----------|
| `ColliderEnabledBinder` | `bool` | `Collider.enabled` |
| `ColliderIsTriggerBinder` | `bool` | `Collider.isTrigger` |
| `ColliderMaterialBinder` | `PhysicsMaterial` | `Collider.material` |
| `ColliderMaterialSwitcherBinder` | `bool` → `PhysicsMaterial` | Picks a material by condition |
| `ColliderProvidesContactsBinder` | `bool` | `Collider.providesContacts` |
| `ColliderContactOffsetBinder` | `float` | `Collider.contactOffset`, minimum 0.0001 |
| `ColliderIncludeLayersBinder` | `int` | `Collider.includeLayers` |
| `ColliderExcludeLayersBinder` | `int` | `Collider.excludeLayers` |

---

## BoxCollider

| Binder | Data type | Description |
|--------|-----------|----------|
| `BoxColliderCenterBinder` | `Vector3` | `BoxCollider.center` |
| `BoxColliderSizeBinder` | `Vector3` | `BoxCollider.size`, negative components are raised to 0 |

---

## CapsuleCollider

| Binder | Data type | Description |
|--------|-----------|----------|
| `CapsuleColliderCenterBinder` | `Vector3` | `CapsuleCollider.center` |
| `CapsuleColliderRadiusBinder` | `float` | `CapsuleCollider.radius`, not below 0 |
| `CapsuleColliderHeightBinder` | `float` | `CapsuleCollider.height`, not below 0 |
| `CapsuleColliderDirectionBinder` | `int` | `CapsuleCollider.direction`, 0..2 |

---

## SphereCollider

| Binder | Data type | Description |
|--------|-----------|----------|
| `SphereColliderCenterBinder` | `Vector3` | `SphereCollider.center` |
| `SphereColliderRadiusBinder` | `float` | `SphereCollider.radius`, not below 0 |

---

## MeshCollider

| Binder | Data type | Description |
|--------|-----------|----------|
| `MeshColliderMeshBinder` | `Mesh` | `MeshCollider.sharedMesh` |
| `MeshColliderConvexBinder` | `bool` | `MeshCollider.convex` |
| `MeshColliderCookingOptionsBinder` | `MeshColliderCookingOptions` | `MeshCollider.cookingOptions` |

---

## Collider2D

| Binder | Data type | Description |
|--------|-----------|----------|
| `Collider2DIsTriggerBinder` | `bool` | `Collider2D.isTrigger` |
| `Collider2DMaterialBinder` | `PhysicsMaterial2D` | `Collider2D.sharedMaterial` |
| `Collider2DOffsetBinder` | `Vector2` | `Collider2D.offset`, finite values only |
| `Collider2DDensityBinder` | `float` | `Collider2D.density`, not below 0; effective with `Rigidbody2D.useAutoMass` |
| `BoxCollider2DSizeBinder` | `Vector2` | `BoxCollider2D.size`, negative components are raised to 0 |
| `CapsuleCollider2DSizeBinder` | `Vector2` | `CapsuleCollider2D.size`, negative components are raised to 0 |
| `CircleCollider2DRadiusBinder` | `float` | `CircleCollider2D.radius`, not below 0 |

---

## Supported modes

Every collider binder supports **OneWay**, **OneTime** and **OneWayToSource**. TwoWay is not available.

---

## Example

```csharp
[ViewModel]
public partial class DamageZoneViewModel
{
    [OneWayBind] private bool _isActive;
    [OneWayBind] private float _radius;
}
```

In the Inspector:
- `ColliderEnabledBinder` → bind to `IsActive`
- `SphereColliderRadiusBinder` → bind to `Radius`

---

## See also

- [Binders](../06-binders.md), writing custom binders
- [StarterKit overview](README.md), every component in one table

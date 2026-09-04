# Collider Binders

Биндеры для управления свойствами коллайдеров через привязку к ViewModel.

---

## Общие биндеры

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `ColliderEnabledBinder` | `bool` | `Collider.enabled` — включение/отключение |
| `ColliderIsTriggerBinder` | `bool` | `Collider.isTrigger` — режим триггера |
| `ColliderMaterialBinder` | `PhysicsMaterial` | `Collider.material` — физический материал |
| `ColliderMaterialSwitcherBinder` | `bool` → `PhysicsMaterial` | Выбор материала по условию |
| `ColliderProvidesContactsBinder` | `bool` | `Collider.providesContacts` |
| `ColliderContactOffsetBinder` | `float` | `Collider.contactOffset`, минимум 0.0001 |
| `ColliderIncludeLayersBinder` | `int` | `Collider.includeLayers` |
| `ColliderExcludeLayersBinder` | `int` | `Collider.excludeLayers` |

---

## BoxCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `BoxColliderCenterBinder` | `Vector3` | `BoxCollider.center` |
| `BoxColliderSizeBinder` | `Vector3` | `BoxCollider.size`, отрицательные компоненты поднимаются до 0 |

---

## CapsuleCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `CapsuleColliderCenterBinder` | `Vector3` | `CapsuleCollider.center` |
| `CapsuleColliderRadiusBinder` | `float` | `CapsuleCollider.radius`, не ниже 0 |
| `CapsuleColliderHeightBinder` | `float` | `CapsuleCollider.height`, не ниже 0 |
| `CapsuleColliderDirectionBinder` | `int` | `CapsuleCollider.direction`, 0..2 |

---

## SphereCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `SphereColliderCenterBinder` | `Vector3` | `SphereCollider.center` |
| `SphereColliderRadiusBinder` | `float` | `SphereCollider.radius`, не ниже 0 |

---

## MeshCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `MeshColliderMeshBinder` | `Mesh` | `MeshCollider.sharedMesh` |
| `MeshColliderConvexBinder` | `bool` | `MeshCollider.convex` |
| `MeshColliderCookingOptionsBinder` | `MeshColliderCookingOptions` | `MeshCollider.cookingOptions` |

---

## Collider2D

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `Collider2DIsTriggerBinder` | `bool` | `Collider2D.isTrigger` |
| `Collider2DMaterialBinder` | `PhysicsMaterial2D` | `Collider2D.sharedMaterial` |
| `Collider2DOffsetBinder` | `Vector2` | `Collider2D.offset`, только конечные значения |
| `Collider2DDensityBinder` | `float` | `Collider2D.density`, не ниже 0; работает при `Rigidbody2D.useAutoMass` |
| `BoxCollider2DSizeBinder` | `Vector2` | `BoxCollider2D.size`, отрицательные компоненты поднимаются до 0 |
| `CapsuleCollider2DSizeBinder` | `Vector2` | `CapsuleCollider2D.size`, отрицательные компоненты поднимаются до 0 |
| `CircleCollider2DRadiusBinder` | `float` | `CircleCollider2D.radius`, не ниже 0 |

---

## Поддерживаемые режимы

Все collider-биндеры поддерживают **OneWay**, **OneTime** и **OneWayToSource**. TwoWay не доступен.

---

## Пример использования

```csharp
[ViewModel]
public partial class DamageZoneViewModel
{
    [OneWayBind] private bool _isActive;
    [OneWayBind] private float _radius;
}
```

В Inspector:
- `ColliderEnabledBinder` → привязка к `IsActive`
- `SphereColliderRadiusBinder` → привязка к `Radius`

---

## См. также

- [Биндеры](../06-binders.md) — создание кастомных биндеров
- [Обзор StarterKit](README.md) — таблица всех компонентов

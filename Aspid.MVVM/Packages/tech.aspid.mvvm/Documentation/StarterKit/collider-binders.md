# Collider Binders

Биндеры для управления свойствами коллайдеров через привязку к ViewModel.

---

## Общие биндеры

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `ColliderEnabledBinder` | `bool` | `Collider.enabled` — включение/отключение |
| `ColliderIsTriggerBinder` | `bool` | `Collider.isTrigger` — режим триггера |
| `ColliderMaterialBinder` | `PhysicMaterial` | `Collider.material` — физический материал |
| `ColliderMaterialSwitcherBinder` | `bool` → `PhysicMaterial` | Выбор материала по условию |
| `ColliderProvidesContactsBinder` | `bool` | `Collider.providesContacts` |

---

## BoxCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `BoxColliderCenterBinder` | `Vector3` | `BoxCollider.center` |
| `BoxColliderSizeBinder` | `Vector3` | `BoxCollider.size` |

---

## CapsuleCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `CapsuleColliderCenterBinder` | `Vector3` | `CapsuleCollider.center` |
| `CapsuleColliderRadiusBinder` | `float` | `CapsuleCollider.radius` |

---

## SphereCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `SphereColliderCenterBinder` | `Vector3` | `SphereCollider.center` |
| `SphereColliderRadiusBinder` | `float` | `SphereCollider.radius` |

---

## MeshCollider

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `MeshColliderMeshBinder` | `Mesh` | `MeshCollider.sharedMesh` |
| `MeshColliderConvexBinder` | `bool` | `MeshCollider.convex` |

---

## Поддерживаемые режимы

Все collider-биндеры поддерживают **OneWay** и **OneTime**. TwoWay и OneWayToSource не доступны.

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

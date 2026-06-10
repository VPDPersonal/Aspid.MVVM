---
title: Image-биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/ImageSpriteBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Fill/ImageFillBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/ImageSpriteSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/Sprite/Mono/ImageSpriteAddressableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Images/OneToSource/Mono/ImageToSourceMonoBinder.cs
tags: [binder, starterkit, unity-ui, image]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Image Binders.md
translated_at: 2026-05-31
---

# Image-биндеры

> Биндеры из StarterKit, управляющие компонентом Unity UI `Image` из ViewModel — его спрайтом, степенью заполнения или ссылкой на компонент — чтобы графика и индикаторы прогресса реагировали на привязанное состояние без ручной настройки `Image`.

Семейство делится по тому, **что** именно оно записывает в `Image`: подгруппа **Sprite** работает с `Image.sprite`, а подгруппа **Fill** — с `Image.fillAmount` (ограничивается диапазоном `[0, 1]`).

## Таблица семейства

| Binder | Целевое свойство | Привязываемое значение |
|--------|----------------|-------------|
| `ImageSpriteBinder` | `Image.sprite` | `Sprite` (а также `IBinder<Texture2D>` — строит спрайт из текстуры) |
| `ImageSpriteSwitcherBinder` | `Image.sprite` | `bool` → выбирает `trueValue`/`falseValue` |
| `ImageSpriteEnumMonoBinder` / `…EnumGroupMonoBinder` | `Image.sprite` | enum → сопоставленный спрайт |
| `ImageSpriteAddressableMonoBinder` | `Image.sprite` | ключ Addressables → загруженный спрайт |
| `ImageFillBinder` | `Image.fillAmount` | `float` (ограничен 0–1) |
| `ImageFillSwitcherBinder` / `…EnumMonoBinder` / `…EnumGroupMonoBinder` | `Image.fillAmount` | bool / enum |
| `ImageToSourceMonoBinder` | (нет — передаёт ссылку на `Image`) | `OneWayToSource` |

## Оси вариантов

- **Plain против Mono** — Plain-биндеры (`ImageSpriteBinder`, `ImageFillBinder`, биндеры `…SwitcherBinder`) — это `[Serializable]`-классы, создаваемые в коде с ссылкой на целевой компонент; они наследуются от [[Binder Base Classes|TargetBinder]] / `TargetFloatBinder` / `SwitcherBinder`. Mono-варианты (в каталоге `Mono/`) — это MonoBehaviour'ы, наследующиеся от `ComponentMonoBinder<Image, …>`, которые кэшируют компонент и добавляют `[AddComponentMenu]` / `[AddBinderContextMenu]`, чтобы их можно было повесить на GameObject из инспектора. См. [[Mono Binders]].
- **Switcher** — переключается между двумя предустановленными спрайтами (или значениями заполнения) на основе привязанного `bool`.
- **Enum / EnumGroup** — `Enum` сопоставляет одно значение enum одному спрайту/значению заполнения; `EnumGroup` сопоставляет набор значений, что удобно для смены состояний.
- **OneWayToSource** — `ImageToSourceMonoBinder` разворачивает поток: при привязке он отправляет кэшированную ссылку на `Image` обратно в ViewModel (см. `OneWayToSource` в [[BindMode]]).

## Особенности поведения

- **`disabledWhenNull`** — Sprite-биндеры предоставляют этот флаг; когда вычисленный спрайт равен `null`, компонент `Image` отключается, скрывая его вместо отображения пустого квада.
- **Путь через Texture2D** — `ImageSpriteBinder` / `ImageSpriteMonoBinder` также принимают `Texture2D`, вызывая `Sprite.Create` и уничтожая сгенерированный спрайт в `OnUnbound`, чтобы избежать утечек (вероятно, именно поэтому эти биндеры управляют жизненным циклом явно).
- **Нет TwoWay** — sprite/fill-биндеры выбрасывают исключение при `BindMode.TwoWay`.
- **Ограничение заполнения** — `ImageFillBinder.GetConvertedValue` выполняет `Mathf.Clamp01` после конвертации; переопределения должны вызывать `base`.
- **Вариант Addressables** активируется флагом `ASPID_MVVM_ADDRESSABLES_INTEGRATION` и показывает спрайт по умолчанию во время загрузки. См. [[External Dependencies]].

Mono-биндеры объявлены как `partial`, потому что генератор Aspid создаёт обвязку `IBinder` — см. [[ViewModel to Generated Code]] и [[Must Be Partial]].

## Исходники

`StarterKit/Unity/Runtime/Binders/Images/` — `Sprite/`, `Fill/`, `OneToSource/`. Связанное: [[RawImage Binders]], [[Graphic Binders]], [[Binders Catalog]], [[Converters]].

---
title: Биндеры RawImage
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/RawImageTextureBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/RawImageTextureSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/Texture/Mono/RawImageTextureAddressableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/OneWayToSource/Mono/RawImageToSourceMonoBinder.cs
tags: [binder, starterkit, ui, rawimage, texture]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/RawImage Binders.md
translated_at: 2026-05-31
---

# Биндеры RawImage

> Биндеры StarterKit, которые управляют `RawImage.texture` из Unity UI на основе значений ViewModel — «сырой» текстурный аналог [[Image Binders]] (которые работают со спрайтами).

`RawImage` отображает произвольную `Texture`, поэтому это семейство биндеров привязывает значения ViewModel к `RawImage.texture`. Все варианты разделяют одно поведение: когда вычисленная текстура равна `null` и включена опция **Disable When Null** (по умолчанию), компонент `RawImage` отключается, скрывая устаревшую или пустую графику.

## Семейство

| Биндер | Базовый класс | Привязываемый вход |
|--------|-----------|-------------|
| `RawImageTextureBinder` | `TargetBinder<RawImage, Texture, Converter>` | `Texture` (также `Sprite` через `IBinder<Sprite>`) |
| `RawImageTextureMonoBinder` | `ComponentMonoBinder<RawImage, Texture, Converter>` | `Texture` / `Sprite` |
| `RawImageTextureSwitcherBinder` | `SwitcherBinder<…>` | `bool` -> текстура true/false |
| `RawImageTextureSwitcherMonoBinder` | switcher MonoBinder | `bool` |
| `RawImageTextureEnumMonoBinder` | `EnumMonoBinder<…>` | enum -> текстура |
| `RawImageTextureEnumGroupMonoBinder` | `EnumGroupMonoBinder<…>` | enum -> текстура для нескольких RawImage |
| `RawImageTextureAddressableMonoBinder` | `AddressableMonoBinder<Texture2D, RawImage>` | ключ адреса (загружает ассет) |
| `RawImageToSourceMonoBinder` | `ComponentToSourceMonoBinder<RawImage>` | читает RawImage обратно во VM |

## Оси вариаций

- **Plain vs Mono.** Обычные `[Serializable]`-биндеры (`RawImageTextureBinder`) создаются в коде с указанием цели и [[BindMode]]; формы `…MonoBinder` — это `MonoBehaviour`, которые настраиваются в инспекторе и регистрируются для контекстных меню и меню компонентов. См. [[Mono Binders]] и [[Binder Base Classes]].
- **Switcher.** Выбирает одну из двух заранее заданных текстур на основе привязанного `bool`.
- **Enum / EnumGroup.** Сопоставляют случаи enum с текстурами; EnumGroup применяет выбор сразу ко списку компонентов `RawImage`.
- **Addressable.** Защищён директивой `ASPID_MVVM_ADDRESSABLES_INTEGRATION`; загружает `Texture2D` из системы Addressables, показывая текстуру по умолчанию во время загрузки.
- **OneWayToSource.** `RawImageToSourceMonoBinder` отправляет текущее состояние компонента обратно во ViewModel ([[BindMode|OneWayToSource]]). Обычный `RawImageTextureBinder` выбрасывает исключение, если создан с `BindMode.TwoWay`; Mono-вариант также напрямую поддерживает OneWayToSource.

## Заметное поведение

- Тип конвертера зависит от версии: на Unity 2023.1+ это `IConverter<Texture?, Texture?>`; в более старых версиях используется `IConverterTexture`. См. [[Converters]].
- Входные значения `Sprite` принимаются за счёт извлечения `sprite.texture`, поэтому один и тот же биндер обслуживает любой из источников значений.
- `Disable When Null` по умолчанию равно `true` везде, кроме параметра конструктора обычного `RawImageTextureBinder`, который также по умолчанию равен `true`.
- Метод `SetValue(Sprite)` у Mono-биндера помечен атрибутом `[BinderLog]`, тело логирования которого генерируется [[Source Generator]] (не видно в написанном вручную коде).

## Исходный код

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/RawImages/` — `Texture/` (plain + `Mono/`) и `OneWayToSource/Mono/`. См. также [[IBinder]] и [[Binders Catalog]].

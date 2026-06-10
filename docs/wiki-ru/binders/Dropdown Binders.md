---
title: Биндеры выпадающих списков
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/DropdownValueBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/DropdownValueSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/Mono/DropdownValueEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Value/Mono/DropdownValueEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Options/DropdownOptionsBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/Command/DropdownCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/AlphaFadeSpeed/DropdownAlphaFadeSpeedBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/OneWayToSource/Mono/DropdownToSourceMonoBinder.cs
tags:
  - binders
  - starterkit
  - dropdown
  - tmp
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Dropdown Binders.md
translated_at: 2026-05-31
---

# Биндеры выпадающих списков

> Биндеры из StarterKit, которые связывают ViewModel с `TMP_Dropdown`: выбранный индекс, список опций, скорость затухания и команды смены выбора.

Каждый биндер этого семейства работает с `TMP_Dropdown` из TextMeshPro, поэтому вся папка ограничена условием `#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`. Используйте их вместо написания собственного кода, когда выбор или содержимое выпадающего списка должны управляться [[Bindable Members|биндируемым членом]].

## Семейство

| Папка | Целевое свойство | Биндируемый тип | Базовый класс |
|--------|-----------------|-----------|----------|
| `Value/` | `TMP_Dropdown.value` (выбранный индекс) | `int` | `TargetIntBinder<TMP_Dropdown>` |
| `Options/` | `TMP_Dropdown.options` | `List<string>` / `List<Sprite>` / `OptionData` | `TargetBinder<TMP_Dropdown>` |
| `AlphaFadeSpeed/` | `TMP_Dropdown.alphaFadeSpeed` | `float` (ограничен ≥ 0) | `TargetFloatBinder<TMP_Dropdown>` |
| `Command/` | реагирует на `onValueChanged` | `IRelayCommand<int/long[,...]>` | `TargetBinder<TMP_Dropdown>` |

## Оси вариаций

Одна и та же группа свойств воспроизводится по ортогональным осям:

- **Plain против Mono** — Plain-биндеры (например, `DropdownValueBinder`) — это `[Serializable]`-классы, встроенные во [[View]]; Mono-варианты (например, `DropdownValueMonoBinder`) — это компоненты MonoBehaviour с пунктами `[AddComponentMenu]` / `[AddBinderContextMenu]` в меню *Aspid/MVVM/Binders/UI/Dropdown*. См. [[Mono Binders]].
- **Switcher** — `…SwitcherBinder` переключает `value` между двумя предустановленными значениями `int` на основе биндируемого `bool` (построен на `SwitcherIntBinder`).
- **Enum / EnumGroup** — только Mono. `…EnumMonoBinder` (построен на `EnumIntMonoBinder`) сопоставляет биндируемый enum с индексом одного выпадающего списка; `…EnumGroupMonoBinder` (построен на `EnumGroupIntMonoBinder`) управляет сразу несколькими выпадающими списками.
- **OneWayToSource** — `DropdownToSourceMonoBinder` — это `ComponentToSourceMonoBinder<TMP_Dropdown>`, который при привязке передаёт текущее состояние выпадающего списка обратно во ViewModel.

## Особенности поведения

- Большинство value-биндеров отклоняют [[BindMode|TwoWay]] (они вызывают `ThrowExceptionIfMatches(BindMode.TwoWay)`); `DropdownOptionsBinder` объявляет `[BindModeOverride(OneWay, OneTime, OneWayToSource)]`. В режиме `OneWayToSource` метод `DropdownOptionsBinder.OnBound` вызывает `ValueChanged` с текущими `options`, передавая их источнику.
- `DropdownOptionsBinder` реализует несколько интерфейсов [[IBinder]], поэтому одно и то же поле принимает строковые подписи, спрайты или `OptionData`; перед применением он очищает существующие опции.
- `DropdownCommandBinder` (с 0–3 дополнительными дженериками `Param`) выполняет привязанную [[IRelayCommand]] при каждой смене выбора, передавая `Target.value`. Его `InteractableMode` (Interactable / Visible / Custom / None) отражает `CanExecute` на состояние интерактивности выпадающего списка, и он принимает типизацию аргументов команды как `int`, так и `long`.
- `DropdownAlphaFadeSpeedBinder` ограничивает преобразованное значение с помощью `Mathf.Max(value, 0)`.

## Исходники

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Dropdowns/`. Базовые классы находятся в [[Binder Base Classes]]; общий каталог — [[Binders Catalog]]. Близкие по смыслу семейства: [[Toggle Binders]], [[Slider Binders]], [[InputField Binders]].

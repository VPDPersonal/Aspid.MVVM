---
title: Binder'ы слайдера
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/SliderValueBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/Mono/SliderValueMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/Mono/SliderValueEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Value/SliderValueMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/MinMax/SliderMinMaxBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/MinMax/SliderMinMaxSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/OneWayToSource/Mono/SliderToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Command/SliderCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Sliders/Extensions/SliderSetters.cs
tags: [binder, starterkit, slider, ui]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Slider Binders.md
translated_at: 2026-05-31
---

# Binder'ы слайдера

> Binder'ы из StarterKit, которые связывают Unity UI `Slider` с ViewModel — его `value`, диапазон `min/max` или команду, вызываемую при перетаскивании.

## Что связывается

Два разных аспекта `Slider`. Binder'ы **Value** читают/записывают `Slider.value`. Binder'ы **MinMax** записывают `Slider.minValue` / `Slider.maxValue` (через расширение `SliderSetters.SetMinMax`, управляемое значением `SliderValueMode` — `Min`, `Max` или `Range`). Все они принимают необязательный [[Converters|конвертер]], применяемый перед присваиванием, а value-binder'ы подавляют повторный вход `onValueChanged` при выталкивании значения.

Value-binder'ы реализуют `INumberBinder` + `INumberReverseBinder`, поэтому `int`/`long`/`float`/`double` из ViewModel передаётся в обе стороны (исходящие int приводятся к float). См. [[Bindable Members]].

## Семейство

| Binder | Базовый класс | Цель | Примечания |
|--------|------|--------|-------|
| `SliderValueBinder` | `TargetBinder<Slider>` | `value` | OneWay / TwoWay / OneWayToSource |
| `SliderValueMonoBinder` | `ComponentMonoBinder<Slider>` | `value` | то же, настраивается в инспекторе |
| `SliderMinMaxBinder` | `TargetBinder<Slider,Vector2,Converter>` | `min`/`max` | не TwoWay; `SliderValueMode` |
| `SliderMinMaxMonoBinder` | mono | `min`/`max` | настраивается в инспекторе |
| `SliderMinMaxSwitcherBinder` / `…MonoBinder` | `SwitcherBinder<…>` | `min`/`max` | bool выбирает один из двух диапазонов |
| `SliderValueSwitcherMonoBinder` | switcher | `value` | bool выбирает одно из двух значений |
| `SliderValueEnum/EnumGroupMonoBinder`, `SliderMinMaxEnum/EnumGroupMonoBinder` | enum-float mono | `value` / `min`/`max` | enum сопоставляется со значением для каждого состояния |
| `SliderToSourceMonoBinder` | `ComponentToSourceMonoBinder<Slider>` | `value` | выталкивает текущее значение в источник |
| `SliderCommandBinder` / `SliderCommandBinder<T…>` | `TargetBinder<Slider>` | `onValueChanged` | выполняет [[IRelayCommand]] при каждом изменении |

## Оси вариантов

- **Plain vs Mono** — одна и та же логика существует как создаваемый вручную [[IBinder]] (подкласс `TargetBinder`, `[Serializable]`) и как компонент `MonoBinder` (`AddComponentMenu` "Aspid/MVVM/Binders/UI/Slider/…"). См. [[Mono Binders]] и [[Binder Base Classes]].
- **Switcher** — bool-значение из ViewModel выбирает `trueValue` или `falseValue`.
- **Enum / EnumGroup** — значение enum задаёт float для каждого состояния; *EnumGroup* применяет его ко всем `Slider` в группе.
- **OneWayToSource** — `SliderToSourceMonoBinder` плюс режим [[BindMode|OneWayToSource]] на value-binder'ах: при привязке текущее значение слайдера немедленно передаётся в ViewModel, а затем при каждом перетаскивании.

## Заметное поведение

`SliderCommandBinder` принимает команду, типизированную `int`/`long`/`float`/`double` (плюс 1-3 дополнительных аргумента `Param` в обобщённых перегрузках), и направляет вызов в ту из них, что не равна null, начиная с float. Его `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) отражает `CanExecute` на слайдер — удобно для [[Relay Commands]]. Режим OneWayToSource у value-binder'ов немедленно синхронизирует источник при привязке; это, вероятно, инициализирует ViewModel значением по умолчанию из инспектора.

## Источник

`StarterKit/Unity/Runtime/Binders/Sliders/`. Связанное: [[Scrollbar Binders]], [[Toggle Binders]], [[Binders Catalog]], [[Data Binding]].

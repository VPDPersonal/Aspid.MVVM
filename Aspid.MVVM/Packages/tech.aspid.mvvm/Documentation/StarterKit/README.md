# StarterKit -- Обзор

StarterKit -- это набор готовых к использованию биндеров и компонентов для Aspid.MVVM. Каждый биндер привязывает конкретное свойство Unity-компонента к полю ViewModel, избавляя от необходимости писать шаблонный код вручную.

Все биндеры доступны в двух вариантах:
- **Binder** (POCO) -- сериализуемый класс, встраиваемый в `[Bind]`-поле ViewModel. Не требует MonoBehaviour.
- **MonoBinder** -- MonoBehaviour-обёртка для добавления через Inspector на GameObject.

---

## Text

Биндеры для `TMP_Text` (TextMeshPro). [Подробнее](text-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `TextBinder` | `string`, `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.text` с поддержкой чисел и `CultureInfoMode` |
| `TextSwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Переключение текста между двумя значениями |
| `TextFontBinder` | `TMP_FontAsset` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.font` |
| `TextFontSwitcherBinder` | `bool` -> `TMP_FontAsset` | OneWay, OneTime | Переключение шрифта между двумя значениями |
| `TextFontSizeBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.fontSize` |
| `TextFontSizeSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение размера шрифта |
| `TextAlignmentBinder` | `TextAlignmentOptions` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.alignment` |
| `TextAlignmentSwitcherBinder` | `bool` -> `TextAlignmentOptions` | OneWay, OneTime | Переключение выравнивания текста |
| `TextFontStyleBinder` | `FontStyles` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.fontStyle` |
| `TextAutoSizeBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.enableAutoSizing` |
| `TextRichTextBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.richText` |
| `TextCharacterSpacingBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.characterSpacing` |
| `TextLineSpacingBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.lineSpacing` |
| `TextMarginBinder` | `Vector4` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.margin` (left, top, right, bottom) |
| `TextMaxVisibleCharactersBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `TMP_Text.maxVisibleCharacters` |

---

## InputField

Биндеры для `TMP_InputField` (TextMeshPro). [Подробнее](input-field-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `InputFieldBinder` | `string`, `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка текста ввода с обратной связью |
| `InputFieldCharacterValidationBinder` | `CharacterValidation` | OneWay, OneTime | Валидация символов |
| `InputFieldCharacterValidationSwitcherBinder` | `bool` -> `CharacterValidation` | OneWay, OneTime | Переключение валидации |
| `InputFieldContentTypeBinder` | `ContentType` | OneWay, OneTime | Тип контента поля ввода |
| `InputFieldContentTypeSwitcherBinder` | `bool` -> `ContentType` | OneWay, OneTime | Переключение типа контента |
| `InputFieldInputTypeBinder` | `InputType` | OneWay, OneTime | Тип ввода (Standard, AutoCorrect, Password) |
| `InputFieldInputTypeSwitcherBinder` | `bool` -> `InputType` | OneWay, OneTime | Переключение типа ввода |
| `InputFieldLineTypeBinder` | `LineType` | OneWay, OneTime | Тип строки (SingleLine, MultiLine) |
| `InputFieldLineTypeSwitcherBinder` | `bool` -> `LineType` | OneWay, OneTime | Переключение типа строки |
| `InputFieldCharacterLimitBinder` | `int` | OneWay, OneTime, OneWayToSource | Лимит символов, `0` без лимита |
| `InputFieldCaretPositionBinder` | `int` | OneWay, OneTime, OneWayToSource | Позиция каретки |
| `InputFieldReadOnlyBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `readOnly` |
| `InputFieldPlaceholderBinder` | `Graphic` | OneWay, OneTime, OneWayToSource | Привязка `placeholder` |

---

## Image

Биндеры для `UnityEngine.UI.Image`. [Подробнее](image-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ImageSpriteBinder` | `Sprite`, `Texture2D` | OneWay, OneTime, OneWayToSource | Привязка спрайта с авто-отключением при `null` |
| `ImageSpriteSwitcherBinder` | `bool` -> `Sprite` | OneWay, OneTime | Переключение спрайта между двумя значениями |
| `ImageFillBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `fillAmount` (0-1) |
| `ImageFillSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение заполнения |
| `ImageSpriteAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Загрузка спрайта через Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `ImageTypeBinder` | `Image.Type` | OneWay, OneTime, OneWayToSource | Привязка `Image.type` |
| `ImagePreserveAspectBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `preserveAspect` |
| `ImageFillOriginBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `fillOrigin` |
| `ImageFillClockwiseBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `fillClockwise` |
| `Image{Sprite, Fill}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы Image |

---

## RawImage

Биндеры для `UnityEngine.UI.RawImage`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `RawImageTextureBinder` | `Texture`, `Sprite` | OneWay, OneTime, OneWayToSource | Привязка текстуры с авто-отключением при `null` |
| `RawImageTextureSwitcherBinder` | `bool` -> `Texture` | OneWay, OneTime | Переключение текстуры |
| `RawImageTextureAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Загрузка текстуры через Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `RawImageTextureEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Текстура по enum, для одного или группы RawImage |
| `RawImageUvRectBinder` | `Rect` | OneWay, OneTime, OneWayToSource | Привязка `uvRect`; неконечные значения отклоняются |

---

## Button / Command

Биндеры команд для UI-элементов.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ButtonCommandBinder` | `IRelayCommand`, `IRelayCommand<bool>` | OneWay, OneTime | Привязка команды к Button.onClick с InteractableMode |
| `ButtonCommandBinder<T>` … `<T1,T2,T3,T4>` | `IRelayCommand<T…>` | OneWay, OneTime | Команда с 1–4 параметрами из Inspector |
| `ButtonCommand{Int, Float, Bool, String, Object}MonoBinder` | `IRelayCommand<T>` | OneWay, OneTime | Готовые Mono-варианты с одним параметром |
| `ToggleCommandBinder` | `IRelayCommand`, `IRelayCommand<bool>` | OneWay, OneTime | Команда для Toggle.onValueChanged |
| `SliderCommandBinder` | `IRelayCommand<int/long/float/double>` | OneWay, OneTime | Команда для Slider.onValueChanged |
| `DropdownCommandBinder` | `IRelayCommand<int>` | OneWay, OneTime | Команда для TMP_Dropdown.onValueChanged |
| `InputFieldCommandBinder` | `IRelayCommand`, `IRelayCommand<string>` | OneWay, OneTime | Команда для события TMP_InputField (`UpdateInputFieldEvent`) |
| `ScrollRectCommandBinder` | `IRelayCommand<Vector2>`, `IRelayCommand<Vector3>` | OneWay, OneTime | Команда для ScrollRect.onValueChanged |
| `ScrollbarCommandBinder` | `IRelayCommand<int/long/float/double>` | OneWay, OneTime | Команда для Scrollbar.onValueChanged |
| `EventTriggerCommandBinder` | `IRelayCommand`, `IRelayCommand<BaseEventData>`, `IRelayCommand<EventTriggerType>` | OneWay, OneTime | Команда для выбранного события EventTrigger; варианты `<T1..T3>` с параметрами |

---

## Slider

Биндеры для `UnityEngine.UI.Slider`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `SliderValueBinder` | `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка `Slider.value` с обратной связью |
| `SliderMinMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка min/max слайдера (`SliderRangeMode`) |
| `SliderMinMaxSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение min/max |
| `Slider{Value, MinMax}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы Slider |
| `SliderValueSwitcherMonoBinder` | `bool` -> `float` | OneWay, OneTime | Переключение `Slider.value` |

---

## Scrollbar

Биндеры для `UnityEngine.UI.Scrollbar`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ScrollbarValueBinder` | `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка `Scrollbar.value` (в [0, 1]) с обратной связью |
| `ScrollbarSizeBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `Scrollbar.size` (в [0, 1]) |
| `ScrollbarValue{Switcher, Enum, EnumGroup}MonoBinder` | `bool` / `Enum` | OneWay, OneTime | Значение по флагу или enum |

---

## ScrollRect

Биндеры для `UnityEngine.UI.ScrollRect`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ScrollRectNormalizedPositionBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `normalizedPosition` (компоненты в [0, 1]) |
| `ScrollRect{Horizontal, Vertical}NormalizedPositionBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка положения по оси (в [0, 1]) |
| `ScrollRect{Horizontal, Vertical}Binder` | `bool` | OneWay, OneTime, OneWayToSource | Включение прокрутки по оси |
| `ScrollRect{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы ScrollRect |

---

## Toggle

Биндеры для `UnityEngine.UI.Toggle`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ToggleIsOnBinder` | `bool` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка `Toggle.isOn` с опциональным конвертером |
| `ToggleIsOnEnumBinder` | `Enum` -> `bool` | OneWay, OneTime | `isOn` по значению enum |
| `ToggleIsOnEnumGroupBinder` | `Enum` -> `bool` | OneWay, OneTime | `isOn` группы Toggle по значению enum |
| `ToggleGroupAllowSwitchOffBinder` | `bool` | OneWay, OneTime | Привязка `ToggleGroup.allowSwitchOff` |

---

## Dropdown

Биндеры для `TMP_Dropdown`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `DropdownValueBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка выбранного индекса |
| `DropdownValueSwitcherBinder` | `bool` -> `int` | OneWay, OneTime | Переключение индекса |
| `DropdownOptionsBinder` | `List<string>`, `List<Sprite>`, `IEnumerable<OptionData>` | OneWay, OneTime, OneWayToSource | Привязка списка опций |
| `DropdownOptionsSwitcherBinder` | `bool` -> `List<OptionData>` | OneWay, OneTime | Переключение списка опций |
| `DropdownOptionsByEnumMonoBinder` | `Enum` | OneWay, OneTime | Опции из значений enum-типа |
| `DropdownAlphaFadeSpeedBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка скорости затухания |
| `DropdownAlphaFadeSpeedSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение скорости затухания |

---

## GameObject

Биндеры для `GameObject`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `GameObjectVisibleBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `SetActive` |
| `GameObjectVisibleByBindMonoBinder` | любой | OneTime | Показ объекта, пока есть привязка |
| `GameObjectTagBinder` | `string` | OneWay, OneTime, OneWayToSource | Привязка `tag` |
| `GameObjectTagSwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Переключение тега |
| `GameObjectLayerBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `layer`; несуществующий индекс отклоняется |
| `GameObjectNameMonoBinder` | `string` | OneWay, OneTime, OneWayToSource | Имя объекта; `null` очищает |
| `GameObjectInstantiateAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Инстанс префаба из Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `GameObject{Visible, Tag}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы объектов |

---

## Transform

Биндеры для `Transform` и `RectTransform`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `TransformPositionBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка `localPosition` |
| `TransformPositionSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение позиции |
| `TransformRotationBinder` | `Quaternion` | OneWay, OneTime, OneWayToSource | Привязка `localRotation` |
| `TransformRotationSwitcherBinder` | `bool` -> `Quaternion` | OneWay, OneTime | Переключение поворота |
| `TransformEulerAnglesBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка `localEulerAngles` |
| `TransformEulerAnglesSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение углов Эйлера |
| `TransformScaleBinder` | `Vector3`, `float` | OneWay, OneTime, OneWayToSource | Привязка `localScale` |
| `TransformScaleSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение масштаба |
| `RectTransformAnchoredPositionBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка `anchoredPosition` |
| `RectTransformAnchoredPositionSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение якорной позиции |
| `RectTransformSizeDeltaBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка `sizeDelta` |
| `RectTransformSizeDeltaSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение размера |
| `RectTransformAnchorMinBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `anchorMin` |
| `RectTransformAnchorMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `anchorMax` |
| `RectTransformOffsetMinBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `offsetMin` |
| `RectTransformOffsetMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `offsetMax` |
| `RectTransformPivotBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `pivot` |
| `RectTransformAnchorMinSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение `anchorMin` |
| `RectTransformAnchorMaxSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение `anchorMax` |
| `RectTransformPivotSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение `pivot` |
| `TransformParentBinder` | `Transform` | OneWay, OneTime, OneWayToSource | Привязка `Transform.parent` |
| `TransformSiblingIndexBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка индекса среди соседей |

---

## CanvasGroup

Биндеры для `CanvasGroup`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `CanvasGroupAlphaBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `alpha` |
| `CanvasGroupAlphaSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение прозрачности |
| `CanvasGroupInteractableBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `interactable` |
| `CanvasGroupBlocksRaycastsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `blocksRaycasts` |
| `CanvasGroupIgnoreParentGroupsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `ignoreParentGroups` |
| `CanvasGroup{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы CanvasGroup |

---

## Animator

Биндеры для `Animator`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `AnimatorSetBoolBinder` | `bool` | OneWay, OneTime, OneWayToSource | Установка bool-параметра; в OneWayToSource отдаёт `Action<bool>`/`IRelayCommand<bool>` |
| `AnimatorSetIntBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Установка int-параметра |
| `AnimatorSetFloatBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Установка float-параметра |
| `AnimatorSetTriggerBinder` / `AnimatorResetTriggerBinder` | `Action`, `IRelayCommand` | OneWayToSource | `SetTrigger` / `ResetTrigger` как команда для ViewModel |
| `AnimatorSpeedBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `Animator.speed` |
| `AnimatorLayerWeightBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Вес слоя, [0, 1] |
| `AnimatorControllerBinder` | `RuntimeAnimatorController` | OneWay, OneTime, OneWayToSource | Привязка `runtimeAnimatorController` |
| `AnimatorPlayStateBinder` | `string` | OneWay, OneTime | `Animator.Play` состояния по имени |

---

## Graphic / Renderer

Биндеры для `Graphic` (UI) и `Renderer` (3D).

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `GraphicColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Привязка `Graphic.color` |
| `GraphicColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета |
| `GraphicColorChannelBinder` | `float` | OneWay, OneTime | Привязка выбранных каналов цвета (`ColorChannels`) |
| `GraphicColorChannelSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение выбранных каналов цвета |
| `GraphicMaterialBinder` | `Material` | OneWay, OneTime | Привязка `Graphic.material` |
| `GraphicMaterialSwitcherBinder` | `bool` -> `Material` | OneWay, OneTime | Переключение материала |
| `RendererMaterialsBinder` | `Material`, `IReadOnlyCollection<Material>` | OneWay, OneTime, OneWayToSource | Привязка `Renderer.material` / `materials` |
| `RendererMaterialsSwitcherBinder` | `bool` -> `Material[]` | OneWay, OneTime | Переключение массива материалов |
| `RendererMaterialsColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Цвет shader-свойства на всех материалах |
| `RendererMaterialsColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета материалов |
| `RendererEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Renderer.enabled` |
| `RendererShadowCastingBinder` | `ShadowCastingMode` | OneWay, OneTime, OneWayToSource | Привязка `Renderer.shadowCastingMode` |
| `RendererSortingOrderBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `Renderer.sortingOrder` |
| `RendererSortingLayerNameBinder` | `string` | OneWay, OneTime, OneWayToSource | Привязка `Renderer.sortingLayerName` |
| `RendererPropertyBlock{Float, Color, Vector, Texture}MonoBinder` | `float` / `Color` / `Vector4` / `Texture` | OneWay, OneTime | Shader-свойство через `MaterialPropertyBlock` |
| `LineRendererColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Цвет начала/конца LineRenderer (`LineRendererColorMode`) |
| `LineRendererColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета LineRenderer |
| `LineRendererColorEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Цвет по enum, для одного или группы LineRenderer |
| `LineRendererLoopBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `LineRenderer.loop` |
| `LineRendererWidthMultiplierBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `widthMultiplier` (не ниже нуля) |

---

## SpriteRenderer

Биндеры для `SpriteRenderer` (2D).

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `SpriteRendererSpriteBinder` | `Sprite`, `Texture2D` | OneWay, OneTime, OneWayToSource | Привязка `SpriteRenderer.sprite`; текстура оборачивается в спрайт |
| `SpriteRendererColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Привязка `SpriteRenderer.color` |
| `SpriteRendererFlipXBinder` / `FlipYBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `flipX` / `flipY` |
| `SpriteRendererSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `size` (Sliced/Tiled); отрицательные компоненты обнуляются |
| `SpriteRendererSortingOrderBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `sortingOrder` |
| `SpriteRenderer{Sprite, Color, FlipX, FlipY, SortingOrder}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы SpriteRenderer |

---

## AudioSource

Биндеры для `AudioSource`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `AudioSourceVolumeBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка громкости |
| `AudioSourcePitchBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка высоты тона |
| `AudioSourceClipBinder` | `AudioClip` | OneWay, OneTime, OneWayToSource | Привязка аудиоклипа |
| `AudioSourceMuteBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка Mute |
| `AudioSourceLoopBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка Loop |
| `AudioSourceTimeBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка позиции воспроизведения |
| `AudioSourceSpatialBlendBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка 2D/3D баланса |
| `AudioSourcePanStereoBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка стерео-панорамы |
| `AudioSourceDopplerLevelBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка уровня эффекта Доплера |
| `AudioSourceMinMaxDistanceBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка min/max расстояния |
| `AudioSourcePriorityBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка приоритета |
| `AudioSourceSpreadBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка угла распространения |
| `AudioSourceOutputAudioMixerGroupBinder` | `AudioMixerGroup` | OneWay, OneTime, OneWayToSource | Привязка группы микшера |
| `AudioSourceBypassEffectsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка bypass-эффектов |
| `AudioSourceBypassListenerEffectsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка bypass listener эффектов |
| `AudioSourceBypassReverbZonesBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка bypass reverb zones |
| `AudioSourceReverbZoneMixBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка reverb zone mix |
| `AudioSourceTimeSamplesBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка позиции в сэмплах |

---

## Collider

Биндеры для коллайдеров. [Подробнее](collider-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ColliderEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Collider.enabled` |
| `ColliderIsTriggerBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Collider.isTrigger` |
| `ColliderMaterialBinder` | `PhysicsMaterial` | OneWay, OneTime, OneWayToSource | Привязка физического материала |
| `ColliderProvidesContactsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `providesContacts` |
| `BoxColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка центра BoxCollider |
| `BoxColliderSizeBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка размера BoxCollider |
| `SphereColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка центра SphereCollider |
| `SphereColliderRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка радиуса SphereCollider |
| `CapsuleColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка центра CapsuleCollider |
| `CapsuleColliderRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка радиуса CapsuleCollider |
| `MeshColliderConvexBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `MeshCollider.convex` |
| `MeshColliderMeshBinder` | `Mesh` | OneWay, OneTime, OneWayToSource | Привязка меша коллайдера |
| `MeshColliderCookingOptionsBinder` | `MeshColliderCookingOptions` | OneWay, OneTime, OneWayToSource | Привязка `MeshCollider.cookingOptions` |
| `CapsuleColliderHeightBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка высоты CapsuleCollider |
| `CapsuleColliderDirectionBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка оси CapsuleCollider (0..2) |
| `ColliderContactOffsetBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `Collider.contactOffset` |
| `ColliderIncludeLayersBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `Collider.includeLayers` |
| `ColliderExcludeLayersBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `Collider.excludeLayers` |

### Collider2D

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `Collider2DIsTriggerBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Collider2D.isTrigger` |
| `Collider2DMaterialBinder` | `PhysicsMaterial2D` | OneWay, OneTime, OneWayToSource | Привязка `Collider2D.sharedMaterial` |
| `Collider2DOffsetBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `Collider2D.offset` |
| `Collider2DDensityBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `Collider2D.density` |
| `BoxCollider2DSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка размера BoxCollider2D |
| `CapsuleCollider2DSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка размера CapsuleCollider2D |
| `CircleCollider2DRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка радиуса CircleCollider2D |

---

## Rigidbody

Биндеры для `Rigidbody` и `Rigidbody2D`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `RigidbodyMassBinder` / `Rigidbody2DMassBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `mass`; неконечное значение отклоняется |
| `RigidbodyIsKinematicBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody.isKinematic` |
| `RigidbodyUseGravityBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody.useGravity` |
| `RigidbodyConstraintsBinder` | `RigidbodyConstraints` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody.constraints` |
| `Rigidbody2DBodyTypeBinder` | `RigidbodyType2D` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody2D.bodyType` |
| `Rigidbody2DGravityScaleBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody2D.gravityScale` |
| `Rigidbody2DSimulatedBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Rigidbody2D.simulated` |

---

## Aggregator / Conditional

Комбинирование нескольких привязок и выбор по условию; результат уходит в `UnityEvent<T>`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `AndBoolMonoBinder` / `OrBoolMonoBinder` | вход `bool` | — | Агрегатор: `UnityEvent<bool>` со сводкой всех входов |
| `FormatStringMonoBinder` | вход `string` | — | Агрегатор: `string.Format` по входам |
| `BoolAggregatorInputMonoBinder` / `StringAggregatorInputMonoBinder` | `bool` / `string` | OneWay, OneTime | Вход агрегатора с индексом |
| `Conditional{Color, Float, String}MonoBinder` | `bool` | OneWay, OneTime | Одно из двух значений в `UnityEvent<T>` |

---

## RateLimit / Tween

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `Debounce{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | Пропускает последнее значение, когда поток затих |
| `Throttle{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | Не больше одного значения за интервал |
| `Delay{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | Каждое значение с задержкой, по порядку |
| `Tween{Float, Vector3, Color}MonoBinder` | `float` / `Vector3` / `Color` | OneWay, OneTime | Плавный переход к новому значению через `UnityEvent<T>` |

---

## UI Toolkit

Биндеры ищут элемент в `UIDocument` по имени или USS-классу.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ElementLabelTextMonoBinder` | любой | OneWay, OneTime | `Label.text` |
| `ElementTextFieldValueMonoBinder` | `string` | все | Текст `TextField` с обратной связью |
| `ElementSliderValueMonoBinder` | `int`, `float`, `long`, `double` | все | Значение `Slider` с обратной связью |
| `ElementButtonCommandMonoBinder` | `IRelayCommand` | OneWay, OneTime | Клик по `Button` |
| `Element{Enabled, Display, Class}MonoBinder` | `bool` | OneWay, OneTime | `SetEnabled`, `display`, USS-класс |
| `ElementListViewItemsSourceMonoBinder` | `IReadOnlyList<object>`, observable/filtered | OneWay, OneTime | Источник `ListView` |

---

## UnityEvent

Биндеры для вызова `UnityEvent` при изменении значения. [Подробнее](unity-event-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `UnityEvent{Bool, Int, Long, Float, Double, String, Color, Vector2, Vector3, Quaternion}MonoBinder` | соответствующий тип | OneWay, OneTime | Вызов `UnityEvent<T>` со значением; у числовых и строкового принимаются все числовые типы |
| `UnityEventEnumMonoBinder` | `Enum` | OneWay, OneTime | Вызов `UnityEvent`, сопоставленного значению enum |
| `UnityEventSwitcherMonoBinder` | `bool` | OneWay, OneTime | Вызов одного из двух `UnityEvent` |
| `UnityEventNumberCondition(Switcher)MonoBinder` | `float` | OneWay, OneTime | Число → `bool` через конвертер → `UnityEvent<bool>` или одно из двух событий |
| `UnityEventBoolByBindMonoBinder` | любой | OneTime | `UnityEvent<bool>` с фактом наличия привязки |

---

## Collections

Биндеры для коллекций ViewModel. [Подробнее](collection-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ViewModelObservableListBinder` | `ObservableList<IViewModel>` | OneWay, OneTime | Динамический список с фабрикой View |
| `ViewModelCollectionBinder` | `IReadOnlyList<IViewModel>` | OneWay, OneTime | Статическая коллекция |
| `ViewModelObservableDictionaryBinder` | `ObservableDict<K,IViewModel>` | OneWay, OneTime | Словарь с фабрикой View |
| `VirtualizedListItemSourceBinder` | `IReadOnlyList<IViewModel>` | OneWay, OneTime | Источник данных для VirtualizedList |

---

## Selectable

Биндеры для `Selectable` (базовый класс Button, Toggle, Slider и т.д.).

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `SelectableInteractableBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `interactable` |
| `SelectableColorBlockBinder` | `ColorBlock` | OneWay, OneTime, OneWayToSource | Привязка `colors` |
| `SelectableColorBlockSwitcherBinder` | `bool` -> `ColorBlock` | OneWay, OneTime | Переключение цветовой схемы |
| `SelectableTransitionBinder` | `Selectable.Transition` | OneWay, OneTime, OneWayToSource | Привязка `transition` |
| `SelectableTargetGraphicBinder` | `Graphic` | OneWay, OneTime, OneWayToSource | Привязка `targetGraphic` |
| `Selectable{Interactable, ColorBlock}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum, для одного или группы Selectable |

---

## Behaviour

Биндеры для `Behaviour`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `BehaviourEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | Привязка `Behaviour.enabled`; пустая ссылка берёт первый не-биндер на объекте |
| `BehaviourEnabledByBindMonoBinder` | любой | OneTime | Биндер включён, пока есть привязка |
| `BehaviourEnabledEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | `enabled` по enum, для одного или группы Behaviour |

---

## Layout

Биндеры для Layout-компонентов.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `LayoutGroupPaddingBinder` | `RectOffset`, `int` | OneWay, OneTime, OneWayToSource | Привязка padding; стороны выбираются `RectSides` |
| `LayoutGroupPaddingSwitcherBinder` | `bool` -> `RectOffset` | OneWay, OneTime | Переключение padding |
| `HorizontalOrVerticalLayoutGroupSpacingBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Привязка spacing |
| `HorizontalOrVerticalLayoutGroupSpacingSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение spacing |
| `GridLayoutGroupCellSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `cellSize` (не ниже нуля) |
| `GridLayoutGroupSpacingBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `spacing` |
| `GridLayoutGroupConstraintBinder` | `GridLayoutGroup.Constraint` | OneWay, OneTime, OneWayToSource | Привязка `constraint` |
| `GridLayoutGroupConstraintCountBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка `constraintCount` |
| `{LayoutGroup, HorizontalOrVerticalLayoutGroup}{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Значение по enum |

---

## Object

Биндеры для `UnityEngine.Object`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ObjectNameBinder` | `string` | OneWay, OneTime, OneWayToSource | Привязка `Object.name` |

---

## Caster

MonoBinder-кастеры для преобразования типов между биндерами.

| Компонент | Преобразование | Описание |
|-----------|---------------|----------|
| `AnyToStringCasterMonoBinder` | `any` -> `string` | Преобразование любого значения в строку |
| `ValueToStringCasterMonoBinder<T>` | `T` -> `string` | Абстрактная база типизированного преобразования в строку |
| `TimeSpanToStringCasterMonoBinder` | `TimeSpan` -> `string` | Форматирование TimeSpan в строку |
| `StringToBoolCasterMonoBinder` | `string` -> `bool` | Преобразование строки в bool |
| `StringToIntCasterMonoBinder` | `string` -> `int` | Разбор строки в int |
| `StringToFloatCasterMonoBinder` | `string` -> `float` | Разбор строки в float |
| `StringToEnumCasterMonoBinder<TEnum>` | `string` -> `TEnum` | Абстрактная база разбора строки в enum |
| `Vector2ToVector3CasterMonoBinder` | `Vector2` -> `Vector3` | Конвертация 2D-вектора в 3D |
| `Vector3ToVector2CasterMonoBinder` | `Vector3` -> `Vector2` | Конвертация 3D-вектора в 2D |

---

## Generic

Универсальные биндеры для произвольных типов.

| Компонент | Режим | Описание |
|-----------|-------|----------|
| `DelegateOneWayBinder<T>` | OneWay | Универсальный OneWay-биндер поверх `Action<T>` |
| `DelegateOneTimeBinder<T>` | OneTime | Универсальный OneTime-биндер поверх `Action<T>` |
| `DelegateOneWayToSourceBinder<T>` | OneWayToSource | Универсальный OneWayToSource-биндер |
| `DelegateTwoWayBinder<T>` | TwoWay | Универсальный TwoWay-биндер |
| `CasterBinder<TFrom, TTo>` | OneWay, OneTime | Универсальный кастер типов |

---

## UI-компоненты (прочие)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `CanvasSortingOrderBinder` / `CanvasOverrideSortingBinder` | `int` / `bool` | OneWay, OneTime, OneWayToSource | `Canvas.sortingOrder`, `overrideSorting` |
| `CanvasScaler{UiScaleMode, ScaleFactor, ReferenceResolution, MatchWidthOrHeight}Binder` | `ScaleMode` / `float` / `Vector2` / `float` | OneWay, OneTime, OneWayToSource | Свойства `CanvasScaler` с ограничениями Unity |
| `ContentSizeFitter{HorizontalFit, VerticalFit}Binder` | `ContentSizeFitter.FitMode` | OneWay, OneTime, OneWayToSource | Режимы подгонки размера |
| `AspectRatioFitter{AspectMode, AspectRatio}Binder` | `AspectMode` / `float` | OneWay, OneTime, OneWayToSource | Режим и соотношение сторон |
| `LayoutElement{PreferredWidth, PreferredHeight, FlexibleWidth, FlexibleHeight}Binder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Размеры элемента; отрицательное = «без предпочтения» |
| `LayoutElementIgnoreLayoutBinder` | `bool` | OneWay, OneTime, OneWayToSource | `ignoreLayout` |
| `MaskShowMaskGraphicBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Mask.showMaskGraphic` |
| `RectMask2DPaddingBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `RectMask2D.padding` (left, bottom, right) |
| `Shadow{EffectColor, EffectDistance}Binder` | `Color` / `Vector2` | OneWay, OneTime, OneWayToSource | Цвет и смещение `Shadow`/`Outline` |

---

## ParticleSystem

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ParticleSystemEmissionEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | Включение эмиссии без остановки системы |
| `ParticleSystemEmissionRateBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Множитель `rateOverTime`, не ниже нуля |
| `ParticleSystemStartColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Цвет новых частиц |
| `ParticleSystem{Play, Stop, Pause, Clear}MonoBinder` | `Action`, `IRelayCommand` | OneWayToSource | Операция воспроизведения как команда для ViewModel |

---

## Сцена и рендеринг

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `Camera{BackgroundColor, FieldOfView, Orthographic, OrthographicSize}Binder` | `Color` / `float` / `bool` / `float` | OneWay, OneTime, OneWayToSource | Свойства камеры |
| `Light{Color, Intensity, Range, SpotAngle}Binder` | `Color` / `float` | OneWay, OneTime, OneWayToSource | Свойства источника света |
| `VideoPlayer{Clip, IsLooping, PlaybackSpeed}Binder` | `VideoClip` / `bool` / `float` | OneWay, OneTime, OneWayToSource | Клип, зацикливание, скорость (0..10) |
| `NavMeshAgent{Speed, IsStopped}Binder` | `float` / `bool` | OneWay, OneTime, OneWayToSource | Агент вне NavMesh: запись `isStopped` логируется и пропускается |

---

## Глобальное состояние

Биндеры без цели: статические свойства движка.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `TimeScaleBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `Time.timeScale`, не ниже нуля |
| `TargetFrameRateBinder` | `int` | OneWay, OneTime, OneWayToSource | `Application.targetFrameRate`, не ниже -1 |
| `QualityLevelBinder` | `int` | OneWay, OneTime, OneWayToSource | Уровень качества, индекс ограничен списком проекта |
| `ScreenFullScreenBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Screen.fullScreen` |
| `AudioListener{Volume, Pause}Binder` | `float` / `bool` | OneWay, OneTime, OneWayToSource | Глобальная громкость и пауза звука |
| `AudioMixerFloatBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Exposed-параметр `AudioMixer` |
| `AudioMixerSnapshotBinder` | `int`, `string` | OneWay, OneTime | Переход к снапшоту по индексу или имени |

---

## Debug

Утилиты для отладки.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `DebugLogBinder` | любой | все | Вывод значения в `Debug.Log`; только в Editor и development-сборках |

---

## Localization

Биндеры для интеграции с Unity Localization.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `TextLocalizationEntryBinder` | `LocalizedString` | OneWay, OneTime | Привязка локализованной строки к TMP_Text |
| `TextLocalizationEntrySwitcherBinder` | `bool` -> `LocalizedString` | OneWay, OneTime | Переключение локализованной строки |
| `LocalizeStringEventEntryBinder` | `string` (ключ записи) | OneWay, OneTime, OneWayToSource | Привязка ключа к LocalizeStringEvent |
| `LocalizeStringEventEntrySwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Переключение ключа локализации |
| `LocalizeStringEventVariableBinder` | числа, `bool`, `string`, `Object` | OneWay, OneTime | Smart String-переменная в LocalizeStringEvent; создаётся по типу значения |

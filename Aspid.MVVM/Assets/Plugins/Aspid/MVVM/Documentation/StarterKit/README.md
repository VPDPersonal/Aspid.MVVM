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

---

## Image

Биндеры для `UnityEngine.UI.Image`. [Подробнее](image-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ImageSpriteBinder` | `Sprite`, `Texture2D` | OneWay, OneTime, OneWayToSource | Привязка спрайта с авто-отключением при `null` |
| `ImageSpriteSwitcherBinder` | `bool` -> `Sprite` | OneWay, OneTime | Переключение спрайта между двумя значениями |
| `ImageFillBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `fillAmount` (0-1) |
| `ImageFillSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение заполнения |

---

## RawImage

Биндеры для `UnityEngine.UI.RawImage`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `RawImageTextureBinder` | `Texture` | OneWay, OneTime | Привязка текстуры |
| `RawImageTextureSwitcherBinder` | `bool` -> `Texture` | OneWay, OneTime | Переключение текстуры |

---

## Button / Command

Биндеры команд для UI-элементов.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ButtonCommandBinder` | `IRelayCommand` | OneWay, OneTime | Привязка команды к Button.onClick с InteractableMode |
| `ButtonCommandBinder<T>` | `IRelayCommand<T>` | OneWay, OneTime | Команда с параметром |
| `ButtonCommandBinder<T1,T2>` | `IRelayCommand<T1,T2>` | OneWay, OneTime | Команда с двумя параметрами |
| `ToggleCommandBinder` | `IRelayCommand<bool>` | OneWay, OneTime | Команда для Toggle.onValueChanged |
| `SliderCommandBinder` | `IRelayCommand<float>` | OneWay, OneTime | Команда для Slider.onValueChanged |
| `DropdownCommandBinder` | `IRelayCommand<int>` | OneWay, OneTime | Команда для TMP_Dropdown.onValueChanged |
| `InputFieldCommandBinder` | `IRelayCommand<string>` | OneWay, OneTime | Команда для TMP_InputField |
| `ScrollRectCommandBinder` | `IRelayCommand<Vector2>` | OneWay, OneTime | Команда для ScrollRect.onValueChanged |
| `ScrollbarCommandBinder` | `IRelayCommand<float>` | OneWay, OneTime | Команда для Scrollbar.onValueChanged |
| `EventTriggerCommandBinder` | `IRelayCommand` | OneWay, OneTime | Команда для EventTrigger |

---

## Slider

Биндеры для `UnityEngine.UI.Slider`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `SliderValueBinder` | `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка `Slider.value` с обратной связью |
| `SliderMinMaxBinder` | `Vector2` | OneWay, OneTime | Привязка min/max значений слайдера |
| `SliderMinMaxSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение min/max |

---

## Toggle

Биндеры для `UnityEngine.UI.Toggle`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ToggleIsOnBinder` | `bool` | OneWay, TwoWay, OneTime, OneWayToSource | Привязка `Toggle.isOn` с поддержкой инверсии |

---

## Dropdown

Биндеры для `TMP_Dropdown`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `DropdownValueBinder` | `int` | OneWay, OneTime, OneWayToSource | Привязка выбранного индекса |
| `DropdownValueSwitcherBinder` | `bool` -> `int` | OneWay, OneTime | Переключение индекса |
| `DropdownOptionsBinder` | `List<OptionData>` | OneWay, OneTime | Привязка списка опций |
| `DropdownOptionsSwitcherBinder` | `bool` -> `List<OptionData>` | OneWay, OneTime | Переключение списка опций |
| `DropdownAlphaFadeSpeedBinder` | `float` | OneWay, OneTime | Привязка скорости затухания |
| `DropdownAlphaFadeSpeedSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение скорости затухания |

---

## GameObject

Биндеры для `GameObject`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `GameObjectVisibleBinder` | `bool` | OneWay, OneTime | Привязка `SetActive` |
| `GameObjectTagBinder` | `string` | OneWay, OneTime | Привязка `tag` |
| `GameObjectTagSwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Переключение тега |

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
| `TransformScaleBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | Привязка `localScale` |
| `TransformScaleSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Переключение масштаба |
| `RectTransformAnchoredPositionBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `anchoredPosition` |
| `RectTransformAnchoredPositionSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение якорной позиции |
| `RectTransformSizeDeltaBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Привязка `sizeDelta` |
| `RectTransformSizeDeltaSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Переключение размера |

---

## CanvasGroup

Биндеры для `CanvasGroup`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `CanvasGroupAlphaBinder` | `float` | OneWay, OneTime, OneWayToSource | Привязка `alpha` |
| `CanvasGroupAlphaSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение прозрачности |
| `CanvasGroupInteractableBinder` | `bool` | OneWay, OneTime | Привязка `interactable` |
| `CanvasGroupBlocksRaycastsBinder` | `bool` | OneWay, OneTime | Привязка `blocksRaycasts` |
| `CanvasGroupIgnoreParentGroupsBinder` | `bool` | OneWay, OneTime | Привязка `ignoreParentGroups` |

---

## Animator

Биндеры для `Animator`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `AnimatorSetBoolBinder` | `bool` | OneWay, OneTime | Установка bool-параметра аниматора |
| `AnimatorSetIntBinder` | `int` | OneWay, OneTime | Установка int-параметра аниматора |
| `AnimatorSetFloatBinder` | `float` | OneWay, OneTime | Установка float-параметра аниматора |
| `AnimatorSetTriggerBinder` | `bool` | OneWay, OneTime | Установка/сброс триггера аниматора |
| `AnimatorSetParameterBinder` | `AnimatorControllerParameterType` | OneWay, OneTime | Универсальный биндер параметра |

---

## Graphic / Renderer

Биндеры для `Graphic` (UI) и `Renderer` (3D).

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `GraphicColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Привязка `Graphic.color` |
| `GraphicColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета |
| `GraphicColorComponentBinder` | `float` | OneWay, OneTime | Привязка компонента цвета (R/G/B/A) |
| `GraphicColorComponentSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение компонента цвета |
| `GraphicMaterialBinder` | `Material` | OneWay, OneTime | Привязка `Graphic.material` |
| `GraphicMaterialSwitcherBinder` | `bool` -> `Material` | OneWay, OneTime | Переключение материала |
| `RendererMaterialsBinder` | `Material` | OneWay, OneTime | Привязка `Renderer.material` |
| `RendererMaterialsSwitcherBinder` | `bool` -> `Material` | OneWay, OneTime | Переключение материала рендерера |
| `RendererMaterialColorBinder` | `Color` | OneWay, OneTime | Привязка цвета материала рендерера |
| `RendererMaterialColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета материала |
| `LineRendererColorBinder` | `Color` | OneWay, OneTime | Привязка цвета LineRenderer |
| `LineRendererColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Переключение цвета LineRenderer |

---

## AudioSource

Биндеры для `AudioSource`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `AudioSourceVolumeBinder` | `float` | OneWay, OneTime | Привязка громкости |
| `AudioSourcePitchBinder` | `float` | OneWay, OneTime | Привязка высоты тона |
| `AudioSourceClipBinder` | `AudioClip` | OneWay, OneTime | Привязка аудиоклипа |
| `AudioSourceMuteBinder` | `bool` | OneWay, OneTime | Привязка Mute |
| `AudioSourceLoopBinder` | `bool` | OneWay, OneTime | Привязка Loop |
| `AudioSourceTimeBinder` | `float` | OneWay, OneTime | Привязка позиции воспроизведения |
| `AudioSourceSpatialBlendBinder` | `float` | OneWay, OneTime | Привязка 2D/3D баланса |
| `AudioSourcePanStereoBinder` | `float` | OneWay, OneTime | Привязка стерео-панорамы |
| `AudioSourceDopplerLevelBinder` | `float` | OneWay, OneTime | Привязка уровня эффекта Доплера |
| `AudioSourceMinMaxDistanceBinder` | `Vector2` | OneWay, OneTime | Привязка min/max расстояния |
| `AudioSourcePriorityBinder` | `int` | OneWay, OneTime | Привязка приоритета |
| `AudioSourceSpreadBinder` | `float` | OneWay, OneTime | Привязка угла распространения |
| `AudioSourceOutputAudioMixerGroupBinder` | `AudioMixerGroup` | OneWay, OneTime | Привязка группы микшера |
| `AudioSourceBypassEffectsBinder` | `bool` | OneWay, OneTime | Привязка bypass-эффектов |
| `AudioSourceBypassListenerEffectsBinder` | `bool` | OneWay, OneTime | Привязка bypass listener эффектов |
| `AudioSourceBypassReverbZonesBinder` | `bool` | OneWay, OneTime | Привязка bypass reverb zones |
| `AudioSourceReverbZoneMixBinder` | `float` | OneWay, OneTime | Привязка reverb zone mix |
| `AudioSourceTimeSamplesBinder` | `int` | OneWay, OneTime | Привязка позиции в сэмплах |

---

## Collider

Биндеры для коллайдеров. [Подробнее](collider-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ColliderEnabledBinder` | `bool` | OneWay, OneTime | Привязка `Collider.enabled` |
| `ColliderIsTriggerBinder` | `bool` | OneWay, OneTime | Привязка `Collider.isTrigger` |
| `ColliderMaterialBinder` | `PhysicMaterial` | OneWay, OneTime | Привязка физического материала |
| `ColliderProvidesContactsBinder` | `bool` | OneWay, OneTime | Привязка `providesContacts` |
| `BoxColliderCenterBinder` | `Vector3` | OneWay, OneTime | Привязка центра BoxCollider |
| `BoxColliderSizeBinder` | `Vector3` | OneWay, OneTime | Привязка размера BoxCollider |
| `SphereColliderCenterBinder` | `Vector3` | OneWay, OneTime | Привязка центра SphereCollider |
| `SphereColliderRadiusBinder` | `float` | OneWay, OneTime | Привязка радиуса SphereCollider |
| `CapsuleColliderCenterBinder` | `Vector3` | OneWay, OneTime | Привязка центра CapsuleCollider |
| `CapsuleColliderRadiusBinder` | `float` | OneWay, OneTime | Привязка радиуса CapsuleCollider |
| `MeshColliderConvexBinder` | `bool` | OneWay, OneTime | Привязка `MeshCollider.convex` |
| `MeshColliderMeshBinder` | `Mesh` | OneWay, OneTime | Привязка меша коллайдера |

---

## UnityEvent

Биндеры для вызова `UnityEvent` при изменении значения. [Подробнее](unity-event-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `UnityEventBoolMonoBinder` | `bool` | OneWay, OneTime | Вызов UnityEvent при изменении bool |
| `UnityEventIntMonoBinder` | `int` | OneWay, OneTime | Вызов UnityEvent при изменении int |
| `UnityEventFloatMonoBinder` | `float` | OneWay, OneTime | Вызов UnityEvent при изменении float |
| `UnityEventStringMonoBinder` | `string` | OneWay, OneTime | Вызов UnityEvent при изменении string |
| `UnityEventVector2MonoBinder` | `Vector2` | OneWay, OneTime | Вызов UnityEvent при изменении Vector2 |
| `UnityEventVector3MonoBinder` | `Vector3` | OneWay, OneTime | Вызов UnityEvent при изменении Vector3 |
| `UnityEventColorMonoBinder` | `Color` | OneWay, OneTime | Вызов UnityEvent при изменении Color |
| `UnityEventQuaternionMonoBinder` | `Quaternion` | OneWay, OneTime | Вызов UnityEvent при изменении Quaternion |
| `UnityEventSwitcherMonoBinder` | `bool` | OneWay, OneTime | Вызов одного из двух UnityEvent по условию |

---

## Collections

Биндеры для коллекций ViewModel. [Подробнее](collection-binders.md)

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ViewModelObservableListBinder` | `ObservableList<IViewModel>` | OneWay, OneTime | Динамический список с фабрикой View |
| `ViewModelCollectionBinder` | `IReadOnlyList<IViewModel>` | OneWay, OneTime | Статическая коллекция |
| `ViewModelObservableDictionaryBinder` | `ObservableDict<K,IViewModel>` | OneWay, OneTime | Словарь с фабрикой View |
| `VirtualizedListItemSourceBinder` | `IVirtualizedListItemSource` | OneWay, OneTime | Источник данных для VirtualizedList |

---

## Selectable

Биндеры для `Selectable` (базовый класс Button, Toggle, Slider и т.д.).

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `SelectableInteractableBinder` | `bool` | OneWay, OneTime | Привязка `interactable` |
| `SelectableColorBlockBinder` | `ColorBlock` | OneWay, OneTime | Привязка `colors` |
| `SelectableColorBlockSwitcherBinder` | `bool` -> `ColorBlock` | OneWay, OneTime | Переключение цветовой схемы |

---

## Behaviour

Биндеры для `Behaviour`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `BehaviourEnabledBinder` | `bool` | OneWay, OneTime | Привязка `Behaviour.enabled` |

---

## Layout

Биндеры для Layout-компонентов.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `LayoutGroupPaddingBinder` | `RectOffset` / `int` | OneWay, OneTime | Привязка padding |
| `LayoutGroupPaddingSwitcherBinder` | `bool` -> padding | OneWay, OneTime | Переключение padding |
| `HorizontalOrVerticalLayoutSpacingBinder` | `float` | OneWay, OneTime | Привязка spacing |
| `HorizontalOrVerticalLayoutSpacingSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Переключение spacing |

---

## Object

Биндеры для `UnityEngine.Object`.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `ObjectNameBinder` | `string` | OneWay, OneTime | Привязка `Object.name` |

---

## Caster

MonoBinder-кастеры для преобразования типов между биндерами.

| Компонент | Преобразование | Описание |
|-----------|---------------|----------|
| `AnyToStringCasterMonoBinder` | `any` -> `string` | Преобразование любого значения в строку |
| `GenericToStringCasterMonoBinder` | `T` -> `string` | Типизированное преобразование в строку |
| `StringToBoolCasterMonoBinder` | `string` -> `bool` | Преобразование строки в bool |
| `TimeSpanToStringCasterMonoBinder` | `TimeSpan` -> `string` | Форматирование TimeSpan в строку |
| `Vector2ToVector3CasterMonoBinder` | `Vector2` -> `Vector3` | Конвертация 2D-вектора в 3D |
| `Vector3ToVector2CasterMonoBinder` | `Vector3` -> `Vector2` | Конвертация 3D-вектора в 2D |

---

## Generic

Универсальные биндеры для произвольных типов.

| Компонент | Режим | Описание |
|-----------|-------|----------|
| `UnityGenericOneWayBinder` | OneWay | Универсальный OneWay-биндер |
| `UnityGenericOneTimeBinder` | OneTime | Универсальный OneTime-биндер |
| `UnityGenericOneWayToSourceBinder` | OneWayToSource | Универсальный OneWayToSource-биндер |
| `UnityGenericTwoWayBinder` | TwoWay | Универсальный TwoWay-биндер |
| `UnityGenericCasterBinder` | -- | Универсальный кастер типов |

---

## Debug

Утилиты для отладки.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `DebugLogBinder` | `any` | OneWay, OneTime | Вывод значения в `Debug.Log` при изменении |

---

## Localization

Биндеры для интеграции с Unity Localization.

| Компонент | Тип данных | Режимы | Описание |
|-----------|-----------|--------|----------|
| `TextLocalizationEntryBinder` | `LocalizedString` | OneWay, OneTime | Привязка локализованной строки к TMP_Text |
| `TextLocalizationEntrySwitcherBinder` | `bool` -> `LocalizedString` | OneWay, OneTime | Переключение локализованной строки |
| `LocalizeStringEventEntryBinder` | `TableEntryReference` | OneWay, OneTime | Привязка ключа к LocalizeStringEvent |
| `LocalizeStringEventEntrySwitcherBinder` | `bool` -> `TableEntryReference` | OneWay, OneTime | Переключение ключа локализации |
| `LocalizeStringEventVariableBinder` | `string` / `int` / `float` | OneWay, OneTime | Привязка переменной в LocalizeStringEvent |

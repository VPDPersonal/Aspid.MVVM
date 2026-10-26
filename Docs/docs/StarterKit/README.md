# StarterKit Overview

StarterKit is the set of ready-to-use binders and components for Aspid.MVVM. Each binder connects one property of a Unity component to a ViewModel member, so no boilerplate has to be written by hand.

Every binder comes in two flavours:
- **Binder** (POCO): a serializable class embedded into a `[Bind]` field of a View. No MonoBehaviour required.
- **MonoBinder**: a MonoBehaviour wrapper added to a GameObject through the Inspector.

---

## Text

Binders for `TMP_Text` (TextMeshPro). [Details](text-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `TextBinder` | `string`, `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `TMP_Text.text` with number support and `CultureInfoMode` |
| `TextSwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Switches the text between two values |
| `TextFontBinder` | `TMP_FontAsset` | OneWay, OneTime, OneWayToSource | `TMP_Text.font` |
| `TextFontSwitcherBinder` | `bool` -> `TMP_FontAsset` | OneWay, OneTime | Switches the font between two values |
| `TextFontSizeBinder` | `float` | OneWay, OneTime, OneWayToSource | `TMP_Text.fontSize` |
| `TextFontSizeSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the font size |
| `TextAlignmentBinder` | `TextAlignmentOptions` | OneWay, OneTime, OneWayToSource | `TMP_Text.alignment` |
| `TextAlignmentSwitcherBinder` | `bool` -> `TextAlignmentOptions` | OneWay, OneTime | Switches the alignment |
| `TextFontStyleBinder` | `FontStyles` | OneWay, OneTime, OneWayToSource | `TMP_Text.fontStyle` |
| `TextAutoSizeBinder` | `bool` | OneWay, OneTime, OneWayToSource | `TMP_Text.enableAutoSizing` |
| `TextRichTextBinder` | `bool` | OneWay, OneTime, OneWayToSource | `TMP_Text.richText` |
| `TextCharacterSpacingBinder` | `float` | OneWay, OneTime, OneWayToSource | `TMP_Text.characterSpacing` |
| `TextLineSpacingBinder` | `float` | OneWay, OneTime, OneWayToSource | `TMP_Text.lineSpacing` |
| `TextMarginBinder` | `Vector4` | OneWay, OneTime, OneWayToSource | `TMP_Text.margin` (left, top, right, bottom) |
| `TextMaxVisibleCharactersBinder` | `int` | OneWay, OneTime, OneWayToSource | `TMP_Text.maxVisibleCharacters` |

---

## InputField

Binders for `TMP_InputField` (TextMeshPro). [Details](input-field-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `InputFieldBinder` | `string`, `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | Input text with feedback |
| `InputFieldCharacterValidationBinder` | `CharacterValidation` | OneWay, OneTime | Character validation |
| `InputFieldCharacterValidationSwitcherBinder` | `bool` -> `CharacterValidation` | OneWay, OneTime | Switches the validation |
| `InputFieldContentTypeBinder` | `ContentType` | OneWay, OneTime | Content type of the field |
| `InputFieldContentTypeSwitcherBinder` | `bool` -> `ContentType` | OneWay, OneTime | Switches the content type |
| `InputFieldInputTypeBinder` | `InputType` | OneWay, OneTime | Input type (Standard, AutoCorrect, Password) |
| `InputFieldInputTypeSwitcherBinder` | `bool` -> `InputType` | OneWay, OneTime | Switches the input type |
| `InputFieldLineTypeBinder` | `LineType` | OneWay, OneTime | Line type (SingleLine, MultiLine) |
| `InputFieldLineTypeSwitcherBinder` | `bool` -> `LineType` | OneWay, OneTime | Switches the line type |
| `InputFieldCharacterLimitBinder` | `int` | OneWay, OneTime, OneWayToSource | Character limit, `0` for none |
| `InputFieldCaretPositionBinder` | `int` | OneWay, OneTime, OneWayToSource | Caret position |
| `InputFieldReadOnlyBinder` | `bool` | OneWay, OneTime, OneWayToSource | `readOnly` |
| `InputFieldPlaceholderBinder` | `Graphic` | OneWay, OneTime, OneWayToSource | `placeholder` |

---

## Image

Binders for `UnityEngine.UI.Image`. [Details](image-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ImageSpriteBinder` | `Sprite`, `Texture2D` | OneWay, OneTime, OneWayToSource | Sprite with auto-disable on `null` |
| `ImageSpriteSwitcherBinder` | `bool` -> `Sprite` | OneWay, OneTime | Switches the sprite between two values |
| `ImageFillBinder` | `float` | OneWay, OneTime, OneWayToSource | `fillAmount` (0-1) |
| `ImageFillSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the fill |
| `ImageSpriteAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Sprite loaded through Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `ImageTypeBinder` | `Image.Type` | OneWay, OneTime, OneWayToSource | `Image.type` |
| `ImagePreserveAspectBinder` | `bool` | OneWay, OneTime, OneWayToSource | `preserveAspect` |
| `ImageFillOriginBinder` | `int` | OneWay, OneTime, OneWayToSource | `fillOrigin` |
| `ImageFillClockwiseBinder` | `bool` | OneWay, OneTime, OneWayToSource | `fillClockwise` |
| `Image{Sprite, Fill}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one Image or a group |

---

## RawImage

Binders for `UnityEngine.UI.RawImage`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `RawImageTextureBinder` | `Texture`, `Sprite` | OneWay, OneTime, OneWayToSource | Texture with auto-disable on `null` |
| `RawImageTextureSwitcherBinder` | `bool` -> `Texture` | OneWay, OneTime | Switches the texture |
| `RawImageTextureAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Texture loaded through Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `RawImageTextureEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Texture by enum, for one RawImage or a group |
| `RawImageUvRectBinder` | `Rect` | OneWay, OneTime, OneWayToSource | `uvRect`; non-finite values are rejected |

---

## Button / Command

Command binders for UI elements.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ButtonCommandBinder` | `IRelayCommand`, `IRelayCommand<bool>` | OneWay, OneTime | Command on Button.onClick with InteractableMode |
| `ButtonCommandBinder<T>` … `<T1,T2,T3,T4>` | `IRelayCommand<T…>` | OneWay, OneTime | Command with 1–4 parameters from the Inspector |
| `ButtonCommand{Int, Float, Bool, String, Object}MonoBinder` | `IRelayCommand<T>` | OneWay, OneTime | Ready-made Mono variants with one parameter |
| `ToggleCommandBinder` | `IRelayCommand`, `IRelayCommand<bool>` | OneWay, OneTime | Command on Toggle.onValueChanged |
| `SliderCommandBinder` | `IRelayCommand<int/long/float/double>` | OneWay, OneTime | Command on Slider.onValueChanged |
| `DropdownCommandBinder` | `IRelayCommand<int>` | OneWay, OneTime | Command on TMP_Dropdown.onValueChanged |
| `InputFieldCommandBinder` | `IRelayCommand`, `IRelayCommand<string>` | OneWay, OneTime | Command on a TMP_InputField event (`UpdateInputFieldEvent`) |
| `ScrollRectCommandBinder` | `IRelayCommand<Vector2>`, `IRelayCommand<Vector3>` | OneWay, OneTime | Command on ScrollRect.onValueChanged |
| `ScrollbarCommandBinder` | `IRelayCommand<int/long/float/double>` | OneWay, OneTime | Command on Scrollbar.onValueChanged |
| `EventTriggerCommandBinder` | `IRelayCommand`, `IRelayCommand<BaseEventData>`, `IRelayCommand<EventTriggerType>` | OneWay, OneTime | Command on the chosen EventTrigger event; `<T1..T3>` variants with parameters |

---

## Slider

Binders for `UnityEngine.UI.Slider`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `SliderValueBinder` | `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | `Slider.value` with feedback |
| `SliderMinMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Slider min/max (`SliderRangeMode`) |
| `SliderMinMaxSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Switches min/max |
| `Slider{Value, MinMax}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one Slider or a group |
| `SliderValueSwitcherMonoBinder` | `bool` -> `float` | OneWay, OneTime | Switches `Slider.value` |

---

## Scrollbar

Binders for `UnityEngine.UI.Scrollbar`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ScrollbarValueBinder` | `int`, `float`, `long`, `double` | OneWay, TwoWay, OneTime, OneWayToSource | `Scrollbar.value` (in [0, 1]) with feedback |
| `ScrollbarSizeBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `Scrollbar.size` (in [0, 1]) |
| `ScrollbarValue{Switcher, Enum, EnumGroup}MonoBinder` | `bool` / `Enum` | OneWay, OneTime | Value by flag or enum |

---

## ScrollRect

Binders for `UnityEngine.UI.ScrollRect`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ScrollRectNormalizedPositionBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `normalizedPosition` (components in [0, 1]) |
| `ScrollRect{Horizontal, Vertical}NormalizedPositionBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Position along one axis (in [0, 1]) |
| `ScrollRect{Horizontal, Vertical}Binder` | `bool` | OneWay, OneTime, OneWayToSource | Enables scrolling along an axis |
| `ScrollRect{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one ScrollRect or a group |

---

## Toggle

Binders for `UnityEngine.UI.Toggle`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ToggleIsOnBinder` | `bool` | OneWay, TwoWay, OneTime, OneWayToSource | `Toggle.isOn` with an optional converter |
| `ToggleIsOnEnumBinder` | `Enum` -> `bool` | OneWay, OneTime | `isOn` by enum value |
| `ToggleIsOnEnumGroupBinder` | `Enum` -> `bool` | OneWay, OneTime | `isOn` of a Toggle group by enum value |
| `ToggleGroupAllowSwitchOffBinder` | `bool` | OneWay, OneTime | `ToggleGroup.allowSwitchOff` |

---

## Dropdown

Binders for `TMP_Dropdown`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `DropdownValueBinder` | `int` | OneWay, OneTime, OneWayToSource | Selected index |
| `DropdownValueSwitcherBinder` | `bool` -> `int` | OneWay, OneTime | Switches the index |
| `DropdownOptionsBinder` | `List<string>`, `List<Sprite>`, `IEnumerable<OptionData>` | OneWay, OneTime, OneWayToSource | Option list |
| `DropdownOptionsSwitcherBinder` | `bool` -> `List<OptionData>` | OneWay, OneTime | Switches the option list |
| `DropdownOptionsByEnumMonoBinder` | `Enum` | OneWay, OneTime | Options from the values of an enum type |
| `DropdownAlphaFadeSpeedBinder` | `float` | OneWay, OneTime, OneWayToSource | Fade speed |
| `DropdownAlphaFadeSpeedSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the fade speed |

---

## GameObject

Binders for `GameObject`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `GameObjectVisibleBinder` | `bool` | OneWay, OneTime, OneWayToSource | `SetActive` |
| `GameObjectVisibleByBindMonoBinder` | any | OneTime | Object shown while a binding exists |
| `GameObjectTagBinder` | `string` | OneWay, OneTime, OneWayToSource | `tag` |
| `GameObjectTagSwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Switches the tag |
| `GameObjectLayerBinder` | `int` | OneWay, OneTime, OneWayToSource | `layer`; a missing index is rejected |
| `GameObjectNameMonoBinder` | `string` | OneWay, OneTime, OneWayToSource | Object name; `null` clears |
| `GameObjectInstantiateAddressableMonoBinder` | `string`, `IKeyEvaluator` | OneWay, OneTime | Prefab instance from Addressables (`ASPID_MVVM_ADDRESSABLES_INTEGRATION`) |
| `GameObject{Visible, Tag}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one object or a group |

---

## Transform

Binders for `Transform` and `RectTransform`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `TransformPositionBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `localPosition` |
| `TransformPositionSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Switches the position |
| `TransformRotationBinder` | `Quaternion` | OneWay, OneTime, OneWayToSource | `localRotation` |
| `TransformRotationSwitcherBinder` | `bool` -> `Quaternion` | OneWay, OneTime | Switches the rotation |
| `TransformEulerAnglesBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `localEulerAngles` |
| `TransformEulerAnglesSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Switches the Euler angles |
| `TransformScaleBinder` | `Vector3`, `float` | OneWay, OneTime, OneWayToSource | `localScale` |
| `TransformScaleSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Switches the scale |
| `RectTransformAnchoredPositionBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `anchoredPosition` |
| `RectTransformAnchoredPositionSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Switches the anchored position |
| `RectTransformSizeDeltaBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `sizeDelta` |
| `RectTransformSizeDeltaSwitcherBinder` | `bool` -> `Vector3` | OneWay, OneTime | Switches the size |
| `RectTransformAnchorMinBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `anchorMin` |
| `RectTransformAnchorMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `anchorMax` |
| `RectTransformOffsetMinBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `offsetMin` |
| `RectTransformOffsetMaxBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `offsetMax` |
| `RectTransformPivotBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `pivot` |
| `RectTransformAnchorMinSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Switches `anchorMin` |
| `RectTransformAnchorMaxSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Switches `anchorMax` |
| `RectTransformPivotSwitcherBinder` | `bool` -> `Vector2` | OneWay, OneTime | Switches `pivot` |
| `TransformParentBinder` | `Transform` | OneWay, OneTime, OneWayToSource | `Transform.parent` |
| `TransformSiblingIndexBinder` | `int` | OneWay, OneTime, OneWayToSource | Index among siblings |

---

## CanvasGroup

Binders for `CanvasGroup`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `CanvasGroupAlphaBinder` | `float` | OneWay, OneTime, OneWayToSource | `alpha` |
| `CanvasGroupAlphaSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the opacity |
| `CanvasGroupInteractableBinder` | `bool` | OneWay, OneTime, OneWayToSource | `interactable` |
| `CanvasGroupBlocksRaycastsBinder` | `bool` | OneWay, OneTime, OneWayToSource | `blocksRaycasts` |
| `CanvasGroupIgnoreParentGroupsBinder` | `bool` | OneWay, OneTime, OneWayToSource | `ignoreParentGroups` |
| `CanvasGroup{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one CanvasGroup or a group |

---

## Animator

Binders for `Animator`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `AnimatorSetBoolBinder` | `bool` | OneWay, OneTime, OneWayToSource | Sets a bool parameter; in OneWayToSource hands out `Action<bool>`/`IRelayCommand<bool>` |
| `AnimatorSetIntBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Sets an int parameter |
| `AnimatorSetFloatBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Sets a float parameter |
| `AnimatorSetTriggerBinder` / `AnimatorResetTriggerBinder` | `Action`, `IRelayCommand` | OneWayToSource | `SetTrigger` / `ResetTrigger` as a command for the ViewModel |
| `AnimatorSpeedBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `Animator.speed` |
| `AnimatorLayerWeightBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Layer weight, [0, 1] |
| `AnimatorControllerBinder` | `RuntimeAnimatorController` | OneWay, OneTime, OneWayToSource | `runtimeAnimatorController` |
| `AnimatorPlayStateBinder` | `string` | OneWay, OneTime | `Animator.Play` of a state by name |

---

## Graphic / Renderer

Binders for `Graphic` (UI) and `Renderer` (3D).

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `GraphicColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | `Graphic.color` |
| `GraphicColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Switches the color |
| `GraphicColorChannelBinder` | `float` | OneWay, OneTime | Selected color channels (`ColorChannels`) |
| `GraphicColorChannelSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the selected channels |
| `GraphicMaterialBinder` | `Material` | OneWay, OneTime | `Graphic.material` |
| `GraphicMaterialSwitcherBinder` | `bool` -> `Material` | OneWay, OneTime | Switches the material |
| `RendererMaterialsBinder` | `Material`, `IReadOnlyCollection<Material>` | OneWay, OneTime, OneWayToSource | `Renderer.material` / `materials` |
| `RendererMaterialsSwitcherBinder` | `bool` -> `Material[]` | OneWay, OneTime | Switches the material array |
| `RendererMaterialsColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Shader property color on all materials |
| `RendererMaterialsColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Switches the material color |
| `RendererEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Renderer.enabled` |
| `RendererShadowCastingBinder` | `ShadowCastingMode` | OneWay, OneTime, OneWayToSource | `Renderer.shadowCastingMode` |
| `RendererSortingOrderBinder` | `int` | OneWay, OneTime, OneWayToSource | `Renderer.sortingOrder` |
| `RendererSortingLayerNameBinder` | `string` | OneWay, OneTime, OneWayToSource | `Renderer.sortingLayerName` |
| `RendererPropertyBlock{Float, Color, Vector, Texture}MonoBinder` | `float` / `Color` / `Vector4` / `Texture` | OneWay, OneTime | Shader property through `MaterialPropertyBlock` |
| `LineRendererColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | LineRenderer start/end color (`LineRendererColorMode`) |
| `LineRendererColorSwitcherBinder` | `bool` -> `Color` | OneWay, OneTime | Switches the LineRenderer color |
| `LineRendererColorEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Color by enum, for one LineRenderer or a group |
| `LineRendererLoopBinder` | `bool` | OneWay, OneTime, OneWayToSource | `LineRenderer.loop` |
| `LineRendererWidthMultiplierBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `widthMultiplier` (not below zero) |

---

## SpriteRenderer

Binders for `SpriteRenderer` (2D).

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `SpriteRendererSpriteBinder` | `Sprite`, `Texture2D` | OneWay, OneTime, OneWayToSource | `SpriteRenderer.sprite`; a texture is wrapped in a sprite |
| `SpriteRendererColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | `SpriteRenderer.color` |
| `SpriteRendererFlipXBinder` / `FlipYBinder` | `bool` | OneWay, OneTime, OneWayToSource | `flipX` / `flipY` |
| `SpriteRendererSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `size` (Sliced/Tiled); negative components are zeroed |
| `SpriteRendererSortingOrderBinder` | `int` | OneWay, OneTime, OneWayToSource | `sortingOrder` |
| `SpriteRenderer{Sprite, Color, FlipX, FlipY, SortingOrder}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one SpriteRenderer or a group |

---

## AudioSource

Binders for `AudioSource`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `AudioSourceVolumeBinder` | `float` | OneWay, OneTime, OneWayToSource | Volume |
| `AudioSourcePitchBinder` | `float` | OneWay, OneTime, OneWayToSource | Pitch |
| `AudioSourceClipBinder` | `AudioClip` | OneWay, OneTime, OneWayToSource | Audio clip |
| `AudioSourceMuteBinder` | `bool` | OneWay, OneTime, OneWayToSource | Mute |
| `AudioSourceLoopBinder` | `bool` | OneWay, OneTime, OneWayToSource | Loop |
| `AudioSourceTimeBinder` | `float` | OneWay, OneTime, OneWayToSource | Playback position |
| `AudioSourceSpatialBlendBinder` | `float` | OneWay, OneTime, OneWayToSource | 2D/3D blend |
| `AudioSourcePanStereoBinder` | `float` | OneWay, OneTime, OneWayToSource | Stereo pan |
| `AudioSourceDopplerLevelBinder` | `float` | OneWay, OneTime, OneWayToSource | Doppler level |
| `AudioSourceMinMaxDistanceBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | Min/max distance |
| `AudioSourcePriorityBinder` | `int` | OneWay, OneTime, OneWayToSource | Priority |
| `AudioSourceSpreadBinder` | `float` | OneWay, OneTime, OneWayToSource | Spread angle |
| `AudioSourceOutputAudioMixerGroupBinder` | `AudioMixerGroup` | OneWay, OneTime, OneWayToSource | Mixer group |
| `AudioSourceBypassEffectsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Bypass effects |
| `AudioSourceBypassListenerEffectsBinder` | `bool` | OneWay, OneTime, OneWayToSource | Bypass listener effects |
| `AudioSourceBypassReverbZonesBinder` | `bool` | OneWay, OneTime, OneWayToSource | Bypass reverb zones |
| `AudioSourceReverbZoneMixBinder` | `float` | OneWay, OneTime, OneWayToSource | Reverb zone mix |
| `AudioSourceTimeSamplesBinder` | `int` | OneWay, OneTime, OneWayToSource | Position in samples |

---

## Collider

Binders for colliders. [Details](collider-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ColliderEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Collider.enabled` |
| `ColliderIsTriggerBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Collider.isTrigger` |
| `ColliderMaterialBinder` | `PhysicsMaterial` | OneWay, OneTime, OneWayToSource | Physics material |
| `ColliderProvidesContactsBinder` | `bool` | OneWay, OneTime, OneWayToSource | `providesContacts` |
| `BoxColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | BoxCollider center |
| `BoxColliderSizeBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | BoxCollider size |
| `SphereColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | SphereCollider center |
| `SphereColliderRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | SphereCollider radius |
| `CapsuleColliderCenterBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | CapsuleCollider center |
| `CapsuleColliderRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | CapsuleCollider radius |
| `MeshColliderConvexBinder` | `bool` | OneWay, OneTime, OneWayToSource | `MeshCollider.convex` |
| `MeshColliderMeshBinder` | `Mesh` | OneWay, OneTime, OneWayToSource | Collider mesh |
| `MeshColliderCookingOptionsBinder` | `MeshColliderCookingOptions` | OneWay, OneTime, OneWayToSource | `MeshCollider.cookingOptions` |
| `CapsuleColliderHeightBinder` | `float` | OneWay, OneTime, OneWayToSource | CapsuleCollider height |
| `CapsuleColliderDirectionBinder` | `int` | OneWay, OneTime, OneWayToSource | CapsuleCollider axis (0..2) |
| `ColliderContactOffsetBinder` | `float` | OneWay, OneTime, OneWayToSource | `Collider.contactOffset` |
| `ColliderIncludeLayersBinder` | `int` | OneWay, OneTime, OneWayToSource | `Collider.includeLayers` |
| `ColliderExcludeLayersBinder` | `int` | OneWay, OneTime, OneWayToSource | `Collider.excludeLayers` |

### Collider2D

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `Collider2DIsTriggerBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Collider2D.isTrigger` |
| `Collider2DMaterialBinder` | `PhysicsMaterial2D` | OneWay, OneTime, OneWayToSource | `Collider2D.sharedMaterial` |
| `Collider2DOffsetBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `Collider2D.offset` |
| `Collider2DDensityBinder` | `float` | OneWay, OneTime, OneWayToSource | `Collider2D.density` |
| `BoxCollider2DSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | BoxCollider2D size |
| `CapsuleCollider2DSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | CapsuleCollider2D size |
| `CircleCollider2DRadiusBinder` | `float` | OneWay, OneTime, OneWayToSource | CircleCollider2D radius |

---

## Rigidbody

Binders for `Rigidbody` and `Rigidbody2D`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `RigidbodyMassBinder` / `Rigidbody2DMassBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `mass`; a non-finite value is rejected |
| `RigidbodyIsKinematicBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Rigidbody.isKinematic` |
| `RigidbodyUseGravityBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Rigidbody.useGravity` |
| `RigidbodyConstraintsBinder` | `RigidbodyConstraints` | OneWay, OneTime, OneWayToSource | `Rigidbody.constraints` |
| `Rigidbody2DBodyTypeBinder` | `RigidbodyType2D` | OneWay, OneTime, OneWayToSource | `Rigidbody2D.bodyType` |
| `Rigidbody2DGravityScaleBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `Rigidbody2D.gravityScale` |
| `Rigidbody2DSimulatedBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Rigidbody2D.simulated` |

---

## Aggregator / Conditional

Combining several bindings and choosing by condition; the result goes to a `UnityEvent<T>`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `AndBoolMonoBinder` / `OrBoolMonoBinder` | input `bool` | — | Aggregator: `UnityEvent<bool>` with the summary of all inputs |
| `FormatStringMonoBinder` | input `string` | — | Aggregator: `string.Format` over the inputs |
| `BoolAggregatorInputMonoBinder` / `StringAggregatorInputMonoBinder` | `bool` / `string` | OneWay, OneTime | Aggregator input with an index |
| `Conditional{Color, Float, String}MonoBinder` | `bool` | OneWay, OneTime | One of two values into a `UnityEvent<T>` |

---

## RateLimit / Tween

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `Debounce{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | Passes the last value once the stream goes quiet |
| `Throttle{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | At most one value per interval |
| `Delay{Float, String}MonoBinder` | `float` / `string` | OneWay, OneTime | Every value delayed, in order |
| `Tween{Float, Vector3, Color}MonoBinder` | `float` / `Vector3` / `Color` | OneWay, OneTime | Smooth transition to the new value through a `UnityEvent<T>` |

---

## UI Toolkit

The binders find the element in a `UIDocument` by name or USS class.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ElementLabelTextMonoBinder` | any | OneWay, OneTime | `Label.text` |
| `ElementTextFieldValueMonoBinder` | `string` | all | `TextField` text with feedback |
| `ElementSliderValueMonoBinder` | `int`, `float`, `long`, `double` | all | `Slider` value with feedback |
| `ElementButtonCommandMonoBinder` | `IRelayCommand` | OneWay, OneTime | `Button` click |
| `Element{Enabled, Display, Class}MonoBinder` | `bool` | OneWay, OneTime | `SetEnabled`, `display`, USS class |
| `ElementListViewItemsSourceMonoBinder` | `IReadOnlyList<object>`, observable/filtered | OneWay, OneTime | `ListView` source |

---

## UnityEvent

Binders that invoke a `UnityEvent` on value change. [Details](unity-event-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `UnityEvent{Bool, Int, Long, Float, Double, String, Color, Vector2, Vector3, Quaternion}MonoBinder` | the matching type | OneWay, OneTime | Invokes `UnityEvent<T>` with the value; the numeric and string ones accept every numeric type |
| `UnityEventEnumMonoBinder` | `Enum` | OneWay, OneTime | Invokes the `UnityEvent` mapped to the enum value |
| `UnityEventSwitcherMonoBinder` | `bool` | OneWay, OneTime | Invokes one of two `UnityEvent`s |
| `UnityEventNumberCondition(Switcher)MonoBinder` | `float` | OneWay, OneTime | Number → `bool` through a converter → `UnityEvent<bool>` or one of two events |
| `UnityEventBoolByBindMonoBinder` | any | OneTime | `UnityEvent<bool>` with whether a binding exists |

---

## Collections

Binders for ViewModel collections. [Details](collection-binders.md)

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ViewModelObservableListBinder` | `ObservableList<IViewModel>` | OneWay, OneTime | Dynamic list with a View factory |
| `ViewModelCollectionBinder` | `IReadOnlyList<IViewModel>` | OneWay, OneTime | Static collection |
| `ViewModelObservableDictionaryBinder` | `ObservableDict<K,IViewModel>` | OneWay, OneTime | Dictionary with a View factory |
| `VirtualizedListItemSourceBinder` | `IReadOnlyList<IViewModel>` | OneWay, OneTime | Data source for a VirtualizedList |

---

## Selectable

Binders for `Selectable` (the base of Button, Toggle, Slider and others).

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `SelectableInteractableBinder` | `bool` | OneWay, OneTime, OneWayToSource | `interactable` |
| `SelectableColorBlockBinder` | `ColorBlock` | OneWay, OneTime, OneWayToSource | `colors` |
| `SelectableColorBlockSwitcherBinder` | `bool` -> `ColorBlock` | OneWay, OneTime | Switches the color scheme |
| `SelectableTransitionBinder` | `Selectable.Transition` | OneWay, OneTime, OneWayToSource | `transition` |
| `SelectableTargetGraphicBinder` | `Graphic` | OneWay, OneTime, OneWayToSource | `targetGraphic` |
| `Selectable{Interactable, ColorBlock}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum, for one Selectable or a group |

---

## Behaviour

Binders for `Behaviour`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `BehaviourEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Behaviour.enabled`; an empty reference takes the first non-binder on the object |
| `BehaviourEnabledByBindMonoBinder` | any | OneTime | Binder enabled while a binding exists |
| `BehaviourEnabledEnum(Group)MonoBinder` | `Enum` | OneWay, OneTime | `enabled` by enum, for one Behaviour or a group |

---

## Layout

Binders for layout components.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `LayoutGroupPaddingBinder` | `RectOffset`, `int` | OneWay, OneTime, OneWayToSource | Padding; sides chosen through `RectSides` |
| `LayoutGroupPaddingSwitcherBinder` | `bool` -> `RectOffset` | OneWay, OneTime | Switches the padding |
| `HorizontalOrVerticalLayoutGroupSpacingBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Spacing |
| `HorizontalOrVerticalLayoutGroupSpacingSwitcherBinder` | `bool` -> `float` | OneWay, OneTime | Switches the spacing |
| `GridLayoutGroupCellSizeBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `cellSize` (not below zero) |
| `GridLayoutGroupSpacingBinder` | `Vector2` | OneWay, OneTime, OneWayToSource | `spacing` |
| `GridLayoutGroupConstraintBinder` | `GridLayoutGroup.Constraint` | OneWay, OneTime, OneWayToSource | `constraint` |
| `GridLayoutGroupConstraintCountBinder` | `int` | OneWay, OneTime, OneWayToSource | `constraintCount` |
| `{LayoutGroup, HorizontalOrVerticalLayoutGroup}{…}Enum(Group)MonoBinder` | `Enum` | OneWay, OneTime | Value by enum |

---

## Object

Binders for `UnityEngine.Object`.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ObjectNameBinder` | `string` | OneWay, OneTime, OneWayToSource | `Object.name` |

---

## Caster

MonoBinder casters that convert types between binders.

| Component | Conversion | Description |
|-----------|---------------|----------|
| `AnyToStringCasterMonoBinder` | `any` -> `string` | Any value to a string |
| `ValueToStringCasterMonoBinder<T>` | `T` -> `string` | Abstract base of typed to-string conversion |
| `TimeSpanToStringCasterMonoBinder` | `TimeSpan` -> `string` | Formats a TimeSpan as a string |
| `StringToBoolCasterMonoBinder` | `string` -> `bool` | String to bool |
| `StringToIntCasterMonoBinder` | `string` -> `int` | Parses a string into an int |
| `StringToFloatCasterMonoBinder` | `string` -> `float` | Parses a string into a float |
| `StringToEnumCasterMonoBinder<TEnum>` | `string` -> `TEnum` | Abstract base of string-to-enum parsing |
| `Vector2ToVector3CasterMonoBinder` | `Vector2` -> `Vector3` | 2D vector to 3D |
| `Vector3ToVector2CasterMonoBinder` | `Vector3` -> `Vector2` | 3D vector to 2D |

---

## Generic

Universal binders for arbitrary types.

| Component | Mode | Description |
|-----------|-------|----------|
| `DelegateOneWayBinder<T>` | OneWay | Universal OneWay binder over an `Action<T>` |
| `DelegateOneTimeBinder<T>` | OneTime | Universal OneTime binder over an `Action<T>` |
| `DelegateOneWayToSourceBinder<T>` | OneWayToSource | Universal OneWayToSource binder |
| `DelegateTwoWayBinder<T>` | TwoWay | Universal TwoWay binder |
| `CasterBinder<TFrom, TTo>` | OneWay, OneTime | Universal type caster |

---

## Other UI components

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `CanvasSortingOrderBinder` / `CanvasOverrideSortingBinder` | `int` / `bool` | OneWay, OneTime, OneWayToSource | `Canvas.sortingOrder`, `overrideSorting` |
| `CanvasScaler{UiScaleMode, ScaleFactor, ReferenceResolution, MatchWidthOrHeight}Binder` | `ScaleMode` / `float` / `Vector2` / `float` | OneWay, OneTime, OneWayToSource | `CanvasScaler` properties with Unity's constraints |
| `ContentSizeFitter{HorizontalFit, VerticalFit}Binder` | `ContentSizeFitter.FitMode` | OneWay, OneTime, OneWayToSource | Size fit modes |
| `AspectRatioFitter{AspectMode, AspectRatio}Binder` | `AspectMode` / `float` | OneWay, OneTime, OneWayToSource | Aspect mode and ratio |
| `LayoutElement{PreferredWidth, PreferredHeight, FlexibleWidth, FlexibleHeight}Binder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Element sizes; negative means "no preference" |
| `LayoutElementIgnoreLayoutBinder` | `bool` | OneWay, OneTime, OneWayToSource | `ignoreLayout` |
| `MaskShowMaskGraphicBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Mask.showMaskGraphic` |
| `RectMask2DPaddingBinder` | `Vector3` | OneWay, OneTime, OneWayToSource | `RectMask2D.padding` (left, bottom, right) |
| `Shadow{EffectColor, EffectDistance}Binder` | `Color` / `Vector2` | OneWay, OneTime, OneWayToSource | Color and offset of `Shadow`/`Outline` |

---

## ParticleSystem

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `ParticleSystemEmissionEnabledBinder` | `bool` | OneWay, OneTime, OneWayToSource | Toggles emission without stopping the system |
| `ParticleSystemEmissionRateBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `rateOverTime` multiplier, not below zero |
| `ParticleSystemStartColorBinder` | `Color` | OneWay, OneTime, OneWayToSource | Color of new particles |
| `ParticleSystem{Play, Stop, Pause, Clear}MonoBinder` | `Action`, `IRelayCommand` | OneWayToSource | Playback operation as a command for the ViewModel |

---

## Scene and rendering

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `Camera{BackgroundColor, FieldOfView, Orthographic, OrthographicSize}Binder` | `Color` / `float` / `bool` / `float` | OneWay, OneTime, OneWayToSource | Camera properties |
| `Light{Color, Intensity, Range, SpotAngle}Binder` | `Color` / `float` | OneWay, OneTime, OneWayToSource | Light properties |
| `VideoPlayer{Clip, IsLooping, PlaybackSpeed}Binder` | `VideoClip` / `bool` / `float` | OneWay, OneTime, OneWayToSource | Clip, looping, speed (0..10) |
| `NavMeshAgent{Speed, IsStopped}Binder` | `float` / `bool` | OneWay, OneTime, OneWayToSource | Agent off the NavMesh: writing `isStopped` is logged and skipped |

---

## Global state

Binders without a target: static engine properties.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `TimeScaleBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | `Time.timeScale`, not below zero |
| `TargetFrameRateBinder` | `int` | OneWay, OneTime, OneWayToSource | `Application.targetFrameRate`, not below -1 |
| `QualityLevelBinder` | `int` | OneWay, OneTime, OneWayToSource | Quality level, index limited to the project list |
| `ScreenFullScreenBinder` | `bool` | OneWay, OneTime, OneWayToSource | `Screen.fullScreen` |
| `AudioListener{Volume, Pause}Binder` | `float` / `bool` | OneWay, OneTime, OneWayToSource | Global volume and audio pause |
| `AudioMixerFloatBinder` | `int`, `float`, `long`, `double` | OneWay, OneTime, OneWayToSource | Exposed `AudioMixer` parameter |
| `AudioMixerSnapshotBinder` | `int`, `string` | OneWay, OneTime | Transition to a snapshot by index or name |

---

## Debug

Debugging utilities.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `DebugLogBinder` | any | all | Prints the value to `Debug.Log`; Editor and development builds only |

---

## Localization

Binders for the Unity Localization integration.

| Component | Data type | Modes | Description |
|-----------|-----------|--------|----------|
| `TextLocalizationEntryBinder` | `LocalizedString` | OneWay, OneTime | Localized string on a TMP_Text |
| `TextLocalizationEntrySwitcherBinder` | `bool` -> `LocalizedString` | OneWay, OneTime | Switches the localized string |
| `LocalizeStringEventEntryBinder` | `string` (entry key) | OneWay, OneTime, OneWayToSource | Key on a LocalizeStringEvent |
| `LocalizeStringEventEntrySwitcherBinder` | `bool` -> `string` | OneWay, OneTime | Switches the localization key |
| `LocalizeStringEventVariableBinder` | numbers, `bool`, `string`, `Object` | OneWay, OneTime | Smart String variable in a LocalizeStringEvent; created by value type |

---
title: "Class InputFieldExtensions"
sidebar_label: "InputFieldExtensions"
description: "Class InputFieldExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InputFieldExtensions {#Aspid_MVVM_StarterKit_InputFieldExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Event and number helpers shared by the `TMP_InputField` binders.

```csharp
public static class InputFieldExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InputFieldExtensions](Aspid.MVVM.StarterKit.InputFieldExtensions.md)



## Methods

### GetEvent\(TMP\_InputField, UpdateInputFieldEvent\) {#Aspid_MVVM_StarterKit_InputFieldExtensions_GetEvent_TMPro_TMP_InputField_Aspid_MVVM_StarterKit_UpdateInputFieldEvent_}

Returns the `TMP_InputField` event selected by <code class="paramref">updateEvent</code>.

```csharp
public static UnityEvent<string> GetEvent(this TMP_InputField field, UpdateInputFieldEvent updateEvent)
```

#### Parameters

`field` TMP\_InputField

The field whose event is returned.

`updateEvent` [UpdateInputFieldEvent](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md)

The event to select.

#### Returns

 UnityEvent\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The selected event.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">updateEvent</code> is unknown.

### RaiseNumber\(TMP\_InputField, ref NumberReverseChannel, string, CultureInfoMode\) {#Aspid_MVVM_StarterKit_InputFieldExtensions_RaiseNumber_TMPro_TMP_InputField_Aspid_MVVM_StarterKit_NumberReverseChannel__System_String_Aspid_MVVM_StarterKit_CultureInfoMode_}

Parses <code class="paramref">text</code> and raises it on the numeric channels when the field holds a number.

```csharp
public static void RaiseNumber(this TMP_InputField field, ref NumberReverseChannel channel, string text, CultureInfoMode culture)
```

#### Parameters

`field` TMP\_InputField

The field the text came from.

`channel` [NumberReverseChannel](Aspid.MVVM.StarterKit.NumberReverseChannel.md)

The numeric channels to raise.

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text to parse.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is parsed with.

#### Remarks

Only `IntegerNumber` and
`DecimalNumber` fields report numbers. An integer channel receives
a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a> when the text fits one, since a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> loses integer precision
past 2^53.

### RemoveListenerFromAll\(TMP\_InputField, UnityAction\<string\>\) {#Aspid_MVVM_StarterKit_InputFieldExtensions_RemoveListenerFromAll_TMPro_TMP_InputField_UnityEngine_Events_UnityAction_System_String__}

Removes <code class="paramref">listener</code> from every event [`UpdateInputFieldEvent`](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md) can select.

```csharp
public static void RemoveListenerFromAll(this TMP_InputField field, UnityAction<string> listener)
```

#### Parameters

`field` TMP\_InputField

The field whose events are cleaned.

`listener` UnityAction\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The listener to remove.

#### Remarks

Used when the selected event may have changed since the listener was added.


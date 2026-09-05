---
title: "Class TextLocalizationEntryBinder"
sidebar_label: "TextLocalizationEntryBinder"
description: "Class TextLocalizationEntryBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TextLocalizationEntryBinder {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds `text` to a Unity
Localization entry.

```csharp
[Serializable]
public class TextLocalizationEntryBinder : TargetBinder<TMP_Text, string>, IRebindableBinder, IBinder<string>, IReverseBinder<string>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TMP\_Text\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<TMP\_Text, string\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[TextLocalizationEntryBinder](Aspid.MVVM.StarterKit.TextLocalizationEntryBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<string\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<TextLocalizationEntryBinder\>\(TextLocalizationEntryBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<TextLocalizationEntryBinder\>\(TextLocalizationEntryBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<TextLocalizationEntryBinder\>\(TextLocalizationEntryBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The bound string is the table entry key; the localized text is written whenever the entry resolves.

## Constructors

### TextLocalizationEntryBinder\(TMP\_Text, string?, List\<Object\>?, IConverter\<string?, string?\>?, BindMode\) {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder__ctor_TMPro_TMP_Text_System_String_System_Collections_Generic_List_UnityEngine_Object__Aspid_MVVM_StarterKit_IConverter_System_String_System_String__Aspid_MVVM_BindMode_}

```csharp
public TextLocalizationEntryBinder(TMP_Text target, string? entry = null, List<Object>? formatArguments = null, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_Text

The text to bind.

`entry` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The initial table entry key, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it unset.

`formatArguments` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<Object\>?

Format arguments passed to the localized string, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[string](https://learn.microsoft.com/dotnet/api/system.string)?, [string](https://learn.microsoft.com/dotnet/api/system.string)?\>?

The converter applied to the bound key, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it as-is.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

## Properties

### Property {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed string? Property { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### OnBinding\(\) {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder_OnBinding}

Subscribes to [`StringChanged`](https://docs.unity3d.com/ScriptReference/Localization-LocalizedString-StringChanged.html) before the first value arrives.

```csharp
protected override void OnBinding()
```

#### Remarks

When overriding, always call <code>base.OnBinding()</code>.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder_OnUnbound}

Unsubscribes from [`StringChanged`](https://docs.unity3d.com/ScriptReference/Localization-LocalizedString-StringChanged.html).

```csharp
protected override void OnUnbound()
```

#### Remarks

When overriding, always call <code>base.OnUnbound()</code>.

### UpdateString\(string\) {#Aspid_MVVM_StarterKit_TextLocalizationEntryBinder_UpdateString_System_String_}

Writes the localized <code class="paramref">value</code> to `text`.

```csharp
protected virtual void UpdateString(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted localized string.


---
title: "Struct EnumToDropdownOptionDataConverter.OptionEntry"
sidebar_label: "EnumToDropdownOptionDataConverter.OptionEntry"
description: "Struct EnumToDropdownOptionDataConverter.OptionEntry — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct EnumToDropdownOptionDataConverter.OptionEntry {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

The label and icon authored for one enum member.

```csharp
[Serializable]
public struct EnumToDropdownOptionDataConverter.OptionEntry
```



## Constructors

### OptionEntry\(string, string?, Sprite?\) {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry__ctor_System_String_System_String_UnityEngine_Sprite_}

```csharp
public OptionEntry(string name, string? label = null, Sprite? icon = null)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

The member name this entry belongs to. A name the enum does not declare, and a name
listed twice, are reported as errors on every conversion.

`label` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text shown for it, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to show the member name.

`icon` Sprite?

The icon shown beside it.

## Properties

### Icon {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry_Icon}

Gets the icon shown beside the member.

```csharp
public readonly Sprite? Icon { get; }
```

#### Property Value

 Sprite?

### Label {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry_Label}

Gets the text shown for the member. When empty, the member name is used.

```csharp
public readonly string Label { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### Name {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry_Name}

Gets the member name this entry belongs to.

```csharp
public readonly string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)


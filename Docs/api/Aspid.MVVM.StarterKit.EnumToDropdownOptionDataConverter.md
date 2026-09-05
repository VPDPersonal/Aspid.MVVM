---
title: "Class EnumToDropdownOptionDataConverter"
sidebar_label: "EnumToDropdownOptionDataConverter"
description: "Class EnumToDropdownOptionDataConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnumToDropdownOptionDataConverter {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Builds the option list of a dropdown out of an enum's members.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Enum/To Collection", Name = "To Dropdown Options", Tooltip = "Builds the option list of a dropdown out of an enum's members")]
public sealed class EnumToDropdownOptionDataConverter : IConverter<Enum?, IEnumerable<TMP_Dropdown.OptionData>>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EnumToDropdownOptionDataConverter](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverter.md)

#### Implements

[IConverter\<Enum?, IEnumerable\<TMP\_Dropdown.OptionData\>\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The option list depends on the enum type, not the value, so it is built once per type and
reused; editing the entries afterward does not rebuild it.

## Constructors

### EnumToDropdownOptionDataConverter\(\) {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter__ctor}

```csharp
public EnumToDropdownOptionDataConverter()
```

#### Remarks

Default: every member by its name, with its InspectorName honored.

### EnumToDropdownOptionDataConverter\(OptionEntry\[\]?, bool\) {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter__ctor_Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_OptionEntry___System_Boolean_}

```csharp
public EnumToDropdownOptionDataConverter(EnumToDropdownOptionDataConverter.OptionEntry[]? entries, bool useInspectorNames = true)
```

#### Parameters

`entries` [EnumToDropdownOptionDataConverter](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverter.md).[OptionEntry](Aspid.MVVM.StarterKit.EnumToDropdownOptionDataConverter.OptionEntry.md)\[\]?

Labels and icons per member.

`useInspectorNames` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to use the [`InspectorNameAttribute`](https://docs.unity3d.com/ScriptReference/InspectorNameAttribute.html) for members
<code class="paramref">entries</code> does not cover.

## Methods

### Convert\(Enum?\) {#Aspid_MVVM_StarterKit_EnumToDropdownOptionDataConverter_Convert_System_Enum_}

Builds the option list for the type of the specified value.

```csharp
public IEnumerable<TMP_Dropdown.OptionData> Convert(Enum? value)
```

#### Parameters

`value` [Enum](https://learn.microsoft.com/dotnet/api/system.enum)?

Any member of the enum whose options are wanted.

#### Returns

 [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<TMP\_Dropdown.OptionData\>

One option per member, in declaration order. The same list is returned while the type is
unchanged, so it must not be mutated by the caller. A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value carries
no type to build from and answers with an empty list silently: a ViewModel with nothing
selected is a state, not a mistake.


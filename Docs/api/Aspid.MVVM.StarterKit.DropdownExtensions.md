---
title: "Class DropdownExtensions"
sidebar_label: "DropdownExtensions"
description: "Class DropdownExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DropdownExtensions {#Aspid_MVVM_StarterKit_DropdownExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods that write validated values to a `TMP_Dropdown`.

```csharp
public static class DropdownExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DropdownExtensions](Aspid.MVVM.StarterKit.DropdownExtensions.md)



## Methods

### SetOptions\(TMP\_Dropdown, List\<OptionData\>\) {#Aspid_MVVM_StarterKit_DropdownExtensions_SetOptions_TMPro_TMP_Dropdown_System_Collections_Generic_List_TMPro_TMP_Dropdown_OptionData__}

Replaces `options` with a copy of <code class="paramref">options</code>, keeping the selection
where the new list still has room for it.

```csharp
public static void SetOptions(this TMP_Dropdown dropdown, List<TMP_Dropdown.OptionData> options)
```

#### Parameters

`dropdown` TMP\_Dropdown

The dropdown whose options are replaced.

`options` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<TMP\_Dropdown.OptionData\>

The options to copy, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear.

#### Remarks

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> clears the options. The list is copied, so the source is never mutated by the dropdown.


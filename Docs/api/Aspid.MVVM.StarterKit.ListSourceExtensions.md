---
title: "Class ListSourceExtensions"
sidebar_label: "ListSourceExtensions"
description: "Class ListSourceExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ListSourceExtensions {#Aspid_MVVM_StarterKit_ListSourceExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a read-only collection as the [`IList`](https://learn.microsoft.com/dotnet/api/system.collections.ilist) a [`ListView`](https://docs.unity3d.com/ScriptReference/UIElements-ListView.html) takes.

```csharp
public static class ListSourceExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ListSourceExtensions](Aspid.MVVM.StarterKit.ListSourceExtensions.md)



## Methods

### ToListSource\(IReadOnlyList\<object\>\) {#Aspid_MVVM_StarterKit_ListSourceExtensions_ToListSource_System_Collections_Generic_IReadOnlyList_System_Object__}

Wraps <code class="paramref">list</code> without copying; every mutating member throws.

```csharp
public static IList ToListSource(this IReadOnlyList<object> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[object](https://learn.microsoft.com/dotnet/api/system.object)\>

The collection to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.ilist)

The [`IList`](https://learn.microsoft.com/dotnet/api/system.collections.ilist) view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.


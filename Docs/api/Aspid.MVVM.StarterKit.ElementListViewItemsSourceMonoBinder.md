---
title: "Class ElementListViewItemsSourceMonoBinder"
sidebar_label: "ElementListViewItemsSourceMonoBinder"
description: "Class ElementListViewItemsSourceMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ElementListViewItemsSourceMonoBinder {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`itemsSource`](https://docs.unity3d.com/ScriptReference/UIElements-BaseVerticalCollectionView-itemsSource.html)
of a [`ListView`](https://docs.unity3d.com/ScriptReference/UIElements-ListView.html) to a read-only collection.

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime })]
[AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – ListView Items Source")]
[AddBinderContextMenu(typeof(Component), new string[] { }, Path = "Add General Binder/UIToolkit/Element Binder – ListView Items Source")]
public sealed class ElementListViewItemsSourceMonoBinder : VisualElementMonoBinder<ListView>, IMonoBinderValidatable, IBinder<IReadOnlyObservableList<object>>, IBinder<IReadOnlyFilteredList<object>>, IBinder<IReadOnlyList<object>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[VisualElementMonoBinder\<ListView\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) ←
[ElementListViewItemsSourceMonoBinder](Aspid.MVVM.StarterKit.ElementListViewItemsSourceMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<IReadOnlyObservableList\<object\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IReadOnlyFilteredList\<object\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IReadOnlyList\<object\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ElementListViewItemsSourceMonoBinder\>\(ElementListViewItemsSourceMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<ElementListViewItemsSourceMonoBinder\>\(ElementListViewItemsSourceMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\<ElementListViewItemsSourceMonoBinder\>\(ElementListViewItemsSourceMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Observable and filtered lists are followed and refreshed on every change. The collection is wrapped so the
list view cannot write into it.

## Methods

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnElementDetached\(ListView\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_OnElementDetached_UnityEngine_UIElements_ListView_}

Called before an element leaves the binding. Override to release its callbacks.

```csharp
protected override void OnElementDetached(ListView element)
```

#### Parameters

`element` ListView

The previous element.

### RestoreElement\(\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_RestoreElement}

Called when a replacement element needs the last bound value.

```csharp
protected override void RestoreElement()
```

### SetValue\(IReadOnlyObservableList\<object\>\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_SetValue_Aspid_Collections_Observable_IReadOnlyObservableList_System_Object__}

```csharp
public void SetValue(IReadOnlyObservableList<object> value)
```

#### Parameters

`value` IReadOnlyObservableList\<[object](https://learn.microsoft.com/dotnet/api/system.object)\>

### SetValue\(IReadOnlyFilteredList\<object\>\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_SetValue_Aspid_Collections_Observable_Filtered_IReadOnlyFilteredList_System_Object__}

```csharp
public void SetValue(IReadOnlyFilteredList<object> value)
```

#### Parameters

`value` IReadOnlyFilteredList\<[object](https://learn.microsoft.com/dotnet/api/system.object)\>

### SetValue\(IReadOnlyList\<object\>\) {#Aspid_MVVM_StarterKit_ElementListViewItemsSourceMonoBinder_SetValue_System_Collections_Generic_IReadOnlyList_System_Object__}

```csharp
public void SetValue(IReadOnlyList<object> value)
```

#### Parameters

`value` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[object](https://learn.microsoft.com/dotnet/api/system.object)\>

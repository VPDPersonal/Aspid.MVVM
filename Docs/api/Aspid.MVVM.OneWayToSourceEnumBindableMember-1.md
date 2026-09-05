---
title: "Class OneWayToSourceEnumBindableMember<T>"
sidebar_label: "OneWayToSourceEnumBindableMember<T>"
description: "Class OneWayToSourceEnumBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceEnumBindableMember\<T\> {#Aspid_MVVM_OneWayToSourceEnumBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)
for one-way-to-source enum bindings, forwarding View-side enum changes back to the ViewModel.

```csharp
public sealed class OneWayToSourceEnumBindableMember<T> : OneWayToSourceStructBindableMember<T, Enum>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, Enum
```

#### Type Parameters

`T` 

The enum type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayToSourceStructBindableMember\<T, Enum\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) ← 
[OneWayToSourceEnumBindableMember\<T\>](Aspid.MVVM.OneWayToSourceEnumBindableMember-1.md)

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayToSourceEnumBindableMember\(Action\<T\>\) {#Aspid_MVVM_OneWayToSourceEnumBindableMember_1__ctor_System_Action__0__}

Initializes a new instance of the [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) class with the specified value setter action.

```csharp
public OneWayToSourceEnumBindableMember(Action<T> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.


---
cover: .gitbook/assets/Frame 58.png
coverY: 0
---

# 🐍 Introduction

<mark style="color:$primary;">**Aspid.MVVM**</mark> <mark style="color:$primary;">**is a high-performance MVVM framework**</mark> for [**Unity**](https://unity.com/), built on [Source Generator](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md), designed with a focus on simplicity, scalability, and clear separation of business logic from presentation.

The framework enables a clean architecture where the View, logic, and data are distinctly separated. This allows programmers and designers to work in parallel without interfering with each other, enabling teams to scale projects without descending into chaos.

***

<h2 align="center">[<a href="https://github.com/VPDPersonal/Aspid.MVVM/tree/main">Source Code</a>] [<a href="https://assetstore.unity.com/packages/slug/298463">Unity Assets Store</a>]</h2>

<h2 align="center">[<a href="./#donate">Donate</a>] [<a href="./#get-started">Getting Started</a>]</h2>

***

<h2 align="center"><i class="fa-square-bolt">:square-bolt:</i> Key Features</h2>

### <i class="fa-link-horizontal">:link-horizontal:</i> **Data Binding**

<mark style="color:$primary;">**Aspid.MVVM**</mark> supports four primary data binding modes between View and ViewModel:

* <mark style="color:$primary;">**OneWay**</mark> – Automatically updates the View when the ViewModel changes.
* <mark style="color:$primary;">**TwoWay**</mark> – Bidirectional synchronization between View and ViewModel.
* <mark style="color:$primary;">**OneTime**</mark> – Sets the value once during initialization.
* <mark style="color:$primary;">**OneWayToSource**</mark> – Updates the ViewModel when the View changes.

Binding modes can be easily specified:

* In the **View**: Directly via the Unity Inspector.
* In the **ViewModel**: Using attributes to restrict allowed binding modes.

{% hint style="success" %}
## Bindings operate without reflection or boxing/unboxing, ensuring high performance.
{% endhint %}

***

### <i class="fa-file-code">:file-code:</i> ViewModel

With the built-in Source Generator, you can bind any data type:

* No need to inherit from specialized base classes.
* No wrappers or wrapped properties required.
* No boilerplate code, using attributes for configuration.

***

### <i class="fa-command">:command:</i> Commands

A power command mechanism:

* Supports up to four parameters — simply select the desired signature.
* The <mark style="color:$warning;">`[RelayCommand]`</mark> attribute transforms a regular method into a command with <mark style="color:$warning;">`CanExecute`</mark> support.

***

### <i class="fa-list-ol">:list-ol:</i> Observable Collections

A set of flexible, covariant observable collections:

* <mark style="color:$warning;">`ObservableList<T>`</mark>
* <mark style="color:$warning;">`ObservableDictionary<TKey, TValue>`</mark>
* <mark style="color:$warning;">`ObservableHasSet<T>`</mark>
* <mark style="color:$warning;">`ObservableStack<T>`</mark>
* <mark style="color:$warning;">`ObservableQueue<T>`</mark>

Features:

* Easy synchronization between two dependent collections.
* Support for filtering and sorting without modifying the source collection.

***

### <i class="fa-rocket-launch">:rocket-launch:</i> StarterKit

A ready-to-use set of components for a quick start:

* <mark style="color:$primary;">**Binders**</mark>: Quickly connect to the desired component property.
* <mark style="color:$primary;">**Value Converters**</mark>: Transform values for display without altering the ViewModel.
* <mark style="color:$primary;">**List Components**</mark>: Including:
  * <mark style="color:$primary;">**Virtualized List**</mark>: Efficiently handles thousands of elements.
* <mark style="color:$primary;">**Dynamic ViewModel**</mark>: For simple structured data without writing a specialized ViewModel.
* <mark style="color:$primary;">**View Initialization Components**</mark>: Initialize Views by ViewModel via the Unity Inspector, with support for popular DI frameworks: [Zenject](https://github.com/Mathijs-Bakker/Extenject), [VContainer](https://github.com/hadashiA/VContainer).

***

### <i class="fa-bug">:bug:</i> Convenient Debuggin&#x67;**:**&#x20;

* View and modify ViewModel state directly in the Unity Inspector, even for plain C# classes.
* <mark style="color:$warning;">`[BinderLog]`</mark> attribute for automatic logging of value changes.
* Clear visual errors in the editor for incorrect bindings.

***

### <i class="fa-bolt">:bolt:</i> High Performance

<mark style="color:$primary;">**Aspid.MVVM**</mark> is designed with performance in mind:

* No reflection in bindings.
* No boxing/unboxing when passing values.
* Minimized memory allocations.

***

### <i class="fa-maximize">:maximize:</i> Extensibility

The framework is easily extensible:

* Create custom binders, converters, components, and more.
* Extend the framework to suit project needs without modifying its core.

***

### <i class="fa-laptop-mobile">:laptop-mobile:</i> Cross-Platform Support

<mark style="color:$primary;">**Aspid.MVVM**</mark> works on all Unity-supported platforms:

* PC, mobile devices, consoles.
* Create different Views for different platforms without changing the ViewModel or business logic.

***

<h2 align="center"><i class="fa-square-user">:square-user:</i> Who is Aspid.MVVM For?</h2>

### <i class="fa-unity">:unity:</i> <mark style="color:$primary;">Unity Developers</mark>, who want to:

* Simplify maintenance of complex UI and other presentations.
* Improve code structure, avoiding "spaghetti code" in large projects.
* Achieve an architecture suitable for testing and extension.

***

### <i class="fa-users">:users:</i> <mark style="color:$primary;">Teams</mark>, aiming to:

* Enable parallel work for designers and developers.
* Implement modular development and testing.
* Build scalable applications.

***

### <i class="fa-folder">:folder:</i> <mark style="color:$primary;">Projects</mark>, where the following are critical:

* Flexibility in adapting to changing requirements.
* Robust architecture.
* High performance.

***

{% hint style="success" %}
## Aspid.MVVM makes MVVM in Unity not only possible but also convenient.
{% endhint %}

***

<h2 align="center"><i class="fa-money-bill">:money-bill:</i> Donate</h2>

This project is developed on a voluntary basis. If you find it useful, you can support its development financially. This helps allocate more time to improving and maintaining <mark style="color:$primary;">**Aspid.MVVM**</mark>.

You can donate via the following platforms:

* \[[Unity Asset Store](https://assetstore.unity.com/packages/slug/298463)]

***

<h2 align="center"><i class="fa-star">:star:</i> Get Started</h2>

Here are some helpful pages to quickly and easily get started with our product:

<table data-card-size="large" data-view="cards"><thead><tr><th align="center"></th><th data-hidden data-card-target data-type="content-ref"></th><th data-hidden data-card-cover data-type="files"></th></tr></thead><tbody><tr><td align="center"><a href="introduction/what-is-mvvm.md"> What is MVVM?</a></td><td><a href="introduction/what-is-mvvm.md">what-is-mvvm.md</a></td><td><a href=".gitbook/assets/Aspid.MVVM Preview.png">Aspid.MVVM Preview.png</a></td></tr><tr><td align="center"><a href="introduction/getting-started/"> Getting Started</a></td><td><a href="introduction/getting-started/">getting-started</a></td><td><a href=".gitbook/assets/C Code Illustration.jpg">C Code Illustration.jpg</a></td></tr><tr><td align="center"><a href="overview/overview-viewmodel.md">Overview - ViewModel</a></td><td><a href="overview/overview-viewmodel.md">overview-viewmodel.md</a></td><td><a href=".gitbook/assets/image (71).png">image (71).png</a></td></tr><tr><td align="center"><a href="overview/overview-commands.md">Overview - Commands</a></td><td><a href="overview/overview-commands.md">overview-commands.md</a></td><td><a href=".gitbook/assets/Cyberpunk Holographic Design.jpg">Cyberpunk Holographic Design.jpg</a></td></tr><tr><td align="center"><a href="overview/overview-binders.md">Overview - Binders</a></td><td><a href="overview/overview-binders.md">overview-binders.md</a></td><td><a href=".gitbook/assets/Aspid.MVVM Preview Binders.png">Aspid.MVVM Preview Binders.png</a></td></tr><tr><td align="center"><a href="overview/overview-view.md">Overview - View</a></td><td><a href="overview/overview-view.md">overview-view.md</a></td><td><a href=".gitbook/assets/Beautiful Green UI Design.jpg">Beautiful Green UI Design.jpg</a></td></tr></tbody></table>

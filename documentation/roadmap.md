---
icon: map-location
---

# Roadmap

<h2 align="center">Features</h2>

### Planned

* [ ] <mark style="color:$primary;">**UIToolkit Support**</mark>: Integration with Unity’s UI Toolkit to enable MVVM data binding and UI development using UXML and USS, similar to XAML-based frameworks. This will align with modern Unity UI practices.
* [ ] <mark style="color:$primary;">**\[DebugRelayCommand]**</mark>
* [ ] <mark style="color:$primary;">**Improvements for VirtualizedList**</mark>
* [ ] <mark style="color:$primary;">**Property Binding in ViewModel**</mark>
* [ ] <mark style="color:$primary;">**Additional Code Analyzers:**</mark>
* [ ] <mark style="color:$primary;">**\[RelayCommand] for Properties and Fields**</mark>: Extend the <mark style="color:$warning;">`[RelayCommand]`</mark> attribute to generate commands directly from properties or fields, simplifying command creation in ViewModels.
* [ ] <mark style="color:$primary;">**Universal View for Specified ViewModel**</mark>: A generic View class that automatically adapts to any specified ViewModel, reducing the need for custom View implementations.
* [ ] <mark style="color:$primary;">**Validation Support for Nested Views in Unity**</mark>
* [ ] <mark style="color:$primary;">**Validation Support for Composite Binders in Unity**</mark>
* [ ] <mark style="color:$success;">**Attribute for Updating Commands on Property Changes**</mark>: An attribute to automatically trigger NotifyCanExecuteChanged for commands when dependent properties change, reducing manual boilerplate code.
* [ ] <mark style="color:$primary;">**Extended \[AsBinder] Support - Method Support:**</mark> Expand the <mark style="color:$warning;">`[AsBinder]`</mark> attribute to support binding to methods, enabling more flexible component interactions without requiring new binder classes.
* [ ] <mark style="color:$primary;">**Binding Any Component Field Without Creating a New Binder**</mark>: Allow direct binding to any field of a Unity component (e.g., <mark style="color:$warning;">`Image.sprite`</mark>) without needing a custom <mark style="color:$warning;">`MonoBinder`</mark>, streamlining View setup.

### &#x20;Under Consideration

* [ ] <mark style="color:$primary;">**Godot Support**</mark>: Explore adapting <mark style="color:$primary;">**Aspid.MVVM**</mark> for the Godot engine, enabling MVVM patterns for cross-engine development.
* [ ] <mark style="color:$primary;">**View Autogeneration:**</mark> Automatically generate View classes based on ViewModel definitions, reducing manual setup for common View scenarios.
* [ ] <mark style="color:$primary;">**Extended \[AsBinder] Support - Auto-Type Detection**</mark>: Enhance <mark style="color:$warning;">`[AsBinder]`</mark> to automatically detect the type of the target component, eliminating the need to specify types explicitly in some cases.
* [ ] <mark style="color:$primary;">**OneTimeToSource Binders:**</mark> Introduce OneTimeToSource binding mode for binders to simplify one-time data updates from View to ViewModel, complementing existing OneWay and TwoWay modes.
* [ ] <mark style="color:$primary;">**Reactive Properties in ViewModel - R3 and Custom Solutions Support**</mark>: Add support for reactive properties in ViewModels, integrating with R3 or custom reactive frameworks for real-time data updates.

<h2 align="center">Technical Aspects</h2>

### Planned

* [ ] <mark style="color:$primary;">**Refactor Generators for Simplified Logic**</mark>: Streamline the Source Generator codebase to reduce complexity and improve extensibility for features like property and command generation.
* [ ] <mark style="color:$primary;">**Refactor Unity Debugging Mechanism**</mark>: Simplify the debugging infrastructure for <mark style="color:$warning;">`MonoBinder`</mark> and <mark style="color:$warning;">`MonoView`</mark>, enhancing usability and reducing overhead in the Unity Editor.
* [ ] <mark style="color:$primary;">**Decouple MonoBinder and MonoView Dependency**</mark>: Remove dependencies between <mark style="color:$warning;">`MonoBinder`</mark> and <mark style="color:$warning;">`MonoView`</mark> debugging systems, enabling more flexible use and reducing potential errors in complex setups.


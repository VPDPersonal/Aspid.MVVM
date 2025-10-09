---
icon: list-dropdown
layout:
  width: default
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: true
  pagination:
    visible: true
  metadata:
    visible: true
---

# Bind Mode

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, the <mark style="color:$warning;">`BindMode`</mark> determines how and in which direction data is transferred between the ViewModel and the View.

By default, binders inheriting from <mark style="color:$warning;">`Binder`</mark> or <mark style="color:$warning;">`MonoBinder`</mark> can only be set to OneWay or OneTime binding modes in the Unity Inspector. To override the available binding modes in the Inspector, mark the binder with the <mark style="color:$warning;">`[BindModeOverride]`</mark> attribute.

```csharp
// Allows any binding mode to be selected in the Inspector.
[BindModeOverride(IsAll = true)]

// Allows OneWay and OneTime binding modes to be selected in the Inspector.
[BindModeOverride(IsOne = true)]

// Allows TwoWay and OneWayToSource binding modes to be selected in the Inspector.
[BindModeOverride(IsTwo = true)]

// Allows only the OneTime binding mode to be selected in the Inspector.
[BindModeOverride(BindMode.OneTime)]
```

#### Supported Bind Modes

<table><thead><tr><th width="156.48828125">Mode</th><th width="184.5859375">Direction</th><th>Description</th></tr></thead><tbody><tr><td>OneWay</td><td>ViewModel ⇒ View</td><td>Data flows only from ViewModel to View.</td></tr><tr><td>TwoWay</td><td>ViewModel ⇄ View</td><td>Full bidirectional synchronization.</td></tr><tr><td>OneTime</td><td>ViewModel ⇒ View</td><td>Data is transferred only during initialization.</td></tr><tr><td>OneWayToSource</td><td>View ⇒ ViewModel</td><td>Only user input updates the ViewModel; ViewModel does not affect the View.</td></tr></tbody></table>

---
icon: sidebar
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

# ScriptableView

To streamline the integration of Views with the Unity Inspector and the <mark style="color:$primary;">**Aspid.MVVM**</mark> binding system, the <mark style="color:$warning;">`ScriptableView`</mark> abstract class is used for <mark style="color:$warning;">`ScriptableObject`</mark>.

This class:

* Combines <mark style="color:$warning;">`ScriptableObject`</mark> and <mark style="color:$warning;">`IView`</mark>.
* Supports debugging directly in the Unity Inspector.

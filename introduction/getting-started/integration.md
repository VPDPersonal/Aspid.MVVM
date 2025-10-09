---
icon: plug-circle-bolt
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

# Integration

You can install <mark style="color:$primary;">**Aspid.MVVM**</mark> using one of the following methods:

* <mark style="color:$primary;">**Download .unitypackage**</mark>**:** Visit the [Release page on GitHub](https://github.com/VPDPersonal/Aspid.MVVM/releases) and download the latest version, <mark style="color:$warning;">`Aspid.MVVM.X.X.X.unitypackage`</mark>. Import it into your project.
* [<mark style="color:$primary;">**Via Unity Asset Store**</mark>](https://assetstore.unity.com/packages/slug/298463)

***

<h2 align="center">StarterKit</h2>

<mark style="color:$primary;">**Aspid.MVVM**</mark> is designed for seamless integration within the Unity ecosystem and works effortlessly with other popular tools. For full functionality, we recommend using the StarterKit, which includes integration with the following components:

### <i class="fa-syringe">:syringe:</i> Dependency Injection

<mark style="color:$primary;">**Aspid.MVVM**</mark> supports automatic ViewModel injection into Views for two DI frameworks:

#### [Zenject](https://github.com/Mathijs-Bakker/Extenject)

To enable Zenject support:

1. Install Zenject in your project.
2. Enable the <mark style="color:$warning;">`ASPID_MVVM_ZENJECT_INTEGRATION`</mark> compilation symbol:
   * Open <mark style="color:$primary;">**Project Settings**</mark> in Unity.&#x20;
   * Navigate to <mark style="color:$primary;">**Player**</mark> <mark style="color:$primary;"></mark><mark style="color:$primary;">→</mark><mark style="color:$primary;">**Other**</mark> <mark style="color:$primary;">**Settings**</mark>.&#x20;
   * Add <mark style="color:$warning;">`ASPID_MVVM_ZENJECT_INTEGRATION`</mark> to the <mark style="color:$primary;">**Scripting Define Symbols**</mark> field for each required platform.

#### [VContainer](https://github.com/hadashiA/VContainer)

VContainer support is automatically enabled if the <mark style="color:$warning;">`jp.hadashikick.vcontainer`</mark> package is installed in your project. If automatic detection fails, you can enable it manually:

* Open <mark style="color:$primary;">**Project Settings**</mark> in Unity.&#x20;
* Navigate to <mark style="color:$primary;">**Player**</mark> <mark style="color:$primary;"></mark><mark style="color:$primary;">→</mark> <mark style="color:$primary;"></mark><mark style="color:$primary;">**Other**</mark> <mark style="color:$primary;">**Settings**</mark>.&#x20;
* Add <mark style="color:$warning;">`ASPID_MVVM_VCONTAINER_INTEGRATION`</mark> to the <mark style="color:$primary;">**Scripting Define Symbols**</mark> field for each required platform.

#### Zenject and VContainer

Both frameworks can be used simultaneously. In this case, <mark style="color:$primary;">**Aspid.MVVM**</mark> first attempts to resolve dependencies via Zenject, then falls back to VContainer if Zenject fails.

### <i class="fa-address-book">:address-book:</i> Addressables

<mark style="color:$primary;">**Aspid.MVVM**</mark> supports integration with the [Addressables](https://docs.unity3d.com/Packages/com.unity.addressables@2.7/manual/index.html) system for asynchronous resource loading. To enable Addressables support:

1. [Install the Addressables package](../../) via Package Manager.
2. If automatic package detection fails, enable the compilation symbols manually:
   * Open <mark style="color:$success;">**Project Settings**</mark> in Unity.
   * Navigate to <mark style="color:$primary;">**Player → Other Settings**</mark>.
   * Add <mark style="color:$warning;">`ASPID_MVVM_ADDRESSABLES_INTEGRATION`</mark> for the Addressables package to the <mark style="color:$primary;">**Scripting Define Symbols**</mark> field for each required platform.

### <i class="fa-file-binary">:file-binary:</i> SerializeReference&#x20;

All converters included in the StarterKit use the [\[SerializeReference\]](https://docs.unity3d.com/ScriptReference/SerializeReference.html) attribute to display objects in the Unity Inspector. Since Unity does not display such fields by default, <mark style="color:$primary;">**Aspid.MVVM**</mark> supports integration with [SerializeReferenceDropdown](https://github.com/AlexeyTaranov/SerializeReferenceDropdown).

{% hint style="warning" %}
* Compatibility with [SerializeReferenceDropdown](https://github.com/AlexeyTaranov/SerializeReferenceDropdown) has been verified with version **1.1.4**.
* Future releases will include a custom solution for visualizing <mark style="color:$warning;">`SerializeReference`</mark> in the Inspector.
{% endhint %}

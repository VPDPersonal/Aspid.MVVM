---
icon: file-code
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

# ScriptableViewModel

To implement a ViewModel as a <mark style="color:$warning;">`ScriptableObject`</mark>, it is recommended to inherit from <mark style="color:$warning;">`ScriptableViewModel`</mark> for enhanced debugging capabilities. When fields are modified through the Unity Inspector, [all binding members are updated](../viewmodel/binding-members/notifyall.md) to reflect the changes in the View.

## Usage

```csharp
using Aspid.MVVM;
using UnityEditor;

[ViewModel]
public partial class MyScriptableViewModel : ScriptableViewModel
{
    [OneWayBind]
    [SerializeField] [Min(0)] private int _age;
    
    [OneWayBind]
    [SerializeField] private string _name;
}
```

<figure><img src="../../.gitbook/assets/image (47).png" alt=""><figcaption></figcaption></figure>

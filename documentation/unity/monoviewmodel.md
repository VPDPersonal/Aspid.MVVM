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

# MonoViewModel

To implement a ViewModel as a <mark style="color:$warning;">`MonoBehaviour`</mark>, it is recommended to inherit from <mark style="color:$warning;">`MonoViewModel`</mark> for enhanced debugging capabilities. When fields are modified through the Unity Inspector, [all binding members are updated](../viewmodel/binding-members/notifyall.md) to reflect the changes in the View.

## Usage

```csharp
using Aspid.MVVM;
using UnityEngine;
using Aspid.Collections.Observable;

namespace Aspid.MVVM.SamplesVirtualizedList
{
    [ViewModel]
    public sealed partial class ListViewModel : MonoViewModel
    {
        [SerializeField] [Min(0)] private int _count = 100;
           
        [OneTimeBind] private readonly ObservableList<int> _items = new();

        private int _number;
        
        [RelayCommand]
        private void AddViewModel() =>
            Items.Add(CreateElement());

        [RelayCommand]
        private void InsertViewModel(int index) =>
            Items.Insert(index, CreateElement());
        
        [RelayCommand]
        private void Move(int oldIndex, int newIndex) =>
            Items.Move(oldIndex, newIndex);
        
        [RelayCommand]
        private void Swap(int index1, int index2) =>
            Items.Swap(index1, index2);

        [RelayCommand]
        private void Remove(int index) =>
            Items.RemoveAt(index);

        [RelayCommand]
        private void Replace(int index) =>
            Items[index] = CreateElement();

        private int CreateElement() => _number++;
    }
}
```

<figure><img src="../../.gitbook/assets/image (70).png" alt=""><figcaption></figcaption></figure>

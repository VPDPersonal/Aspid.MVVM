---
icon: hand-wave
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

# Hello World

<mark style="color:$primary;">**The "Hello**</mark> <mark style="color:$primary;">**World"**</mark> example is a simple demonstration that covers all the key aspects of <mark style="color:$primary;">**Aspid.MVVM**</mark>. You can find the complete example after importing <mark style="color:$primary;">**Aspid.MVVM**</mark> into your project at the path: <mark style="color:$warning;">`Assets/Samples/Aspid/MVVM/HelloWorld`</mark>.

## Task Description

The goal is to implement a simple user interface where a user can input any string, and upon clicking a button, the string is displayed in a predefined text element.

## Model

First, create a model that implements the required logic: storing the text to be displayed and providing a method to set new text.

```csharp
using System;

public class Speaker
{
    public event Action<string> TextChanged;
    
    private string _text;
    
    public string Text
    {
        get => _text;
        private set
        {
            _text = value;
            TextChanged?.Invoke();
        }
    }
    
    public void Say(string text) =>
        Text = text;
}
```

## Next

If you are unfamiliar with the benefits of the MVVM pattern, we recommend first reviewing the example of this task implemented using the [MVP](mvp.md) pattern, then proceeding to implement it with [MVVM](mvvm.md).

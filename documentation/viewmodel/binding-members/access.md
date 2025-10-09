---
icon: unlock
---

# \[Access]

<mark style="color:$primary;">**The \[Access] attribute**</mark> provides flexible control over the access modifiers of generated properties created with the [\[Bind\]](bind.md) attribute.

This is particularly useful when you want to control the visibility of <mark style="color:$warning;">`get`</mark> and <mark style="color:$warning;">`set`</mark> methods, for example:

* Allow reading from outside but restrict writing.
* Make the property visible only to derived classes (<mark style="color:$warning;">`protected`</mark>).
* Restrict access to within the ViewModel.

***

## <i class="fa-clock">:clock:</i> When to Use \[Access]

By default, generated properties have <mark style="color:$warning;">`private`</mark> access. The <mark style="color:$warning;">`[Access]`</mark> attribute allows you to override this behavior.

***

## <i class="fa-s">:s:</i> Syntax

```csharp
[Access(Access.Public)] // Public access for both get and set.
[Access(Get = Access.Public)] // Public access for get only.
[Access(Set = Access.Protected)] // Protected access for set only.
[Access(Get = Access.Protected, Set = Access.Public)] // Combined access.
```

***

## <i class="fa-square-code">:square-code:</i> Example

```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    // private string Text1
    // {
    //     get => _text1;
    //     set => SetText1(value);
    // }
    [Bind] private string _text1;
    
    // public string Text2
    // {
    //     get => _text2;
    //     set => SetText2(value);
    // }
    [Access(Access.Public)]
    [Bind] private string _text2;
    
    // protected string Text3
    // {
    //     get => _text3;
    //      set => SetText3(value);
    // }
    [Access(Access.Protected)]
    [Bind] private string _text3;
    
    // public string Text4
    // {
    //     get => _text4;
    //     private set => SetText4(value);
    // }
    [Access(Get = Access.Public)]
    [Bind] private string _text4;

    // protected string Text5
    // {
    //     get => _text5;
    //     private set => SetText5(value);
    // }
    [Access(Get = Access.Protected)]
    [Bind] private string _text5;
    
    // public string Text6
    // {
    //     private get => _text6;
    //      set => SetText6(value);
    // }
    [Access(Set = Access.Public)]
    [Bind] private string _text6;
    
    // protected string Text7
    // {
    //     private get => _text7;
    //     set => SetText7(value);
    // }
    [Access(Set = Access.Protected)]
    [Bind] private string _text7;
    
    // public string Text8
    // {
    //     get => _text8;
    //     protected set => SetText8(value);
    // }
    [Access(Get = Access.Public, Set = Access.Protected)]
    [Bind] private string _text8;
    
    // public string Text9
    // {
    //     protected get => _text9;
    //      set => SetText9(value);
    // }
    [Access(Get = Access.Protected, Set = Access.Public)]
    [Bind] private string _text9;
}
```


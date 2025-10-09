---
icon: link-horizontal
---

# Binding Members

<mark style="color:$primary;">**Aspid.MVVM**</mark> provides a flexible data binding system between View and ViewModel, enabling state synchronization in various directions.

The framework supports four binding modes, each defining how and when data is synchronized between ViewModel and View:

| Mode           | Direction        | Description                                     |
| -------------- | ---------------- | ----------------------------------------------- |
| OneWay         | ViewModel ⇒ View | Updates the View when the ViewModel changes.    |
| TwoWay         | ViewModel ⇄ View | Bidirectional updates (e.g., for input fields). |
| OneTime        | ViewModel ⇒ View | Single update during initialization.            |
| OneWayToSource | View ⇒ ViewModel | View updates the ViewModel, but not vice versa. |

***

## <i class="fa-hexagon-check">:hexagon-check:</i> Какой режим выбрать?

While TwoWay is the most versatile, explicitly specifying a mode provides:

* <mark style="color:$primary;">**Predictability**</mark>: Clearly indicates the expected binding behavior, simplifying code understanding and reducing errors.
* <mark style="color:$primary;">**Safety**</mark>: Exceptions prevent incorrect usage (e.g., attempting TwoWay where only OneWay is allowed).
* <mark style="color:$primary;">**Performance**</mark>: TwoWay binding, even when used for OneWay, is the most resource-intensive. OneTime binding is the most performant, as data is transferred only once during initialization.

***

## <i class="fa-link-simple">:link-simple:</i> Binding Attributes

ViewModel members participating in binding must be marked with specific attributes:

| Attribute                                       | Mode           |
| ----------------------------------------------- | -------------- |
| [\[Bind\]](bind.md)                             | Auto-detection |
| [\[OneWayBind\]](onewaybind.md)                 | OneWay         |
| [\[TwoWayBind\]](twowaybind.md)                 | TwoWay         |
| [\[OneTimeBind\]](onetimebind.md)               | OneTime        |
| [\[OneWayToSourceBind\]](onewaytosourcebind.md) | OneWayToSource |

***

## <i class="fa-fingerprint">:fingerprint:</i> Generate Id

Field names must follow a naming convention for correct generation of bound properties. The field can include a prefix (<mark style="color:$warning;">`_`</mark>_,_ <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>) or start with a lowercase letter. Examples:

* <mark style="color:$warning;">`_fieldName`</mark>
* <mark style="color:$warning;">`m_fieldName`</mark>
* <mark style="color:$warning;">`s_fieldName`</mark>
* <mark style="color:$warning;">`fieldName`</mark>

The ID for the bound field is generated as follows:

1. The prefix (<mark style="color:$warning;">`_`</mark>_,_ <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>) is removed.
2. The first character is converted to uppercase.

For more details, see the [Generate ID](../../generate-id.md) section.

***

## <i class="fa-square-code">:square-code:</i> Example

{% code fullWidth="false" %}
```csharp
using Aspid.MVVM;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private int _age;
    [OneWayBind] private string _firstName;
    [TwoWayBind] private string _lastName;
    [OneTimeBind] private int _id;
    [OneWayToSource] private int _salary;
}
```
{% endcode %}

{% hint style="warning" %}
#### Notes

* The class must be marked with the <mark style="color:$warning;">`[ViewModel]`</mark> attribute.
* For proper code generation, field names must follow the format: <mark style="color:$warning;">`_fieldName`</mark>, <mark style="color:$warning;">`m_fieldName`</mark>, <mark style="color:$warning;">`s_fieldName`</mark>, or <mark style="color:$warning;">`fieldName`</mark>. Field names cannot start with an uppercase letter.
{% endhint %}

{% hint style="danger" %}
#### Current Limitations

Only the following are supported:

1. Fields
2. Constants

Properties and indexers are not supported but are planned for future versions. For now, the [\[BindAlso\]](bindalso.md) attribute can be used to bind properties
{% endhint %}


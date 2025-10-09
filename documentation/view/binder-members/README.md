---
icon: link-horizontal
---

# Binder Members

In <mark style="color:$primary;">**Aspid.MVVM**</mark>, elements within a View that implement <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark> can be automatically bound to a ViewModel using the Source Generator.

This system significantly simplifies working with View components: you don’t need to manually configure bindings—the framework handles it for you.

All fields and properties that implement <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark> participate in code generation.

***

## <i class="fa-fingerprint">:fingerprint:</i> Generate ID

The generated ID for a field or property must match the ID of the corresponding Binding Member in the ViewModel. The ID generation for fields and properties implementing <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark> follows this algorithm:

1. The prefix (<mark style="color:$warning;">`_`</mark>, <mark style="color:$warning;">`m_`</mark>, <mark style="color:$warning;">`s_`</mark>) is removed
2. The first character is converted to uppercase if it is lowercase.

To override the ID, use the <mark style="color:$warning;">`[BindId(string id)]`</mark> attribute. For more details, see the [Generate ID](../../generate-id.md#generaciya-id-vo-view) section.

{% hint style="warning" %}
#### Notes

The class must be marked with the <mark style="color:$warning;">`[View]`</mark> attribute.
{% endhint %}

### How It Works?

* The Source Generator analyzes all fields and properties in a class marked with the <mark style="color:$warning;">`[View]`</mark> attribute.
* If an element implements <mark style="color:$warning;">`IBinder`</mark> or <mark style="color:$warning;">`IView`</mark>, it participates in binding.
* Each element involved in binding is linked to a [binding member](../../viewmodel/binding-members/) in the ViewModel with the same ID as the element.

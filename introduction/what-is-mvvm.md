---
icon: question
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

# What is MVVM?

<mark style="color:$primary;">**MVVM (Model-View-ViewModel)**</mark> is an architectural design pattern designed to efficiently organize presentations in applications. It separates the View, Model, and ViewModel, resulting in cleaner, more scalable, and testable code.

<h2 align="center">MVVM Components</h2>

### <i class="fa-sidebar">:sidebar:</i> View&#x20;

This is everything the user sees and interacts with:

* Responsible for displaying data.
* Contains no business logic.
* Sends commands to and receives updates from the ViewModel.

***

### <i class="fa-file-code">:file-code:</i> ViewModel

Manages the state displayed in the View:

* Handles user interactions.
* Executes presentation-related business logic.
* Connects to the View through Data Binding.
* Contains commands and observable properties.

***

### <i class="fa-cabinet-filing">:cabinet-filing:</i> Model

Represents the data and business logic:

* Unaware of the View and ViewModel.
* Encapsulates the application's data and core business logic.

***

<h2 align="center">How MVVM Works</h2>

<div data-full-width="false"><figure><img src="../.gitbook/assets/MVVM Scheme Diagram.jpg" alt=""><figcaption></figcaption></figure></div>

1. The user interacts with the View (e.g., clicks a button).
2. The View triggers a command in the ViewModel.
3. The ViewModel:
   1. Updates its state.
   2. Communicates with the Model if necessary.&#x20;
4. The ViewModel notifies the View of changes via Data Binding.
5. The View automatically updates without direct calls to <mark style="color:$warning;">`UpdateView()`</mark> or similar methods.

***

<h2 align="center">Why Use MVVM in Unity?</h2>

#### As Unity projects grow, View logic often merges with business logic, leading to:

{% hint style="danger" %}
* Mixing of View and business logic in MonoBehaviours.
* "Spaghetti code" that is difficult to maintain and support.
* Challenges with modular testing.
{% endhint %}

***

#### MVVM addresses these issues by:

{% hint style="success" %}
* Improving code readability and scalability.
* Enabling modular testing of logic in the ViewModel.
* Simplifying logic reuse.
* Making the UI less fragile and easier to replace.
{% endhint %}

***

<h2 align="center">Learn More About MVVM</h2>

* [MVVM - Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm)
* [MVVM - Wikipedia](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel)

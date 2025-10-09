---
icon: circle-1
---

# MVP

To implement the task using the <mark style="color:$primary;">**MVP (Model-View-Presenter)**</mark> approach, we need two Views: one to display the text and another to handle text input. Each View requires its own Presenter.

<h2 align="center">1. Initial Version</h2>

### 1.1. Create a Scene

Create a new scene and add a [Canvas](https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/UICanvas.html), to it. On the Canvas , add an empty GameObject named "Out View MVP" and attach a [TextMeshProUGUI](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/TMPObjectUIText.html) component to it.

<figure><img src="../../.gitbook/assets/image (7).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (6).png" alt=""><figcaption></figcaption></figure>

Add an object named "Input View MVP" to the Canvas.

<figure><img src="../../.gitbook/assets/image (15).png" alt=""><figcaption></figcaption></figure>

Inside "Input View MVP," add two objects: an [InputField](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/api/TMPro.TMP_InputField.html) and a [Button](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/script-Button.html).

<figure><img src="../../.gitbook/assets/image (11).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (12).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (13).png" alt=""><figcaption></figcaption></figure>

### 1.2. Create OutViewMVP

Let's create a View that will display text on the screen.

```csharp
using TMPro;
using UnityEngine;

public class OutViewMVP : MonoBehaviour
{
    // The component where we will place the text.
    [SerializeField] private TMP_Text _text;
    
    // Public property for setting text in a component from outside.
    public string Text
    {
        get => _text.text;
        set => _text.text;
    }
}
```

### 1.3. Configure OutViewMVP

Add the <mark style="color:$warning;">`OutViewMVP`</mark> component to the "Out View MVP" object.

<figure><img src="../../.gitbook/assets/image (9).png" alt=""><figcaption></figcaption></figure>

### 1.4. Create InputViewMVP

Create a View to handle the processing of entered text:&#x20;

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputViewMVP : MonoBehaviour
{
    // Event to notify when the entered text should be read.
    public event UnityAction Clicked
    {
        add => _sayButton.onClick.AddListener(value);
        remove => _sayButton.onClick.RemoveListener(value);       
    }

    // Button that signals to read the entered text when clicked.
    [SerializeField] private Button _sayButton;
        
    // Component to read the entered text from.
    [SerializeField] private TMP_InputField _inputField;

    // Public property to read or set the entered text.
    public string Text
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }
}
```

### 1.5. Configure InputViewMVP

Add the <mark style="color:$warning;">`InputViewMVP`</mark> component to the "Input View MVP" GameObject in the scene.

<figure><img src="../../.gitbook/assets/image (19).png" alt=""><figcaption></figcaption></figure>

### 1.6. Create OutPresenterMVP

Create the <mark style="color:$warning;">`OutPresenterMVP`</mark> for <mark style="color:$warning;">`OutViewMVP`</mark>:

```csharp
using System;

public sealed class OutPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly OutViewMVP _view;

    public OutPresenterMVP(Speaker model, OutViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = _model.Text;

        Subscribe();
    }

    private void Subscribe() =>
        _model.TextChanged += OnTextChanged;

    private void Unsubscribe() =>
        _model.TextChanged -= OnTextChanged;
        
    private void OnTextChanged(string value) =>
        _view.Text = value;

    public void Dispose() =>
        Unsubscribe();
}
```

### 1.7. Создайте InputPresenterMVP

Create the <mark style="color:$warning;">`InputPresenterMVP`</mark> for <mark style="color:$warning;">`InputViewMVP`</mark>:

```csharp
using System;

public sealed class InputPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly InputViewMVP _view;

    public InputPresenterMVP(Speaker model, InputViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = model.Text;
            
        Subscribe();
    }

    private void Subscribe() =>
        _view.Clicked += OnClicked;
        
    private void Unsubscribe() =>
        _view.Clicked -= OnClicked;

    private void OnClicked() =>
        _model.Say(_view.Text);

    public void Dispose() =>
        Unsubscribe();
}
```

### 1.8. Create Bootstrap

Create a <mark style="color:$warning;">`Bootstrap`</mark> class to connect all Presenters and Views:

```csharp
using System;
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [Header("Out View")]
    [SerializeField] private OutViewMVP _outView;

    [Header("Input View")]
    [SerializeField] private InputViewMVP _inputView;

    private Speaker _speaker; 
    private OutPresenterMVP _outPresenter;
    private InputPresenterMVP _inputPresenter;

    private void Awake()
    {
        _speaker = new Speaker();
        _outPresenter = new OutPresenterMVP(_speaker, _outView);
        _inputPresenter = new InputPresenterMVP(_speaker, _inputView);
    }
        
    private void OnDestroy()
    {
        _outPresenter.Dispose();
        _inputPresenter.Dispose();
    }
}
```

### 1.9. Configure Bootstrap

Create an empty GameObject in the scene, add the <mark style="color:$warning;">`Bootstrap`</mark> component to it, and assign all references in the Inspector.

<figure><img src="../../.gitbook/assets/image (17).png" alt=""><figcaption></figcaption></figure>

### 1.10. Run and Test

{% tabs %}
{% tab title="Result" %}
<figure><img src="../../.gitbook/assets/image (18).png" alt=""><figcaption></figcaption></figure>
{% endtab %}

{% tab title="C#" %}
## Model

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

## OutViewMVP

```csharp
using TMPro;
using UnityEngine;

public class OutViewMVP : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public string Text
    {
        get => _text.text;
        set => _text.text;
    }
}
```

## InputViewMVP

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputViewMVP : MonoBehaviour
{
    public event UnityAction Clicked
    {
        add => _sayButton.onClick.AddListener(value);
        remove => _sayButton.onClick.RemoveListener(value);       
    }

    [SerializeField] private Button _sayButton;
    [SerializeField] private TMP_InputField _inputField;

    public string Text
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }
}
```

## OutPresenterMVP

```csharp
using System;

public sealed class OutPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly OutViewMVP _view;

    public OutPresenterMVP(Speaker model, OutViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = _model.Text;

        Subscribe();
    }

    private void Subscribe() =>
        _model.TextChanged += OnTextChanged;

    private void Unsubscribe() =>
        _model.TextChanged -= OnTextChanged;
        
    private void OnTextChanged(string value) =>
        _view.Text = value;

    public void Dispose() =>
        Unsubscribe();
}
```

## InputPresenterMVP

```csharp
using System;

public sealed class InputPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly InputViewMVP _view;

    public InputPresenterMVP(Speaker model, InputViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = model.Text;
            
        Subscribe();
    }

    private void Subscribe() =>
        _view.Clicked += OnClicked;
        
    private void Unsubscribe() =>
        _view.Clicked -= OnClicked;

    private void OnClicked() =>
        _model.Say(_view.Text);

    public void Dispose() =>
        Unsubscribe();
}
```

## Bootstrap

```csharp
using System;
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [Header("Out View")]
    [SerializeField] private OutViewMVP _outView;

    [Header("Input View")]
    [SerializeField] private InputViewMVP _inputView;

    private Speaker _speaker; 
    private OutPresenterMVP _outPresenter;
    private InputPresenterMVP _inputPresenter;

    private void Awake()
    {
        _speaker = new Speaker();
        _outPresenter = new OutPresenterMVP(_speaker, _outView);
        _inputPresenter = new InputPresenterMVP(_speaker, _inputView);
    }
        
    private void OnDestroy()
    {
        _outPresenter?.Dispose();
        _inputPresenter?.Dispose();
    }
}
```
{% endtab %}

{% tab title="Editor" %}
<figure><img src="../../.gitbook/assets/image (9).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (19).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (17).png" alt=""><figcaption></figcaption></figure>
{% endtab %}
{% endtabs %}

<h2 align="center">2. New Requirements</h2>

The task was completed, but the requirements have changed slightly. Now, the text must be displayed in multiple text elements instead of a single one. This requires modifying only the <mark style="color:$warning;">`OutViewMVP`</mark>.

### 2.1. Modify OutViewMVP

```csharp
using TMPro;
using System.Linq;
using UnityEngine;

public class OutViewMVP : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;

    public string Text
    {
        get => _texts.FirstOrDefault()?.text ?? string.Empty;
        set
        {
            foreach (var text in _texts)
                text.text = value;
        }
    }
}
```

### 2.2. Reconfigure OutViewMVP

Add additional text elements to the "Out View MVP" GameObject and arrange them in the scene.

<figure><img src="../../.gitbook/assets/image (2).png" alt=""><figcaption></figcaption></figure>

### 2.3. Additional Requirements&#x20;

The implementation was easily adapted to display text in multiple elements by using an array. However, a new requirement states that the text must now be updated instantly as it changes in the <mark style="color:$warning;">`InputField`</mark>.

### 2.4. Create MomentInputViewMVP

Create a View to handle instant text updates from the <mark style="color:$warning;">`InputField`</mark>:

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MomentInputViewMVP : MonoBehaviour
{
    public event UnityAction<string> TextChanged
    {
        add => _inputField.onValueChanged.AddListener(value);
        remove => _inputField.onValueChanged.RemoveListener(value);
    }
        
    [SerializeField] private TMP_InputField _inputField;
        
    public string Text
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }
}
```

### 2.5. Configure MomentInputViewMVP

Remove the "Say" Button from the "Input View MVP" GameObject, then replace the <mark style="color:$warning;">`InputViewMVP`</mark> component with <mark style="color:$warning;">`MomentInputViewMVP`</mark>.

<figure><img src="../../.gitbook/assets/image (3).png" alt=""><figcaption></figcaption></figure>

### 2.6. Create MomentInputPresenterMVP

Create the <mark style="color:$warning;">`MomentInputPresenterMVP`</mark> for <mark style="color:$warning;">`MomentInputViewMVP`</mark>:

```csharp
using System;

public sealed class MomentInputPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly MomentInputViewMVP _view;
        
    public MomentInputPresenterMVP(Speaker model, MomentInputViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = model.Text;

        Subscribe();
    }

    private void Subscribe() =>
        _view.TextChanged += OnTextChanged;

    private void Unsubscribe() =>
        _view.TextChanged -= OnTextChanged;
        
    private void OnTextChanged(string value) =>
        _model.Say(value);

    public void Dispose() =>
        Unsubscribe();
}
```

### 2.7. Modify Bootstrap

The new requirements specify that the input View should update instantly from the <mark style="color:$warning;">`InputField`</mark> instead of requiring a button press:

```csharp
using System;
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [Header("Out View")]
    [SerializeField] private OutViewMVP _outView;

    [Header("Input View")]
    [SerializeField] private MomentInputViewMVP _inputView;

    private Speaker _speaker;
        
    private OutPresenterMVP _outPresenter;
    private MomentInputPresenterMVP _inputPresenter;

    private void Awake()
    {
        _speaker = new Speaker();
        _outPresenter = new OutPresenterMVP(_speaker, _outView);
        _inputPresenter = new MomentInputPresenterMVP(_speaker, _inputView);
    }
    
    private void OnDestroy()
    {
        _outPresenter.Dispose();
        _inputPresenter.Dispose();
    }
}
```

### 2.8. Reconfigure Bootstrap

Assign the new <mark style="color:$warning;">`MomentInputViewMVP`</mark> component to the corresponding field in the <mark style="color:$warning;">`Bootstrap`</mark> component in the Inspector.

<figure><img src="../../.gitbook/assets/image (4).png" alt=""><figcaption></figcaption></figure>

### 2.9. Run and Test

{% tabs %}
{% tab title="Result" %}
<figure><img src="../../.gitbook/assets/image (5).png" alt=""><figcaption></figcaption></figure>
{% endtab %}

{% tab title="C#" %}
## Model

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

## OutViewMVP

```csharp
using TMPro;
using System.Linq;
using UnityEngine;

public class OutViewMVP : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;

    public string Text
    {
        get => _texts.FirstOrDefault()?.text ?? string.Empty;
        set
        {
            foreach (var text in _texts)
                text.text = value;
        }
    }
}
```

## MomentInputViewMVP

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MomentInputViewMVP : MonoBehaviour
{
    public event UnityAction<string> TextChanged
    {
        add => _inputField.onValueChanged.AddListener(value);
        remove => _inputField.onValueChanged.RemoveListener(value);
    }
        
    [SerializeField] private TMP_InputField _inputField;
        
    public string Text
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }
}
```

## OutViewPresenterMVP

```csharp
using System;

public sealed class OutPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly OutViewMVP _view;

    public OutPresenterMVP(Speaker model, OutViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = _model.Text;

        Subscribe();
    }

    private void Subscribe() =>
        _model.TextChanged += OnTextChanged;

    private void Unsubscribe() =>
        _model.TextChanged -= OnTextChanged;
        
    private void OnTextChanged(string value) =>
        _view.Text = value;

    public void Dispose() =>
        Unsubscribe();
}
```

## MomentInputPresenterMVP

```csharp
using System;

public sealed class MomentInputPresenterMVP : IDisposable
{
    private readonly Speaker _model;
    private readonly MomentInputViewMVP _view;
        
    public MomentInputPresenterMVP(Speaker model, MomentInputViewMVP view)
    {
        _view = view;
        _model = model;
        _view.Text = model.Text;

        Subscribe();
    }

    private void Subscribe() =>
        _view.TextChanged += OnTextChanged;

    private void Unsubscribe() =>
        _view.TextChanged -= OnTextChanged;
        
    private void OnTextChanged(string value) =>
        _model.Say(value);

    public void Dispose() =>
        Unsubscribe();
}
```

## Bootstrap

```csharp
using System;
using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    [Header("Out View")]
    [SerializeField] private OutViewMVP _outView;

    [Header("Input View")]
    [SerializeField] private MomentInputViewMVP _inputView;

    private Speaker _speaker;
        
    private OutPresenterMVP _outPresenter;
    private MomentInputPresenterMVP _inputPresenter;

    private void Awake()
    {
        _speaker = new Speaker();
        _outPresenter = new OutPresenterMVP(_speaker, _outView);
        _inputPresenter = new MomentInputPresenterMVP(_speaker, _inputView);
    }
    
    private void OnDestroy()
    {
        _outPresenter.Dispose();
        _inputPresenter.Dispose();
    }
}
```
{% endtab %}

{% tab title="Editor" %}
<figure><img src="../../.gitbook/assets/image (2).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (3).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/image (4).png" alt=""><figcaption></figcaption></figure>
{% endtab %}
{% endtabs %}

<h2 align="center">3. Further Requirements</h2>

Now, the system must support both instant input and button-based input, as well as potentially handling input submission via the <mark style="color:$warning;">`InputField`</mark>'s Submit action. This requires modifying the Views and Presenters. You can view this implementation (excluding <mark style="color:$warning;">`InputField`</mark> Submit support) in the project, if you imported the samples, at the path: <mark style="color:$warning;">`Assets/Samples/Aspid/MVVM/HelloWorld/MVP`</mark>.

## Summary

* <mark style="color:$primary;">**Initial Requirements**</mark>: We created two Views and one Presenter for each View.
* <mark style="color:$primary;">**First Changes**</mark>: We modified only the text display View to support multiple text elements.
* <mark style="color:$primary;">**Instant Input Requirement**</mark>: We created a new View and Presenter for instant text input, created a new prefab, and modified the <mark style="color:$warning;">`Bootstrap`</mark>.
* <mark style="color:$primary;">**Final Requirements**</mark>: Fully supporting both input methods and <mark style="color:$warning;">`InputField`</mark> Submit requires significant modifications to <mark style="color:$warning;">`Bootstrap`</mark> and updates to <mark style="color:$warning;">`InputViewMVP`</mark> to handle the Submit action.

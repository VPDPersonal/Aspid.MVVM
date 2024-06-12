using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public partial class ModelView : ModelViewBase, IDisposable
{
    [Header("Default data")]
    [SerializeField] [Bind] private int _age;
    [SerializeField] [Bind] private string _name;
    
    [Header("Buttons")]
    [SerializeField] private Button[] _sendButtons;
    [SerializeField] private Button[] _closeButtons;
    
    public ModelView()
    {
        Bind();
        SubscribeUnityHandlers();
    }
    
    public void Dispose()
    {
        Unbind();
        UnsubscribeUnityHandlers();
    }
}

[Serializable]
public partial class ModelView
{
    private event Action<int> AgeChanged;
    private event Action<string> NameChanged;
    
    [Header("Binders")]
    [SerializeField] private Binder[] _ageBinders;
    [SerializeField] private Binder[] _nameBinders;
    
    private int Age
    {
        get => _age;
        set => SetValue(ref _age,value, ref AgeChanged);
    }
    
    private string Name
    {
        get => _name;
        set => SetValue(ref _name,value, ref NameChanged);
    }
    
    private void Bind()
    {
        Bind(ref AgeChanged, _age, _ageBinders);
        Bind(ref NameChanged, _name, _nameBinders);
    }
    
    public void Unbind()
    {
        Unbind(ref AgeChanged, _ageBinders);
        Unbind(ref NameChanged, _nameBinders);
    }
}

public partial class ModelView
{
    public void SubscribeUnityHandlers() { }
    
    public void UnsubscribeUnityHandlers() { }
}

public abstract class ModelViewBase
{
    protected static void Bind<T>(ref Action<T> changed, T value, Binder[] binders)
    {
        for (var i = 0; i < binders.Length; i++)
        {
            if (binders[i] is IBinder<T> binder)
            {
                changed += binder.SetValue;
                binder.SetValue(value);
            }
        }
    }
    
    protected static void Unbind<T>(ref Action<T> changed, Binder[] binders)
    {
        for (var i = 0; i < binders.Length; i++)
        {
            if (binders[i] is IBinder<T> binder)
                changed -= binder.SetValue;
        }
    }
    
    protected static void SetValue<T>(ref T value, T newValue, ref Action<T> changed)
    {
        if (value.Equals(newValue)) return;
        
        value = newValue;
        changed?.Invoke(value);
    }
}

public class View : MonoBehaviour
{
    [SerializeReference] private ModelView _modelView;
}

public sealed class TextBinder : Binder, IBinder<string>, IBinderNumber
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public void SetValue(string value) =>
        _text.text = value;
    
    public void SetValue(int value) =>
        SetValue(value.ToString());
    
    public void SetValue(long value) =>
        SetValue(value.ToString());
    
    public void SetValue(float value) =>
        SetValue(value.ToString(CultureInfo.InvariantCulture));
    
    public void SetValue(double value) =>
        SetValue(value.ToString(CultureInfo.InvariantCulture));
}

public interface IBinderNumber : IBinder<int>, IBinder<long>, IBinder<float>, IBinder<double> { }

public interface ICaster<out TType>
{
    public bool CanCast<T>() => true;
    
    public TType Cast<T>(T value);
}

public abstract class Binder : MonoBehaviour { }

public interface IBinder<in T>
{
    public void SetValue(T value);
}

[AttributeUsage(AttributeTargets.Field)]
public sealed class BindAttribute : Attribute { }

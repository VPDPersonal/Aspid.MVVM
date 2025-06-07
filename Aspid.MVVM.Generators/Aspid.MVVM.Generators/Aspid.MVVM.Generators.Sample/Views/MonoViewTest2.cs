using Aspid.MVVM.Generators.Sample.Binders;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.Generators.Sample.Views;

public static class Info
{
    public static IBinder<string> Field;
}

[View]
public partial class MonoViewTest2 : MonoView
{
    private IBinder<string> _text;
    
    private int age;
    
    private IBinder<int> Age => age switch
    {
        1 => null,
        2 => null,
        3 => null,
        _ => null
    };
    
    private IReverseBinder<string> InputCommand2 =>
            GetComponent<IReverseBinder<string>>();
    
    private IBinder<string>? InputCommand3 => null;
    
    private IBinder<string>? Text1
    {
        get => _text;
        set => _text = value;
    }
    
    private IBinder<string>? Text2
    {
        get
        {
            if (0 > 0) return Text1;
            return null;
        }
        set => _text = value;
    }
    
    private IReverseBinder<string> InputCommand =>
        GetComponent<IReverseBinder<string>>();
    
    private IBinder<IRelayCommand<int>>[] NumberCommand =>
        GetComponentsInChildren<IBinder<IRelayCommand<int>>>();
    
    private ButtonMonoBinder OkCommand =>
        GetComponent<ButtonMonoBinder>();
}
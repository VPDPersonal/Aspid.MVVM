namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex3BindViewModel
    {
        // Сгенерирует OneWay свойство
        [Bind(BindMode.OneWay)] private int _oneWayBind;
        
        // Сгенерирует TwoWay свойство
        [Bind] private int _twoWayBind1;
        [Bind(BindMode.TwoWay)] private int _twoWayBind2;
        
        // Сгенерирует OneTime свойство
        [Bind(BindMode.OneTime)] private int _oneTimeBind1;
        [Bind] private readonly int _oneTimeBind2;
        
        // Сгенерирует OneWayToSource свойство
        [Bind(BindMode.OneWayToSource)] private int _oneWayToSourceBind;
    }
    
    // public partial class Ex3BindViewModel
    // {
    //     public event Action<int> OneWayBindChanged;
    //     public event Action<int> TwoWayBind1Changed;
    //     public event Action<int> TwoWayBind2Changed;
    //     
    //     private int OneWayBind
    //     {
    //         get => _oneWayBind;
    //         set => SetOneWayBind(value);
    //     }
    //     
    //     private int TwoWayBind1
    //     {
    //         get => _twoWayBind1;
    //         set => SetTwoWayBind1(value);
    //     }
    //     
    //     private int TwoWayBind2
    //     {
    //         get => _twoWayBind2;
    //         set => SetTwoWayBind2(value);
    //     }
    //     
    //     private int OneTimeBind1 => _oneTimeBind1;
    //     
    //     private int OneTimeBind2 => _oneTimeBind2;
    //     
    //     private int OneWayToSourceBind
    //     {
    //         get => _oneWayToSourceBind;
    //         set => SetOneWayToSourceBind(value);
    //     }
    //     
    //     private void SetOneWayBind(int value)
    //     {
    //         if (EqualityComparer<int>.Default.Equals(_oneWayBind, value)) return;
    //         _oneWayBind = value;
    //          OneWayBindChanged?.Invoke(_oneWayBind);
    //     }
    //     
    //     private void SetTwoWayBind1(int value)
    //     {
    //         if (EqualityComparer<int>.Default.Equals(_twoWayBind1, value)) return;
    //         _twoWayBind1 = value;
    //         TwoWayBind1Changed?.Invoke(_twoWayBind1);
    //     }
    //     
    //     private void SetTwoWayBind2(int value)
    //     {
    //         if (EqualityComparer<int>.Default.Equals(_twoWayBind2, value)) return;
    //         _twoWayBind2 = value;
    //         TwoWayBind2Changed?.Invoke(_twoWayBind2);
    //     }
    //     
    //     private void SetOneWayToSourceBind(int value)
    //     {
    //         if (EqualityComparer<int>.Default.Equals(_oneWayToSourceBind, value)) return;
    //         _oneWayToSourceBind = value;
    //     }
    // }
}
namespace Aspid.MVVM.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex3BindViewModel
    {
        [OneWayBind] private int _oneWayValue;
        [TwoWayBind] private int _twoWayValue;
        [OneTimeBind] private int _oneTimeValue1;
        [OneTimeBind] private readonly int _oneTimeValue2;
        [OneWayToSourceBind] private int _oneWayToSourceValue;
    }
}

//  Generated Code:
//  public partial class Ex3BindViewModel
//  {
//  	public event Action<int> OneWayValueChanged;
//  	public event Action<int> TwoWayValueChanged;
//                   
//  	private int OneWayValue
//  	{
//  	    get => _oneWayValue;
//  	    set => SetOneWayValue(value);
//  	}
//                    
//  	private int TwoWayValue
//  	{
//  	    get => _twoWayValue;
//  	    set => SetTwoWayValue(value);
//  	}
//                    
//  	private int OneTimeValue1 => _oneTimeValue1;
//                    
//  	private int OneTimeValue2 => _oneTimeValue2;
//                    
//  	private int OneWayToSourceValue
//  	{
//  	    get => _oneWayToSourceValue;
//  	    set => SetOneWayToSourceValue(value);
//  	}
//                    
//  	private void SetOneWayValue(int value)
//  	{
//  	    if (EqualityComparer<int>.Default.Equals(_oneWayValue, value)) return;
//                	    
//  	    OnOneWayValueChanging(_oneWayValue, value);
//  	    _oneWayValue = value;
//  	    OnOneWayValueChanged(value);
//  		OneWayValueChanged?.Invoke(_oneWayValue);
//  	}
//                    
//  	partial void OnOneWayValueChanging(int oldValue, int newValue);
//                	
//  	partial void OnOneWayValueChanged(int newValue);
//                    
//  	private void SetTwoWayValue(int value)
//  	{
//  	    if (EqualityComparer<int>.Default.Equals(_twoWayValue, value)) return;
//                	    
//  	    OnTwoWayValueChanging(_twoWayValue, value);
//  	    _twoWayValue = value;
//  	    OnTwoWayValueChanged(value);
//  		TwoWayValueChanged?.Invoke(_twoWayValue);
//  	}
//                    
//  	partial void OnTwoWayValueChanging(int oldValue, int newValue);
//                	
//  	partial void OnTwoWayValueChanged(int newValue);
//                    
//  	private void SetOneWayToSourceValue(int value)
//  	{
//  	    if (EqualityComparer<int>.Default.Equals(_oneWayToSourceValue, value)) return;
//                	    
//  	    OnOneWayToSourceValueChanging(_oneWayToSourceValue, value);
//  	    _oneWayToSourceValue = value;
//  	    OnOneWayToSourceValueChanged(value);
//  	}
//                    
//  	partial void OnOneWayToSourceValueChanging(int oldValue, int newValue);
//                	
//  	partial void OnOneWayToSourceValueChanged(int newValue);
//  }
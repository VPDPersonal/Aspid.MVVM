namespace Aspid.MVVM.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex4BindAlsoViewModel
    {
        [BindAlso(nameof(Nickname))]
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _family;

        private string Nickname => _name.ToLower();
        
        private string FullName => $"{Name} {Family}";
    }
}

//  Generated Code:
//  public partial class Ex4BindAlsoViewModel
//  {
//  	public event Action<string> NameChanged;
//  	public event Action<string> FamilyChanged;
//  	public event Action<string> NicknameChanged;
//  	public event global::System.Action<string> FullNameChanged;

//  	private string Name
//  	{
//  	    get => _name;
//  	    set => SetName(value);
//  	}
//      
//  	private string Family
//  	{
//  	    get => _family;
//  	    set => SetFamily(value);
//  	}
//      
//  	private void SetName(string value)
//  	{
//  	    if (EqualityComparer<string>.Default.Equals(_name, value)) return;
//  	    
//  	    OnNameChanging(_name, value);
//  	    _name = value;
//  	    OnNameChanged(value);
//  		NameChanged?.Invoke(_name);
//  		NicknameChanged?.Invoke(Nickname);
//  		FullNameChanged?.Invoke(FullName);
//  	}
//      
//  	partial void OnNameChanging(string oldValue, string newValue);
//  	
//  	partial void OnNameChanged(string newValue);
//      
//  	private void SetFamily(string value)
//  	{
//  	    if (EqualityComparer<string>.Default.Equals(_family, value)) return;
//  	    
//  	    OnFamilyChanging(_family, value);
//  	    _family = value;
//  	    OnFamilyChanged(value);
//  		FamilyChanged?.Invoke(_family);
//  		FullNameChanged?.Invoke(FullName);
//  	}
//      
//  	partial void OnFamilyChanging(string oldValue, string newValue);
//  	
//  	partial void OnFamilyChanged(string newValue);
//  }
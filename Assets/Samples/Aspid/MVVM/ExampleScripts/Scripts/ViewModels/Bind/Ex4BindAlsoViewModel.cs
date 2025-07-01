namespace Aspid.MVVM.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class Ex4BindAlsoViewModel
    {
        [BindAlso(nameof(Nickname))]
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _family;

        private string Nickname => Name.ToLower();
        
        private string FullName => $"{Name} {Family}";
    }
}
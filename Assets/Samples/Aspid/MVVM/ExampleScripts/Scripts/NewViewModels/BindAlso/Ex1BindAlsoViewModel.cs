namespace Aspid.MVVM.ExampleScripts.NewViewModels.BindAlso
{
    [ViewModel]
    public partial class Ex1BindAlsoViewModel
    {
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _family;

        private string FullName => $"{Name} {Family}";
    }
}
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind
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
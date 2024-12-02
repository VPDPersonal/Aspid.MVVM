using Aspid.MVVM.ViewModels.Generation;

namespace Aspid.MVVM.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class WithBindAlsoViewModel
    {
        [BindAlso(nameof(NickName))]
        [BindAlso(nameof(FullName))]
        [Bind] private string _firstName;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _lastName;
        
        [BindAlso(nameof(NickName))]
        [Bind] private int _age;
        
        public string FullName => FirstName + " " + LastName;

        public string NickName => FirstName + "_" + Age;
    }
}
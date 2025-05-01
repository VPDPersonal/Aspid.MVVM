namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex2BindViewModel
    {
        [Bind] private readonly int _age;

        public Ex2BindViewModel(int age)
        {
            _age = age;
        }
    }
    
    // Пример сгенерированного кода:
    // public partial class Ex2BindViewModel
    // {
    //     private int Age => _age;
    // }
}
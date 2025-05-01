namespace Aspid.MVVM.ExampleScripts.NewViewModels.Bind
{
    [ViewModel]
    public partial class Ex1BindViewModel
    {
        [Bind] private int _age;

        public Ex1BindViewModel(int age)
        {
            // В конструкторе рекомендуется инициализировать через поле,
            // а не через сгенерированое свойство
            _age = age;
        }

        private void UpdateAge(int age)
        {
            // Для корректной работы связывания необходимо использовать
            // сгенерированное свойство, в противном случаи анализатор выдаст ошибку.
            Age = age;
        }
    }
    
    // Пример сгенерированного кода:
    // public partial class Ex1BindViewModel
    // {
    //     public event Action<int> AgeChanged;
    //
    //     private int Age
    //     {
    //         get => _age;
    //         set => SetAge(value);
    //     }
    //
    //     private void SetAge(int value)
    //     {
    //         if (EqualityComparer<int>.Default.Equals(_age, value)) return;
    //         
    //         _age = value;
    //         AgeChanged?.Invoke(_age);
    //     }
    // }
}
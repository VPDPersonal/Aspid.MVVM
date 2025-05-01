using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.HelloWorld.Views
{
    // ViewAttribute - маркер для Source Generator.
    // Для правильно работы Source Generator, класс должен быть partial.
    // Source generator реализует абстрактные методы инициализации и деинициализации,
    // где инициализирует все перечисленные IBinder во View.
    [View]
    public partial class InputSpeakerView : MonoView
    {
        // RequireBinderAttribute - необязательный атрибут.
        // Он нужен только для фильтрации биндеров, которые реализуют IMonoBinderValidable.
        // В этом случаи, только биндеры, которые реализуют IBinder<string> могут быть установлены в это поле.
        // Биндер - это компонент, который связывает компонент с данными из ViewModel.
        // Стоит обратить внимание, что поле должно называться точно так же, как и поле во ViewModel,
        // и хоть это можно переопределить рекомендуется называть точно так же.
        // Поля: m_inputText, _inputText и inputText являются эквивалентными.
        // MonoBinder - базовый класс для всех биндеров, которые должны наследоваться от MonoBehaviour.
        // _inputText может принять только один биндер. Данный подход удобен, когда мы точно знаем, что будет однин биндер.
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _inputText;
        
        // _sayCommand объявлен как массив - это удобно для того, чтобы мы могли указать бесчисленное количество биндеров.
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _sayCommand;
    }
}
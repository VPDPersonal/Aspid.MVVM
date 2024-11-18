using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

namespace Aspid.UI.HelloWorld.Views
{
    // TODO Aspid.UI Translate
    // Аттрибут View служит меткой для Source Generator.
    // При этом класс обязательно должен быть частичным.
    // Source Generator реализует абстрактные методы MonoView и анализирует другие атрибуты в классе.
    [View]
    public partial class SpeakerView : MonoView
    {
        // TODO Aspid.UI Translate
        // RequireBinder - не обязательный атрибут.
        // Он служит только для фильтрации биндеров реализующих IMonoBinderValidable
        // В данном случаи в поле _text можно будет установить только те биндеры, которые реализуют IBinder<string>
        [RequireBinder(typeof(string))]
        // TODO Aspid.UI Translate
        // Binder - компонент, который связывает визуальный компонент с данными из ViewModel.
        // Обратите внимание, что поле должно называться точно так же как и в ViewModel.
        // Но стоит так же учесть, что названия _text, m_text, text эквивалентны.
        // Массив указывает на то, что к данному значению можно подключить несколько биндеров.
        [SerializeField] private MonoBinder[] _text;
        
        [RequireBinder(typeof(string))]
        // TODO Aspid.UI Translate
        // Тут мы указываем, что мы хотим указать только один биндер для связывания.
        // Учтите, что указывать биндер не обязательно. Вы не получите никакого лога о том, что биндер не установлен.
        [SerializeField] private MonoBinder _inputText;

        // TODO Aspid.UI Translate
        // Биндер не обязательно должен наследоваться от MonoBinder или же от MonoBehaviour. 
        // Самое главное условие для Source Generator - реализация интерфейса IBinder
        [SerializeField] private ButtonCommandBinder _sayCommand;
    }
}
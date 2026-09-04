// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind
{
    // [Bind] on a property instead of a field: the setter must call the generated On{Name}PropertyChanged().
    [ViewModel]
    public partial class Ex5BindPropertyViewModel
    {
        private string _text;

        [Bind]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;

                _text = value;
                OnTextPropertyChanged();
            }
        }

        // A get-only property is OneTime unless another member re-sends it with [BindAlso].
        [Bind]
        public int Length => Text?.Length ?? 0;
    }
}

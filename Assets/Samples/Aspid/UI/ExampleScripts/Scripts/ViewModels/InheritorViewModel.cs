using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Samples.Aspid.UI.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class InheritorViewModel : ViewModel
    {
        [Bind] private int _number;
        [Bind] private string _text;

        [Bind] private int[] _numbers;
        [Bind] private string[] _texts;

        // До изменения _number
        partial void OnNumberChanging(int oldValue, int newValue) { }

        // После изменения _number
        partial void OnNumberChanged(int newValue) { }

        // До изменения _text
        partial void OnTextChanging(string oldValue, string newValue) { }

        // После изменения _text
        partial void OnTextChanged(string newValue) { } 

        // До изменения _numbers
        partial void OnNumbersChanging(int[] oldValue, int[] newValue) { }

        // После изменения _numbers
        partial void OnNumbersChanged(int[] newValue) { }

        // До изменения _texts
        partial void OnTextsChanging(string[] oldValue, string[] newValue) { }

        // После изменения _texts
        partial void OnTextsChanged(string[] newValue) { }
    }
}
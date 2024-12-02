using Aspid.MVVM.ViewModels.Generation;

namespace Aspid.MVVM.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class NoneViewModel
    {
        [Bind] private int _number;
        [Bind] private string _text;

        [Bind] private int[] _numbers;
        [Bind] private string[] _texts;

        // Before changing _number
        partial void OnNumberChanging(int oldValue, int newValue) { }

        // After changing _number
        partial void OnNumberChanged(int newValue) { }

        // Before changing _text
        partial void OnTextChanging(string oldValue, string newValue) { }

        // After changing _text
        partial void OnTextChanged(string newValue) { } 

        // Before changing _numbers
        partial void OnNumbersChanging(int[] oldValue, int[] newValue) { }

        // After changing _numbers
        partial void OnNumbersChanged(int[] newValue) { }

        // Before changing _texts
        partial void OnTextsChanging(string[] oldValue, string[] newValue) { }

        // After changing _texts
        partial void OnTextsChanged(string[] newValue) { }
    }
}
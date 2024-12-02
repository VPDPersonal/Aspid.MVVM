using System;

namespace Aspid.MVVM.TodoList.Models
{
    public class EditTodoPopUp
    {
        public string CurrentText;
        public readonly string SourceText;
        
        public event Action Canceled; 
        public event Action<string> Renamed;

        public EditTodoPopUp(string text)
        {
            SourceText = text;
        }

        public void Cancel()
        {
            Canceled?.Invoke();
        }

        public void Rename()
        {
            if (CurrentText == SourceText) return;
            Renamed?.Invoke(CurrentText);
        }
    }
}
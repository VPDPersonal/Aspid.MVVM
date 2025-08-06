using System;

namespace Aspid.MVVM.Samples.TodoList
{
    public sealed class Todo : IReadOnlyTodo
    {
        public event Action<string> TextChanged;
        public event Action<bool> IsCompletedChanged;
        
        private string _text;
        private bool _isCompleted;
        
        public string Id { get; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                TextChanged?.Invoke(value);
            }
        }
        
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                IsCompletedChanged?.Invoke(value);
            }
        }
        
        public Todo(string id)
        {
            Id = id;
        }
    }
}
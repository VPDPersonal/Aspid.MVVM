using System;
using System.Linq;
using Aspid.Collections.Observable;

namespace Aspid.MVVM.TodoList.Todos.Storages
{
    public sealed class TodoStorage
    {
        private readonly ObservableList<Todo> _todos = new();
        
        public int CountAddedTodo { get; private set; }

        public IReadOnlyObservableList<Todo> Todos => _todos;
        
        public Todo this[string id] => _todos.First(todo => todo.Id == id);

        public void AddTodo(string text)
        {
            var newTodo = new Todo(Guid.NewGuid().ToString())
            {
                Text = text,
                IsCompleted = false,
            };
            
            _todos.Add(newTodo);
            CountAddedTodo++;
        }
        
        public void RemoveTodo(string id)
        {
            for (var i = 0; i < _todos.Count; i++)
            {
                if (id != _todos[i].Id) continue;
                
                _todos.RemoveAt(i);
                return;
            }
            
            throw new ArgumentException($"Todo with {id} id not found");
        }
    }
}
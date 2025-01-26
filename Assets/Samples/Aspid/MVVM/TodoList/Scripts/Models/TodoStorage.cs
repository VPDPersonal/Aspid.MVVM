using System;
using System.Linq;
using System.Collections.Generic;

namespace Aspid.MVVM.TodoList.Models
{
    public class TodoStorage
    {
        public event Action<Todo> TodoAdded;
        public event Action<Todo> TodoRemoved;
        
        private readonly List<Todo> _todos = new();
        
        public int CountAddedTodo { get; private set; }

        public IReadOnlyCollection<Todo> Todos => _todos;
        
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
            
            TodoAdded?.Invoke(newTodo);
        }
        
        public void RemoveTodo(string id)
        {
            Todo todo = null;
            
            for (var i = 0; i < _todos.Count; i++)
            {
                if (id == _todos[i].Id)
                {
                    todo = _todos[i];
                    _todos.RemoveAt(i);
                    
                    break;
                }
            }

            if (todo is null) throw new NullReferenceException(nameof(todo));
            TodoRemoved?.Invoke(todo);
        }
    }
}
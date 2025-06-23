using System;
using System.Collections;
using System.Collections.Generic;
using Aspid.Collections.Observable;

namespace Aspid.MVVM.Samples.TodoList.Todos.Storages
{
    public sealed class TodoStorage : IEnumerable<Todo>
    {
        private readonly ObservableList<Todo> _todos = new();
        
        public IReadOnlyObservableList<Todo> Todos => _todos;
        
        public void Add(string text = "", bool isCompleted = false)
        {
            var id = Guid.NewGuid().ToString();
            var newTodo = new Todo(id)
            {
                Text = text,
                IsCompleted = isCompleted,
            };
            
            _todos.Add(newTodo);
        }

        public void Remove(Todo todo) =>
            Remove(todo.Id);
        
        public void Remove(string id)
        {
            for (var i = 0; i < _todos.Count; i++)
            {
                if (id != _todos[i].Id) continue;
                
                _todos.RemoveAt(i);
                return;
            }
            
            throw new ArgumentException($"Todo with {id} id not found");
        }
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<Todo> GetEnumerator() =>
            _todos.GetEnumerator();
    }
}
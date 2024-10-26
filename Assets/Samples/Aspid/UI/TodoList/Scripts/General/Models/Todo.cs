namespace Aspid.UI.TodoList.Models
{
    public class Todo : IReadOnlyTodo
    {
        public string Id { get; }
        
        public string Text { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public Todo(string id)
        {
            Id = id;
        }
    }
}
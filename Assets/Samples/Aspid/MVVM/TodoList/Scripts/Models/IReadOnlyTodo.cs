namespace Aspid.MVVM.TodoList.Models
{
    public interface IReadOnlyTodo
    {
        public string Id { get; }
        
        public string Text { get; }
        
        public bool IsCompleted { get; }
    }
}
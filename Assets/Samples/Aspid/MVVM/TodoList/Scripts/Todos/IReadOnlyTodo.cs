namespace Aspid.MVVM.Samples.TodoList.Todos
{
    public interface IReadOnlyTodo
    {
        public string Id { get; }
        
        public string Text { get; }
        
        public bool IsCompleted { get; }
    }
}
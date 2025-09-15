// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
{
    public interface IReadOnlyTodo
    {
        public string Id { get; }
        
        public string Text { get; }
        
        public bool IsCompleted { get; }
    }
}
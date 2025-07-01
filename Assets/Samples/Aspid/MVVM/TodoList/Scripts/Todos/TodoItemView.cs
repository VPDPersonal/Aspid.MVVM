using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.Samples.TodoList
{
    [View]
    public sealed partial class TodoItemView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _text;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isCompleted;
        
        [SerializeField] private ButtonCommandBinder[] _editCommand;
        [SerializeField] private ButtonCommandBinder[] _deleteCommand;
        
        private GameObjectVisibleBinder IsVisible => new(gameObject);
    }
}
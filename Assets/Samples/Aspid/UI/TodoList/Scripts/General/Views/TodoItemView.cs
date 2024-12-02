using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders;

namespace Aspid.UI.TodoList.Views
{
    [View]
    public partial class TodoItemView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _text;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isCompleted;
        
        [SerializeField] private ButtonCommandBinder[] _editCommand;
        [SerializeField] private ButtonCommandBinder[] _deleteCommand;

        // Binding of the object visibility is done through a property, so that you don't have to customize it in the inspector.
        // The property is called once - then the cached instance is used.
        private GameObjectVisibleBinder IsVisible => new(gameObject);
    }
}
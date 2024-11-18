using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;
using Aspid.UI.MVVM.StarterKit.Binders.GameObjects;

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

        // TODO Aspid.UI Translate
        // Привязка видимости объекта сделано, через свойство, что не настраивать его в инспекторе.
        // Свойство вызывается один раз - дальше используется кэшированный экземпляр.
        private GameObjectVisibleBinder IsVisible => new(gameObject);
    }
}
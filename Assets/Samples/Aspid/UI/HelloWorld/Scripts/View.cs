using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

namespace Aspid.UI.HelloWorld
{
    [View]
    public partial class View : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _text;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _inputText;

        [SerializeField] private ButtonCommandProvider _sayCommand;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders;

namespace Aspid.UI.Elements.Views
{
    [View]
    public partial class GenericDialogView : MonoView
    {
        [AsBinder(typeof(TextBinder))]
        [SerializeField] private TextMeshProUGUI _title;
        
        [AsBinder(typeof(TextBinder))]
        [SerializeField] private TextMeshProUGUI _message;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        [SerializeField] private Image[] _icons;

        [SerializeField] private MonoView _buttons;
    }
}
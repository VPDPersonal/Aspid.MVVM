using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Generation;
using Aspid.MVVM.StarterKit.Binders;

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
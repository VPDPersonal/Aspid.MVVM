using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Views.Generation;
using AspidUI.MVVM.StarterKit.Binders.Texts;
using AspidUI.MVVM.StarterKit.Binders.Images;

namespace Plugins.AspidUI.Elements.Runtime.Views
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
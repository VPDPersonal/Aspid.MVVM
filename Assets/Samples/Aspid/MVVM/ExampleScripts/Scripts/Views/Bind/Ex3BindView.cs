using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class Ex3BindView
    {
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image _image;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] _images;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertyImage { get; }
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertyImages { get; }
    }
}
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
// ReSharper disable UnassignedGetOnlyAutoProperty
namespace Aspid.MVVM.Samples.ExampleScripts.Views.Bind
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
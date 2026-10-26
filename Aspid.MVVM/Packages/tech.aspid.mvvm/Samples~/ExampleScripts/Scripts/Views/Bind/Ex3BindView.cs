using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.Views.Bind
{
    [View]
    public partial class Ex3BindView : MonoView
    {
        private static readonly IConverter<bool, bool> Invert = new BoolInvertConverter();

        // Generated: new ImageSpriteBinder(_image)
        [AsBinder(typeof(ImageSpriteBinder))]
        [SerializeField] private Image _image;

        // Generated: one ImageSpriteBinder per element of _images
        [AsBinder(typeof(ImageSpriteBinder))]
        [SerializeField] private Image[] _images;

        // Generated: new AnimatorSetFloatBinder(_speed, "Speed")
        [AsBinder(typeof(AnimatorSetFloatBinder), "Speed")]
        [SerializeField] private Animator _speed;

        // Generated: new AnimatorSetFloatBinder(_jumpHeight, "JumpHeight", null, BindMode.OneTime)
        [AsBinder(typeof(AnimatorSetFloatBinder), "JumpHeight", null, BindMode.OneTime)]
        [SerializeField] private Animator _jumpHeight;

        // nameof passes a member of the View, not a string. Generated: new GameObjectVisibleBinder(_isDead, Invert)
        [AsBinder(typeof(GameObjectVisibleBinder), nameof(Invert))]
        [SerializeField] private GameObject _isDead;

        // Generated: new ImageSpriteBinder(PropertyImage)
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertyImage => _image;
    }
}

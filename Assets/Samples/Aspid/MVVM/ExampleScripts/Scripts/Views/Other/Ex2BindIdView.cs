using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class Ex2BindIdView
    {
        // Generated ID: Id1
        [BindId("Id1")]
        private Binder _binder;
        
        // Generated ID: Id2
        [BindId("Id2")]
        private Binder[] _binders;

        // Generated ID: Id3
        [BindId("Id3")]
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image _asBinder;
        
        // Generated ID: Id4
        [BindId("Id4")]
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] _asBinders;
        
        // Generated ID: Id5
        [BindId("Id5")]
        private Binder Binder { get; }
        
        // Generated ID: Id6
        [BindId("Id6")]
        private Binder[] Binders { get; }
        
        // Generated ID: Id7
        [BindId("Id7")]
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image AsBinder { get; }
        
        // Generated ID: Id8
        [BindId("Id8")]
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] AsBinders { get; }
    }
}
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Generation;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Binders;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class InheritorMonoView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _singleBinder;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _arrayBinders;

        [AsBinder(typeof(ImageSpriteBinder))]
        [SerializeField] private Image _singeImage;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        [SerializeField] private Image[] _arrayImages;

        [SerializeField] private MonoView _childView;
        
        [SerializeField] private MonoView[] _childrenViews;

        private GameObjectVisibleBinder SingleVisibleBinder => new(gameObject);

        private GameObjectVisibleBinder[] ArrayVisibleBinders
        {
            get
            {
                var binders = new List<GameObjectVisibleBinder>();

                for (var i = 0; i < transform.childCount; i++)
                    binders.Add(new GameObjectVisibleBinder(transform.GetChild(i).gameObject));
                
                return binders.ToArray();
            }
        }

        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertySingleImage => GetComponent<Image>();
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] PropertyArrayImages => GetComponentsInChildren<Image>();
    }
}
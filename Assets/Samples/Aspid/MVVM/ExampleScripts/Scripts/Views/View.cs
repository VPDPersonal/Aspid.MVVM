using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Aspid.MVVM.ExampleScripts.NewViewModels;
using Aspid.MVVM.StarterKit.Unity;
using Zenject;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class View
    {
        private readonly MonoBinder _singleBinder; 
        private readonly MonoBinder[] _arrayBinders;

        [AsBinder(typeof(ImageSpriteBinder))]
        private readonly Image _singleImage;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private readonly Image[] _arrayImages;

        private readonly GameObject _gameObject;
        
        private readonly MonoView _childView;
        private readonly MonoView[] _childrenViews;

        private GameObjectVisibleBinder SingleVisibleBinder => new(_gameObject);

        private GameObjectVisibleBinder[] ArrayVisibleBinders
        {
            get
            {
                var binders = new List<GameObjectVisibleBinder>();

                for (var i = 0; i < _gameObject.transform.childCount; i++)
                    binders.Add(new GameObjectVisibleBinder(_gameObject.transform.GetChild(i).gameObject));
                
                return binders.ToArray();
            }
        }

        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertySingleImage => _gameObject.GetComponent<Image>();
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] PropertyArrayImages => _gameObject.GetComponentsInChildren<Image>();

        public View(
            MonoView childView, 
            GameObject gameObject, 
            MonoBinder singleBinder,
            MonoView[] childrenViews,
            MonoBinder[] arrayBinders,
            Image singleImage,
            Image[] arrayImages)
        {
            _singleBinder = singleBinder;
            _arrayBinders = arrayBinders;
            _singleImage = singleImage;
            _arrayImages = arrayImages;
            _gameObject = gameObject;
            _childView = childView;
            _childrenViews = childrenViews;
        }
    }
}
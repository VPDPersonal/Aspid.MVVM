using TMPro;
using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Views;
using System.Collections.Generic;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Texts;
using Aspid.UI.MVVM.StarterKit.Binders.GameObjects;

namespace Samples.Aspid.UI.ScriptExamples.Views
{
    [View]
    public partial class InheritorView : View
    {
        private readonly MonoBinder _singleBinder; 
        private readonly MonoBinder[] _arrayBinders;

        [AsBinder(typeof(TextBinder))]
        private readonly TextMeshProUGUI _singleText;
        
        [AsBinder(typeof(TextBinder))]
        private readonly TextMeshProUGUI[] _arrayTexts;

        private readonly GameObject _gameObject;

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

        [AsBinder(typeof(TextBinder))]
        private TextMeshProUGUI PropertySingleText => _gameObject.GetComponent<TextMeshProUGUI>();
        
        [AsBinder(typeof(TextBinder))]
        private TextMeshProUGUI[] PropertyArrayTexts => _gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        public InheritorView(
            MonoBinder singleBinder,
            MonoBinder[] arrayBinders,
            TextMeshProUGUI singleText,
            TextMeshProUGUI[] arrayTexts,
            GameObject gameObject)
        {
            _singleBinder = singleBinder;
            _arrayBinders = arrayBinders;
            _singleText = singleText;
            _arrayTexts = arrayTexts;
            _gameObject = gameObject;
        }
    }
}
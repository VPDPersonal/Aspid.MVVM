using TMPro;
using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using System.Collections.Generic;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Texts;
using Aspid.UI.MVVM.StarterKit.Binders.GameObjects;

namespace Samples.Aspid.UI.ScriptExamples.Views
{
    [View]
    public partial class InheritorMonoView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _singleBinder;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _arrayBinders;

        [AsBinder(typeof(TextBinder))]
        [SerializeField] private TextMeshProUGUI _singleText;
        
        [AsBinder(typeof(TextBinder))]
        [SerializeField] private TextMeshProUGUI[] _arrayTexts;

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

        [AsBinder(typeof(TextBinder))]
        private TextMeshProUGUI PropertySingleText => GetComponent<TextMeshProUGUI>();
        
        [AsBinder(typeof(TextBinder))]
        private TextMeshProUGUI[] PropertyArrayTexts => GetComponentsInChildren<TextMeshProUGUI>();
    }
}
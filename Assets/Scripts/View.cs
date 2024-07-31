using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Views;
using UnityEngine;

namespace DefaultNamespace
{
    public class TextBinder : Binder
    {
        public TextBinder(TextMeshProUGUI text)
        {
        }
    }
    
    public class LikeBinder : Attribute
    {
        public LikeBinder(Type type, params object[] parameters) { }
    }
    
    public class GetBinder : Attribute
    {
    }
    
    public partial class View : MonoBehaviour
    {
        [LikeBinder(typeof(TextBinder), "", 0, 0f, null)]
        [SerializeField] private TextMeshProUGUI _name;
        
        [LikeBinder(typeof(TextBinder))]
        [SerializeField] private TextMeshProUGUI[] _nameArray;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder _age;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _ageArray;

        [GetBinder]
        private TextBinder NameProperty =>
            new(gameObject.GetComponent<TextMeshProUGUI>());
        
        [GetBinder]
        private TextBinder[] NamePropertyArray => gameObject
            .GetComponentsInChildren<TextMeshProUGUI>()
            .Select(text => new TextBinder(text))
            .ToArray();
    }
    
    public partial class View : IView
    {
        private TextBinder _nameBinder;
        private TextBinder[] _nameArrayBinders;
        
        private TextBinder _gottenNamePropertyBinder;
        private TextBinder[] _gottenNamePropertyArrayBinders;
        
        public void Initialize(IViewModel viewModel)
        {
            CreateBinders();
            
            _nameBinder.Bind(viewModel, "");

            for (var i = 0; i < _nameArrayBinders.Length; i++)
                _nameArrayBinders[i].Bind(viewModel, "");
                
            _age.Bind(viewModel, "");
            
            for (var i = 0; i < _ageArray.Length; i++)
                _ageArray[i].Bind(viewModel, "");
            
            _gottenNamePropertyBinder.Bind(viewModel, "");
            
            for (var i = 0; i < _gottenNamePropertyArrayBinders.Length; i++)
                _gottenNamePropertyArrayBinders[i].Bind(viewModel, "");
        }

        private void CreateBinders()
        {
            _nameBinder ??= new TextBinder(_name);

            if (_nameArrayBinders == null && _nameArray is { Length: > 0 })
            {
                _nameArrayBinders = new TextBinder[_nameArray.Length];

                for (var i = 0; i < _nameArray.Length; i++)
                    _nameArrayBinders[i] = new TextBinder(_nameArray[i]);
            }

            _gottenNamePropertyBinder ??= NameProperty;
            _gottenNamePropertyArrayBinders ??= NamePropertyArray;
        }
    }
}
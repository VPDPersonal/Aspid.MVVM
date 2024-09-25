using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.StarterKit.Utilities
{
    [Serializable]
    public class SerializableInterface<T> 
        where T : class
    {
        [SerializeField] private Object _obj;
    
        private T _instance;
    
        public T Instance
        {
            get
            {
                if (_instance != null && (object)_instance == _obj) return _instance;
            
                if (_obj == null) Instance = null;
                else if (_obj is T inst) _instance = inst;
                else if (_obj is GameObject go && go.TryGetComponent(out inst)) _instance = inst;
                else Instance = null;
            
                return _instance;
            }
            set
            {
                _instance = value;
                _obj = _instance as Object;
            }
        }
    }
}
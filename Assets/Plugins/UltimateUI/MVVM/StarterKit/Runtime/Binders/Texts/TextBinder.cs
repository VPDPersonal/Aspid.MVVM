#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using System;
using UnityEngine;
using System.Globalization;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    public partial class TextBinder : TextBinderBase, IBinder<string>, IBinderNumber
    {
        [SerializeField] private string _format;

        protected string Format => _format;
    
        [BinderLog]
        public void SetValue(string value) =>
            CachedText.text = string.IsNullOrEmpty(_format) ? value : string.Format(_format, value);
        
        public void SetValue(int value) =>
            SetValue(value.ToString());
        
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
        
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }


#if UNITY_EDITOR
    public partial class TextBinder
    {
        [SerializeField] private bool _isDebug;
        
        // TODO Custom Property
        [SerializeField] private List<string> _log;
        
        void IBinder<string>.SetValue(string value)
        {
            if (_isDebug)
            {
                try
                {
                    SetValue(value);
                    AddLog($"Set Value: {value}");
                }
                catch (Exception e)
                {
                    AddLog($"<color=red>Exception: {e.Message}. Value: {value}</color>");
                    throw;
                }
            }
            else SetValue(value);
        }
        
        private void AddLog(string log)
        {
            _log ??= new List<string>();
            _log.Add(log);
        }
    }
#endif
}
#endif
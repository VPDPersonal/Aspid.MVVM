using System;
using UnityEngine;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Texts
{
    public partial class TextBinder : TextBinderBase, IBinder<string>, IBinderNumber
    {
        // TODO Add format
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedText.text = value;

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
        [SerializeField] private List<string> _log;
        
        void IBinder<string>.SetValue(string value)
        {
            try
            {
                SetValue(value);
                AddLog($"Set Value: {value}");
            }
            catch (Exception e)
            {
                AddLog($"<color=red>Exception: {e.Message}. Value: {value}</color>");
                Debug.LogError($"Exception: {e.Message}. Value: {value}");
            }
        }
        
        private void AddLog(string log)
        {
            _log ??= new List<string>();
            _log.Add(log);
        }
    }
#endif
    
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public class BinderLogAttribute : Attribute { }
}
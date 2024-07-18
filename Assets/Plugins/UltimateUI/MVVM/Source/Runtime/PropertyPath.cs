using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    [Serializable]
    public sealed class PropertyPath
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _calculateMarker = new("PropertyPath.Calculate");
#endif
        
        [SerializeField] private string _fullPath;
        
        private string? _name;
        private string? _path;

        public string Name
        {
            get
            {
                if (_name == null)
                    Calculate(FullPath);

                return _name!;
            }
        }

        public string Path
        {
            get
            {
                if (_path == null)
                    Calculate(FullPath);

                return _path!;
            }
        }

        public string FullPath => _fullPath;

        public PropertyPath(string path)
        {
            _fullPath = path;
            Calculate(FullPath);
        }

        private void Calculate(string path)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_calculateMarker.Auto())
#endif
            {
                // TODO Use ZString
                var parts = path.Split('.');
            
                if (parts.Length > 1)
                {
                    _name = parts[^1]; 
                    _path = string.Join(".", parts.Take(parts.Length - 1)); 
                }
                else
                {
                    _name = path;
                    _path = string.Empty;
                }
            }
        }
    }
}
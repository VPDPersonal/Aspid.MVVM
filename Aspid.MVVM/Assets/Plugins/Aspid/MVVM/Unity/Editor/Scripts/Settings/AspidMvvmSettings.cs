#nullable enable
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Static helper class for managing Aspid MVVM settings through scripting define symbols.
    /// </summary>
    public static class AspidMvvmSettings
    {
        public const string Version = "1.1.0";
        
        private const string ProfilerDefine = "ASPID_MVVM_UNITY_PROFILER_DISABLED";
        private const string BinderLogDefine = "ASPID_MVVM_BINDER_LOG_DISABLED";
        private const string CheckForEditorDefine = "ASPID_MVVM_EDITOR_DISABLED";

        /// <summary>
        /// Gets or sets whether the profiler is enabled.
        /// When enabled, profiler markers are available for performance monitoring.
        /// </summary>
        public static bool IsEnabledProfiler
        {
            get => !GetDefines(GetTarget()).Contains(ProfilerDefine);
            set
            {
                var target = GetTarget();
                var defines = GetDefines(target);
                
                if (value) RemoveDefine(target, ProfilerDefine, defines);
                else AddDefine(target, ProfilerDefine, defines);
            }
        }

        /// <summary>
        /// Gets or sets whether binder logging is enabled.
        /// When enabled, detailed logs for binder operations are available.
        /// </summary>
        public static bool IsEnabledBinderLog
        {
            get => !GetDefines(GetTarget()).Contains(BinderLogDefine);
            set
            {
                var target = GetTarget();
                var defines = GetDefines(target);
                
                if (value) RemoveDefine(target, BinderLogDefine, defines);
                else AddDefine(target, BinderLogDefine, defines);
            }
        }

        /// <summary>
        /// Gets or sets whether editor checks are enabled.
        /// When enabled, additional validation checks run in the Editor.
        /// </summary>
        public static bool IsEnabledCheckForEditor
        {
            get => !GetDefines(GetTarget()).Contains(CheckForEditorDefine);
            set
            {
                var target = GetTarget();
                var defines = GetDefines(target);
                
                if (value) RemoveDefine(target, CheckForEditorDefine, defines);
                else AddDefine(target, CheckForEditorDefine, defines);
            }
        }

        private static NamedBuildTarget GetTarget() => 
            NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

        private static List<string> GetDefines(NamedBuildTarget target)
        {
            PlayerSettings.GetScriptingDefineSymbols(target, out var defines);
            return defines.ToList();
        }

        private static void AddDefine(NamedBuildTarget target, string define, IList<string> defines)
        {
            if (defines.Contains(define)) return;
            
            defines.Add(define);
            PlayerSettings.SetScriptingDefineSymbols(target, defines.ToArray());
        }

        private static void RemoveDefine(NamedBuildTarget target, string define, IList<string> defines)
        {
            if (!defines.Remove(define)) return;
            PlayerSettings.SetScriptingDefineSymbols(target, defines.ToArray());
        }
    }
}
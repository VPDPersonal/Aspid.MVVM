using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using System.Collections.Generic;

namespace Aspid.MVVM
{
    public static class AppidMvvmMenu
    {
        private const string ProfilerMenu = "Tools/Aspid/Mvvm/Settings/Enable Profiler";
        private const string ProfilerDefine = "ASPID_MVVM_UNITY_PROFILER_DISABLED";
        
        private const string BinderLogMenu = "Tools/Aspid/Mvvm/Settings/Enable Binder Log";
        private const string BinderLogDefine = "ASPID_MVVM_BINDER_LOG_DISABLED";
        
        private const string CheckForEditorMenu = "Tools/Aspid/Mvvm/Settings/Checks for Editor";
        private const string CheckForEditorDefine = "ASPID_MVVM_EDITOR_DISABLED";

        private static bool IsEnabledProfiler
        {
            get
            {
                var defines = GetDefines(GetTarget());
                return !defines.Contains(ProfilerDefine);
            }
            set
            {
                if (value)
                {
                    var target = GetTarget();
                    RemoveDefine(target, ProfilerDefine, GetDefines(target));
                }
                else
                {
                    var target = GetTarget();
                    AddDefine(target, ProfilerDefine, GetDefines(target));
                }
            }
        }
        
        private static bool IsEnabledBinderLog
        {
            get
            {
                var defines = GetDefines(GetTarget());
                return !defines.Contains(BinderLogDefine);
            }
            set
            {
                if (value)
                {
                    var target = GetTarget();
                    RemoveDefine(target, BinderLogDefine, GetDefines(target));
                }
                else
                {
                    var target = GetTarget();
                    AddDefine(target, BinderLogDefine, GetDefines(target));
                }
            }
        }
        
        private static bool IsEnabledCheckForEditor
        {
            get
            {
                var defines = GetDefines(GetTarget());
                return !defines.Contains(CheckForEditorDefine);
            }
            set
            {
                if (value)
                {
                    var target = GetTarget();
                    RemoveDefine(target, CheckForEditorDefine, GetDefines(target));
                }
                else
                {
                    var target = GetTarget();
                    AddDefine(target, CheckForEditorDefine, GetDefines(target));
                }
            }
        }

        [MenuItem(ProfilerMenu)]
        private static void SwitchProfiler()
        {
            IsEnabledProfiler = !IsEnabledProfiler;
        }
        
        [MenuItem(ProfilerMenu, true)]
        private static bool ToggleProfilerValidate()
        {
            Menu.SetChecked(ProfilerMenu, IsEnabledProfiler);
            return true;
        }
        
        [MenuItem(BinderLogMenu)]
        private static void SwitchBinderLog()
        {
            IsEnabledBinderLog = !IsEnabledBinderLog;
        }
        
        [MenuItem(BinderLogMenu, true)]
        private static bool ToggleBinderLogValidate()
        {
            Menu.SetChecked(BinderLogMenu, IsEnabledBinderLog);
            return true;
        }
        
        [MenuItem(CheckForEditorMenu)]
        private static void SwitchCheckForEditor()
        {
            IsEnabledCheckForEditor = !IsEnabledCheckForEditor;
        }
        
        [MenuItem(CheckForEditorMenu, true)]
        private static bool ToggleCheckForEditorValidate()
        {
            Menu.SetChecked(CheckForEditorMenu, IsEnabledCheckForEditor);
            return true;
        }

        private static NamedBuildTarget GetTarget() => 
            NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        
        private static List<string> GetDefines(NamedBuildTarget target)
        {
            PlayerSettings.GetScriptingDefineSymbols(target, out var defines);
            return defines.ToList();
        }

        private static bool AddDefine(NamedBuildTarget target, string define, IList<string> defines)
        {
            if (defines.Contains(define)) return false;
            
            defines.Add(define);
            PlayerSettings.SetScriptingDefineSymbols(target, defines.ToArray());
            
            return true;
        }
        
        private static bool RemoveDefine(NamedBuildTarget target, string define, IList<string> defines)
        {
            if (!defines.Remove(define)) return false;
            PlayerSettings.SetScriptingDefineSymbols(target, defines.ToArray());

            return true;
        }
    }
}
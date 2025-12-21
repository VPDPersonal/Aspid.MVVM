using UnityEditor;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedMethodReturnValue.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Menu items for Aspid MVVM settings.
    /// These menu items provide quick access to toggle settings from the Tools menu.
    /// For a more convenient UI, use Tools/Aspid/Mvvm/Settings Window.
    /// </summary>
    public static class AppidMvvmMenu
    {
        private const string ProfilerMenu = "Tools/Aspid/Mvvm/Settings/Enable Profiler";
        private const string BinderLogMenu = "Tools/Aspid/Mvvm/Settings/Enable Binder Log";
        private const string CheckForEditorMenu = "Tools/Aspid/Mvvm/Settings/Checks for Editor";

        [MenuItem(ProfilerMenu)]
        private static void SwitchProfiler()
        {
            AspidMvvmSettings.IsEnabledProfiler = !AspidMvvmSettings.IsEnabledProfiler;
        }
        
        [MenuItem(ProfilerMenu, true)]
        private static bool ToggleProfilerValidate()
        {
            Menu.SetChecked(ProfilerMenu, AspidMvvmSettings.IsEnabledProfiler);
            return true;
        }
        
        [MenuItem(BinderLogMenu)]
        private static void SwitchBinderLog()
        {
            AspidMvvmSettings.IsEnabledBinderLog = !AspidMvvmSettings.IsEnabledBinderLog;
        }
        
        [MenuItem(BinderLogMenu, true)]
        private static bool ToggleBinderLogValidate()
        {
            Menu.SetChecked(BinderLogMenu, AspidMvvmSettings.IsEnabledBinderLog);
            return true;
        }
        
        [MenuItem(CheckForEditorMenu)]
        private static void SwitchCheckForEditor()
        {
            AspidMvvmSettings.IsEnabledCheckForEditor = !AspidMvvmSettings.IsEnabledCheckForEditor;
        }
        
        [MenuItem(CheckForEditorMenu, true)]
        private static bool ToggleCheckForEditorValidate()
        {
            Menu.SetChecked(CheckForEditorMenu, AspidMvvmSettings.IsEnabledCheckForEditor);
            return true;
        }
    }
}
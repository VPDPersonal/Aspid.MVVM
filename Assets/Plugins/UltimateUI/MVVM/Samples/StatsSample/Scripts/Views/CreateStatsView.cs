using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using UltimateUI.MVVM.StarterKit.Binders.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Views
{
    [View]
    public partial class CreateStatsView : ReadOnlyStatsView
    {
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;
        
        [Header("Commands")]
        [SerializeField] private ButtonCommandProvider[] _confirmCommand;
        [SerializeField] private ButtonCommandProvider[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _removeSkillPointToCommand;
    }
}
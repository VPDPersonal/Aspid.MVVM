using UnityEngine;
using AspidUI.MVVM.Unity.Views;
using AspidUI.StatsSample.Models;
using AspidUI.MVVM.Views.Generation;
using AspidUI.MVVM.StarterKit.Binders.Commands;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.StatsSample.Views
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
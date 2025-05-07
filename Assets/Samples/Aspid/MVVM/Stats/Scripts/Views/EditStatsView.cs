using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.StarterKit.Unity;
using Aspid.MVVM.Stats.ViewModels;

namespace Aspid.MVVM.Stats.Views
{
    [View]
    public partial class EditStatsView : ReadOnlyStatsView, IView<EditStatsViewModel>
    {
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;
        
        [Header("Commands")]
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private ButtonCommandBinder[] _confirmCommand;
        
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private ButtonCommandBinder[] _resetToDefaultCommand;
        
        [RequireBinder(typeof(IRelayCommand<Skill>))]
        [SerializeField] private ButtonCommandBinder<Skill>[] _addSkillPointToCommand;
        
        [RequireBinder(typeof(IRelayCommand<Skill>))]
        [SerializeField] private ButtonCommandBinder<Skill>[] _removeSkillPointToCommand;
    }
}
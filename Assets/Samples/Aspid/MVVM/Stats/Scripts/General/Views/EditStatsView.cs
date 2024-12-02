using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.Views.Generation;
using Aspid.MVVM.StarterKit.Binders;

namespace Aspid.MVVM.Stats.Views
{
    [View]
    public partial class EditStatsView : ReadOnlyStatsView
    {
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;
        
        [Header("Commands")]
        [SerializeField] private ButtonCommandBinder[] _confirmCommand;
        [SerializeField] private ButtonCommandBinder[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _removeSkillPointToCommand;
    }
}
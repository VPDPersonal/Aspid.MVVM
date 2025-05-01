using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.StarterKit.Unity;

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
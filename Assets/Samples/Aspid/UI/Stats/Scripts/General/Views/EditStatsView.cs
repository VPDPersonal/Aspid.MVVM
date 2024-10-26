using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.Stats.Models;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

namespace Aspid.UI.Stats.Views
{
    [View]
    public partial class EditStatsView : ReadOnlyStatsView
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
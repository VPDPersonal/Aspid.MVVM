using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.StatsSample.Models;
using UnityEngine;

namespace Aspid.UI.StatsSample.Views
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
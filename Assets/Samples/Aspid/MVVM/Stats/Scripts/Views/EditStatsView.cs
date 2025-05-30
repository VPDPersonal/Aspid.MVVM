using UnityEngine;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.StarterKit.Unity;
using Aspid.MVVM.Stats.ViewModels;

namespace Aspid.MVVM.Stats.Views
{
    [View]
    public partial class EditStatsView : ReadOnlyStatsView, IView<EditStatsViewModel>
    {
        [Header("Commands")]
        [SerializeField] private ButtonCommandBinder[] _confirmCommand;
        [SerializeField] private ButtonCommandBinder[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _removeSkillPointToCommand;
    }
}
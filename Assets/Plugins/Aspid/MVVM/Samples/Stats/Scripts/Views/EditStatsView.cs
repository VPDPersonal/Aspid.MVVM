using UnityEngine;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.Samples.Stats
{
    [View]
    public sealed partial class EditStatsView : ReadOnlyStatsView
    {
        [Header("Commands")]
        [SerializeField] private ButtonCommandBinder[] _confirmCommand;
        [SerializeField] private ButtonCommandBinder[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandBinder<Skill>[] _removeSkillPointToCommand;
    }
}
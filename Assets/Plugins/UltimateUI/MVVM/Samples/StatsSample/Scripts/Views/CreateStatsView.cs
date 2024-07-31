using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using UltimateUI.MVVM.StarterKit.Binders.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Views
{
    // [View]
    public partial class CreateStatsView : ReadOnlyStatsView
    {
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;
        
        [Header("Commands")]
        [SerializeField] private ButtonCommandProvider[] _confirmCommand;
        [SerializeField] private ButtonCommandProvider[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _removeSkillPointToCommand;

        private const string IsDraftId = "IsDraft";
        private const string ConfirmCommandId = "ConfirmCommand";
        private const string ResetToDefaultCommandId = "ResetToDefaultCommand";
        private const string AddSkillPointToCommandId = "AddSkillPointToCommand";
        private const string RemoveSkillPointToCommandId = "RemoveSkillPointToCommand";
        
        protected override void InitializeIternal(IViewModel viewModel)
        {
            base.InitializeIternal(viewModel);
            viewModel.AddBinder(_isDraft, IsDraftId);
            viewModel.AddBinder(_confirmCommand, ConfirmCommandId);
            viewModel.AddBinder(_resetToDefaultCommand, ResetToDefaultCommandId);
            viewModel.AddBinder(_addSkillPointToCommand, AddSkillPointToCommandId);
            viewModel.AddBinder(_removeSkillPointToCommand, RemoveSkillPointToCommandId);
        }
    }
}
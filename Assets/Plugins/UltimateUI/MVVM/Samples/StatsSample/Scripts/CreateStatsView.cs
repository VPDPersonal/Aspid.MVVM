using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using UltimateUI.MVVM.StarterKit.Binders.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample
{
    public partial class CreateStatsView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _cool;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _power;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _reflexes;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _intelligence;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _technicalAbility;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _skillPointsAvailable;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;

        [Header("Commands")]
        [SerializeField] private ButtonCommandProvider[] _confirmCommand;
        [SerializeField] private ButtonCommandProvider[] _resetToDefaultCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _addSkillPointToCommand;
        [SerializeField] private ButtonCommandProvider<Skill>[] _removeSkillPointToCommand;
    }

    public partial class CreateStatsView
    {
        private const string CoolId = "Cool";
        private const string PowerId = "Power";
        private const string ReflexesIdId = "Reflexes";
        private const string IntelligenceId = "Intelligence";
        private const string TechnicalAbilityId = "TechnicalAbility";
        private const string SkillPointsAvailableId = "SkillPointsAvailable";
        private const string IsDraftId = "IsDraft";
        private const string ConfirmCommandId = "ConfirmCommand";
        private const string ResetToDefaultCommandId = "ResetToDefaultCommand";
        private const string AddSkillPointToCommandId = "AddSkillPointToCommand";
        private const string RemoveSkillPointToCommandId = "RemoveSkillPointToCommand";
    }
    
    public partial class CreateStatsView
    {
        protected override void InitializeIternal(IViewModel viewModel)
        {
            for (var i = 0; i < _cool.Length; i++)
                _cool[i].Bind(viewModel, CoolId);
            
            for (var i = 0; i < _power.Length; i++)
                _power[i].Bind(viewModel, PowerId);
            
            for (var i = 0; i < _reflexes.Length; i++)
                _reflexes[i].Bind(viewModel, ReflexesIdId);
            
            for (var i = 0; i < _intelligence.Length; i++)
                _intelligence[i].Bind(viewModel, IntelligenceId);
            
            for (var i = 0; i < _technicalAbility.Length; i++)
                _technicalAbility[i].Bind(viewModel, TechnicalAbilityId);
            
            for (var i = 0; i < _skillPointsAvailable.Length; i++)
                _skillPointsAvailable[i].Bind(viewModel, SkillPointsAvailableId);
            
            for (var i = 0; i < _isDraft.Length; i++)
                _isDraft[i].Bind(viewModel, IsDraftId);
            
            for (var i = 0; i < _confirmCommand.Length; i++)
                _confirmCommand[i].Bind(viewModel, ConfirmCommandId);
            
            for (var i = 0; i < _resetToDefaultCommand.Length; i++)
                _resetToDefaultCommand[i].Bind(viewModel, ResetToDefaultCommandId);
            
            for (var i = 0; i < _addSkillPointToCommand.Length; i++)
                _addSkillPointToCommand[i].Bind(viewModel, AddSkillPointToCommandId);
            
            for (var i = 0; i < _removeSkillPointToCommand.Length; i++)
                _removeSkillPointToCommand[i].Bind(viewModel, RemoveSkillPointToCommandId);
        }
    }
}
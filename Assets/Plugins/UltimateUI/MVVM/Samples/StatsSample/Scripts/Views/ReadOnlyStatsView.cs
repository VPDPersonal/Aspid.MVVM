using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Views
{
    // [View]
    public partial class ReadOnlyStatsView : MonoView
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
        
        private const string CoolId = "Cool";
        private const string PowerId = "Power";
        private const string ReflexesId = "Reflexes";
        private const string IntelligenceId = "Intelligence";
        private const string TechnicalAbilityId = "TechnicalAbility";
        private const string SkillPointsAvailableId = "SkillPointsAvailable";
        
        protected override void InitializeIternal(IViewModel viewModel)
        {
            viewModel.AddBinder(_cool, CoolId);
            viewModel.AddBinder(_power, PowerId);
            viewModel.AddBinder(_reflexes, ReflexesId);
            viewModel.AddBinder(_intelligence, IntelligenceId);
            viewModel.AddBinder(_technicalAbility, TechnicalAbilityId);
            viewModel.AddBinder(_skillPointsAvailable, SkillPointsAvailableId);
        }
    }
}

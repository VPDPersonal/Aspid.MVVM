using System;
using UnityEngine;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;
using UltimateUI.MVVM.Samples.StatsSample;
using UltimateUI.MVVM.StarterKit.Binders.Commands;

namespace Plugins.UltimateUI.MVVM.Samples.StatsSample.Scripts
{
    [View]
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

        public override IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy()
        {
            if (_cool.Length > 0)
                yield return ("Cool", _cool);
            
            if (_power.Length > 0)
                yield return ("Power", _power);  
            
            if (_reflexes.Length > 0)
                yield return ("Reflexes", _reflexes);
            
            if (_intelligence.Length > 0)
                yield return ("Intelligence", _intelligence);
            
            if (_technicalAbility.Length > 0)
                yield return ("TechnicalAbility", _technicalAbility);
            
            if (_isDraft.Length > 0)
                yield return ("IsDraft", _isDraft);
            
            if (_confirmCommand.Length > 0)
                yield return ("ConfirmCommand", _confirmCommand);
            
            if (_resetToDefaultCommand.Length > 0)
                yield return ("ResetToDefaultCommand", _resetToDefaultCommand);
            
            if (_addSkillPointToCommand.Length > 0)
                yield return ("AddSkillPointToCommand", _addSkillPointToCommand);
            
            if (_removeSkillPointToCommand.Length > 0)
                yield return ("RemoveSkillPointToCommand", _removeSkillPointToCommand);
        }
    }

    [ViewModelToView(typeof(HeroViewModel))]
    public partial class HeroView : MonoBehaviour
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
    }

    public class ViewModelToViewAttribute : Attribute
    {
        public ViewModelToViewAttribute(params Type[] types)
        {
            
        }
    }
}
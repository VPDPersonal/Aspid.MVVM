using System.Collections.Generic;
using UnityEngine;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Views;

namespace Plugins.UltimateUI.MVVM.Samples.StatsSample.Scripts
{
    public class ReadOnlyStatsView : MonoView
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
        
        protected sealed override IReadOnlyBindersCollectionById GetBinders()
        {
            var binders = new BindersCollectionById(11);
            if (_cool.Length > 0) binders.Add("Cool", _cool);
            if (_power.Length > 0) binders.Add("Power", _power);
            if (_reflexes.Length > 0) binders.Add("Reflexes", _reflexes);
            if (_intelligence.Length > 0) binders.Add("Intelligence", _intelligence);
            if (_technicalAbility.Length > 0) binders.Add("TechnicalAbility", _technicalAbility);
            if (_isDraft.Length > 0) binders.Add("IsDraft", _isDraft);
            
            return binders;
        }

        public override IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy()
        {
            throw new System.NotImplementedException();
        }
    }
}

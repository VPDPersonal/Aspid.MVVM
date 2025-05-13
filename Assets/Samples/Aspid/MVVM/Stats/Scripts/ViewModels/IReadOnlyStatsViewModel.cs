namespace Aspid.MVVM.Stats.ViewModels
{
    public interface IReadOnlyStatsViewModel : IViewModel
    {
        public IBindableMemberEventAdder Cool { get; }
        
        public IBindableMemberEventAdder Power { get; }
        
        public IBindableMemberEventAdder Reflexes { get; }
        
        public IBindableMemberEventAdder Intelligence { get; }
        
        public IBindableMemberEventAdder TechnicalAbility { get; }
        
        public IBindableMemberEventAdder SkillPointsAvailable { get; }
    }
}
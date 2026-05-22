#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    public interface IMonoBinderValidable : IBinder
    {
        public bool IsMonoExist { get; }

        #region View Properties
        public IView? View { get; }
        
        public MonoBinderPreviousView PreviousView { get; }
        #endregion

        #region Id Properties
        public string Id { get; }
        
        public MonoBinderPreviousId PreviousId { get; }
        #endregion

        #region Set Methods
        public void SetView(IView? view);
        
        public void SetId(string? id);
        #endregion
        
        #region Reset Nethods
        public void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard);
        
        public void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard);

        public void Reset(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
        {
            if (!IsMonoExist) return;
            
            ResetId(mode);
            ResetView(mode);
        }
        #endregion
    }
}
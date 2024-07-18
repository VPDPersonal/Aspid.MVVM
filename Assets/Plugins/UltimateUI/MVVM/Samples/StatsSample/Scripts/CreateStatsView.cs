using UnityEngine;
using Unity.Profiling;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;

namespace UltimateUI.MVVM.Samples.StatsSample
{
    public partial class CreateStatsView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _cool;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _power;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _reflexes;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _intelligence;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _technicalAbility;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinding[] _skillPointsAvailable;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinding[] _isDraft;
    }
    
    public partial class CreateStatsView
    {
        private static readonly ProfilerMarker _constructorMarker = new("CreateStatsView.Constructor");

        private HeroViewModel _viewModel;

        private void Awake()
        {
            using (_constructorMarker.Auto())
            {
                var coolBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.Cool,
                    setter: value => _viewModel.Cool = value,
                    _viewModel);
                
                for (var i = 0; i < _cool.Length; i++)
                    _cool[i].Bind(in coolBindData);
                
                //--------------------------------------------------
                
                var powerBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.Power,
                    setter: value => _viewModel.Power = value,
                    _viewModel);
                
                for (var i = 0; i < _power.Length; i++)
                    _power[i].Bind(in powerBindData);
                
                //--------------------------------------------------
                
                var reflexesBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.Reflexes,
                    setter: value => _viewModel.Reflexes = value,
                    _viewModel);
                
                for (var i = 0; i < _reflexes.Length; i++)
                    _reflexes[i].Bind(in reflexesBindData);
                
                //--------------------------------------------------
                
                var intelligenceBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.Intelligence,
                    setter: value => _viewModel.Intelligence = value,
                    _viewModel);
                
                for (var i = 0; i < _intelligence.Length; i++)
                    _intelligence[i].Bind(in intelligenceBindData);
                
                //--------------------------------------------------
                
                var technicalAbilityBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.TechnicalAbility,
                    setter: value => _viewModel.TechnicalAbility = value,
                    _viewModel);
                
                for (var i = 0; i < _technicalAbility.Length; i++)
                    _technicalAbility[i].Bind(in technicalAbilityBindData);
                
                //--------------------------------------------------
                
                var skillPointsAvailableBindData = BindData<int>.FullAccess(
                    getter: () => _viewModel.SkillPointsAvailable,
                    setter: value => _viewModel.SkillPointsAvailable = value,
                    _viewModel);
                
                for (var i = 0; i < _skillPointsAvailable.Length; i++)
                    _skillPointsAvailable[i].Bind(in technicalAbilityBindData);
                
                //--------------------------------------------------
                
                var isDraftBindData = BindData<bool>.FullAccess(
                    getter: () => _viewModel.IsDraft,
                    setter: value => _viewModel.IsDraft = value,
                    _viewModel);
                
                for (var i = 0; i < _isDraft.Length; i++)
                    _isDraft[i].Bind(in isDraftBindData);
            }
        }

        [VContainer.Inject]
        public void Constructor(HeroViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
    
    // [View]
    // public partial class CreateStatsView : MonoView
    // {
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _cool;
    //     
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _power;
    //     
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _reflexes;
    //     
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _intelligence;
    //     
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _technicalAbility;
    //     
    //     [RequireBinder(typeof(int))]
    //     [SerializeField] private MonoBinder[] _skillPointsAvailable;
    //     
    //     [RequireBinder(typeof(bool))]
    //     [SerializeField] private MonoBinder[] _isDraft;
    //
    //     [Header("Commands")]
    //     [SerializeField] private ButtonCommandProvider[] _confirmCommand;
    //     [SerializeField] private ButtonCommandProvider[] _resetToDefaultCommand;
    //     [SerializeField] private ButtonCommandProvider<Skill>[] _addSkillPointToCommand;
    //     [SerializeField] private ButtonCommandProvider<Skill>[] _removeSkillPointToCommand;
    // }
    //
    // public partial class CreateStatsView
    // {
    //     protected IViewModel ViewMode { get; set; }
    //     
    //     
    //     public void Constructor(TestViewModel testViewModel, HeroViewModel heroViewModel)
    //     {
    //         var coolBindData = BindData<int>.ReadOnly(() => heroViewModel.Cool, heroViewModel);
    //         for (var i = 0; i < _cool.Length; i++)
    //             _cool[i].Bind(coolBindData);
    //         
    //         var idBinding = new Binding("Id");
    //         idBinding.Bind(BindData<string>.FullAccess(
    //             getter: () => testViewModel.Id, 
    //             setter: value => testViewModel.Id = value,
    //             testViewModel));
    //
    //         var doctor = testViewModel.Doctor;
    //         var doctorNameBinding = new Binding("Name");
    //         doctorNameBinding.Bind(BindData<string>.FullAccess(
    //             getter: () => doctor.Name,
    //             setter: value => doctor.Name = value,
    //             doctor));
    //     }
    // }
    //
    // public class NewView : MonoBehaviour
    // {
    //     private MonoBinder[] _idBindings;
    //     
    //     [RequireBinder(typeof(Person))]
    //     private MonoBinder[] _whoBindings;
    //
    //     private void OnValidate()
    //     {
    //         _idBindings = _idBindings.Where(idBinding => idBinding.Id != "Id").ToArray();
    //         
    //         for (var i = 0; i < _whoBindings.Length; i++)
    //         {
    //             if (_whoBindings[i].Id != "Who")
    //                 _whoBindings[i] = null;
    //         }
    //
    //         for (int i = 0; i < UPPER; i++)
    //         {
    //             
    //         }
    //     }
    //
    //     public void Initialize()
    //     {
    //         
    //     }
    // }
    //
    // public class TestViewModel : INotifyPropertyChanged
    // {
    //     public event PropertyChangedEventHandler PropertyChanged;
    //
    //     private string _id;
    //     private Person _person;
    //
    //     public string Id
    //     {
    //         get => _id;
    //         set => SetField(ref _id, value);
    //     }
    //
    //     public Person Doctor => _person;
    //
    //     protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
    //         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //
    //     protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    //     {
    //         if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    //         field = value;
    //         OnPropertyChanged(propertyName);
    //         return true;
    //     }
    // }
    //
    //
    // public class Person : INotifyPropertyChanged
    // {
    //     public event PropertyChangedEventHandler PropertyChanged;
    //     
    //     private int _age;
    //     private string _name;
    //     
    //     public string Name
    //     {
    //         get => _name;
    //         set => SetField(ref _name, value);
    //     }
    //     
    //     public int Age
    //     {
    //         get => _age;
    //         set => SetField(ref _age, value);
    //     }
    //
    //     protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
    //         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //
    //     protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    //     {
    //         if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    //         field = value;
    //         OnPropertyChanged(propertyName);
    //         return true;
    //     }
    // }
}
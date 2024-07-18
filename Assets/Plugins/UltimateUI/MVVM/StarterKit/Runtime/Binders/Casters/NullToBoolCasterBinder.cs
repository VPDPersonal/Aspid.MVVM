// using UnityEngine;
// using UltimateUI.MVVM.Views;
//
// // ReSharper disable once CheckNamespace
// namespace UltimateUI.MVVM.StarterKit.Binders.Casters
// {
//     public class NullToBoolCasterBinder : MonoBinder, IAnyBinder
//     {
//         [Header("Binders")]
//         [RequireBinder(typeof(bool))]
//         [SerializeField] private MonoBinder[] _binders;
//         
//         public void SetValue<T>(T value)
//         {
//             var casterValue = value == null;
//             
//             foreach (var binder in _binders)
//             {
//                 if (binder is IBinder<bool> boolBinder)
//                     boolBinder.SetValue(casterValue);
//             }
//         }
//     }
// }
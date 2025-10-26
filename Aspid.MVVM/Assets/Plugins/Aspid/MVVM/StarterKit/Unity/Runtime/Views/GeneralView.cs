// using System;
// using UnityEngine;
//
// // ReSharper disable once CheckNamespace
// namespace Aspid.MVVM.StarterKit
// {
//     public class GeneralView : MonoView
//     {
// #if UNITY_EDITOR
//         [SerializeField] private SerializableMonoScript<IViewModel> _designViewModel;
// #endif
//         
//         [RequireBinder]
//         [SerializeField] private Binders[] _binders;
//
//         protected override void InitializeInternal(IViewModel viewModel)
//         {
//             foreach (var binder in _binders)
//             {
//                 binder.Bind(viewModel);
//             }
//         }
//
//         protected override void DeinitializeInternal()
//         {
//             foreach (var binder in _binders)
//             {
//                 binder.Unbind();
//             }
//         }
//
//         [Serializable]
//         private sealed class Binders
//         {
// #if UNITY_EDITOR
//             [HideInInspector]
//             [SerializeField] private string _assemblyQualifiedName;
// #endif
//             
//             [SerializeField] private string _id;
//             
// #if UNITY_EDITOR
//             [RequireBinder(nameof(_assemblyQualifiedName))]
// #endif
//             [SerializeReferenceDropdown]
//             [SerializeReference] private IBinder[] _binders;
//
//             public void Bind(IViewModel viewModel)
//             {
//                 var result = viewModel.FindBindableMember(new FindBindableMemberParameters(_id));
//                 if (!result.IsFound) return;
//
//                 var binderAdder = result.Adder;
//                 foreach (var binder in _binders)
//                 {
//                     binder.Bind(binderAdder);
//                 }
//             }
//
//             public void Unbind()
//             {
//                 foreach (var binder in _binders)
//                 {
//                     binder.Unbind();
//                 }
//             }
//         }
//     }
// }
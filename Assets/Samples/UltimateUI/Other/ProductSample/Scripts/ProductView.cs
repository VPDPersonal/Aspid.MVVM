// using System.Collections.Generic;
// using UnityEngine;
// using UltimateUI.MVVM.Views;
//
// namespace UltimateUI.MVVM.Samples.ProductSample
// {
//     [View]
//     public partial class ProductView : MonoView
//     {
//         [RequireBinder(typeof(Sprite))]
//         [SerializeField] private MonoBinder[] _icon;
//         
//         [RequireBinder(typeof(string))]
//         [SerializeField] private MonoBinder[] _name;
//         
//         [RequireBinder(typeof(string))]
//         [SerializeField] private MonoBinder[] _description;
//         
//         [RequireBinder(typeof(Sprite))]
//         [SerializeField] private Sprite _currencyIcon;
//
//         public override IEnumerable<IBinder> GetBindersLazy()
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }
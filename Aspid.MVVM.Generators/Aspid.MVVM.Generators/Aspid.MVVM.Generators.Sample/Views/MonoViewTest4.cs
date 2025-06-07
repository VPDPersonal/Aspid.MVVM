// using System;
// using Aspid.UI.MVVM;
// using Aspid.UI.MVVM.Mono.Views;
// using Aspid.UI.MVVM.ViewModels;
// using Aspid.UI.MVVM.Views.Generation;
//
// namespace Aspid.MVVM.Generators.Sample.Views;
//
// [View]
// public partial class MonoViewTest4 : MonoView
// {
//     private int _choice = 0;
//     private IBinder<string>? _stringBinder1;
//     
//     [AsBinder(typeof(TextBinder))]
//     private IBinder<string>? _stringBinder2;
//     
//     private IBinder<string>? StringBinder1 => _stringBinder1;
//
//     private IBinder<string>? StringBinder2 => _stringBinder1 is not null ? null : null;
//
//     private IBinder<string>? StringBinder3 => _choice switch
//     {
//         0 => null,
//         1 => null,
//         _ => throw new ArgumentOutOfRangeException()
//     };
//
//     private IBinder<string> StringBinder4
//     {
//         get => null;
//         set => _stringBinder1 = value;
//     }
//     
//     private IBinder<string>? StringBinder5
//     {
//         get => _stringBinder1 is not null ? null : _stringBinder1 is not null ? null : null;
//         set => _stringBinder1 = value;
//     }
//     
//     private IBinder<string>? StringBinder6
//     {
//         get => _choice switch
//         {
//             0 => _choice switch
//             {
//                 0 => null,
//                 1 => null,
//                 _ => throw new ArgumentOutOfRangeException()
//             },
//             1 => null,
//             _ => throw new ArgumentOutOfRangeException()
//         };
//         set => _stringBinder1 = value;
//     }
//
//     private IBinder<string>? StringBinder7
//     {
//         get
//         {
//             if ("a" == "v")
//             {
//                 return null;
//             }
//             
//             return _choice switch
//             {
//                 0 => _choice switch
//                 {
//                     0 => null,
//                     1 => null,
//                     _ => throw new ArgumentOutOfRangeException()
//                 },
//                 1 => null,
//                 _ => throw new ArgumentOutOfRangeException()
//             };;
//         }
//     }
// }
//
// public class TextBinder : IBinder<string>
// {
//     public TextBinder(IBinder<string> binder)
//     {
//     }
//
//     public void SetValue(string value)
//     {
//         
//     }
//
//     public void Bind(IViewModel viewModel, string id)
//     {
//         
//     }
//
//     public void Unbind(IViewModel viewModel, string id)
//     {
//     }
// }
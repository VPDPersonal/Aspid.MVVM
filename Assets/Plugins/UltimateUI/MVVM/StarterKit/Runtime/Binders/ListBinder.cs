using System;
using System.Collections.Generic;

namespace UltimateUI.MVVM.StarterKit.Binders
{
    public partial class ListBinder : MonoBinder, IBinder<IReadOnlyList<int>>
    {
        [BindInheritorsAlso]
        public void SetValue(IReadOnlyList<int> value)
        {
            
        }
        
        
    }

    // public partial class ListBinder
    // {
    //     protected readonly Dictionary<Delegate, Delegate> Handlers = new();
    //     
    //     public bool Bind<T>(in T value, ref Action<T> changed)
    //     {
    //         switch (this)
    //         {
    //             case IBinder<T> specificBinder:
    //                 specificBinder.SetValue(value);
    //                 changed += specificBinder.SetValue;
    //                 return true;
    //
    //             case IAnyBinder anyBinder:
    //                 anyBinder.SetValue(value);
    //                 changed += anyBinder.SetValue;
    //                 return true;
    //
    //             default:
    //                 if (!BindFromHandler(value, out var handler)) return false;
    //
    //                 changed += handler;
    //                 Handlers.Add(changed, handler);
    //                 return true;
    //         }
    //     }
    //
    //     protected virtual bool BindFromHandler<T>(T value, out Action<T> handler)
    //     {
    //         handler = null;
    //         
    //         if (value is IReadOnlyList<int> aValue)
    //         {
    //             SetValue(aValue);
    //             handler = param => SetValue(param as IReadOnlyList<int>);
    //             return true;
    //         }
    //
    //         return false;
    //     }
    //
    //     public bool Unbind<T>(ref Action<T> changed)
    //     {
    //         if (base.Unbind(ref changed)) return true;
    //         if (!Handlers.TryGetValue(changed, out var handler)) return false;
    //         
    //         changed -= (Action<T>)handler;
    //         Handlers.Remove(changed);
    //         
    //         return true;
    //     }
    // }
}
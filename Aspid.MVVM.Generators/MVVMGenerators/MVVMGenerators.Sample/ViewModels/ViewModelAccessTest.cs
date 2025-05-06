using Aspid.MVVM;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModelAccessTest
{
    [Bind] private int _a; 

    [Access(Access.Public)]
    [Bind] private int _b; 
    
    [Access(Access.Protected)]
    [Bind] private int _c; 
    
    [Access(Get = Access.Public)]
    [Bind] private int _d; 
    
    [Access(Get = Access.Protected)]
    [Bind] private int _e; 
    
    [Access(Set = Access.Public)]
    [Bind] private int _f; 
    
    [Access(Set = Access.Protected)]
    [Bind] private int _g; 
    
    [Access(Get = Access.Public, Set = Access.Protected)]
    [Bind] private int _h; 
    
    [Access(Get = Access.Protected, Set = Access.Public)]
    [Bind] private int _i; 
}
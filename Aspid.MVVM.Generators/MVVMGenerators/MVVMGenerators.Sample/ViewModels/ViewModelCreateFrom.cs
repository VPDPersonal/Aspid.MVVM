using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

public class CreateFromModel
{
    
}

[ViewModel]
public partial class ViewModelCreateFrom
{
    [CreateFrom(typeof(CreateFromModel))]
    public ViewModelCreateFrom(int b, CreateFromModel a)
    {
    }
    
    [CreateFrom(typeof(int))]
    public ViewModelCreateFrom(CreateFromModel a, int s)
    {
        
    }
    
    [CreateFrom(typeof(int))]
    public ViewModelCreateFrom(CreateFromModel a, int c, int b)
    {
        
    }
}
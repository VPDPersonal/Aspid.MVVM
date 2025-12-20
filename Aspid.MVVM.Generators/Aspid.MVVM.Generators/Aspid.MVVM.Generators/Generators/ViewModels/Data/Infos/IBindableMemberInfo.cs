using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Generators.Ids.Data;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public interface IBindableMemberInfo
{
    public ISymbol Member { get; }
    
    public string Type { get; }
    
    public string Name { get; }
    
    public IdData Id { get; }
    
    public BindMode Mode { get; }
    
    public GeneratedBindableMembers Bindable { get; }
}

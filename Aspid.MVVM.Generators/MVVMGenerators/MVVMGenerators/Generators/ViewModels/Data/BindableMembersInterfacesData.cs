using System.Linq;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct BindableMembersInterfacesData(ImmutableArray<BindableMembersInterfaceData> interfaces)
{
    public readonly ImmutableArray<BindableMembersInterfaceData> Interfaces = interfaces;

    public bool HasName(string name) =>
        Enumerable.Any(Interfaces, @interface => @interface.Names.Contains(name));

    public bool TryGetInterface(string name, out BindableMembersInterfaceData interfaceData)
    {
        foreach (var @interface in Interfaces)
        {
            if (!@interface.Names.Contains(name)) continue;
            
            interfaceData = @interface;
            return true;
        }

        interfaceData = default;
        return false;
    }
}
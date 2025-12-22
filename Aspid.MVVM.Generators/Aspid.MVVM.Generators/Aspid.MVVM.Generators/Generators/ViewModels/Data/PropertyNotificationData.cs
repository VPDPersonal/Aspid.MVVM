using System.Collections.Generic;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data;

public sealed class PropertyNotificationData
{
    // Key - propertyName, Value - List of line numbers
    public Dictionary<string, List<int>> OnPropertyChangedCalls { get; } = [];
    
    // Key - type, Value - Dictionary (propertyName -> List of line numbers) 
    public Dictionary<string, Dictionary<string, List<int>>> SetFieldCallsByType { get; } = [];
    
    public HashSet<string> PropertiesRequiringSetFieldBody { get; } = [];
    
    public bool HasOnPropertyChangedCalls => OnPropertyChangedCalls.Count > 0;
    
    public bool HasSetFieldCalls => SetFieldCallsByType.Count > 0;

    public void AddOnPropertyChangedCall(string propertyName, int line)
    {
        if (!OnPropertyChangedCalls.TryGetValue(propertyName, out var lines))
        {
            lines = [];
            OnPropertyChangedCalls[propertyName] = lines;
        }
        
        lines.Add(line);
    }

    public void AddSetFieldCall(string propertyName, int line, string type)
    {
        if (!SetFieldCallsByType.TryGetValue(type, out var propertyCalls))
        {
            propertyCalls = [];
            SetFieldCallsByType[type] = propertyCalls;
        }
        
        if (!propertyCalls.TryGetValue(propertyName, out var lines))
        {
            lines = [];
            propertyCalls[propertyName] = lines;
        }
        
        lines.Add(line);
        PropertiesRequiringSetFieldBody.Add(propertyName);
    }
}

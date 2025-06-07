using System;

#nullable disable
namespace UnityEngine;

public sealed class AddComponentMenu : Attribute
{
    public AddComponentMenu(string menuName) { }
    
    public AddComponentMenu(string menuName, int order) { }
}
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.Internal
{
    public static class EditorConstants
    {
        public const string AspidIconRed = "AspidIconRed";
        public const string AspidIconGreen = "AspidIconGreen";
        public const string AspidIconYellow = "AspidIconYellow";

        public static readonly Color ErrorColor = new(r: 0.41f, g: 0.05f, b: 0.05f);
        public static readonly Color WarningColor = new(r: 0.61f, g: 0.44f, b: 0.11f);
        public static readonly Color SuccessColor = new(r: 0.04f, g: 0.27f, b: 0.17f);
    } 
}
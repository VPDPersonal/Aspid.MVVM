using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class ToCultureStringExtensions
    {
        public static CultureInfo ToCultureInfo(this CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => CultureInfo.CurrentCulture,
            CultureInfoMode.CurrentUICulture => CultureInfo.CurrentUICulture,
            CultureInfoMode.InvariantCulture => CultureInfo.InvariantCulture,
            CultureInfoMode.InstalledUICulture => CultureInfo.InstalledUICulture,
            CultureInfoMode.DefaultThreadCurrentCulture => CultureInfo.DefaultThreadCurrentCulture,
            CultureInfoMode.DefaultThreadCurrentUICulture => CultureInfo.DefaultThreadCurrentUICulture,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

        public static string ToCultureString(this int number, CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => number.ToString(CultureInfo.CurrentCulture),
            CultureInfoMode.CurrentUICulture => number.ToString(CultureInfo.CurrentUICulture),
            CultureInfoMode.InvariantCulture => number.ToString(CultureInfo.InvariantCulture),
            CultureInfoMode.InstalledUICulture => number.ToString(CultureInfo.InstalledUICulture),
            CultureInfoMode.DefaultThreadCurrentCulture => number.ToString(CultureInfo.DefaultThreadCurrentCulture),
            CultureInfoMode.DefaultThreadCurrentUICulture => number.ToString(CultureInfo.DefaultThreadCurrentUICulture),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
        
        public static string ToCultureString(this uint number, CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => number.ToString(CultureInfo.CurrentCulture),
            CultureInfoMode.CurrentUICulture => number.ToString(CultureInfo.CurrentUICulture),
            CultureInfoMode.InvariantCulture => number.ToString(CultureInfo.InvariantCulture),
            CultureInfoMode.InstalledUICulture => number.ToString(CultureInfo.InstalledUICulture),
            CultureInfoMode.DefaultThreadCurrentCulture => number.ToString(CultureInfo.DefaultThreadCurrentCulture),
            CultureInfoMode.DefaultThreadCurrentUICulture => number.ToString(CultureInfo.DefaultThreadCurrentUICulture),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
        
        public static string ToCultureString(this long number, CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => number.ToString(CultureInfo.CurrentCulture),
            CultureInfoMode.CurrentUICulture => number.ToString(CultureInfo.CurrentUICulture),
            CultureInfoMode.InvariantCulture => number.ToString(CultureInfo.InvariantCulture),
            CultureInfoMode.InstalledUICulture => number.ToString(CultureInfo.InstalledUICulture),
            CultureInfoMode.DefaultThreadCurrentCulture => number.ToString(CultureInfo.DefaultThreadCurrentCulture),
            CultureInfoMode.DefaultThreadCurrentUICulture => number.ToString(CultureInfo.DefaultThreadCurrentUICulture),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
        
        public static string ToCultureString(this double number, CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => number.ToString(CultureInfo.CurrentCulture),
            CultureInfoMode.CurrentUICulture => number.ToString(CultureInfo.CurrentUICulture),
            CultureInfoMode.InvariantCulture => number.ToString(CultureInfo.InvariantCulture),
            CultureInfoMode.InstalledUICulture => number.ToString(CultureInfo.InstalledUICulture),
            CultureInfoMode.DefaultThreadCurrentCulture => number.ToString(CultureInfo.DefaultThreadCurrentCulture),
            CultureInfoMode.DefaultThreadCurrentUICulture => number.ToString(CultureInfo.DefaultThreadCurrentUICulture),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
        
        public static string ToCultureString(this float number, CultureInfoMode mode) => mode switch
        {
            CultureInfoMode.CurrentCulture => number.ToString(CultureInfo.CurrentCulture),
            CultureInfoMode.CurrentUICulture => number.ToString(CultureInfo.CurrentUICulture),
            CultureInfoMode.InvariantCulture => number.ToString(CultureInfo.InvariantCulture),
            CultureInfoMode.InstalledUICulture => number.ToString(CultureInfo.InstalledUICulture),
            CultureInfoMode.DefaultThreadCurrentCulture => number.ToString(CultureInfo.DefaultThreadCurrentCulture),
            CultureInfoMode.DefaultThreadCurrentUICulture => number.ToString(CultureInfo.DefaultThreadCurrentUICulture),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}
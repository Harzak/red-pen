using System.Globalization;
using System.Reflection;
using System.Resources;

namespace redpen.resources;

/// <summary>
/// Resolves localized strings from the embedded resources.
/// </summary>
public sealed class ResourceLocalizer : ILocalizer
{
    private readonly ResourceManager _resourceManager;

    public ResourceLocalizer()
    {
        _resourceManager = new ResourceManager("redpen.resources.Strings", Assembly.GetExecutingAssembly());
    }

    public string this[string key]
    {
        get
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);
            return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
        }
    }
}


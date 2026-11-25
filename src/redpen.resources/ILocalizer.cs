namespace redpen.resources;

/// <summary>
/// Provides localized strings to the application layers.
/// </summary>
public interface ILocalizer
{
    /// <summary>
    /// Gets the localized string associated with the specified key.
    /// </summary>
    string this[string key] { get; }
}

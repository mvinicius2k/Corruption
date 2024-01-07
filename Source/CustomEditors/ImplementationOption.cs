using System;

namespace CustomEditors;

/// <summary>
/// Bind between type and displayed name for user
/// </summary>
public record ImplementationOption
{
    public string DisplayName { get; init; }
    public Type Type { get; init; }
}


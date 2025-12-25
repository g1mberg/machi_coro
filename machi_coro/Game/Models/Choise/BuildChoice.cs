using Game.Models.Enterprises;

namespace Game.Models;

public class BuildChoice
{
    public BuildChoiceType Type { get; init; }
    public string? EnterpriseName { get; init; }
    public string? SiteName { get; init; }
}

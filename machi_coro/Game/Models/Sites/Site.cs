

namespace Game.Models.Sites;

public class Site 
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public bool IsActivated { get; private set; } = false;
    public bool Activate() => IsActivated = true;
}
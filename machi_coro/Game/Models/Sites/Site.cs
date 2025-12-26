

namespace Game.Models.Sites;

public class Site 
{
    public string Name { get; init; }
    public int Cost { get; init; }
    public bool IsActivated { get; private set; }
    public void Activate() => IsActivated = true;
}
namespace Game.Models.Sites;

public abstract class Site
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public virtual bool IsActivated { get; private set; } = false;
    public virtual void Activate() => IsActivated = true;
}
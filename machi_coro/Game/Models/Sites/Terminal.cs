namespace Game.Models.Sites;

public class Terminal : Site
{
    public Terminal()
    {
        Name = "Вокзал";
        Cost = 4;
        Description = "Можно бросать 2 кубика вместо 1";
    }
}
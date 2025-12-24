using Game.Models.Enterprises;
using Game.Utils;

Console.WriteLine(JsonRepository<Enterprise>.Get("CheeseFactory").Foreach);

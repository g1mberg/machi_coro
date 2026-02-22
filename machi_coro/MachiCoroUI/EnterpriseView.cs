namespace MachiCoroUI
{
    public class EnterpriseView
    {
        private static readonly Dictionary<string, string> NameToImage = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Пшеничное поле"] = "wheat field",
            ["Яблочный Сад"] = "apple",
            ["Ферма"] = "farm",
            ["Заповедник"] = "reserve",
            ["Рудник"] = "mine",
            ["Пекарня"] = "bakery",
            ["Супермаркет"] = "supermarket",
            ["Сыроварня"] = "cheese factory",
            ["Мебельная фабрика"] = "furniture factory",
            ["Овощной рынок"] = "vegetable market",
            ["Кафе"] = "cafe",
            ["Закусочная"] = "eatary",
            ["Стадион"] = "stadium",
            ["Телецентр"] = "tvcenter",
            ["Бизнес-центр"] = "business",
            ["Парк развлечений"] = "Park",
            ["Вокзал"] = "terminal",
            ["Торговый центр"] = "Mall",
            ["Телебашня"] = "TV",
            ["Park"] = "Park",
            ["Terminal"] = "terminal",
            ["Mall"] = "Mall",
            ["TvTower"] = "TV",
        };

        public string Name { get; init; }
        public string ImageName
        {
            get
            {
                if (NameToImage.TryGetValue(Name, out var img))
                    return $"{img}.png";
                return $"{Name.ToLower()}.png";
            }
        }
    }


}

using System.Text.Json;
using System.Text.Json.Serialization;
using Game.Models.Enterprises;

namespace Game.Utils;

public static class EnterpriseFromJson
{
    static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private static readonly Dictionary<string, Enterprise> Cache =
        JsonSerializer.Deserialize<Dictionary<string, Enterprise>>(File.ReadAllText("enterprises.json"), Options)!;


    public static Enterprise Get(string key) => Cache[key];

    public static List<Enterprise> GetAll() => Cache.Values.ToList();
}
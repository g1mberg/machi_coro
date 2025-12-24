using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game.Utils;

public static class JsonRepository<T>
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private static readonly Dictionary<string, Dictionary<string, T>> Caches = new();

    private static string GetDefaultFileName()
        => typeof(T).Name.ToLowerInvariant() + "s.json";

    private static Dictionary<string, T> GetCache(string? fileName = null)
    {
        fileName ??= GetDefaultFileName();

        if (Caches.TryGetValue(fileName, out var cache))
            return cache;

        var json = File.ReadAllText(fileName);
        cache = JsonSerializer.Deserialize<Dictionary<string, T>>(json, Options)!;
        Caches[fileName] = cache;
        return cache;
    }

    public static T Get(string key)
        => GetCache()[key];

    public static List<T> GetAll()
        => GetCache().Values.ToList();

    public static Dictionary<string, T> GetDict() 
        => GetCache();

    public static T Get(string key, string fileName)
        => GetCache(fileName)[key];

    public static List<T> GetAll(string fileName)
        => GetCache(fileName).Values.ToList();

    public static Dictionary<string, T> GetDict(string fileName)
        => GetCache(fileName);
}
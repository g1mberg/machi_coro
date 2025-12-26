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

        if (!File.Exists(fileName))
            throw new FileNotFoundException($"JsonRepository<{typeof(T).Name}>: файл не найден: {fileName}");

        var json = File.ReadAllText(fileName);

        var dict = JsonSerializer.Deserialize<Dictionary<string, T>>(json, Options);
        if (dict is null)
            throw new Exception($"JsonRepository<{typeof(T).Name}>: не удалось десериализовать {fileName}");

        Caches[fileName] = dict;
        return dict;
    }


    public static T Get(string key)
    {
        var cache = GetCache();
        if (!cache.TryGetValue(key, out var value))
            throw new KeyNotFoundException(
                $"JsonRepository<{typeof(T).Name}>: ключ '{key}' не найден. Доступные: {string.Join(", ", cache.Keys)}");
        return value!;
    }


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
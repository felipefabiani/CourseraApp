using System.Text.Json;


namespace EventEaseApp.Extensions;
public static class ObjectExtensions
{
    public static T DeepClone<T>(this T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json)!;
    }
}

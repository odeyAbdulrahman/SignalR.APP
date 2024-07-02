
using SignalR.API.Dto;

namespace SignalR.API.Extensions;
public static class FieldMapperUtility
{
    public static Dictionary<string, string?> Fildes(this FieldMapperDto? mailFildes)
    {
        return new Dictionary<string, string?>
        {
            { "@Name", mailFildes?.Name },
            { "@Email", mailFildes?.Email },
            { "@Phone", mailFildes?.Phone },
            { "@Link", mailFildes?.Link }
        };
    }

    public static string? ReplacePlaceholders(this string input, Dictionary<string, string?> replacements)
    {
        foreach (var replacement in replacements)
        {
            input = input.Replace(replacement.Key, replacement.Value);
        }
        return input;
    }
}

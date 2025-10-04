using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Extensions;

public static class IdentityResultExtensions
{
    public static Dictionary<string, string[]> ToErrorDictionary(this IdentityResult result)
    {
        return result.Errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray()
            );
    }
}
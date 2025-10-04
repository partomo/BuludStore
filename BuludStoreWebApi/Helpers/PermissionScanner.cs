using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace BuludStore.Helpers;

public class PermissionScanner
{
    public static List<string> GetControllerResources(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => t.Name.Replace("Controller", "")) 
            .ToList();
    }
}
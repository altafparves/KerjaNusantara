using System.Collections.Generic;

namespace KerjaNusantara.ConsoleApp.Framework;

public class MenuSession
{
    public string? CurrentUserId { get; set; }
    public Dictionary<string, object> Data { get; } = new Dictionary<string, object>();

    public void Clear()
    {
        CurrentUserId = null;
        Data.Clear();
    }
}

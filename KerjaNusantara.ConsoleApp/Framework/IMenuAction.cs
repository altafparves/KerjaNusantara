using System;

namespace KerjaNusantara.ConsoleApp.Framework;

public interface IMenuAction
{
    string Name { get; }
    void Execute(MenuSession session);
}

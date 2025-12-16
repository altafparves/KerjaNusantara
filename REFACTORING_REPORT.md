# Refactoring Report: Government Portal Remodel

## 📋 Overview
This document details the recent refactoring of the `GovernmentMenu` component in the **KerjaNusantara** Console Application. The refactoring aimed to address mounting technical debt and improve the maintainability of the codebase by transitioning from a monolithic class structure to a modular **Command Pattern**.

## 🏚️ Previous Code State
**File**: `GovernmentMenu.cs`
- **Architecture**: Monolithic "God Class".
- **Complexity**: ~320 lines of code.
- **Responsibilities**:
  - 🖥️ **UI Rendering**: Handled all console output and menu loops.
  - 📥 **Input Validation**: Directly managed user input parsing and validation logic.
  - 🔄 **Business Logic**: Contained implementation for 7 distinct features (Registration, Login, Project Management, Bidding, etc.).
  - 💾 **State Management**: Manually tracked session state (`_currentGovernmentId`) mixed with logic.

### ❌ Code Example (Before: The Monolith)
The `GovernmentMenu` was filled with long switch statements and tightly coupled logic:
```csharp
// BEFORE: Monolithic class handling everything
public class GovernmentMenu
{
    private string? _currentGovernmentId; // State mixed with UI logic

    public void Show() { /* ... loop ... */ }

    private bool ShowMainMenu()
    {
        // ... display options ...
        switch (choice)
        {
            case 1: CreateProject(); break; // Private method in the same class
            case 2: ViewMyProjects(); break;
            // ... cases 3, 4, 5, 6
        }
        return false;
    }

    // Business logic mixed directly into the menu class
    private void CreateProject()
    {
         var title = ConsoleHelper.ReadInput("Project Title");
         var budget = ConsoleHelper.ReadDecimal("Budget");
         _tenderService.CreateProject(_currentGovernmentId, ...);
    }
}
```

## 🛠️ Why It Was Refactored
The code suffered from several forms of **Technical Debt**:
1.  **Single Responsibility Principle (SRP) Violation**: The class had too many reasons to change. A change in "Project Creation" logic required editing the Menu class.
2.  **Tight Coupling**: UI logic was inseparable from business logic, making it impossible to test features in isolation.
3.  **Low Maintainability**: The file was growing indefinitely. Adding new features meant taking the risk of breaking existing menu navigation or unrelated features.

## 🏗️ What Was Refactored
We implemented the **Command Pattern** to decouple the *Invoker* (the Menu) from the *Receivers/Actions* (the Features).
We extracted business logic into granular, single-purpose classes in the `Menus/GovernmentActions` directory.

### 🔍 Deep Dive: Extracting Logic
Here is exactly how we moved logic from the main file to a focused class.

#### Before: `GovernmentMenu.cs` (Private Method)
```csharp
private void RegisterGovernment()
{
    ConsoleHelper.DisplaySection("Register Government Entity");
    // Input reading and validation ...
    var name = ConsoleHelper.ReadInput("Contact Person Name", ...);
    var email = ConsoleHelper.ReadInput("Email", ...);

    // Direct service call mixed with UI
    var government = _userService.RegisterGovernment(name, email, ...);
    _currentGovernmentId = government.Id; // Modifying class state
}
```

#### After: `Menus/GovernmentActions/RegisterGovernmentAction.cs` (Isolated Class)
```csharp
public class RegisterGovernmentAction : IMenuAction
{
    private readonly IUserService _userService; // Dependency injected
    public string Name => "Register New Government Entity";

    public void Execute(MenuSession session)
    {
        ConsoleHelper.DisplaySection("Register Government Entity");
        // Logic is identical, but isolated!
        var name = ConsoleHelper.ReadInput("Contact Person Name", ...);
        
        var government = _userService.RegisterGovernment(name, ...);
        session.CurrentUserId = government.Id; // Using session, not global state
    }
}
```

### ✅ Code Example (After: The Clean Orchestrator)
The `GovernmentMenu` is now clean and declarative:
```csharp
// AFTER: Clean orchestrator using Command Pattern
public class GovernmentMenu
{
    private readonly MenuSession _session;
    private readonly List<IMenuAction> _mainActions;

    public GovernmentMenu(...)
    {
        _session = new MenuSession();
        // Configuration: Just add new classes to this list to add features!
        _mainActions = new List<IMenuAction>
        {
            new RegisterGovernmentAction(userService),
            new CreateProjectAction(tenderService),
            new ViewMyProjectsAction(tenderService),
            // ...
        };
    }

    private bool ShowMainMenu()
    {
        // One generic loop handles ALL actions. No switch statements.
        for (int i = 0; i < _mainActions.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_mainActions[i].Name}");
        }
        
        // Execution is delegated to the isolated Action class
        _mainActions[choice - 1].Execute(_session);
        return false;
    }
}
```

## 🎯 Goal
- **Decoupling**: Separate "What to do" (Action) from "How to show it" (Menu).
- **Scalability**: Enable adding new features by simply creating a new class, without touching existing, stable code (Open/Closed Principle).
- **Readability**: transform a complex 300+ line file into a simple, declarative list of actions.

## 🚀 Impact
| Metric | Before | After |
| :--- | :--- | :--- |
| **Lines of Code (Menu)** | ~320 | ~100 |
| **Coupling** | High | Low |
| **Extensibility** | Low (Risk of regression) | High (Add new class) |
| **Readability** | Poor (Nested switches) | Excellent (List of Actions) |

## 📍 Where is the Refactoring?
You can find the changes in the following locations:

- **The Main Menu**: [`KerjaNusantara.ConsoleApp/Menus/GovernmentMenu.cs`](../KerjaNusantara.ConsoleApp/Menus/GovernmentMenu.cs)
- **The New Framework**: [`KerjaNusantara.ConsoleApp/Framework/`](../KerjaNusantara.ConsoleApp/Framework/)
- **The Extracted Actions**: [`KerjaNusantara.ConsoleApp/Menus/GovernmentActions/`](../KerjaNusantara.ConsoleApp/Menus/GovernmentActions/)

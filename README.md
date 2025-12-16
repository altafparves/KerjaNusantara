# KerjaNusantara - Employment Management System

> **Final Project for Object-Oriented Programming (OOP) Lecture**

A comprehensive C# .NET console application that simulates an employment ecosystem connecting citizens, companies, and government entities in Indonesia.

---

## 📋 Project Overview

**KerjaNusantara** is an employment management platform designed to facilitate job matching, tender bidding, and workforce development. The system demonstrates advanced OOP principles and design patterns through a real-world employment scenario.

### Key Features

- 🎯 **AI-Powered Job Matching** - Intelligent skill-based matching algorithm (70% skills + 30% experience)
- 👥 **Multi-User System** - Separate portals for Citizens, Companies, and Government
- 💼 **Job Management** - Post jobs, apply, review applications with match scores
- 🏛️ **Government Tenders** - Create projects, submit bids, award contracts
- 📊 **Analytics Dashboard** - Real-time employment statistics and insights
- 💾 **JSON Persistence** - File-based data storage for all entities

---

## 🎓 Academic Requirements Met

### OOP Principles (4/4)
- ✅ **Encapsulation** - Private fields with public properties, data hiding
- ✅ **Inheritance** - User hierarchy (Citizen, Company, Government extend User)
- ✅ **Polymorphism** - DisplayDashboard() method overriding, Strategy Pattern
- ✅ **Abstraction** - Abstract classes and interfaces throughout

### Design Patterns (3+ Required)
1. ✅ **Repository Pattern** - Data access abstraction with JSON persistence
2. ✅ **Factory Pattern** - UserFactory for centralized object creation
3. ✅ **Strategy Pattern** - IMatchingStrategy for pluggable matching algorithms

### Architecture
- ✅ **Layered Architecture** - Domain, Repository, Services, Presentation
- ✅ **Dependency Injection** - Loose coupling using Microsoft.Extensions.DependencyInjection
- ✅ **Separation of Concerns** - Clear boundaries between layers

---

## 🏗️ Project Structure

```
KerjaNusantara/
├── KerjaNusantara.Domain/          # Domain models and business entities
│   ├── Models/
│   │   ├── Users/                  # Citizen, Company, Government
│   │   ├── Employment/             # Job, JobApplication, Payment
│   │   ├── Skills/                 # Skill, SkillProfile, SkillRequirement
│   │   ├── Projects/               # GovernmentProject, TenderBid
│   │   └── Matching/               # MatchResult, SkillGap
│   ├── Enums/                      # JobStatus, TenderStatus, etc.
│   └── Interfaces/                 # IIdentifiable
│
├── KerjaNusantara.Repository/      # Data access layer
│   ├── Interfaces/                 # IRepository<T>, specific repositories
│   ├── Implementations/            # JsonRepository<T>, concrete repos
│   └── Utilities/                  # JsonFileHelper
│
├── KerjaNusantara.Services/        # Business logic layer
│   ├── Interfaces/                 # Service contracts
│   ├── Implementations/            # Service implementations
│   ├── Factories/                  # UserFactory (Factory Pattern)
│   └── Matching/                   # SkillBasedMatcher (Strategy Pattern)
│
└── KerjaNusantara.ConsoleApp/      # Presentation layer
    ├── Configuration/              # Dependency Injection setup
    ├── Menus/                      # MainMenu, CitizenMenu, etc.
    └── Utilities/                  # ConsoleHelper
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Terminal/Command Prompt

### Installation

1. **Clone or download the project**
   ```bash
   cd /path/to/KerjaNusantara
   ```

2. **Build the project**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run --project KerjaNusantara.ConsoleApp/KerjaNusantara.ConsoleApp.csproj
   ```

   **Or run the compiled executable directly (faster):**
   ```bash
   ./KerjaNusantara.ConsoleApp/bin/Debug/net9.0/KerjaNusantara.ConsoleApp
   ```

---

## 💻 Usage Guide

### Main Menu
Upon starting, you'll see three portal options:
1. **Citizen Portal** - For job seekers
2. **Company Portal** - For employers
3. **Government Portal** - For project managers

### Citizen Workflow
1. Register with NIK (16-digit ID)
2. Add skills with proficiency levels
3. View AI-powered job recommendations
4. Apply to jobs with cover letters
5. Track application status
6. View payment history

### Company Workflow
1. Register company details
2. Post job openings with skill requirements
3. Review applications with match scores
4. Browse government tenders
5. Submit bids on projects
6. Track bid status

### Government Workflow
1. Register government entity
2. Create public projects with budgets
3. Manage tender submissions
4. Award contracts to companies
5. View employment analytics dashboard

---

## 📊 Technical Highlights

### AI Matching Algorithm
- **Formula**: 70% skill match + 30% experience match
- **Skill Gap Analysis**: Identifies missing/insufficient skills
- **Training Recommendations**: Suggests improvement paths
- **Match Levels**: Highly Recommended (70%+), Good Match (50-69%), Potential (30-49%)

### Data Persistence
- **Format**: JSON files in `data/` directory
- **Files**: citizens.json, companies.json, jobs.json, applications.json, projects.json, tenderbids.json, payments.json, government.json
- **Auto-save**: All changes persist immediately

### Design Pattern Implementation

**Repository Pattern:**
```csharp
IRepository<T> → JsonRepository<T> → CitizenRepository
```

**Factory Pattern:**
```csharp
IUserFactory.CreateCitizen() → Validates & creates Citizen object
```

**Strategy Pattern:**
```csharp
IMatchingStrategy → SkillBasedMatcher (pluggable algorithms)
```

---

## 📈 Project Statistics

| Metric | Count |
|--------|-------|
| **Total Projects** | 4 |
| **Domain Models** | 22 |
| **Enums** | 5 |
| **Interfaces** | 14 |
| **Implementations** | 21 |
| **Design Patterns** | 3 |
| **Total Classes** | 68+ |
| **Lines of Code** | ~4,000+ |

---

## 🎯 Learning Outcomes

This project demonstrates:
- ✅ Advanced OOP concepts in a real-world scenario
- ✅ Design pattern implementation (Repository, Factory, Strategy)
- ✅ Clean architecture and separation of concerns
- ✅ Dependency injection and SOLID principles
- ✅ File-based data persistence
- ✅ Console UI design and user experience
- ✅ Business logic implementation
- ✅ Algorithm design (matching system)

---

## 👨‍💻 Author

**Final Project - Object-Oriented Programming Course**

---

## 📝 License

This project is created for educational purposes as part of an OOP course final project.

---

## 🙏 Acknowledgments

- Inspired by Indonesia's employment ecosystem
- Built with .NET 9.0 and C#
- Uses Microsoft.Extensions.DependencyInjection for DI

---

**Note**: This is a console application designed to demonstrate OOP principles and design patterns. It uses JSON file storage for simplicity and educational purposes.

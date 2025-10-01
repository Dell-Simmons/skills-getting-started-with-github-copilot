# NopCommerce 4.8 Product Name Modifier Plugin - Architecture

## Plugin Structure

```
Nop.Plugin.Misc.ProductNameModifier/
│
├── Plugin Core
│   ├── plugin.json                           # Plugin metadata and descriptor
│   ├── ProductNameModifierPlugin.cs          # Main plugin class (install/uninstall)
│   ├── ProductNameModifierSettings.cs        # Configuration settings
│   └── Nop.Plugin.Misc.ProductNameModifier.csproj  # Project build file
│
├── Domain Layer (Entities)
│   └── Domain/
│       └── ProductNameOverride.cs            # Entity for custom product names
│
├── Data Layer (Database)
│   └── Data/
│       ├── ProductNameOverrideBuilder.cs     # EF Core entity configuration
│       └── SchemaMigration.cs                # Database migration
│
├── Service Layer (Business Logic)
│   └── Services/
│       ├── IProductNameModifierService.cs    # Service interface
│       └── ProductNameModifierService.cs     # Service implementation
│
├── Infrastructure (Configuration)
│   └── Infrastructure/
│       ├── DependencyRegistrar.cs            # Dependency injection setup
│       └── RouteProvider.cs                  # MVC route configuration
│
└── Presentation Layer (UI)
    ├── Controllers/
    │   └── ProductNameModifierController.cs  # Admin controller
    ├── Models/
    │   └── ConfigurationModel.cs             # View model
    └── Views/
        ├── Configure.cshtml                  # Configuration page view
        └── _ViewImports.cshtml               # Razor imports

```

## Component Interaction Flow

```
┌─────────────────────────────────────────────────────────────┐
│                    Admin User Interface                      │
│                 (Configure.cshtml View)                       │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ▼
┌─────────────────────────────────────────────────────────────┐
│              ProductNameModifierController                   │
│  • Configure() - Display settings                            │
│  • Configure(model) - Save settings                          │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ▼
┌─────────────────────────────────────────────────────────────┐
│             ISettingService (nopCommerce)                    │
│  • LoadSettingAsync<ProductNameModifierSettings>()           │
│  • SaveSettingAsync(settings)                                │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ▼
┌─────────────────────────────────────────────────────────────┐
│          ProductNameModifierSettings Storage                 │
│  • Enabled: bool                                             │
│  • Prefix: string                                            │
│  • Suffix: string                                            │
│  • ApplyToAllProducts: bool                                  │
└─────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────┐
│                  Product Display Flow                        │
└─────────────────────────────────────────────────────────────┘
                        │
                        ▼
┌─────────────────────────────────────────────────────────────┐
│         IProductNameModifierService                          │
│  • GetModifiedProductNameAsync(productId, originalName)      │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ├──────────────────────┐
                        ▼                      ▼
        ┌───────────────────────┐  ┌──────────────────────────┐
        │ Check Settings        │  │ Check Product Override    │
        │ (Prefix/Suffix)       │  │ (Custom Name)             │
        └───────────────────────┘  └──────────────────────────┘
                        │                      │
                        └──────────┬───────────┘
                                   ▼
                        ┌──────────────────────┐
                        │ Return Modified Name │
                        └──────────────────────┘
```

## Database Schema

```
┌─────────────────────────────────────┐
│     ProductNameOverride Table       │
├─────────────────────────────────────┤
│ Id            INT         (PK)      │
│ ProductId     INT         (FK)      │
│ CustomName    NVARCHAR(400)         │
│ IsActive      BIT                   │
└─────────────────────────────────────┘
         │
         │ Foreign Key
         ▼
┌─────────────────────────────────────┐
│      Product Table (Core)           │
│  (Existing nopCommerce table)       │
└─────────────────────────────────────┘
```

## Dependency Injection Flow

```
Application Startup
        │
        ▼
DependencyRegistrar.Register()
        │
        ├─ Register IProductNameModifierService
        │       └─► ProductNameModifierService (Implementation)
        │
        └─ InstancePerLifetimeScope (Scoped Lifetime)


Controller/Service Request
        │
        ▼
Autofac Container
        │
        ├─ Resolve IProductNameModifierService
        │       └─► Returns ProductNameModifierService instance
        │
        ├─ Inject IRepository<ProductNameOverride>
        │
        └─ Inject ISettingService
```

## Plugin Lifecycle

```
1. Installation
   ├─ ProductNameModifierPlugin.InstallAsync()
   ├─ Create default settings
   ├─ Add localization resources
   ├─ Run database migrations (SchemaMigration)
   └─ Create ProductNameOverride table

2. Configuration
   ├─ Navigate to admin panel
   ├─ Load Configure.cshtml view
   ├─ Display ProductNameModifierSettings
   ├─ Save updated settings
   └─ Persist to database

3. Usage
   ├─ Product name requested
   ├─ IProductNameModifierService.GetModifiedProductNameAsync()
   ├─ Check if enabled
   ├─ Check for product-specific override
   ├─ Apply global prefix/suffix if configured
   └─ Return modified name

4. Uninstallation
   ├─ ProductNameModifierPlugin.UninstallAsync()
   ├─ Delete settings
   ├─ Delete localization resources
   └─ Drop ProductNameOverride table (via migration rollback)
```

## Key Features Implementation

### Feature 1: Global Prefix/Suffix
```csharp
// When ApplyToAllProducts = true and no override exists
modifiedName = Prefix + originalName + Suffix
```

### Feature 2: Product-Specific Override
```csharp
// When override exists and IsActive = true
if (productOverride != null && productOverride.IsActive)
    return productOverride.CustomName;
```

### Feature 3: Enable/Disable
```csharp
// Check at the beginning of modification logic
if (!settings.Enabled)
    return originalName;
```

## Integration Points with nopCommerce

1. **ISettings** - Configuration persistence
2. **IRepository<T>** - Data access
3. **BasePlugin** - Plugin lifecycle
4. **IMiscPlugin** - Plugin category
5. **BasePluginController** - Admin UI controllers
6. **FluentMigrator** - Database migrations
7. **Autofac** - Dependency injection

## Usage Example

```csharp
// Example: Modifying a product name programmatically

// 1. Inject the service
private readonly IProductNameModifierService _modifierService;

public YourClass(IProductNameModifierService modifierService)
{
    _modifierService = modifierService;
}

// 2. Get modified name
var originalName = "Sample Product";
var modifiedName = await _modifierService
    .GetModifiedProductNameAsync(productId: 123, originalName);
// Result: "NEW - Sample Product - SALE" (if prefix and suffix are set)

// 3. Create a specific override
var override = new ProductNameOverride
{
    ProductId = 123,
    CustomName = "Limited Edition Sample Product",
    IsActive = true
};
await _modifierService.SaveProductNameOverrideAsync(override);

// 4. Now the same product returns the custom name
modifiedName = await _modifierService
    .GetModifiedProductNameAsync(productId: 123, originalName);
// Result: "Limited Edition Sample Product"
```

## File Sizes and Line Counts

| File | Lines | Purpose |
|------|-------|---------|
| ProductNameModifierPlugin.cs | ~80 | Main plugin class |
| ProductNameModifierService.cs | ~100 | Service implementation |
| IProductNameModifierService.cs | ~40 | Service interface |
| ProductNameModifierController.cs | ~75 | Admin controller |
| ProductNameModifierSettings.cs | ~30 | Settings model |
| ConfigurationModel.cs | ~28 | View model |
| ProductNameOverride.cs | ~25 | Domain entity |
| ProductNameOverrideBuilder.cs | ~23 | EF configuration |
| SchemaMigration.cs | ~17 | Database migration |
| DependencyRegistrar.cs | ~27 | DI configuration |
| RouteProvider.cs | ~24 | Route configuration |
| Configure.cshtml | ~55 | Admin view |
| _ViewImports.cshtml | ~3 | View imports |
| plugin.json | ~10 | Plugin metadata |
| .csproj | ~52 | Build configuration |
| **Total** | **~589 lines** | Complete plugin |

## Technology Stack

- **.NET 8.0**: Modern cross-platform framework
- **ASP.NET Core MVC**: Web application framework
- **Entity Framework Core**: Object-relational mapping
- **FluentMigrator**: Database migration tool
- **Autofac**: IoC container
- **Razor Pages**: View engine
- **C# 12**: Programming language

## Compliance with nopCommerce Standards

✓ Follows nopCommerce naming conventions  
✓ Uses async/await patterns  
✓ Implements proper dependency injection  
✓ Includes localization support  
✓ Provides admin configuration UI  
✓ Uses nopCommerce data access patterns  
✓ Includes proper plugin metadata  
✓ Implements installation/uninstallation logic  
✓ Uses FluentMigrator for database changes  
✓ Follows MVC pattern  

## Version Compatibility

| Component | Version |
|-----------|---------|
| nopCommerce | 4.80 |
| .NET | 8.0 |
| Entity Framework Core | As per nopCommerce |
| ASP.NET Core MVC | As per nopCommerce |

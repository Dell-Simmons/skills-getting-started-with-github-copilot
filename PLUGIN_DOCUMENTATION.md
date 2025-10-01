# NopCommerce 4.8 Product Name Modifier Plugin

## Plugin Overview
This repository now contains a complete nopCommerce 4.8 plugin for modifying product names. The plugin provides flexible options for customizing how product names are displayed in your e-commerce store.

## What's Included

### Core Plugin Files
- **plugin.json** - Plugin descriptor with metadata and version information
- **ProductNameModifierPlugin.cs** - Main plugin class with install/uninstall logic
- **ProductNameModifierSettings.cs** - Configuration settings model
- **Nop.Plugin.Misc.ProductNameModifier.csproj** - Project file for building the plugin

### Domain Layer
- **Domain/ProductNameOverride.cs** - Entity for storing custom product name overrides

### Data Layer
- **Data/ProductNameOverrideBuilder.cs** - Entity configuration for database mapping
- **Data/SchemaMigration.cs** - Database migration for creating the plugin's table

### Service Layer
- **Services/IProductNameModifierService.cs** - Service interface
- **Services/ProductNameModifierService.cs** - Service implementation for managing product name modifications

### Infrastructure
- **Infrastructure/DependencyRegistrar.cs** - Dependency injection configuration
- **Infrastructure/RouteProvider.cs** - MVC route configuration

### Presentation Layer
- **Controllers/ProductNameModifierController.cs** - Admin controller for plugin configuration
- **Models/ConfigurationModel.cs** - View model for configuration page
- **Views/Configure.cshtml** - Admin configuration page view
- **Views/_ViewImports.cshtml** - View imports for Razor pages

### Documentation
- **README.md** - Comprehensive plugin documentation

## Key Features

1. **Global Prefix/Suffix**: Add a prefix or suffix to all product names
2. **Product-Specific Overrides**: Override individual product names with custom text
3. **Enable/Disable Toggle**: Easy on/off switch for the entire plugin
4. **Admin Configuration UI**: User-friendly admin panel for managing settings
5. **Database Persistence**: Custom names stored in database
6. **Service-Based Architecture**: Clean, testable service layer

## Installation in a nopCommerce Store

To use this plugin in an actual nopCommerce 4.8 installation:

1. Copy the `Nop.Plugin.Misc.ProductNameModifier` folder to your nopCommerce installation's `Plugins` directory
2. The typical path would be: `[nopCommerce Root]/Plugins/Nop.Plugin.Misc.ProductNameModifier/`
3. Restart your nopCommerce application (or recycle the IIS application pool)
4. Log in to the admin panel
5. Navigate to Configuration → Local Plugins
6. Find "Product Name Modifier" in the plugin list
7. Click "Install"
8. After installation, click "Configure" to set up the plugin

## Configuration Options

### Admin Settings Panel
- **Enabled**: Toggle the plugin on/off
- **Prefix**: Text to prepend to product names (e.g., "NEW - ")
- **Suffix**: Text to append to product names (e.g., " - SALE")
- **Apply to All Products**: When enabled, prefix/suffix applies globally

## Technical Architecture

### Technology Stack
- **.NET 8.0**: Target framework
- **ASP.NET Core MVC**: Web framework
- **Entity Framework Core**: ORM for data access
- **FluentMigrator**: Database migrations
- **Autofac**: Dependency injection

### Design Patterns
- Repository Pattern (via nopCommerce's IRepository)
- Service Layer Pattern
- Dependency Injection
- MVC Pattern

### Database Schema
The plugin creates one table:
```sql
ProductNameOverride
├── Id (int, PK)
├── ProductId (int, FK to Product)
├── CustomName (nvarchar(400))
└── IsActive (bit)
```

## Usage Examples

### Example 1: Add "NEW" Prefix to All Products
```
1. Navigate to Admin → Configuration → Local Plugins
2. Configure Product Name Modifier:
   - Enabled: ✓
   - Prefix: "NEW - "
   - Apply to All Products: ✓
3. Save
Result: "Product A" becomes "NEW - Product A"
```

### Example 2: Custom Name for Specific Product
```csharp
// Inject the service
private readonly IProductNameModifierService _modifierService;

// Create an override
var override = new ProductNameOverride
{
    ProductId = 123,
    CustomName = "Special Edition Product",
    IsActive = true
};

await _modifierService.SaveProductNameOverrideAsync(override);
```

## Development Notes

This plugin follows nopCommerce 4.8 conventions:
- Uses async/await patterns throughout
- Implements ISettings for configuration
- Uses IMiscPlugin interface
- Follows nopCommerce naming conventions
- Includes proper dependency injection setup

## Testing the Plugin

Since this is a standalone plugin without the full nopCommerce framework:
1. The plugin requires a complete nopCommerce 4.8 installation to build and test
2. Place it in the Plugins folder of a nopCommerce installation
3. Build the solution to compile the plugin
4. Test in the admin panel after installation

## Future Enhancements

Potential improvements for future versions:
- Bulk import/export of product name overrides
- Schedule-based name changes (e.g., sale names during promotions)
- Category-level name modifications
- Support for multiple languages
- Product name templates with variables
- Preview functionality before applying changes

## Version Information
- **Plugin Version**: 1.0
- **nopCommerce Version**: 4.8.0
- **.NET Version**: 8.0

## License
This plugin follows nopCommerce licensing terms.

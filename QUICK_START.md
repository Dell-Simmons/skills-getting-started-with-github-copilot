# Quick Start Guide - Product Name Modifier Plugin

## Overview
This guide will help you quickly understand and deploy the nopCommerce 4.8 Product Name Modifier plugin.

## What Does This Plugin Do?

The plugin allows you to modify how product names appear in your store by:
1. Adding a **prefix** to product names (e.g., "NEW - Product Name")
2. Adding a **suffix** to product names (e.g., "Product Name - SALE")
3. Setting **custom names** for specific products
4. Enabling/disabling modifications with a simple toggle

## Prerequisites

- nopCommerce 4.8.0 or higher installed and running
- Admin access to your nopCommerce store
- .NET 8.0 SDK (for building from source)

## Installation Steps

### Step 1: Copy Plugin to nopCommerce
```bash
# Navigate to your nopCommerce installation
cd /path/to/your/nopcommerce

# Copy the plugin folder to the Plugins directory
cp -r /path/to/Nop.Plugin.Misc.ProductNameModifier ./Plugins/
```

### Step 2: Restart Application
- If using IIS: Recycle the application pool
- If using Kestrel: Restart the application
- Or simply restart the web server

### Step 3: Install via Admin Panel
1. Log in to your nopCommerce admin panel
2. Navigate to: **Configuration → Local Plugins**
3. Find "Product Name Modifier" in the list
4. Click the **Install** button
5. Wait for installation to complete
6. Click **Restart application** when prompted

### Step 4: Configure the Plugin
1. After restart, go back to: **Configuration → Local Plugins**
2. Find "Product Name Modifier"
3. Click **Configure**
4. Set your desired options (see Configuration section below)
5. Click **Save**

## Configuration Options

### Basic Settings

| Setting | Type | Description | Example |
|---------|------|-------------|---------|
| **Enabled** | Checkbox | Turn the plugin on/off | ☑ Enabled |
| **Prefix** | Text | Text to add before product names | "NEW - " |
| **Suffix** | Text | Text to add after product names | " - SALE" |
| **Apply to All Products** | Checkbox | Apply prefix/suffix globally | ☑ Apply |

### Configuration Examples

#### Example 1: Sale Banner
```
Enabled: ☑
Prefix: 🔥 SALE: 
Suffix: (empty)
Apply to All Products: ☑

Result: "🔥 SALE: Laptop Computer"
```

#### Example 2: New Arrivals
```
Enabled: ☑
Prefix: NEW - 
Suffix: ⭐
Apply to All Products: ☑

Result: "NEW - Laptop Computer ⭐"
```

#### Example 3: Seasonal Promotion
```
Enabled: ☑
Prefix: (empty)
Suffix:  - Holiday Special
Apply to All Products: ☑

Result: "Laptop Computer - Holiday Special"
```

## Usage Scenarios

### Scenario 1: Store-Wide Sale
**Goal**: Add "SALE" prefix to all products  
**Steps**:
1. Configure → Enable plugin
2. Set Prefix: "SALE - "
3. Enable "Apply to All Products"
4. Save

### Scenario 2: New Product Launch
**Goal**: Highlight specific new products  
**Steps**:
1. Configure → Enable plugin
2. Use the service API to set custom names for specific products
3. See "Developer Usage" section below

### Scenario 3: Seasonal Branding
**Goal**: Add holiday-themed suffix temporarily  
**Steps**:
1. Configure → Enable plugin
2. Set Suffix: " 🎄 Holiday Special"
3. Enable "Apply to All Products"
4. Save
5. After holiday: Disable plugin or remove suffix

## Developer Usage

### Using the Service in Code

```csharp
using Nop.Plugin.Misc.ProductNameModifier.Services;
using Nop.Plugin.Misc.ProductNameModifier.Domain;

public class YourService
{
    private readonly IProductNameModifierService _modifierService;

    public YourService(IProductNameModifierService modifierService)
    {
        _modifierService = modifierService;
    }

    // Get modified product name
    public async Task<string> GetProductDisplayName(int productId, string originalName)
    {
        return await _modifierService.GetModifiedProductNameAsync(productId, originalName);
    }

    // Set custom name for a specific product
    public async Task SetCustomProductName(int productId, string customName)
    {
        var override = new ProductNameOverride
        {
            ProductId = productId,
            CustomName = customName,
            IsActive = true
        };
        
        await _modifierService.SaveProductNameOverrideAsync(override);
    }

    // Remove custom name
    public async Task RemoveCustomProductName(int productId)
    {
        var override = await _modifierService.GetProductNameOverrideByProductIdAsync(productId);
        if (override != null)
        {
            await _modifierService.DeleteProductNameOverrideAsync(override);
        }
    }
}
```

### Dependency Injection
The service is automatically registered and can be injected anywhere:

```csharp
// Constructor injection
public MyController(IProductNameModifierService modifierService)
{
    _modifierService = modifierService;
}

// Or in a service class
public MyService(IProductNameModifierService modifierService)
{
    _modifierService = modifierService;
}
```

## Troubleshooting

### Plugin doesn't appear in admin panel
- **Solution**: Ensure the plugin folder is in the correct location: `[nopCommerce]/Plugins/Nop.Plugin.Misc.ProductNameModifier/`
- Restart the application

### Changes don't appear on storefront
- **Solution 1**: Clear nopCommerce cache (Configuration → Settings → All Settings → Clear Cache)
- **Solution 2**: Check if plugin is enabled in configuration
- **Solution 3**: Verify "Apply to All Products" is checked if using prefix/suffix

### Build errors
- **Solution**: Ensure you have the correct nopCommerce version (4.8.0 or higher)
- Check that all nopCommerce dependencies are properly referenced in the .csproj file

### Database errors during installation
- **Solution**: Ensure your database user has permissions to create tables
- Check the error log in the admin panel for specific details

## Uninstallation

1. Navigate to: **Configuration → Local Plugins**
2. Find "Product Name Modifier"
3. Click **Uninstall**
4. Confirm the action
5. The plugin will:
   - Remove all settings
   - Delete the ProductNameOverride table
   - Remove localization resources

## Best Practices

### Performance
- The service uses efficient database queries
- Results are retrieved with minimal overhead
- Consider caching if calling frequently in custom code

### Data Management
- Product overrides are stored separately from products
- Original product names remain unchanged
- You can enable/disable without losing data

### Testing
- Test changes on a staging environment first
- Use the configuration page to quickly preview settings
- Disable the plugin to immediately revert to original names

## Advanced Features

### Bulk Operations
If you need to set custom names for many products, you can create a simple script:

```csharp
public async Task BulkSetPrefixForCategory(int categoryId, string prefix)
{
    var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
    
    foreach (var product in products)
    {
        var override = new ProductNameOverride
        {
            ProductId = product.Id,
            CustomName = prefix + product.Name,
            IsActive = true
        };
        
        await _modifierService.SaveProductNameOverrideAsync(override);
    }
}
```

### Integration with Other Plugins
The service interface can be injected into other plugins or custom code, making it easy to integrate with your existing functionality.

## Support and Resources

### Documentation Files
- `README.md` - Plugin-specific documentation
- `PLUGIN_DOCUMENTATION.md` - Comprehensive repository documentation
- `ARCHITECTURE.md` - Technical architecture details

### nopCommerce Resources
- [nopCommerce Official Documentation](https://docs.nopcommerce.com/)
- [nopCommerce Forums](https://www.nopcommerce.com/boards/)
- [nopCommerce GitHub](https://github.com/nopSolutions/nopCommerce)

## Version History

- **v1.0** - Initial release
  - Global prefix/suffix support
  - Product-specific overrides
  - Admin configuration UI
  - nopCommerce 4.8.0 compatibility

## License
This plugin is provided for use with nopCommerce 4.8.0 and follows nopCommerce licensing terms.

---

## Quick Reference Card

```
┌─────────────────────────────────────────────────┐
│         QUICK REFERENCE                         │
├─────────────────────────────────────────────────┤
│ INSTALL: Configuration → Local Plugins          │
│ CONFIGURE: Click "Configure" after install      │
│ ENABLE: Check "Enabled" checkbox                │
│ PREFIX: Add text before product names           │
│ SUFFIX: Add text after product names            │
│ APPLY: Check "Apply to All Products"            │
│ SAVE: Click "Save" button                       │
│ DISABLE: Uncheck "Enabled" checkbox             │
│ UNINSTALL: Click "Uninstall" in plugins list    │
└─────────────────────────────────────────────────┘
```

---

**Need Help?** Check the troubleshooting section or review the detailed architecture documentation.

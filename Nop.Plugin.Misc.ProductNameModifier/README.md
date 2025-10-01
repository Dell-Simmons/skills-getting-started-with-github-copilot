# Product Name Modifier Plugin for nopCommerce 4.8

## Overview
This plugin allows you to modify product names in your nopCommerce 4.8 store. It provides the following capabilities:

- Add prefix to product names
- Add suffix to product names
- Override specific product names with custom text
- Enable/disable modifications globally

## Features

### Global Modifications
- **Prefix**: Add a text prefix to all product names (e.g., "NEW - Product Name")
- **Suffix**: Add a text suffix to all product names (e.g., "Product Name - SALE")
- **Apply to All Products**: Toggle to apply prefix/suffix globally

### Individual Product Overrides
- Override specific product names with completely custom text
- Enable/disable overrides per product
- Overrides take precedence over global prefix/suffix settings

## Installation

1. Copy the plugin folder to your nopCommerce `Plugins` directory
2. Restart your application
3. Navigate to Admin → Configuration → Local Plugins
4. Find "Product Name Modifier" in the list
5. Click "Install"
6. Click "Configure" to set up the plugin

## Configuration

### Admin Configuration
1. Navigate to Admin → Configuration → Local Plugins
2. Find "Product Name Modifier" and click "Configure"
3. Configure the following settings:
   - **Enabled**: Enable or disable the plugin
   - **Prefix**: Text to add before product names
   - **Suffix**: Text to add after product names
   - **Apply to All Products**: Enable to apply prefix/suffix to all products

### Database Schema
The plugin creates the following table:
- `ProductNameOverride`: Stores custom product name overrides
  - `Id`: Primary key
  - `ProductId`: Reference to the product
  - `CustomName`: The custom name to display
  - `IsActive`: Whether the override is active

## Usage Examples

### Example 1: Add Prefix to All Products
1. Enable the plugin
2. Set Prefix to "NEW - "
3. Enable "Apply to All Products"
4. Result: All products will display with "NEW - " prefix

### Example 2: Override Specific Product
1. Use the `IProductNameModifierService` service in your code
2. Call `SaveProductNameOverrideAsync` with a `ProductNameOverride` object
3. The specific product will display your custom name

## Technical Details

### Services
- **IProductNameModifierService**: Main service interface for managing product name modifications
  - `GetModifiedProductNameAsync`: Get the modified name for a product
  - `SaveProductNameOverrideAsync`: Save a product-specific override
  - `GetProductNameOverrideByProductIdAsync`: Retrieve an override for a product
  - `DeleteProductNameOverrideAsync`: Delete a product override

### Architecture
- Uses nopCommerce 4.8 plugin architecture
- Implements dependency injection via Autofac
- Uses Entity Framework Core for data access
- Follows nopCommerce coding standards and conventions

## Version History
- **1.0**: Initial release for nopCommerce 4.8

## Support
For issues and questions, please contact your system administrator or nopCommerce support.

## License
This plugin is provided as-is for nopCommerce 4.8 implementations.

# skills-getting-started-with-github-copilot
Exercise: Get started using GitHub Copilot

## NopCommerce 4.8 Product Name Modifier Plugin

This repository contains a complete nopCommerce 4.8 plugin for modifying product names in your e-commerce store.

### Features
- ✅ Add prefix to product names
- ✅ Add suffix to product names
- ✅ Custom product-specific name overrides
- ✅ Enable/disable functionality with a toggle
- ✅ Admin configuration interface
- ✅ Database persistence for custom names
- ✅ Service-based architecture

### Quick Links
- 📖 [Quick Start Guide](QUICK_START.md) - Get up and running fast
- 📚 [Plugin Documentation](PLUGIN_DOCUMENTATION.md) - Comprehensive documentation
- 🏗️ [Architecture Overview](ARCHITECTURE.md) - Technical details and diagrams
- 📁 [Plugin Source](Nop.Plugin.Misc.ProductNameModifier/) - Browse the source code

### Installation
1. Copy the `Nop.Plugin.Misc.ProductNameModifier` folder to your nopCommerce `Plugins` directory
2. Restart your nopCommerce application
3. Navigate to Admin → Configuration → Local Plugins
4. Install and configure the plugin

For detailed instructions, see the [Quick Start Guide](QUICK_START.md).

### Plugin Structure
```
Nop.Plugin.Misc.ProductNameModifier/
├── Controllers/          # Admin controllers
├── Data/                 # Database migrations and mappings
├── Domain/               # Entities
├── Infrastructure/       # DI and routing configuration
├── Models/               # View models
├── Services/             # Business logic services
├── Views/                # Razor views
└── plugin.json           # Plugin metadata
```

### Requirements
- nopCommerce 4.8.0 or higher
- .NET 8.0
- SQL Server (or compatible database)

### Documentation
See [PLUGIN_DOCUMENTATION.md](PLUGIN_DOCUMENTATION.md) for complete documentation including:
- Installation instructions
- Configuration options
- Usage examples
- API reference
- Development notes

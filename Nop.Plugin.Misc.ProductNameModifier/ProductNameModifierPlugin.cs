using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.ProductNameModifier.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.ProductNameModifier
{
    /// <summary>
    /// Product Name Modifier plugin
    /// </summary>
    public class ProductNameModifierPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public ProductNameModifierPlugin(
            IWebHelper webHelper,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/ProductNameModifier/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override async Task InstallAsync()
        {
            // Settings
            await _settingService.SaveSettingAsync(new ProductNameModifierSettings
            {
                Enabled = false,
                Prefix = string.Empty,
                Suffix = string.Empty,
                ApplyToAllProducts = false
            });

            // Locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.ProductNameModifier.Fields.Enabled"] = "Enabled",
                ["Plugins.Misc.ProductNameModifier.Fields.Enabled.Hint"] = "Enable or disable the product name modifier",
                ["Plugins.Misc.ProductNameModifier.Fields.Prefix"] = "Prefix",
                ["Plugins.Misc.ProductNameModifier.Fields.Prefix.Hint"] = "Prefix to add to product names",
                ["Plugins.Misc.ProductNameModifier.Fields.Suffix"] = "Suffix",
                ["Plugins.Misc.ProductNameModifier.Fields.Suffix.Hint"] = "Suffix to add to product names",
                ["Plugins.Misc.ProductNameModifier.Fields.ApplyToAllProducts"] = "Apply to all products",
                ["Plugins.Misc.ProductNameModifier.Fields.ApplyToAllProducts.Hint"] = "Apply prefix/suffix to all products"
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override async Task UninstallAsync()
        {
            // Settings
            await _settingService.DeleteSettingAsync<ProductNameModifierSettings>();

            // Locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Misc.ProductNameModifier");

            await base.UninstallAsync();
        }
    }
}

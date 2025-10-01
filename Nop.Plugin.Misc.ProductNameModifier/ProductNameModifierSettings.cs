using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.ProductNameModifier
{
    /// <summary>
    /// Represents settings for product name modifier plugin
    /// </summary>
    public class ProductNameModifierSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to enable the plugin
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the prefix to add to product names
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix to add to product names
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to apply to all products
        /// </summary>
        public bool ApplyToAllProducts { get; set; }
    }
}

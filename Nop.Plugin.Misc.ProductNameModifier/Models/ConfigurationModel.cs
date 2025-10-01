using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.ProductNameModifier.Models
{
    /// <summary>
    /// Represents configuration model for product name modifier
    /// </summary>
    public class ConfigurationModel
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

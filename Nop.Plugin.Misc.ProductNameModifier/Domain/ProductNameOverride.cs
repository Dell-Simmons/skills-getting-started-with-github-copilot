using Nop.Core;

namespace Nop.Plugin.Misc.ProductNameModifier.Domain
{
    /// <summary>
    /// Represents a product name override
    /// </summary>
    public class ProductNameOverride : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the custom product name
        /// </summary>
        public string CustomName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the override is active
        /// </summary>
        public bool IsActive { get; set; }
    }
}

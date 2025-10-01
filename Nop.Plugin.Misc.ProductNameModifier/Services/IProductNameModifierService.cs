using System.Threading.Tasks;
using Nop.Plugin.Misc.ProductNameModifier.Domain;

namespace Nop.Plugin.Misc.ProductNameModifier.Services
{
    /// <summary>
    /// Product name modifier service interface
    /// </summary>
    public interface IProductNameModifierService
    {
        /// <summary>
        /// Gets the modified product name
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="originalName">Original product name</param>
        /// <returns>Modified product name</returns>
        Task<string> GetModifiedProductNameAsync(int productId, string originalName);

        /// <summary>
        /// Saves a product name override
        /// </summary>
        /// <param name="productNameOverride">Product name override</param>
        Task SaveProductNameOverrideAsync(ProductNameOverride productNameOverride);

        /// <summary>
        /// Gets a product name override by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product name override</returns>
        Task<ProductNameOverride> GetProductNameOverrideByProductIdAsync(int productId);

        /// <summary>
        /// Deletes a product name override
        /// </summary>
        /// <param name="productNameOverride">Product name override</param>
        Task DeleteProductNameOverrideAsync(ProductNameOverride productNameOverride);
    }
}

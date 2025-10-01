using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.ProductNameModifier.Domain;
using Nop.Services.Configuration;

namespace Nop.Plugin.Misc.ProductNameModifier.Services
{
    /// <summary>
    /// Product name modifier service implementation
    /// </summary>
    public class ProductNameModifierService : IProductNameModifierService
    {
        private readonly IRepository<ProductNameOverride> _productNameOverrideRepository;
        private readonly ISettingService _settingService;

        public ProductNameModifierService(
            IRepository<ProductNameOverride> productNameOverrideRepository,
            ISettingService settingService)
        {
            _productNameOverrideRepository = productNameOverrideRepository;
            _settingService = settingService;
        }

        /// <summary>
        /// Gets the modified product name
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="originalName">Original product name</param>
        /// <returns>Modified product name</returns>
        public virtual async Task<string> GetModifiedProductNameAsync(int productId, string originalName)
        {
            var settings = await _settingService.LoadSettingAsync<ProductNameModifierSettings>();

            if (!settings.Enabled)
                return originalName;

            // Check for specific product override
            var productOverride = await GetProductNameOverrideByProductIdAsync(productId);
            if (productOverride != null && productOverride.IsActive)
                return productOverride.CustomName;

            // Apply prefix/suffix if configured
            if (settings.ApplyToAllProducts)
            {
                var modifiedName = originalName;
                if (!string.IsNullOrEmpty(settings.Prefix))
                    modifiedName = settings.Prefix + modifiedName;
                if (!string.IsNullOrEmpty(settings.Suffix))
                    modifiedName = modifiedName + settings.Suffix;
                return modifiedName;
            }

            return originalName;
        }

        /// <summary>
        /// Saves a product name override
        /// </summary>
        /// <param name="productNameOverride">Product name override</param>
        public virtual async Task SaveProductNameOverrideAsync(ProductNameOverride productNameOverride)
        {
            if (productNameOverride == null)
                return;

            if (productNameOverride.Id == 0)
                await _productNameOverrideRepository.InsertAsync(productNameOverride);
            else
                await _productNameOverrideRepository.UpdateAsync(productNameOverride);
        }

        /// <summary>
        /// Gets a product name override by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product name override</returns>
        public virtual async Task<ProductNameOverride> GetProductNameOverrideByProductIdAsync(int productId)
        {
            if (productId == 0)
                return null;

            var query = from pno in _productNameOverrideRepository.Table
                        where pno.ProductId == productId
                        select pno;

            return await Task.FromResult(query.FirstOrDefault());
        }

        /// <summary>
        /// Deletes a product name override
        /// </summary>
        /// <param name="productNameOverride">Product name override</param>
        public virtual async Task DeleteProductNameOverrideAsync(ProductNameOverride productNameOverride)
        {
            await _productNameOverrideRepository.DeleteAsync(productNameOverride);
        }
    }
}

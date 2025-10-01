using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.ProductNameModifier.Domain;

namespace Nop.Plugin.Misc.ProductNameModifier.Data
{
    /// <summary>
    /// Represents a product name override entity builder
    /// </summary>
    public class ProductNameOverrideBuilder : NopEntityBuilder<ProductNameOverride>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(ProductNameOverride.ProductId)).AsInt32().NotNullable()
                .WithColumn(nameof(ProductNameOverride.CustomName)).AsString(400).NotNullable()
                .WithColumn(nameof(ProductNameOverride.IsActive)).AsBoolean().NotNullable();
        }
    }
}

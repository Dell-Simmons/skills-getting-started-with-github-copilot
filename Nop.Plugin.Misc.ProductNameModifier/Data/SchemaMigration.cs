using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.ProductNameModifier.Domain;

namespace Nop.Plugin.Misc.ProductNameModifier.Data
{
    [NopMigration("2024/01/01 00:00:00", "Misc.ProductNameModifier base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            Create.TableFor<ProductNameOverride>();
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TrackerEnabledDbContext.Common.Configuration
{
    //https://stackoverflow.com/questions/2958921/entity-framework-4-how-to-find-the-primary-key
    //https://stackoverflow.com/questions/30688909/how-to-get-primary-key-value-with-entity-framework-core
    internal static class DbContextExtensions
    {
        public static IEnumerable<PropertyConfigurationKey> GetKeyNames<TEntity>(this DbContext context)
            where TEntity : class
        {
            return context.GetKeyNames(typeof(TEntity));
        }

        public static IEnumerable<PropertyConfigurationKey> GetKeyNames(this DbContext context, Type entityType)
        {
            var primaryKey = context.Model.FindEntityType(entityType.GetType()).FindPrimaryKey();

            if (primaryKey == null)
                return Enumerable.Empty<PropertyConfigurationKey>();

            context.Model.FindEntityType(entityType.GetType()).FindPrimaryKey().Properties.Select(x => x.Name);

            return primaryKey.Properties.Select(x => new PropertyConfigurationKey(x.Name, entityType.FullName));
        }
    }
}

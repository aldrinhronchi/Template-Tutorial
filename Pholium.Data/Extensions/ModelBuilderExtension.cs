using Microsoft.EntityFrameworkCore;
using Pholium.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Pholium.Domain.Models;

namespace Pholium.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                    new User { ID = Guid.Parse("b25fca61-2f00-472a-bcb0-b87e7080b255"), Name = "User Default", Email = "userdefault@template.com", DateCreated = new DateTime(2022,2,2), IsDeleted = false, DateUpdated = null }
                );
            return builder;
        }
        public static ModelBuilder ApplyGlobalConfigurations(this ModelBuilder builder)
        {
            foreach(IMutableEntityType? entityType in builder.Model.GetEntityTypes())
            {
                foreach(IMutableProperty? property in entityType.GetProperties())
                {
                    switch(property.Name)
                    {
                        case nameof(Entity.ID):
                            property.IsKey();
                            break;
                        case nameof(Entity.DateUpdated):
                            property.IsNullable = true;
                            break;
                        case nameof(Entity.DateCreated):
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        case nameof(Entity.IsDeleted):
                            property.IsNullable = false;
                            property.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }
            return builder;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Pholium.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pholium.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                    new User { ID = Guid.Parse("b25fca61-2f00-472a-bcb0-b87e7080b255"), Name = "User Default", Email = "userdefault@template.com" }
                );
            return builder;
        }
    }
}

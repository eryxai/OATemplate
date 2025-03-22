using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
using TemplateService.DataAccess.Contexts;
using TemplateService.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateService.DataAccess.Mappings
{
    public class ExperienceMap : IEntityTypeConfiguration<Experience>
    {
        public static List<Experience> Experiences;
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            // builder.ToTable($"{typeof(Announcement).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.NameEn).HasMaxLength(150).IsRequired();
            builder.Property(x=>x.NameAr).HasMaxLength(150).IsRequired();



            #region Set Initial Data
            this.Seed(builder);
            #endregion
        }
        private void Seed(EntityTypeBuilder<Experience> builder)
        {
            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {
                Experiences = new List<Experience>
                {
                    new Experience
                    {
                        Id = 1,
                        NameEn = "Hospitaltly",
                        NameAr = "Hospitaltly",
                        CreationDate = DateTime.Now
                    },
                    new Experience
                    {
                        Id = 2,
                        NameEn = "Hotels",
                        NameAr = "Hotels",
                        CreationDate = DateTime.Now,
                        ParentId = 1
                    },
                    new Experience
                    {
                        Id = 3,
                        NameEn = "Advanced",
                        NameAr = "متقدم",
                        CreationDate = DateTime.Now
                    },
                    
                };
                builder.HasData(Experiences);
            }
        }

    }
}

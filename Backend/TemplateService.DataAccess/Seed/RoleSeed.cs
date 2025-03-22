using Microsoft.EntityFrameworkCore;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateService.DataAccess.Seed
{
    public class RoleSeed
    {
        static RoleSeed()
        {
            Seed();
        }

        private static void Seed()
        {
            SeedList = new List<Role>();

            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {
                // Seed Roles
                List<Role> entityList = new List<Role>()
                {
                   new Role { Id = 1, NameAr = "مدير النظام", NameEn = "Gloable Admin"
                   , DescriptionAr = "مدير النظام", DescriptionEn = "Gloable Admin", IsDeleted = false, CreationDate = DateTime.Now 
                   
                   }
                   

                };
                
                SeedList = entityList;
            }
        }

        public static List<Role> SeedList { get; set; }
    }
}

using TemplateService.Core.Common;
using TemplateService.Core.Helper;
using TemplateService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateService.DataAccess.Seed
{
	public class UserSeed
	{

		static UserSeed()
		{
			Seed();
		}

		private static void Seed()
		{
			SeedList = new List<User>();

			if (ApplicationGlobalConfig.EnableSeedOnMigration)
			{

				DateTime now = new DateTime(2022, 9, 8, 13, 15, 24, 581, DateTimeKind.Local);


				List<User> entityList = new List<User>() {
				new User
				{
					Id = 1,
					CreationDate = now,
					NameEn = "Admin",
					NameAr = "System Admin",
					Username = "admin@eryxai.com",
					IsActive = true,
					IsDeleted = false,
					Password = HashPass.HashPassword("P@ssw0rd")
				},
				new User
				{
					Id = 2,
					CreationDate = now,
					NameEn = "User1",
					NameAr = "User1",
					Username = "user1@eryxai.com",
					IsActive = true,
					IsDeleted = false,
					Password = HashPass.HashPassword("123456")
				} ,
				//maker
				new User
				{
					Id = 3,
					CreationDate = now,
					NameEn = "maker",
					NameAr = "Maker",
					Username="maker@eryxai.com",
					IsActive = true,
					IsDeleted = false,
					Password = HashPass.HashPassword("P@ssw0rd")
				},
				//checker
				new User
                {
                    Id = 4,
                    CreationDate = now,
                    NameEn = "checker",
                    NameAr = "cheker",
                    Username="checker@eryxai.com",
                    IsActive = true,
                    IsDeleted = false,
                    Password = HashPass.HashPassword("P@ssw0rd")
                },
                };

				SeedList = entityList;
			}
		}

		public static List<User> SeedList { get; set; }
	}
}

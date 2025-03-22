#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Core.Common;
using TemplateService.DataAccess.Seed;
using TemplateService.Entity.Entities;
#endregion

/*


*/
namespace TemplateService.DataAccess.Mappings
{
	/// <summary>
	/// Provides a configuration for entity 
	/// UserPermission
	/// that will called when model created.
	/// </summary>
	public class UserPermissionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserPermission>
	{
		#region Methods

		/// <summary>
		/// Configures the entity of type TEntity.
		/// </summary>
		/// <param name="builder">
		/// The builder to be used to configure the entity type.
		/// </param>
		public void Configure(EntityTypeBuilder<UserPermission> builder)
		{
			builder.ToTable($"{typeof(UserPermission).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

			#region Configure Fields

			#endregion

			#region Configure Relations
			#endregion

			#region Ignore
			/* 
             * If do you want to initialize some data in 
             * this entity use this method: 
             * builder.Ignore(entity => entity.<PropertyName>);
             */
			#endregion

			#region Set Initial Data
			/* 
             * If do you want to initialize some data in
             * this entity use this method: 
             * builder.HasData(); 
             */
			this.Seed(builder);
			#endregion
		}

		/// <summary>
		/// Initialize databas entity with some data.
		/// </summary>
		/// <param name="builder"></param>
		private void Seed(EntityTypeBuilder<UserPermission> builder)
		{
			if (ApplicationGlobalConfig.EnableSeedOnMigration)
			{
				DateTime now = DateTime.Now;
				int idCounter = 0;
				List<UserPermission> entityList = new List<UserPermission>();

				User user = UserSeed.SeedList.FirstOrDefault();
				

			
				//entityList.AddRange(new UserPermission[]
				//{
				//new UserPermission
				//{
				//Id = 1,
				//CreationDate = now,
				//},
				//});

				builder.HasData(entityList);
			}
		}
		#endregion
	}
}

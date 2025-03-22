#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
#endregion

/*


*/
namespace TemplateService.DataAccess.Mappings
{
	/// <summary>
	/// Provides a configuration for entity 
	/// UserRole
	/// that will called when model created.
	/// </summary>
	public class UserRoleMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserRole>
	{
		#region Methods

		/// <summary>
		/// Configures the entity of type TEntity.
		/// </summary>
		/// <param name="builder">
		/// The builder to be used to configure the entity type.
		/// </param>
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable($"{typeof(UserRole).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

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
		private void Seed(EntityTypeBuilder<UserRole> builder)
		{
			if (ApplicationGlobalConfig.EnableSeedOnMigration)
			{
				DateTime now = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local);
				int idCounter = 0;
				List<UserRole> entityList = new List<UserRole>();

				entityList.AddRange(new UserRole[]
				{
				new UserRole
				{
				Id = 1,
				CreationDate = now,
				UserId =1,
				RoleId = 1,
				}
				}
                
				);

				builder.HasData(entityList);
			}
		}
		#endregion
	}
}

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
	/// UserLogin
	/// that will called when model created.
	/// </summary>
	public class UserLoginMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserLogin>
	{
		#region Methods

		/// <summary>
		/// Configures the entity of type TEntity.
		/// </summary>
		/// <param name="builder">
		/// The builder to be used to configure the entity type.
		/// </param>
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			builder.ToTable($"{typeof(UserLogin).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

			#region Configure Fields
			builder.Property(prop => prop.Username).HasMaxLength(200);
			builder.Property(prop => prop.IPV4).HasMaxLength(200);
			builder.Property(prop => prop.IPV6).HasMaxLength(200);
			builder.Property(prop => prop.ChangePassword).HasDefaultValue(false);

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
		private void Seed(EntityTypeBuilder<UserLogin> builder)
		{
			if (ApplicationGlobalConfig.EnableSeedOnMigration)
			{
				//DateTime now = DateTime.Now;
				//int idCounter = 0;
				//List<UserLogin> entityList = new List<UserLogin>();

				//entityList.AddRange(new UserLogin[]
				//{
				//new UserLogin
				//{
				//Id = 1,
				//CreationDate = now,
				//Username = "",
				//},
				//});

				//builder.HasData(entityList);
			}
		}
		#endregion
	}
}

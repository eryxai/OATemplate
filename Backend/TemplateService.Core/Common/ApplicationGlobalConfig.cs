namespace TemplateService.Core.Common
{
    public static class ApplicationGlobalConfig
	{
		static ApplicationGlobalConfig()
		{
			EnableSeedOnMigration = true;
		}

		public static bool EnableSeedOnMigration { get; set; }


		public static class Schema
		{
			public static string DefaultSchema
			{
				get { return "dbo"; }
			}

			public static string UserManagementSchema
			{
				get { return "UserManagement"; }
			}
		}
	}
}

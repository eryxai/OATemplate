#region Using ...
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Program  
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					
					webBuilder.UseIISIntegration();
					webBuilder.UseStartup<Startup>();
				});
	}
}

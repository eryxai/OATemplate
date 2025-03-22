#region Using ...
using Framework.Common.Enums;
using Framework.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Settings.Configuration;
using System;
using System.Diagnostics;

#endregion

/*
 
 
 */
namespace TemplateService.Business.Common
{
    /// <summary>
    /// Specify a functionality to 
    /// log any thing in a log.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        #region Data Members
        private readonly IHttpContextAccessor _httpContext;
        private readonly string _rootPath = "logs\\";
        private readonly ILogger _logger;
        private  IConfiguration _appConfiguration;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of 
        /// type LoggerService.
        /// </summary>
        public LoggerService(IHttpContextAccessor httpContext, IConfiguration appConfiguration)
        {
            this._httpContext = httpContext;
            this._appConfiguration = appConfiguration;

            _logger = new LoggerConfiguration().
                ReadFrom.Configuration(_appConfiguration, new ConfigurationReaderOptions { SectionName = "Serilog" })
                            //Map(

                            //le => new Tuple<DateTime, LogEventLevel>(new DateTime(le.Timestamp.Year, le.Timestamp.Month, le.Timestamp.Day), le.Level),
                            //(key, log) => log.File(path:
                            //Path.Combine(_rootPath, $"{key.Item1:yyyy-MM-dd}/{key.Item2}-.txt"),
                            //fileSizeLimitBytes: (long?)3e+8, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, flushToDiskInterval: TimeSpan.FromSeconds(60)),
                            //    sinkMapCountLimit: 1)
                            //            // add console as logging target
                            //.WriteTo.File(fileSizeLimitBytes: (long?)3e+8, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day, path: _rootPath, retainedFileCountLimit: null, flushToDiskInterval: TimeSpan.FromSeconds(60))
                            // set default minimum level
                            .MinimumLevel.Debug()
                            .CreateLogger();
            _logger.Information("startlogging");
            _logger.Debug("startlogging");
            _logger.Error("startlogging");
            _logger.Warning("startlogging");



        }
        #endregion

        #region ILoggerService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="customFileName"></param>
        public void Log(string content, LogType type, string customFileName = null)
        {
            DateTime now = DateTime.Now;

            try
            {
                //if (Directory.Exists(this._rootPath) == false)
                //	Directory.CreateDirectory(this._rootPath);

                //string yearFolderPath = $"{this._rootPath}\\{now.Year}";
                //if (Directory.Exists(yearFolderPath) == false)
                //	Directory.CreateDirectory(yearFolderPath);

                //string monthFolderPath = $"{this._rootPath}\\{now.Year}\\{now.Month}";
                //if (Directory.Exists(monthFolderPath) == false)
                //	Directory.CreateDirectory(monthFolderPath);

                //string dayFolderPath = $"{this._rootPath}\\{now.Year}\\{now.Month}\\{now.Day}";
                //if (Directory.Exists(dayFolderPath) == false)
                //	Directory.CreateDirectory(dayFolderPath);

                ////string filePath = $"{_rootPath}\\{now.Year}\\{now.Month}\\{now.Day}\\{now.ToLongTimeString().Replace(":", "-")}-{type.ToString()}.log";

                //string filePath = $"{_rootPath}\\{now.Year}\\{now.Month}\\{now.Day}";
                //string fileName = $"Logs.log";



                //if (string.IsNullOrEmpty(customFileName) == false)
                //{
                //	fileName = $"{customFileName}-{now.ToLongTimeString().Replace(":", "-")}-{type.ToString()}.log";
                //}

                //string fullPath = $"{filePath}\\{fileName}";

                //string path = $"{filePath}\\{fileName}";
                //if (!File.Exists(path))
                //{ // Create a file to write to   
                //	using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                //	{
                //		using (StreamWriter wr = new StreamWriter(fs))
                //		{
                //			wr.WriteLine("Start Loggging ");
                //			wr.Flush();
                //		}
                //	}
                //} // Open the file to read from.  

                //using (StreamWriter sw = File.AppendText(fullPath))
                //{
                //	sw.WriteLine(content);
                //	sw.Flush();

                //}
                switch (type)
                {
                    case LogType.Information:
                        _logger.Information(content);
                        break;
                    case LogType.Warning:
                        _logger.Warning(content);
                        break;
                    case LogType.Error:
                        _logger.Error(content);
                        break;
                    case LogType.Text:
                        _logger.Debug(content);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    #region Log Exception in EventLog
                    EventLog eventLog = new EventLog(this.GetType().FullName, System.Environment.MachineName);

                    eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                    #endregion
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public void LogError(string content)
        {
            this.Log(content, LogType.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void LogError(Exception ex)
        {
            this.Log(ex.ToString(), LogType.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="customFileName"></param>
        public void LogInfo(string content, string customFileName = null)
        {
            this.Log(content, LogType.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="customFileName"></param>
        public void LogText(string content, string customFileName = null)
        {
            this.Log(content, LogType.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="customFileName"></param>
        public void LogWarning(string content, string customFileName = null)
        {
            this.Log(content, LogType.Warning);
        }

        #endregion
    }
}

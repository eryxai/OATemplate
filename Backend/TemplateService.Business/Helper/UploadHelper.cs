using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TemplateService.Business.Helper
{
    public class UploadHelper
    {
        private IHostingEnvironment _environment;
        private readonly IConfiguration _appConfiguration;
        string _entity;
        //IWebHostEnvironment environment,
        public UploadHelper(IHostingEnvironment environment, IConfiguration appConfiguration, string entity)
        {
            _environment = environment;
            // _appConfiguration = environment.GetAppConfiguration();
            _entity = entity;
            _appConfiguration = appConfiguration;
        }
        public string Upload(string file, long id, string fileName)
        {
            CreateEntityIfNotExst();
            CreateEntityIdIfNotExst(id);
            string base64 = string.Empty;
            base64 = file.Substring(file.IndexOf(',') + 1);
            var data = Convert.FromBase64String(base64);
            string filePath = $"attachment\\{_entity}\\{id}\\{DateTime.Now.Millisecond.ToString()}-{fileName}";
            File.WriteAllBytes($"{_environment.WebRootPath}\\{filePath}", data);

            //   _appConfiguration["App:ServerRootAddress"] +
            filePath = filePath.Replace('\\', '/');
            return '/' + filePath;
        }

        public string Upload(string fileBase64)
        {
            try
            {
                var data = fileBase64.Split('@');
                string file = data[0];
                string base64 = string.Empty;
                base64 = file.Substring(file.IndexOf(',') + 1);
                string filePath = $"{$"attachment\\{DateTime.Now.Millisecond.ToString()}-{data[1]}"}";
                File.WriteAllBytes($"{_environment.WebRootPath}\\{filePath}", Convert.FromBase64String(base64));
                filePath = //_appConfiguration["App:ServerRootAddress"] +
                    DateTime.Now.Millisecond.ToString() + "-" + data[1];
                return '/' + filePath;

            }
            catch (Exception ex)
            {
                throw new Exception($"Upload file exception :  ", ex);
            }

        }

        private void CreateEntityIfNotExst()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(
                $"{_environment.WebRootPath}\\attachment\\{ _entity}");

            if (!directoryInfo.Exists) directoryInfo.Create();
        }

        private void CreateEntityIdIfNotExst(long id)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(
                $"{_environment.WebRootPath}\\attachment\\{ _entity}\\{id}");

            if (!directoryInfo.Exists) directoryInfo.Create();
        }

        public bool FileIsAlreadyExst(string fileName, byte[] fileData, long id)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(
                $"{_environment.WebRootPath}\\attachment\\{ _entity}\\{id}");
            bool result = false;
            if (directoryInfo.Exists)
            {
                foreach (var item in directoryInfo.GetFiles())
                {
                    if (item.Name.EndsWith(fileName))
                    {
                        Stream stream = item.OpenRead();
                        if (stream.Length == fileData.Length)
                        {
                            result = true;
                            break;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }

                }
            }
            return result;
        }

        public bool FileIsGreaterThanLimitSize(byte[] fileData)
        {
            var limitSize = int.Parse(_appConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("fileLimitSize")
                    .Value);

            return fileData.Length > limitSize;
        }


        public bool IsFileAllowedException(string fileName)
        {
            string[] exceptions = _appConfiguration.GetSection("CommonSettings").GetSection("notAllowedException").Value.Split(',');
            bool allowed = true;
            foreach (var item in exceptions)
            {
                if (fileName.EndsWith(item))
                {
                    allowed = false;
                    break;
                }
            }
            return allowed;
        }

        public bool DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

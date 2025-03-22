using Microsoft.AspNetCore.Http;

namespace TemplateService.Core.Models.ViewModels
{
    public class FileModel
    {
        public string FilePath { get; set; }
        public IFormFile File { get; set; }
    }
}

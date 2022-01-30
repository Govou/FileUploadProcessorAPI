using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadServiceWebAPI.Services
{
    public interface IFileUploader
    {
        bool CheckIfExcelFile(IFormFile file);
        Task<string> WriteFile(IFormFile file);
    }
}

using FileUploadServiceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadServiceWebAPI.Services
{
    public interface IFileProcessor
    {
        ProccesedData ExtractDataFromExcelFile(string filname);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1 . Get folder path
            //var folderPath = @"E:\\Back-End Course\\MVC\\05\\Company.Web\\Company.Web\\wwwroot\\Files\\Images\\";


            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            // 2. Get File Name

            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            // 3. Combine folder + file
            var filePath = Path.Combine(folderPath, fileName);

            // 4. save
            using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;


        }
    }
}

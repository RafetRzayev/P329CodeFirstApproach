using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.Areas.AdminPanel.Data
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            
            return false;
        }

        public static bool IsAllowedSize(this IFormFile file, double mb) 
        {
            if (file.Length > mb * 1024 * 1024)
                return false;

            return true;
        }

        public  async static Task<string> GenerateFile(this IFormFile file, string path)
        {
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            path = Path.Combine(path, fileName);

            var fs = new FileStream(path, FileMode.CreateNew);

            await file.CopyToAsync(fs);

            fs.Close();

            return fileName;
        }
    }
}

namespace P329CodeFirstApproach.Areas.AdminPanel.Data
{
    public static class FileUtils
    {
        public static bool IsImage(IFormFile file) 
        {
            if (file.ContentType.Contains("image"))
                return true;


            return false;
        }
    }
}

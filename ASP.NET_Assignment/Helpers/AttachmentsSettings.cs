namespace ASP.NET.Assignment.PL.Helpers
{
    public static class AttachmentsSettings
    {
        public static string Upload(IFormFile file, string FolderName = "Images")
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files",FolderName);
            var FileName = $"{Guid.NewGuid()}{file.FileName}";
            var FilePath = Path.Combine(FolderPath,FileName);

            using var stream = new FileStream(FilePath , FileMode.Create);

            file.CopyTo(stream);

            return FileName;
        }
        public static void Delete(string fileName , string folderName = "Images")
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName,fileName);
            var DefaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", "DefaultImage", "default.png");
            if (File.Exists(FilePath)) { 
                File.Delete(FilePath);
            }
        }
    }
}

using Users.Microservice.Services.DTOs;

namespace Users.Microservice.Services.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// This function for saving images to the wwwroot file asynchronously
        /// </summary>
        /// <param name="file"><see cref="AttachmentForCreationDto"/></param>
        /// <returns>return saved file name end this file static path</returns>
        public static async Task<(string fileName, string filePath)> SaveAsync(AttachmentForCreationDto file, bool isExist = false)
        {
            // genarate file destination
            string fileName = isExist ? file.FileName : Guid.NewGuid().ToString("N") + "-" + file.FileName;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await file.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            return (fileName, EnvironmentHelper.FilePath + "/" + fileName);
        }

        /// <summary>
        /// This founction for remove file from wwwroot by given static path which is given by function parametr
        /// </summary>
        /// <param name="staticPath">file static path</param>
        /// <returns>if file is exists and deleted successfully return true else false</returns>
        public static bool Remove(string staticPath)
        {
            string fullPath = Path.Combine(EnvironmentHelper.WebRootPath, staticPath);

            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);
            return true;
        }
    }
}

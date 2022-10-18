using Users.Microservice.Services.DTOs;

namespace Users.Microservice.Services.Extentions
{
    public static class FormFileExtentions
    {
        public static AttachmentForCreationDto ToAttachmentOrDefault(this IFormFile formFile)
        {

            if (formFile?.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AttachmentForCreationDto()
                {
                    FileName = formFile.FileName,
                    Stream = new MemoryStream(fileBytes)
                };
            }

            return null;
        }
    }
}

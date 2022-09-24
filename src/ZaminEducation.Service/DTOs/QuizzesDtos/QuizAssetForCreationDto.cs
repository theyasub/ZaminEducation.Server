using ZaminEducation.Service.DTOs.Commons;

namespace ZaminEducation.Service.DTOs.QuizzesDtos
{
    public class QuizAssetForCreationDto
    {
        public QuizContentForCreationDto Content { get; set; }

        public AttachmentForCreationDto File { get; set; }
    }
}

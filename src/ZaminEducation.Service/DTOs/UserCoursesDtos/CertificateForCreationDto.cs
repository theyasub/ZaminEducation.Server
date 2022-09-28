using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.UserCoursesDtos;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.DTOs.UserCourses
{
    public class CertificateForCreationDto
    {
        public long CourseId { get; set; }

        public long UserId { get; set; }

        public CertificateResultDto Result { get; set; }
    }
}

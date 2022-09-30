namespace ZaminEducation.Service.DTOs.UserCourses
{
    public class CourseCommentForCreationDto
    {
        public string Text { get; set; }

        public long UserId { get; set; }

        public long CourseId { get; set; }

        public long ParentId { get; set; }
    }
}


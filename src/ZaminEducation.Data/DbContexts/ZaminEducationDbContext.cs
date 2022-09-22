
using Microsoft.EntityFrameworkCore;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.Quizzes;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Domain.Entities.Users;

namespace ZaminEducation.Data.DbContexts
{
    public class ZaminEducationDbContext : DbContext
    {
        public ZaminEducationDbContext(DbContextOptions<ZaminEducationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<CourseModule> CourseModules { get; set; }
        public virtual DbSet<CourseTarget> CourseTargets { get; set; }
        public virtual DbSet<CourseVideo> CourseVideos { get; set; }
        public virtual DbSet<HashTag> HashTags { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizAsset> QuizAssets { get; set; }
        public virtual DbSet<QuizContent> QuizContents { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CourseComment> CourseComments { get; set; }
        public virtual DbSet<CourseRate> CourseRates { get; set; }
        public virtual DbSet<SavedCourse> SavedCourses { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSocialNetwork> UserSocialNetworks { get; set; }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using ZaminEducation.Data.DbContexts;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Data.Repositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.Mappers;
using ZaminEducation.Service.Services;
using ZaminEducation.Service.Services.Courses;

namespace ZaminEducation.Test.Unit.Services.YouTube
{
    public partial class YoutubeServiceTest
    {
        private readonly ZaminEducationDbContext zaminEducationDbContext;
        private readonly IRepository<Course> courseRepositoryMock;
        private readonly IRepository<CourseVideo> courseVideoRepositoryMock;
        private readonly IRepository<CourseRate> courseRateRepositoryMock;
        private readonly IRepository<Attachment> attachmentReositoryMock;
        private readonly IRepository<ReferralLink> referralLinkRepositoryMock;

        private readonly YouTubeService youTubeService;
        private readonly CourseService courseService;
        private readonly AttachmentService attachmentService;
        private readonly IMapper mapper;

        public YoutubeServiceTest()
        {
            var options = new DbContextOptionsBuilder<ZaminEducationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            mapper = new MapperConfiguration(
                mapper => mapper.AddProfile<MappingProfile>()).CreateMapper();

            zaminEducationDbContext = new ZaminEducationDbContext(options);
            courseRepositoryMock = new Repository<Course>(zaminEducationDbContext);
            courseVideoRepositoryMock = new Repository<CourseVideo>(zaminEducationDbContext);
            attachmentReositoryMock = new Repository<Attachment>(zaminEducationDbContext);
            referralLinkRepositoryMock = new Repository<ReferralLink>(zaminEducationDbContext);

            youTubeService = new YouTubeService(courseVideoRepositoryMock, courseRepositoryMock);

            courseService = new CourseService(courseRepositoryMock,
                youTubeService,
                courseRateRepositoryMock,
                attachmentService,
                mapper,
                referralLinkRepositoryMock);
        }


        private CourseForCreationDto CreateRandomCourse(CourseForCreationDto courseForCreationDto)
        {
            courseForCreationDto.YouTubePlaylistLink = "https://www.youtube.com/watch?v=2NwXtLaZJkQ&list=PLAXSS6gGBPcVrqEaXc1Av4CLaLxbv9HOx";
            courseForCreationDto.Description = Faker.Lorem.Sentence(3);
            courseForCreationDto.Name = Faker.Name.First();

            return courseForCreationDto;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.IO;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.MainPages;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.HomePage;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Helpers;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Interfaces.Courses;

namespace ZaminEducation.Service.Services;

public class HomePageService : IHomePageService
{
    private readonly IHomePageRepository _homePageRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IMapper _mapper;
    private readonly string path; 

    public HomePageService(IHomePageRepository homePageRepository, 
        IMapper mapper, IRepository<Course> courseRepsository)
    {
        _homePageRepository = homePageRepository;
        _mapper = mapper;
        _courseRepository = courseRepsository;
        path = Path.Combine(EnvironmentHelper.WebRootPath, EnvironmentHelper.ResourcesPath); 
    }

    public async ValueTask<HomePage> GetHomePageAsync()
    { 
        var homePage = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        
        // you tube link problems effected bug
        // homePage.PopularCourses = await GetPopularCoursesAsync();

        return homePage;
    }

    public async ValueTask<bool> UpdateHeaderAsync(HomePageHeaderForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "Main page is not found.");

        var header = page.HomePageHeader;
        var mappedHeader = _mapper.Map(dto, header);
        page.HomePageHeader = mappedHeader;
        page.Update();

        if (dto.File is not null)
        {
            string fileName = await UpdateImageAsync(
                page.HomePageHeader.Image.Name, dto.File);

            page.HomePageHeader.Image.Path = Path.Combine(EnvironmentHelper.ResourcesPath, fileName);
            page.HomePageHeader.Image.Name = fileName;
        }

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);

        return true;
    }

    public async ValueTask<bool> UpdateOpportinutyAsync(OfferedOpportunitesForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "data is not found.");

        var oppoptunity = page.OpportunitiesOffered;
        var mappedOpportunity = _mapper.Map(dto, oppoptunity);
        page.OpportunitiesOffered = mappedOpportunity;
        page.Update();

        if (dto.File is not null)
        {
            string fileName = await UpdateImageAsync(
                page.OpportunitiesOffered.Image.Name, dto.File);

            page.OpportunitiesOffered.Image.Path = Path.Combine(EnvironmentHelper.ResourcesPath, fileName);
            page.HomePageHeader.Image.Name = fileName;
        }

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);

        return true;
    }

    public async ValueTask<bool> UpdatePhotoGalleryAsync(long id, ImageForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "data is not found.");

        if (id <= 0 || id > 6)
            throw new ZaminEducationException(404, "Image not found.");

        string fileName = await UpdateImageAsync(
                page.PhotoGallery.Photos[(int)id-1].Name, dto.File);

        page.PhotoGallery.Photos[(int)id - 1].Path = Path.Combine(EnvironmentHelper.ResourcesPath, fileName);
        page.PhotoGallery.Photos[(int)id - 1].Name = fileName;
        page.Update();

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);
        
        return true;
    }

    public async ValueTask<bool> UpdateProjectAboutInfoAsync(InfoAboutProjectForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "Main page is not found.");

        var aboutUs = page.InfoAboutProject;
        var mappedInfo = _mapper.Map(dto, aboutUs);
        page.InfoAboutProject = mappedInfo;
        page.Update();

        if (dto.File is not null)
        {
            string fileName = await UpdateImageAsync(
                page.HomePageHeader.Image.Name, dto.File);

            page.InfoAboutProject.Image.Path = Path.Combine(EnvironmentHelper.ResourcesPath, fileName);
            page.InfoAboutProject.Image.Name = fileName;
        }

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);

        return true;
    }

    public async ValueTask<bool> UpdateReasonAsync(long id, ReasonForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "Main page is not found.");

        if (id <= 0 || id > 4)
            throw new ZaminEducationException(404, "Not found.");

        var mappedReason = _mapper.Map(dto, 
            page.OpportunitiesOffered.Reasons[(int)id - 1]);
        page.OpportunitiesOffered.Reasons[(int)id - 1] = mappedReason;
        page.Update();

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);

        return true;
    }

    public async ValueTask<bool> UpdateSocialNetwordAsync(SocialNetworksForCreationDto dto)
    {
        var page = await _homePageRepository.GetAsync(EnvironmentHelper.MainPagePath);
        if (page == null)
            throw new ZaminEducationException(404, "Main page is not found.");

        page.SocialNetworks = _mapper.Map(dto, page.SocialNetworks);
        page.Update();

        await _homePageRepository.WriteAsync(page, EnvironmentHelper.MainPagePath);

        return true;
    }

    private async ValueTask<IEnumerable<Course>> GetPopularCoursesAsync()
    {
        return await _courseRepository.GetAll()
            .OrderBy(c => c.ViewCount)
                .Take(6)
                    .ToListAsync();
    }

    private async ValueTask<string> UpdateImageAsync(string fileName, IFormFile file)
    {
        string filExtention = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!Path.GetExtension(fileName).Equals(filExtention))
        {
            File.Delete(Path.Combine(path, fileName));

            fileName = Path.GetFileNameWithoutExtension(fileName) + filExtention;
        }

        // copy image to the destination as stream
        FileStream fileStream = File.OpenWrite(Path.Combine(path, fileName));
        await file.CopyToAsync(fileStream);

        // clear
        await fileStream.FlushAsync();
        fileStream.Close();

        return fileName;
    }
}
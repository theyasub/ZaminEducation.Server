using Microsoft.AspNetCore.Http;
using ZaminEducation.Domain.Entities.MainPages;
using ZaminEducation.Service.DTOs.HomePage;

namespace ZaminEducation.Service.Interfaces;

public interface IHomePageService
{
    ValueTask<HomePage> GetHomePageAsync();
    ValueTask<bool> UpdateHeaderAsync(HomePageHeaderForCreationDto dto);

    ValueTask<bool> UpdateProjectAboutInfoAsync(InfoAboutProjectForCreationDto dto);

    ValueTask<bool> UpdateOpportinutyAsync(OfferedOpportunitesForCreationDto dto );

    ValueTask<bool> UpdateReasonAsync(long id, ReasonForCreationDto dto);

    ValueTask<bool> UpdateSocialNetwordAsync(SocialNetworksForCreationDto dto);

    ValueTask<bool> UpdatePhotoGalleryAsync(long id, ImageForCreationDto dto);
}
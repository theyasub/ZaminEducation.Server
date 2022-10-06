using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Courses;

namespace ZaminEducation.Domain.Entities.MainPages;

public class HomePage : Auditable  
{
    public HomePageHeader HomePageHeader { get; set; }

    public InfoAboutProject InfoAboutProject { get; set; }

    public OfferedOpportunities OpportunitiesOffered { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Course> PopularCourses { get; set; }

    public PhotoGallery PhotoGallery { get; set; }

    public SocialNetworks SocialNetworks { get; set; }

}
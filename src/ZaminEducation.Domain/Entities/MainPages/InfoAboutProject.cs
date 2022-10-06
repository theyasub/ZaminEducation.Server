using Newtonsoft.Json;
using ZaminEducation.Domain.Commons;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class InfoAboutProject : Text
{
    public Image Image { get; set; }
}
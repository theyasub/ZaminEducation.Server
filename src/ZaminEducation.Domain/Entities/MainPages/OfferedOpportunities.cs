using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.MainPages.Commons;

namespace ZaminEducation.Domain.Entities.MainPages;

public class OfferedOpportunities : Text
{
    public IList<Reason> Reasons { get; set; }

    public Image Image { get; set; }
}
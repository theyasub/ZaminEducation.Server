using System.Threading.Tasks;
using ZaminEducation.Domain.Entities.MainPages;

namespace ZaminEducation.Data.IRepositories;

public interface IHomePageRepository
{
    public ValueTask<HomePage> GetAsync(string path);
    public ValueTask<HomePage> WriteAsync(HomePage homePage, string path);
}
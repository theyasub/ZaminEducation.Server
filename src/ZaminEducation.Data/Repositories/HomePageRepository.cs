using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.MainPages;

namespace ZaminEducation.Data.Repositories;

public class HomePageRepository : IHomePageRepository
{
    public async ValueTask<HomePage> GetAsync(string path)
    {
        string json = await File.ReadAllTextAsync(path);
        return JsonConvert.DeserializeObject<HomePage>(json);
    }

    public async ValueTask<HomePage> WriteAsync(HomePage homePage, string path)
    {
        string json = JsonConvert.SerializeObject(homePage, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);

        return homePage;
    }
}
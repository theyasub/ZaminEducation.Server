using System.IO;

namespace ZaminEducation.Service.Helpers;

public class EnvironmentHelper
{
    public static string WebRootPath { get; set; }
    public static string AttachmentPath => Path.Combine(WebRootPath, "images");
    public static string FilePath => "images";
    public static string CertificatePath => "certificates";

    public static string ResourcesPath => "resources";
    public static string MainPagePath => Path.Combine(WebRootPath, ResourcesPath, "mainpage.json");
    public static string HomePagesInfoConnectinString =>
        @"C:\Users\Muhammadamin\Source\Repos\ZaminEducation.Server\src\ZaminEducation.Api\wwwroot\ZCApplicantInfo.json";

}

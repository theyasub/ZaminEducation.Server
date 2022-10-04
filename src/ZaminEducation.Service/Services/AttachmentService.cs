using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Helpers;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IRepository<Attachment> _repository;

    public AttachmentService(IRepository<Attachment> repository)
    {
        _repository = repository;
    }

    public async ValueTask<Attachment> CreateAsync(string fileName, string filePath)
    {
        var file = new Attachment()
        {
            Name = fileName,
            Path = filePath,
            CreatedBy = HttpContextHelper.UserId
        };

        file = await _repository.AddAsync(file);
        file.Create();
        await _repository.SaveChangesAsync();

        return file;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
    {
        var file = await _repository.GetAsync(expression);

        if (file is null)
            throw new ZaminEducationException(404, "Attachment not found");

        // delete file from wwwroot
        string fullPath = Path.Combine(EnvironmentHelper.WebRootPath, file.Path);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        // datele database information
        FileHelper.Remove(file.Path);

        _repository.Delete(file);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async ValueTask<Attachment> UploadAsync(AttachmentForCreationDto dto)
    {
        // genarate file destination
        string fileName = Guid.NewGuid().ToString("N") + "-" + dto.FileName;
        string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

        if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
            Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

        // copy image to the destination as stream
        FileStream fileStream = File.OpenWrite(filePath);
        await dto.Stream.CopyToAsync(fileStream);

        // clear
        await fileStream.FlushAsync();
        fileStream.Close();

        var newAttachement = new Attachment()
        {
            Name = fileName,
            Path = Path.Combine(EnvironmentHelper.FilePath, fileName),
        };

        newAttachement.Create();
        newAttachement = await _repository.AddAsync(newAttachement);
        await _repository.SaveChangesAsync();

        return newAttachement;
    }

    public async ValueTask<Attachment> GetAsync(Expression<Func<Attachment, bool>> expression)
    {
        var existAttachement = await _repository.GetAsync(expression, null);

        if (existAttachement is null)
            throw new ZaminEducationException(404, "Attachment not found.");

        return existAttachement;
    }

    public async ValueTask<Attachment> UpdateAsync(long id, Stream stream)
    {
        var existAttachment = await _repository.GetAsync(a => a.Id == id, null);

        if (existAttachment is null)
            throw new ZaminEducationException(404, "Attachment not found.");

        string fileName = existAttachment.Path;
        string filePath = Path.Combine(EnvironmentHelper.WebRootPath, fileName);

        // copy image to the destination as stream
        FileStream fileStream = File.OpenWrite(filePath);
        await stream.CopyToAsync(fileStream);

        // clear
        await fileStream.FlushAsync();
        fileStream.Close();

        existAttachment.Update();
        existAttachment = _repository.Update(existAttachment);
        await _repository.SaveChangesAsync();

        return existAttachment;
    }
}

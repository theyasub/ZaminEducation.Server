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
        await _repository.SaveChangesAsync();

        return file;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
    {
        var file = await _repository.GetAsync(expression);

        if (file is null)
            throw new ZaminEducationException(404, "Attachment not found");

        FileHelper.Remove(file.Path);

        _repository.Delete(file);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async ValueTask<Attachment> DownloadAsync(AttachmentForCreationDto dto)
    {
        var (fileName, filePath) = await FileHelper.SaveAsync(dto);

        var newAttachement = new Attachment()
        {
            Name = fileName,
            Path = filePath,
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

        await FileHelper.SaveAsync(new()
        {
            FileName = existAttachment.Name,
            Stream = stream,
        },
        true);

        existAttachment.Update();

        existAttachment = _repository.Update(existAttachment);
        await _repository.SaveChangesAsync();

        return existAttachment;
    }


}

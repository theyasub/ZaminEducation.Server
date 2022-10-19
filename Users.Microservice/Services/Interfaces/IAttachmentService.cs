using System.Linq.Expressions;
using Users.Microservice.Models.Entities;
using Users.Microservice.Services.DTOs;

namespace Users.Microservice.Services.Interfaces
{
    public interface IAttachmentService
    {
        ValueTask<Attachment> UploadAsync(AttachmentForCreationDto dto);
        ValueTask<Attachment> UpdateAsync(long id, Stream stream);
        ValueTask<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression);
        ValueTask<Attachment> GetAsync(Expression<Func<Attachment, bool>> expression);
        ValueTask<Attachment> CreateAsync(string fileName, string filePath);
    }
}

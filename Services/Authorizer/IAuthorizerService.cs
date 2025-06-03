using PicPaySimplified.Models.Enum;

namespace PicPaySimplified.Services.Authorizer
{
    public interface IAuthorizerService
    {
        Task<bool> AuthorizeAsync();
    }
}
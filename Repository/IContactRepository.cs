using Contact.API.Dto;
using Contact.API.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.API.Repository
{
    public interface IContactRepository
    {
        Task<bool> UpdateContactInfo(BaseUserInfo userInfo, CancellationToken cancellationToken);
        Task<bool> AddContactAsync(int userId, BaseUserInfo contact, CancellationToken cancellationToken);
        Task<List<Models.Contact>> GetContactsAsync(int userId, CancellationToken cancellationToken);
        /// <summary>
        ///用户给好友打标签：给指定用户的 指定好友打标签
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="contactId">好友id</param>
        /// <param name="tags">标签列表</param>
        /// <returns></returns>
        Task<bool> TagsContactAsync(int userId, int contactId, List<string> tags, CancellationToken cancellationToken);

    }
}

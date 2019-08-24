using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Dto;

namespace Contact.API.Services
{
    public class UserServcie : IUserService
    {
        public Task<BaseUserInfo> GetBaseUserInfoAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

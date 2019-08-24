using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Models;
using Contact.API.Repository;
using Contact.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private IContactApplyRequestRepository _contactApplyRequestRepository;
        private IUserService _userService;

        public ContactController(IContactApplyRequestRepository contactApplyRequestRepository
            , IUserService userService)
        {
            _contactApplyRequestRepository = contactApplyRequestRepository;
            _userService = userService;
        }

        #region   好友申请

        /// <summary>
        /// 获取好友申请列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("apply-request")]
        public async Task<List<ContactApplyRequest>> GetApplyRequests()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var requests = await _contactApplyRequestRepository.GetRequestListAsync(UserIdentity.UserId, cancellationToken);
            return requests;
        }

        /// <summary>
        /// 申请人选择 用户 进行好友申请
        /// </summary>
        /// <param name="userId">被申请人id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply-request/{userId}")]
        public async Task<IActionResult> AddApplyRequest(int userId, CancellationToken cancellationToken)
        {
            var baseUserInfo = await _userService.GetBaseUserInfoAsync(userId, cancellationToken);
            if (baseUserInfo == null)
            {
                throw new Exception("用户参数错误");
            }
            var result = await _contactApplyRequestRepository.AddRequestAsync(new ContactApplyRequest()
            {
                UserId = userId,
                ApplierId = UserIdentity.UserId,
                Name = baseUserInfo.Name,
                Company = baseUserInfo.Company,
                Title = baseUserInfo.Title,
                CreateTime = DateTime.Now,
                Avatar = baseUserInfo.Avatar//TBD 申请人和被申请人有可能搞乱
            }, new CancellationToken());
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }


        #endregion
    }
}

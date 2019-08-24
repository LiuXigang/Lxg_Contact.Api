using Contact.API.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected UserIdentity UserIdentity
        {
            get
            {
                var identity = new UserIdentity();
                //TBD
                identity.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "sub").Value ?? "");
                identity.Name = User.Claims.FirstOrDefault(c => c.Type == "name").Value ?? "";
                identity.Company = User.Claims.FirstOrDefault(c => c.Type == "company").Value ?? "";
                identity.Avatar = User.Claims.FirstOrDefault(c => c.Type == "avatar").Value ?? "";
                identity.Title = User.Claims.FirstOrDefault(c => c.Type == "title").Value ?? "";
                //string ss = User.Claims.FirstOrDefault(c => c.Type == "company").Value ?? "";
                //identity.UserId = 1;
                //identity.Name = "jesse";
                //identity.Company = "company";
                //identity.Avatar = "avatar";
                //identity.Title = "title";
                return identity;
            }


        }



    }
}

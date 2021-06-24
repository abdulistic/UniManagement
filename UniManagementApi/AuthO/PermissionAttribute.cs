using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static Restaurant.ClassLibrary.ViewModel.AuthenticateResponse;

namespace UniManagementApi.AuthO
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roleItem;

        public PermissionAttribute(string[] roleItem)
        {
            _roleItem = roleItem;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() != null)
            {
                string token = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    UserVM userRegisterVM = DecodeToken(token);


                    if (string.IsNullOrEmpty(userRegisterVM?.Role) || !_roleItem.Any(x => x == userRegisterVM?.Role))
                    {
                        //context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                        return;
                    }
                }
                else
                {
                    //context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                    return;
                }
            }
            else
            {
                //context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                return;
            }
        }

        public UserVM DecodeToken(string token)
        {
            UserVM userVM = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(token) && token != "null")
                {
                    var handler = new JwtSecurityTokenHandler();
                    var tokenObj = handler.ReadToken(token) as JwtSecurityToken;
                    var payloadData = tokenObj.Payload;
                    userVM = new UserVM { UserId = Convert.ToInt64(payloadData["UserId"] + ""), Role = payloadData["Role"] + "" };
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
            }

            return userVM;
        }
    }
}

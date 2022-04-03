using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telerivet.Client;
using TelerivetExample.Helpers;
using TelerivetExample.Managers;
using TelerivetExample.Models;

namespace TelerivetExample.Services
{
    public class TelerivetService
    {
        //validate the Token from webconfig and passed value
        public TelerivetTokenStatus ValidateTelerivetToken(string telerivetToken)
        {
            bool tokenStatus = (ConfigurationManager.AppSettings["TelerivetToken"] == telerivetToken);
            TelerivetTokenStatus status = new TelerivetTokenStatus();
            status.IsValid = tokenStatus;
            if (tokenStatus)
            {
                status.Message = "Token provided matched with the System.";
                status.StatusCode = 403;
            }
            else
            {
                status.Message = "Token provided do not match with the System.";
                status.StatusCode = 200;
            }
            return status;
        }

        public TelerivetTokenStatus HandleIncomingMessage(HttpRequestBase request)
        {
            var requestTelerivetToen = request["secret"];
            Services.TelerivetService telerivetService = new Services.TelerivetService();
            TelerivetTokenStatus telerivetTokenStatus = telerivetService.ValidateTelerivetToken(requestTelerivetToen);

            if (telerivetTokenStatus.IsValid)
            {
                if (request["event"] == "incoming_message")
                {
                    string content = request["content"];
                    string fromNumber = request["from_number"];
                    string phoneId = request["phone_id"];

                    EventManager.InsertOrUpdate(new Event()
                    {
                        Content = content,
                        FromNumber = fromNumber,
                        PhoneID = phoneId
                    });

                    HandleRequest(fromNumber, content);
                    SendAckReplyMessage(fromNumber);
                }
                else
                {
                    telerivetTokenStatus.StatusCode = 400;
                    telerivetTokenStatus.IsValid = false;
                    telerivetTokenStatus.Message = "Something went wrong. Please try again.";
                }
            }
            return telerivetTokenStatus;

        }

        public void HandleRequest(string fromNumber, string content)
        {
            var splittedContent = content.Split(' ');
            if(splittedContent.Length > 1)
            {
                switch (splittedContent[0])
                {
                    #region user related cases
                    case "RR":
                        HandleUserRegistrationAsync(fromNumber, splittedContent);
                        break;
                    case "RA":
                        HandleUserApproveAsync(fromNumber, splittedContent);
                        break;
                    case "UPC":
                        HandleUserPinChangeAsync(fromNumber, splittedContent);
                        break;
                    #endregion
                    #region project related cases
                    case "RC":
                        HandleProjectRegistrationAsync(fromNumber, splittedContent);
                        break;
                    case "AA":
                        HandleProjectApproveAsync(fromNumber, splittedContent);
                        break;
                    case "PPC":
                        HandleProjectPinChangeAsync(fromNumber, splittedContent);
                        break;
                    #endregion
                    default:
                        SendFormatErrorMessage(fromNumber);
                        break;

                }
            }
        }

        #region handle user related incoming messages
        public async Task HandleUserRegistrationAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 3)
            {
                var newUser = new User()
                {
                    Phone = fromNumber,
                    Name = splittedContent[1],
                    Pin = splittedContent[2]
                };
                var userInsertStatus = UserManager.InsertOrUpdate(newUser);

                #region Identity setup for new user 
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var role = new IdentityRole();
                role.Name = "Administrator";

                var user = new IdentityUser() { Id = userInsertStatus.ToString() , UserName = newUser.Name, PhoneNumber=newUser.Phone};
                
                IdentityResult result = manager.Create(user, newUser.Pin);

                if (result.Succeeded)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                    //Response.Redirect("~/Login");
                }
                else
                {
                    //StatusMessage.Text = result.Errors.FirstOrDefault();
                    var x = result.Errors.FirstOrDefault();
                }
                #endregion

                if (userInsertStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "User(" + splittedContent[1] + ") Registration success. Thank you.");
                }
                else if (userInsertStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "Uesr(" + splittedContent[1] + ") already exists.");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for User Registration. Use Format:RR[space]Name[space]Pincode");
            }
        }

        public async Task HandleUserApproveAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 3)
            {
                var approveStatus = UserManager.ApproveUser(fromNumber);
                if (approveStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "User(" + splittedContent[1] + ") Approval success. Thank you.");
                }
                else if (approveStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "Project not found. Use Format:AA[space]ProjectCode[space]Request_ID[space]Pincode");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for User Approval. Use Format:RA[space]Name[space]Pincode");
            }
        }

        public async Task HandleUserPinChangeAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 4)
            {
                User user = new User()
                {
                    Name = splittedContent[1],
                    Phone = splittedContent[2],
                    Pin = splittedContent[3]
                };
                var pingChangeStatus = UserManager.ChangeUserPin(user);
                if (pingChangeStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "User(" + splittedContent[1] + ") Pincode Change success. Thank you.");
                }
                else if (pingChangeStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "User not found. Use Format:UPC[space]Name[space]Phone[space]Pincode");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for User Approval. Use Format:RA[space]Name[space]Pincode");
            }
        }

        #endregion

        #region handle project related incoming messages
        public async Task HandleProjectRegistrationAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 4)
            {
                UserProject userProject = new UserProject()
                {
                    ProjectCode = splittedContent[1],
                    Pin = splittedContent[3],
                    FromNumber= fromNumber
                };
                decimal.TryParse(splittedContent[2], out decimal amount);
                userProject.Amount = amount;

                var userProjectInsertStatus = UserProjectManager.InsertOrUpdate(userProject);
                if (userProjectInsertStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "Project(" + splittedContent[1] + ") Registration success. Thank you.");
                }
                else if (userProjectInsertStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "Project(" + splittedContent[1] + ") already exists.");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for Project Registration. Use Format:RC[space]ProjectCode[space]Amount[space]Pincode");
            }
        }

        public async Task HandleProjectApproveAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 4)
            {
                string projectCode = splittedContent[1];
                int.TryParse(splittedContent[2], out int requestID);

                int approveStatus = UserProjectManager.ApproveProject(requestID, projectCode);
                if (approveStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "Project(" + splittedContent[1] + ") Approve success. Thank you.");
                }
                else if (approveStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "Project not found. Use Format:AA[space]ProjectCode[space]Request_ID[space]Pincode");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for Project Approval. Use Format:AA[space]ProjectCode[space]Request_ID[space]Pincode");
            }
        }

        public async Task HandleProjectPinChangeAsync(string fromNumber, string[] splittedContent)
        {
            if (splittedContent.Length == 4)
            {
                UserProject userProject = new UserProject()
                {
                    ProjectCode = splittedContent[1],
                    Pin = splittedContent[3]
                };

                int.TryParse(splittedContent[2], out int requestID);
                userProject.RequestID = requestID;

                var pingChangeStatus = UserProjectManager.ChangeProjectPin(userProject);
                if (pingChangeStatus > 0)
                {
                    await SendCustomMessage(fromNumber,
                        "Project(" + splittedContent[1] + ") Pincode Change success. Thank you.");
                }
                else if (pingChangeStatus == -1)
                {
                    await SendCustomMessage(fromNumber,
                        "Project not found. Use Format:PPC[space]ProjectCode[space]RequestID[space]Pincode");
                }
                else
                {
                    SendSysErrorMessage(fromNumber);
                }
            }
            else
            {
                await SendCustomMessage(fromNumber,
                    "Format is incorrect for User Approval. Use Format:RA[space]Name[space]Pincode");
            }
        }

        #endregion

        public async Task SendAckReplyMessage(string toNumber)
        {
            await TelerivetProject.project.SendMessageAsync(
               Util.Options("content",
               "Message recieved by " + Common.CompanyName + ". Please wait for validation. Thank you for your patience.",
               "to_number",
               toNumber
               ));
        }
        public async Task SendSysErrorMessage(string toNumber)
        {
            await TelerivetProject.project.SendMessageAsync(
                Util.Options("content",
                Common.CompanyName + "'s System is having technical issue. Please conatact the Administrator(9876543210). Sorry for inconvenience.",
                "to_number",
                toNumber
                ));
        }
        public async Task SendFormatErrorMessage(string toNumber)
        {
            await TelerivetProject.project.SendMessageAsync(
                Util.Options("content",
                  " 1.User Registration: RR[space]Name[space]Pincode \n"
                + " 2.User Approve: RA[space]Name[space]Pincode \n"
                + " 3.Project Creation : RC[space]ProjectCode[space]Amount[space]Pincode \n"
                + " 4.Project Approve : AA[space]ProjectCode[space]Request_ID[space]Pincode \n"
                + " 5.User Pincode Change : UPC[space]Name[space]Phone[space]Pincode \n"  
                + " 6.Project Pincode Change : PPC[space]ProjectCode[space]RequestID[space]Pincode",
                "to_number",
                toNumber
                ));
        }
        public async Task SendCustomMessage(string toNumber, string customMessage)
        {
            await TelerivetProject.project.SendMessageAsync(
                Util.Options("content",
                customMessage,
                "to_number",
                toNumber
                ));
        }

        public async Task<Message> SendMockData(Event evt)
        {
            var x=  await TelerivetProject.project.SendMessageAsync(
                Util.Options("content",
                evt.Content,
                "to_number",
                "9849"
                ));
            return x;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories;
using Varldsklass.Web.Infrastructure;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.Controllers
{
    public class AccountController : Controller
    {

        private IAccountRepository _accountRepository;
        private CustomMembership membership;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            membership = new CustomMembership();
            membership.AccountRepository = _accountRepository;
        }

        private bool IsAdmin(string email)
        {
            return (_accountRepository.FindAll(u => u.Email == email && u.Administrator).Count() > 0);
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                if (membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if(IsAdmin(model.Email))
                            return RedirectToAction("Index", "Admin");
                        else
                            return RedirectToAction("Index", "Home");
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                membership.CreateUser(model.FirstName, model.LastName, model.Email, model.Password, model.Company, model.Address, model.Phone, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public ActionResult Edit()
        {
            EditAccountViewModel viewModel = new EditAccountViewModel();
            Account account = _accountRepository.FindAll().Where(a => a.Email == User.Identity.Name).FirstOrDefault();

            viewModel.FirstName = account.FirstName;
            viewModel.LastName = account.LastName;
            viewModel.Company = account.Company;
            viewModel.Address = account.Address;
            viewModel.Phone = account.Phone;

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditAccountViewModel viewModel)
        {
            Account account = _accountRepository.FindAll(a => a.Email == User.Identity.Name).FirstOrDefault();

            account.FirstName = viewModel.FirstName;
            account.LastName = viewModel.LastName;
            account.Company = viewModel.Company;
            account.Address = viewModel.Address;
            account.Phone = viewModel.Phone;

            _accountRepository.Save(account);

            return RedirectToAction("View");
        }

        [Authorize]
        public ActionResult View()
        {
            Account account = _accountRepository.FindAll(a => a.Email == User.Identity.Name).FirstOrDefault();
            return View(account);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}

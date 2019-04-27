using BotDetect.Web.Mvc;
using Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT.Common;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ActivatingUser()
        {
            if(Session[CommonConstants.OTP_SESSION]==null)
            {
                if (Session[CommonConstants.USER_INFO_SESSION] != null)
                {
                    var otp = new OTPSimulation().MakeOTP();
                    Session[CommonConstants.OTP_SESSION] = otp;
                    var user = (Account)Session[CommonConstants.USER_INFO_SESSION];
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/Client/template/CodeActive.html"));
                    content = content.Replace("{{Username}}", user.Username);
                    content = content.Replace("{{CodeActive}}", otp);
                    new MailHelper().SendMail(user.Email, "Mã kích hoạt tài khoản", content);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult ActivatingUser(string OTP)
        {
            if (ModelState.IsValid)
            {
                var otp = Session[CommonConstants.OTP_SESSION].ToString();
                var user = (Account)Session[CommonConstants.USER_INFO_SESSION];
                if (otp.Equals(OTP))
                {
                    var isActivated = new AccountDAO().UpdateStatusUser(user.Username);
                    Session[CommonConstants.OTP_SESSION] = null;
                    Session[CommonConstants.USER_INFO_SESSION] = null;
                    Session[CommonConstants.ACTIVATED_USER_SUCCESS] = "Kích hoạt thành công";
                    return Redirect("/dang-nhap");
                }
                else
                {
                    ModelState.AddModelError("", "Mã kích hoạt không chính xác");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không chính xác!")]
        public ActionResult Register(RegisterModel account)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var EncryptingMD5Pw = Encryptor.MD5Hash(account.Password);
                account.Password = EncryptingMD5Pw;
                var user = new Account();
                user.Username = account.Username;
                user.Password = account.Password;
                user.Name = account.Name;
                user.Phone = account.Phone;
                user.Address = account.Address;
                user.Email = account.Email;
                user.Status = false;
                user.CreateDate = DateTime.Now;
                user.Level = 0;
                var result = dao.Insert(user);
                if (result > 0)
                {
                    var otp = new OTPSimulation().MakeOTP();
                    Session[CommonConstants.OTP_SESSION] = otp;
                    Session.Add(CommonConstants.USER_INFO_SESSION, user);
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/Client/template/CodeActive.html"));
                    content = content.Replace("{{Username}}", user.Username);
                    content = content.Replace("{{CodeActive}}", otp);
                    new MailHelper().SendMail(user.Email,"Mã kích hoạt tài khoản", content);
                    account = new RegisterModel();
                    return Redirect("/kich-hoat-tk");
                }
                else
                {
                    return Redirect("/register-failed");
                }
            }
            return View(account);
        }

        public ActionResult NotActivated()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetInfoByUsername(model.UserName);
                    Session.Add(CommonConstants.USER_INFO_SESSION, user);
                    var userSession = new UserLogin();
                    if (user.Status == true)
                    {
                        userSession.UserName = model.UserName;
                        userSession.UserID = user.ID;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        if (user.Level == -1)
                        {
                            return Redirect("/Admin");
                        }
                        return Redirect("/");
                    }
                    else
                    {
                        return RedirectToAction("NotActivated");
                    }
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thật bại");
                }
            }
            return View(model);
        }
        public JsonResult CheckUsername(string username)
        {
            var row = new AccountDAO().FindByUsername(username);
            if (row == null)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        public JsonResult CheckEmail(string email)
        {
            var row = new AccountDAO().IsExitsEmail(email);
            if (row == true)
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true
                });
            }
        }

        public JsonResult CheckPhone(string phone)
        {
            var row = new AccountDAO().IsExitsPhone(phone);
            if (row == true)
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true
                });
            }
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session[CommonConstants.USER_INFO_SESSION] = null;
            return Redirect("/");
        }
    }
}
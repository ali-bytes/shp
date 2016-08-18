using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Domain.EStore;
using Domain.EStore.Repositories;
using Ui.EStore.Helpers;
using Ui.EStore.Models;

namespace Ui.EStore.Controllers
{
    public class AdminController : Controller
    {

        readonly IUnitOfWork _unitOfWork = new UnitOfWork();



        // GET: /Admin/Login
        public ActionResult Login()
        {
            if (Session["UserID"] != null)
                Response.Redirect("/Admin");

            return View();
        }

        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            Response.Redirect("/Admin/Login");
            return View();
        }

        //
        // GET: /Admin/ValidateUser
        public JsonResult ValidateUser(User usr)
        {
            var clientMessage = new ClientMessage<string>();

            var regUser =
                _unitOfWork.Users.FindAll().FirstOrDefault(
                    u =>
                    u.Email == usr.Email &&
                    u.Password == Security.EncodePassword(usr.Password) && u.IsActive == true);
            if (regUser != null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                Session["UserID"] = regUser.Id;
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;

                clientMessage.ClientMessageContent = new List<string>() { "خطأ بيانات دخولك غير صحيحه" };

            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/

        public ActionResult Index()
        {
            Security.CheckLogin();

            return View();
        }

        public ActionResult AboutUs()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }
        public ActionResult ContactUs()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }
        public ActionResult RequestUnderApproved()
        {

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "RequestUnderApproved"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }

        public ActionResult RequestApproved()
        {

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "RequestApproved"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }



        #region AboutUs
        [HttpGet]
        public JsonResult GetAboutUs()
        {
            var clientMessage = new ClientMessage<HomeDetail>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            var aboutUs = _unitOfWork.HomeDetails.FindById(1);
            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = aboutUs;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult SaveAboutUs(HomeDetail homeDetail)
        {
            var clientMessage = new ClientMessage<string>();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }

            var isSaved = _unitOfWork.HomeDetails.Add(homeDetail);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ البيانات" };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);

        }



        #endregion

        #region SaveContactUs
        [HttpGet]
        public JsonResult GetContactUs()
        {
            var clientMessage = new ClientMessage<HomeDetail>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            var aboutUs = _unitOfWork.HomeDetails.FindById(3);
            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = aboutUs;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult SaveContactUs(HomeDetail homeDetail)
        {
            var clientMessage = new ClientMessage<string>();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditAboutUs"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }

            var isSaved = _unitOfWork.HomeDetails.Add(homeDetail);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ البيانات" };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);

        }



        #endregion

        #region  Speciality



        [HttpPost]
        public JsonResult SaveSpeciality(Speciality speciality)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();

            if (speciality.Id <= 0)
            {

                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "AddSpeciality"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditSpeciality"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }

                if (_unitOfWork.Specialities.FindAll().Any(c => c.Name == speciality.Name && c.Id != speciality.Id))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "التخصص مضاف  من قبل " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }
            }
            var isSaved = _unitOfWork.Specialities.Add(speciality);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ التخصص  " };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSpecialitys()
        {
            var clientMessage = new ClientMessage<IQueryable>();
            var categories = _unitOfWork.Specialities.FindAll().ToList().Select(r => new
            {
                r.Id,

                r.Name,
                CanDelete = CanDelete(r)
            }).AsQueryable();

            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }




        private bool CanDelete(Speciality speciality)
        {

            return true;
        }

        [HttpGet]
        public JsonResult GetSpeciality(int id)
        {
            var clientMessage = new ClientMessage<Speciality>();
            var categories = _unitOfWork.Specialities.FindFirstOrDefault(id);
            if (categories == null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تحقق من التخصص المختار " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSpeciality(int id)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "DeleteSpeciality"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };

            }
            if (id > 0)
            {
                var categories = _unitOfWork.Specialities.FindFirstOrDefault(id);
                if (categories == null)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تحقق من التخصص المختار " };

                }

                var isDeleted = _unitOfWork.Specialities.Remove(categories);
                if (isDeleted)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تم حذف التخصص " };
                }
                else
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
                }
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تاكد من البيانات  " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region User

        public ActionResult AddUser()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "AddUser"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }
        public ActionResult EditUser()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditUser"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }

        [HttpPost]
        public JsonResult SaveUser(User user)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();

            if (user.Id <= 0)
            {

                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "AddUser"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }
                user.Password = Security.EncodePassword(user.Password);
            }
            else
            {
                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditUser"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }



                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = Security.EncodePassword(user.Password);

                }
                else
                {
                    user.Password = _unitOfWork.Users.FindById(user.Id).Password;
                }
            }
            if (_unitOfWork.Users.FindAll().Any(c => c.UserName == user.UserName && c.Id != user.Id))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "المستخدم مضاف  من قبل " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }
            var isSaved = _unitOfWork.Users.Add(user);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ المستخدم  " };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            var clientMessage = new ClientMessage<IQueryable>();
            var categories = _unitOfWork.Users.FindAll().ToList().Select(r => new
            {
                r.Id,
                r.Email,
                r.IsActive,
                r.RuleId,

                r.UserName,
                CanDelete = CanDelete(r)

            }).AsQueryable();

            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        private bool CanDelete(User user)
        {
            if (_unitOfWork.UserOperations.FindAll().Any(c => c.UserId == user.Id))
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public JsonResult GetUser(int id)
        {
            var clientMessage = new ClientMessage<User>();
            var categories = _unitOfWork.Users.FindFirstOrDefault(id);
            if (categories == null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تحقق من المستخدم  " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "DeleteUser"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };

            }
            if (id > 0)
            {
                var categories = _unitOfWork.Users.FindFirstOrDefault(id);
                if (categories == null)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تحقق من المستخدم " };

                }

                var isDeleted = _unitOfWork.Users.Remove(categories);
                if (isDeleted)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تم حذف المستخدم " };
                }
                else
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
                }
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تاكد من البيانات  " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region Rule

        public ActionResult AddRole()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "AddRole"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }
        public ActionResult EditRole()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditRole"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }

        [HttpPost]
        public JsonResult SaveRule(Rule rule)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();

            if (rule.Id <= 0)
            {

                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "AddRule"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditRule"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }


            }
            if (_unitOfWork.Rules.FindAll().Any(c => c.Name == rule.Name && c.Id != rule.Id))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "الصلاحية مضافة  من قبل " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }
            var isSaved = _unitOfWork.Rules.Add(rule);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ الصلاحية  " };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRules()
        {
            var clientMessage = new ClientMessage<IQueryable>();
            var categories = _unitOfWork.Rules.FindAll().ToList().Select(r => new
            {
                r.Id,
                r.IsActive,

                r.Name,
                CanDelete = CanDelete(r)
            }).AsQueryable();

            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        private bool CanDelete(Rule rule)
        {

            if (_unitOfWork.Users.FindAll().Any(c => c.RuleId == rule.Id))
            {
                return false;
            }
            if (_unitOfWork.RulePrivileges.FindAll().Any(c => c.RuleId == rule.Id))
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public JsonResult GetRule(int id)
        {
            var clientMessage = new ClientMessage<Rule>();
            var categories = _unitOfWork.Rules.FindFirstOrDefault(id);
            if (categories == null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تحقق من الصلاحية  " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteRule(int id)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "admin", "DeleteRule"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };

            }
            if (id > 0)
            {
                var categories = _unitOfWork.Rules.FindFirstOrDefault(id);
                if (categories == null)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تحقق من الصلاحية  " };

                }

                var isDeleted = _unitOfWork.Rules.Remove(categories);
                if (isDeleted)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تم حذف الصلاحية " };
                }
                else
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تعذر الحفظ ,اعد المحاولة او اتصل بالدعم الفنى " };
                }
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تاكد من البيانات  " };
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region MyRules

        public static List<Rule> GetSysRule()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var rules = unitOfWork.Rules.FindAll().ToList();
            return rules;
        }

        public ActionResult Editrule()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditRole"))
            {
                return RedirectToAction("Error403", "Home");
            }


            return View();
        }

        #endregion

        #region FAQ

        public static List<FAQ> GetFAQsView()
        {
            var unitOfWork = new UnitOfWork();
            return unitOfWork.FAQs.FindAll().OrderBy(s => s.FAQId).ToList();
        }

        // GET: /SupportCenter/AddFAQToDb
        public JsonResult AddFAQToDb(FAQ faq)
        {

            var clientMessage = new ClientMessage<string>();

            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "NewFAQ"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }

            faq.FAQCreateDate = DateTime.Now.ToUniversalTime(); ;



           
            var tmpService =
                _unitOfWork.FAQs.FindAll().FirstOrDefault(
                    cat =>
                    cat.FAQTitle == faq.FAQTitle );
            if (tmpService != null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "السؤال مضاف من قبل " };
            }

            _unitOfWork.FAQs.Add(faq);
            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;

            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        // GET: /SupportCenter/GetAllFAQs
        public JsonResult GetAllFAQs()
        {
            var clientMessage = new ClientMessage<IQueryable>();


           

            List<TwoProps> usersList;
            
                usersList = _unitOfWork.FAQs.FindAll().Select(
                                u =>
                                new TwoProps()
                                {

                                    Id = u.FAQId,
                                    Name = u.FAQTitle
                                }).ToList();
            

            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = usersList.AsQueryable();
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        // GET: /TrainingServices/GetFAQ
        public JsonResult GetFAQ(int faqId)
        {
            var clientMessage = new ClientMessage<IQueryable>();
            //  Convert.ToDecimal(Session) == SYS_UserID for profile page
          

            var DownloadFile =
                _unitOfWork.FAQs.FindAll().Where(u => u.FAQId == faqId)
                           .Select(
                               us =>
                               new
                               {
                                   us.FAQCreateDate,
                                   us.FAQDetails,
                                   
                                   us.FAQId,
                                   us.FAQTitle,
                                   


                               });
            if (!DownloadFile.Any())
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "السؤال غير موجود " };
            }
            else
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = DownloadFile;
            }
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        // GET: /TrainingServices/EditFAQToDb
        public JsonResult EditFAQToDb(FAQ faq)
        {
            var clientMessage = new ClientMessage<string>();

            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditFAQ"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }
      


            var tmpusr = _unitOfWork.FAQs.FindAll().FirstOrDefault(u => u.FAQId == faq.FAQId);
            if (tmpusr == null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "السؤال غير موجود " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }




            tmpusr.FAQDetails = faq.FAQDetails;
           
            tmpusr.FAQId = faq.FAQId;
            tmpusr.FAQTitle = faq.FAQTitle;


            _unitOfWork.FAQs.Add(tmpusr);
            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = null;
            clientMessage.ClientMessageContent = new List<string>() {"تم الحفظ" };
            return Json(clientMessage, JsonRequestBehavior.AllowGet);


        }

        // GET: /TrainingServices/DeleteFAQToDb
        public JsonResult DeleteFAQToDb(FAQ faq)
        {
            var clientMessage = new ClientMessage<string>();

            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "EditFAQ"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }
         
            var exsistCategory =
               _unitOfWork.FAQs.FindAll().FirstOrDefault(c => (c.FAQId == faq.FAQId));

            if (exsistCategory != null)
            {

              _unitOfWork.FAQs.Remove(exsistCategory);

        
            }


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = null;
            clientMessage.ClientMessageContent = new List<string>() { "" };

            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        #endregion
        //    GET: /Admin/GetRoleDetailsByRoleId
        public JsonResult GetRoleDetailsByRoleId(int roleId)
        {
            var clientMessage = new ClientMessage<IQueryable>();

            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "Editrule"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية" };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }

            var allPrivilage = new List<PrivilegeDetails>();
            var privileges = _unitOfWork.PrivilegeActions.FindAll();


            foreach (var sysPrivilegeAction in privileges)
            {
                var privilage = new PrivilegeDetails
                {
                    SysActionId = sysPrivilegeAction.Id.ToString(),
                    SysControllerId = sysPrivilegeAction.ControllerName,
                    SysPrivilegeId = Convert.ToDecimal(sysPrivilegeAction.PrivilegeId),
                    SysPrivilegeCategoryId = Convert.ToDecimal(_unitOfWork.Privileges.FindAll().First(p => p.Id == sysPrivilegeAction.PrivilegeId)
                                .PrivilegeCategoryId)
                };
                privilage.SysPrivilegeCategoryName = _unitOfWork.PrivilegeCategories.FindAll().First(p => p.Id == privilage.SysPrivilegeCategoryId)
                        .Name;
                privilage.SysPrivilegeDisplayName =
                    _unitOfWork.Privileges.FindAll().First(p => p.Id == sysPrivilegeAction.PrivilegeId)
                        .Name;

                privilage.SysPrivilageState =
                    _unitOfWork.RulePrivileges.FindAll().FirstOrDefault(
                        r => r.RuleId == roleId && r.PrivilegeId == sysPrivilegeAction.PrivilegeId) !=
                    null
                        ? true
                        : false;
                allPrivilage.Add(privilage);

            }
            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = allPrivilage.AsQueryable();


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = allPrivilage.AsQueryable();

            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/SaveRoleDetails
        public JsonResult SaveRoleDetails(List<PrivilegeRule> roles)
        {
            var clientMessage = new ClientMessage<IQueryable>();

            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "Admin", "Editrule"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "عفوا ليس لديك صلاحية" };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }

            var privoles = _unitOfWork.RulePrivileges.Find(c => c.RuleId == roles.First().SysRoleId);
            _unitOfWork.RulePrivileges.RemoveAll(privoles);

            //dataContext.ExecuteCommand("Delete from SYS_RulePrivileges WHERE SYS_RuleID=" + roles.First().SysRoleId);
            //dataContext.SubmitChanges();

            foreach (var role in roles)
            {
                if (role.SysPrivilageState == true)
                {
                    var sysrole = new RulePrivilege { PrivilegeId = role.SysPrivilegeId, RuleId = role.SysRoleId };
                    _unitOfWork.RulePrivileges.Add(sysrole);
                }
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string>() { "تم الحفظ" };
            }

            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }


    }
}

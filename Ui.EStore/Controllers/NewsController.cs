using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.EStore;
using Ui.EStore.Helpers;
using Domain.EStore.Repositories;

namespace Ui.EStore.Controllers
{
    public class NewsController : Controller
    {
        readonly IUnitOfWork _unitOfWork = new UnitOfWork();

        #region ActionResult
        public ActionResult AddNew()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "New", "AddNew"))
            {
                return RedirectToAction("Error403", "Home");
            }
            return View();
        }

        public ActionResult EditNew()
        {
            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "New", "EditNew"))
            {
                return RedirectToAction("Error403", "Home");
            }
            return View();
        }
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region SysNew

        [HttpPost]
        public JsonResult SaveSysNew(SysNew sysNew)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();

            if (sysNew.Id <= 0)
            {

                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "New", "AddNew"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "New", "EditNew"))
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };
                    return Json(clientMessage, JsonRequestBehavior.AllowGet);
                }

               
            }
            var isSaved = _unitOfWork.SysNews.Add(sysNew);
            if (isSaved)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تم حفظ الخبر  " };
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
        public JsonResult GetAllSysNews()
        {
            var clientMessage = new ClientMessage<IQueryable>();
            var categories = _unitOfWork.SysNews.FindAll().ToList().Select(r => new
            {
                r.Id,
                r.Berif,
                r.Details,
                r.ImageUrl,
                r.Title
               
            }).AsQueryable();

            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }

  

        [HttpGet]
        public JsonResult GetSysNew(int id)
        {
            var clientMessage = new ClientMessage<SysNew>();
            var categories = _unitOfWork.SysNews.FindFirstOrDefault(id);
            if (categories == null)
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "تحقق من الخبر المختارة " };
                return Json(clientMessage, JsonRequestBehavior.AllowGet);
            }


            clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
            clientMessage.ReturnedData = categories;
            return Json(clientMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSysNew(int id)
        {
            var clientMessage = new ClientMessage<string>();

            Security.CheckLogin();
            if (!Security.IsAuthorized(Enums.PrivilegeType.Action, "New", "DeleteNew"))
            {
                clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                clientMessage.ReturnedData = null;
                clientMessage.ClientMessageContent = new List<string> { "عفوا ليس لديك صلاحية " };

            }
            if (id > 0)
            {
                var categories = _unitOfWork.SysNews.FindFirstOrDefault(id);
                if (categories == null)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Error;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تحقق من الخبر  " };

                }

                var isDeleted = _unitOfWork.SysNews.Remove(categories);
                if (isDeleted)
                {
                    clientMessage.ClientStatusCode = Enums.OperationStatus.Ok;
                    clientMessage.ReturnedData = null;
                    clientMessage.ClientMessageContent = new List<string> { "تم حذف الخبر " };
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


    }
}

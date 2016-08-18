using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.EStore;
using Domain.EStore.Repositories;

namespace Ui.EStore.Controllers
{
    public class AboutUsController : Controller
    {
        //
        // GET: /AboutUs/

        public ActionResult Index()
        {
            return View();
        }

        public static HomeDetail AboutUsDetails()
        {
            var _unitOfWork = new UnitOfWork();
          return _unitOfWork.HomeDetails.FindById(1);
        }
    }
}

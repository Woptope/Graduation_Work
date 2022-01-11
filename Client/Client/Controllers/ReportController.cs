using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            return View("~/Views/Reports/EventParamsView.cshtml");
        }
        public ActionResult ShowHomeworkParams()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            return View("~/Views/Reports/HomeworkParamsView.cshtml");
        }

        public ActionResult ShowEventReport(ReportParametersModel reportParametersModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }
            return View("~/Views/Reports/ReportsView.cshtml", reportParametersModel);
        }

        public ActionResult ShowHomeworkReport(ReportParametersModel reportParametersModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }
            return View("~/Views/Reports/HomeworkReportView.cshtml", reportParametersModel);
        }
    }
}
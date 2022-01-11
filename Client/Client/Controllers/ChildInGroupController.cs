using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ChildInGroupController : Controller
    {
        private ChildInGroupService _childInGroupService;
        public ChildInGroupController()
        {
            _childInGroupService = new ChildInGroupService();
        }

        [HttpPost]
        public ActionResult AddChildInGroup(GroupModel groupModel)//добавление пользователя
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            if (!ModelState.IsValid)
                        {
                            return View("~/Views/Users/AddUserView.cshtml", model);
                        }*/
            try
            {
                _childInGroupService.AddChildInGroup(new ChildInGroup()
                {
                    ChildId = groupModel.ChildId,
                    GroupId = groupModel.GroupId,
                });
            }
            catch (Exception)
            {
                return View("~/Views/Group/ShowGroupView.cshtml", groupModel);
            }

            return RedirectToAction("ShowGroup", "Group", groupModel);
        }

        public ActionResult DeleteChildInGroup(Guid childId, Guid groupId)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _childInGroupService.DeleteChildInGroup(childId, groupId);
            return RedirectToAction("ShowGroup", "Group", new { groupId = groupId});
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherServer;
using OpenSchool.Models;

namespace OpenSchool.Controllers
{
    public class AuthController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();   


        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            var currentUser = db.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                                .ToList()[0];

            var options = new PusherOptions();
            options.Cluster = "us2";

            var pusher = new Pusher(
            "530632",
            "ed5c2354a385822ffe87",
            "0314372fa7f33ce2837a", options);

            if (channel_name.IndexOf(currentUser.Id.ToString()) == -1)
            {
                return Json(
                  new { status = "error", message = "User cannot join channel" }
                );
            }

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);
        }
    }
}
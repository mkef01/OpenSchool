using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenSchool.Models;
using PusherServer;

namespace OpenSchool.Controllers
{
    public class ChatController : Controller
    {

        private Pusher pusher;

        //class constructor
        public ChatController()
        {
            var options = new PusherOptions();
            options.Cluster = "us2";

            pusher = new Pusher(
            "530632",
            "ed5c2354a385822ffe87",
            "0314372fa7f33ce2837a", options);
        }

        [HttpPost]
        public JsonResult SendMessage(string message,string contact, string socket_id)
        {
            ApplicationDbContext bd = new ApplicationDbContext();
            var user = bd.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                                 .ToList();
            var currentUser = user[0];

            MensajeChat convo = new MensajeChat
            {
                sender_id = currentUser.Id,
                message = message,
                receiver_id = contact,
                created_at = DateTime.UtcNow
            };

            using (var db = new Models.ApplicationDbContext())
            {
                db.MensajeChat.Add(convo);
                db.SaveChanges();
            }

            var conversationChannel = getConvoChannel(currentUser.Id, contact);

            pusher.TriggerAsync(
              conversationChannel,
              "new_message",
              convo,
              new TriggerOptions() { SocketId = socket_id });

            return Json(new { status = "success", data = convo },
            JsonRequestBehavior.AllowGet);
        }

        private String getConvoChannel(string user_id, string contact_id)
        {
            if (Convert.ToInt32(user_id.ToCharArray()[0]) > Convert.ToInt32(contact_id.ToCharArray()[0]))
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }

            return "private-chat-" + user_id + "-" + contact_id;
        }
        // GET: Chat
        [Authorize]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Coordinador"
                                   from user in role.Users
                                   select user.UserId;
            var users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            if (User.IsInRole("Administrador"))
            {
                roleUserIdsQuery = from role in db.Roles
                                       where role.Name == "Administrador" || role.Name == "Coordinador"
                                       from user in role.Users
                                       select user.UserId;
                 users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            }else if (User.IsInRole("Coordinador") && User.IsInRole("Profesor"))
            {
                 roleUserIdsQuery = from role in db.Roles
                                       where role.Name == "Administrador" || role.Name == "Profesor" || role.Name == "Alumno" || role.Name =="Coordinador"
                                       from user in role.Users
                                       select user.UserId;
                users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            }
            else if (User.IsInRole("Coordinador"))
            {
                roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Administrador" || role.Name == "Profesor" || role.Name == "Coordinador"
                                   from user in role.Users
                                   select user.UserId;
                users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            }
            else if (User.IsInRole("Profesor"))
            {
                roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Coordinador" || role.Name == "Profesor" || role.Name == "Alumno"
                                   from user in role.Users
                                   select user.UserId;
                users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            }
            else
            {
                roleUserIdsQuery = from role in db.Roles
                                   where role.Name == "Profesor" || role.Name == "Alumnos"
                                   from user in role.Users
                                   select user.UserId;
                users = db.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
            }

             var currentUser = db.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                                 .ToList()[0];
                ViewBag.allUsers = users.Where(u => u.UserName != currentUser.UserName)
                                 .ToList();
                
                ViewBag.currentUser = currentUser;
            


            


            return View();
        }

        public JsonResult ConversationWithContact(string contact)
        {
            ApplicationDbContext bd = new ApplicationDbContext();
            var user = bd.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
                                 .ToList(); 
            var currentUser = user[0];

            var conversations =  bd.MensajeChat.
                                  Where(c => (c.receiver_id == currentUser.Id
                                      && c.sender_id == contact) ||
                                      (c.receiver_id == contact
                                      && c.sender_id == currentUser.Id))
                                  .OrderBy(c => c.created_at)
                                  .Select(c => new
                                  {
                                      id = c.id,
                                      receiver_id = c.receiver_id,
                                      message = c.message,
                                      created_at = c.created_at,
                                      sender_id = c.sender_id,
                                      status = c.status
                                  })
                                  .ToList();
            

            return Json(
                new { status = "success", data = conversations },
                JsonRequestBehavior.AllowGet
            );
        }

        //[HttpPost]
        //public JsonResult SendMessage(string message, string receiver)
        //{
        //    ApplicationDbContext bd = new ApplicationDbContext();
        //    var user = bd.UsuariosAsps.Where(u => u.UserName == User.Identity.Name)
        //                         .ToList();
        //    var currentUser = user[0];

            
        //    MensajeChat convo = new MensajeChat
        //    {
        //        sender_id = currentUser.Id,
        //        message = message,
        //        receiver_id = receiver,
        //        created_at = DateTime.UtcNow
        //    };

        //    using (var db = new Models.ApplicationDbContext())
        //    {
        //        db.MensajeChat.Add(convo);
        //        db.SaveChanges();
        //    }

        //    return Json(convo);
        //}
    }
}
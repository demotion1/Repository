using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Finovex;

namespace Finovex.Controllers
{
    public class meController : ApiController
    {
        private FinovexEntities db = new FinovexEntities();

        // GET: api/me
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        // GET: api/me/5
        [ResponseType(typeof(user))]
        public IEnumerable<user> Getuser(int userid)
        {
            try
            {
                // Authenticate
                // return BadRequest("404");

                user user = db.users.Find(userid);
                if (user == null)
                {
                    return null;
                }

                List<user> listOfusers = new List<user>();

                user userList = new user();
                userList.userid = user.userid;
                userList.firstname = user.firstname;
                userList.lastname = user.lastname;
                userList.email = user.email;
                userList.password = user.password;
                userList.active = user.active;
                listOfusers.Add(userList);

                IEnumerable<user> users = listOfusers;

                return users;

                //return Ok("200");
            }
            catch
            {
                return null; // return BadRequest("500");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.userid == id) > 0;
        }
    }
}
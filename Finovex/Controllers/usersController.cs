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
using System.Security.Cryptography;

namespace Finovex.Controllers
{
    public class usersController : ApiController
    {
        private FinovexEntities db = new FinovexEntities();
        
        // DSA - Register - POST: api/users
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("500");
            }

            try
            {
                // DSA - Validate user email
                var foundUser = (from j in db.users
                                 where j.email == user.email && j.active == true
                                 select j);
                int count = foundUser.Count();
                if ((count > 0) ||
                    (user.firstname == "" || user.lastname == "" || user.email == "" || user.password == ""))
                {
                    return BadRequest("400");
                }

                // DSA - save user
                user.active = true;
                user.create_time = DateTime.Now;

                db.users.Add(user);
                await db.SaveChangesAsync();

                return Ok("201");
            }
            catch (System.Exception e)
            {
                return BadRequest("500");
            }
        }

        // GET: api/users/id - Login
        public string Getuser(string email, string password)
        {
            var users = (from j in db.users
                        where j.email == email && j.password == password && j.active == true
                        select j);

            if (users.Count() != 1)
                return "500";
            else
                return users.FirstOrDefault().userid.ToString();
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
            return db.users.Count(e => e.userid == id && e.active==true) > 0;
        }
    }
}
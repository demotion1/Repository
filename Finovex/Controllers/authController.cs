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
    public class authController : ApiController
    {
        private FinovexEntities db = new FinovexEntities();

        // POST: api/auth
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("500");
            }

            try
            {

            var users = (from j in db.users
                         where j.email == user.email && j.password == user.password && j.active == true
                         select j);

            if (users.Count() != 1)
                return BadRequest("401");

            return Ok(users.FirstOrDefault().userid.ToString());  //Ok("204");
            }
            catch
            {
                return BadRequest("500");
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
        
    }
}
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
    public class logoutController : ApiController
    {
        private FinovexEntities db = new FinovexEntities();
        
        // POST: api/logout
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("500");
            }

            // Authenticate user / remove token
            // eturn BadRequest("403");

            return Ok("204");
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
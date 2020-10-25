using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APIBaitap.Models;

namespace APIBaitap.Controllers
{
    public class sanphamsController : ApiController
    {
        private CuaHangEntities4 db = new CuaHangEntities4();

        // GET: api/sanphams
        public IQueryable<sanpham> Getsanphams()
        {
            return db.sanphams;
        }

        // GET: api/sanphams/5
        [ResponseType(typeof(sanpham))]
        public IHttpActionResult Getsanpham(int id)
        {
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return Ok(sanpham);
        }
    }
}
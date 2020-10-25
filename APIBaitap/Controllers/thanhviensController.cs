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
    public class thanhviensController : ApiController
    {
        private CuaHangEntities4 db = new CuaHangEntities4();

        // GET: api/thanhviens
        public IQueryable<thanhvien> Getthanhviens()
        {
            return db.thanhviens;
        }

        // GET: api/thanhviens/5
        [ResponseType(typeof(thanhvien))]
        public IHttpActionResult Getthanhvien(int id)
        {
            thanhvien thanhvien = db.thanhviens.Find(id);
            if (thanhvien == null)
            {
                return NotFound();
            }

            return Ok(thanhvien);
        }

        // PUT: api/thanhviens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putthanhvien(int id, thanhvien thanhvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thanhvien.matv)
            {
                return BadRequest();
            }

            db.Entry(thanhvien).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

   

        // DELETE: api/thanhviens/5
        [ResponseType(typeof(thanhvien))]
        public IHttpActionResult Deletethanhvien(int id)
        {
            thanhvien thanhvien = db.thanhviens.Find(id);
            if (thanhvien == null)
            {
                return NotFound();
            }

            db.thanhviens.Remove(thanhvien);
            db.SaveChanges();

            return Ok(thanhvien);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Exists(int id)
        {
            return db.thanhviens.Count(e => e.matv == id) > 0;
        }
    }
}
       
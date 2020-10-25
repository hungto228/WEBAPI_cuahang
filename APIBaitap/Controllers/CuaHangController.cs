using APIBaitap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace APIBaitap.Controllers
{
    public class CuaHangController : ApiController
    {
        private CuaHangEntities4 db = new CuaHangEntities4();
        // GET: api/SanPhams
        [Route("CuaHang/GetAll")]
        public IQueryable<sanpham> GetSanPhams()
        {
            return db.sanphams;
        }

        // GET: api/SanPhams/5
        [Route("CuaHang/getSP_id/{id}")]
        [ResponseType(typeof(sanpham))]
        public IHttpActionResult GetSanPham(string id)
        {
            sanpham sanPham = db.sanphams.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        // Get sản phẩm theo mã loại sản phẩm
        [HttpGet]
        [Route("CuaHang/getSP_Maloai/{maloai}")]
        public IHttpActionResult getSP_MaDanhMuc(string maloai)
        {
            return Ok(db.SP_GETMALoai(maloai));
        }


        // Get sản phẩm theo tên sản phẩm
        [HttpGet]
        [Route("CuaHang/getSP_ten/{ten}")]
        public IHttpActionResult getSP_Ten(string ten)
        {
            return Ok(db.SP_GETTEN(ten));
        }


        // Get sản phẩm theo giá sản phẩm
        [HttpGet]
        [Route("CuaHang/getSP_giaMinMax/{GiaMin}/{GiaMax}")]
        public IHttpActionResult getSP_GiaMinMax(int GiaMin, int GiaMax)
        {
            return Ok(db.getSP_giaMinMax(GiaMin, GiaMax));
        }


        // Get sản phẩm mới theo mã loại
        [HttpGet]
        [Route("CuaHang/getnewSP/{idsanpham}")]
        public IHttpActionResult getSP_laptop(string idsanpham)
        {
            return Ok(db.SP_GETNew(idsanpham));
        }
       
        // get hoa đon
        [HttpGet]
        [Route("CuaHang/getdonhang/{id}")]
        public IHttpActionResult getdonhang(string id)
        {
            return Ok(db.gethoadon(id));
        }


        //get hóa đơn theo trạng thái của người dùng
        [HttpGet]
        [Route("CuaHang/get_hoadonUser/{trangthai}/{matv}")]
        public IHttpActionResult getdonhang(int trangthai, int matv)
        {
            return Ok(db.get_hoadonUser(trangthai, matv));
        }


        // get hoa don theo trang thai all admin
        [HttpGet]
        [Route("CuaHang/get_hoadonAdmin/{trangthai}")]
        public IHttpActionResult getdonhangAdmin(int trangthai)
        {
            return Ok(db.get_hoadonAdmin(trangthai));
        }


        // get all hoa don
        [HttpGet]
        [Route("CuaHang/getallhoadon")]
        public IHttpActionResult getalldonhang()
        {
            return Ok(db.get_allhoadon());
        }

        // đăng nhập
        [HttpGet]
        [Route("CuaHang/getUser/{tendangnhap}/{matkhau}")]
        public IHttpActionResult getUser(string tendangnhap, string matkhau)
        {
            return Ok(db.get_user(tendangnhap, matkhau));
        }


        // đăng ký tài khoản
        // POST: api/thanhviens
        [Route("api/addthanhvien")]
        [ResponseType(typeof(thanhvien))]
        [HttpPost]
        public IHttpActionResult Postthanhvien([FromBody] thanhvien thanhvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.thanhviens.Add(thanhvien);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (thanhvienExists(thanhvien.tendangnhap))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = thanhvien.matv }, thanhvien);
        }
        private bool thanhvienExists(string tendangnhap)
        {
            return db.thanhviens.Count(e => e.tendangnhap == tendangnhap) > 0;
        }
        private bool Exists(int id)
        {
            return db.thanhviens.Count(e => e.matv == id) > 0;
        }

        // Update sản phẩm
        // PUT: api/sanphams/5
        [Route("api/updatesanpham/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Putsanpham(int id, sanpham sanpham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanpham.Id)
            {
                return BadRequest();
            }

            db.Entry(sanpham).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sanphamExists(id))
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


        // thêm sản phẩm
        // POST: api/sanphams
        [Route("api/addsanpham")]
        [ResponseType(typeof(sanpham))]
        [HttpPost]
        public IHttpActionResult Postsanpham(sanpham sanpham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sanphams.Add(sanpham);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (sanphamExists(sanpham.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sanpham.Id }, sanpham);
        }


        // xóa sản phẩm
        // DELETE: api/sanphams/5
        [Route("api/deletesanpham/{id}")]
        [ResponseType(typeof(sanpham))]
        [HttpDelete]
        public IHttpActionResult Deletesanpham(int id)
        {
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            db.sanphams.Remove(sanpham);
            db.SaveChanges();

            return Ok(sanpham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sanphamExists(int id)
        {
            return db.sanphams.Count(e => e.Id == id) > 0;
        }

        // thống kê tháng
        [HttpGet]
        [Route("CuaHang/thongkethang/{year}")]
        public IHttpActionResult thongkethang(string year)
        {
            return Ok(db.thongkethang(year));
        }

        //thống kê năm
        [HttpGet]
        [Route("CuaHang/thongkenam/{year}")]
        public IHttpActionResult thongkenam(string year)
        {
            return Ok(db.thongkenam(year));
        }

    }
}

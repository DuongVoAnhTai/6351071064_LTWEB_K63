using MvcBookStore.Models;
using MvcBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBookStore.Controllers
{
    public class GioHangController : Controller
    {
        QLBANSACHEntities data = new QLBANSACHEntities();

        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<Giohang> Laygiohang()
        {
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if(listGiohang == null)
            {
                listGiohang = new List<Giohang>();
                Session["Giohang"] = listGiohang;
            }
            return listGiohang;
        }

        public ActionResult ThemGiohang (int iMasach, string strURL)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.Find(n => n.iMasach == iMasach);
            if(sanpham == null)
            {
                sanpham = new Giohang(iMasach);
                listGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if(listGiohang != null)
            {
                iTongSoLuong = listGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang != null)
            {
                iTongTien = listGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }

        public ActionResult Giohang()
        {
            List<Giohang> listGiohang = Laygiohang();
            if(listGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGiohang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang (int iMaSP)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if(sanpham != null)
            {
                listGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("GioHang");
            }
            if(listGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if(sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult XoaTacaGiohang()
        {
            List<Giohang> listGiohang = Laygiohang();
            listGiohang.Clear();
            return RedirectToAction("Index", "BookStore");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            // Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }

            // Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            // Them Don hang
            DONDATHANG ddh = new DONDATHANG();

            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();

            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;

            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);

            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;

            data.DONDATHANGs.Add(ddh);
            data.SaveChanges();

            // Them chi tiet don hang
            foreach (var item in gh)
            {
                CHITIETDONTHANG ctdh = new CHITIETDONTHANG();

                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.Masach = item.iMasach;
                ctdh.Soluong = item.iSoluong;
                ctdh.Dongia = (decimal)item.dDonggia;

                data.CHITIETDONTHANGs.Add(ctdh);
                data.SaveChanges();
            }

            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}
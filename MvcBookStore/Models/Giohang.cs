using MvcBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public class Giohang
    {
        QLBANSACHEntities data = new QLBANSACHEntities();

        public int iMasach {  get; set; }
        public string sTensach {  get; set; }
        public string sAnhbia {  get; set; }
        public Double dDonggia {  get; set; }
        public int iSoluong {  get; set; }
        public Double dThanhtien
        {
            get { return iSoluong * dDonggia; }
        }
        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDonggia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }
    }
}
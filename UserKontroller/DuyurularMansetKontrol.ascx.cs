﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DuyurularMansetKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuyurulariVer();
            }
        }

        private void DuyurulariVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HTList = (from p in Veriler.DuyurularTablosu
                          join p1 in Veriler.DuyuruKategorileri
                              on p.KategoriId equals p1.Id
                          join p2 in Veriler.Duyurular
                              on p.DuyuruId equals p2.Id
                          where p1.DilId == DilId
                                && p1.Durum == true
                                && p2.Durum == true
                          orderby p2.BaslangicTarihi ascending
                          select new
                                     {
                                         Id1 = p2.Id,
                                         Id2 = Guid.NewGuid(),
                                         Gorsel =
                              p2.Gorsel1 != null
                                  ? p2.Gorsel1.Replace("~/", "")
                                  : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                         GorselThumbnail =
                              p2.GorselThumbnail1 != null
                                  ? p2.GorselThumbnail1.Replace("~/", "")
                                  : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                         p2.Baslik,
                                         p2.Ozet
                                     }).Distinct().Take(6).ToList();
            RepeaterSag.DataSource = HTList;
            RepeaterSol.DataSource = HTList;
            RepeaterSag.DataBind();
            RepeaterSol.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Duyurular D = Veriler.Duyurular.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/duyurudetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik);
            HL.Text = "[devamı]";
        }

        protected void HyperLink2_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Duyurular D = Veriler.Duyurular.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/duyurudetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik);
        }
    }
}
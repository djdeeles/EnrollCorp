﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class EtkinliklerMansetKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikleriVer();
            }
        }

        private void EtkinlikleriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EList = (from p in Veriler.EtkinlikKategorileri
                         join p1 in Veriler.Etkinlikler
                             on p.Id equals p1.EtkinlikKategoriId
                         where p.DilId == DilId
                               && p.Durum == true
                               && p1.Durum == true
                         orderby p1.BaslangicTarihi ascending
                         select new
                                    {
                                        Id1 = p1.Id,
                                        Id2 = Guid.NewGuid(),
                                        Gorsel =
                             p1.Gorsel1 != null
                                 ? p1.Gorsel1.Replace("~/", "")
                                 : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                        GorselThumbnail =
                             p1.GorselThumbnail1 != null
                                 ? p1.GorselThumbnail1.Replace("~/", "")
                                 : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                        Baslik = p1.Ad,
                                        p1.Ozet
                                    }).Distinct().Take(6).ToList();
            RepeaterSag.DataSource = EList;
            RepeaterSol.DataSource = EList;
            RepeaterSag.DataBind();
            RepeaterSol.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Ad);
            HL.Text = "[devamı]";
        }

        protected void HyperLink2_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Ad);
        }
    }
}
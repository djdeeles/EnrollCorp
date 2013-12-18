using System;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class SayfaIcerik : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        IcerikVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void IcerikVer(int MenuId)
        {
            Menuler M = Veriler.Menuler.Where(p => p.Id == MenuId && p.Durum == true).FirstOrDefault();
            if (M != null)
            {
                M.OkunmaSayisi = M.OkunmaSayisi + 1;
                Veriler.SaveChanges();
                if (!string.IsNullOrEmpty(M.BannerGorsel))
                {
                    Resim.Style.Add("background-image", M.BannerGorsel);
                }
                else
                {
                    Resim.Style.Add("background-image", "../../App_Themes/PendikMainTheme/Images/Default_banner.png");
                }
                if (M.BasligiGöster.Value)
                {
                    LabelIcerik.Text = "<span style='font-weight:bold; font-size:16px;'>"
                                       + M.MenuAdi + " </span><br /><br />";
                }
                LabelIcerik.Text = LabelIcerik.Text + M.Icerik;
                Page.Title = Page.Header.Title + " :: " + M.MenuAdi;
                Page.MetaKeywords = M.AnahtarKelimeler;
                Page.MetaDescription = M.Aciklama;
                string S = SiteHatritasiVer(Convert.ToInt32(M.MenuLokasyonId), Convert.ToInt32(M.UstMenuId));
                var SS = S.Split('/');
                string SSS = string.Empty;
                for (int i = SS.Length - 1; i >= 0; i--)
                {
                    if (SS[i] != "")
                    {
                        int Id = Convert.ToInt32(SS[i]);
                        SSS += SiteMapMenuVer(Id) + " / ";
                    }
                }
                LabelSiteMap.Text = "<a href='../../anasayfa'>Anasayfa</a> / " + SSS + Enroll.IlkHarfBuyuk(M.MenuAdi);
                if (M.UstMenuId != 0)
                {
                    LabelUstMenuBaslik.Text = Veriler.Menuler.Where(p => p.Id == M.UstMenuId).First().MenuAdi;
                }
                else
                {
                    switch (M.MenuLokasyonId)
                    {
                        case 1:
                            LabelUstMenuBaslik.Text = "Pendik Belediyesi";
                            break;
                        case 2:
                            LabelUstMenuBaslik.Text = "Pendik Belediyesi";
                            break;
                        case 3:
                            LabelUstMenuBaslik.Text = "Başkan KENAN ŞAHİN";
                            break;
                        case 4:
                            LabelUstMenuBaslik.Text = "Pendik Belediyesi";
                            break;
                        case 5:
                            LabelUstMenuBaslik.Text = string.Empty;
                            break;
                    }
                }
            }
            else
            {
                Response.Redirect("../../anasayfa");
            }
        }

        public string SiteHatritasiVer(int LokasyonId, int UstMenuId)
        {
            string Sonuc = string.Empty;
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var MList =
                Veriler.Menuler.Where(
                    p => p.Durum == true && p.MenuLokasyonId == LokasyonId && p.MenudeGoster == true && p.DilId == DilId)
                    .ToList();
            foreach (Menuler M in MList)
            {
                if (M.Id == UstMenuId)
                {
                    Sonuc = Sonuc + M.Id + "/";
                    if (M.UstMenuId != 0)
                    {
                        Sonuc = SiteHatritasiVer(Convert.ToInt32(M.MenuLokasyonId), Convert.ToInt32(M.UstMenuId));
                    }
                }
            }
            return Sonuc;
        }

        public string SiteMapMenuVer(int MenuId)
        {
            string Sonuc = string.Empty;
            Menuler M = Veriler.Menuler.Where(p => p.Id == MenuId).First();
            switch (M.MenuTipId)
            {
                case 1:
                    Sonuc = "<a>" + Enroll.IlkHarfBuyuk(M.MenuAdi) + "</a>";
                    break;
                case 2:
                    if (M.Url.Contains("http://"))
                    {
                        Sonuc = "<a href='" + M.Url + "'>" + Enroll.IlkHarfBuyuk(M.MenuAdi) + "</a>";
                    }
                    else
                    {
                        Sonuc = "<a href='../../" + M.Url + "'>" + Enroll.IlkHarfBuyuk(M.MenuAdi) + "</a>";
                    }
                    break;
                case 3:
                    Sonuc = "<a href='../../sayfa/" + M.Id + "/" + M.MenuAdi + "'>" + Enroll.IlkHarfBuyuk(M.MenuAdi) +
                            "</a>";
                    break;
            }
            return Sonuc;
        }
    }
}
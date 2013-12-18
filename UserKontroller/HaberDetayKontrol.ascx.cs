using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class HaberDetayKontrol : UserControl
    {
        public static List<Gorseller> GorsellerList;
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        HaberVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void HaberVer(int HaberId)
        {
            Haberler H = Veriler.Haberler.Where(p => p.Id == HaberId && p.Durum == true).FirstOrDefault();
            if (H != null)
            {
                if (H.OkunmaSayisi == null)
                {
                    H.OkunmaSayisi = 1;
                }
                else
                {
                    H.OkunmaSayisi = H.OkunmaSayisi + 1;
                }
                Veriler.SaveChanges();
                LabelBaslik.Text = H.Baslik;
                LabelTarih.Text = H.KayitTarihi.Value.ToString("dd/MM/yyyy");
                LabelIcerik.Text = H.Icerik;
                BannerGorseliVer();
                Page.MetaKeywords = H.AnahtarKelimeler;
                Page.Title = Page.Header.Title + " :: " + H.Baslik;
                GorsellerList = new List<Gorseller>();
                Gorseller G;
                if (!string.IsNullOrEmpty(H.GorselThumbnail1))
                {
                    G = new Gorseller();
                    G.Gorsel = H.Gorsel1.Replace("~/", "");
                    G.GorselThumbnail = H.GorselThumbnail1.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(H.GorselThumbnail2))
                {
                    G = new Gorseller();
                    G.Gorsel = H.Gorsel2.Replace("~/", "");
                    G.GorselThumbnail = H.GorselThumbnail2.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(H.GorselThumbnail3))
                {
                    G = new Gorseller();
                    G.Gorsel = H.Gorsel3.Replace("~/", "");
                    G.GorselThumbnail = H.GorselThumbnail3.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(H.GorselThumbnail4))
                {
                    G = new Gorseller();
                    G.Gorsel = H.Gorsel4.Replace("~/", "");
                    G.GorselThumbnail = H.GorselThumbnail4.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (GorsellerList.Count > 1)
                {
                    DataListResimler.DataSource = GorsellerList;
                    DataListResimler.DataBind();
                }
                if (GorsellerList.Count != 0)
                {
                    ImageHaberGorsel.ImageUrl = "../" + GorsellerList[0].Gorsel;
                }
                else
                {
                    ImageHaberGorsel.ImageUrl = "/App_Themes/PendikMainTheme/Images/noimage.png";
                }
            }
            else
            {
                Response.Redirect("../../haberler/0/tumhaberler/1");
            }
        }

        private void BannerGorseliVer()
        {
            HaberlerBannerResmi HBR = Veriler.HaberlerBannerResmi.FirstOrDefault();
            if (HBR != null)
            {
                if (!string.IsNullOrEmpty(HBR.Resim))
                {
                    Resim.Style.Add("background-image", HBR.Resim);
                }
                else
                {
                    Resim.Style.Add("background-image", "../../App_Themes/PendikMainTheme/Images/Default_banner.png");
                }
            }
            else
            {
                Resim.Style.Add("background-image", "../../App_Themes/PendikMainTheme/Images/Default_banner.png");
            }
        }

        #region Nested type: Gorseller

        public class Gorseller
        {
            public string Gorsel { get; set; }
            public string GorselThumbnail { get; set; }
        }

        #endregion
    }
}
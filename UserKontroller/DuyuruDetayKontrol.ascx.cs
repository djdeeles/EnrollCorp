using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DuyuruDetayKontrol : UserControl
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
                        DuyuruVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void DuyuruVer(int DuyuruId)
        {
            Duyurular D = Veriler.Duyurular.Where(p => p.Id == DuyuruId && p.Durum == true).FirstOrDefault();
            if (D != null)
            {
                if (D.OkunmaSayisi == null)
                {
                    D.OkunmaSayisi = 1;
                }
                else
                {
                    D.OkunmaSayisi = D.OkunmaSayisi + 1;
                }
                Veriler.SaveChanges();
                LabelBaslik.Text = D.Baslik;
                LabelTarih.Text = D.BaslangicTarihi.Value.ToString("dd/MM/yyyy");
                LabelIcerik.Text = D.Icerik;
                BannerGorseliVer();
                Page.MetaKeywords = D.AnahtarKelimeler;
                Page.Title = Page.Header.Title + " :: " + D.Baslik;
                GorsellerList = new List<Gorseller>();
                Gorseller G;
                if (!string.IsNullOrEmpty(D.GorselThumbnail1))
                {
                    G = new Gorseller();
                    G.Gorsel = D.Gorsel1.Replace("~/", "");
                    G.GorselThumbnail = D.GorselThumbnail1.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(D.GorselThumbnail2))
                {
                    G = new Gorseller();
                    G.Gorsel = D.Gorsel2.Replace("~/", "");
                    G.GorselThumbnail = D.GorselThumbnail2.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(D.GorselThumbnail3))
                {
                    G = new Gorseller();
                    G.Gorsel = D.Gorsel3.Replace("~/", "");
                    G.GorselThumbnail = D.GorselThumbnail3.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(D.GorselThumbnail4))
                {
                    G = new Gorseller();
                    G.Gorsel = D.Gorsel4.Replace("~/", "");
                    G.GorselThumbnail = D.GorselThumbnail4.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (GorsellerList.Count > 1)
                {
                    DataListResimler.DataSource = GorsellerList;
                    DataListResimler.DataBind();
                }
                if (GorsellerList.Count != 0)
                {
                    ImageDuyuruGorsel.ImageUrl = "../" + GorsellerList[0].Gorsel;
                }
                else
                {
                    ImageDuyuruGorsel.ImageUrl = "/App_Themes/PendikMainTheme/Images/noimage.png";
                }
            }
            else
            {
                Response.Redirect("../../duyurular/0/tumduyurular/1");
            }
        }

        private void BannerGorseliVer()
        {
            DuyurularBannerResmi DBR = Veriler.DuyurularBannerResmi.FirstOrDefault();
            if (DBR != null)
            {
                if (!string.IsNullOrEmpty(DBR.Resim))
                {
                    Resim.Style.Add("background-image", DBR.Resim);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class IhaleDetayKontrol : UserControl
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
                        IhaleVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void IhaleVer(int IhaleId)
        {
            Ihaleler I = Veriler.Ihaleler.Where(p => p.Id == IhaleId && p.Durum == true).FirstOrDefault();
            if (I != null)
            {
                if (I.OkunmaSayisi == null)
                {
                    I.OkunmaSayisi = 1;
                }
                else
                {
                    I.OkunmaSayisi = I.OkunmaSayisi + 1;
                }
                Veriler.SaveChanges();
                LabelBaslik.Text = I.Ad;
                LabelTarih.Text = I.BaslangicTarihi.Value.ToString("dd/MM/yyyy");
                LabelIcerik.Text = I.Icerik;
                BannerGorseliVer();
                Page.MetaKeywords = I.AnahtarKelimeler;
                Page.Title = Page.Header.Title + " :: " + I.Ad;
                GorsellerList = new List<Gorseller>();
                Gorseller G;
                if (!string.IsNullOrEmpty(I.GorselThumbnail1))
                {
                    G = new Gorseller();
                    G.Gorsel = I.Gorsel1.Replace("~/", "");
                    G.GorselThumbnail = I.GorselThumbnail1.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(I.GorselThumbnail2))
                {
                    G = new Gorseller();
                    G.Gorsel = I.Gorsel2.Replace("~/", "");
                    G.GorselThumbnail = I.GorselThumbnail2.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(I.GorselThumbnail3))
                {
                    G = new Gorseller();
                    G.Gorsel = I.Gorsel3.Replace("~/", "");
                    G.GorselThumbnail = I.GorselThumbnail3.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(I.GorselThumbnail4))
                {
                    G = new Gorseller();
                    G.Gorsel = I.Gorsel4.Replace("~/", "");
                    G.GorselThumbnail = I.GorselThumbnail4.Replace("~/", "");
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
                Response.Redirect("../../ihaleler/0/tumihaleler/1");
            }
        }

        private void BannerGorseliVer()
        {
            IhalelerBannerResmi IBR = Veriler.IhalelerBannerResmi.FirstOrDefault();
            if (IBR != null)
            {
                if (!string.IsNullOrEmpty(IBR.Resim))
                {
                    Resim.Style.Add("background-image", IBR.Resim);
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
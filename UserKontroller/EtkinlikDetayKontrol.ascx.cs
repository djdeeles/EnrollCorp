using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class EtkinlikDetayKontrol : UserControl
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
                        EtkinlikVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void EtkinlikVer(int EtkinlikId)
        {
            Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == EtkinlikId && p.Durum == true).FirstOrDefault();
            if (E != null)
            {
                if (E.OkunmaSayisi == null)
                {
                    E.OkunmaSayisi = 1;
                }
                else
                {
                    E.OkunmaSayisi = E.OkunmaSayisi + 1;
                }
                Veriler.SaveChanges();
                LabelBaslik.Text = E.Ad;
                LabelTarih.Text = E.BaslangicTarihi.Value.ToString("dd/MM/yyyy");
                LabelIcerik.Text = E.Icerik;
                BannerGorseliVer();
                Page.MetaKeywords = E.AnahtarKelimeler;
                Page.Title = Page.Header.Title + " :: " + E.Ad;
                GorsellerList = new List<Gorseller>();
                Gorseller G;
                if (!string.IsNullOrEmpty(E.GorselThumbnail1))
                {
                    G = new Gorseller();
                    G.Gorsel = E.Gorsel1.Replace("~/", "");
                    G.GorselThumbnail = E.GorselThumbnail1.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(E.GorselThumbnail2))
                {
                    G = new Gorseller();
                    G.Gorsel = E.Gorsel2.Replace("~/", "");
                    G.GorselThumbnail = E.GorselThumbnail2.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(E.GorselThumbnail3))
                {
                    G = new Gorseller();
                    G.Gorsel = E.Gorsel3.Replace("~/", "");
                    G.GorselThumbnail = E.GorselThumbnail3.Replace("~/", "");
                    GorsellerList.Add(G);
                }
                if (!string.IsNullOrEmpty(E.GorselThumbnail4))
                {
                    G = new Gorseller();
                    G.Gorsel = E.Gorsel4.Replace("~/", "");
                    G.GorselThumbnail = E.GorselThumbnail4.Replace("~/", "");
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
            EtkinliklerBannerResmi EBR = Veriler.EtkinliklerBannerResmi.FirstOrDefault();
            if (EBR != null)
            {
                if (!string.IsNullOrEmpty(EBR.Resim))
                {
                    Resim.Style.Add("background-image", EBR.Resim);
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
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class EtkinliklerListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: Etkinlikler";
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.QueryString["code"] != "0")
                        {
                            EtkinlikleriVer(Convert.ToInt32(Request.QueryString["code"]));
                        }
                    }
                }
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
                    Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
                }
            }
            else
            {
                Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
            }
        }

        private void EtkinlikleriVer(int KategoriId)
        {
            DateTime Tarih = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (KategoriId != 0)
            {
                var EtkinliklerList = (from p in Veriler.EtkinlikKategorileri
                                       join p1 in Veriler.Etkinlikler
                                           on p.Id equals p1.EtkinlikKategoriId
                                       where p.Id == KategoriId
                                             && p.Durum == true
                                             && p1.Durum == true
                                       orderby p1.BaslangicTarihi descending
                                       select new
                                                  {
                                                      p1.Id,
                                                      p1.Ad,
                                                      p1.BaslangicTarihi,
                                                      p1.Ozet,
                                                      GorselThumbnail1 =
                                           p1.GorselThumbnail1 != null
                                               ? p1.GorselThumbnail1
                                               : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                                  }).ToList();
                ListView1.DataSource = EtkinliklerList;
                ListView1.DataBind();
            }
            else
            {
                var EtkinliklerList = (from p in Veriler.Etkinlikler
                                       where p.Durum == true
                                       orderby p.BaslangicTarihi descending
                                       select new
                                                  {
                                                      p.Id,
                                                      p.Ad,
                                                      p.BaslangicTarihi,
                                                      p.Ozet,
                                                      GorselThumbnail1 =
                                           p.GorselThumbnail1 != null
                                               ? p.GorselThumbnail1
                                               : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                                  }).ToList();
                ListView1.DataSource = EtkinliklerList;
                ListView1.DataBind();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataPager1.PreRender += DataPager1_PreRender;
        }

        private void DataPager1_PreRender(object sender, EventArgs e)
        {
            foreach (Control control in DataPager1.Controls)
            {
                foreach (Control c in control.Controls)
                {
                    if (c is HyperLink)
                    {
                        HyperLink currentLink = (HyperLink) c;
                        if ((!string.IsNullOrEmpty(Request.Url.AbsolutePath)) &&
                            (!string.IsNullOrEmpty(Request.Url.Query)))
                        {
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/etkinliklerlist.aspx?",
                                                                                      "/etkinlikler/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("etkinliklerpage=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("&", "/");
                        }
                    }
                }
            }
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink myHyper = (HyperLink) sender;
            int newsId = Convert.ToInt32(myHyper.NavigateUrl);
            Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == newsId).First();
            myHyper.NavigateUrl = "~/etkinlikdetay/" + newsId + "/" + MenuUrl.MenuUrlDuzenle(E.Ad);
        }
    }
}
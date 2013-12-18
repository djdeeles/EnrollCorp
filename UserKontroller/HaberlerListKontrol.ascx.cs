using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class HaberlerListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: Haberler";
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.QueryString["code"] != "0")
                        {
                            EntityDataSource1.CommandText =
                                "select DISTINCT p.[Id], p.[Baslik], p.[KayitTarihi], p.[Ozet], p.[GorselThumbnail1] from HaberlerTablosu as it join Haberler as p on it.[HaberId]==p.[Id] where it.KategoriId==" +
                                Request.QueryString["code"] + " and p.[Durum]==true order by KayitTarihi desc";
                        }
                        else
                        {
                            EntityDataSource1.CommandText =
                                "select DISTINCT p.[Id], p.[Baslik], p.[KayitTarihi], p.[Ozet], p.[GorselThumbnail1] from Haberler as p where p.[Durum]==true order by KayitTarihi desc";
                        }
                    }
                }
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/haberlerlist.aspx?",
                                                                                      "/haberler/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("haberlerpage=", "");
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
            Haberler H = Veriler.Haberler.Where(p => p.Id == newsId).First();
            myHyper.NavigateUrl = "~/haberdetay/" + newsId + "/" + MenuUrl.MenuUrlDuzenle(H.Baslik);
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

        public string GorselVer(string Gorsel)
        {
            string Sonuc = string.Empty;
            if (!string.IsNullOrEmpty(Gorsel))
            {
                Sonuc = Gorsel;
            }
            else
            {
                Sonuc = "/App_Themes/PendikMainTheme/Images/noimage.png";
            }
            return Sonuc;
        }
    }
}
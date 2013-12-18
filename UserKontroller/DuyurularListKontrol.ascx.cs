using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DuyurularListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: Duyurular";
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.QueryString["code"] != "0")
                        {
                            DuyurulariVer(Convert.ToInt32(Request.QueryString["code"]));
                        }
                        else
                        {
                            DuyurulariVer(0);
                        }
                    }
                }
            }
        }

        private void DuyurulariVer(int KategoriId)
        {
            DateTime Tarih = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (KategoriId != 0)
            {
                var DuyurularList = (from p in Veriler.DuyurularTablosu
                                     join p1 in Veriler.Duyurular
                                         on p.DuyuruId equals p1.Id
                                     join p2 in Veriler.DuyuruKategorileri
                                         on p.KategoriId equals p2.Id
                                     where p.KategoriId == KategoriId
                                           && p2.Durum == true
                                           && p1.Durum == true
                                           && p1.BaslangicTarihi <= Tarih
                                           && (p1.BitisTarihi >= Tarih || p1.BitisTarihi == null)
                                     orderby p1.BaslangicTarihi descending
                                     select new
                                                {
                                                    p1.Id,
                                                    p1.Baslik,
                                                    p1.BaslangicTarihi,
                                                    p1.Ozet,
                                                    GorselThumbnail1 =
                                         p1.GorselThumbnail1 != null
                                             ? p1.GorselThumbnail1
                                             : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                                }).ToList();
                ListView1.DataSource = DuyurularList;
                ListView1.DataBind();
            }
            else
            {
                var DuyurularList = (from p in Veriler.Duyurular
                                     where p.Durum == true
                                           && p.BaslangicTarihi <= Tarih
                                           && (p.BitisTarihi >= Tarih || p.BitisTarihi == null)
                                     orderby p.BaslangicTarihi descending
                                     select new
                                                {
                                                    p.Id,
                                                    p.Baslik,
                                                    p.BaslangicTarihi,
                                                    p.Ozet,
                                                    GorselThumbnail1 =
                                         p.GorselThumbnail1 != null
                                             ? p.GorselThumbnail1
                                             : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                                }).ToList();
                ListView1.DataSource = DuyurularList;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/duyurularlist.aspx?",
                                                                                      "/duyurular/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("duyurularpage=", "");
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
            Duyurular D = Veriler.Duyurular.Where(p => p.Id == newsId).First();
            myHyper.NavigateUrl = "~/duyurudetay/" + newsId + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik);
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
    }
}
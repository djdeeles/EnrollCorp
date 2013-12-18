using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class IhalelerListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: İhaleler";
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.QueryString["code"] != "0")
                        {
                            IhaleleriVer(Convert.ToInt32(Request.QueryString["code"]));
                        }
                    }
                }
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
                    Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
                }
            }
            else
            {
                Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
            }
        }

        private void IhaleleriVer(int KategoriId)
        {
            DateTime Tarih = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (KategoriId != 0)
            {
                var IhalelerList = (from p in Veriler.IhaleKategorileri
                                    join p1 in Veriler.Ihaleler
                                        on p.Id equals p1.IhaleKategoriId
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
                ListView1.DataSource = IhalelerList;
                ListView1.DataBind();
            }
            else
            {
                var IhalelerList = (from p in Veriler.Ihaleler
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
                ListView1.DataSource = IhalelerList;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/ihalelerlist.aspx?",
                                                                                      "/ihaleler/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("ihalelerpage=", "");
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
            Ihaleler I = Veriler.Ihaleler.Where(p => p.Id == newsId).First();
            myHyper.NavigateUrl = "~/ihaledetay/" + newsId + "/" + MenuUrl.MenuUrlDuzenle(I.Ad);
        }
    }
}
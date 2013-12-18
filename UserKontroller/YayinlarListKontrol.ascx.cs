using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class YayinlarListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        LabelSiteMap.Text = SiteMapVer();
                        YayinlariVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
        }

        private void BannerGorseliVer()
        {
            YayinlarBannerResmi YBR = Veriler.YayinlarBannerResmi.FirstOrDefault();
            if (YBR != null)
            {
                if (!string.IsNullOrEmpty(YBR.Resim))
                {
                    Resim.Style.Add("background-image", YBR.Resim);
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

        private void YayinlariVer(int YayinKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YKList = (from p in Veriler.YayinKategorileri
                          where p.DilId == DilId
                                && p.Durum == true
                          orderby p.KategoriAdi ascending
                          select new
                                     {
                                         p.Id,
                                         Ad = p.KategoriAdi,
                                         p.Aciklama,
                                         GorselThumbnail =
                              p.GorselThumbnail != null
                                  ? p.GorselThumbnail
                                  : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                     }).ToList();
            if (YKList.Count != 0)
            {
                ListView1.DataSource = YKList;
                ListView1.DataBind();
                Page.Title = Page.Header.Title + " :: Yayınlar";
                Page.MetaKeywords = YKList.FirstOrDefault().Aciklama;
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
                Page.Title = Page.Header.Title + " :: Yayınlar";
                Page.MetaKeywords = string.Empty;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/yayinlarlist.aspx?",
                                                                                      "/yayinlar/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("yayinlarpage=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("&", "/");
                        }
                    }
                }
            }
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            YayinKategorileri YK = Veriler.YayinKategorileri.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/yayindetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(YK.KategoriAdi) + "/1";
        }

        public string SiteMapVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../yayinlar/0/tumyayinlar/1'>Tüm Kategoriler</a>");
            return SB.ToString();
        }
    }
}
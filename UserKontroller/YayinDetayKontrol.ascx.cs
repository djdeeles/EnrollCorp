using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class YayinDetayKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    YayinlariVer(Convert.ToInt32(Request.QueryString["code"]));
                    BannerGorseliVer();
                    LabelSiteMap.Text = SiteMapVer();
                }
            }
        }

        private void YayinlariVer(int YayinKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YList = (from p in Veriler.YayinKategorileri
                         join p1 in Veriler.Yayinlar
                             on p.Id equals p1.YayinKategoriId
                         where p.DilId == DilId
                               && p.Id == YayinKategoriId
                               && p.Durum == true
                               && p1.Durum == true
                         select new
                                    {
                                        p1.Id,
                                        Gorsel =
                             p1.Gorsel != null ? p1.Gorsel : "../App_Themes/PendikMainTheme/Images/novideo.jpg",
                                        p1.YayinAdi,
                                        p.KategoriAdi,
                                    }).Distinct().ToList();
            if (YList.Count != 0)
            {
                ListView1.DataSource = YList;
                ListView1.DataBind();
            }
            else
            {
                ListView1.DataSource = null;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/yayindetay.aspx?",
                                                                                      "/yayindetay/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("yayindetaypage=", "");
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
            Yayinlar Y = Veriler.Yayinlar.Where(p => p.Id == Id && p.Durum == true).First();
            HL.NavigateUrl = "../YayinGoster.aspx?code=" + Y.Id + "&iframe=true&width=640&height=480";
            HL.ToolTip = Y.YayinAdi + Y.Aciklama;
        }

        private void BannerGorseliVer()
        {
            YayinKategorileri YKBR = Veriler.YayinKategorileri.FirstOrDefault();
            if (YKBR != null)
            {
                if (!string.IsNullOrEmpty(YKBR.Gorsel))
                {
                    Resim.Style.Add("background-image", YKBR.Gorsel);
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

        public string SiteMapVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            YayinKategorileri YK =
                Veriler.YayinKategorileri.Where(p => p.Id == Id && p.Durum == true && p.DilId == DilId).First();
            lblAlbumName.Text = YK.KategoriAdi;
            LabelKategoriAdi.Text = YK.KategoriAdi;
            Page.Title = Page.Header.Title + " :: " + YK.KategoriAdi + "";
            Page.MetaKeywords = YK.Aciklama;
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../yayinlar/0/tumyayinlar/1'>Tüm Yayınlar</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../yayindetay/"
                      + YK.Id + "/"
                      + MenuUrl.MenuUrlDuzenle(YK.KategoriAdi) + "/1'>"
                      + YK.KategoriAdi + "</a>");
            SB.Append(" / ");
            return SB.ToString();
        }
    }
}
using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DokumanYayinlariListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    DokumanYayinlariVer(Convert.ToInt32(Request.QueryString["code"]));
                    BannerGorseliVer();
                    LabelSiteMap.Text = SiteMapVer();
                }
            }
        }

        private void DokumanYayinlariVer(int DokumanKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (DokumanKategoriId == 0)
            {
                var DYList = (from p in Veriler.DokumanYayinlari
                              join p1 in Veriler.DokumanKategorileri
                                  on p.DokumanKategoriId equals p1.Id
                              where
                                  p.Durum == true
                                  && p1.Durum == true
                                  && p1.DilId == DilId
                              orderby p.KaydetmeTarihi
                              select new
                                         {
                                             p.Id,
                                             Gorsel =
                                  p.GorselThumbnail != null
                                      ? p.GorselThumbnail
                                      : "../App_Themes/PendikMainTheme/Images/noimage.png",
                                             p.DokumanAdi,
                                             p1.KategoriAdi
                                         }).Distinct().ToList();
                if (DYList.Count != 0)
                {
                    ListView1.DataSource = DYList;
                    ListView1.DataBind();
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                }
                LabelKategoriAdi.Text = "Tüm Yayınlar";
            }
            else
            {
                var DYList = (from p in Veriler.DokumanYayinlari
                              join p1 in Veriler.DokumanKategorileri
                                  on p.DokumanKategoriId equals p1.Id
                              where
                                  p.Durum == true
                                  && p1.Durum == true
                                  && p1.DilId == DilId
                                  && p1.Id == DokumanKategoriId
                              orderby p.KaydetmeTarihi
                              select new
                                         {
                                             p.Id,
                                             Gorsel =
                                  p.GorselThumbnail != null
                                      ? p.GorselThumbnail
                                      : "../App_Themes/PendikMainTheme/Images/noimage.png",
                                             p.DokumanAdi,
                                             p1.KategoriAdi
                                         }).Distinct().ToList();
                if (DYList.Count != 0)
                {
                    ListView1.DataSource = DYList;
                    ListView1.DataBind();
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                }
                LabelKategoriAdi.Text = DYList.First().KategoriAdi;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/dokumanyayinlarilist.aspx?",
                                                                                      "/dokumanyayinlari/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("dokumanyayinlarilistpage=", "");
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
            DokumanYayinlari DK = Veriler.DokumanYayinlari.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "../DokumanYayiniGoster.aspx?code=" + DK.Id + "&iframe=true&width=800&height=540";
            HL.ToolTip = DK.DokumanAdi + DK.Aciklama;
        }

        private void BannerGorseliVer()
        {
            DokumanYayinlariBannerResmi DYBR = Veriler.DokumanYayinlariBannerResmi.FirstOrDefault();
            if (DYBR != null)
            {
                if (!string.IsNullOrEmpty(DYBR.Resim))
                {
                    Resim.Style.Add("background-image", DYBR.Resim);
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
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            Page.Title = Page.Header.Title + " :: " + "Doküman Yayınları" + "";
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../dokumanyayinlari/0/tumyayinlar/1'>Tüm Yayınlar</a>");
            SB.Append(" / ");
            var DK = (from p in Veriler.DokumanKategorileri
                      where p.DilId == DilId
                            && p.Durum == true
                            && p.Id == Id
                      select new
                                 {
                                     p.KategoriAdi,
                                     KategoriId = p.Id,
                                     p.Aciklama
                                 }).FirstOrDefault();
            if (DK != null)
            {
                SB.Append("<a href='../../../dokumanyayinlari/"
                          + DK.KategoriId + "/"
                          + MenuUrl.MenuUrlDuzenle(DK.KategoriAdi) + "/1'>"
                          + DK.KategoriAdi + "</a>");
            }
            return SB.ToString();
        }
    }
}
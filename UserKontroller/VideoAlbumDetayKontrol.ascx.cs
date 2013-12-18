using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class VideoAlbumDetayKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    int VideoAlbumId = Convert.ToInt32(Request.QueryString["code"]);
                    VideoGorselleriVer(VideoAlbumId);
                    BannerGorseliVer();
                    LabelSiteMap.Text = SiteMapVer();
                }
            }
        }

        private void VideoGorselleriVer(int VideoAlbumId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VGList = (from p in Veriler.VideoAlbumVideolari
                          join p1 in Veriler.VideoAlbumler
                              on p.VideoAlbumId equals p1.Id
                          join p2 in Veriler.VideoAlbumKategorileri
                              on p1.VideoAlbumKategoriId equals p2.Id
                          where
                              p.Durum == true
                              && p1.Durum == true
                              && p2.Durum == true
                              && p2.DilId == DilId
                              && p.VideoAlbumId == VideoAlbumId
                          orderby p.KaydetmeTarihi
                          select new
                                     {
                                         p.Id,
                                         Gorsel =
                              p.Gorsel != null ? p.Gorsel : "../App_Themes/PendikMainTheme/Images/novideo.jpg",
                                         p.VideoAdi,
                                         AlbumAdi = p1.VideoAlbumAdi,
                                         p2.VideoAlbumKategoriAdi
                                     }).Distinct().ToList();
            if (VGList.Count != 0)
            {
                ListView1.DataSource = VGList;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/videoalbumdetay.aspx?",
                                                                                      "/videoalbumdetay/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("videoalbumdetaypage=", "");
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
            VideoAlbumVideolari VAV = Veriler.VideoAlbumVideolari.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "../VideoGoster.aspx?code=" + VAV.Id + "&iframe=true&width=640&height=480";
            HL.ToolTip = VAV.VideoAdi + VAV.Aciklama;
        }

        private void BannerGorseliVer()
        {
            VideoAlbumBannerResmi VABR = Veriler.VideoAlbumBannerResmi.FirstOrDefault();
            if (VABR != null)
            {
                if (!string.IsNullOrEmpty(VABR.Resim))
                {
                    Resim.Style.Add("background-image", VABR.Resim);
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
            var F = (from p in Veriler.VideoAlbumKategorileri
                     join p1 in Veriler.VideoAlbumler
                         on p.Id equals p1.VideoAlbumKategoriId
                     where p1.Id == Id
                           && p1.Durum == true
                     select new
                                {
                                    KategoriAdi = p.VideoAlbumKategoriAdi,
                                    KategoriId = p.Id,
                                    AlbumAdi = p1.VideoAlbumAdi,
                                    AlbümId = p1.Id,
                                    p1.Aciklama
                                }).FirstOrDefault();
            lblAlbumName.Text = F.AlbumAdi;
            LabelVideoAlbumKategoriAdi.Text = F.KategoriAdi;
            Page.Title = Page.Header.Title + " :: " + F.AlbumAdi + "";
            Page.MetaKeywords = F.Aciklama;
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../videoalbumler/0/tumalbumler/1'>Tüm Kategoriler</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../videoalbumler/"
                      + F.KategoriId + "/"
                      + MenuUrl.MenuUrlDuzenle(F.KategoriAdi) + "/1'>"
                      + F.KategoriAdi + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../videoalbumdetay/"
                      + F.AlbümId + "/"
                      + MenuUrl.MenuUrlDuzenle(F.AlbumAdi) + "/1'>"
                      + F.AlbumAdi + "</a>");
            return SB.ToString();
        }
    }
}
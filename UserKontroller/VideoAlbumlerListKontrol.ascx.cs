using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class VideoAlbumlerListKontrol : UserControl
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
                        AlbumleriVer(Convert.ToInt32(Request.QueryString["code"]));
                    }
                }
            }
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

        private void AlbumleriVer(int VideoAlbumKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (VideoAlbumKategoriId != 0)
            {
                var AlbumlerList = (from p in Veriler.VideoAlbumKategorileri
                                    join p1 in Veriler.VideoAlbumler
                                        on p.Id equals p1.VideoAlbumKategoriId
                                    where p1.VideoAlbumKategoriId == VideoAlbumKategoriId
                                          && p.DilId == DilId
                                          && p.Durum == true
                                          && p1.Durum == true
                                    orderby p.VideoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.VideoAlbumAdi,
                                                   p1.Aciklama,
                                                   GorselThumbnail =
                                        p1.GorselThumbnail != null
                                            ? p1.GorselThumbnail
                                            : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                               }).ToList();
                if (AlbumlerList.Count != 0)
                {
                    ListView1.DataSource = AlbumlerList;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Video Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Video Albümler";
                    Page.MetaKeywords = string.Empty;
                }
            }
            else
            {
                var AlbumlerList = (from p in Veriler.VideoAlbumKategorileri
                                    join p1 in Veriler.VideoAlbumler
                                        on p.Id equals p1.VideoAlbumKategoriId
                                    where p.DilId == DilId
                                          && p.Durum == true
                                          && p1.Durum == true
                                    orderby p.VideoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.VideoAlbumAdi,
                                                   p1.Aciklama,
                                                   GorselThumbnail =
                                        p1.GorselThumbnail != null
                                            ? p1.GorselThumbnail
                                            : "/App_Themes/PendikMainTheme/Images/noimage.png"
                                               }).ToList();
                if (AlbumlerList.Count != 0)
                {
                    ListView1.DataSource = AlbumlerList;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Video Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Video Albümler";
                    Page.MetaKeywords = string.Empty;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/videoalbumlerlist.aspx?",
                                                                                      "/videoalbumler/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("fotoalbumlerpage=", "");
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
            VideoAlbumler VA = Veriler.VideoAlbumler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/videoalbumdetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(VA.VideoAlbumAdi + "/1");
        }

        public string SiteMapVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../videoalbumler/0/tumalbumler/1'>Tüm Kategoriler</a>");
            if (Id != 0)
            {
                var F = (from p in Veriler.VideoAlbumKategorileri
                         where p.Id == Id
                               && p.Durum == true
                         select new
                                    {
                                        KategoriAdi = p.VideoAlbumKategoriAdi,
                                        KategoriId = p.Id,
                                    }).FirstOrDefault();
                SB.Append(" / ");
                SB.Append("<a href='../../../videoalbumler/"
                          + F.KategoriId + "/"
                          + MenuUrl.MenuUrlDuzenle(F.KategoriAdi) + "/1'>"
                          + F.KategoriAdi + "</a>");
            }
            return SB.ToString();
        }
    }
}
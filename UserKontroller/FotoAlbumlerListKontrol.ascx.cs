using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class FotoAlbumlerListKontrol : UserControl
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
                        if (Request.QueryString["code"] != "0")
                        {
                            AlbumleriVer(Convert.ToInt32(Request.QueryString["code"]));
                        }
                    }
                }
            }
        }

        private void BannerGorseliVer()
        {
            FotoAlbumBannerResmi FABR = Veriler.FotoAlbumBannerResmi.FirstOrDefault();
            if (FABR != null)
            {
                if (!string.IsNullOrEmpty(FABR.Resim))
                {
                    Resim.Style.Add("background-image", FABR.Resim);
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

        private void AlbumleriVer(int FotoAlbumKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (FotoAlbumKategoriId != 0)
            {
                var AlbumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    where p1.FotoAlbumKategoriId == FotoAlbumKategoriId
                                          && p.DilId == DilId
                                          && p.Durum == true
                                          && p1.Durum == true
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.FotoAlbumAdi,
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
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = string.Empty;
                }
            }
            else
            {
                var AlbumlerList = (from p in Veriler.FotoAlbumKategorileri
                                    join p1 in Veriler.FotoAlbumler
                                        on p.Id equals p1.FotoAlbumKategoriId
                                    where p.DilId == DilId
                                          && p.Durum == true
                                          && p1.Durum == true
                                    orderby p.FotoAlbumKategoriAdi ascending
                                    select new
                                               {
                                                   p1.Id,
                                                   Ad = p1.FotoAlbumAdi,
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
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
                    Page.MetaKeywords = AlbumlerList.FirstOrDefault().Aciklama;
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();
                    Page.Title = Page.Header.Title + " :: Foto Albümler";
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/fotoalbumlerlist.aspx?",
                                                                                      "/fotoalbumler/");
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
            FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/fotoalbumdetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(FA.FotoAlbumAdi + "/1");
        }

        public string SiteMapVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../fotoalbumler/0/tumalbumler/1'>Tüm Kategoriler</a>");
            if (Id != 0)
            {
                var F = (from p in Veriler.FotoAlbumKategorileri
                         where p.Id == Id
                               && p.Durum == true
                         select new
                                    {
                                        KategoriAdi = p.FotoAlbumKategoriAdi,
                                        KategoriId = p.Id,
                                    }).FirstOrDefault();
                SB.Append(" / ");
                SB.Append("<a href='../../../fotoalbumler/"
                          + F.KategoriId + "/"
                          + MenuUrl.MenuUrlDuzenle(F.KategoriAdi) + "/1'>"
                          + F.KategoriAdi + "</a>");
            }
            return SB.ToString();
        }
    }
}
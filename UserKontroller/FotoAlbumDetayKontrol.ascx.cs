using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class FotoAlbumDetayKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    int FotoAlbumId = Convert.ToInt32(Request.QueryString["code"]);
                    FotoGorselleriVer(FotoAlbumId);
                    BannerGorseliVer();
                    LabelSiteMap.Text = SiteMapVer();
                }
            }
        }

        private void FotoGorselleriVer(int FotoAlbumId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FGList = (from p in Veriler.FotoAlbumGorselleri
                          join p1 in Veriler.FotoAlbumler
                              on p.FotoAlbumId equals p1.Id
                          join p2 in Veriler.FotoAlbumKategorileri
                              on p1.FotoAlbumKategoriId equals p2.Id
                          where
                              p.Durum == true
                              && p1.Durum == true
                              && p2.Durum == true
                              && p2.DilId == DilId
                              && p.FotoAlbumId == FotoAlbumId
                          select new
                                     {
                                         p.Id,
                                         GorselThumbnail =
                              p.GorselThumbnail != null
                                  ? p.GorselThumbnail
                                  : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                         p.GorselAdi,
                                         AlbumAdi = p1.FotoAlbumAdi,
                                         p2.FotoAlbumKategoriAdi
                                     }).Distinct().ToList();
            if (FGList.Count != 0)
            {
                ListView1.DataSource = FGList;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/fotoalbumdetay.aspx?",
                                                                                      "/fotoalbumdetay/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("fotoalbumdetaypage=", "");
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
            FotoAlbumGorselleri FG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
            HL.NavigateUrl = FG.Gorsel.Replace("~/", "../");
            HL.ToolTip = FG.GorselAdi + FG.Aciklama;
        }

        protected void Image1_DataBinding(object sender, EventArgs e)
        {
            Image Resim = (Image) sender;
            int Id = Convert.ToInt32(Resim.ImageUrl);
            FotoAlbumGorselleri FG = Veriler.FotoAlbumGorselleri.Where(p => p.Id == Id).First();
            Resim.ImageUrl = FG.GorselThumbnail.Replace("~/", "../");
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

        public string SiteMapVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            var F = (from p in Veriler.FotoAlbumKategorileri
                     join p1 in Veriler.FotoAlbumler
                         on p.Id equals p1.FotoAlbumKategoriId
                     where p1.Id == Id
                     select new
                                {
                                    KategoriAdi = p.FotoAlbumKategoriAdi,
                                    KategoriId = p.Id,
                                    AlbumAdi = p1.FotoAlbumAdi,
                                    AlbümId = p1.Id,
                                    p1.Aciklama
                                }).FirstOrDefault();
            lblAlbumName.Text = F.AlbumAdi;
            LabelFotoAlbumKategoriAdi.Text = F.KategoriAdi;
            Page.Title = Page.Header.Title + " :: " + F.AlbumAdi + "";
            Page.MetaKeywords = F.Aciklama;
            StringBuilder SB = new StringBuilder();
            SB.Append("<a href='" + "../../../anasayfa" + "'>" + "Anasayfa" + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../fotoalbumler/0/tumalbumler/1'>Tüm Kategoriler</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../fotoalbumler/"
                      + F.KategoriId + "/"
                      + MenuUrl.MenuUrlDuzenle(F.KategoriAdi) + "/1'>"
                      + F.KategoriAdi + "</a>");
            SB.Append(" / ");
            SB.Append("<a href='../../../fotoalbumdetay/"
                      + F.AlbümId + "/"
                      + MenuUrl.MenuUrlDuzenle(F.AlbumAdi) + "/1'>"
                      + F.AlbumAdi + "</a>");
            return SB.ToString();
        }
    }
}
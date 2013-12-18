using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;
using Telerik.Web.UI;

namespace EnrollKurumsal.UserKontroller
{
    public partial class SiteHaritasi : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: Site Haritası";
                MenuleriVer(RadTreeViewMenuler, 2);
                MenuleriVer(RadTreeViewHizmetMenuler, 5);
                HaberleriVer(RadTreeViewHaberler);
                DuyurulariVer(RadTreeViewDuyurular);
                EtkinlikleriVer(RadTreeViewEtkinlikler);
                IhaleleriVer(RadTreeViewIhaleler);
                FotolariVer(RadTreeViewFotolar);
                VideolariVer(RadTreeViewVideolar);
                CanliYayinlariVer(RadTreeViewCanliYayinlar);
                YayinlariVer(RadTreeViewYayinlar);
            }
        }

        private void BannerGorseliVer()
        {
            Resim.Style.Add("background-image", "App_Themes/PendikMainTheme/Images/Default_banner.png");
            LabelUstMenuBaslik.Text = "Site Haritası";
        }

        private void MenuleriVer(RadTreeView RadTreeView, int LokasyonId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var SMList = new List<SiteMap>();
            var Menuler = (from p in Veriler.Menuler
                           where p.MenuLokasyonId == LokasyonId
                                 && p.DilId == DilId
                                 && p.Durum == true
                           orderby p.SiraNo ascending
                           select new
                                      {
                                          p.MenuAdi,
                                          p.Id,
                                          UstMenuId = p.UstMenuId == 0 ? null : p.UstMenuId,
                                          TipId = p.MenuTipId,
                                          p.Url
                                      }).ToList();
            foreach (var Menu in Menuler)
            {
                SiteMap SM = new SiteMap();
                SM.MenuAdi = Menu.MenuAdi;
                SM.Id = Menu.Id;
                SM.UstMenuId = Convert.ToInt32(Menu.UstMenuId);
                switch (Menu.TipId)
                {
                    case 1:
                        SM.NavigatUrl = string.Empty;
                        break;
                    case 2:
                        if (Menu.Url.Contains("http"))
                        {
                            SM.NavigatUrl = Menu.Url;
                            SM.Target = "_blank";
                        }
                        else
                        {
                            SM.NavigatUrl = "/" + Menu.Url;
                        }
                        break;
                    case 3:
                        SM.NavigatUrl = "/sayfa/" + Menu.Id + "/" + MenuUrl.MenuUrlDuzenle(Menu.MenuAdi);
                        break;
                }
                SMList.Add(SM);
            }
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataNavigateUrlField = "NavigatUrl";
            RadTreeView.DataSource = SMList;
            RadTreeView.DataBind();
        }

        private void HaberleriVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var SMList = new List<SiteMap>();
            var HKList = (from p in Veriler.HaberKategorileri
                          where
                              p.DilId == DilId
                              && p.Durum == true
                          orderby p.SiraNo ascending
                          select new
                                     {
                                         MenuAdi = p.KategoriAdi,
                                         p.Id,
                                     }).ToList();
            SiteMap S = new SiteMap();
            S.MenuAdi = "Tüm Haberler";
            S.NavigatUrl = "/haberler/0/tumhaberler/1";
            SMList.Add(S);
            foreach (var Menu in HKList)
            {
                SiteMap SM = new SiteMap();
                SM.MenuAdi = Menu.MenuAdi;
                SM.Id = Menu.Id;
                SM.NavigatUrl = "/haberler/" + Menu.Id + "/" + MenuUrl.MenuUrlDuzenle(Menu.MenuAdi) + "/1";
                SMList.Add(SM);
            }
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataNavigateUrlField = "NavigatUrl";
            RadTreeView.DataSource = SMList;
            RadTreeView.DataBind();
        }

        private void DuyurulariVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var SMList = new List<SiteMap>();
            var HKList = (from p in Veriler.DuyuruKategorileri
                          where
                              p.DilId == DilId
                              && p.Durum == true
                          orderby p.SiraNo ascending
                          select new
                                     {
                                         MenuAdi = p.KategoriAdi,
                                         p.Id,
                                     }).ToList();
            SiteMap S = new SiteMap();
            S.MenuAdi = "Tüm Duyurular";
            S.NavigatUrl = "/duyurular/0/tumduyurular/1";
            SMList.Add(S);
            foreach (var Menu in HKList)
            {
                SiteMap SM = new SiteMap();
                SM.MenuAdi = Menu.MenuAdi;
                SM.Id = Menu.Id;
                SM.NavigatUrl = "/duyurular/" + Menu.Id + "/" + MenuUrl.MenuUrlDuzenle(Menu.MenuAdi) + "/1";
                ;
                SMList.Add(SM);
            }
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataNavigateUrlField = "NavigatUrl";
            RadTreeView.DataSource = SMList;
            RadTreeView.DataBind();
        }

        private void EtkinlikleriVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var SMList = new List<SiteMap>();
            var HKList = (from p in Veriler.EtkinlikKategorileri
                          where
                              p.DilId == DilId
                              && p.Durum == true
                          orderby p.SiraNo ascending
                          select new
                                     {
                                         MenuAdi = p.KategoriAdi,
                                         p.Id,
                                     }).ToList();
            SiteMap S = new SiteMap();
            S.MenuAdi = "Tüm Etkinlikler";
            S.NavigatUrl = "/etkinlikler/0/tumetkinlikler/1";
            SMList.Add(S);
            foreach (var Menu in HKList)
            {
                SiteMap SM = new SiteMap();
                SM.MenuAdi = Menu.MenuAdi;
                SM.Id = Menu.Id;
                SM.NavigatUrl = "/etkinlikler/" + Menu.Id + "/" + MenuUrl.MenuUrlDuzenle(Menu.MenuAdi) + "/1";
                ;
                SMList.Add(SM);
            }
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataNavigateUrlField = "NavigatUrl";
            RadTreeView.DataSource = SMList;
            RadTreeView.DataBind();
        }

        private void IhaleleriVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var SMList = new List<SiteMap>();
            var HKList = (from p in Veriler.IhaleKategorileri
                          where
                              p.DilId == DilId
                              && p.Durum == true
                          orderby p.SiraNo ascending
                          select new
                                     {
                                         MenuAdi = p.KategoriAdi,
                                         p.Id,
                                     }).ToList();
            SiteMap S = new SiteMap();
            S.MenuAdi = "Tüm İhaleler";
            S.NavigatUrl = "/ihaleler/0/tumihaleler/1";
            SMList.Add(S);
            foreach (var Menu in HKList)
            {
                SiteMap SM = new SiteMap();
                SM.MenuAdi = Menu.MenuAdi;
                SM.Id = Menu.Id;
                SM.NavigatUrl = "/ihaleler/" + Menu.Id + "/" + MenuUrl.MenuUrlDuzenle(Menu.MenuAdi) + "/1";
                ;
                SMList.Add(SM);
            }
            RadTreeView.DataTextField = "MenuAdi";
            RadTreeView.DataFieldParentID = "UstMenuId";
            RadTreeView.DataFieldID = "Id";
            RadTreeView.DataValueField = "Id";
            RadTreeView.DataNavigateUrlField = "NavigatUrl";
            RadTreeView.DataSource = SMList;
            RadTreeView.DataBind();
        }

        private void FotolariVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList = Veriler.FotoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            RadTreeNode SN = new RadTreeNode();
            SN.Text = "Tüm Albümler";
            SN.NavigateUrl = "/fotoalbumler/0/tumalbumler/1";
            RadTreeView.Nodes.Add(SN);
            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                RadTreeNode siteNode = new RadTreeNode();
                siteNode.Text = FAK.FotoAlbumKategoriAdi;
                siteNode.NavigateUrl = "/fotoalbumler/" + FAK.Id + "/" +
                                       MenuUrl.MenuUrlDuzenle(FAK.FotoAlbumKategoriAdi) + "/1";
                RadTreeView.Nodes.Add(siteNode);
                var FAList =
                    Veriler.FotoAlbumler.Where(p => p.FotoAlbumKategoriId == FAK.Id && p.Durum == true).ToList();
                foreach (FotoAlbumler FA in FAList)
                {
                    RadTreeNode groupNode = new RadTreeNode();
                    groupNode.Text = FA.FotoAlbumAdi;
                    groupNode.NavigateUrl = "/fotoalbumdetay/" + FA.Id + "/" + MenuUrl.MenuUrlDuzenle(FA.FotoAlbumAdi) +
                                            "/1";
                    siteNode.Nodes.Add(groupNode);
                }
            }
        }

        private void VideolariVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VAKList = Veriler.VideoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            RadTreeNode SN = new RadTreeNode();
            SN.Text = "Tüm Albümler";
            SN.NavigateUrl = "/videoalbumler/0/tumalbumler/1";
            RadTreeView.Nodes.Add(SN);
            foreach (VideoAlbumKategorileri VAK in VAKList)
            {
                RadTreeNode siteNode = new RadTreeNode();
                siteNode.Text = VAK.VideoAlbumKategoriAdi;
                siteNode.NavigateUrl = "/videoalbumler/" + VAK.Id + "/" +
                                       MenuUrl.MenuUrlDuzenle(VAK.VideoAlbumKategoriAdi) + "/1";
                RadTreeView.Nodes.Add(siteNode);
                var VAList =
                    Veriler.VideoAlbumler.Where(p => p.VideoAlbumKategoriId == VAK.Id && p.Durum == true).ToList();
                foreach (VideoAlbumler VA in VAList)
                {
                    RadTreeNode groupNode = new RadTreeNode();
                    groupNode.Text = VA.VideoAlbumAdi;
                    groupNode.NavigateUrl = "/videoalbumdetay/" + VA.Id + "/" + MenuUrl.MenuUrlDuzenle(VA.VideoAlbumAdi) +
                                            "/1";
                    siteNode.Nodes.Add(groupNode);
                }
            }
        }

        private void CanliYayinlariVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YKList = Veriler.YayinKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            RadTreeNode SN = new RadTreeNode();
            SN.Text = "Tüm Yayınlar";
            SN.NavigateUrl = "/yayinlar/0/tumyayinlar/1";
            RadTreeView.Nodes.Add(SN);
            foreach (YayinKategorileri YK in YKList)
            {
                RadTreeNode siteNode = new RadTreeNode();
                siteNode.Text = YK.KategoriAdi;
                siteNode.NavigateUrl = "/yayindetay/" + YK.Id + "/" + MenuUrl.MenuUrlDuzenle(YK.KategoriAdi) + "/1";
                RadTreeView.Nodes.Add(siteNode);
            }
        }

        private void YayinlariVer(RadTreeView RadTreeView)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DKList = Veriler.DokumanKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            RadTreeNode SN = new RadTreeNode();
            SN.Text = "Tüm Yayınlar";
            SN.NavigateUrl = "/dokumanyayinlari/0/tumdokumanyayinlari/1";
            RadTreeView.Nodes.Add(SN);
            foreach (DokumanKategorileri DK in DKList)
            {
                RadTreeNode siteNode = new RadTreeNode();
                siteNode.Text = DK.KategoriAdi;
                siteNode.NavigateUrl = "/dokumanyayinlari/" + DK.Id + "/" + MenuUrl.MenuUrlDuzenle(DK.KategoriAdi) +
                                       "/1";
                RadTreeView.Nodes.Add(siteNode);
            }
        }

        #region Nested type: SiteMap

        public class SiteMap
        {
            public int Id { get; set; }
            public string MenuAdi { get; set; }
            public int UstMenuId { get; set; }
            public string NavigatUrl { get; set; }
            public string Target { get; set; }
        }

        #endregion
    }
}
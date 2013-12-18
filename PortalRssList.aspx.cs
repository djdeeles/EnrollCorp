using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class PortalRssList : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Sayfa = "http://" + Request.Url.Host + "/";
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                SiteBilgileri SB = Veriler.SiteBilgileri.Where(p => p.DilId == DilId).First();
                Response.Clear();
                Response.ContentType = "text/xml";
                XmlTextWriter objX = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
                objX.WriteStartDocument();
                objX.WriteStartElement("rss");
                objX.WriteAttributeString("version", "2.0");
                objX.WriteStartElement("channel");
                objX.WriteElementString("title", SB.PageTitle);
                objX.WriteElementString("link", Sayfa + "PortalRssList.aspx");
                objX.WriteElementString("description", SB.Description);
                HaberlerRssVer(DilId, objX, Sayfa);
                DuyurularRssVer(DilId, objX, Sayfa);
                EtkinliklerRssVer(DilId, objX, Sayfa);
                IhalelerRssVer(DilId, objX, Sayfa);
                AlbumlerRssVer(DilId, objX, Sayfa);
                DokumanlarRssVer(DilId, objX, Sayfa);
                VideolarRssVer(DilId, objX, Sayfa);
                objX.WriteEndDocument();
                objX.Flush();
                objX.Close();
                Response.End();
                Response.Write(Request.RawUrl);
            }
        }

        private void HaberlerRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var HList = (from HT in Veriler.HaberlerTablosu
                         join HK in Veriler.HaberKategorileri
                             on HT.KategoriId equals HK.Id
                         join H in Veriler.Haberler
                             on HT.HaberId equals H.Id
                         where
                             HK.DilId == DilId
                             && HK.Durum == true
                             && H.Durum == true
                         orderby H.KayitTarihi descending
                         select new
                                    {
                                        H.Id,
                                        H.Baslik,
                                        H.Ozet,
                                        Tarih = H.KaydetmeTarihi,
                                    }).ToList();
            foreach (var H in HList)
            {
                DateTime Tarih = Convert.ToDateTime(H.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Haber");
                    XTW.WriteElementString("title", H.Baslik);
                    XTW.WriteElementString("description", H.Ozet);
                    XTW.WriteElementString("date", H.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "haberdetay/"
                                                   + H.Id
                                                   + "/"
                                                   + MenuUrl.MenuUrlDuzenle(H.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void DuyurularRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var DList = (from DT in Veriler.DuyurularTablosu
                         join DK in Veriler.DuyuruKategorileri
                             on DT.KategoriId equals DK.Id
                         join D in Veriler.Duyurular
                             on DT.DuyuruId equals D.Id
                         where
                             DK.DilId == DilId
                             && DK.Durum == true
                             && D.Durum == true
                         orderby D.BaslangicTarihi descending
                         select new
                                    {
                                        D.Id,
                                        D.Baslik,
                                        D.Ozet,
                                        Tarih = D.KaydetmeTarihi,
                                    }).ToList();
            foreach (var D in DList)
            {
                DateTime Tarih = Convert.ToDateTime(D.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Duyuru");
                    XTW.WriteElementString("title", D.Baslik);
                    XTW.WriteElementString("description", D.Ozet);
                    XTW.WriteElementString("date", D.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "duyurudetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void EtkinliklerRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var EList = (from EK in Veriler.EtkinlikKategorileri
                         join E in Veriler.Etkinlikler
                             on EK.Id equals E.EtkinlikKategoriId
                         where
                             EK.DilId == DilId
                             && EK.Durum == true
                             && E.Durum == true
                         orderby E.BaslangicTarihi descending
                         select new
                                    {
                                        E.Id,
                                        Baslik = E.Ad,
                                        E.Ozet,
                                        Tarih = E.KaydetmeTarihi,
                                    }).ToList();
            foreach (var E in EList)
            {
                DateTime Tarih = Convert.ToDateTime(E.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Etkinlik");
                    XTW.WriteElementString("title", E.Baslik);
                    XTW.WriteElementString("description", E.Ozet);
                    XTW.WriteElementString("date", E.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void IhalelerRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var IList = (from IK in Veriler.IhaleKategorileri
                         join I in Veriler.Ihaleler
                             on IK.Id equals I.IhaleKategoriId
                         where
                             IK.DilId == DilId
                             && IK.Durum == true
                             && I.Durum == true
                         orderby I.BaslangicTarihi descending
                         select new
                                    {
                                        I.Id,
                                        Baslik = I.Ad,
                                        I.Ozet,
                                        Tarih = I.KaydetmeTarihi,
                                    }).ToList();
            foreach (var I in IList)
            {
                DateTime Tarih = Convert.ToDateTime(I.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "İhale");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("date", I.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "ihaledetay/" + I.Id + "/" + MenuUrl.MenuUrlDuzenle(I.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void AlbumlerRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var FAList = (from FA in Veriler.FotoAlbumler
                          join FAK in Veriler.FotoAlbumKategorileri
                              on FA.FotoAlbumKategoriId equals FAK.Id
                          where
                              FAK.DilId == DilId
                              && FAK.Durum == true
                              && FA.Durum == true
                          orderby FA.KaydetmeTarihi descending
                          select new
                                     {
                                         FA.Id,
                                         Baslik = FA.FotoAlbumAdi,
                                         Ozet = FA.Aciklama,
                                         Tarih = FA.KaydetmeTarihi,
                                     }).ToList();
            foreach (var I in FAList)
            {
                DateTime Tarih = Convert.ToDateTime(I.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Albüm");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("date", I.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "fotoalbumdetay/"
                                                   + I.Id
                                                   + "/"
                                                   + MenuUrl.MenuUrlDuzenle(I.Baslik) + "/1");
                    XTW.WriteEndElement();
                }
            }
        }

        private void DokumanlarRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var DYList = (from D in Veriler.DokumanYayinlari
                          join DK in Veriler.DokumanKategorileri
                              on D.DokumanKategoriId equals DK.Id
                          where
                              DK.DilId == DilId
                              && DK.Durum == true
                              && D.Durum == true
                          orderby D.KaydetmeTarihi descending
                          select new
                                     {
                                         D.Id,
                                         KategoriId = D.DokumanKategoriId,
                                         DK.KategoriAdi,
                                         Baslik = D.DokumanAdi,
                                         Ozet = D.Aciklama,
                                         Tarih = D.KaydetmeTarihi,
                                     }).ToList();
            foreach (var I in DYList)
            {
                DateTime Tarih = Convert.ToDateTime(I.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Doküman");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("date", I.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "dokumanyayinlari/"
                                                   + I.KategoriId
                                                   + "/"
                                                   + MenuUrl.MenuUrlDuzenle(I.KategoriAdi)
                                                   + "/"
                                                   + DokumanSayfaNoVer(Convert.ToInt32(I.KategoriId), I.Id).ToString()
                                                   + "#!prettyPhoto[iframes]/"
                                                   + DokumanSiraNoVer(Convert.ToInt32(I.KategoriId), I.Id).ToString()
                                                   + "/");
                    XTW.WriteEndElement();
                }
            }
        }

        // Aşağıda sorgunun koşulu dokümanların listelendiği sayfadaki koşul ile aynı olmalıdır.
        // Yine ayrı şekilde aşağıda dokümanın sayfa numarasnını alma ve index'ini alma metodlarındaki koşul da dokümanların listelendiği sayfadaki sayfalama sayısına bağlıdır.

        public int DokumanSayfaNoVer(int KategoriId, int DokumanId)
        {
            int Sayi = 0;
            var DYList = (from DY in Veriler.DokumanYayinlari
                          join DK in Veriler.DokumanKategorileri
                              on DY.DokumanKategoriId equals DK.Id
                          where DK.Id == KategoriId
                          orderby DY.KaydetmeTarihi
                          select DY).ToList();
            int MesajIdIndex = 0;
            for (int i = 0; i <= DYList.Count - 1; i++)
            {
                if (DYList[i].Id == DokumanId)
                {
                    if (i == 0)
                    {
                        MesajIdIndex = 1;
                    }
                    else
                    {
                        MesajIdIndex = i + 1;
                    }
                }
            }
            if (DYList.Count != 0)
            {
                Sayi = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(MesajIdIndex)/20)));
            }
            return Sayi;
        }

        public int DokumanSiraNoVer(int KategoriId, int DokumanId)
        {
            int Sayi = 0;
            var DYList = (from DY in Veriler.DokumanYayinlari
                          join DK in Veriler.DokumanKategorileri
                              on DY.DokumanKategoriId equals DK.Id
                          where DK.Id == KategoriId
                          orderby DY.KaydetmeTarihi
                          select DY).ToList();
            int MesajIdIndex = 0;
            for (int i = 0; i <= DYList.Count - 1; i++)
            {
                if (DYList[i].Id == DokumanId)
                {
                    Sayi = (MesajIdIndex = i)%20;
                }
            }
            return Sayi;
        }

        private void VideolarRssVer(int DilId, XmlTextWriter XTW, string Sayfa)
        {
            var VAList = (from V in Veriler.VideoAlbumVideolari
                          join VA in Veriler.VideoAlbumler
                              on V.VideoAlbumId equals VA.Id
                          join VAK in Veriler.VideoAlbumKategorileri
                              on VA.VideoAlbumKategoriId equals VAK.Id
                          where
                              VAK.DilId == DilId
                              && VA.Durum == true
                              && VA.Durum == true
                              && V.Durum == true
                          orderby V.KaydetmeTarihi descending
                          select new
                                     {
                                         V.Id,
                                         AlbumId = VA.Id,
                                         AlbumAdi = VA.VideoAlbumAdi,
                                         Baslik = V.VideoAdi,
                                         Ozet = V.Aciklama,
                                         Tarih = V.KaydetmeTarihi,
                                     }).ToList();
            foreach (var I in VAList)
            {
                DateTime Tarih = Convert.ToDateTime(I.Tarih.Value.ToShortDateString());
                DateTime BirHaftaOncesi = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                if (Tarih >= BirHaftaOncesi)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("category", "Video");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("date", I.Tarih.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    XTW.WriteElementString("link", Sayfa
                                                   + "videoalbumdetay/"
                                                   + I.AlbumId
                                                   + "/"
                                                   + MenuUrl.MenuUrlDuzenle(I.AlbumAdi)
                                                   + "/"
                                                   + VideoSayfaNoVer(Convert.ToInt32(I.AlbumId), I.Id).ToString()
                                                   + "#!prettyPhoto[iframes]/"
                                                   + VideoSiraNoVer(Convert.ToInt32(I.AlbumId), I.Id).ToString()
                                                   + "/");
                    XTW.WriteEndElement();
                    // http://yeniweb.pendik.bel.tr/videoalbumdetay/1/album_01/1#!prettyPhoto[iframes]/0/
                }
            }
        }

        // Aşağıda sorgunun koşulu videoların listelendiği sayfadaki koşul ile aynı olmalıdır.
        // Yine ayrı şekilde aşağıda videoın sayfa numarasnını alma ve index'ini alma metodlarındaki koşul da dokümanların listelendiği sayfadaki sayfalama sayısına bağlıdır.

        public int VideoSayfaNoVer(int AlbumId, int VideoId)
        {
            int Sayi = 0;
            var VList = (from V in Veriler.VideoAlbumVideolari
                         join VA in Veriler.VideoAlbumler
                             on V.VideoAlbumId equals VA.Id
                         join VAK in Veriler.VideoAlbumKategorileri
                             on VA.VideoAlbumKategoriId equals VAK.Id
                         where VA.Id == AlbumId
                         orderby V.KaydetmeTarihi
                         select V).ToList();
            int MesajIdIndex = 0;
            for (int i = 0; i <= VList.Count - 1; i++)
            {
                if (VList[i].Id == VideoId)
                {
                    if (i == 0)
                    {
                        MesajIdIndex = 1;
                    }
                    else
                    {
                        MesajIdIndex = i + 1;
                    }
                }
            }
            if (VList.Count != 0)
            {
                Sayi = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(MesajIdIndex)/20)));
            }
            return Sayi;
        }

        public int VideoSiraNoVer(int AlbumId, int VideoId)
        {
            int Sayi = 0;
            var VList = (from V in Veriler.VideoAlbumVideolari
                         join VA in Veriler.VideoAlbumler
                             on V.VideoAlbumId equals VA.Id
                         join VAK in Veriler.VideoAlbumKategorileri
                             on VA.VideoAlbumKategoriId equals VAK.Id
                         where VA.Id == AlbumId
                         orderby V.KaydetmeTarihi
                         select V).ToList();
            int MesajIdIndex = 0;
            for (int i = 0; i <= VList.Count - 1; i++)
            {
                if (VList[i].Id == VideoId)
                {
                    Sayi = (MesajIdIndex = i)%20;
                }
            }
            return Sayi;
        }
    }
}
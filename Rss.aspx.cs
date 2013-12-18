using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class Rss : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["bolum"] != null)
                {
                    if (Request.QueryString["kategori"] != null)
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
                        objX.WriteElementString("link", Sayfa + "rss.aspx");
                        objX.WriteElementString("description", SB.Description);
                        int Bolum = Convert.ToInt32(Request.QueryString["bolum"]);
                        int KategoriId = Convert.ToInt32(Request.QueryString["kategori"]);
                        switch (Bolum)
                        {
                            case 0:
                                HaberlerRssVer(KategoriId, DilId, objX, Sayfa);
                                DuyurularRssVer(KategoriId, DilId, objX, Sayfa);
                                EtkinliklerRssVer(KategoriId, DilId, objX, Sayfa);
                                IhalelerRssVer(KategoriId, DilId, objX, Sayfa);
                                break;
                            case 1:
                                HaberlerRssVer(KategoriId, DilId, objX, Sayfa);
                                break;
                            case 2:
                                DuyurularRssVer(KategoriId, DilId, objX, Sayfa);
                                break;
                            case 3:
                                EtkinliklerRssVer(KategoriId, DilId, objX, Sayfa);
                                break;
                            case 4:
                                IhalelerRssVer(KategoriId, DilId, objX, Sayfa);
                                break;
                        }
                        objX.WriteEndDocument();
                        objX.Flush();
                        objX.Close();
                        Response.End();
                        Response.Write(Request.RawUrl);
                    }
                }
            }
        }

        private void HaberlerRssVer(int KategoriId, int DilId, XmlTextWriter XTW, string Sayfa)
        {
            if (KategoriId == 0)
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
                                            H.Ozet
                                        }).ToList();
                foreach (var H in HList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", H.Baslik);
                    XTW.WriteElementString("description", H.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "haberdetay/" + H.Id + "/" + MenuUrl.MenuUrlDuzenle(H.Baslik));
                    XTW.WriteEndElement();
                }
            }
            else
            {
                var HList = (from HT in Veriler.HaberlerTablosu
                             join HK in Veriler.HaberKategorileri
                                 on HT.KategoriId equals HK.Id
                             join H in Veriler.Haberler
                                 on HT.HaberId equals H.Id
                             where
                                 HK.DilId == DilId
                                 && HK.Id == KategoriId
                                 && HK.Durum == true
                                 && H.Durum == true
                             orderby H.KayitTarihi descending
                             select new
                                        {
                                            H.Id,
                                            H.Baslik,
                                            H.Ozet
                                        }).ToList();
                foreach (var H in HList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", H.Baslik);
                    XTW.WriteElementString("description", H.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "haberdetay/" + H.Id + "/" + MenuUrl.MenuUrlDuzenle(H.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void DuyurularRssVer(int KategoriId, int DilId, XmlTextWriter XTW, string Sayfa)
        {
            if (KategoriId == 0)
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
                                            D.Ozet
                                        }).ToList();
                foreach (var D in DList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", D.Baslik);
                    XTW.WriteElementString("description", D.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "duyurudetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik));
                    XTW.WriteEndElement();
                }
            }
            else
            {
                var DList = (from DT in Veriler.DuyurularTablosu
                             join DK in Veriler.DuyuruKategorileri
                                 on DT.KategoriId equals DK.Id
                             join D in Veriler.Duyurular
                                 on DT.DuyuruId equals D.Id
                             where
                                 DK.DilId == DilId
                                 && DK.Durum == true
                                 && DK.Id == KategoriId
                                 && D.Durum == true
                             orderby D.BaslangicTarihi descending
                             select new
                                        {
                                            D.Id,
                                            D.Baslik,
                                            D.Ozet
                                        }).ToList();
                foreach (var D in DList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", D.Baslik);
                    XTW.WriteElementString("description", D.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "duyurudetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void EtkinliklerRssVer(int KategoriId, int DilId, XmlTextWriter XTW, string Sayfa)
        {
            if (KategoriId == 0)
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
                                            E.Ozet
                                        }).ToList();
                foreach (var E in EList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", E.Baslik);
                    XTW.WriteElementString("description", E.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Baslik));
                    XTW.WriteEndElement();
                }
            }
            else
            {
                var EList = (from EK in Veriler.EtkinlikKategorileri
                             join E in Veriler.Etkinlikler
                                 on EK.Id equals E.EtkinlikKategoriId
                             where
                                 EK.DilId == DilId
                                 && EK.Durum == true
                                 && EK.Id == KategoriId
                                 && E.Durum == true
                             orderby E.BaslangicTarihi descending
                             select new
                                        {
                                            E.Id,
                                            Baslik = E.Ad,
                                            E.Ozet
                                        }).ToList();
                foreach (var E in EList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", E.Baslik);
                    XTW.WriteElementString("description", E.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }

        private void IhalelerRssVer(int KategoriId, int DilId, XmlTextWriter XTW, string Sayfa)
        {
            if (KategoriId == 0)
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
                                            I.Ozet
                                        }).ToList();
                foreach (var I in IList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "ihaledetay/" + I.Id + "/" + MenuUrl.MenuUrlDuzenle(I.Baslik));
                    XTW.WriteEndElement();
                }
            }
            else
            {
                var IList = (from IK in Veriler.IhaleKategorileri
                             join I in Veriler.Ihaleler
                                 on IK.Id equals I.IhaleKategoriId
                             where
                                 IK.DilId == DilId
                                 && IK.Id == KategoriId
                                 && IK.Durum == true
                                 && I.Durum == true
                             orderby I.BaslangicTarihi descending
                             select new
                                        {
                                            I.Id,
                                            Baslik = I.Ad,
                                            I.Ozet
                                        }).ToList();
                foreach (var I in IList)
                {
                    XTW.WriteStartElement("item");
                    XTW.WriteElementString("title", I.Baslik);
                    XTW.WriteElementString("description", I.Ozet);
                    XTW.WriteElementString("link", Sayfa
                                                   + "ihaledetay/" + I.Id + "/" + MenuUrl.MenuUrlDuzenle(I.Baslik));
                    XTW.WriteEndElement();
                }
            }
        }
    }
}
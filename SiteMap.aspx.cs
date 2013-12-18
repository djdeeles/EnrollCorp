using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class SiteMap : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SiteMapVer();
            }
        }

        private void SiteMapVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            string Host = "http://" + Request.Url.Host;
            StringBuilder SB = new StringBuilder();
            SB.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            SB.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            #region SMMenuler

            var MList = Veriler.Menuler.Where(p => p.Durum == true && p.DilId == DilId).ToList();
            foreach (Menuler M in MList)
            {
                if (M.MenuTipId == 3)
                {
                    SB.AppendLine("<url>");
                    SB.AppendLine("<loc>");
                    SB.AppendLine(Host + "/sayfa/" + M.Id + "/" + MenuUrl.MenuUrlDuzenle(M.MenuAdi));
                    SB.AppendLine("</loc>");
                    // <lastmod>2012-01-02T09:19:00+00:00</lastmod>
                    SB.AppendLine("<changefreq>");
                    SB.AppendLine("always");
                    SB.AppendLine("</changefreq>");
                    SB.AppendLine("<priority>");
                    SB.AppendLine("1");
                    SB.AppendLine("</priority>");
                    SB.AppendLine("</url>");
                }
            }

            #endregion

            #region SMHaberler

            var HList = (from HK in Veriler.HaberKategorileri
                         join HT in Veriler.HaberlerTablosu
                             on HK.Id equals HT.KategoriId
                         join H in Veriler.Haberler
                             on HT.HaberId equals H.Id
                         where HK.DilId == DilId
                               && HK.Durum == true
                               && H.Durum == true
                         orderby H.DuzenlenmeTarihi descending
                         select new
                                    {
                                        H.Id,
                                        H.Baslik,
                                        Tarih = H.DuzenlenmeTarihi
                                    }).Take(50).ToList();
            foreach (var H in HList)
            {
                SB.AppendLine("<url>");
                SB.AppendLine("<loc>");
                SB.AppendLine(Host + "/haberdetay/" + H.Id + "/" + MenuUrl.MenuUrlDuzenle(H.Baslik));
                SB.AppendLine("</loc>");
                SB.AppendLine("<lastmod>");
                SB.AppendLine(H.Tarih.ToString());
                SB.AppendLine("</lastmod>");
                SB.AppendLine("<changefreq>");
                SB.AppendLine("always");
                SB.AppendLine("</changefreq>");
                SB.AppendLine("<priority>");
                SB.AppendLine("1");
                SB.AppendLine("</priority>");
                SB.AppendLine("</url>");
            }

            #endregion

            #region SMDuyurular

            var DList = (from DK in Veriler.DuyuruKategorileri
                         join DT in Veriler.DuyurularTablosu
                             on DK.Id equals DT.KategoriId
                         join D in Veriler.Duyurular
                             on DT.DuyuruId equals D.Id
                         where DK.DilId == DilId
                               && DK.Durum == true
                               && D.Durum == true
                         orderby D.BaslangicTarihi descending
                         select new
                                    {
                                        D.Id,
                                        D.Baslik,
                                        Tarih = D.BaslangicTarihi
                                    }).Take(50).ToList();
            foreach (var D in DList)
            {
                SB.AppendLine("<url>");
                SB.AppendLine("<loc>");
                SB.AppendLine(Host + "/haberdetay/" + D.Id + "/" + MenuUrl.MenuUrlDuzenle(D.Baslik));
                SB.AppendLine("</loc>");
                SB.AppendLine("<lastmod>");
                SB.AppendLine(D.Tarih.ToString());
                SB.AppendLine("</lastmod>");
                SB.AppendLine("<changefreq>");
                SB.AppendLine("always");
                SB.AppendLine("</changefreq>");
                SB.AppendLine("<priority>");
                SB.AppendLine("1");
                SB.AppendLine("</priority>");
                SB.AppendLine("</url>");
            }

            #endregion

            #region SMEtkinlikler

            var EList = (from EK in Veriler.EtkinlikKategorileri
                         join E in Veriler.Etkinlikler
                             on EK.Id equals E.EtkinlikKategoriId
                         where EK.DilId == DilId
                               && EK.Durum == true
                               && E.Durum == true
                         orderby E.BaslangicTarihi descending
                         select new
                                    {
                                        E.Id,
                                        Baslik = E.Ad,
                                        Tarih = E.BaslangicTarihi
                                    }).Take(50).ToList();
            foreach (var E in DList)
            {
                SB.AppendLine("<url>");
                SB.AppendLine("<loc>");
                SB.AppendLine(Host + "/haberdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Baslik));
                SB.AppendLine("</loc>");
                SB.AppendLine("<lastmod>");
                SB.AppendLine(E.Tarih.ToString());
                SB.AppendLine("</lastmod>");
                SB.AppendLine("<changefreq>");
                SB.AppendLine("always");
                SB.AppendLine("</changefreq>");
                SB.AppendLine("<priority>");
                SB.AppendLine("1");
                SB.AppendLine("</priority>");
                SB.AppendLine("</url>");
            }

            #endregion

            #region SMIhaleler

            var IList = (from IK in Veriler.IhaleKategorileri
                         join I in Veriler.Ihaleler
                             on IK.Id equals I.IhaleKategoriId
                         where IK.DilId == DilId
                               && IK.Durum == true
                               && I.Durum == true
                         orderby I.BaslangicTarihi descending
                         select new
                                    {
                                        I.Id,
                                        Baslik = I.Ad,
                                        Tarih = I.BaslangicTarihi
                                    }).Take(50).ToList();
            foreach (var I in IList)
            {
                SB.AppendLine("<url>");
                SB.AppendLine("<loc>");
                SB.AppendLine(Host + "/haberdetay/" + I.Id + "/" + MenuUrl.MenuUrlDuzenle(I.Baslik));
                SB.AppendLine("</loc>");
                SB.AppendLine("<lastmod>");
                SB.AppendLine(I.Tarih.ToString());
                SB.AppendLine("</lastmod>");
                SB.AppendLine("<changefreq>");
                SB.AppendLine("always");
                SB.AppendLine("</changefreq>");
                SB.AppendLine("<priority>");
                SB.AppendLine("1");
                SB.AppendLine("</priority>");
                SB.AppendLine("</url>");
            }

            #endregion

            SB.AppendLine("</urlset>");
            Response.ContentType = "text/xml";
            Response.Write(SB.ToString());
            Response.End();
        }
    }
}
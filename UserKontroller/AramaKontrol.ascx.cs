using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class AramaKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["kategori"] != null)
                    {
                        if (Request.QueryString["code"] != null)
                        {
                            AramaSonuclariVer(Request.QueryString["kategori"], Request.QueryString["code"]);
                        }
                    }
                }
            }
        }

        public string HtmlDegistir(string Kelime)
        {
            Kelime = Kelime.Replace("&uuml;", "u");
            Kelime = Kelime.Replace("&Uuml;", "u");
            Kelime = Kelime.Replace("&ccedil;", "c");
            Kelime = Kelime.Replace("&Ccedil;", "c");
            Kelime = Kelime.Replace("&ouml;", "o");
            Kelime = Kelime.Replace("&Ouml;", "o");
            return Kelime;
        }

        public string QueryStringeTersCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.ToLower();
            ArananKelime = ArananKelime.Replace("u_", "&uuml;");
            ArananKelime = ArananKelime.Replace("_u", "&Uuml;");
            ArananKelime = ArananKelime.Replace("o_", "&ouml;");
            ArananKelime = ArananKelime.Replace("_o", "&Ouml;");
            ArananKelime = ArananKelime.Replace("c_", "&Ccedil;");
            ArananKelime = ArananKelime.Replace("_c", "&Ccedil;");
            ArananKelime = ArananKelime.Replace("s_", "ş");
            ArananKelime = ArananKelime.Replace("_s", "Ş");
            ArananKelime = ArananKelime.Replace("i_", "ı");
            ArananKelime = ArananKelime.Replace("_i", "I");
            ArananKelime = ArananKelime.Replace("g_", "ğ");
            ArananKelime = ArananKelime.Replace("_g", "Ğ");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }

        private void MenulerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            List<Menuler> MenulerList = null;
            string Sql = "it.DilId = " + DilId + " and it.Durum = True and it.MenuTipId = 3";
            foreach (string AK in ArananKelimeler)
            {
                Sql += " and (it.Aciklama like '%"
                       + QueryStringeTersCevir(AK)
                       + "%' or it.AnahtarKelimeler like '%"
                       + QueryStringeTersCevir(AK)
                       + "%' or it.Icerik like '%"
                       + QueryStringeTersCevir(AK)
                       + "%' or it.MenuAdi like '%"
                       + QueryStringeTersCevir(AK)
                       + "%' or it.Ozet like '%"
                       + QueryStringeTersCevir(AK)
                       + "%')";
            }
            MenulerList = Veriler.Menuler.Where(Sql).ToList();
            if (MenulerList.Count != 0)
            {
                Arama MenulerListArama = new Arama();
                MenulerListArama.Ara =
                    "<div class=\"searchtitle\">İçerik bölümünde " + MenulerList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(MenulerListArama);
            }
            foreach (Menuler M in MenulerList)
            {
                Arama A = new Arama();
                A.Ara =
                    "<table class=\"searchresult\"><tr><td>"
                    + "<div class=\"searchresulttitle\"><a href=\""
                    + "/sayfa/"
                    + M.Id
                    + "/"
                    + MenuUrl.MenuUrlDuzenle(M.MenuAdi)
                    + "\">"
                    + M.MenuAdi
                    + "</a></div> <div class=\"searchresultozet\">"
                    + M.Ozet
                    + "</div></td></tr></table>";
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void HaberlerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var HaberlerList = new List<Haberler>();
            string S =
                "select H.Id as Id, H.Baslik as Baslik, H.Ozet as Ozet, H.GorselThumbnail1 as GorselThumbnail1 from HaberKategorileri as HK join HaberlerTablosu as HT on HK.Id==HT.KategoriId join Haberler as H on HT.HaberId==H.Id where HK.DilId==" +
                DilId + " and HK.Durum==True and H.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( H.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                     + "%' or H.Baslik like '%" + QueryStringeTersCevir(AK)
                     + "%' or H.Ozet like '%" + QueryStringeTersCevir(AK)
                     + "%' or H.Icerik like '%" + QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Haberler H = new Haberler();
                H.Id = Convert.ToInt32(item["Id"].ToString());
                H.Baslik = item["Baslik"].ToString();
                H.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    H.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    H.GorselThumbnail1 = null;
                }
                HaberlerList.Add(H);
            }
            if (HaberlerList.Count != 0)
            {
                Arama HaberlerListArama = new Arama();
                HaberlerListArama.Ara =
                    "<div class=\"searchtitle\">Haberler bölümünde " + HaberlerList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(HaberlerListArama);
            }
            foreach (Haberler H in HaberlerList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(H.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"><a href=\""
                        + "/haberdetay/"
                        + H.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(H.Baslik)
                        + "\">"
                        + H.Baslik
                        + "</a></div><div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + H.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + H.Baslik
                        + "\"/></div> <div class=\"searchresultozet\">"
                        + H.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"><a href=\""
                        + "/haberdetay/"
                        + H.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(H.Baslik)
                        + "\">"
                        + H.Baslik
                        + "</a></div><divclass=\"searchresultozet\">"
                        + H.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void DuyurulardaArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var DuyurularList = new List<Duyurular>();
            string S =
                "select D.Id as Id, D.Baslik as Baslik, D.Ozet as Ozet, D.GorselThumbnail1 as GorselThumbnail1 from DuyuruKategorileri as DK join DuyurularTablosu as DT on DK.Id==DT.KategoriId join Duyurular as D on DT.DuyuruId==D.Id where DK.DilId==" +
                DilId + " and DK.Durum==True and D.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( D.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                     + "%' or D.Baslik like '%" + QueryStringeTersCevir(AK)
                     + "%' or D.Ozet like '%" + QueryStringeTersCevir(AK)
                     + "%' or D.Icerik like '%" + QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Duyurular D = new Duyurular();
                D.Id = Convert.ToInt32(item["Id"].ToString());
                D.Baslik = item["Baslik"].ToString();
                D.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    D.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    D.GorselThumbnail1 = null;
                }
                DuyurularList.Add(D);
            }
            if (DuyurularList.Count != 0)
            {
                Arama DuyurularListArama = new Arama();
                DuyurularListArama.Ara =
                    "<div class=\"searchtitle\">Duyurular bölümünde " + DuyurularList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(DuyurularListArama);
            }
            foreach (Duyurular D in DuyurularList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(D.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/duyurudetay/"
                        + D.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(D.Baslik)
                        + "\">"
                        + D.Baslik
                        + "</a></div><div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + D.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + D.Baslik
                        + "\"/></div> <div class=\"searchresultozet\">"
                        + D.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/duyurudetay/"
                        + D.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(D.Baslik)
                        + "\">"
                        + D.Baslik
                        + "</a></div><div class=\"searchresultozet\">"
                        + D.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void EtkinliklerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var EtkinliklerList = new List<Etkinlikler>();
            string S =
                "select E.Id as Id, E.Ad as Ad, E.Ozet as Ozet, E.GorselThumbnail1 as GorselThumbnail1 from EtkinlikKategorileri as EK join Etkinlikler as E on EK.Id==E.EtkinlikKategoriId where EK.DilId==" +
                DilId + " and EK.Durum==True and E.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( E.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                     + "%' or E.Ad like '%" + QueryStringeTersCevir(AK)
                     + "%' or E.Ozet like '%" + QueryStringeTersCevir(AK)
                     + "%' or E.Icerik like '%" + QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Etkinlikler E = new Etkinlikler();
                E.Id = Convert.ToInt32(item["Id"].ToString());
                E.Ad = item["Ad"].ToString();
                E.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    E.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    E.GorselThumbnail1 = null;
                }
                EtkinliklerList.Add(E);
            }
            if (EtkinliklerList.Count != 0)
            {
                Arama EtkinliklerListArama = new Arama();
                EtkinliklerListArama.Ara =
                    "<div class=\"searchtitle\">Etkinlikler bölümünde " + EtkinliklerList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(EtkinliklerListArama);
            }
            foreach (Etkinlikler E in EtkinliklerList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(E.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/etkinlikdetay/"
                        + E.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(E.Ad)
                        + "\">"
                        + E.Ad
                        + "</a></div><div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + E.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + E.Ad
                        + "\"/></div> <div class=\"searchresultozet\">"
                        + E.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/etkinlikdetay/"
                        + E.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(E.Ad)
                        + "\">"
                        + E.Ad
                        + "</a></div><div class=\"searchresultozet\">"
                        + E.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void IhalelerdeArama(int DilId, List<string> ArananKelimeler, List<Arama> AramaList)
        {
            var IhalelerList = new List<Ihaleler>();
            string S =
                "select I.Id as Id, I.Ad as Ad, I.Ozet as Ozet, I.GorselThumbnail1 as GorselThumbnail1 from IhaleKategorileri as IK join Ihaleler as I on IK.Id==I.IhaleKategoriId where IK.DilId==" +
                DilId + " and IK.Durum==True and I.Durum==True";
            foreach (string AK in ArananKelimeler)
            {
                S += " and ( I.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                     + "%' or I.Ad like '%" + QueryStringeTersCevir(AK)
                     + "%' or I.Ozet like '%" + QueryStringeTersCevir(AK)
                     + "%' or I.Icerik like '%" + QueryStringeTersCevir(AK)
                     + "%')";
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                Ihaleler I = new Ihaleler();
                I.Id = Convert.ToInt32(item["Id"].ToString());
                I.Ad = item["Ad"].ToString();
                I.Ozet = item["Ozet"].ToString();
                if (item["GorselThumbnail1"] != null)
                {
                    I.GorselThumbnail1 = item["GorselThumbnail1"].ToString();
                }
                else
                {
                    I.GorselThumbnail1 = null;
                }
                IhalelerList.Add(I);
            }
            if (IhalelerList.Count != 0)
            {
                Arama IhalelerListArama = new Arama();
                IhalelerListArama.Ara =
                    "<div class=\"searchtitle\">İhaleler bölümünde " + IhalelerList.Count.ToString() +
                    " adet sonuç bulunmuştur.</div>";
                AramaList.Add(IhalelerListArama);
            }
            foreach (Ihaleler I in IhalelerList)
            {
                Arama A = new Arama();
                if (!string.IsNullOrEmpty(I.GorselThumbnail1))
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/ihaledetay/"
                        + I.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(I.Ad)
                        + "\">"
                        + I.Ad
                        + "</a></div><div class=\"searchresultgorsel\"><img width=\"75\" src=\""
                        + I.GorselThumbnail1.Replace("~/", "../../../")
                        + "\""
                        + " alt=\""
                        + I.Ad
                        + "\"/></div> <div class=\"searchresultozet\">"
                        + I.Ozet
                        + "</div></td></tr></table>";
                }
                else
                {
                    A.Ara =
                        "<table class=\"searchresult\"><tr><td>"
                        + "<div class=\"searchresulttitle\"> <a href=\""
                        + "/ihaledetay/"
                        + I.Id
                        + "/"
                        + MenuUrl.MenuUrlDuzenle(I.Ad)
                        + "\">"
                        + I.Ad
                        + "</a></div><div class=\"searchresultozet\">"
                        + I.Ozet
                        + "</div></td></tr></table>";
                }
                if (!AramaList.Contains(A))
                {
                    AramaList.Add(A);
                }
            }
        }

        private void AramaSonuclariVer(string Kategori, string ArananKelime)
        {
            var AraList = new List<Arama>();
            var ArananKelimeler = ArananKelime.Split('-').ToList();
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            switch (Kategori)
            {
                case "0":
                    MenulerdeArama(DilId, ArananKelimeler, AraList);
                    HaberlerdeArama(DilId, ArananKelimeler, AraList);
                    DuyurulardaArama(DilId, ArananKelimeler, AraList);
                    EtkinliklerdeArama(DilId, ArananKelimeler, AraList);
                    IhalelerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "1":
                    MenulerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "2":
                    HaberlerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "3":
                    DuyurulardaArama(DilId, ArananKelimeler, AraList);
                    break;
                case "4":
                    EtkinliklerdeArama(DilId, ArananKelimeler, AraList);
                    break;
                case "5":
                    IhalelerdeArama(DilId, ArananKelimeler, AraList);
                    break;
            }
            ListView1.DataSource = AraList;
            ListView1.DataBind();
        }

        private void BannerGorseliVer()
        {
            Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
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
                            currentLink.NavigateUrl
                                = currentLink.NavigateUrl.Replace("/arama.aspx?", "/arama/");
                            currentLink.NavigateUrl =
                                currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl =
                                currentLink.NavigateUrl.Replace("aramapage=", "");
                            currentLink.NavigateUrl =
                                currentLink.NavigateUrl.Replace("&", "/");
                        }
                    }
                }
            }
        }

        #region Nested type: Arama

        public class Arama
        {
            public string Ara { get; set; }
        }

        #endregion
    }
}
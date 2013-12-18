using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class CenazelerListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: Cenazeler";
                IlleriVer(DropDownListDefinYeri);
                IlleriVer(DropDownListDogumYeri);
                // YillariVer(DropDownListDefinYili);
                MahalleleriVer(DropDownListMahalleAdi);
                CenazeleriVer();
            }
        }

        private void CenazeleriVer()
        {
            var CenazelerList = (from p in Veriler.Cenazeler
                                 join p1 in Veriler.Cinsiyetler
                                     on p.CinsiyetId equals p1.Id
                                 join p2 in Veriler.Iller
                                     on p.DogumYeriIlId equals p2.Id
                                 join p3 in Veriler.Mahalleler
                                     on p.YasadigiYerMahalleId equals p3.Id
                                 join p4 in Veriler.VefatNedenleri
                                     on p.VefatNedeniId equals p4.Id
                                 join p5 in Veriler.Iller
                                     on p.DefinYeriIlId equals p5.Id
                                 join p6 in Veriler.CenazelerDefinZamanlari
                                     on p.DefinZamaniId equals p6.Id
                                 join p7 in Veriler.Meslekler
                                     on p.MeslekId equals p7.Id
                                 orderby p.VefatTarihi descending
                                 select new
                                            {
                                                p.Id,
                                                p.Ad,
                                                p.Soyad,
                                                p1.Cinsiyet,
                                                Resim =
                                     p.Resim != null
                                         ? p.Resim.Replace("~/", "../../")
                                         : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                                DogumYeri = p2.IlAdi,
                                                YasadigiYer = p3.MahalleAdi,
                                                p.Yas,
                                                p.YakiniAd,
                                                p.YakiniSoyad,
                                                p.YakiniTel,
                                                p.VefatTarihi,
                                                p4.VefatNedeni,
                                                DefinYeri = p5.IlAdi,
                                                p6.DefinZamani,
                                                p.DefinTarihi,
                                                Meslek = p7.MeslekAdi,
                                                p.Adres,
                                                p.Yemek,
                                                p.Otobus,
                                                p.NamazaIstirak,
                                                p.EvdeTaziye,
                                                p.DigerHizmetler,
                                                p.Aciklama,
                                                p.KayitTarihi
                                            }).ToList();
            ListView1.DataSource = CenazelerList;
            ListView1.DataBind();
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/cenazelerlist.aspx?",
                                                                                      "/cenazeler/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("cenazelerpage=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("&", "/");
                        }
                    }
                }
            }
        }

        private void BannerGorseliVer()
        {
            CenazelerBannerResmi CBR = Veriler.CenazelerBannerResmi.FirstOrDefault();
            if (CBR != null)
            {
                if (!string.IsNullOrEmpty(CBR.Resim))
                {
                    Resim.Style.Add("background-image", CBR.Resim);
                }
                else
                {
                    Resim.Style.Add("background-image", "../../App_Themes/PendikMainTheme/Images/Default_banner.png");
                }
            }
            else
            {
                Resim.Style.Add("background-image", "../../App_Themes/PendikMainTheme/Images/Default_banner.png");
            }
        }

        private void DogumYeriIlleriVer(DropDownList DropDownList)
        {
            var IList = Veriler.Iller.ToList();
            DropDownList.DataTextField = "IlAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = IList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void IlleriVer(DropDownList DropDownList)
        {
            var IList = Veriler.Iller.ToList();
            DropDownList.DataTextField = "IlAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = IList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void YillariVer(DropDownList DropDownList)
        {
            int Yil = Convert.ToInt32(DateTime.Now.Year);
            int YilTemp = Yil - 5;
            for (int i = Yil; i >= YilTemp; i--)
            {
                DropDownList.Items.Add(i.ToString());
            }
            DropDownList.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        private void MahalleleriVer(DropDownList DropDownList)
        {
            var MList = Veriler.Mahalleler.ToList();
            DropDownList.DataTextField = "MahalleAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = MList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Tümü", "0"));
        }

        protected void ImageButtonAra_Click(object sender, ImageClickEventArgs e)
        {
            string Sorgu = string.Empty;
            Sorgu +=
                "select C.Id as Id, C.Ad as Ad, C.Soyad as Soyad, CI.Cinsiyet as Cinsiyet, case when C.Resim != '' then C.Resim else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as Resim, I1.IlAdi as DogumYeri, M.MahalleAdi as YasadigiYer, C.Yas as Yas, C.YakiniAd as YakiniAd, C.YakiniSoyad as YakiniSoyad, C.YakiniTel as YakiniTel, C.VefatTarihi as VefatTarihi, VN.VefatNedeni as VefatNedeni, I2.IlAdi as DefinYeri, CDZ.DefinZamani as DefinZamani, C.DefinTarihi as DefinTarihi, ME.MeslekAdi as Meslek, C.Adres as Adres, C.Yemek as Yemek, C.Otobus as Otobus, C.NamazaIstirak as NamazaIstirak, C.EvdeTaziye as EvdeTaziye, C.DigerHizmetler as DigerHizmetler, C.Aciklama as Aciklama, C.KayitTarihi as KayitTarihi from Cenazeler as C join Cinsiyetler as CI on C.CinsiyetId == CI.Id join Iller as I1 on C.DogumYeriIlId == I1.Id join Mahalleler as M on C.YasadigiYerMahalleId == M.Id join VefatNedenleri as VN on C.VefatNedeniId == VN.Id join Iller as I2 on C.DefinYeriIlId == I2.Id join CenazelerDefinZamanlari as CDZ on C.DefinZamaniId == CDZ.Id join Meslekler as ME on C.MeslekId == ME.Id where (";
            Sorgu += " C.Ad like '%" + TextBoxAd.Text + "%' ";
            Sorgu += " and C.Soyad like '%" + TextBoxSoyad.Text + "%' ";
            if (DropDownListDogumYeri.SelectedIndex != 0)
            {
                Sorgu += " and I1.IlAdi like '%" + DropDownListDogumYeri.SelectedItem.Text + "%' ";
            }
            if (DropDownListDefinYeri.SelectedIndex != 0)
            {
                Sorgu += " and I2.IlAdi like '%" + DropDownListDefinYeri.SelectedItem.Text + "%' ";
            }
            //if (DropDownListDefinYili.SelectedIndex != 0)
            //{
            //    DateTime BaslangicTarihi =
            //      Convert.ToDateTime(DropDownListDefinYili.SelectedItem.Text
            //      + "-01-01 00:00:00.000");
            //    DateTime BitisTarihi =
            //        Convert.ToDateTime(DropDownListDefinYili.SelectedItem.Text
            //        + "-12-31 23:59:59.999");

            //    Sorgu += " and C.DefinTarihi between ('"
            //        + DropDownListDefinYili.SelectedItem.Text
            //        + "-01-01 00:00:00.0000000') and ('"
            //        + DropDownListDefinYili.SelectedItem.Text
            //        + "-12-31 23:59:59.9999999')";

            //    //Sorgu += " and C.DefinTarihi between ('" + "01.01.2012 00:00:00" + "') and ('" + "31.12.2012 23:59:59" + "')";

            //    // CONVERT(nvarchar(4), C.DefinTarihi, 120) like '2012'

            //    //Sorgu += " and CONVERT(nvarchar(4), C.DefinTarihi, 120) like '" + DropDownListDefinYili.SelectedItem.Text + "'";
            //    // Sorgu += " and C.DefinTarihi =2012-04-11 10:24:00.000'";

            //    //Sorgu += " and C.DefinTarihi in('" + DropDownListDefinYili.SelectedItem.Text + "'";
            //}
            if (DropDownListMahalleAdi.SelectedIndex != 0)
            {
                Sorgu += " and M.MahalleAdi like '%" + DropDownListMahalleAdi.SelectedItem.Text + "%' ";
            }
            Sorgu += " and C.Aciklama like '%" + TextBoxAciklama.Text + "%' ";
            Sorgu += " ) order by C.VefatTarihi desc";
            var CTList = new List<CenazelerTemp>();
            var S = Veriler.CreateQuery<DbDataRecord>(Sorgu);
            foreach (var Item in S)
            {
                CenazelerTemp C = new CenazelerTemp();
                C.Id = Convert.ToInt32(Item["Id"].ToString());
                C.Ad = Item["Ad"].ToString();
                C.Soyad = Item["Soyad"].ToString();
                C.Cinsiyet = Item["Cinsiyet"].ToString();
                C.Resim = Item["Resim"].ToString().Replace("~/", "../");
                C.DogumYeri = Item["DogumYeri"].ToString();
                C.YasadigiYer = Item["YasadigiYer"].ToString();
                C.Yas = Convert.ToInt32(Item["Yas"].ToString());
                C.YakiniAd = Item["YakiniAd"].ToString();
                C.YakiniSoyad = Item["YakiniSoyad"].ToString();
                C.YakiniTel = Item["YakiniTel"].ToString();
                C.VefatTarihi = Convert.ToDateTime(Item["VefatTarihi"].ToString());
                C.VefatNedeni = Item["VefatNedeni"].ToString();
                C.DefinYeri = Item["DefinYeri"].ToString();
                C.DefinZamani = Item["DefinZamani"].ToString();
                C.DefinTarihi = Convert.ToDateTime(Item["DefinTarihi"].ToString());
                C.Meslek = Item["Meslek"].ToString();
                C.Adres = Item["Adres"].ToString();
                C.Yemek = Convert.ToBoolean(Item["Yemek"].ToString());
                C.Otobus = Convert.ToBoolean(Item["Otobus"].ToString());
                C.NamazaIstirak = Convert.ToBoolean(Item["NamazaIstirak"].ToString());
                C.EvdeTaziye = Convert.ToBoolean(Item["EvdeTaziye"].ToString());
                C.DigerHizmetler = Item["DigerHizmetler"].ToString();
                C.Aciklama = Item["Aciklama"].ToString();
                C.KayitTarihi = Convert.ToDateTime(Item["KayitTarihi"].ToString());
                CTList.Add(C);
            }
            ListView1.DataSource = CTList;
            ListView1.DataBind();
        }

        #region Nested type: CenazelerTemp

        public class CenazelerTemp
        {
            public int Id { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Cinsiyet { get; set; }
            public string Resim { get; set; }
            public string DogumYeri { get; set; }
            public string YasadigiYer { get; set; }
            public int Yas { get; set; }
            public string YakiniAd { get; set; }
            public string YakiniSoyad { get; set; }
            public string YakiniTel { get; set; }
            public DateTime VefatTarihi { get; set; }
            public string VefatNedeni { get; set; }
            public string DefinYeri { get; set; }
            public string DefinZamani { get; set; }
            public DateTime DefinTarihi { get; set; }
            public string Meslek { get; set; }
            public string Adres { get; set; }
            public bool Yemek { get; set; }
            public bool Otobus { get; set; }
            public bool NamazaIstirak { get; set; }
            public bool EvdeTaziye { get; set; }
            public string DigerHizmetler { get; set; }
            public string Aciklama { get; set; }
            public DateTime KayitTarihi { get; set; }
        }

        #endregion
    }
}
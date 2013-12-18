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
    public partial class BelediyeHizmetleriListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Temizle();
                BannerGorseliVer();
                Page.Title = Page.Header.Title + " :: A'dan Z'ye Belediye Hizmetleri";
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.QueryString["code"] != "0")
                        {
                            if (Request.QueryString["arama"] != null)
                            {
                                BelediyeHizmetlerindedeAra(
                                    Convert.ToInt32(Request.QueryString["code"]), Request.QueryString["arama"]);
                            }
                            else
                            {
                                BelediyeHizmetleriVer(Convert.ToInt32(Request.QueryString["code"]));
                            }
                        }
                        else
                        {
                            if (Request.QueryString["arama"] != null)
                            {
                                BelediyeHizmetlerindedeAra(
                                    Convert.ToInt32(Request.QueryString["code"]), Request.QueryString["arama"]);
                            }
                            else
                            {
                                BelediyeHizmetleriVer(0);
                            }
                        }
                    }
                }
            }
        }

        private void BelediyeHizmetleriVer(int KategoriId)
        {
            if (KategoriId != 0)
            {
                var BelediyeHizmetleriList = (from p in Veriler.BelediyeHizmetleri
                                              join p1 in Veriler.BelediyeHizmetleriKategorileri
                                                  on p.BelediyeHizmetleriKategoriId equals p1.Id
                                              where p.BelediyeHizmetleriKategoriId == KategoriId
                                                    && p.Durum == true
                                                    && p1.Durum == true
                                              orderby p1.SiraNo
                                              select new
                                                         {
                                                             p.Id,
                                                             p.Soru,
                                                             p.Cevap
                                                         }).ToList();
                ListView1.DataSource = BelediyeHizmetleriList;
                ListView1.DataBind();
            }
            else
            {
                var BelediyeHizmetleriList = (from p in Veriler.BelediyeHizmetleri
                                              where p.Durum == true
                                              orderby p.Tarih descending
                                              select new
                                                         {
                                                             p.Id,
                                                             p.Soru,
                                                             p.Cevap
                                                         }).ToList();
                ListView1.DataSource = BelediyeHizmetleriList;
                ListView1.DataBind();
            }
        }

        public string QueryStringeTersCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.ToLower();
            ArananKelime = ArananKelime.Replace("u_", "ü");
            ArananKelime = ArananKelime.Replace("o_", "ö");
            ArananKelime = ArananKelime.Replace("c_", "ç");
            ArananKelime = ArananKelime.Replace("s_", "ş");
            ArananKelime = ArananKelime.Replace("i_", "ı");
            ArananKelime = ArananKelime.Replace("g_", "ğ");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }

        private void BelediyeHizmetlerindedeAra(int KategoriId, string ArananKelime)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHList = new List<BelediyeHizmetleri>();
            var ArananKelimeler = ArananKelime.Split('-').ToList();
            string S = string.Empty;
            if (KategoriId == 0)
            {
                S = "select BH.Soru as Soru, BH.Cevap as Cevap from BelediyeHizmetleri as BH join BelediyeHizmetleriKategorileri as BHK on BH.BelediyeHizmetleriKategoriId == BHK.Id where BHK.Durum==True and BHK.DilId == "
                    + DilId
                    + " and BH.Durum==True ";
                foreach (string AK in ArananKelimeler)
                {
                    S += " and ( BH.Soru like '%" + QueryStringeTersCevir(AK)
                         + "%' or BH.Cevap like '%" + QueryStringeTersCevir(AK)
                         + "%' or BH.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                         + "%')";
                }
            }
            else
            {
                S = "select BH.Soru as Soru, BH.Cevap as Cevap from BelediyeHizmetleri as BH where BH.BelediyeHizmetleriKategoriId =="
                    + KategoriId
                    + " and BH.Durum==True ";
                foreach (string AK in ArananKelimeler)
                {
                    S += " and ( BH.Soru like '%" + QueryStringeTersCevir(AK)
                         + "%' or BH.Cevap like '%" + QueryStringeTersCevir(AK)
                         + "%' or BH.AnahtarKelimeler like '%" + QueryStringeTersCevir(AK)
                         + "%')";
                }
            }
            var Sorgu = Veriler.CreateQuery<DbDataRecord>(S);
            foreach (var item in Sorgu)
            {
                BelediyeHizmetleri BH = new BelediyeHizmetleri();
                BH.Soru = item["Soru"].ToString();
                BH.Cevap = item["Cevap"].ToString();
                BHList.Add(BH);
            }
            ListView1.DataSource = BHList;
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
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("/belediyehizmetlerilist.aspx?",
                                                                                      "/belediyehizmetleri/");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("code=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("title=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("belediyehizmetleripage=", "");
                            currentLink.NavigateUrl = currentLink.NavigateUrl.Replace("&", "/");
                        }
                    }
                }
            }
        }

        private void BannerGorseliVer()
        {
            BelediyeHizmetleriBannerResmi BHBR = Veriler.BelediyeHizmetleriBannerResmi.FirstOrDefault();
            if (BHBR != null)
            {
                if (!string.IsNullOrEmpty(BHBR.Resim))
                {
                    Resim.Style.Add("background-image", BHBR.Resim);
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

        protected void ButtonGonder_Click(object sender, EventArgs e)
        {
            try
            {
                BelediyeHizmetleriSorular BHS = new BelediyeHizmetleriSorular();
                BHS.Soru = TextBoxSoru.Text;
                BHS.SorulanTarih = DateTime.Now;
                BHS.Ad = TextBoxAd.Text;
                BHS.Soyad = TextBoxSoyad.Text;
                BHS.EPosta = TextBoxEPosta.Text;
                if (RadioButtonEvet.Checked)
                {
                    BHS.BilgilendirmeTalebi = true;
                }
                else
                {
                    BHS.BilgilendirmeTalebi = false;
                }
                BHS.CevaplandiMi = false;
                BHS.CevaplananTarih = null;
                Veriler.AddToBelediyeHizmetleriSorular(BHS);
                Veriler.SaveChanges();
                Temizle();
                LabelMesaj.Text = "Sorunuz kayıt edilmiştir. Teşekkürler.";
            }
            catch (Exception Hata)
            {
                LabelMesaj.Text = "Hata oluştu!";
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        private void Temizle()
        {
            TextBoxSoru.Text = string.Empty;
            TextBoxAd.Text = string.Empty;
            TextBoxSoyad.Text = string.Empty;
            TextBoxEPosta.Text = string.Empty;
            LabelMesaj.Text = string.Empty;
        }
    }
}
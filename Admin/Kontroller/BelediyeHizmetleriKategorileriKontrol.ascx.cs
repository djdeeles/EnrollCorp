using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class BelediyeHizmetleriKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Kategoriler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 15))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void Temizle()
        {
            TextBoxKategoriAdi.Text = string.Empty;
            SiraNolariniVer();
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void SiraNolariniVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHK = Veriler.BelediyeHizmetleriKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = BHK.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = BHK.Count() + 1;
            }
            for (int i = 1; i <= Sayi; i++)
            {
                DropDownListSiraNolari.Items.Add(i.ToString());
            }
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Belediye Hizmetleri Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BelediyeHizmetleriKategorileri BHK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    BHK = Veriler.BelediyeHizmetleriKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(BHK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    BHK.KategoriAdi = TextBoxKategoriAdi.Text;
                    BHK.SiraNo = YeniSiraNo;
                    BHK.Durum = CheckBoxDurum.Checked;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                    BHK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BHK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    BHK = new BelediyeHizmetleriKategorileri();
                    BHK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    BHK.KategoriAdi = TextBoxKategoriAdi.Text;
                    BHK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    BHK.Durum = CheckBoxDurum.Checked;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    BHK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BHK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToBelediyeHizmetleriKategorileri(BHK);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        private void SiraNolariniGuncelle(int SiraNo)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHKS =
                Veriler.BelediyeHizmetleriKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (BelediyeHizmetleriKategorileri BHK in BHKS)
            {
                BHK.SiraNo = BHK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<BelediyeHizmetleriKategorileri> BHKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    BHKS =
                        Veriler.BelediyeHizmetleriKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (BelediyeHizmetleriKategorileri BHK in BHKS)
                    {
                        BHK.SiraNo = BHK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    BHKS =
                        Veriler.BelediyeHizmetleriKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (BelediyeHizmetleriKategorileri BHK in BHKS)
                    {
                        BHK.SiraNo = BHK.SiraNo - 1;
                        Veriler.SaveChanges();
                    }
                }
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                BelediyeHizmetleriKategorileri BHK =
                    Veriler.BelediyeHizmetleriKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(BHK);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                BelediyeHizmetleriKategorileri BHK =
                    Veriler.BelediyeHizmetleriKategorileri.Where(p => p.Id == Id).First();
                Veriler.BelediyeHizmetleriKategorileri.DeleteObject(BHK);
                Veriler.SaveChanges();
                var BHKList =
                    Veriler.BelediyeHizmetleriKategorileri.Where(p => p.SiraNo >= BHK.SiraNo && p.DilId == DilId).ToList
                        ();
                foreach (BelediyeHizmetleriKategorileri BHKK in BHKList)
                {
                    BelediyeHizmetleriKategorileri BHKKK =
                        Veriler.BelediyeHizmetleriKategorileri.Where(p => p.Id == BHKK.Id).First();
                    BHKKK.SiraNo = BHKKK.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(BelediyeHizmetleriKategorileri BHK)
        {
            TextBoxKategoriAdi.Text = BHK.KategoriAdi;
            CheckBoxDurum.Checked = BHK.Durum.Value;
            HiddenFieldId.Value = BHK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = BHK.SiraNo.Value.ToString();
            LabelBaslik.Text = "Belediye Hizmetleri Kategori Düzenle";
        }
    }
}
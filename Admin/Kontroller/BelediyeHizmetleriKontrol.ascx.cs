using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class BelediyeHizmetleriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Hizmetler Yönetimi";
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
                    KategorileriVer(DropDownListKategorilerGridView, new ListItem("Tümü", "0"));
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by Tarih desc";
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
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            TextBoxSoru.Text = string.Empty;
            RadEditorCevap.Content = string.Empty;
            RadDateTimePickerTarih.SelectedDate = DateTime.Now;
            TextBoxAnahtarKelimeler.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void KategorileriVer(DropDownList DropDownListKategoriler, ListItem ListItem)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var BHK =
                Veriler.BelediyeHizmetleriKategorileri.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(
                    p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = BHK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, ListItem);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Belediye Hizmetleri Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BelediyeHizmetleri BH;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    BH = Veriler.BelediyeHizmetleri.Where(p => p.Id == Id).First();
                    BH.BelediyeHizmetleriKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    BH.Soru = TextBoxSoru.Text;
                    BH.Cevap = RadEditorCevap.Content;
                    BH.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    BH.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    BH.Durum = CheckBoxDurum.Checked;
                    BH.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BH.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.Id=="
                        + BH.BelediyeHizmetleriKategoriId.ToString()
                        + " order by Tarih desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    BH = new BelediyeHizmetleri();
                    BH.BelediyeHizmetleriKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    BH.Soru = TextBoxSoru.Text;
                    BH.Cevap = RadEditorCevap.Content;
                    BH.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    BH.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    BH.Durum = CheckBoxDurum.Checked;
                    BH.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BH.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToBelediyeHizmetleri(BH);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.Id=="
                        + BH.BelediyeHizmetleriKategoriId.ToString()
                        + " order by Tarih desc";
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
                BelediyeHizmetleri BH = Veriler.BelediyeHizmetleri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(BH);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                BelediyeHizmetleri BH = Veriler.BelediyeHizmetleri.Where(p => p.Id == Id).FirstOrDefault();
                Veriler.BelediyeHizmetleri.DeleteObject(BH);
                Veriler.SaveChanges();
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " order by p.[Tarih] desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.Id=="
                        + KategoriId.ToString()
                        + " order by p.[Tarih] desc";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }

            else if (e.CommandName == "Sort")
            {
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " order by p.[Tarih] desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.Id=="
                        + KategoriId
                        + " order by p.[Tarih] desc";
                }
            }
        }

        private void Guncelle(BelediyeHizmetleri BH)
        {
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            DropDownListKategoriler.SelectedValue = BH.BelediyeHizmetleriKategoriId.ToString();
            TextBoxSoru.Text = BH.Soru;
            RadEditorCevap.Content = BH.Cevap;
            RadDateTimePickerTarih.SelectedDate = BH.Tarih.Value;
            TextBoxAnahtarKelimeler.Text = BH.AnahtarKelimeler;
            CheckBoxDurum.Checked = BH.Durum.Value;
            HiddenFieldId.Value = BH.Id.ToString();
            LabelBaslik.Text = "Belediye Hizmetleri Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " order by p.[Tarih] desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Soru], p.[Tarih], p.[Durum] from BelediyeHizmetleri as p join BelediyeHizmetleriKategorileri as p1 on p.BelediyeHizmetleriKategoriId==p1.Id where p1.[Durum]==true and p1.Id=="
                    + KategoriId.ToString()
                    + " order by p.[Tarih] desc";
            }
            GridViewVeriler.DataBind();
        }
    }
}
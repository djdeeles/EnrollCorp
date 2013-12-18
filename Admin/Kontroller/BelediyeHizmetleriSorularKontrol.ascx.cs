using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class BelediyeHizmetleriSorularKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Sorular Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 15))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p order by SorulanTarih desc";
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
            LabelSoru.Text = string.Empty;
            LabelAd.Text = string.Empty;
            LabelSoyad.Text = string.Empty;
            LabelEPosta.Text = string.Empty;
            LabelTarih.Text = string.Empty;
            LabelBilgilendirme.Text = string.Empty;
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

        private void BelediyeHizmetleriSorularVer(BelediyeHizmetleriSorular BHS)
        {
            LabelSoru.Text = BHS.Soru;
            LabelAd.Text = BHS.Ad;
            LabelSoyad.Text = BHS.Soyad;
            LabelEPosta.Text = BHS.EPosta;
            LabelTarih.Text = BHS.SorulanTarih.ToString();
            LabelBilgilendirme.Text = "İstenmiyor";
            if (BHS.BilgilendirmeTalebi.Value)
            {
                LabelBilgilendirme.Text = "isteniyor";
            }
            HiddenFieldId.Value = BHS.Id.ToString();
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BelediyeHizmetleri BH = new BelediyeHizmetleri();
                BH.BelediyeHizmetleriKategoriId =
                    Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                BH.Soru = TextBoxSoru.Text;
                BH.Cevap = RadEditorCevap.Content;
                BH.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                BH.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                BH.Durum = CheckBoxDurum.Checked;
                Veriler.AddToBelediyeHizmetleri(BH);
                Veriler.SaveChanges();
                BelediyeHizmetleriSorularGuncelle(Convert.ToInt32(HiddenFieldId.Value), BH.Tarih.Value);
                Temizle();

                int BHSId = Convert.ToInt16(HiddenFieldId.Value);
                BelediyeHizmetleriSorular BHS = Veriler.BelediyeHizmetleriSorular.Where(p => p.Id == BHSId).First();
                if (BHS.BilgilendirmeTalebi == true)
                {
                    string Link = Request.Url.Host + "";
                    MailDefinition MD = new MailDefinition();
                    ListDictionary LD = new ListDictionary();
                    LD.Add("%%Ad%%", BHS.Ad);
                    LD.Add("%%Soyad%%", BHS.Soyad);
                    LD.Add("%%Tarih%%", BHS.SorulanTarih);
                    LD.Add("%%Soru%%", BHS.Soru);
                    LD.Add("%%WebKategori%%", DropDownListKategoriler.SelectedItem.Text);
                    LD.Add("%%WebSoru%%", BH.Soru);
                    LD.Add("%%WebCevap%%", BH.Cevap);
                    LD.Add("%%Link%%", Link);
                    MD.IsBodyHtml = true;
                    MD.BodyFileName = "Templates/belediyehizmetleribilgilendirme.htm";
                    MD.Subject = "Pendik Belediyesi Bilginlendirme";
                    MD.From = "localhost";
                    MailMessage MM = MD.CreateMailMessage(BHS.EPosta, LD, this);
                    MM.BodyEncoding = Encoding.Default;
                    MM.SubjectEncoding = Encoding.Default;
                    MM.Priority = MailPriority.High;
                    SmtpClient SC = new SmtpClient("localhost", 25);
                    // smtp.Credentials = new System.Net.NetworkCredential("kultursanat@pendik.bel.tr", "Pendik4918");
                    SC.Send(MM);
                }
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 1)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]!=null order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 2)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]==null order by SorulanTarih desc";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = -1;
                MesajKontrol2.Mesaj(true, "Kayıt edildi.");
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        private void BelediyeHizmetleriSorularGuncelle(int Id, DateTime Tarih)
        {
            BelediyeHizmetleriSorular BHS = Veriler.BelediyeHizmetleriSorular.Where(p => p.Id == Id).First();
            BHS.CevaplandiMi = true;
            BHS.CevaplananTarih = Tarih;
            Veriler.SaveChanges();
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                BelediyeHizmetleriSorular BHS = Veriler.BelediyeHizmetleriSorular.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 0;
                Guncelle(BHS);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                BelediyeHizmetleriSorular BHS = Veriler.BelediyeHizmetleriSorular.Where(p => p.Id == Id).First();
                Veriler.BelediyeHizmetleriSorular.DeleteObject(BHS);
                Veriler.SaveChanges();
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 1)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]!=null order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 2)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]==null order by SorulanTarih desc";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = -1;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 1)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]!=null order by SorulanTarih desc";
                }
                else if (DropDownListKategorilerGridView.SelectedIndex == 2)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi] from BelediyeHizmetleriSorular as p where p.[CevaplananTarih]==null order by SorulanTarih desc";
                }
            }
        }

        private void Guncelle(BelediyeHizmetleriSorular BHS)
        {
            Temizle();
            BelediyeHizmetleriSorularVer(BHS);
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi], p.[CevaplananTarih] from BelediyeHizmetleriSorular as p order by SorulanTarih desc";
            }
            else if (DropDownListKategorilerGridView.SelectedIndex == 1)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi], p.[CevaplananTarih] from BelediyeHizmetleriSorular as p where p.[CevaplandiMi]==True order by SorulanTarih desc";
            }
            else if (DropDownListKategorilerGridView.SelectedIndex == 2)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Soru], p.[SorulanTarih], p.[BilgilendirmeTalebi], p.[CevaplananTarih] from BelediyeHizmetleriSorular as p where p.[CevaplandiMi]==False order by SorulanTarih desc";
            }
            GridViewVeriler.DataBind();
        }
    }
}
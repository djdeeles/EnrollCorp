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
    public partial class HizmetSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Hizmet Slider Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 6))
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
        }

        private void Temizle()
        {
            TextBoxHizmetAdi.Text = string.Empty;
            TextBoxResim.Text = string.Empty;
            TextBoxUrl.Text = string.Empty;
            SiraNolariniVer();
            CheckBoxDurum.Checked = false;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
            ImageButtonResim.OnClientClick = "window.open('FileManager.aspx?returnField="
                                             + TextBoxResim.ClientID + "','','width=740,height=740');";
        }

        private void SiraNolariniVer()
        {
            var HizmetSlider = Veriler.HizmetSlider.ToList();
            int Sayi = HizmetSlider.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenField1.Value == string.Empty)
            {
                Sayi = HizmetSlider.Count() + 1;
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
            SiraNolariniVer();
            LabelBaslik.Text = "Hizmet Slider Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HizmetSlider HS;
                if (HiddenField1.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenField1.Value);
                    HS = Veriler.HizmetSlider.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(HS.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    HS.HizmetAdi = TextBoxHizmetAdi.Text;
                    HS.Resim = TextBoxResim.Text;
                    HS.Url = TextBoxUrl.Text;
                    HS.SiraNo = YeniSiraNo;
                    HS.Durum = CheckBoxDurum.Checked;
                    HS.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HS.DuzenlemeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    HS = new HizmetSlider();
                    HS.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    HS.HizmetAdi = TextBoxHizmetAdi.Text;
                    HS.Resim = TextBoxResim.Text;
                    HS.Url = TextBoxUrl.Text;
                    HS.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    HS.Durum = CheckBoxDurum.Checked;
                    HS.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HS.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToHizmetSlider(HS);
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
            var HSS = Veriler.HizmetSlider.Where(p => p.SiraNo >= SiraNo).ToList();
            foreach (HizmetSlider HS in HSS)
            {
                HS.SiraNo = HS.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            if (EskiSiraNo != YeniSiraNo)
            {
                List<HizmetSlider> HSS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    HSS = Veriler.HizmetSlider.Where(p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo).ToList();
                    foreach (HizmetSlider HS in HSS)
                    {
                        HS.SiraNo = HS.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    HSS = Veriler.HizmetSlider.Where(p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo).ToList();
                    foreach (HizmetSlider HS in HSS)
                    {
                        HS.SiraNo = HS.SiraNo - 1;
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
                MesajKontrol1.Reset();
                MesajKontrol2.Reset();
                int Id = Convert.ToInt32(e.CommandArgument);
                HizmetSlider HS = Veriler.HizmetSlider.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(HS);
            }
            else if (e.CommandName == "Sil")
            {
                MesajKontrol1.Reset();
                MesajKontrol2.Reset();
                int Id = Convert.ToInt32(e.CommandArgument);
                HizmetSlider HS = Veriler.HizmetSlider.Where(p => p.Id == Id).First();
                Veriler.DeleteObject(HS);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(HizmetSlider HS)
        {
            TextBoxHizmetAdi.Text = HS.HizmetAdi;
            TextBoxResim.Text = HS.Resim;
            TextBoxUrl.Text = HS.Url;
            CheckBoxDurum.Checked = HS.Durum.Value;
            HiddenField1.Value = HS.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = HS.SiraNo.Value.ToString();
            LabelBaslik.Text = "Hizmet Slider Düzenle";
        }
    }
}
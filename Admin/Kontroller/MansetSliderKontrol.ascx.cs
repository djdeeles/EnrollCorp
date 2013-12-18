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
    public partial class MansetSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Manşet Slider Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 4))
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
            TextBoxMansetAdi.Text = string.Empty;
            TextBoxResim.Text = string.Empty;
            TextBoxUrl.Text = string.Empty;
            SiraNolariniVer();
            CheckBoxDurum.Checked = false;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
            ImageButtonResim.OnClientClick = "window.open('FileManager.aspx?returnField="
                                             + TextBoxResim.ClientID + "','','width=740,height=740');";
            HiddenField1.Value = string.Empty;
        }

        private void SiraNolariniVer()
        {
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            int Sayi = Veriler.MansetSlider.Count();
            if (HiddenField1.Value == string.Empty)
            {
                Sayi = Sayi + 1;
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
            LabelBaslik.Text = "Manşet Slider Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                MansetSlider MS;
                if (HiddenField1.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenField1.Value);
                    MS = Veriler.MansetSlider.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(MS.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    MS.MansetAdi = TextBoxMansetAdi.Text;
                    MS.Resmi = TextBoxResim.Text;
                    MS.Url = TextBoxUrl.Text;
                    MS.SiraNo = YeniSiraNo;
                    MS.Durum = CheckBoxDurum.Checked;
                    MS.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    MS.DuzenlemeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    MS = new MansetSlider();
                    MS.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    MS.MansetAdi = TextBoxMansetAdi.Text;
                    MS.Resmi = TextBoxResim.Text;
                    MS.Url = TextBoxUrl.Text;
                    MS.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    MS.Durum = CheckBoxDurum.Checked;
                    MS.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    MS.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToMansetSlider(MS);
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
            var MSS = Veriler.MansetSlider.Where(p => p.SiraNo >= SiraNo).ToList();
            foreach (MansetSlider MS in MSS)
            {
                MS.SiraNo = MS.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            if (EskiSiraNo != YeniSiraNo)
            {
                List<MansetSlider> MSS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    MSS = Veriler.MansetSlider.Where(p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo).ToList();
                    foreach (MansetSlider MS in MSS)
                    {
                        MS.SiraNo = MS.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    MSS = Veriler.MansetSlider.Where(p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo).ToList();
                    foreach (MansetSlider MS in MSS)
                    {
                        MS.SiraNo = MS.SiraNo - 1;
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
                MansetSlider MS = Veriler.MansetSlider.Where(p => p.Id == Id).First();

                MultiView1.ActiveViewIndex = 1;
                Guncelle(MS);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                MansetSlider MS = Veriler.MansetSlider.Where(p => p.Id == Id).First();
                Veriler.DeleteObject(MS);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(MansetSlider MS)
        {
            Temizle();
            TextBoxMansetAdi.Text = MS.MansetAdi;
            TextBoxResim.Text = MS.Resmi;
            TextBoxUrl.Text = MS.Url;
            CheckBoxDurum.Checked = MS.Durum.Value;
            HiddenField1.Value = MS.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = MS.SiraNo.Value.ToString();
            LabelBaslik.Text = "Manşet Slider Düzenle";
        }
    }
}
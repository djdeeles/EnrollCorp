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
    public partial class BannerSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Banner Slider Yönetimi";
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
            TextBoxBannerAdi.Text = string.Empty;
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
            var BannerSlider = Veriler.BannerSlider.ToList();
            int Sayi = BannerSlider.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenField1.Value == string.Empty)
            {
                Sayi = BannerSlider.Count() + 1;
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
            LabelBaslik.Text = "Banner Slider Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BannerSlider BS;
                if (HiddenField1.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenField1.Value);
                    BS = Veriler.BannerSlider.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(BS.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    BS.BannerAdi = TextBoxBannerAdi.Text;
                    BS.Resim = TextBoxResim.Text;
                    BS.Url = TextBoxUrl.Text;
                    BS.SiraNo = YeniSiraNo;
                    BS.Durum = CheckBoxDurum.Checked;
                    BS.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BS.DuzenlemeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                }
                else
                {
                    BS = new BannerSlider();
                    BS.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    BS.BannerAdi = TextBoxBannerAdi.Text;
                    BS.Resim = TextBoxResim.Text;
                    BS.Url = TextBoxUrl.Text;
                    BS.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    BS.Durum = CheckBoxDurum.Checked;
                    BS.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BS.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToBannerSlider(BS);
                }
                Veriler.SaveChanges();
                Temizle();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt edildi.");
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        private void SiraNolariniGuncelle(int SiraNo)
        {
            var BSS = Veriler.BannerSlider.Where(p => p.SiraNo >= SiraNo).ToList();
            foreach (BannerSlider BS in BSS)
            {
                BS.SiraNo = BS.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            if (EskiSiraNo != YeniSiraNo)
            {
                List<BannerSlider> BSS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    BSS = Veriler.BannerSlider.Where(p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo).ToList();
                    foreach (BannerSlider BS in BSS)
                    {
                        BS.SiraNo = BS.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    BSS = Veriler.BannerSlider.Where(p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo).ToList();
                    foreach (BannerSlider BS in BSS)
                    {
                        BS.SiraNo = BS.SiraNo - 1;
                        Veriler.SaveChanges();
                    }
                }
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                MesajKontrol1.Reset();
                MesajKontrol2.Reset();
                int Id = Convert.ToInt32(e.CommandArgument);
                BannerSlider BS = Veriler.BannerSlider.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(BS);
            }
            else if (e.CommandName == "Sil")
            {
                MesajKontrol1.Reset();
                MesajKontrol2.Reset();
                int Id = Convert.ToInt32(e.CommandArgument);
                BannerSlider BS = Veriler.BannerSlider.Where(p => p.Id == Id).First();
                Veriler.DeleteObject(BS);
                Veriler.SaveChanges();
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
            }
        }

        private void Guncelle(BannerSlider BS)
        {
            TextBoxBannerAdi.Text = BS.BannerAdi;
            TextBoxResim.Text = BS.Resim;
            TextBoxUrl.Text = BS.Url;
            CheckBoxDurum.Checked = BS.Durum.Value;
            HiddenField1.Value = BS.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = BS.SiraNo.Value.ToString();
            LabelBaslik.Text = "Banner Slider Düzenle";
        }
    }
}
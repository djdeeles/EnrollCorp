using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class IhaleKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İhale Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 10))
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
            var IK = Veriler.IhaleKategorileri.Where(p => p.DilId == DilId).ToList();
            int Sayi = IK.Count();
            DropDownListSiraNolari.DataSource = string.Empty;
            DropDownListSiraNolari.DataBind();
            if (HiddenFieldId.Value == string.Empty)
            {
                Sayi = IK.Count() + 1;
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
            LabelBaslik.Text = "İhale Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                IhaleKategorileri IK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    IK = Veriler.IhaleKategorileri.Where(p => p.Id == Id).First();
                    int EskiSiraNo = Convert.ToInt32(IK.SiraNo);
                    int YeniSiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    IK.KategoriAdi = TextBoxKategoriAdi.Text;
                    IK.SiraNo = YeniSiraNo;
                    IK.Durum = CheckBoxDurum.Checked;
                    IK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IK.DuzenlemeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(EskiSiraNo, YeniSiraNo);
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    IK = new IhaleKategorileri();
                    IK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    IK.KategoriAdi = TextBoxKategoriAdi.Text;
                    IK.SiraNo = Convert.ToInt32(DropDownListSiraNolari.SelectedValue);
                    IK.Durum = CheckBoxDurum.Checked;
                    IK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IK.KaydetmeTarihi = DateTime.Now;
                    SiraNolariniGuncelle(Convert.ToInt32(DropDownListSiraNolari.SelectedValue));
                    Veriler.AddToIhaleKategorileri(IK);
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
            var IKS = Veriler.IhaleKategorileri.Where(p => p.SiraNo >= SiraNo && p.DilId == DilId).ToList();
            foreach (IhaleKategorileri IK in IKS)
            {
                IK.SiraNo = IK.SiraNo + 1;
                Veriler.SaveChanges();
            }
        }

        private void SiraNolariniGuncelle(int EskiSiraNo, int YeniSiraNo)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            if (EskiSiraNo != YeniSiraNo)
            {
                List<IhaleKategorileri> IKS;
                if (EskiSiraNo > YeniSiraNo)
                {
                    IKS =
                        Veriler.IhaleKategorileri.Where(
                            p => p.SiraNo < EskiSiraNo && p.SiraNo >= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (IhaleKategorileri IK in IKS)
                    {
                        IK.SiraNo = IK.SiraNo + 1;
                        Veriler.SaveChanges();
                    }
                }
                else if (EskiSiraNo < YeniSiraNo)
                {
                    IKS =
                        Veriler.IhaleKategorileri.Where(
                            p => p.SiraNo > EskiSiraNo && p.SiraNo <= YeniSiraNo && p.DilId == DilId).ToList();
                    foreach (IhaleKategorileri IK in IKS)
                    {
                        IK.SiraNo = IK.SiraNo - 1;
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

        private void GorselSil(string Resim)
        {
            if (File.Exists(Server.MapPath(Resim)))
            {
                File.Delete(Server.MapPath(Resim));
            }
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                IhaleKategorileri IK = Veriler.IhaleKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(IK);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                IhaleKategorileri IK = Veriler.IhaleKategorileri.Where(p => p.Id == Id).First();
                var IList = Veriler.Ihaleler.Where(p => p.IhaleKategoriId == IK.Id).ToList();
                foreach (Ihaleler I in IList)
                {
                    Veriler.Ihaleler.DeleteObject(I);
                    Veriler.SaveChanges();
                    GorselSil(I.Gorsel1);
                    GorselSil(I.GorselThumbnail1);
                    GorselSil(I.Gorsel2);
                    GorselSil(I.GorselThumbnail2);
                    GorselSil(I.Gorsel3);
                    GorselSil(I.GorselThumbnail3);
                    GorselSil(I.Gorsel4);
                    GorselSil(I.GorselThumbnail4);
                }
                Veriler.IhaleKategorileri.DeleteObject(IK);
                Veriler.SaveChanges();
                var IKList = Veriler.IhaleKategorileri.Where(p => p.SiraNo >= IK.SiraNo && p.DilId == DilId).ToList();
                foreach (IhaleKategorileri IKK in IKList)
                {
                    IhaleKategorileri IhaleKategorileri = Veriler.IhaleKategorileri.Where(p => p.Id == IKK.Id).First();
                    IhaleKategorileri.SiraNo = IhaleKategorileri.SiraNo - 1;
                    Veriler.SaveChanges();
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(IhaleKategorileri IK)
        {
            TextBoxKategoriAdi.Text = IK.KategoriAdi;
            CheckBoxDurum.Checked = IK.Durum.Value;
            HiddenFieldId.Value = IK.Id.ToString();
            SiraNolariniVer();
            DropDownListSiraNolari.SelectedValue = IK.SiraNo.Value.ToString();
            LabelBaslik.Text = "İhale Kategori Düzenle";
        }
    }
}
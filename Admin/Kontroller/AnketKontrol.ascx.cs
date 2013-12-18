using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class AnketKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Anket Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 5))
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
            TextBoxSoru.Text = string.Empty;
            CheckBoxSoruDurum.Checked = false;
            HiddenFieldSoruId.Value = string.Empty;
            MesajKontrol1.Reset();
            TextBoxCevap.Text = string.Empty;
            TextBoxOysayisi.Text = string.Empty;
            CheckBoxCevapDurum.Checked = false;
            MesajKontrol2.Reset();
            HiddenFieldSoruId1.Value = string.Empty;
            HiddenFieldCevapId.Value = string.Empty;
            MesajKontrol3.Reset();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelSoruBaslik.Text = "Anket Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AnketSorulari AnketSorulari;
                if (HiddenFieldSoruId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldSoruId.Value);
                    AnketSorulari = Veriler.AnketSorulari.Where(p => p.Id == Id).First();
                    AnketSorulari.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    AnketSorulari.Soru = TextBoxSoru.Text;
                    AnketSorulari.Durum = CheckBoxSoruDurum.Checked;
                    AnketSorulari.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    AnketSorulari.DuzenlemeTarihi = DateTime.Now;
                }
                else
                {
                    AnketSorulari = new AnketSorulari();
                    AnketSorulari.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    AnketSorulari.Soru = TextBoxSoru.Text;
                    AnketSorulari.Durum = CheckBoxSoruDurum.Checked;
                    AnketSorulari.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    AnketSorulari.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToAnketSorulari(AnketSorulari);
                }
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = 0;
                Temizle();
                MesajKontrol3.Mesaj(true, "Kayıt edildi.");
                GridViewAnketSorular.DataBind();
            }
            catch (Exception Hata)
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewAnketSorular_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ImageButtonDuzenle = (ImageButton) e.Row.FindControl("ImageButtonDuzenle");
                GridView GridViewAnketCevaplar = ((GridView) e.Row.FindControl("GridViewAnketCevaplar"));
                EntityDataSourceAnketCevaplari.Where = "it.AnketSorulariId=" + ImageButtonDuzenle.CommandArgument;
                GridViewAnketCevaplar.DataSource = EntityDataSourceAnketCevaplari;
                GridViewAnketCevaplar.DataBind();
            }
        }

        protected void GridViewAnketSorular_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SoruDuzenle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                AnketSorulari AnketSorulari = Veriler.AnketSorulari.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Temizle();
                TextBoxSoru.Text = AnketSorulari.Soru;
                CheckBoxSoruDurum.Checked = AnketSorulari.Durum.Value;
                HiddenFieldSoruId.Value = Id.ToString();
                LabelSoruBaslik.Text = "Anket Düzenle";
            }
            else if (e.CommandName == "SoruSil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                AnketSorulari AnketSorulari = Veriler.AnketSorulari.Where(p => p.Id == Id).First();
                Veriler.AnketSorulari.DeleteObject(AnketSorulari);
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = 0;
                Temizle();
                MesajKontrol3.Mesaj(true, "Silindi.");
                GridViewAnketSorular.DataBind();
            }
            else if (e.CommandName == "CevapEkle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Temizle();
                MultiView1.ActiveViewIndex = 2;
                HiddenFieldSoruId1.Value = Id.ToString();
                LabelCevapBaslik.Text = "Cevap Ekle";
            }
        }

        protected void GridViewAnketCevaplar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CevapDuzenle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                AnketCevaplari AnketCevaplari = Veriler.AnketCevaplari.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 2;
                Temizle();
                TextBoxCevap.Text = AnketCevaplari.Cevap;
                TextBoxOysayisi.Text = AnketCevaplari.OySayisi.Value.ToString();
                CheckBoxCevapDurum.Checked = AnketCevaplari.Durum.Value;
                HiddenFieldSoruId1.Value = AnketCevaplari.AnketSorulariId.Value.ToString();
                HiddenFieldCevapId.Value = Id.ToString();
                LabelCevapBaslik.Text = "Cevap Düzenle";
            }
            else if (e.CommandName == "CevapSil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                AnketCevaplari AnketCevaplari = Veriler.AnketCevaplari.Where(p => p.Id == Id).First();
                Veriler.AnketCevaplari.DeleteObject(AnketCevaplari);
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = 0;
                Temizle();
                MesajKontrol3.Mesaj(true, "Silindi.");
                GridViewAnketSorular.DataBind();
            }
        }

        protected void ImageButtonKaydet1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AnketCevaplari AnketCevaplari;
                int AnketId = Convert.ToInt32(HiddenFieldSoruId1.Value);
                if (HiddenFieldCevapId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldCevapId.Value);
                    AnketCevaplari = Veriler.AnketCevaplari.Where(p => p.Id == Id).First();
                    AnketCevaplari.AnketSorulariId = AnketId;
                    AnketCevaplari.Cevap = TextBoxCevap.Text;
                    if (!string.IsNullOrEmpty(TextBoxOysayisi.Text))
                    {
                        AnketCevaplari.OySayisi = Convert.ToInt32(TextBoxOysayisi.Text);
                    }
                    else
                    {
                        AnketCevaplari.OySayisi = 0;
                    }
                    AnketCevaplari.Durum = CheckBoxCevapDurum.Checked;
                    AnketCevaplari.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    AnketCevaplari.DuzenlemeTarihi = DateTime.Now;
                }
                else
                {
                    AnketCevaplari = new AnketCevaplari();
                    AnketCevaplari.AnketSorulariId = AnketId;
                    AnketCevaplari.Cevap = TextBoxCevap.Text;
                    if (!string.IsNullOrEmpty(TextBoxOysayisi.Text))
                    {
                        AnketCevaplari.OySayisi = Convert.ToInt32(TextBoxOysayisi.Text);
                    }
                    else
                    {
                        AnketCevaplari.OySayisi = 0;
                    }
                    AnketCevaplari.Durum = CheckBoxSoruDurum.Checked;
                    AnketCevaplari.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    AnketCevaplari.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToAnketCevaplari(AnketCevaplari);
                }
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol3.Mesaj(true, "Kayıt edildi.");
                GridViewAnketSorular.DataBind();
            }
            catch (Exception Hata)
            {
                MesajKontrol2.Mesaj(false, "Hata oluştu.");
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
            }
        }

        protected void ImageButtonIptal1_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}
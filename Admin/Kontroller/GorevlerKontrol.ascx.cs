using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class GorevlerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "E-Posta Gönderimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 8))
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
            SablonlariVer(DropDownListSablonlar, new ListItem("Şeciniz.", "0"));
            TextBoxBaslik.Text = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            RadEditoIcerik.Content = string.Empty;
            RadDateTimePickerBaslamaTarihi.SelectedDate = DateTime.Now;
            CheckBoxOkunduBilgisi.Checked = false;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
        }

        private void SablonlariVer(DropDownList DropDownListSablonlar, ListItem ListItem)
        {
            var GS = Veriler.GorevSablonlari.Where(p => p.Durum == true).ToList();
            DropDownListSablonlar.DataTextField = "Baslik";
            DropDownListSablonlar.DataValueField = "Id";
            DropDownListSablonlar.DataSource = GS;
            DropDownListSablonlar.DataBind();
            DropDownListSablonlar.Items.Insert(0, ListItem);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Görev Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Gorevler G;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    G = Veriler.Gorevler.Where(p => p.Id == Id).First();
                    G.Baslik = TextBoxBaslik.Text;
                    G.GorevAciklamasi = TextBoxAciklama.Text;
                    G.Icerik = RadEditoIcerik.Content;
                    if (RadDateTimePickerBaslamaTarihi.SelectedDate != null)
                    {
                        G.BaslamaTarihi = Convert.ToDateTime(RadDateTimePickerBaslamaTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        G.BaslamaTarihi = null;
                    }
                    G.OkunduBilgisi = CheckBoxOkunduBilgisi.Checked;
                    G.Durum = CheckBoxDurum.Checked;
                    G.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    G.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    G = new Gorevler();
                    G.Baslik = TextBoxBaslik.Text;
                    G.GorevAciklamasi = TextBoxAciklama.Text;
                    G.Icerik = RadEditoIcerik.Content;
                    G.SorguCumlesi = "select HaberGrubuUyeligi.EPosta where HaberGrubuUyeligi.EPosta.Durum=True";
                    if (RadDateTimePickerBaslamaTarihi.SelectedDate != null)
                    {
                        G.BaslamaTarihi = Convert.ToDateTime(RadDateTimePickerBaslamaTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        G.BaslamaTarihi = null;
                    }
                    G.OkunduBilgisi = CheckBoxOkunduBilgisi.Checked;
                    G.Durum = CheckBoxDurum.Checked;
                    G.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    G.KayitTarihi = DateTime.Now;
                    Veriler.AddToGorevler(G);
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

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Gorevler G = Veriler.Gorevler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(G);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Gorevler G = Veriler.Gorevler.Where(p => p.Id == Id).First();
                Veriler.Gorevler.DeleteObject(G);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(Gorevler G)
        {
            SablonlariVer(DropDownListSablonlar, new ListItem("Şeciniz.", "0"));
            TextBoxBaslik.Text = G.Baslik;
            TextBoxAciklama.Text = G.GorevAciklamasi;
            RadEditoIcerik.Content = G.Icerik;
            if (G.BaslamaTarihi != null)
            {
                RadDateTimePickerBaslamaTarihi.SelectedDate = G.BaslamaTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslamaTarihi.SelectedDate = null;
            }
            CheckBoxOkunduBilgisi.Checked = G.OkunduBilgisi.Value;
            CheckBoxDurum.Checked = G.Durum.Value;
            HiddenFieldId.Value = G.Id.ToString();
            LabelBaslik.Text = "Görev Düzenle";
        }

        protected void DropDownListSablonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(DropDownListSablonlar.SelectedValue);
            if (Id != 0)
            {
                GorevSablonlari GS = Veriler.GorevSablonlari.Where(p => p.Id == Id).First();
                RadEditoIcerik.Content = GS.Icerik;
            }
            else
            {
                RadEditoIcerik.Content = string.Empty;
            }
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class VideoAlbumKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Albüm Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 13))
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
            TextBoxVideoAlbumKategoriAdi.Text = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Video Albüm Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                VideoAlbumKategorileri VAK;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    VAK = Veriler.VideoAlbumKategorileri.Where(p => p.Id == Id).First();
                    VAK.VideoAlbumKategoriAdi = TextBoxVideoAlbumKategoriAdi.Text;
                    VAK.Aciklama = TextBoxAciklama.Text;
                    VAK.Durum = CheckBoxDurum.Checked;
                    VAK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VAK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    VAK = new VideoAlbumKategorileri();
                    VAK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    VAK.VideoAlbumKategoriAdi = TextBoxVideoAlbumKategoriAdi.Text;
                    VAK.Aciklama = TextBoxAciklama.Text;
                    VAK.Durum = CheckBoxDurum.Checked;
                    VAK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VAK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToVideoAlbumKategorileri(VAK);
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
                VideoAlbumKategorileri VAK = Veriler.VideoAlbumKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(VAK);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                VideoAlbumKategorileri VAK = Veriler.VideoAlbumKategorileri.Where(p => p.Id == Id).First();
                var VAList = Veriler.VideoAlbumler.Where(p => p.VideoAlbumKategoriId == VAK.Id).ToList();
                foreach (VideoAlbumler VAV in VAList)
                {
                    VideoAlbumler VA = Veriler.VideoAlbumler.Where(p => p.Id == VAV.Id).First();
                    if (!string.IsNullOrEmpty(VA.Gorsel))
                    {
                        GorselSil(VA.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(VA.GorselThumbnail))
                    {
                        GorselSil(VA.GorselThumbnail);
                    }
                    var VAVlist = Veriler.VideoAlbumVideolari.Where(p => p.VideoAlbumId == VAV.Id).ToList();
                    foreach (VideoAlbumVideolari VAVTemp in VAVlist)
                    {
                        VideoAlbumVideolari VAVSil = Veriler.VideoAlbumVideolari.Where(p => p.Id == VAVTemp.Id).First();
                        if (!string.IsNullOrEmpty(VAVSil.Video))
                        {
                            GorselSil(VAVSil.Video);
                        }
                        if (!string.IsNullOrEmpty(VAVSil.Gorsel))
                        {
                            GorselSil(VAVSil.Gorsel);
                        }
                        Veriler.VideoAlbumVideolari.DeleteObject(VAVSil);
                        Veriler.SaveChanges();
                    }
                    Veriler.VideoAlbumler.DeleteObject(VA);
                    Veriler.SaveChanges();
                }
                Veriler.VideoAlbumKategorileri.DeleteObject(VAK);
                Veriler.SaveChanges();
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(VideoAlbumKategorileri VAK)
        {
            TextBoxVideoAlbumKategoriAdi.Text = VAK.VideoAlbumKategoriAdi;
            TextBoxAciklama.Text = VAK.Aciklama;
            CheckBoxDurum.Checked = VAK.Durum.Value;
            HiddenFieldId.Value = VAK.Id.ToString();
            LabelBaslik.Text = "Video Albüm Kategori Düzenle";
        }
    }
}
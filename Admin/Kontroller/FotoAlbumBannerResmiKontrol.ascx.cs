using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class FotoAlbumBannerResmiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Foto Albüm Banner Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 12))
                {
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
        }

        private void Temizle()
        {
            TextBoxBannerGorsel.Text = string.Empty;
            MesajKontrol1.Reset();
            ImageButtonBannerGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                                    + TextBoxBannerGorsel.ClientID + "','','width=640,height=480');";
            HiddenFieldId.Value = string.Empty;
            FotoAlbumBannerGorseliVer();
        }

        private void FotoAlbumBannerGorseliVer()
        {
            FotoAlbumBannerResmi FABR = Veriler.FotoAlbumBannerResmi.FirstOrDefault();
            if (FABR != null)
            {
                if (!string.IsNullOrEmpty(FABR.Resim))
                {
                    ImageBannerGorsel.Visible = true;
                    ImageBannerGorsel.ImageUrl = FABR.Resim;
                }
                else
                {
                    ImageBannerGorsel.Visible = false;
                }
                HiddenFieldId.Value = FABR.Id.ToString();
                TextBoxBannerGorsel.Text = FABR.Resim;
            }
            else
            {
                ImageBannerGorsel.Visible = false;
            }
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                FotoAlbumBannerResmi FABR;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    FABR = Veriler.FotoAlbumBannerResmi.Where(p => p.Id == Id).First();
                    if (!string.IsNullOrEmpty(TextBoxBannerGorsel.Text))
                    {
                        FABR.Resim = TextBoxBannerGorsel.Text;
                    }
                    else
                    {
                        FABR.Resim = null;
                    }
                    FABR.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FABR.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    FABR = new FotoAlbumBannerResmi();
                    FABR.Resim = TextBoxBannerGorsel.Text;
                    FABR.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FABR.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumBannerResmi(FABR);
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
            }
            catch
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu!");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            Temizle();
        }
    }
}
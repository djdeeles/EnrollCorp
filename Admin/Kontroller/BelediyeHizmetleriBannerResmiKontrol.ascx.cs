using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class BelediyeHizmetleriBannerResmiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Banner Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 15))
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
            BelediyeHizmetleriBannerGorseliVer();
        }

        private void BelediyeHizmetleriBannerGorseliVer()
        {
            BelediyeHizmetleriBannerResmi BHBR = Veriler.BelediyeHizmetleriBannerResmi.FirstOrDefault();
            if (BHBR != null)
            {
                if (!string.IsNullOrEmpty(BHBR.Resim))
                {
                    ImageBannerGorsel.Visible = true;
                    ImageBannerGorsel.ImageUrl = BHBR.Resim;
                }
                else
                {
                    ImageBannerGorsel.Visible = false;
                }
                HiddenFieldId.Value = BHBR.Id.ToString();
                TextBoxBannerGorsel.Text = BHBR.Resim;
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
                BelediyeHizmetleriBannerResmi BHBR;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    BHBR = Veriler.BelediyeHizmetleriBannerResmi.Where(p => p.Id == Id).First();
                    if (!string.IsNullOrEmpty(TextBoxBannerGorsel.Text))
                    {
                        BHBR.Resim = TextBoxBannerGorsel.Text;
                    }
                    else
                    {
                        BHBR.Resim = null;
                    }
                    BHBR.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BHBR.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    BHBR = new BelediyeHizmetleriBannerResmi();
                    BHBR.Resim = TextBoxBannerGorsel.Text;
                    BHBR.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    BHBR.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToBelediyeHizmetleriBannerResmi(BHBR);
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
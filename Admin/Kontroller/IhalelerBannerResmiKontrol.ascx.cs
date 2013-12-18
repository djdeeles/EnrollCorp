using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class IhalelerBannerResmiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İhaleler Banner Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 10))
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
            IhalelerBannerGorseliVer();
        }

        private void IhalelerBannerGorseliVer()
        {
            IhalelerBannerResmi IBR = Veriler.IhalelerBannerResmi.FirstOrDefault();
            if (IBR != null)
            {
                if (!string.IsNullOrEmpty(IBR.Resim))
                {
                    ImageBannerGorsel.Visible = true;
                    ImageBannerGorsel.ImageUrl = IBR.Resim;
                }
                else
                {
                    ImageBannerGorsel.Visible = false;
                }
                HiddenFieldId.Value = IBR.Id.ToString();
                TextBoxBannerGorsel.Text = IBR.Resim;
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
                IhalelerBannerResmi IBR;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    IBR = Veriler.IhalelerBannerResmi.Where(p => p.Id == Id).First();
                    if (!string.IsNullOrEmpty(TextBoxBannerGorsel.Text))
                    {
                        IBR.Resim = TextBoxBannerGorsel.Text;
                    }
                    else
                    {
                        IBR.Resim = null;
                    }
                    IBR.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IBR.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    IBR = new IhalelerBannerResmi();
                    IBR.Resim = TextBoxBannerGorsel.Text;
                    IBR.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    IBR.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToIhalelerBannerResmi(IBR);
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
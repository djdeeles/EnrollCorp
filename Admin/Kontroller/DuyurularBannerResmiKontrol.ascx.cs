using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class DuyurularBannerResmiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Duyuru Banner Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 7))
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
            DuyurularBannerGorseliVer();
        }

        private void DuyurularBannerGorseliVer()
        {
            DuyurularBannerResmi DBR = Veriler.DuyurularBannerResmi.FirstOrDefault();
            if (DBR != null)
            {
                if (!string.IsNullOrEmpty(DBR.Resim))
                {
                    ImageBannerGorsel.Visible = true;
                    ImageBannerGorsel.ImageUrl = DBR.Resim;
                }
                else
                {
                    ImageBannerGorsel.Visible = false;
                }
                HiddenFieldId.Value = DBR.Id.ToString();
                TextBoxBannerGorsel.Text = DBR.Resim;
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
                DuyurularBannerResmi DBR;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    DBR = Veriler.DuyurularBannerResmi.Where(p => p.Id == Id).First();
                    if (!string.IsNullOrEmpty(TextBoxBannerGorsel.Text))
                    {
                        DBR.Resim = TextBoxBannerGorsel.Text;
                    }
                    else
                    {
                        DBR.Resim = null;
                    }
                    DBR.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DBR.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    DBR = new DuyurularBannerResmi();
                    DBR.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DBR.KaydetmeTarihi = DateTime.Now;
                    DBR.Resim = TextBoxBannerGorsel.Text;
                    Veriler.AddToDuyurularBannerResmi(DBR);
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
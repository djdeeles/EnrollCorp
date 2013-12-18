﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class HaberlerBannerResmiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Haberler Banner Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 6))
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
            HaberlerBannerGorseliVer();
        }

        private void HaberlerBannerGorseliVer()
        {
            HaberlerBannerResmi HBR = Veriler.HaberlerBannerResmi.FirstOrDefault();
            if (HBR != null)
            {
                if (!string.IsNullOrEmpty(HBR.Resim))
                {
                    ImageBannerGorsel.Visible = true;
                    ImageBannerGorsel.ImageUrl = HBR.Resim;
                }
                else
                {
                    ImageBannerGorsel.Visible = false;
                }
                HiddenFieldId.Value = HBR.Id.ToString();
                TextBoxBannerGorsel.Text = HBR.Resim;
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
                HaberlerBannerResmi HBR;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    HBR = Veriler.HaberlerBannerResmi.Where(p => p.Id == Id).First();
                    if (!string.IsNullOrEmpty(TextBoxBannerGorsel.Text))
                    {
                        HBR.Resim = TextBoxBannerGorsel.Text;
                    }
                    else
                    {
                        HBR.Resim = null;
                    }
                    HBR.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HBR.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MesajKontrol1.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    HBR = new HaberlerBannerResmi();
                    HBR.Resim = TextBoxBannerGorsel.Text;
                    HBR.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    HBR.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToHaberlerBannerResmi(HBR);
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
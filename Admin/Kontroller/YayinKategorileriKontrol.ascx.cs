using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class YayinKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Yayın Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 14))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from YayinKategorileri as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
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
            ImageGorsel.Visible = false;
            TextBoxGorsel.Text = string.Empty;
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
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
            LabelBaslik.Text = "Yayın Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                YayinKategorileri YK;
                string AnaDizin = "../Gorseller";
                string Kategori = "YayinKategorileri";
                string Yil = DateTime.Now.Year.ToString();
                string Ay = string.Empty;
                int A = DateTime.Now.Month;
                if (A < 9)
                {
                    Ay = "0" + A.ToString();
                }
                else
                {
                    Ay = A.ToString();
                }
                string Thumbnails = "Thumbnails";
                if (!Directory.Exists(Server.MapPath(AnaDizin)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil));
                }
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay)))
                {
                    Directory.CreateDirectory(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay));
                }
                if (
                    !Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails));
                }
                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriThumbnail = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    YK = Veriler.YayinKategorileri.Where(p => p.Id == Id).First();
                    YK.KategoriAdi = TextBoxKategoriAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != YK.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(YK.Gorsel))
                            {
                                GorselSil(YK.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(YK.GorselThumbnail))
                            {
                                GorselSil(YK.GorselThumbnail);
                            }
                            YK.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            YK.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(YK.Gorsel))
                        {
                            GorselSil(YK.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(YK.GorselThumbnail))
                        {
                            GorselSil(YK.GorselThumbnail);
                        }
                        YK.Gorsel = null;
                        YK.GorselThumbnail = null;
                    }
                    YK.Aciklama = TextBoxAciklama.Text;
                    YK.Durum = CheckBoxDurum.Checked;
                    YK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    YK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from YayinKategorileri as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    YK = new YayinKategorileri();
                    YK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    YK.KategoriAdi = TextBoxKategoriAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        YK.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        YK.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        YK.Gorsel = null;
                        YK.GorselThumbnail = null;
                    }
                    YK.Aciklama = TextBoxAciklama.Text;
                    YK.Durum = CheckBoxDurum.Checked;
                    YK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    YK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToYayinKategorileri(YK);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from YayinKategorileri as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
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

        public string GorselBoyutlandir(TextBox ResimYolu, string KayitYeri, int Genislik)
        {
            Bitmap OrjinalResim = new Bitmap(Server.MapPath(ResimYolu.Text.Replace("~", "..")));
            ImageFormat Format = OrjinalResim.RawFormat;
            ImageCodecInfo ICI;
            if (Format.Equals(ImageFormat.Gif) || Format.Equals(ImageFormat.Png))
            {
                ICI = GetEncoder(ImageFormat.Png);
            }
            else
            {
                ICI = GetEncoder(ImageFormat.Jpeg);
            }
            Encoder E = Encoder.Quality;
            EncoderParameters EP = new EncoderParameters(1);
            EncoderParameter eParam = new EncoderParameter(E, 70L);
            EP.Param[0] = eParam;
            Bitmap YeniResim = null;
            Graphics Graphic = null;
            if (OrjinalResim.Width >= OrjinalResim.Height)
            {
                if (OrjinalResim.Width > Genislik)
                {
                    decimal ORW = OrjinalResim.Width;
                    decimal ORH = OrjinalResim.Height;
                    decimal ORO = ORW/ORH;
                    int NW = Genislik;
                    decimal NHT = NW/ORO;
                    int NH = Convert.ToInt16(NHT);
                    YeniResim = new Bitmap(OrjinalResim, NW, NH);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, NW, NH);
                }
                else
                {
                    YeniResim = new Bitmap(OrjinalResim, OrjinalResim.Width, OrjinalResim.Height);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, OrjinalResim.Width, OrjinalResim.Height);
                }
            }
            else
            {
                if (OrjinalResim.Height > Genislik)
                {
                    decimal ORO = OrjinalResim.Height/OrjinalResim.Width;
                    int NW = Convert.ToInt16(Genislik/ORO);
                    YeniResim = new Bitmap(OrjinalResim, NW, Genislik);
                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, NW, Genislik);
                }
                else
                {
                    YeniResim = new Bitmap(OrjinalResim, OrjinalResim.Width, OrjinalResim.Height);

                    Graphic = Graphics.FromImage(YeniResim);
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.DrawImage(OrjinalResim, 0, 0, OrjinalResim.Width, OrjinalResim.Height);
                }
            }
            string ResimAdi = Guid.NewGuid() + ".jpg";
            YeniResim.Save(Server.MapPath(KayitYeri + ResimAdi), ICI, EP);
            return KayitYeri.Replace("../", "~/") + ResimAdi;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void GorselSil(string Resim)
        {
            if (File.Exists(Server.MapPath(Resim)))
            {
                File.Delete(Server.MapPath(Resim));
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                YayinKategorileri YK = Veriler.YayinKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(YK);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                YayinKategorileri YK = Veriler.YayinKategorileri.Where(p => p.Id == Id).First();
                var YList = Veriler.Yayinlar.Where(p => p.YayinKategoriId == YK.Id).ToList();
                foreach (Yayinlar Y in YList)
                {
                    GorselSil(Y.Gorsel);
                    GorselSil(Y.GorselThumbnail);
                    Veriler.Yayinlar.DeleteObject(Y);
                    Veriler.SaveChanges();
                }
                GorselSil(YK.Gorsel);
                GorselSil(YK.GorselThumbnail);
                Veriler.YayinKategorileri.DeleteObject(YK);
                Veriler.SaveChanges();
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from YayinKategorileri as p where p.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(YayinKategorileri YK)
        {
            TextBoxKategoriAdi.Text = YK.KategoriAdi;
            if (!string.IsNullOrEmpty(YK.GorselThumbnail))
            {
                ImageGorsel.Visible = true;
                ImageGorsel.ImageUrl = YK.GorselThumbnail;
                TextBoxGorsel.Text = YK.Gorsel;
            }
            TextBoxAciklama.Text = YK.Aciklama;
            CheckBoxDurum.Checked = YK.Durum.Value;
            HiddenFieldId.Value = YK.Id.ToString();
            LabelBaslik.Text = "Yayın Kategori Düzenle";
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
        }
    }
}
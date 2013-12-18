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
    public partial class DokumanKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Doküman Kategorileri Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 16))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanKategorileri as p where p.DilId=="
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
            LabelBaslik.Text = "Doküman Kategori Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DokumanKategorileri DK;
                string AnaDizin = "../Gorseller";
                string Kategori = "DokumanKategorileri";
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
                    DK = Veriler.DokumanKategorileri.Where(p => p.Id == Id).First();
                    DK.KategoriAdi = TextBoxKategoriAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != DK.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(DK.Gorsel))
                            {
                                GorselSil(DK.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(DK.GorselThumbnail))
                            {
                                GorselSil(DK.GorselThumbnail);
                            }
                            DK.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            DK.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(DK.Gorsel))
                        {
                            GorselSil(DK.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(DK.GorselThumbnail))
                        {
                            GorselSil(DK.GorselThumbnail);
                        }
                        DK.Gorsel = null;
                        DK.GorselThumbnail = null;
                    }
                    DK.Aciklama = TextBoxAciklama.Text;
                    DK.Durum = CheckBoxDurum.Checked;
                    DK.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DK.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanKategorileri as p where p.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    DK = new DokumanKategorileri();
                    DK.DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    DK.KategoriAdi = TextBoxKategoriAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        DK.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        DK.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        DK.Gorsel = null;
                        DK.GorselThumbnail = null;
                    }
                    DK.Aciklama = TextBoxAciklama.Text;
                    DK.Durum = CheckBoxDurum.Checked;
                    DK.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DK.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToDokumanKategorileri(DK);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanKategorileri as p where p.DilId=="
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
                DokumanKategorileri DK = Veriler.DokumanKategorileri.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(DK);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                DokumanKategorileri DK = Veriler.DokumanKategorileri.Where(p => p.Id == Id).First();
                var DYList = Veriler.DokumanYayinlari.Where(p => p.DokumanKategoriId == DK.Id).ToList();
                foreach (DokumanYayinlari DY in DYList)
                {
                    if (!string.IsNullOrEmpty(DY.Gorsel))
                    {
                        GorselSil(DY.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(DY.GorselThumbnail))
                    {
                        GorselSil(DY.GorselThumbnail);
                    }
                    Veriler.DokumanYayinlari.DeleteObject(DY);
                    Veriler.SaveChanges();
                }
                if (!string.IsNullOrEmpty(DK.Gorsel))
                {
                    GorselSil(DK.Gorsel);
                }
                if (!string.IsNullOrEmpty(DK.GorselThumbnail))
                {
                    GorselSil(DK.GorselThumbnail);
                }
                Veriler.DokumanKategorileri.DeleteObject(DK);
                Veriler.SaveChanges();
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[KategoriAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanKategorileri as p where p.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(DokumanKategorileri DK)
        {
            TextBoxKategoriAdi.Text = DK.KategoriAdi;
            if (!string.IsNullOrEmpty(DK.GorselThumbnail))
            {
                ImageGorsel.Visible = true;
                ImageGorsel.ImageUrl = DK.GorselThumbnail;
                TextBoxGorsel.Text = DK.Gorsel;
            }
            TextBoxAciklama.Text = DK.Aciklama;
            CheckBoxDurum.Checked = DK.Durum.Value;
            HiddenFieldId.Value = DK.Id.ToString();
            LabelBaslik.Text = "Doküman Kategori Düzenle";
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
        }
    }
}
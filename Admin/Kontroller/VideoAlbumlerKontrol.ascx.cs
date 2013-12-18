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
    public partial class VideoAlbumlerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Video Albüm Yönetimi";
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
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p join VideoAlbumKategorileri as p1 on p.VideoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
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
            VideoAlbumKategorileriVer
                (DropDownListVideoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            VideoAlbumKategorileriVer
                (DropDownListVideoAlbumKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxVideoAlbumAdi.Text = string.Empty;
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

        private void VideoAlbumKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VAK = Veriler.VideoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            DropDownList.DataTextField = "VideoAlbumKategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = VAK;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Video Album Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                VideoAlbumler VA;
                string AnaDizin = "../Gorseller";
                string Kategori = "VideoAlbumler";
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
                    VA = Veriler.VideoAlbumler.Where(p => p.Id == Id).First();
                    VA.VideoAlbumKategoriId = Convert.ToInt32(DropDownListVideoAlbumKategorileri.SelectedValue);
                    VA.VideoAlbumAdi = TextBoxVideoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != VA.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(VA.Gorsel))
                            {
                                GorselSil(VA.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(VA.GorselThumbnail))
                            {
                                GorselSil(VA.GorselThumbnail);
                            }
                            VA.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            VA.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(VA.Gorsel))
                        {
                            GorselSil(VA.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(VA.GorselThumbnail))
                        {
                            GorselSil(VA.GorselThumbnail);
                        }
                        VA.Gorsel = null;
                        VA.GorselThumbnail = null;
                    }
                    VA.Aciklama = TextBoxAciklama.Text;
                    VA.Durum = CheckBoxDurum.Checked;
                    VA.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VA.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListVideoAlbumKategorileriGridView.SelectedValue = VA.VideoAlbumKategoriId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p where p.VideoAlbumKategoriId == " +
                        VA.VideoAlbumKategoriId + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    VA = new VideoAlbumler();
                    VA.VideoAlbumKategoriId = Convert.ToInt32(DropDownListVideoAlbumKategorileri.SelectedValue);
                    VA.VideoAlbumAdi = TextBoxVideoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        VA.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        VA.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        VA.Gorsel = null;
                        VA.GorselThumbnail = null;
                    }
                    VA.Aciklama = TextBoxAciklama.Text;
                    VA.Durum = CheckBoxDurum.Checked;
                    VA.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VA.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToVideoAlbumler(VA);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListVideoAlbumKategorileriGridView.SelectedValue
                        = VA.VideoAlbumKategoriId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p where p.VideoAlbumKategoriId == " +
                        VA.VideoAlbumKategoriId + "";
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
                VideoAlbumler VA = Veriler.VideoAlbumler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(VA);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                VideoAlbumler VA = Veriler.VideoAlbumler.Where(p => p.Id == Id).First();
                var VAVList = Veriler.VideoAlbumVideolari.Where(p => p.VideoAlbumId == VA.Id).ToList();
                foreach (VideoAlbumVideolari VAV in VAVList)
                {
                    VideoAlbumVideolari VAVTemp = Veriler.VideoAlbumVideolari.Where(p => p.Id == VAV.Id).First();
                    Veriler.VideoAlbumVideolari.DeleteObject(VAVTemp);
                    if (!string.IsNullOrEmpty(VAVTemp.Video))
                    {
                        GorselSil(VAVTemp.Video);
                    }
                    if (!string.IsNullOrEmpty(VAVTemp.Gorsel))
                    {
                        GorselSil(VA.GorselThumbnail);
                    }
                    Veriler.SaveChanges();
                }
                Veriler.VideoAlbumler.DeleteObject(VA);
                Veriler.SaveChanges();
                if (!string.IsNullOrEmpty(VA.Gorsel))
                {
                    GorselSil(VA.Gorsel);
                }
                if (!string.IsNullOrEmpty(VA.GorselThumbnail))
                {
                    GorselSil(VA.GorselThumbnail);
                }
                if (DropDownListVideoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p join VideoAlbumKategorileri as p1 on p.VideoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    int VideoAlbumKategoriId =
                        Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p where p.VideoAlbumKategoriId == " +
                        VideoAlbumKategoriId + "";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListVideoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p join VideoAlbumKategorileri as p1 on p.VideoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    int VideoAlbumKategoriId =
                        Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p where p.VideoAlbumKategoriId == " +
                        VideoAlbumKategoriId + "";
                }
            }
        }

        private void Guncelle(VideoAlbumler VA)
        {
            DropDownListVideoAlbumKategorileri.SelectedValue = VA.VideoAlbumKategoriId.Value.ToString();
            TextBoxVideoAlbumAdi.Text = VA.VideoAlbumAdi;
            TextBoxGorsel.Text = VA.Gorsel;
            TextBoxAciklama.Text = VA.Aciklama;
            CheckBoxDurum.Checked = VA.Durum.Value;
            HiddenFieldId.Value = VA.Id.ToString();
            LabelBaslik.Text = "Video Albüm Düzenle";
        }

        protected void DropDownListVideoAlbumKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListVideoAlbumKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p join VideoAlbumKategorileri as p1 on p.VideoAlbumKategoriId == p1.Id where p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
            }
            else
            {
                int VideoAlbumKategoriId =
                    Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[VideoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from VideoAlbumler as p where p.VideoAlbumKategoriId == " +
                    VideoAlbumKategoriId + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}
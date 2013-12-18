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
    public partial class VideoAlbumVideolariKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Albüm Görselleri Yönetimi";
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
                        "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumKategorileri as VAK join VideoAlbumler as VA on VAK.Id==VA.VideoAlbumKategoriId join VideoAlbumVideolari as VAV on VA.Id==VAV.VideoAlbumId where VAK.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and VAK.Durum==true and VA.Durum==true";
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
                (DropDownListVideoAlbumKategorileriGridView, new ListItem("Seçiniz", "0"));
            VideoAlbumleriVer
                (DropDownListVideoAlbumler, new ListItem("Seçiniz", "0"), 0);
            VideoAlbumleriVer
                (DropDownListVideoAlbumlerGridView, new ListItem("Tümü", "0"), 0);
            TextBoxVideoAdi.Text = string.Empty;
            ImageGorsel.Visible = false;
            TextBoxGorsel.Text = string.Empty;
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            LabelVideo.Text = string.Empty;
            TextBoxVideo.Text = string.Empty;
            ImageButtonVideo.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                             + TextBoxVideo.ClientID + "','','width=640,height=480');";
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

        private void VideoAlbumleriVer(DropDownList DropDownList, ListItem Item, int VideoAlbumKategoriId)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VA =
                Veriler.VideoAlbumler.Where(p => p.VideoAlbumKategoriId == VideoAlbumKategoriId && p.Durum == true).
                    ToList();
            DropDownList.DataTextField = "VideoAlbumAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = VA;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Video Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                VideoAlbumVideolari VAV;
                string AnaDizin = "../Gorseller";
                string Kategori = "VideoAlbumVideolari";
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
                string KayitYeriVideo = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                string KayitYeriGorsel = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Thumbnails + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    VAV = Veriler.VideoAlbumVideolari.Where
                        (p => p.Id == Id).First();
                    VAV.VideoAlbumId = Convert.ToInt32
                        (DropDownListVideoAlbumler.SelectedValue);
                    VAV.VideoAdi = TextBoxVideoAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != VAV.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(VAV.Gorsel))
                            {
                                GorselSil(VAV.Gorsel);
                            }
                            VAV.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeriGorsel, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(VAV.Gorsel))
                        {
                            GorselSil(VAV.Gorsel);
                        }
                        VAV.Gorsel = null;
                    }
                    if (TextBoxVideo.Text != string.Empty)
                    {
                        if (TextBoxVideo.Text != VAV.Video)
                        {
                            if (!string.IsNullOrEmpty(VAV.Video))
                            {
                                GorselSil(VAV.Video);
                            }
                            GorselSil(VAV.Video);
                            VAV.Video = VideoKaydet(TextBoxVideo, KayitYeriVideo);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(VAV.Video))
                        {
                            GorselSil(VAV.Video);
                        }
                        VAV.Video = null;
                    }
                    VAV.Aciklama = TextBoxAciklama.Text;
                    VAV.Durum = CheckBoxDurum.Checked;
                    VAV.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VAV.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListVideoAlbumKategorileriGridView.SelectedValue =
                        Veriler.VideoAlbumler.Where(p => p.Id == VAV.VideoAlbumId).First().VideoAlbumKategoriId.Value.
                            ToString();
                    VideoAlbumleriVer(DropDownListVideoAlbumlerGridView, new ListItem("Seçiniz", "0"),
                                      Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue));
                    DropDownListVideoAlbumlerGridView.SelectedValue
                        = VAV.VideoAlbumId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumVideolari as VAV where VAV.VideoAlbumId=="
                        + DropDownListVideoAlbumlerGridView.SelectedValue + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    VAV = new VideoAlbumVideolari();
                    VAV.VideoAlbumId = Convert.ToInt32(DropDownListVideoAlbumler.SelectedValue);
                    VAV.VideoAdi = TextBoxVideoAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        VAV.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeriGorsel, 175);
                    }
                    else
                    {
                        VAV.Gorsel = null;
                    }
                    if (TextBoxVideo.Text != string.Empty)
                    {
                        VAV.Video = VideoKaydet(TextBoxVideo, KayitYeriVideo);
                    }
                    else
                    {
                        VAV.Gorsel = null;
                    }
                    VAV.Aciklama = TextBoxAciklama.Text;
                    VAV.Durum = CheckBoxDurum.Checked;
                    VAV.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    VAV.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToVideoAlbumVideolari(VAV);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListVideoAlbumKategorileriGridView.SelectedValue
                        =
                        Veriler.VideoAlbumler.Where(p => p.Id == VAV.VideoAlbumId).First().VideoAlbumKategoriId.Value.
                            ToString();
                    VideoAlbumleriVer(DropDownListVideoAlbumlerGridView, new ListItem("Seçiniz", "0"),
                                      Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue));
                    DropDownListVideoAlbumlerGridView.SelectedValue
                        = VAV.VideoAlbumId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumVideolari as VAV where VAV.VideoAlbumId=="
                        + DropDownListVideoAlbumlerGridView.SelectedValue + "";
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

        public string VideoKaydet(TextBox Video, string KayitYeri)
        {
            string VideoAdi = Guid.NewGuid() + ".mp4";
            File.Copy(Server.MapPath(Video.Text.Replace("~", ".."))
                      , Server.MapPath(KayitYeri + VideoAdi));
            return KayitYeri.Replace("../", "~/") + VideoAdi;
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
                VideoAlbumVideolari VAV = Veriler.VideoAlbumVideolari.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(VAV);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                VideoAlbumVideolari VAV = Veriler.VideoAlbumVideolari.Where(p => p.Id == Id).First();
                Veriler.VideoAlbumVideolari.DeleteObject(VAV);
                Veriler.SaveChanges();
                GorselSil(VAV.Video);
                GorselSil(VAV.Gorsel);
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListVideoAlbumlerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumKategorileri as VAK join VideoAlbumler as VA on VAK.Id==VA.VideoAlbumKategoriId join VideoAlbumVideolari as VAV on VA.Id==VAV.VideoAlbumId where VAK.DilId=="
                        + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                        + " and VAK.Durum==true and VA.Durum==true";
                }
                else
                {
                    EntityDataSource1.CommandText =
                        "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumVideolari as VAV where VAV.VideoAlbumId=="
                        + DropDownListVideoAlbumlerGridView.SelectedValue + "";
                }
            }
        }

        private void Guncelle(VideoAlbumVideolari VAV)
        {
            DropDownListVideoAlbumKategorileri.SelectedValue =
                Veriler.VideoAlbumler.Where(p => p.Id == VAV.VideoAlbumId).First().VideoAlbumKategoriId.ToString();
            VideoAlbumleriVer(DropDownListVideoAlbumler, new ListItem("Seçiniz", "0"),
                              Convert.ToInt32(DropDownListVideoAlbumKategorileri.SelectedValue));
            DropDownListVideoAlbumler.SelectedValue = VAV.VideoAlbumId.Value.ToString();
            TextBoxVideoAdi.Text = VAV.VideoAdi;
            ImageGorsel.Visible = true;
            ImageGorsel.ImageUrl = VAV.Gorsel;
            TextBoxGorsel.Text = VAV.Gorsel;
            ImageGorsel.Visible = true;
            ImageGorsel.ImageUrl = VAV.Gorsel;
            LabelVideo.Text = VAV.Video;
            LiteralVideo.Text =
                "<script type=\"text/javascript\"> jwplayer(\"mediaplayer\").setup({flashplayer: \"Theme/VideoPlayer/player.swf\", file: \""
                + LabelVideo.Text.Replace("~/", "../../")
                + "\", image: \""
                + ImageGorsel.ImageUrl.Replace("~/", "../../")
                + "\"});</script>";
            TextBoxVideo.Text = VAV.Video;
            TextBoxAciklama.Text = VAV.Aciklama;
            CheckBoxDurum.Checked = VAV.Durum.Value;
            HiddenFieldId.Value = VAV.Id.ToString();
            LabelBaslik.Text = "Video Düzenle";
        }

        protected void DropDownListVideoAlbumKategorileri_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoAlbumleriVer(DropDownListVideoAlbumler, new ListItem("Seçiniz", "0"),
                              Convert.ToInt32(DropDownListVideoAlbumKategorileri.SelectedValue));
        }

        protected void DropDownListVideoAlbumKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoAlbumleriVer(DropDownListVideoAlbumlerGridView, new ListItem("Tümü", "0"),
                              Convert.ToInt32(DropDownListVideoAlbumKategorileriGridView.SelectedValue));
        }

        protected void DropDownListVideoAlbumlerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListVideoAlbumlerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumKategorileri as VAK join VideoAlbumler as VA on VAK.Id==VA.VideoAlbumKategoriId join VideoAlbumVideolari as VAV on VA.Id==VAV.VideoAlbumId where VAK.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString()
                    + " and VAK.Durum==true and VA.Durum==true";
            }
            else
            {
                EntityDataSource1.CommandText =
                    "select VAV.Id, VAV.VideoAdi, case when VAV.Gorsel != '' then VAV.Gorsel else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as Gorsel, VAV.Aciklama, VAV.Durum from VideoAlbumVideolari as VAV where VAV.VideoAlbumId=="
                    + DropDownListVideoAlbumlerGridView.SelectedValue + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}
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
    public partial class FotoAlbumlerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Foto Albüm Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 12))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
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
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileri, new ListItem("Seçiniz", "0"));
            FotoAlbumKategorileriVer(DropDownListFotoAlbumKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxFotoAlbumAdi.Text = string.Empty;
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

        private void FotoAlbumKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAK = Veriler.FotoAlbumKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            DropDownList.DataTextField = "FotoAlbumKategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = FAK;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Foto Album Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                FotoAlbumler FA;
                string AnaDizin = "../Gorseller";
                string Kategori = "FotoAlbumler";
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
                    FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                    FA.FotoAlbumKategoriId = Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    FA.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != FA.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(FA.Gorsel))
                            {
                                GorselSil(FA.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(FA.GorselThumbnail))
                            {
                                GorselSil(FA.GorselThumbnail);
                            }
                            FA.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            FA.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(FA.Gorsel))
                        {
                            GorselSil(FA.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(FA.GorselThumbnail))
                        {
                            GorselSil(FA.GorselThumbnail);
                        }
                        FA.Gorsel = null;
                        FA.GorselThumbnail = null;
                    }
                    FA.Aciklama = TextBoxAciklama.Text;
                    FA.Durum = CheckBoxDurum.Checked;
                    FA.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FA.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue = FA.FotoAlbumKategoriId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        FA.FotoAlbumKategoriId + "";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    FA = new FotoAlbumler();
                    FA.FotoAlbumKategoriId = Convert.ToInt32(DropDownListFotoAlbumKategorileri.SelectedValue);
                    FA.FotoAlbumAdi = TextBoxFotoAlbumAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        FA.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        FA.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        FA.Gorsel = null;
                        FA.GorselThumbnail = null;
                    }
                    FA.Aciklama = TextBoxAciklama.Text;
                    FA.Durum = CheckBoxDurum.Checked;
                    FA.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    FA.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToFotoAlbumler(FA);
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListFotoAlbumKategorileriGridView.SelectedValue = FA.FotoAlbumKategoriId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        FA.FotoAlbumKategoriId + "";
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
                FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(FA);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
                var FAGList = Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == FA.Id).ToList();
                foreach (FotoAlbumGorselleri FAG in FAGList)
                {
                    FotoAlbumGorselleri FAGSil = Veriler.FotoAlbumGorselleri.Where(p => p.Id == FAG.Id).First();
                    if (!string.IsNullOrEmpty(FAGSil.Gorsel))
                    {
                        GorselSil(FAGSil.Gorsel);
                    }
                    if (!string.IsNullOrEmpty(FAGSil.GorselThumbnail))
                    {
                        GorselSil(FAGSil.GorselThumbnail);
                    }
                    Veriler.FotoAlbumGorselleri.DeleteObject(FAGSil);
                    Veriler.SaveChanges();
                }
                if (!string.IsNullOrEmpty(FA.Gorsel))
                {
                    GorselSil(FA.Gorsel);
                }
                if (!string.IsNullOrEmpty(FA.GorselThumbnail))
                {
                    GorselSil(FA.GorselThumbnail);
                }
                Veriler.FotoAlbumler.DeleteObject(FA);
                Veriler.SaveChanges();
                if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    int FotoAlbumKategoriId =
                        Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        FotoAlbumKategoriId + "";
                }
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                }
                else
                {
                    int FotoAlbumKategoriId =
                        Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                        FotoAlbumKategoriId + "";
                }
            }
        }

        private void Guncelle(FotoAlbumler FA)
        {
            DropDownListFotoAlbumKategorileri.SelectedValue = FA.FotoAlbumKategoriId.Value.ToString();
            TextBoxFotoAlbumAdi.Text = FA.FotoAlbumAdi;
            TextBoxGorsel.Text = FA.Gorsel;
            TextBoxAciklama.Text = FA.Aciklama;
            CheckBoxDurum.Checked = FA.Durum.Value;
            HiddenFieldId.Value = FA.Id.ToString();
            LabelBaslik.Text = "Foto Albüm Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListFotoAlbumKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p join FotoAlbumKategorileri as p1 on p.FotoAlbumKategoriId == p1.Id where p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
            }
            else
            {
                int FotoAlbumKategoriId =
                    Convert.ToInt32(DropDownListFotoAlbumKategorileriGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[FotoAlbumAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from FotoAlbumler as p where p.FotoAlbumKategoriId == " +
                    FotoAlbumKategoriId + "";
            }
            GridViewVeriler.DataBind();
        }
    }
}
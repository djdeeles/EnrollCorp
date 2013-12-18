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
    public partial class YayinlarKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Yayınlar Yönetimi";
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
                    BilgileriVer();
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
            YayinKategorileriVer(DropDownListYayinKategorileri, new ListItem("Seçiniz", "0"));
            YayinKategorileriVer(DropDownListYayinKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxYayinAdi.Text = string.Empty;
            ImageGorsel.Visible = false;
            TextBoxGorsel.Text = string.Empty;
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            TextBoxUrl.Text = string.Empty;
            RadDateTimePickerTarih.SelectedDate = DateTime.Now;
            TextBoxAciklama.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void YayinKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YKList = Veriler.YayinKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            DropDownList.DataTextField = "KategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = YKList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Yayın Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Yayinlar Y;
                string AnaDizin = "../Gorseller";
                string Kategori = "Yayinlar";
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
                    Y = Veriler.Yayinlar.Where(p => p.Id == Id).First();
                    Y.YayinKategoriId = Convert.ToInt32(DropDownListYayinKategorileri.SelectedValue);
                    Y.YayinAdi = TextBoxYayinAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != Y.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(Y.Gorsel))
                            {
                                GorselSil(Y.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(Y.GorselThumbnail))
                            {
                                GorselSil(Y.GorselThumbnail);
                            }
                            Y.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                            Y.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Y.Gorsel))
                        {
                            GorselSil(Y.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(Y.GorselThumbnail))
                        {
                            GorselSil(Y.GorselThumbnail);
                        }
                        Y.Gorsel = null;
                        Y.GorselThumbnail = null;
                    }
                    Y.Url = TextBoxUrl.Text;
                    Y.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    Y.Aciklama = TextBoxAciklama.Text;
                    Y.Durum = CheckBoxDurum.Checked;
                    Y.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Y.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    Y = new Yayinlar();
                    Y.YayinKategoriId = Convert.ToInt32(DropDownListYayinKategorileri.SelectedValue);
                    Y.YayinAdi = TextBoxYayinAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        Y.Gorsel = GorselBoyutlandir(TextBoxGorsel, KayitYeri, 700);
                        Y.GorselThumbnail = GorselBoyutlandir(TextBoxGorsel, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        Y.Gorsel = null;
                        Y.GorselThumbnail = null;
                    }
                    Y.Url = TextBoxUrl.Text;
                    Y.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    Y.Aciklama = TextBoxAciklama.Text;
                    Y.Durum = CheckBoxDurum.Checked;
                    Y.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Y.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToYayinlar(Y);
                    Veriler.SaveChanges();
                    Temizle();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                BilgileriVer();
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
                Yayinlar Y = Veriler.Yayinlar.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(Y);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                Yayinlar Y = Veriler.Yayinlar.Where(p => p.Id == Id).First();
                if (!string.IsNullOrEmpty(Y.Gorsel))
                {
                    GorselSil(Y.Gorsel);
                }
                if (!string.IsNullOrEmpty(Y.Gorsel))
                {
                    GorselSil(Y.GorselThumbnail);
                }
                Veriler.Yayinlar.DeleteObject(Y);
                Veriler.SaveChanges();
                BilgileriVer();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(Yayinlar Y)
        {
            YayinKategorileriVer(DropDownListYayinKategorileri, new ListItem("Seçiniz", "0"));
            DropDownListYayinKategorileri.SelectedValue = Y.YayinKategoriId.ToString();
            TextBoxYayinAdi.Text = Y.YayinAdi;
            if (!string.IsNullOrEmpty(Y.GorselThumbnail))
            {
                ImageGorsel.Visible = true;
                ImageGorsel.ImageUrl = Y.GorselThumbnail;
                TextBoxGorsel.Text = Y.Gorsel;
            }
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            TextBoxUrl.Text = Y.Url;
            RadDateTimePickerTarih.SelectedDate = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
            TextBoxAciklama.Text = Y.Aciklama;
            CheckBoxDurum.Checked = Y.Durum.Value;
            HiddenFieldId.Value = Y.Id.ToString();
            LabelBaslik.Text = "Yayın Düzenle";
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
        }

        protected void DropDownListVideoAlbumKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            BilgileriVer();
        }

        private void BilgileriVer()
        {
            if (DropDownListYayinKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[YayinKategoriId], p.[YayinAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from Yayinlar as p join YayinKategorileri as p1 on p.YayinKategoriId == p1.Id where p1.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                GridViewVeriler.DataBind();
            }
            else
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[YayinKategoriId], p.[YayinAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/novideo.jpg' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from Yayinlar as p where p.YayinKategoriId=="
                    + DropDownListYayinKategorileriGridView.SelectedValue + "";
                GridViewVeriler.DataBind();
            }
        }
    }
}
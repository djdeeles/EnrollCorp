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
    public partial class IhalelerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "İhaleler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 10))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                    KategorileriVer(DropDownListKategorilerGridView, new ListItem("Tümü", "0"));
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
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
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            TextBoxAd.Text = string.Empty;
            TextBoxOzet.Text = string.Empty;
            RadEditorDetay.Content = string.Empty;
            RadDateTimePickerBaslangicTarihi.SelectedDate = DateTime.Now;
            RadDateTimePickerBitisTarihi.SelectedDate = null;
            ImageResim1.Visible = false;
            TextBoxResim1.Text = string.Empty;
            ImageButtonResim1.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim1.ClientID + "','','width=640,height=480');";
            ImageResim2.Visible = false;
            TextBoxResim2.Text = string.Empty;
            ImageButtonResim2.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim2.ClientID + "','','width=640,height=480');";
            ImageResim3.Visible = false;
            TextBoxResim3.Text = string.Empty;
            ImageButtonResim3.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim3.ClientID + "','','width=640,height=480');";
            ImageResim4.Visible = false;
            TextBoxResim4.Text = string.Empty;
            ImageButtonResim4.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxResim4.ClientID + "','','width=640,height=480');";
            CheckBoxDurum.Checked = false;
            TextBoxAnahtarKelimeler.Text = string.Empty;
            HiddenFieldId.Value = string.Empty;
        }

        private void KategorileriVer(DropDownList DropDownListKategoriler, ListItem ListItem)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IK =
                Veriler.IhaleKategorileri.Where(p => p.DilId == DilId && p.Durum == true).OrderBy(p => p.SiraNo).ToList();
            DropDownListKategoriler.DataTextField = "KategoriAdi";
            DropDownListKategoriler.DataValueField = "Id";
            DropDownListKategoriler.DataSource = IK;
            DropDownListKategoriler.DataBind();
            DropDownListKategoriler.Items.Insert(0, ListItem);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "İhale Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Ihaleler I;
                string AnaDizin = "../Gorseller";
                string Kategori = "Ihaleler";
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
                    I = Veriler.Ihaleler.Where(p => p.Id == Id).First();
                    I.IhaleKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    I.Ad = TextBoxAd.Text;
                    I.Ozet = TextBoxOzet.Text;
                    I.Icerik = RadEditorDetay.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        I.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        I.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BitisTarihi = null;
                    }
                    if (TextBoxResim1.Text != string.Empty)
                    {
                        if (TextBoxResim1.Text != I.Gorsel1)
                        {
                            GorselSil(I.Gorsel1);
                            GorselSil(I.GorselThumbnail1);
                            I.Gorsel1 = GorselBoyutlandir(TextBoxResim1, KayitYeri, 700);
                            I.GorselThumbnail1 = GorselBoyutlandir(TextBoxResim1, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel1);
                        GorselSil(I.GorselThumbnail1);
                        I.Gorsel1 = null;
                        I.GorselThumbnail1 = null;
                    }

                    if (TextBoxResim2.Text != string.Empty)
                    {
                        if (TextBoxResim2.Text != I.Gorsel2)
                        {
                            GorselSil(I.Gorsel2);
                            GorselSil(I.GorselThumbnail2);
                            I.Gorsel2 = GorselBoyutlandir(TextBoxResim2, KayitYeri, 700);
                            I.GorselThumbnail2 = GorselBoyutlandir(TextBoxResim2, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel2);
                        GorselSil(I.GorselThumbnail2);
                        I.Gorsel2 = null;
                        I.GorselThumbnail2 = null;
                    }

                    if (TextBoxResim3.Text != string.Empty)
                    {
                        if (TextBoxResim3.Text != I.Gorsel3)
                        {
                            GorselSil(I.Gorsel3);
                            GorselSil(I.GorselThumbnail3);
                            I.Gorsel3 = GorselBoyutlandir(TextBoxResim3, KayitYeri, 700);
                            I.GorselThumbnail3 = GorselBoyutlandir(TextBoxResim3, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel3);
                        GorselSil(I.GorselThumbnail3);
                        I.Gorsel3 = null;
                        I.GorselThumbnail3 = null;
                    }

                    if (TextBoxResim4.Text != string.Empty)
                    {
                        if (TextBoxResim4.Text != I.Gorsel4)
                        {
                            GorselSil(I.Gorsel4);
                            GorselSil(I.GorselThumbnail4);
                            I.Gorsel4 = GorselBoyutlandir(TextBoxResim4, KayitYeri, 700);
                            I.GorselThumbnail4 = GorselBoyutlandir(TextBoxResim4, KayitYeriThumbnail, 175);
                        }
                    }
                    else
                    {
                        GorselSil(I.Gorsel4);
                        GorselSil(I.GorselThumbnail4);
                        I.Gorsel4 = null;
                        I.GorselThumbnail4 = null;
                    }
                    I.Durum = CheckBoxDurum.Checked;
                    I.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    I.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    DropDownListKategorilerGridView.SelectedValue = I.IhaleKategoriId.Value.ToString();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    I = new Ihaleler();
                    I.IhaleKategoriId = Convert.ToInt32(DropDownListKategoriler.SelectedValue);
                    I.Ad = TextBoxAd.Text;
                    I.Ozet = TextBoxOzet.Text;
                    I.Icerik = RadEditorDetay.Content;
                    if (RadDateTimePickerBaslangicTarihi.SelectedDate != null)
                    {
                        I.BaslangicTarihi = Convert.ToDateTime(RadDateTimePickerBaslangicTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BaslangicTarihi = null;
                    }
                    if (RadDateTimePickerBitisTarihi.SelectedDate != null)
                    {
                        I.BitisTarihi = Convert.ToDateTime(RadDateTimePickerBitisTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        I.BitisTarihi = null;
                    }
                    I.Durum = CheckBoxDurum.Checked;
                    if (TextBoxResim1.Text != string.Empty)
                    {
                        I.Gorsel1 = GorselBoyutlandir(TextBoxResim1, KayitYeri, 700);
                        I.GorselThumbnail1 = GorselBoyutlandir(TextBoxResim1, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel1 = null;
                        I.GorselThumbnail1 = null;
                    }

                    if (TextBoxResim2.Text != string.Empty)
                    {
                        I.Gorsel2 = GorselBoyutlandir(TextBoxResim2, KayitYeri, 700);
                        I.GorselThumbnail2 = GorselBoyutlandir(TextBoxResim2, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel2 = null;
                        I.GorselThumbnail2 = null;
                    }

                    if (TextBoxResim3.Text != string.Empty)
                    {
                        I.Gorsel3 = GorselBoyutlandir(TextBoxResim3, KayitYeri, 700);
                        I.GorselThumbnail3 = GorselBoyutlandir(TextBoxResim3, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel3 = null;
                        I.GorselThumbnail3 = null;
                    }

                    if (TextBoxResim4.Text != string.Empty)
                    {
                        I.Gorsel4 = GorselBoyutlandir(TextBoxResim4, KayitYeri, 700);
                        I.GorselThumbnail4 = GorselBoyutlandir(TextBoxResim4, KayitYeriThumbnail, 175);
                    }
                    else
                    {
                        I.Gorsel4 = null;
                        I.GorselThumbnail4 = null;
                    }
                    I.AnahtarKelimeler = TextBoxAnahtarKelimeler.Text;
                    I.OkunmaSayisi = 0;
                    I.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    I.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToIhaleler(I);
                    Veriler.SaveChanges();
                    Temizle();
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
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
        }

        protected void GridViewVeriler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Ihaleler I = Veriler.Ihaleler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(I);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Ihaleler I = Veriler.Ihaleler.Where(p => p.Id == Id).First();
                Veriler.Ihaleler.DeleteObject(I);
                Veriler.SaveChanges();
                GorselSil(I.Gorsel1);
                GorselSil(I.GorselThumbnail1);
                GorselSil(I.Gorsel2);
                GorselSil(I.GorselThumbnail2);
                GorselSil(I.Gorsel3);
                GorselSil(I.GorselThumbnail3);
                GorselSil(I.Gorsel4);
                GorselSil(I.GorselThumbnail4);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
            else if (e.CommandName == "Sort")
            {
                if (DropDownListKategorilerGridView.SelectedIndex == 0)
                {
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                        EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
                }
                else
                {
                    int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                    EntityDataSource1.CommandText =
                        "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.Id==" +
                        KategoriId.ToString() + " order by BaslangicTarihi desc";
                }
            }
        }

        private void Guncelle(Ihaleler I)
        {
            KategorileriVer(DropDownListKategoriler, new ListItem("Seçiniz", "0"));
            DropDownListKategoriler.SelectedValue = I.IhaleKategoriId.Value.ToString();
            TextBoxAd.Text = I.Ad;
            TextBoxOzet.Text = I.Ozet;
            RadEditorDetay.Content = I.Icerik;
            if (I.BaslangicTarihi != null)
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = I.BaslangicTarihi.Value;
            }
            else
            {
                RadDateTimePickerBaslangicTarihi.SelectedDate = null;
            }
            if (I.BitisTarihi != null)
            {
                RadDateTimePickerBitisTarihi.SelectedDate = I.BitisTarihi.Value;
            }
            else
            {
                RadDateTimePickerBitisTarihi.SelectedDate = null;
            }
            if (I.GorselThumbnail1 != null)
            {
                ImageResim1.Visible = true;
                ImageResim1.ImageUrl = I.GorselThumbnail1;
                TextBoxResim1.Text = I.Gorsel1;
            }
            if (I.GorselThumbnail2 != null)
            {
                ImageResim2.Visible = true;
                ImageResim2.ImageUrl = I.GorselThumbnail2;
                TextBoxResim2.Text = I.Gorsel2;
            }
            if (I.GorselThumbnail3 != null)
            {
                ImageResim3.Visible = true;
                ImageResim3.ImageUrl = I.GorselThumbnail3;
                TextBoxResim3.Text = I.Gorsel3;
            }
            if (I.GorselThumbnail4 != null)
            {
                ImageResim4.Visible = true;
                ImageResim4.ImageUrl = I.GorselThumbnail4;
                TextBoxResim4.Text = I.Gorsel4;
            }
            CheckBoxDurum.Checked = I.Durum.Value;
            TextBoxAnahtarKelimeler.Text = I.AnahtarKelimeler;
            HiddenFieldId.Value = I.Id.ToString();
            LabelBaslik.Text = "İhale Düzenle";
        }

        protected void DropDownListKategorilerGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListKategorilerGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.DilId==" +
                    EnrollContext.Current.WorkingLanguage.languageId.ToString() + " order by BaslangicTarihi desc";
            }
            else
            {
                int KategoriId = Convert.ToInt32(DropDownListKategorilerGridView.SelectedValue);
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[Ad], p.[BaslangicTarihi], p.[BitisTarihi], p.[Durum] from Ihaleler as p join IhaleKategorileri as p1 on p.IhaleKategoriId==p1.Id where p1.[Durum]==true and p1.Id==" +
                    KategoriId.ToString() + " order by BaslangicTarihi desc";
            }
            GridViewVeriler.DataBind();
        }
    }
}
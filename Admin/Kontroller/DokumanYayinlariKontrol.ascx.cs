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
    public partial class DokumanYayinlariKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Doküman Yönetimi";
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
            DokumanKategorileriVer(DropDownListDokumanKategorileri, new ListItem("Seçiniz", "0"));
            DokumanKategorileriVer(DropDownListDokumanKategorileriGridView, new ListItem("Tümü", "0"));
            TextBoxDokumanAdi.Text = string.Empty;
            ImageGorsel.Visible = false;
            ImageGorsel.ImageUrl = string.Empty;
            TextBoxGorsel.Text = string.Empty;
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            HyperLinkDokuman.Visible = false;
            HyperLinkDokuman.NavigateUrl = string.Empty;
            TextBoxDokuman.Text = string.Empty;
            ImageButtonDokuman.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxDokuman.ClientID + "','','width=640,height=480');";
            TextBoxAciklama.Text = string.Empty;
            RadDateTimePickerTarih.SelectedDate = DateTime.Now;
            CheckBoxDurum.Checked = false;
            HiddenFieldId.Value = string.Empty;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
            RequiredFieldValidator4.Visible = true;
        }

        private void DokumanKategorileriVer(DropDownList DropDownList, ListItem Item)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DKList = Veriler.DokumanKategorileri.Where(p => p.DilId == DilId && p.Durum == true).ToList();
            DropDownList.DataTextField = "KategoriAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = DKList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, Item);
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Doküman Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DokumanYayinlari DY;
                string AnaDizin = "../Gorseller";
                string Kategori = "DokumanYayinlari";
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
                string Gorsel = "Gorsel";
                string GorselThumbnail = "GorselThumbnail";
                string Dokumanlar = "Dokumanlar";
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
                if (!Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Gorsel)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Gorsel));
                }
                if (
                    !Directory.Exists(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + GorselThumbnail)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + GorselThumbnail));
                }
                if (
                    !Directory.Exists(Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Dokumanlar)))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + Dokumanlar));
                }
                string KayitYeriGorsel =
                    AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + "Gorsel" + "/";
                ;
                string KayitYeriGorselThumbnail =
                    AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + "GorselThumbnail" + "/";
                string KayitYeriDokuman =
                    AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/" + "Dokumanlar" + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    DY = Veriler.DokumanYayinlari.Where(p => p.Id == Id).First();
                    DY.DokumanKategoriId = Convert.ToInt32(DropDownListDokumanKategorileri.SelectedValue);
                    DY.DokumanAdi = TextBoxDokumanAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != DY.Gorsel)
                        {
                            if (!string.IsNullOrEmpty(DY.Gorsel))
                            {
                                GorselSil(DY.Gorsel);
                            }
                            if (!string.IsNullOrEmpty(DY.GorselThumbnail))
                            {
                                GorselSil(DY.GorselThumbnail);
                            }
                            DY.Gorsel =
                                GorselBoyutlandir(TextBoxGorsel, KayitYeriGorsel, 700);
                            DY.GorselThumbnail =
                                GorselBoyutlandir(TextBoxGorsel, KayitYeriGorselThumbnail, 175);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(DY.Gorsel))
                        {
                            GorselSil(DY.Gorsel);
                        }
                        if (!string.IsNullOrEmpty(DY.GorselThumbnail))
                        {
                            GorselSil(DY.GorselThumbnail);
                        }
                        DY.Gorsel = null;
                        DY.GorselThumbnail = null;
                    }
                    if (TextBoxDokuman.Text != string.Empty)
                    {
                        if (TextBoxDokuman.Text != DY.Dokuman)
                        {
                            if (!string.IsNullOrEmpty(DY.Dokuman))
                            {
                                GorselSil(DY.Dokuman);
                            }
                            DY.Dokuman = DokumanKaydet(TextBoxDokuman, KayitYeriDokuman);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(DY.Dokuman))
                        {
                            GorselSil(DY.Dokuman);
                        }
                        DY.Dokuman = null;
                    }
                    DY.Aciklama = TextBoxAciklama.Text;
                    DY.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    DY.Durum = CheckBoxDurum.Checked;
                    DY.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DY.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    DY = new DokumanYayinlari();
                    DY.DokumanKategoriId = Convert.ToInt32(DropDownListDokumanKategorileri.SelectedValue);
                    DY.DokumanAdi = TextBoxDokumanAdi.Text;
                    if (TextBoxGorsel.Text != string.Empty)
                    {
                        if (TextBoxGorsel.Text != DY.Gorsel)
                        {
                            DY.Gorsel =
                                GorselBoyutlandir(TextBoxGorsel, KayitYeriGorsel, 700);
                            DY.GorselThumbnail =
                                GorselBoyutlandir(TextBoxGorsel, KayitYeriGorselThumbnail, 175);
                        }
                    }
                    else
                    {
                        DY.Gorsel = null;
                        DY.GorselThumbnail = null;
                    }
                    if (TextBoxDokuman.Text != string.Empty)
                    {
                        DY.Dokuman = DokumanKaydet(TextBoxDokuman, KayitYeriDokuman);
                    }
                    else
                    {
                        DY.Dokuman = null;
                    }
                    DY.Aciklama = TextBoxAciklama.Text;
                    DY.Tarih = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
                    DY.Durum = CheckBoxDurum.Checked;
                    DY.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    DY.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToDokumanYayinlari(DY);
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

        public string DokumanKaydet(TextBox Dokuman, string KayitYeri)
        {
            string DokumanAdi = Guid.NewGuid() + ".swf";
            File.Copy(Server.MapPath(Dokuman.Text.Replace("~", ".."))
                      , Server.MapPath(KayitYeri + DokumanAdi));
            return KayitYeri.Replace("../", "~/") + DokumanAdi;
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
                DokumanYayinlari DY = Veriler.DokumanYayinlari.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(DY);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                DokumanYayinlari DY = Veriler.DokumanYayinlari.Where(p => p.Id == Id).First();
                if (!string.IsNullOrEmpty(DY.Gorsel))
                {
                    GorselSil(DY.Gorsel);
                }
                if (!string.IsNullOrEmpty(DY.GorselThumbnail))
                {
                    GorselSil(DY.GorselThumbnail);
                }
                if (!string.IsNullOrEmpty(DY.GorselThumbnail))
                {
                    GorselSil(DY.Dokuman);
                }
                Veriler.DokumanYayinlari.DeleteObject(DY);
                Veriler.SaveChanges();
                BilgileriVer();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(DokumanYayinlari DY)
        {
            DokumanKategorileriVer(DropDownListDokumanKategorileri, new ListItem("Seçiniz", "0"));
            DropDownListDokumanKategorileri.SelectedValue = DY.DokumanKategoriId.ToString();
            TextBoxDokumanAdi.Text = DY.DokumanAdi;
            if (!string.IsNullOrEmpty(DY.GorselThumbnail))
            {
                ImageGorsel.Visible = true;
                ImageGorsel.ImageUrl = DY.GorselThumbnail;
                TextBoxGorsel.Text = DY.Gorsel;
            }
            ImageButtonGorsel.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                              + TextBoxGorsel.ClientID + "','','width=640,height=480');";
            if (!string.IsNullOrEmpty(DY.Dokuman))
            {
                HyperLinkDokuman.Visible = true;
                HyperLinkDokuman.NavigateUrl = DY.Dokuman;
                TextBoxDokuman.Text = DY.Dokuman;
            }
            ImageButtonDokuman.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                               + TextBoxDokuman.ClientID + "','','width=640,height=480');";
            TextBoxAciklama.Text = DY.Aciklama;
            RadDateTimePickerTarih.SelectedDate = Convert.ToDateTime(RadDateTimePickerTarih.SelectedDate.Value);
            CheckBoxDurum.Checked = DY.Durum.Value;
            HiddenFieldId.Value = DY.Id.ToString();
            LabelBaslik.Text = "Yayın Düzenle";
            RequiredFieldValidator4.Visible = false;
        }

        protected void DropDownListDokumanKategorileriGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            BilgileriVer();
        }

        private void BilgileriVer()
        {
            if (DropDownListDokumanKategorileriGridView.SelectedIndex == 0)
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[DokumanKategoriId], p.[DokumanAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanYayinlari as p join DokumanKategorileri as p1 on p.DokumanKategoriId == p1.Id where p1.DilId=="
                    + EnrollContext.Current.WorkingLanguage.languageId.ToString() + "";
                GridViewVeriler.DataBind();
            }
            else
            {
                EntityDataSource1.CommandText =
                    "select p.[Id], p.[DokumanKategoriId], p.[DokumanAdi], case when p.[GorselThumbnail] != '' then p.[GorselThumbnail] else '~/App_Themes/PendikMainTheme/Images/noimage.png' end as [GorselThumbnail], p.[Aciklama], p.[Durum] from DokumanYayinlari as p where p.DokumanKategoriId=="
                    + DropDownListDokumanKategorileriGridView.SelectedValue + "";
                GridViewVeriler.DataBind();
            }
        }
    }
}
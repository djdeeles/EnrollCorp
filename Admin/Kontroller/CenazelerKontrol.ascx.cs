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
    public partial class CenazelerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Cenazeler Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 11))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void CinsiyetleriVer(DropDownList DropDownList)
        {
            var CList = Veriler.Cinsiyetler.ToList();
            DropDownList.DataTextField = "Cinsiyet";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = CList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void UlkeleriVer(DropDownList DropDownList)
        {
            var UList = Veriler.Ulkeler.ToList();
            DropDownList.DataTextField = "UlkeAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = UList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void IlleriVer(DropDownList DropDownList, int UlkeId)
        {
            var IList = Veriler.Iller.Where(p => p.UlkeId == UlkeId).ToList();
            DropDownList.DataTextField = "IlAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = IList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void IlceleriVer(DropDownList DropDownList, int IlId)
        {
            var IList = Veriler.Ilceler.Where(p => p.IlId == IlId).ToList();
            DropDownList.DataTextField = "IlceAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = IList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void MahalleleriVer(DropDownList DropDownList, int IlceId)
        {
            var MList = Veriler.Mahalleler.Where(p => p.IlceId == IlceId).ToList();
            DropDownList.DataTextField = "MahalleAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = MList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void VefatNedenleriVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            var VNList = Veriler.VefatNedenleri.ToList();
            DropDownList.DataTextField = "VefatNedeni";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = VNList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void CenazelerDefinZamanlariVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            var CDZList = Veriler.CenazelerDefinZamanlari.ToList();
            DropDownList.DataTextField = "DefinZamani";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = CDZList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void MeslekleriVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            var MList = Veriler.Meslekler.ToList();
            DropDownList.DataTextField = "MeslekAdi";
            DropDownList.DataValueField = "Id";
            DropDownList.DataSource = MList;
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void YemekVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            DropDownList.Items.Add(new ListItem("Evet", "True"));
            DropDownList.Items.Add(new ListItem("Hayır", "False"));
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void OtobusVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            DropDownList.Items.Add(new ListItem("Evet", "True"));
            DropDownList.Items.Add(new ListItem("Hayır", "False"));
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void NamazaIstirakVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            DropDownList.Items.Add(new ListItem("Evet", "True"));
            DropDownList.Items.Add(new ListItem("Hayır", "False"));
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void EvdeTaziyeVer(DropDownList DropDownList)
        {
            DropDownList.DataSource = string.Empty;
            DropDownList.DataBind();
            DropDownList.Items.Add(new ListItem("Evet", "True"));
            DropDownList.Items.Add(new ListItem("Hayır", "False"));
            DropDownList.Items.Insert(0, new ListItem("Seçiniz", "0"));
        }

        private void Temizle()
        {
            TextBoxAd.Text = string.Empty;
            TextBoxSoyad.Text = string.Empty;
            CinsiyetleriVer(DropDownListCinsiyetler);
            TextBoxResim.Text = string.Empty;
            UlkeleriVer(DropDownListDogumYeriUlkeler);
            IlleriVer(DropDownListDogumYeriIller, 0);
            UlkeleriVer(DropDownListYasadigiYerUlkeler);
            IlleriVer(DropDownListYasadigiYerIller, 0);
            IlceleriVer(DropDownListYasadigiYerIlceler, 0);
            MahalleleriVer(DropDownListYasadigiYerMahalleler, 0);
            TextBoxYas.Text = string.Empty;
            TextBoxYakiniAd.Text = string.Empty;
            TextBoxYakiniSoyad.Text = string.Empty;
            TextBoxYakiniTelefon.Text = string.Empty;
            RadDateTimePickerVefatTarihi.SelectedDate = DateTime.Now;
            VefatNedenleriVer(DropDownListVefatNedenleri);
            UlkeleriVer(DropDownListDefinYeriUlkeler);
            IlleriVer(DropDownListDefinYeriIller, 0);
            CenazelerDefinZamanlariVer(DropDownListDefinZamanlari);
            RadDateTimePickerDefinTarihi.SelectedDate = DateTime.Now;
            MeslekleriVer(DropDownListMeslekler);
            TextBoxAdres.Text = string.Empty;
            YemekVer(DropDownListYemek);
            OtobusVer(DropDownListOtobus);
            NamazaIstirakVer(DropDownListNamazaIstirak);
            EvdeTaziyeVer(DropDownListEvdeTaziye);
            TextBoxDigerHizmetler.Text = string.Empty;
            TextBoxAciklama.Text = string.Empty;
            ImageButtonResim.OnClientClick = "window.open('FileManager.aspx?ReturnField="
                                             + TextBoxResim.ClientID + "','','width=640,height=480');";
            ImageResim.Visible = false;
            HiddenFieldId.Value = string.Empty;
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            LabelBaslik.Text = "Cenaze Ekle";
        }

        protected void DropDownListDogumYeriUlkeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListDogumYeriIller, Convert.ToInt32(DropDownListDogumYeriUlkeler.SelectedValue));
        }

        protected void DropDownListYasadigiYerUlkeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListYasadigiYerIller, Convert.ToInt32(DropDownListYasadigiYerUlkeler.SelectedValue));
        }

        protected void DropDownListYasadigiYerIller_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlceleriVer(DropDownListYasadigiYerIlceler, Convert.ToInt32(DropDownListYasadigiYerIller.SelectedValue));
        }

        protected void DropDownListYasadigiYerIlceler_SelectedIndexChanged(object sender, EventArgs e)
        {
            MahalleleriVer(DropDownListYasadigiYerMahalleler,
                           Convert.ToInt32(DropDownListYasadigiYerIlceler.SelectedValue));
        }

        protected void DropDownListDefinYeriUlkeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            IlleriVer(DropDownListDefinYeriIller, Convert.ToInt32(DropDownListDefinYeriUlkeler.SelectedValue));
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Cenazeler C;
                string AnaDizin = "../Gorseller";
                string Kategori = "Cenazeler";
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
                string KayitYeri = AnaDizin + "/" + Kategori + "/" + Yil + "/" + Ay + "/";
                if (HiddenFieldId.Value != string.Empty)
                {
                    int Id = Convert.ToInt32(HiddenFieldId.Value);
                    C = Veriler.Cenazeler.Where(p => p.Id == Id).First();
                    C.Ad = TextBoxAd.Text;
                    C.Soyad = TextBoxSoyad.Text;
                    C.CinsiyetId = Convert.ToInt32(DropDownListCinsiyetler.SelectedValue);
                    if (TextBoxResim.Text != string.Empty)
                    {
                        if (TextBoxResim.Text != C.Resim)
                        {
                            GorselSil(C.Resim);
                            C.Resim = GorselBoyutlandir(TextBoxResim, KayitYeri, 175);
                        }
                    }
                    else
                    {
                        GorselSil(C.Resim);
                        C.Resim = null;
                    }
                    C.DogumYeriIlId = Convert.ToInt32(DropDownListDogumYeriIller.SelectedValue);
                    C.YasadigiYerMahalleId = Convert.ToInt32(DropDownListYasadigiYerMahalleler.SelectedValue);
                    C.Yas = TextBoxYas.Text;
                    C.YakiniAd = TextBoxYakiniAd.Text;
                    C.YakiniSoyad = TextBoxYakiniSoyad.Text;
                    C.YakiniTel = TextBoxYakiniTelefon.Text;
                    if (RadDateTimePickerVefatTarihi.SelectedDate != null)
                    {
                        C.VefatTarihi = Convert.ToDateTime(RadDateTimePickerVefatTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        C.VefatTarihi = null;
                    }
                    C.VefatNedeniId = Convert.ToInt32(DropDownListVefatNedenleri.SelectedValue);
                    C.DefinYeriIlId = Convert.ToInt32(DropDownListDefinYeriIller.SelectedValue);
                    C.DefinZamaniId = Convert.ToInt32(DropDownListDefinZamanlari.SelectedValue);
                    if (RadDateTimePickerDefinTarihi.SelectedDate != null)
                    {
                        C.DefinTarihi = Convert.ToDateTime(RadDateTimePickerDefinTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        C.DefinTarihi = null;
                    }
                    C.MeslekId = Convert.ToInt32(DropDownListMeslekler.SelectedValue);
                    C.Adres = TextBoxAdres.Text;
                    C.Yemek = Convert.ToBoolean(DropDownListYemek.SelectedValue);
                    C.Otobus = Convert.ToBoolean(DropDownListOtobus.SelectedValue);
                    C.NamazaIstirak = Convert.ToBoolean(DropDownListNamazaIstirak.SelectedValue);
                    C.EvdeTaziye = Convert.ToBoolean(DropDownListEvdeTaziye.SelectedValue);
                    C.DigerHizmetler = TextBoxDigerHizmetler.Text;
                    C.Aciklama = TextBoxAciklama.Text;
                    C.DuzenleyenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    C.DuzenlemeTarihi = DateTime.Now;
                    Veriler.SaveChanges();
                    Temizle();
                    GridViewVeriler.DataBind();
                    MultiView1.ActiveViewIndex = 0;
                    MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                }
                else
                {
                    C = new Cenazeler();
                    C.Ad = TextBoxAd.Text;
                    C.Soyad = TextBoxSoyad.Text;
                    C.CinsiyetId = Convert.ToInt32(DropDownListCinsiyetler.SelectedValue);
                    if (TextBoxResim.Text != string.Empty)
                    {
                        C.Resim = GorselBoyutlandir(TextBoxResim, KayitYeri, 175);
                    }
                    else
                    {
                        C.Resim = null;
                    }
                    C.DogumYeriIlId = Convert.ToInt32(DropDownListDogumYeriIller.SelectedValue);
                    C.YasadigiYerMahalleId = Convert.ToInt32(DropDownListYasadigiYerMahalleler.SelectedValue);
                    C.Yas = TextBoxYas.Text;
                    C.YakiniAd = TextBoxYakiniAd.Text;
                    C.YakiniSoyad = TextBoxYakiniSoyad.Text;
                    C.YakiniTel = TextBoxYakiniTelefon.Text;
                    if (RadDateTimePickerVefatTarihi.SelectedDate != null)
                    {
                        C.VefatTarihi = Convert.ToDateTime(RadDateTimePickerVefatTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        C.VefatTarihi = null;
                    }
                    C.VefatNedeniId = Convert.ToInt32(DropDownListVefatNedenleri.SelectedValue);
                    C.DefinYeriIlId = Convert.ToInt32(DropDownListDefinYeriIller.SelectedValue);
                    C.DefinZamaniId = Convert.ToInt32(DropDownListDefinZamanlari.SelectedValue);
                    if (RadDateTimePickerDefinTarihi.SelectedDate != null)
                    {
                        C.DefinTarihi = Convert.ToDateTime(RadDateTimePickerDefinTarihi.SelectedDate.Value);
                    }
                    else
                    {
                        C.DefinTarihi = null;
                    }
                    C.MeslekId = Convert.ToInt32(DropDownListMeslekler.SelectedValue);
                    C.Adres = TextBoxAdres.Text;
                    C.Yemek = Convert.ToBoolean(DropDownListYemek.SelectedValue);
                    C.Otobus = Convert.ToBoolean(DropDownListOtobus.SelectedValue);
                    C.NamazaIstirak = Convert.ToBoolean(DropDownListNamazaIstirak.SelectedValue);
                    C.EvdeTaziye = Convert.ToBoolean(DropDownListEvdeTaziye.SelectedValue);
                    C.DigerHizmetler = TextBoxDigerHizmetler.Text;
                    C.Aciklama = TextBoxAciklama.Text;
                    C.KayitTarihi = DateTime.Now;
                    C.KaydedenKullaniciId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    C.KaydetmeTarihi = DateTime.Now;
                    Veriler.AddToCenazeler(C);
                    Veriler.SaveChanges();
                    Temizle();
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
            if (!string.IsNullOrEmpty(Resim))
            {
                if (File.Exists(Server.MapPath(Resim)))
                {
                    File.Delete(Server.MapPath(Resim));
                }
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
                Cenazeler C = Veriler.Cenazeler.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                Guncelle(C);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Cenazeler C = Veriler.Cenazeler.Where(p => p.Id == Id).First();
                Veriler.Cenazeler.DeleteObject(C);
                Veriler.SaveChanges();
                GorselSil(C.Resim);
                GridViewVeriler.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
            }
        }

        private void Guncelle(Cenazeler C)
        {
            TextBoxAd.Text = C.Ad;
            TextBoxSoyad.Text = C.Soyad;
            DropDownListCinsiyetler.SelectedValue = C.CinsiyetId.Value.ToString();
            if (C.Resim != null)
            {
                ImageResim.Visible = true;
                ImageResim.ImageUrl = C.Resim;
                TextBoxResim.Text = C.Resim;
            }
            var Dogum = from p in Veriler.Ulkeler
                        join p1 in Veriler.Iller
                            on p.Id equals p1.UlkeId
                        where p1.Id == C.DogumYeriIlId
                        select new
                                   {
                                       UlkeId = p.Id,
                                       IlId = p1.Id,
                                   };
            UlkeleriVer(DropDownListDogumYeriUlkeler);
            DropDownListDogumYeriUlkeler.SelectedValue = Dogum.First().UlkeId.ToString();
            IlleriVer(DropDownListDogumYeriIller, Convert.ToInt32(Dogum.First().UlkeId.ToString()));
            DropDownListDogumYeriIller.SelectedValue = Dogum.First().IlId.ToString();
            var Yasam = from p in Veriler.Ulkeler
                        join p1 in Veriler.Iller
                            on p.Id equals p1.UlkeId
                        join p2 in Veriler.Ilceler
                            on p1.Id equals p2.IlId
                        join p3 in Veriler.Mahalleler
                            on p2.Id equals p3.IlceId
                        where p3.Id == C.YasadigiYerMahalleId
                        select new
                                   {
                                       UlkeId = p.Id,
                                       IlId = p1.Id,
                                       IlceId = p2.Id,
                                   };
            UlkeleriVer(DropDownListYasadigiYerUlkeler);
            DropDownListYasadigiYerUlkeler.SelectedValue = Yasam.First().UlkeId.ToString();
            IlleriVer(DropDownListYasadigiYerIller, Convert.ToInt32(Yasam.First().UlkeId.ToString()));
            DropDownListYasadigiYerIller.SelectedValue = Yasam.First().IlId.ToString();
            IlceleriVer(DropDownListYasadigiYerIlceler, Convert.ToInt32(Yasam.First().IlId.ToString()));
            DropDownListYasadigiYerIlceler.SelectedValue = Yasam.First().IlceId.ToString();
            MahalleleriVer(DropDownListYasadigiYerMahalleler, Convert.ToInt32(Yasam.First().IlceId.ToString()));
            DropDownListYasadigiYerMahalleler.SelectedValue = C.YasadigiYerMahalleId.Value.ToString();
            TextBoxYas.Text = C.Yas;
            TextBoxYakiniAd.Text = C.YakiniAd;
            TextBoxYakiniSoyad.Text = C.YakiniSoyad;
            TextBoxYakiniTelefon.Text = C.YakiniTel;
            if (C.VefatTarihi != null)
            {
                RadDateTimePickerVefatTarihi.SelectedDate = C.VefatTarihi.Value;
            }
            else
            {
                RadDateTimePickerVefatTarihi.SelectedDate = null;
            }
            VefatNedenleriVer(DropDownListVefatNedenleri);
            DropDownListVefatNedenleri.SelectedValue = C.VefatNedeniId.Value.ToString();
            var Defin = from p in Veriler.Ulkeler
                        join p1 in Veriler.Iller
                            on p.Id equals p1.UlkeId
                        where p1.Id == C.DefinYeriIlId
                        select new
                                   {
                                       UlkeId = p.Id,
                                       IlId = p1.Id,
                                   };
            UlkeleriVer(DropDownListDefinYeriUlkeler);
            DropDownListDefinYeriUlkeler.SelectedValue = Defin.First().UlkeId.ToString();
            IlleriVer(DropDownListDefinYeriIller, Convert.ToInt32(Defin.First().UlkeId.ToString()));
            DropDownListDefinYeriIller.SelectedValue = Defin.First().IlId.ToString();
            DropDownListDefinZamanlari.SelectedValue = C.DefinZamaniId.Value.ToString();
            if (C.DefinTarihi != null)
            {
                RadDateTimePickerDefinTarihi.SelectedDate = C.DefinTarihi.Value;
            }
            else
            {
                RadDateTimePickerDefinTarihi.SelectedDate = null;
            }
            DropDownListMeslekler.SelectedValue = C.MeslekId.Value.ToString();
            TextBoxAdres.Text = C.Adres;
            DropDownListYemek.SelectedValue = C.Yemek.Value.ToString();
            DropDownListOtobus.SelectedValue = C.Otobus.Value.ToString();
            DropDownListNamazaIstirak.SelectedValue = C.NamazaIstirak.Value.ToString();
            DropDownListEvdeTaziye.SelectedValue = C.EvdeTaziye.Value.ToString();
            TextBoxDigerHizmetler.Text = C.DigerHizmetler;
            TextBoxAciklama.Text = C.Aciklama;
            HiddenFieldId.Value = C.Id.ToString();
            LabelBaslik.Text = "Cenaze Düzenle";
        }
    }
}
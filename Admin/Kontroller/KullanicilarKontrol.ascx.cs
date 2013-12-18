using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class KullanicilarKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Kullanıcılar Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 2))
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
        }

        private void Temizle()
        {
            TextBoxEPosta.Text = string.Empty;
            TextBoxParola.Text = string.Empty;
            CheckBoxDurum.Checked = false;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        private void RolleriVer(CheckBoxList CheckBoxList)
        {
            var Roller = from p in Veriler.Roller
                         where p.Durum == true
                         orderby p.Id
                         select p;
            if (Roller.Count() != 0)
            {
                CheckBoxList.DataTextField = "RolAdi";
                CheckBoxList.DataValueField = "Id";
                CheckBoxList.DataSource = Roller;
                CheckBoxList.DataBind();
            }
        }

        protected void ImageButtonYeniEkle_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Temizle();
            RolleriVer(CheckBoxListRoller);
            LabelBaslik.Text = "Kullanıcı Ekle";
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Kullanicilar Kullanici;
                if (HiddenFieldId.Value != string.Empty)
                {
                    int KullaniciId = Convert.ToInt32(HiddenFieldId.Value);
                    Kullanicilar K =
                        Veriler.Kullanicilar.Where(p => p.Id != KullaniciId && p.EPosta == TextBoxEPosta.Text).
                            FirstOrDefault();
                    if (K == null)
                    {
                        Kullanici = Veriler.Kullanicilar.Where(p => p.Id == KullaniciId).FirstOrDefault();
                        Kullanici.EPosta = TextBoxEPosta.Text;
                        Kullanici.Parola = TextBoxParola.Text;
                        Kullanici.Durum = CheckBoxDurum.Checked;
                        Veriler.SaveChanges();
                        AktifRolleriKaydet(CheckBoxListRoller, Kullanici);
                        Temizle();
                        RolleriVer(CheckBoxListRoller);
                        GridViewKullanicilar.DataBind();
                        MultiView1.ActiveViewIndex = 0;
                        MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                    }
                    else
                    {
                        if (K.SilindiMi == true)
                        {
                            K.EPosta = TextBoxEPosta.Text;
                            K.Parola = TextBoxParola.Text;
                            K.Durum = CheckBoxDurum.Checked;
                            K.SilindiMi = false;
                            Veriler.SaveChanges();
                            AktifRolleriKaydet(CheckBoxListRoller, K);
                            Temizle();
                            RolleriVer(CheckBoxListRoller);
                            GridViewKullanicilar.DataBind();
                            MultiView1.ActiveViewIndex = 0;
                            MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                        }
                        else
                        {
                            MesajKontrol1.Mesaj(false,
                                                "Bu e-posta hesabı ile daha önce kullanıcı kayıt edilmiş. Başka bir e-posta hesabı giriniz.");
                        }
                    }
                }
                else
                {
                    Kullanicilar K = Veriler.Kullanicilar.Where(p => p.EPosta == TextBoxEPosta.Text).First();
                    if (K == null)
                    {
                        Kullanici = new Kullanicilar();
                        Kullanici.EPosta = TextBoxEPosta.Text;
                        Kullanici.Parola = TextBoxParola.Text;
                        Kullanici.Durum = CheckBoxDurum.Checked;
                        Kullanici.SilindiMi = false;
                        Veriler.AddToKullanicilar(Kullanici);
                        Veriler.SaveChanges();
                        AktifRolleriKaydet(CheckBoxListRoller, Kullanici);
                        Temizle();
                        RolleriVer(CheckBoxListRoller);
                        GridViewKullanicilar.DataBind();
                        MultiView1.ActiveViewIndex = 0;
                        MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                    }
                    else
                    {
                        if (K.SilindiMi == true)
                        {
                            K.EPosta = TextBoxEPosta.Text;
                            K.Parola = TextBoxParola.Text;
                            K.Durum = CheckBoxDurum.Checked;
                            K.SilindiMi = false;
                            Veriler.SaveChanges();
                            AktifRolleriKaydet(CheckBoxListRoller, K);
                            Temizle();
                            RolleriVer(CheckBoxListRoller);
                            GridViewKullanicilar.DataBind();
                            MultiView1.ActiveViewIndex = 0;
                            MesajKontrol2.Mesaj(true, "Kayıt edildi.");
                        }
                        else
                        {
                            MesajKontrol1.Mesaj(false,
                                                "Bu e-posta hesabı ile daha önce kullanıcı kayıt edilmiş. Başka bir e-posta hesabı giriniz.");
                        }
                    }
                }
            }
            catch (Exception Hata)
            {
                EnrollExceptionManager.ManageException(Hata, Request.RawUrl);
                MesajKontrol2.Mesaj(false, Hata.Message);
            }
        }

        private void AktifRolleriKaydet(CheckBoxList Roller, Kullanicilar Kullanici)
        {
            var KRoller = Veriler.KullaniciRolleri.Where(p => p.KullaniciId == Kullanici.Id).ToList();
            foreach (var Rol in KRoller)
            {
                KullaniciRolleri KRR = Veriler.KullaniciRolleri.Where(p => p.KullaniciId == Rol.KullaniciId).First();
                Veriler.DeleteObject(KRR);
                Veriler.SaveChanges();
            }
            foreach (ListItem Li in Roller.Items)
            {
                if (Li.Selected)
                {
                    AktifRolKaydet(Convert.ToInt32(Li.Value), Kullanici.Id);
                }
            }
        }

        private void AktifRolKaydet(int RolId, int KullaniciId)
        {
            KullaniciRolleri KR = new KullaniciRolleri();
            KR.KullaniciId = KullaniciId;
            KR.RolId = RolId;
            Veriler.AddToKullaniciRolleri(KR);
            Veriler.SaveChanges();
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Temizle();
        }

        protected void GridViewKullanicilar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Kullanicilar Kullanici = Veriler.Kullanicilar.Where(p => p.Id == Id).First();
                MultiView1.ActiveViewIndex = 1;
                KullaniciGuncelle(Kullanici);
            }
            else if (e.CommandName == "Sil")
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                Kullanicilar Kullanici = Veriler.Kullanicilar.Where(p => p.Id == Id).First();
                Kullanici.SilindiMi = true;
                var KRList = Veriler.KullaniciRolleri.Where(p => p.KullaniciId == Kullanici.Id).ToList();
                foreach (KullaniciRolleri KR in KRList)
                {
                    Veriler.KullaniciRolleri.DeleteObject(KR);
                }
                Veriler.SaveChanges();
                MesajKontrol2.Mesaj(true, "Kayıt silindi.");
                GridViewKullanicilar.DataBind();
                MultiView1.ActiveViewIndex = 0;
            }
        }

        private void KullaniciGuncelle(Kullanicilar Kullanici)
        {
            Temizle();
            HiddenFieldId.Value = Kullanici.Id.ToString();
            TextBoxEPosta.Text = Kullanici.EPosta;
            TextBoxParola.Text = Kullanici.Parola;
            CheckBoxDurum.Checked = Kullanici.Durum.Value;
            AktifRoller(Kullanici.Id);
            RequiredFieldValidator2.ValidationGroup = "g1";
            LabelBaslik.Text = "Kullanıcı Düzenle";
        }

        private void AktifRoller(int Id)
        {
            RolleriVer(CheckBoxListRoller);
            var AktifRoller = from p in Veriler.KullaniciRolleri
                              where p.KullaniciId == Id
                              select p;
            if (AktifRoller.Count() != 0)
            {
                foreach (var AktifRol in AktifRoller)
                {
                    for (int i = 0; i <= CheckBoxListRoller.Items.Count - 1; i++)
                    {
                        if (CheckBoxListRoller.Items[i].Value == AktifRol.RolId.Value.ToString())
                        {
                            CheckBoxListRoller.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
    }
}
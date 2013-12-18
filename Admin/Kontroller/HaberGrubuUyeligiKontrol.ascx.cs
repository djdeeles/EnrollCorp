using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.Admin.Kontroller
{
    public partial class HaberGrubuUyeligiKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected override void OnInit(EventArgs e)
        {
            Session["currentPath"] = "Haber Grubu Yönetimi";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Enroll.YetkiAlaniKontrol(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name), 8))
                {
                    MultiView2.ActiveViewIndex = 0;
                    DropDownListDurum.SelectedIndex = 0;
                    Temizle();
                }
                else
                {
                    MultiView2.ActiveViewIndex = 1;
                }
            }
            MesajKontrol2.Reset();
        }

        protected void DropDownListDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            Temizle();
            MultiView1.ActiveViewIndex = -1;
            switch (DropDownListDurum.SelectedValue)
            {
                case "Tumu":
                    GridViewHaberGrubuUyeligi.DataSourceID = "EntityDataSourceHaberGrubuUyeligi";
                    GridViewHaberGrubuUyeligi.DataBind();
                    break;

                case "AktifUyeler":
                    GridViewHaberGrubuUyeligi.DataSourceID = "EntityDataSourceHaberGrubuUyeligi";
                    EntityDataSourceHaberGrubuUyeligi.Where = "it.Durum=True";
                    GridViewHaberGrubuUyeligi.DataBind();
                    break;

                case "PasifUyeler":
                    GridViewHaberGrubuUyeligi.DataSourceID = "EntityDataSourceHaberGrubuUyeligi";
                    EntityDataSourceHaberGrubuUyeligi.Where = "it.Durum=False";
                    GridViewHaberGrubuUyeligi.DataBind();
                    break;
            }
        }

        private void Temizle()
        {
            TextBoxEPosta.Text = string.Empty;
            RadDatePickerKayitTarihi.SelectedDate = null;
            RadDatePickerIptalTarihi.SelectedDate = null;
            CheckBoxDurum.Checked = false;
            MesajKontrol1.Reset();
            MesajKontrol2.Reset();
        }

        protected void GridViewHaberGrubuUyeligi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guncelle")
            {
                Temizle();
                Guid Id = new Guid(e.CommandArgument.ToString());
                HaberGrubuUyeligi HGU = Veriler.HaberGrubuUyeligi.Where(o => o.Id == Id).First();
                HiddenFieldId.Value = Id.ToString();
                TextBoxEPosta.Text = HGU.EPosta;
                RadDatePickerKayitTarihi.SelectedDate = Convert.ToDateTime(HGU.KayitTarihi.Value.ToString("dd/MM/yyyy"));
                if (HGU.IptalTarihi != null)
                {
                    RadDatePickerIptalTarihi.SelectedDate =
                        Convert.ToDateTime(HGU.IptalTarihi.Value.ToString("dd/MM/yyyy"));
                }
                CheckBoxDurum.Checked = HGU.Durum.Value;
                MultiView1.ActiveViewIndex = 0;
            }
            else if (e.CommandName == "Sil")
            {
                Temizle();
                Guid Id = new Guid(e.CommandArgument.ToString());
                HaberGrubuUyeligi HGU = Veriler.HaberGrubuUyeligi.Where(o => o.Id == Id).First();
                Veriler.HaberGrubuUyeligi.DeleteObject(HGU);
                Veriler.SaveChanges();
                GridViewHaberGrubuUyeligi.DataBind();
                MultiView1.ActiveViewIndex = 0;
                MesajKontrol2.Mesaj(true, "Kayıt silindi");
            }
        }

        protected void ImageButtonKaydet_Click(object sender, ImageClickEventArgs e)
        {
            MesajKontrol1.Reset();
            try
            {
                Guid Id = new Guid(HiddenFieldId.Value);
                HaberGrubuUyeligi HGU = Veriler.HaberGrubuUyeligi.Where(o => o.Id == Id).First();
                HGU.EPosta = TextBoxEPosta.Text;
                HGU.KayitTarihi =
                    Convert.ToDateTime(RadDatePickerKayitTarihi.ValidationDate + " " + DateTime.Now.ToLongTimeString());
                if (RadDatePickerIptalTarihi.SelectedDate != null)
                {
                    HGU.IptalTarihi =
                        Convert.ToDateTime(RadDatePickerIptalTarihi.ValidationDate + " " +
                                           DateTime.Now.ToLongTimeString());
                }
                else
                {
                    HGU.IptalTarihi = null;
                }
                HGU.Durum = CheckBoxDurum.Checked;
                Veriler.SaveChanges();
                MultiView1.ActiveViewIndex = -1;
                GridViewHaberGrubuUyeligi.DataBind();
                MesajKontrol2.Mesaj(true, "Kayıt edildi.");
            }
            catch
            {
                MesajKontrol1.Mesaj(false, "Hata oluştu.");
            }
        }

        protected void ImageButtonIptal_Click(object sender, ImageClickEventArgs e)
        {
            Temizle();
            MultiView1.ActiveViewIndex = -1;
        }
    }
}